using System.IO;

internal class Day1()    
{
    public string ReadFile(string file)
    {
        using StreamReader reader = new(file);

        return reader.ReadToEnd();
    }
}


class Program
{
    static void Main()
    {
        var day1 = new Day1();
        var data = day1.ReadFile("data.txt");
        // Console.WriteLine(data);
        
        string[] splittedLines = data.Split("\n");

        var linesCount = splittedLines.Count() - 1;
        (int l, int r)[] foundNums = new (int, int)[linesCount];
        for (int i = 0; i < linesCount; i++) {
            var line = splittedLines[i];
            // Console.WriteLine(line);
            for (int k = 0; k < line.Length; k++) {
                var c = line[k];
                if(char.IsDigit(c)) {   
                    foundNums[i].l = (int) char.GetNumericValue(c);
                    break;
                }
            }

            for (int j = line.Length - 1; j >= 0; j--) {
                var c = line[j];
                if(char.IsDigit(c)) {   
                    foundNums[i].r = (int) char.GetNumericValue(c);
                    break;
                }
            }
        }

        // string output = string.Join("\n", foundNums.Select(p => $"{p.l}{p.r}"));
        // File.WriteAllText("output.txt", output);

        int sum = 0;
        foreach(var num in foundNums) {
            var combined = int.Parse($"{num.l}{num.r}");
            sum += combined;
        }
        Console.WriteLine(sum);
    }
}
