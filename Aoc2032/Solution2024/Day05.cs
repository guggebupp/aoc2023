





namespace Aoc2023.Solution2024
{
    internal class Day05
    {
        private static string day = "05";
        private static string fileName = "day" + day + ".txt";

        public static void solve1()
        {
            var total = 0;
            var lines = Util.readFile(fileName);
            var rules = new List<string>();
            var validLines = new List<string>();
            foreach (var line in lines)
            {
                if (line != "")
                {
                    if ( line.Contains("|"))
                    {
                        rules.Add(line);
                    } else
                    {
                        if (isValid(line, rules)) { 
                            validLines.Add(line);
                        }

                    }
                
                    
                }
            }
            foreach (var line in validLines)
            {
                
                var num = line.Split(',');
                
                total += Int32.Parse(num[num.Length / 2]);
            }
            Console.WriteLine("Day05-1: " + total);
        }
        

        public static void solve2()
        {
            var total = 0;
            var lines = Util.readFile(fileName);
            var rules = new List<string>();
            var inValidLines = new List<string>();
            foreach (var line in lines)
            {
                if (line != "")
                {
                    if (line.Contains("|"))
                    {
                        rules.Add(line);
                    }
                    else
                    {
                        if (!isValid(line, rules))
                        {
                            inValidLines.Add(line);
                        }

                    }


                }
            }
            foreach (var line in inValidLines)
            {
                //var correctedLine = correctLine(line, rules);                
                var correctedLine = correct(line.Split(','), rules);

                total += Int32.Parse(correctedLine[correctedLine.Length / 2]);
            }
            Console.WriteLine("Day05-2: " + total);
        }

        private static bool isValid(string line, List<string> rules)
        {
            var numbers = line.Split(',');
            for (int i = 0; i < numbers.Length; i++)
            {
                if ((i + 1) < numbers.Length)
                {

                    for (int j = i + 1; j < numbers.Length; j++)
                    {
                        if (!rules.Any(a => a.Split('|')[0] == numbers[i] && a.Split('|')[1] == numbers[j]))
                        {
                            return false;
                        }
                    }
                }
                for (int j = 0; j < i; j++)
                {
                    if (!rules.Any(a => a.Split('|')[1] == numbers[i] && a.Split('|')[0] == numbers[j]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static string[] correctLine(string line, List<string> rules)
        {
            var numbers = line.Split(',');
            var failingPos = isValid2(numbers, rules);
            var lastFail = -1;
            var lastForward = false ;
            var moveLength = 1;
            var count = 0;
            while (failingPos != -1 && count++ < 100 )
            {
                //Console.WriteLine(failingPos);
                //moveLength = lastFail == failingPos ? (moveLength+1) : 1;
                if (failingPos +moveLength < numbers.Length )
                {
                    lastFail = failingPos;
                    lastForward = true;
                    var ett = numbers[failingPos];
                    var tva = numbers[failingPos + moveLength];
                    numbers[failingPos] = tva;
                    numbers[failingPos + moveLength] = ett;
                    failingPos = isValid2(numbers, rules);
                }
                else if (failingPos-moveLength >= 0) {
                    lastFail = failingPos;
                    lastForward = false ;
                    var ett = numbers[failingPos];
                    var tva = numbers[failingPos - moveLength];
                    numbers[failingPos] = tva;
                    numbers[failingPos - moveLength] = ett;
                    failingPos = isValid2(numbers, rules);
                } else
                {
                    Console.Write("FAIL");
                    failingPos = -1;
                }                
            }

            if (count > 98) {
                var corrected = correct(numbers, rules);
                if (corrected != null) { 
                    numbers = corrected;
                }                
            }
            
            return numbers.ToArray();
        }
        private static int isValid2(string[] numbers, List<string> rules)
        {            
            for (int i = 0; i < numbers.Length; i++)
            {
                if ((i + 1) < numbers.Length)
                {

                    for (int j = i + 1; j < numbers.Length; j++)
                    {
                        if (!rules.Any(a => a.Split('|')[0] == numbers[i] && a.Split('|')[1] == numbers[j]))
                        {
                            return i;
                        }
                    }
                }
                for (int j = 0; j < i; j++)
                {
                    if (!rules.Any(a => a.Split('|')[1] == numbers[i] && a.Split('|')[0] == numbers[j]))
                    {
                        return j;
                    }
                }
            }
            return -1;
        }

        private static string[] correct(string[] numbers, List<string> rules)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                if ((i + 1) < numbers.Length)
                {

                    for (int j = i + 1; j < numbers.Length; j++)
                    {
                        if (!rules.Any(a => a.Split('|')[0] == numbers[i] && a.Split('|')[1] == numbers[j]))
                        {
                            //SWitchable?
                            if (rules.Any(a => a.Split('|')[0] == numbers[j] && a.Split('|')[1] == numbers[i]))
                            {
                                var switchedNumber = new List<string>();
                                foreach (var item in numbers)
                                {
                                    switchedNumber.Add(item);
                                }
                                switchedNumber[i] = numbers[j];
                                switchedNumber[j] = numbers[i];
                                return correct(switchedNumber.ToArray(), rules);

                            } else
                            {
                                return null;
                            }
                                
                        }
                    }
                }
                for (int j = 0; j < i; j++)
                {
                    if (!rules.Any(a => a.Split('|')[1] == numbers[i] && a.Split('|')[0] == numbers[j]))
                    {
                        //SWitchable?
                        if (rules.Any(a => a.Split('|')[0] == numbers[i] && a.Split('|')[1] == numbers[j]))
                        {
                            var switchedNumber = new List<string>();
                            foreach (var item in numbers)
                            {
                                switchedNumber.Add(item);
                            }
                            switchedNumber[i] = numbers[j];
                            switchedNumber[j] = numbers[i];
                            return correct(switchedNumber.ToArray(), rules);

                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }            
            return numbers;
        }
    }
}
