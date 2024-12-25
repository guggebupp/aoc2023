




namespace Aoc2023.Solution2024
{
    internal class Day02
    {
        
        public static void solve1()
        {
            var total = 0;            
            using (var sr = new StreamReader("../../../Input2024/day02.txt"))
            {
                var line = sr.ReadLine();
                var lineNumber = 1;
                while (line != null)
                {
                    var numbers = new List<int>();
                    line.Split(' ').ToList().ForEach(a => numbers.Add(int.Parse(a)));                    
                    
                    if (allIncreasing(numbers) && allDifferOneOrThree(numbers))
                    {                        
                        total++;
                    }

                    if (allDecreasing(numbers) && allDifferOneOrThree(numbers))
                    {                        
                        total++;
                    }


                    lineNumber++;
                    line = sr.ReadLine();                    
                }
            }
            Console.WriteLine("Day02-1: " + total);
        }
        

        public static void solve2()
        {
            var total = 0;
            using (var sr = new StreamReader("../../../Input2024/day02.txt"))
            {
                var line = sr.ReadLine();                
                while (line != null)
                {
                    var numbers = new List<int>();
                    line.Split(' ').ToList().ForEach(a => numbers.Add(int.Parse(a)));                    
                    if (allDifferOneOrThreeDamp(numbers, line))
                    {                        
                        total++;
                    }
                                        
                    line = sr.ReadLine();
                }
            }
            Console.WriteLine("Day02-2: " + total);
        }

        private static bool allDifferOneOrThree(List<int> numbers)
        {            
            for (int i = 1; i < numbers.Count; i++)
            {
                var res = Math.Abs(numbers[i - 1] - numbers[i]);
                if (!(res >=1 && res <=3))
                {                    
                    return false;                    
                }
            }
            return true;
        }

        private static bool allDifferOneOrThreeDamp(List<int> numbers, string line)
        {
            if ( !allDifferOneOrThree(numbers) )
            {                
                for (int i = 0; i < numbers.Count; i++)
                {
                    var newTest = new List<int>();
                    numbers.ForEach(x => newTest.Add(x));
                    newTest.RemoveAt(i);
                    if (allDifferOneOrThree(newTest))
                    {
                        var res = allIncreasing(newTest) || allDecreasing(newTest);
                        if ( res)
                        {                            
                            return true;
                        }
                            
                        
                    }
                }
                return false;
            }
            return allIncreasingDamp(numbers, line) || allDecreasingDamp(numbers, line);
        }

        private static bool allIncreasingDamp(List<int> numbers, string line)
        {
            if ( !allIncreasing(numbers) )
            {
                for (int i = 0; i < numbers.Count; i++)
                {
                    var newTest = new List<int>();
                    numbers.ForEach(x => newTest.Add(x));
                    newTest.RemoveAt(i);
                    if (allIncreasing(newTest))
                    {                       
                        var res = allDifferOneOrThree(newTest);
                        if (res)
                        {                            
                            return true;
                        }
                    }
                }
                return false;
            }
            return true;
        }

        private static bool allDecreasingDamp(List<int> numbers, string line)
        {
            if (!allDecreasing(numbers))
            {
                for (int i = 0; i < numbers.Count; i++)
                {
                    var newTest = new List<int>();
                    numbers.ForEach(x => newTest.Add(x));
                    newTest.RemoveAt(i);
                    if (allDecreasing(newTest))
                    {
                        var res = allDifferOneOrThree(newTest);
                        if (res)
                        {                            
                            return true;
                        }
                    }
                }
                return false;
            }
            return true;
        }

        private static bool allIncreasing(List<int> numbers)
        {            
            for (int i = 1; i < numbers.Count; i++)
            {
                if(numbers[i-1] <= numbers[i])
                {                    
                    
                        return false;                    
                }
            }
            return true;
        }

        private static bool allDecreasing(List<int> numbers)
        {            
            for (int i = 1; i < numbers.Count; i++)
            {
                if (numbers[i - 1] >= numbers[i])
                {                    
                   return false;                    
                }
            }
            return true;

        }
        
    }
}
