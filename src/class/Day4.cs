namespace Days;

class Day4(string dataPath) : Day(dataPath)
{
    private readonly StringSplitOptions splitFlags = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;

    public override string GetResultPart1()
    {        
        int sum = 0;

        foreach(var line in dataLines)
        {
            var lineArr = line.Split(":", splitFlags);
            var cardName = lineArr[0];
            var cardNum = lineArr[0].Split(" ", splitFlags)[1];

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
        int sum = 0;

        Dictionary<int,int> cards = new Dictionary<int, int>();

        foreach(var (line, index) in dataLines.Select((v, i)=>(v, i)))
        {
            cards.Add(index+1, 1);
        }

        foreach(var line in dataLines)
        {
            var lineArr = line.Split(":", splitFlags);
            var cardName = lineArr[0];
            var cardNum = int.Parse(lineArr[0].Split(" ", splitFlags)[1]);
            Console.WriteLine("\nCard: "+ cardNum);

            var cardsString = lineArr[1].Split("|", splitFlags);

            var winningCards = cardsString[0].Split(" ", splitFlags).Select(s => int.Parse(s));
            var playerCards = cardsString[1].Split(" ", splitFlags).Select(s => int.Parse(s));

            Console.WriteLine($"Winning Cards {cardNum}: {string.Join(" ", winningCards)}");
            Console.WriteLine($"Player Cards {cardNum}: {string.Join(" ", playerCards)}");
            
            var intersecting = playerCards.Where(n => winningCards.Contains(n));
            Console.WriteLine(intersecting.Count() + $" card intersecting: {string.Join(" ", intersecting)}");
            var count = intersecting.Count();
                
            cards.TryAdd(cardNum, 1);

            if (count > 0)
            {
                for(int i = cardNum + 1; i < cardNum + count + 1; i++)
                {
                    cards.TryAdd(i, 0);

                    cards[i] += cards[cardNum];

                    Console.WriteLine($"Adding to card num {i}");
                }
            }
        }

        foreach(var card in cards)
        {
            Console.WriteLine($"{string.Join(" ", card)}");
            sum += card.Value;
        }

        return sum.ToString();
    }
}