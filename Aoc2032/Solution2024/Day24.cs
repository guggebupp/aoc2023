




using System.Collections;
using System.Net.Http.Headers;

namespace Aoc2023.Solution2024
{
    internal class Day24
    {
        private static string day = "24";
        private static string fileName = "day" + day + ".txt";

        public static void solve()
        {
            solve1();
            solve2();
        }

        public static void solve1()
        {
            var total = 0;
            var lines = Util.readFile(fileName);
            var gates = new Dictionary<string, int>();      
            var operations = new Dictionary<string, List<string>>();
            var operationsOutPut = new Dictionary<string, string>();
            var outputs = new Dictionary<string, int>();
            
            ReadLines(lines, gates, operations, operationsOutPut);
            //ResolveGates(gates, operations, outputs);
            ResolveGates2(gates, operationsOutPut, outputs);
            /**foreach( var o in outputs.OrderBy(a => a.Key ) ) {
                Console.WriteLine(o.Key + ": " + o.Value);
            }**/

            var out2 = "" ;
            foreach (var o in outputs.Where(a => a.Key.StartsWith("z")).OrderBy(a => a.Key))
            {
                out2 += o.Value;
            }
            Console.WriteLine(out2);
            Console.WriteLine("Day{0}-1: {1}", day, getIntFromString(out2));
        }

        public static void solve2()
        {
            var total = 0;
            var lines = Util.readFile(fileName);
            var gates = new Dictionary<string, int>();
            var operations = new Dictionary<string, List<string>>();
            var operationsOutPut = new Dictionary<string, string>();
            var outputs = new Dictionary<string, int>();
            ReadLines(lines, gates, operations, operationsOutPut);
            double xValue = 0;
            var xValueString1 = "";
            gates.Where(a => a.Key.StartsWith("x")).ToList().ForEach(a => 
            { 
                if ( a.Value == 1)
                {
                    xValue += Math.Pow(2, Int32.Parse(a.Key.Substring(1, 2)));
                }
                xValueString1 = a.Value + xValueString1;
            }
            );
            Console.WriteLine("X: " + xValueString1);
            double yValue = 0;
            var yValueString1 = "";
            gates.Where(a => a.Key.StartsWith("y")).ToList().ForEach(a =>
            {
                if (a.Value == 1)
                {
                    yValue += Math.Pow(2, Int32.Parse(a.Key.Substring(1, 2)));
                }
                yValueString1 = a.Value + yValueString1;
            }
            );
            var yValue2 = yValue;            
            var yValueString2 = "";
            while (yValue2 > 0)
            {
                yValueString2 = (yValue2 % 2) + yValueString2;
                yValue2 -= (yValue2 % 2);
                yValue2 = yValue2 / 2;
            }
            Console.WriteLine("Y: " + yValueString1);
            Console.WriteLine(yValueString2);   
            var zvalue = xValue + yValue;
            var zValueString = "";
            while ( zvalue > 0)
            {
                zValueString = (zvalue % 2) + zValueString;
                zvalue -= (zvalue % 2);
                zvalue = zvalue / 2;
            }
            Console.WriteLine("Z: " + zValueString);
            ResolveGates2(gates, operationsOutPut, outputs);
            double zValue2 = 0;
            outputs.Where(a => a.Key.StartsWith("z")).ToList().ForEach(a =>
            {
                if (a.Value == 1)
                {
                    zValue2 += Math.Pow(2, Int32.Parse(a.Key.Substring(1, 2)));
                }
            }
            );
            Validate(gates, outputs, operationsOutPut, zValueString);
            //Replaceoperations(gates, operations, operationsOutPut);
            /**while(!Validate(gates, outputs))
            {
                var operationCount = operationsOutPut.Count;
                for (int level1 = 0; level1 < operationCount; level1++)
                {
                    for (int level2 = 1; level2 < operationCount; level2++)
                    {
                        for (int level3 = 0; level3 < operationCount; level3++)
                        {
                            for (int level4 = 1; level4 < operationCount; level4++)
                            {
                                for (int level5 = 0; level5 < operationCount; level5++)
                                {

                                    for (int level6 = 1; level6 < operationCount; level6++)
                                    {

                                        for (int level7 = 0; level7 < operationCount; level7++)
                                        {

                                            for (int level8 = 1; level8 < operationCount; level8++)
                                            {
                                                var levels = new List<int>();
                                                levels.Add(level1);
                                                levels.Add(level2);
                                                levels.Add(level3); 
                                                levels.Add(level4);
                                                levels.Add(level5);
                                                levels.Add(level6);
                                                levels.Add(level7);
                                                levels.Add(level8);
                                                if ( levels.Count() == levels.Distinct().Count())
                                                {
                                                    gates = new Dictionary<string, int>();
                                                    operations = new Dictionary<string, List<string>>();
                                                    operationsOutPut = new Dictionary<string, string>();
                                                    outputs = new Dictionary<string, int>();
                                                    ReadLines(lines, gates, operations, operationsOutPut);
                                                    SwitchOperations(levels, operationsOutPut);
                                                    ResolveGates2(gates, operationsOutPut, outputs);
                                                    if (Validate(gates, outputs))
                                                    {
                                                        foreach (var level in levels)
                                                        {
                                                            Console.WriteLine(level);
                                                        }
                                                        break;
                                                    }
                                                }
                                                
                                                
                                                

                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                
                
            }**/



            /**foreach ( var o in operationsOutPut)
            {
                Console.WriteLine(o.Key + " - " + o.Value);
            }**/

            Console.WriteLine("Day{0}-2: {1}", day, total);
        }
       

