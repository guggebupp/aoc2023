




using System.Security.Cryptography.X509Certificates;
using System.Windows.Markup;

namespace Aoc2023.Solution2024
{

    
    internal class Day22
    {
        private static string day = "22";
        private static string fileName = "day" + day + ".txt";

        public static void solve()
        {
            solve1();
            solve2();
        }

        public static void solve1()
        {
            long total = 0;
            var lines = Util.readFile(fileName);
            var inputs = new List<int>();
            foreach (var line in lines)
            {
                if (line != "")
                {
                    inputs.Add(Int32.Parse(line));
                                   }
            }
            var prune = 16777216;
            foreach (var input in inputs)
            {
                long res = input;
                for ( int i = 0; i < 2000; i++ )
                {
                    //1. *64 + XOR prune
                    long temp = res * 64;
                    res = temp ^ res;
                    res = res % prune;
                    //2. /32
                    temp = res / 32;
                    res = temp ^ res;
                    res = res % prune;
                    //3. *2048 + XOR
                    temp = res * 2048;
                    res = temp ^ res;
                    res = res % prune;
                    
                    
                }
                //Console.WriteLine(input + ": " + res);
                total += res;

            }
            Console.WriteLine("Day{0}-1: {1}", day, total);
        }
        

        public static void solve2()
        {
            long total = 0;
            var lines = Util.readFile(fileName);
            var inputs = new List<int>();
            foreach (var line in lines)
            {
                if (line != "")
                {
                    inputs.Add(Int32.Parse(line));
                }
            }
            var prune = 16777216;
            var priceChanges = new Dictionary<int, string>();
            var priceChanges2 = new Dictionary<int, List<int>>();
            var priceCurrent = new Dictionary<int, List<long>>();
            foreach (var input in inputs)
            {
                long res = input;
                priceChanges.Add(input, ",");
                priceChanges2.Add(input, new List<int>());
                priceCurrent.Add(input, new List<long>());
                priceChanges[input] = priceChanges[input]+0;
                priceChanges2[input].Add(0);
                priceCurrent[input].Add(input%10);
                var previousPrice = input % 10;
                for (int i = 0; i < 2000; i++)
                {
                    //1. *64 + XOR prune
                    long temp = res * 64;
                    res = temp ^ res;
                    res = res % prune;
                    //2. /32
                    temp = res / 32;
                    res = temp ^ res;
                    res = res % prune;
                    //3. *2048 + XOR
                    temp = res * 2048;
                    res = temp ^ res;
                    res = res % prune;
                    var price = (int)res % 10;
                    priceChanges[input] = priceChanges[input] + "," + (price - previousPrice);
                    priceChanges2[input].Add((price - previousPrice));
                    priceCurrent[input].Add(price % 10);
                    previousPrice = price;

                }
                

            }
            var bananasDict = new Dictionary<int, Dictionary<string, int>>();
            var patterns = GetSequenceBananas(priceChanges2, priceCurrent, bananasDict);
            Console.WriteLine("p: " + patterns.Count);
            var maxBananas = 0;
            var testedStrings = new List<string>();
            foreach (var pattern in patterns) {
                var testString = pattern;
                var bananas = 0;
                foreach (var banana in bananasDict.Values)
                {
                    if (banana.ContainsKey(testString))
                    {
                        bananas += banana[testString];
                    }
                }
                //var bananas = getBananas(priceChanges, priceCurrent, testString, priceChanges2);
                if (bananas > maxBananas)
                {
                    Console.WriteLine(testString + " - " + bananas);
                    maxBananas = bananas;
                }
            }
            /**
            for (int a1 = -9; a1 <= 9; a1++)
            {
                //Console.WriteLine("Testing: " + a1);
                for (int a2 = -9; a2 <= 9; a2++)
                {
                    for (int a3 = -9; a3 <= 9; a3++)
                    {
                        for (int a4 = -9; a4 <= 9; a4++)
                        {
                            var testString = "," + a1 + "," + a2 + "," + a3 + "," + a4;
                            var bananas = 0;
                            foreach (var banana in bananasDict.Values) {
                                if (banana.ContainsKey(testString)) {
                                    bananas += banana[testString];
                                }
                            }
                            //var bananas = getBananas(priceChanges, priceCurrent, testString, priceChanges2);
                            if (bananas > maxBananas)
                            {
                                Console.WriteLine(testString + " - " + bananas);
                                maxBananas = bananas;
                            }
                        }
                    }
                }
            }
            **/
            /**
            var maxBananas = 0;
            var testedStrings = new List<string>();
            for (int a1 = -9; a1 <= 9; a1++) {
                //Console.WriteLine("Testing: " + a1);
                for (int a2 = -9; a2 <= 9; a2++)
                {
                    for (int a3 = -9; a3 <= 9; a3++)
                    {
                        for (int a4 = -9; a4 <= 9; a4++)
                        {
                            var testString = "," + a1 + "," + a2 + "," + a3 + "," + a4;                            
                            var bananas = getBananas(priceChanges, priceCurrent, testString, priceChanges2);
                            if (bananas > maxBananas)
                            {
                                Console.WriteLine(testString + " - " + bananas);
                                maxBananas = bananas;
                            }
                        }
                    }
                }
            }
            **/

            /**
            foreach (var test in priceChanges2)
            {
                for (int i = 3; i < test.Value.Count; i++)
                {
                    var values = test.Value;
                    var testString = values[i - 3] + "," + values[i - 2] + "," + values[i - 1] + "," + values[i - 0];                    
                    if (!testedStrings.Contains(testString))
                    {
                        var bananas = getBananas(priceChanges, priceCurrent, testString);
                        if (bananas > maxBananas)
                        {
                            Console.WriteLine(testString + " - " + bananas);
                            maxBananas = bananas;
                        }
                        testedStrings.Add(testString);
                    }


                }
            }**/

            total = maxBananas;

            Console.WriteLine("Day{0}-2: {1}", day, total);
        }

