using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023.Solution
{
    internal class Day9
    {
        private static string fileName = "day9.txt";
        public static void solve1()
        {
            var total = 0;
            var lines = readFile();
            foreach (var line in lines)
            {
                var lineValues = new Dictionary<int, List<int>>();
                var values = new List<int>();
                line.Split(' ').ToList().ForEach(a => values.Add(int.Parse(a)));
                var index = 0;
                lineValues.Add(index, values);
                var currentValues = values;
                while (currentValues.Any(a => a != 0))
                {
                    index++;
                    var nextValues = new List<int>();
                    for (int i = 0; i < currentValues.Count - 1; i++)
                    {
                        var nexVal = currentValues[i + 1] - currentValues[i];
                        nextValues.Add(nexVal);
                    }
                    lineValues.Add(index, nextValues);
                    currentValues = nextValues;
                }
                var nextTotal = 0;
                for (int i = lineValues.Count - 1; i >= 0; i--)
                {
                    nextTotal += lineValues[i].Last();
                }
                total += nextTotal;
            }
            Console.WriteLine("Day9-1: " + total);
        }


        public static void solve2()
        {
            var total = 0;
            var lines = readFile();
            foreach (var line in lines)
            {
                var lineValues = new Dictionary<int, List<int>>();
                var values = new List<int>();
                line.Split(' ').ToList().ForEach(a => values.Add(int.Parse(a)));
                var index = 0;
                lineValues.Add(index, values);
                var currentValues = values;
                while (currentValues.Any(a => a != 0))
                {
                    index++;
                    var nextValues = new List<int>();
                    for (int i = 0; i < currentValues.Count - 1; i++)
                    {
                        var nexVal = currentValues[i + 1] - currentValues[i];
                        nextValues.Add(nexVal);
                    }
                    lineValues.Add(index, nextValues);
                    currentValues = nextValues;
                }
                var nextTotal = 0;
                for (int i = lineValues.Count - 1; i >= 0; i--)
                {
                    var next = lineValues[i].First();

                    nextTotal = next - nextTotal;


                }
                total += nextTotal;
            }

            Console.WriteLine("Day9-2: " + total);
        }

        private static List<string> readFile()
        {
            var lines = new List<string>();
            using (var sr = new StreamReader("../../../Input/"+fileName))
            {
                var line = sr.ReadLine();
                while (line != null)
                {
                    lines.Add(line);
                    line = sr.ReadLine();
                }

            }
            return lines;
        }

    }
}
