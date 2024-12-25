




using System.Runtime.ExceptionServices;
using static System.Formats.Asn1.AsnWriter;

namespace Aoc2023.Solution2024
{
    internal class Day11
    {
        private static string day = "11";
        private static string fileName = "day" + day + ".txt";        
        private static Dictionary<double, Dictionary<double, Double>> stoneMap2;

        public static void solve1()
        {
            var total = 0;
            var lines = Util.readFile(fileName);
            var stones = new List<Double>();
            foreach (var line in lines)
            {
                if (line != "")
                {
                    var stone = line.Split(" ");
                    foreach (var item in stone)
                    {
                        stones.Add(Double.Parse(item));
                    }
                }
            }            
            stoneMap2 = new Dictionary<double, Dictionary<double, Double>>();            
            Console.WriteLine("Day11-1: " + blink(stones, 25, 0));
        }                               

        public static void solve2()
        {
            var total = new Double();
            total = 0;
            var lines = Util.readFile(fileName);
            var stones = new List<Double>();
            foreach (var line in lines)
            {
                if (line != "")
                {
                    var stone = line.Split(" ");
                    foreach (var item in stone)
                    {
                        stones.Add(Double.Parse(item));
                    }
                }
            }
            stoneMap2 = new Dictionary<double, Dictionary<double, Double>>();
            total = blink(stones, 75, 0);            
                               
            Console.WriteLine("Day11-2: " + total);
        }

        private static double blink(List<Double> stones, int step, int currentStep)
        {
            var res = new Double();
            res = 0;

            foreach (var stone in stones)
            {
                res += handleStone(stone, step, currentStep);

            }
            return res;
        }

        private static double handleStone(double stone, int step, int currentStep)
        {
            var res = new Double();
            res = 0;
            if (stoneMap2.ContainsKey(stone) && stoneMap2.Where(a => a.Key == stone).Select(a => a.Value).First().ContainsKey(step - currentStep - 1))
            {
                res += stoneMap2.Where(a => a.Key == stone).Select(a => a.Value).First().Where(a => a.Key == (step - currentStep - 1)).Select(a => a.Value).First();
            }
            else
            {
                var newStonesStone = new List<Double>();
                newStonesStone.Add(stone);
                var length = stone.ToString().Length;
                if (stone == 0)
                {
                    if ((currentStep + 1) < step)
                    {
                        var newNewStone = new List<Double>();
                        newNewStone.Add(1);
                        res += blink(newNewStone, step, currentStep + 1);
                    }
                    else
                    {
                        res += 1;
                    }
                }
                else if ((length % 2) == 0)
                {
                    var stoneString = stone.ToString();
                    var stone1 = stoneString.Substring(0, (length / 2));
                    var stone2 = stoneString.Substring((length / 2), (length / 2));
                    if ((currentStep + 1) < step)
                    {
                        var newNewStone = new List<Double>();
                        newNewStone.Add(Double.Parse(stone1));
                        newNewStone.Add(Double.Parse(stone2));
                        res += blink(newNewStone, step, currentStep + 1);
                    }
                    else
                    {
                        res += 2;
                    }

                }
                else
                {

                    if ((currentStep + 1) < step)
                    {
                        var newNewStone = new List<Double>();
                        newNewStone.Add(stone * 2024);
                        res += blink(newNewStone, step, currentStep + 1);
                    }
                    else
                    {
                        res += 1;
                    }
                }
                if (stoneMap2.ContainsKey(stone))
                {
                    var x = stoneMap2.GetValueOrDefault(stone);
                    x.Add(step - currentStep - 1, res);
                }
                else
                {
                    var x = new Dictionary<double, double>();
                    x.Add(step - currentStep - 1, res);
                    stoneMap2.Add(stone, x);
                }
            }
            return res;
        }

    }
}
