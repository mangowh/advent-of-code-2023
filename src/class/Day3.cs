namespace Day3;

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