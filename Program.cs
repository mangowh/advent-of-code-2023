class Program
{
    static void Main(string[] args)
    {
        string dataPath = args[0];

        var day = new Day3.Day3(dataPath);
        Console.WriteLine(day.GetResultPart1());
    }
}
