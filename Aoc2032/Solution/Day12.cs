using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023.Solution
{
    internal class Day12
    {
        private static string day = "12";
        private static string fileName = "day" + day + ".txt";
        public static void solve1()
        {
            long total = 0;
            var lines = readFile();
            long ans = 0;
            foreach (var line in lines)
            {
                if (line == "")
                {
                    continue;
                }
                var grid = line.Split(' ')[0].Trim();
                var nums1 = line.Split(' ')[1];
                var nums = new List<int>();
                nums1.Split(',').ToList().ForEach(a => nums.Add(int.Parse(a)));
                var arrangements = 1;
                var sts = new Dictionary<Pair3, long>();
                var p = new Pair3() { key = 0, length = 0 };
                sts.Add(p, 1);
                for (int i = 0; i < grid.Length; i++)
                {
                    //Console.WriteLine(grid[i]);
                    var new_sts = new Dictionary<Pair3, long>();
                    var poss_chars = new List<char>();
                    poss_chars.Add(grid[i]);
                    if (grid[i] == '?')
                    {
                        poss_chars.Add('.');
                        poss_chars.Add('#');
                    }
                    foreach (var item in sts)
                    //for ( int it=sts.Count-1; it >= 0; it--) 
                    {
                        //    var item = sts.ElementAt(it);
                        var sofar = item.Key.key;
                        var idx = item.Key.length;
                        var v = item.Value;
                        //Console.WriteLine(sofar);
                        foreach (var ch in poss_chars)
                        {
                            if (idx == nums.Count())
                            {
                                if (ch == '.')
                                {
                                    var p2 = new Pair3() { key = sofar, length = idx };
                                    //if (hasKey(new_sts, p2))
                                    if (new_sts.ContainsKey(p2))
                                    {
                                        new_sts[p2] += v;
                                    }
                                    else
                                    {
                                        new_sts.Add(p2, v);
                                    }

                                }
                            }
                            else
                            {
                                if (sofar == nums[idx])
                                {
                                    if (ch == '.')
                                    {
                                        var p2 = new Pair3() { key = 0, length = idx + 1 };
                                        if (new_sts.ContainsKey(p2))
                                        {
                                            new_sts[p2] += v;
                                        }
                                        else
                                        {
                                            new_sts.Add(p2, v);
                                        }
                                    }

                                }
                                else
                                {
                                    if (ch == '.' && sofar == 0)
                                    {
                                        var p2 = new Pair3() { key = sofar, length = idx };
                                        if (new_sts.ContainsKey(p2))
                                        {
                                            new_sts[p2] += v;
                                        }
                                        else
                                        {
                                            new_sts.Add(p2, v);
                                        }
                                    }
                                    if (ch == '#')
                                    {
                                        var p2 = new Pair3() { key = sofar + 1, length = idx };
                                        if (new_sts.ContainsKey(p2))
                                        {
                                            new_sts[p2] += v;
                                        }
                                        else
                                        {
                                            new_sts.Add(p2, v);
                                        }
                                    }
                                }
                            }
                        }

                    }
                    sts = new_sts;
                    /**foreach(  var s in sts )
                    {
                        Console.WriteLine(s.Key.key + ": "  +s.Key.length + ": " + s.Value);
                    }
                    Console.WriteLine("-");**/


                }
                var p3 = new Pair3() { key = 0, length = nums.Count };
                var p4 = new Pair3() { key = nums[nums.Count - 1], length = nums.Count - 1 };
                ans += (sts.ContainsKey(p3) ? sts[p3] : 0) + (sts.ContainsKey(p4) ? sts[p4] : 0);
                //Console.WriteLine(ans);

            }

            Console.WriteLine("Day" + day + "-1: " + ans);
        }

        public static void solve2()
        {
            long total = 0;
            var lines = readFile();
            long ans = 0;
            foreach (var line in lines)
            {
                if (line == "")
                {
                    continue;
                }
                var grid = line.Split(' ')[0].Trim();
                var nums1 = line.Split(' ')[1];
                var nums = new List<int>();
                var numCop = new List<int>();
                nums1.Split(',').ToList().ForEach(a => nums.Add(int.Parse(a)));
                nums1.Split(',').ToList().ForEach(a => numCop.Add(int.Parse(a)));
                grid = grid + "?" + grid + "?" + grid + "?" + grid + "?" + grid;
                nums.AddRange(numCop);
                nums.AddRange(numCop);
                nums.AddRange(numCop);
                nums.AddRange(numCop);
                var arrangements = 1;
                var sts = new Dictionary<Pair3, long>();
                var p = new Pair3() { key = 0, length = 0 };
                sts.Add(p, 1);
                for (int i = 0; i < grid.Length; i++)
                {
                    //Console.WriteLine(grid[i]);
                    var new_sts = new Dictionary<Pair3, long>();
                    var poss_chars = new List<char>();
                    poss_chars.Add(grid[i]);
                    if (grid[i] == '?')
                    {
                        poss_chars.Add('.');
                        poss_chars.Add('#');
                    }
                    foreach (var item in sts)
                    //for (int it = sts.Count - 1; it >= 0; it--)
                    {
                        //var item = sts.ElementAt(it);
                        var sofar = item.Key.key;
                        var idx = item.Key.length;
                        var v = item.Value;
                        //Console.WriteLine(sofar);
                        foreach (var ch in poss_chars)
                        {
                            if (idx == nums.Count())
                            {
                                if (ch == '.')
                                {
                                    var p2 = new Pair3() { key = sofar, length = idx };
                                    if (hasKey(new_sts, p2))
                                    {
                                        new_sts[p2] += v;
                                    }
                                    else
                                    {
                                        new_sts.Add(p2, v);
                                    }

                                }
                            }
                            else
                            {
                                if (sofar == nums[idx])
                                {
                                    if (ch == '.')
                                    {
                                        var p2 = new Pair3() { key = 0, length = idx + 1 };
                                        if (hasKey(new_sts, p2))
                                        {
                                            new_sts[p2] += v;
                                        }
                                        else
                                        {
                                            new_sts.Add(p2, v);
                                        }
                                    }

                                }
                                else
                                {
                                    if (ch == '.' && sofar == 0)
                                    {
                                        var p2 = new Pair3() { key = sofar, length = idx };
                                        if (hasKey(new_sts, p2))
                                        {
                                            new_sts[p2] += v;
                                        }
                                        else
                                        {
                                            new_sts.Add(p2, v);
                                        }
                                    }
                                    if (ch == '#')
                                    {
                                        var p2 = new Pair3() { key = sofar + 1, length = idx };
                                        if (hasKey(new_sts, p2))
                                        {
                                            new_sts[p2] += v;
                                        }
                                        else
                                        {
                                            new_sts.Add(p2, v);
                                        }
                                    }
                                }
                            }
                        }

                    }
                    sts = new_sts;
                    /**foreach(  var s in sts )
                    {
                        Console.WriteLine(s.Key.key + ": "  +s.Key.length + ": " + s.Value);
                    }
                    Console.WriteLine("-");**/


                }
                var p3 = new Pair3() { key = 0, length = nums.Count };
                var p4 = new Pair3() { key = nums[nums.Count - 1], length = nums.Count - 1 };
                ans += (sts.ContainsKey(p3) ? sts[p3] : 0) + (sts.ContainsKey(p4) ? sts[p4] : 0);
                //Console.WriteLine(ans);

            }

            Console.WriteLine("Day" + day + "-2: " + ans);
        }

        private static bool hasKey(Dictionary<Pair3, long> new_sts, Pair3 p2)
        {
            foreach (var s in new_sts)
            {
                if (s.Key.key == p2.key && s.Key.length == p2.length)
                {
                    return true;
                }
            }
            return false;
        }



        private static List<string> readFile()
        {
            var lines = new List<string>();
            using (var sr = new StreamReader("../../../Input/" + fileName))
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

    internal class Pair3 : IEquatable<Pair3>
    {
        public int key;
        public int length;

        public bool Equals(Pair3? other)
        {
            return (key == other.key && length == other.length);
        }

        public override int GetHashCode()
        {
            return key * 1000 + length;
        }
    }
}
