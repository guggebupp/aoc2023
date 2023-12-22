using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023.Solution
{
    internal class Day15
    {
        private static string day = "15";
        private static string fileName = "day" + day + ".txt";
        public static void solve1()
        {
            long total = 0;
            var lines = Util.readFile(fileName);
            total = lines[0].Split(',').Sum(a => GetHash(a));


            Console.WriteLine("Day" + day + "-1: " + total);
        }

        private static int GetHash(string part)
        {
            var val = 0;
            foreach (char a in part)
            {
                val += a;
                val *= 17;
                val = val % 256;
            }
            return val;
        }

        public static void solve2()
        {
            long total = 0;
            var lines = Util.readFile(fileName);
            var parts = lines[0].Split(',');
            var boxes = new Dictionary<int, List<string>>();

            foreach (var part in parts)
            {
                if (part.Contains("="))
                {
                    var boxid = GetHash(part.Split("=")[0]);
                    if (boxes.ContainsKey(boxid))
                    {
                        var p = boxes[boxid];
                        // Add or replace
                        var ind = p.FindIndex(x => x.Contains(part.Split('=')[0]));
                        if (ind != -1)
                        {
                            p[ind] = part;
                        }
                        else
                        {
                            p.Add(part);
                        }
                    }
                    else
                    {
                        boxes.Add(boxid, new List<string> { part });
                    }
                }
                if (part.Contains('-'))
                {
                    var key = part.Split("-")[0];
                    var boxid = GetHash(key);

                    if (boxes.ContainsKey(boxid))
                    {
                        var p = boxes[boxid];
                        var ind = p.FindIndex(x => x.Contains(key));
                        if (ind != -1)
                        {
                            p.RemoveAt(ind);
                        }

                    }
                }
            }
            total = boxes.Keys.Sum(x => boxes[x].Sum(a => (x + 1) * int.Parse(a.Split("=")[1]) * (boxes[x].IndexOf(a) + 1)));


            Console.WriteLine("Day" + day + "-2: " + total);
        }
        
    }
}