        private static void SwitchOperations(List<int> levels, Dictionary<string, string> operationsOutPut)
        {
            var count = 0;
            var replaceKeys = new Dictionary<int, string>();
            foreach (var operation in operationsOutPut)
            {
                if (levels.Contains(count))
                {
                    replaceKeys.Add(count, operation.Key);
                }
                count++;
            }
            for (int i = 0; i < levels.Count(); i = i + 2)
            {
                replaceKeys.TryGetValue(levels[i], out var key1);
                operationsOutPut.TryGetValue(key1, out var operation1);
                replaceKeys.TryGetValue(levels[i+1], out var key2);
                operationsOutPut.TryGetValue(key2, out var operation2);
                operationsOutPut[key1] = operation2;
                operationsOutPut[key2] = operation1;
            }
        }

        private static bool Validate(Dictionary<string, int> gates, Dictionary<string, int> outputs, Dictionary<string, string> operationsOutPut, string zvalue)
        {                        
            for (int index = 0; index < outputs.Where(a => a.Key.StartsWith("z")).Count(); index++) { 
                gates.TryGetValue("x" + (index < 10 ? "0" : "") + index, out var x);
                gates.TryGetValue("y" + (index < 10 ? "0" : "") + index, out var y);
                outputs.TryGetValue("z" + (index < 10 ? "0" : "") + index, out var z);
                operationsOutPut.TryGetValue("z" + (index < 10 ? "0" : "") + index, out var zOperation);
                if ((zvalue[45-index] + "") != ("" + z))
                {
                    Console.WriteLine("Error: p: {0} x: {1} y: {2} z: {3}, operation: {4}", index, x, y, z, zOperation);
                    //return false;
                }
            }
            return true;
        }

        private static void ReadLines(List<string> lines, Dictionary<string, int> gates, Dictionary<string, List<string>> operations, Dictionary<string, string> operationsOutPut)
        {
            foreach (var line in lines)
            {
                if (line != "")
                {
                    if (line.Contains(":"))
                    {
                        var p = line.Split(':');
                        gates.Add(p[0], Int32.Parse(p[1]));
                    }
                    else
                    {
                        var p = line.Split("->");
                        if (operations.ContainsKey(p[0]))
                        {
                            operations[p[0]].Add(p[1].Trim());
                        }
                        else
                        {
                            var t = new List<string>();
                            t.Add(p[1].Trim());
                            operations.Add(p[0], t);
                        }
                        operationsOutPut.Add(p[1].Trim(), p[0]);

                    }
                }
            }
        }

