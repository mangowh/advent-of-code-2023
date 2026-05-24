using System.Globalization;
using System.IO;
using System.Runtime.InteropServices.Marshalling;
using System.Text.RegularExpressions;

using Point = (int x, int y);

internal class Day1(string dataPath)
{
    private string dataPath = dataPath;

    // TODO use StreamReader directly in other Day1 methods
    public string ReadFile(string file)
    {
        using StreamReader reader = new(file);

        return reader.ReadToEnd();
    }

    public List<(string, int)> numbers = new List<(string, int)> {
            ("one", 1), ("two", 2), ("three", 3),
            ("four", 4), ("five", 5), ("six", 6),
            ("seven", 7), ("eight", 8), ("nine", 9)
    };

    public int? GetStartingNumber(string line) {
        foreach(var (word, number) in numbers) {
            if(line.StartsWith(word)) {
                return number;
            }
        }

        return null;
    }

    public int? GetEndingNumber(string line) {
        foreach(var (word, number) in numbers) {
            if(line.EndsWith(word)) {
                return number;
            }
        }

        return null;
    }

    public string GetResult() {
        var data = this.ReadFile(this.dataPath);

        string[] splittedLines = data.Split("\n");

        var linesCount = splittedLines.Count() - 1;
        (int l, int r)[] foundNums = new (int, int)[linesCount];
        for (int i = 0; i < linesCount; i++) {
            var line = splittedLines[i];

            for (int k = 0; k < line.Length; k++) {
                var c = line[k];
                var rest = line[k..];
                if(char.IsDigit(c)) {
                    foundNums[i].l = (int) char.GetNumericValue(c);
                    break;
                } else {
                    var startingNumber = this.GetStartingNumber(rest);
                    if (startingNumber != null) {
                        foundNums[i].l = (int) startingNumber;
                        break;
                    }
                }
            }

            for (int j = line.Length - 1; j >= 0; j--) {
                var c = line[j];
                var rest = line[..j];
                if(char.IsDigit(c)) {
                    foundNums[i].r = (int) char.GetNumericValue(c);
                    break;
                } else {
                    var endingNumber = this.GetEndingNumber(rest);
                    if (endingNumber != null) {
                        foundNums[i].r = (int) endingNumber;
                        break;
                    }
                }
            }
        }

        string output = string.Join("\n", foundNums.Select(p => $"{p.l}{p.r}"));

        int sum = 0;
        foreach(var num in foundNums) {
            var combined = int.Parse($"{num.l}{num.r}");
            sum += combined;
        }

        return sum.ToString();
    }
}

internal class Day2(string dataPath) {
    private string dataPath = dataPath;

    public Dictionary<string, int> cubesRequirements = new Dictionary<string, int> {
        {"red", 12},
        {"green", 13},
        {"blue", 14}
    };

    public string GetResult() {
        int sum = 0;

        using (StreamReader sr = new StreamReader(this.dataPath))
        {
            string? line;
            while((line = sr.ReadLine()) != null) {
                var parts = line.Split(":");
                var (gameName, gameData) = (parts[0].Trim(), parts[1].Trim());
                var extractions = gameData.Split(";");

                bool gameIsPossible = true;

                Console.WriteLine(gameName);
                foreach(var extraction in extractions) {
                    var extractedColors = extraction.Trim().Split(", ");

                    foreach(var extractedColor in extractedColors) {
                        var extractedColorSplitted = extractedColor.Trim().Split(" ");
                        var (extractedColorCount, extractedColorName) = (extractedColorSplitted[0].Trim(), extractedColorSplitted[1].Trim());
                        Console.WriteLine(extractedColorName + ": " + extractedColorCount);

                        if(int.Parse(extractedColorCount) > cubesRequirements[extractedColorName]) {
                            Console.WriteLine($"{gameName} impossible because {extractedColorName} ({extractedColorCount}) is higher then {cubesRequirements[extractedColorName]}");
                            gameIsPossible = false;
                            break;
                        }
                    }
                    Console.WriteLine(";");
                }
                Console.WriteLine("\n");

                if(gameIsPossible) {
                    var gameNameParts = gameName.Split(" ");
                    sum += int.Parse(gameNameParts[1]);
                }
            }
        }

        return sum.ToString();
    }

