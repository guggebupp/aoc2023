




namespace Aoc2023.Solution2024
{
    internal class Day07
    {
        private static string day = "07";
        private static string fileName = "day" + day + ".txt";

        public static void solve1()
        {
            double total = 0;
            var lines = Util.readFile(fileName);
            foreach (var line in lines)
            {
                if (line != "")
                {
                    var correct = false;
                    var testValue = double.Parse(line.Split(':')[0]);
                    var inpuValues = new List<double>();
                    foreach (var item in line.Split(':')[1].Split(" "))
                    {
                        if ( item != "")
                        {
                            inpuValues.Add(float.Parse(item));
                        }
                        
                    }                    
                    //Testa+
                    double newTest = 0;
                    newTest = inpuValues[0];
                    newTest = addpendSum(inpuValues, 1, testValue, newTest);                    
                    if ( newTest != -1)
                    {
                        //Test *
                        newTest = 0;
                        newTest = inpuValues[0];
                        newTest = addpendMul(inpuValues, 1, testValue, newTest);                        
                    }                    
                    if ( newTest == -1)
                    {                        
                        total += testValue;
                    }
                    
                    


                }
            }
            Console.WriteLine("Day07-1: " + total );
        }

        public static void solve2()
        {
            double total = 0;
            var lines = Util.readFile(fileName);
            var enabled = true;
            foreach (var line in lines)
            {
                if (line != "")
                {
                    var correct = false;
                    var testValue = double.Parse(line.Split(':')[0]);
                    var inpuValues = new List<double>();
                    foreach (var item in line.Split(':')[1].Split(" "))
                    {
                        if (item != "")
                        {
                            inpuValues.Add(float.Parse(item));
                        }

                    }
                    //Testa+
                    double newTest = 0;
                    newTest = inpuValues[0];
                    newTest = addpendSum(inpuValues, 1, testValue, newTest, true);
                    if (newTest != -1)
                    {
                        //Test *
                        newTest = 0;
                        newTest = inpuValues[0];
                        newTest = addpendMul(inpuValues, 1, testValue, newTest, true);
                    }
                    if (newTest != -1)
                    {
                        //Test *
                        newTest = 0;
                        newTest = inpuValues[0];
                        newTest = addpendPipe(inpuValues, 1, testValue, newTest, true);
                    }
                    if (newTest == -1)
                    {                        
                        total += testValue;
                    }




                }
            }
            Console.WriteLine("Day07-2: " + total);
        }

        private static double addpendSum(List<double> inpuValues, int i, double testValue, double currentValue, bool usePipe = false)
        {
            if (currentValue == -1)
            {
                return currentValue;
            }
            if ( i < inpuValues.Count)
            {
                currentValue += inpuValues[i];
            }
            if (i == inpuValues.Count && currentValue == testValue) {
                //Hittad
                return -1;
            }
            for (int j = i; j < inpuValues.Count; j++)
            {
                var currentBak = currentValue;
                //Testa +
                currentValue = addpendSum(inpuValues, j + 1, testValue, currentValue, usePipe);                
                if (currentValue != -1)
                {
                    //Testa *
                    currentValue = addpendMul(inpuValues, j + 1, testValue, currentBak, usePipe);
                }
                if (currentValue != -1 && usePipe)
                {
                    //Testa pipe
                    currentValue = addpendPipe(inpuValues, j + 1, testValue, currentBak, usePipe);
                }

            }
            return currentValue;
        }

        private static double addpendMul(List<double> inpuValues, int i, double testValue, double currentValue, bool usePipe = false)
        {
            if (i < inpuValues.Count)
            {
                currentValue *= inpuValues[i];
            }
            if (i == inpuValues.Count && currentValue == testValue)
            {
                //Hittad
                return -1;
            }
            for (int j = i; j < inpuValues.Count; j++)
            {
                var currentBak = currentValue;
                //Testa +
                currentValue = addpendSum(inpuValues, j + 1, testValue, currentValue, usePipe);               
                if (currentValue != -1)
                {
                    //Testa *
                    currentValue = addpendMul(inpuValues, j + 1, testValue, currentBak, usePipe);
                }
                if (currentValue != -1 && usePipe)
                {
                    //Testa pipe
                    currentValue = addpendPipe(inpuValues, j + 1, testValue, currentBak, usePipe);
                }

            }
            return currentValue;
        }

        private static double addpendPipe(List<double> inpuValues, int i, double testValue, double currentValue, bool usePipe = false)
        {
            if (i < inpuValues.Count)
            {
                currentValue = currentValue * Math.Pow(10, inpuValues[i].ToString().Length) + inpuValues[i];
            }
            if (i == inpuValues.Count && currentValue == testValue)
            {
                //Hittad
                return -1;
            }
            for (int j = i; j < inpuValues.Count; j++)
            {
                var currentBak = currentValue;
                //Testa +
                currentValue = addpendSum(inpuValues, j + 1, testValue, currentValue, usePipe);               
                if (currentValue != -1)
                {
                    //Testa *
                    currentValue = addpendMul(inpuValues, j + 1, testValue, currentBak, usePipe);
                }
                if (currentValue != -1 && usePipe)
                {
                    //Testa pipe
                    currentValue = addpendPipe(inpuValues, j + 1, testValue, currentBak, usePipe);
                }
            }
            return currentValue;
        }

       
        
        
    }
}
