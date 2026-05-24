using System.Reflection;

using Days;

class Program
{
    static void Main(string[] args)
    {
        string dayNum = args[0];
        string dataPath = args[1];

        var type = Assembly.GetExecutingAssembly().GetType($"Days.Day{dayNum}");

        if(type == null)
        {
            throw new Exception("Type not found");
        }

        var day = (Day?)Activator.CreateInstance(type, dataPath); ;
        
        if(day == null)
        {
            throw new Exception("Error creating day class");
        }
        
        Console.WriteLine("Results:");
        Console.WriteLine("Part 1: " + day.GetResultPart1());
        // Console.WriteLine("Part 2: " + day.GetResultPart2());
    }
}