        private static List<string> GetSequenceBananas(Dictionary<int, List<int>> priceChanges2, Dictionary<int, List<long>> priceCurrent, Dictionary<int, Dictionary<string, int>> bananasDict)
        {
            var patterns = new List<string>();
            foreach (var monkey in priceChanges2)
            {
                var key = monkey.Key;
                var values = monkey.Value;
                var monkeyDict = new Dictionary<string, int>();
                for (int i = 3; i < values.Count; i++)
                {
                    var testString = values[i - 3] + "," + values[i - 2] + "," + values[i - 1] + "," + values[i - 0];
                    if (!monkeyDict.ContainsKey(testString))
                    {
                        monkeyDict.Add(testString, (int)priceCurrent[key][i]);
                    }
                    if ( !patterns.Contains(testString) )
                    {
                        patterns.Add(testString);
                    }
                    
                }
                bananasDict.Add(key, monkeyDict);
                Console.WriteLine(priceChanges2.Count() + " - " + bananasDict.Count());
            }
            return patterns;
        }

        private static int getBananas(Dictionary<int, string> priceChanges, Dictionary<int, List<long>> priceCurrent, string testString, Dictionary<int, List<int>> priceChanges2)
        {
            long bananas = 0;
            foreach (var test in priceChanges) {
                if (test.Value.Contains(testString)) {
                    
                    var val = test.Value;                    
                    var index = val.IndexOf(testString);
                    var t = val.Substring(0, index).Split(',').Count()+2;
                    bananas += priceCurrent[test.Key][t];
                    if ( testString == "1,2,-6,6")
                    {
                        Console.WriteLine(test.Key + ": " + t + " - " + priceCurrent[test.Key][t] + " - " + priceChanges2[test.Key][t-3] + " - " + priceChanges2[test.Key][t - 1] + " - " + priceChanges2[test.Key][t - 2] + " - " + priceChanges2[test.Key][t - 0]);
                    }
                    if (testString == "-2,1,-1,3")
                    {
                        Console.WriteLine(test.Key + ": " + t + " - " + priceCurrent[test.Key][t] + " - " + priceChanges2[test.Key][t - 3] + " - " + priceChanges2[test.Key][t - 1] + " - " + priceChanges2[test.Key][t - 2] + " - " + priceChanges2[test.Key][t - 0]);
                    }

                }
            }
            return (int)bananas;
        }
    }
}
