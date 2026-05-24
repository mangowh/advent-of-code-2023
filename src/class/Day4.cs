namespace Days;

class Day4(string dataPath) : Day(dataPath)
{
    public override string GetResultPart1()
    {
        var splitFlags = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;
        
        int sum = 0;

        foreach(var line in dataLines)
        {
            var lineArr = line.Split(":", splitFlags);
            var cardName = lineArr[0];
            var cardNum = lineArr[0].Split(" ")[1];

            var cards = lineArr[1].Split("|", splitFlags);

            var winningCards = cards[0].Split(" ", splitFlags).Select(s => int.Parse(s));
            var playerCards = cards[1].Split(" ", splitFlags).Select(s => int.Parse(s));

            var intersecting = playerCards.Where(n => winningCards.Contains(n));

            var count = intersecting.Count();

            var cardValue = 0;
            if(count > 0)
            {
                cardValue = (int)Math.Pow(2, count - 1);
            }
            sum += cardValue;

            Console.WriteLine("");
            Console.WriteLine($"Winning Cards {cardNum}: {string.Join(" ", winningCards)}");
            Console.WriteLine($"Player Cards {cardNum}: {string.Join(" ", playerCards)}");
            Console.WriteLine(intersecting.Count() + $" card intersecting: {string.Join(" ", intersecting)}");
            Console.WriteLine($"Added {cardValue} points");
        }

        return sum.ToString();
    }

    public override string GetResultPart2()
    {
        throw new NotImplementedException();
    }
}