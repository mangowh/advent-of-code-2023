namespace Days;

public class Day1(string dataPath)
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