        private static void ResolveGates(Dictionary<string, int> gates, Dictionary<string, List<string>> operations, Dictionary<string, int> outputs)
        {
            var allSolved = false;
            while (!allSolved)
            {
                allSolved = true;
                foreach (var operation in operations)
                {
                    var opParts = operation.Key.Split(" ");
                    if (gates.TryGetValue(opParts[0], out var opIn1) && gates.TryGetValue(opParts[2], out var opIn2))
                    {
                        var op = opParts[1];
                        var output = 0;
                        if (op == "XOR")
                        {
                            output = opIn1 ^ opIn2;
                        }
                        else if (op == "AND")
                        {
                            output = opIn1 & opIn2;
                        }
                        else if (op == "OR")
                        {
                            output = opIn1 | opIn2;
                        }
                        foreach (var val in operation.Value)
                        {
                            if (gates.ContainsKey(val))
                            {
                                gates.TryGetValue(val, out var tt);
                                if (tt != output)
                                {
                                    Console.WriteLine("Error: K: {0}, V1: {1], V2: {2]", val, tt, output);
                                }
                            }
                            else
                            {
                                gates.Add(val, output);
                                outputs.Add(val, output);
                            }

                        }
                    }
                    else
                    {
                        allSolved = false;
                    }

                }
            }
        }

        private static void ResolveGates2(Dictionary<string, int> gates, Dictionary<string, string> operationOutPuts, Dictionary<string, int> outputs)
        {
            var allSolved = false;
            while (!allSolved)
            {
                allSolved = true;
                foreach (var operation in operationOutPuts)
                {
                    var opParts = operation.Value.Split(" ");
                    if (gates.TryGetValue(opParts[0], out var opIn1) && gates.TryGetValue(opParts[2], out var opIn2))
                    {
                        var op = opParts[1];
                        var output = 0;
                        if (op == "XOR")
                        {
                            output = opIn1 ^ opIn2;
                        }
                        else if (op == "AND")
                        {
                            output = opIn1 & opIn2;
                        }
                        else if (op == "OR")
                        {
                            output = opIn1 | opIn2;
                        }
                        if (gates.ContainsKey(operation.Key))
                        {
                            gates.TryGetValue(operation.Key, out var tt);
                            if (tt != output)
                            {
                                Console.WriteLine("Error: K: {0}, V1: {1], V2: {2]", operation.Key, tt, output);
                            }
                        }
                        else
                        {
                            gates.Add(operation.Key, output);
                            outputs.Add(operation.Key, output);
                        }
                    }
                    else
                    {
                        allSolved = false;
                    }

                }
            }
        }

        private static void Replaceoperations(Dictionary<string, int> gates, Dictionary<string, List<string>> operations, Dictionary<string, string> operationsOutPut)
        {
            foreach (var operation in operations) { 
                var parts = operation.Key.Split(" ");
                var opIn1 = parts[0];
                var opIn2 = parts[2];
                var op = parts[1];
                var changed = false;
                if (!opIn1.StartsWith("x") && !opIn1.StartsWith("y"))
                {
                    changed = true;
                    opIn1 = Replaceoperation(opIn1, operations, operationsOutPut);
                }
                if (!opIn2.StartsWith("x") && !opIn2.StartsWith("y"))
                {
                    changed = true;
                    opIn2 = Replaceoperation(opIn2, operations, operationsOutPut);
                }
                if (changed)
                {
                    foreach (var v in operation.Value)
                    {
                        operationsOutPut[v] = opIn1 + " " + op + " " + opIn2;
                    }
                }
                
                
            }
        }

        private static string Replaceoperation(string opIn, Dictionary<string, List<string>> operations, Dictionary<string, string> operationsOutPut)
        {
            operationsOutPut.TryGetValue(opIn, out var operation);
            var parts = operation.Split(" ");
            var opIn1 = parts[0];
            var opIn2 = parts[2];
            var op = parts[1];
            if (!opIn1.StartsWith("x") && !opIn1.StartsWith("y"))
            {
                opIn1 = Replaceoperation(opIn1, operations, operationsOutPut);
            }
            if (!opIn2.StartsWith("x") && !opIn2.StartsWith("y"))
            {
                opIn2 = Replaceoperation(opIn2, operations, operationsOutPut);
            }
            return opIn1 + " " + op + " " + opIn2;
        }

        private static double getIntFromString(string indata)
        {
            double value = 0;

            for ( int i = 0; i < indata.Length; i++ ) { 
                if (indata[i] == '1')
                {
                    value += Math.Pow(2, i);
                }
            }
            return value;
            
            
        }


    }
}
