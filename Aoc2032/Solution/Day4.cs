


namespace Aoc2023.Solution
{
    internal class Day4
    {
        public static void solve1()
        {
            var total = 0.0;            
            using (var sr = new StreamReader("../../../Input/scratch.txt"))
            {
                var line = sr.ReadLine();
                var lineNumber = 1;
                while (line != null)
                {
                    var winningNumbers = new List<int>();
                    line.Split(':')[1].Split("|")[0].Split(" ").Where(a => a != "").ToList().ForEach(a => winningNumbers.Add(int.Parse(a)));
                    var noOfWins = line.Split(':')[1].Split("|")[1].Split(" ").Count(a => a != "" && winningNumbers.Contains(int.Parse(a)));

                    var points = noOfWins > 0 ? Math.Pow(2, (noOfWins - 1)) : 0;


                    total += points;

                    line = sr.ReadLine();
                    lineNumber++;
                }
            }
            Console.WriteLine("Day4-1: " + total);
        }

        public static void solve2()
        {
            var total = 0;
            var cardsmap = new Dictionary<int, int>();
            using (var sr = new StreamReader("../../../Input/scratch.txt"))
            {
                var line = sr.ReadLine();
                var lineNumber = 1;
                while (line != null)
                {
                    var winningNumbers = new List<int>();
                    line.Split(':')[1].Split("|")[0].Split(" ").Where(a => a != "").ToList().ForEach(a => winningNumbers.Add(int.Parse(a)));
                    var points = line.Split(':')[1].Split("|")[1].Split(" ").Count(a => a != "" && winningNumbers.Contains(int.Parse(a)));

                    var numberOfCurrentCard = 1;
                    if (cardsmap.ContainsKey(lineNumber))
                    {
                        numberOfCurrentCard += cardsmap[lineNumber];
                    }
                    for (int i = 1; i <= points; i++)
                    {
                        if (cardsmap.ContainsKey(lineNumber + i))
                        {
                            cardsmap[lineNumber + i] = cardsmap[lineNumber + i] + numberOfCurrentCard;
                        }
                        else
                        {
                            cardsmap.Add(lineNumber + i, numberOfCurrentCard);
                        }
                    }

                    total += numberOfCurrentCard;

                    line = sr.ReadLine();
                    lineNumber++;
                }
            }
            Console.WriteLine("Day4-2: " + total);
        }
    }
}
