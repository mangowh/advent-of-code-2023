namespace Days;

public abstract class Day
{
    public readonly string dataPath;
    public readonly List<string> dataLines;

    public Day(string dataPath)
    {
        this.dataPath = dataPath;
        
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

    public abstract string GetResultPart1();
    public abstract string GetResultPart2();
}