    public string GetResultPart2() {
        int sumOfPowers = 0;

        using (StreamReader sr = new StreamReader(this.dataPath))
        {
            string? line;
            while((line = sr.ReadLine()) != null) {
                var parts = line.Split(":");
                var (gameName, gameData) = (parts[0].Trim(), parts[1].Trim());
                var extractions = gameData.Split(";");

                var maxPerColor = new Dictionary<string, int> {
                    {"red", int.MinValue},
                    {"green", int.MinValue},
                    {"blue", int.MinValue}
                };

                Console.WriteLine(gameName);
                foreach(var extraction in extractions) {
                    var extractedColors = extraction.Trim().Split(", ");

                    foreach(var extractedColor in extractedColors) {
                        var extractedColorSplitted = extractedColor.Trim().Split(" ");
                        var (extractedColorCount, extractedColorName) = (extractedColorSplitted[0].Trim(), extractedColorSplitted[1].Trim());
                        Console.WriteLine(extractedColorName + ": " + extractedColorCount);

                        var colorCount = int.Parse(extractedColorCount);
                        if(colorCount > maxPerColor[extractedColorName]) {
                            maxPerColor[extractedColorName] = colorCount;
                        }
                    }
                    Console.WriteLine(";");
                }
                Console.WriteLine("\n");

                var power = maxPerColor["red"] * maxPerColor["green"] * maxPerColor["blue"];
                sumOfPowers += power;
            }
        }

        return sumOfPowers.ToString();
    }
}

abstract class Day(string dataPath)
{
    protected string dataPath = dataPath;

    public abstract string GetResultPart1();
    public abstract string GetResultPart2();
}

class NumberChar
{
    public char c;
    public int x;
    public int y;

    public Point[] GetAdiacents()
    {
        List<Point> res = [];
        Point[] dirs = { 
            ( -1, -1 ), ( 0, -1 ), ( +1, -1 ),
            ( -1,  0 ),            ( +1,  0 ),
            ( -1, +1 ), ( 0, +1 ), ( +1, +1 )
        };
    
        foreach(var dir in dirs)
        {
            res.Add(new Point(x + dir.x, y + dir.y));
        }

        return res.ToArray();
    }

    public Point ToPoint()
    {
        return new Point(x, y);
    }

    public override string ToString()
    {
        return $"{c}:({x},{y})";
    }
}

class NumberFound(List<NumberChar> numbers)
{
    public List<NumberChar> numbers = numbers;

    public Point[] GetAdiancents()
    {
        return this.numbers.SelectMany((n) => n.GetAdiacents()).Distinct().ToArray();
    }

    public override string ToString()
    {
        return string.Join(", ", this.numbers.Select((n) => n.ToString()));
    }

    public int ToInt()
    {
        return int.Parse(string.Join("", this.numbers.Select((n) => n.c)));
    }
}


internal class Day3: Day
{
    public bool IsASpecialSymbol(char c)
    {
        return c != '.' && !char.IsLetterOrDigit(c);    
    }

    private List<string> dataLines;

    public Day3(string dataPath): base(dataPath)
    {
        this.dataLines = new List<string>();
        using (StreamReader sr = new StreamReader(this.dataPath))
        {
            string? line;
            while((line = sr.ReadLine()) != null)
            {
                dataLines.Add(line);
            }
        }
    }
    public override string GetResultPart1()
    {

        bool recording = false;
        List<NumberChar> recordingChars = [];
        List<NumberFound> recordedNumbers = [];

        for(int y = 0; y < dataLines.Count(); y++)
        {
            var row = dataLines[y];

            for(int x = 0; x < row.Count(); x++)
            {
                var currentChar = row[x];
                
                recording = char.IsDigit(currentChar);

                if(recording)
                {
                    recordingChars.Add(new NumberChar
                    {
                        c=currentChar,
                        x=x,
                        y=y
                    });
                } else if(recordingChars.Count() > 0)
                {
                    recordedNumbers.Add(new NumberFound(recordingChars));
                    recordingChars = [];
                }
            }

            if(recordingChars.Count > 0)
            {
                recordedNumbers.Add(new NumberFound(recordingChars));
                recordingChars = [];
            }
        }

        int sum = 0;
        foreach(var number in recordedNumbers)
        {
            foreach(var adjacent in number.GetAdiancents())
            {
                if (adjacent.y >= 0 && adjacent.y < dataLines.Count() &&
                    adjacent.x >= 0 && adjacent.x < dataLines[0].Count())
                {
                    if(this.IsASpecialSymbol(dataLines[adjacent.y][adjacent.x]))
                    {
                        sum += number.ToInt();
                        break;
                    }
                }
            }
        }

        return sum.ToString();
    }

    public override string GetResultPart2()
    {
        throw new NotImplementedException();
    }
}

class Program
{
    static void Main()
    {
        string dataPath = "C:/Users/Stefano/Developer/dotnet/advent-of-code-2023/data/data_day3.txt";
        var day = new Day3(dataPath);
        Console.WriteLine(day.GetResultPart1());
    }
}
