
namespace Day2
{    
    class Day2(string dataPath) : Day(dataPath)
    {
        public Dictionary<string, int> cubesRequirements = new Dictionary<string, int> {
            {"red", 12},
            {"green", 13},
            {"blue", 14}
        };

        public override string GetResultPart1()
        {
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

        public override string GetResultPart2() {
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

}