using System.IO;
using System.Text.RegularExpressions;

internal static class Utils {
    public static string Reverse( string s )
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }
}

internal class Day1()    
{
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

    public int? checkStartsWithNumberString(string line) {
        foreach(var (word, number) in numbers) {
            if(line.StartsWith(word)) {
                return number;
            }
        }

        return null;
    }

    public int? getEndingNumber(string line) {
        foreach(var (word, number) in numbers) {
            if(line.EndsWith(word)) {
                return number;
            }
        }

        return null;
    }
}

class Program
{
    static void Main()
    {
        string dataPath = "data.txt";
        var day1 = new Day1();
        var data = day1.ReadFile(dataPath);
        
        string[] splittedLines = data.Split("\n");

        Console.WriteLine(string.Join(", ", splittedLines));

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
                    var startingNumber = day1.checkStartsWithNumberString(rest);
                    if (startingNumber != null) {
                        foundNums[i].l = (int) startingNumber;
                        break;
                    }
                }
            }

            for (int j = line.Length - 1; j >= 0; j--) {
                var c = line[j];
                var rest = line[..j];
                Console.WriteLine(rest);
                if(char.IsDigit(c)) {   
                    foundNums[i].r = (int) char.GetNumericValue(c);
                    break;
                } else {
                    var endingNumber = day1.getEndingNumber(rest);
                    if (endingNumber != null) {
                        foundNums[i].r = (int) endingNumber;
                        break;
                    }
                }
            }
        }

        string output = string.Join("\n", foundNums.Select(p => $"{p.l}{p.r}"));
        Console.WriteLine(output);

        int sum = 0;
        foreach(var num in foundNums) {
            var combined = int.Parse($"{num.l}{num.r}");
            sum += combined;
        }
        Console.WriteLine(sum);
    }
}
