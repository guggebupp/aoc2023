





using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Security.Cryptography.X509Certificates;

namespace Aoc2023.Solution2024
{
    internal class Day23
    {
        private static string day = "23";
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
            var path = new Dictionary<string, List<string>>();
            foreach (var line in lines)
            {
                if (line != "")
                {
                    var pair = line.Split('-');
                    if ( path.ContainsKey(pair[0]) )
                    {
                        path[pair[0]].Add(pair[1]);
                    } else
                    {
                        var l = new List<string>();
                        l.Add(pair[1]);
                        path.Add(pair[0], l);
                    }
                    if (path.ContainsKey(pair[1]))
                    {
                        path[pair[1]].Add(pair[0]);
                    }
                    else
                    {
                        var l = new List<string>();
                        l.Add(pair[0]);
                        path.Add(pair[1], l);
                    }
                }
            }
            var matched = new List<string>();
            foreach (var p in path)
            {
                foreach (var l in p.Value)
                {
                    if ( path.ContainsKey (l) )
                    {
                        path.TryGetValue(l, out var p2);
                        foreach( var l2 in p2)
                        {
                            if (path.ContainsKey(l2))
                            {
                                path.TryGetValue(l2, out var p3);
                                if (p3.Contains(p.Key))
                                {
                                    var match = new List<string>();
                                    match.Add(p.Key);
                                    match.Add(l);
                                    match.Add(l2);
                                    if ( match.Any(a => a.StartsWith('t')))
                                    {
                                        match = match.OrderBy(a => a).ToList();
                                        var m = "";
                                        foreach (var l3 in match)
                                        {
                                            m += "," + l3;
                                        }
                                        matched.Add(m);
                                        Console.WriteLine("Match: {0} - {1} - {2}", match[0], match[1], match[2]);
                                    }
                                    
                                }
                            }
                        }
                    }
                }
            }
            Console.WriteLine("------------------------");
            foreach (var m2 in matched.Distinct().OrderBy(a => a).ToList())
            {
                Console.WriteLine("MatchedD: {0}", m2);
                total++;
            }
            Console.WriteLine("Day{0}-1: {1}", day, total);
        }
        

        public static void solve2()
        {
            var total = -1;
            var lines = Util.readFile(fileName);
            var path = new Dictionary<string, List<string>>();            
            foreach (var line in lines)
            {
                if (line != "")
                {
                    var pair = line.Split('-');                    
                    if (path.ContainsKey(pair[0]))
                    {
                        path[pair[0]].Add(pair[1]);
                    }
                    else
                    {
                        var l = new List<string>();
                        l.Add(pair[1]);
                        path.Add(pair[0], l);
                    }
                    if (path.ContainsKey(pair[1]))
                    {
                        path[pair[1]].Add(pair[0]);
                    }
                    else
                    {
                        var l = new List<string>();
                        l.Add(pair[0]);
                        path.Add(pair[1], l);
                    }
                }
            }            
            var test = new List<string>();
            test.Add("ka");
            test.Add("co");
            test.Add("de");
            test.Add("ta");
            Console.WriteLine("Valid: " + Valid(test, path));

            var testLength = 0;
            var bestPath = new List<string>();
            var failedPath = new List<string>();
            foreach (var p in path)
            {
                var currentPath = new List<string>();
                currentPath.Add(p.Key);
                Console.WriteLine(p.Key);
                
                var res = TestPath(path, currentPath, p.Key, bestPath, failedPath);
                
            }
            Console.WriteLine("--------------------------");
            foreach (var tt in bestPath.OrderBy(a => a).ToList()) 
                {
                    Console.Write(tt + ",");
                }
                Console.WriteLine();
            

            Console.WriteLine("Day{0}-2: {1}", day, total);
        }

        private static bool TestPath(Dictionary<string, List<string>> path, List<string> currentPath, string currentValue, List<string> bestPath, List<string> failedPath)
        {
            var res = 0;
            path.TryGetValue(currentValue, out var nextValues);
           
            var ok = true;
            var testPathAsString = "";
            currentPath.OrderBy(a => a).ToList().ForEach(a => testPathAsString += "," + a);
            if (failedPath.Contains(testPathAsString))
            {
                return false;
            }
            foreach (var test in currentPath)
            {
                foreach (var test2 in currentPath.Where(a => a != test).ToList())
                {
                    if (!path.Any(a => a.Key == test && path.Any(b => b.Key == test2 && b.Value.Contains(test))))
                    {
                        
                        ok = false;
                        break;
                    }
                }
            }
            var anyOk = false;
            if (!ok)
            {
                failedPath.Add(testPathAsString);
                return false;
            } else             
            {                
                foreach (var value in nextValues.Where(a => !currentPath.Contains(a)).ToList())
                {

                    if (ok)
                    {
                        var nextPath = new List<string>();
                        nextPath.AddRange(currentPath);
                        if (nextPath.Count > bestPath.Count)
                        {
                            foreach (var tt in nextPath.OrderBy(a => a).ToList())
                            {
                                Console.Write(tt + ",");
                            }
                            Console.WriteLine();
                            res = nextPath.Count;
                            bestPath.Clear();
                            bestPath.AddRange(nextPath);
                        }
                        nextPath.Add(value);
                        anyOk |= TestPath(path, nextPath, value, bestPath, failedPath);

                    }
                }
                if ( !anyOk)
                {
                    failedPath.Add(testPathAsString);
                }
            }
            
            return anyOk;
        }

        private static int GoOn(Dictionary<string, List<string>> path, string p, List<string> currentPath)
        {
            
            var res = 0;
            var testPath = new List<string>();
            testPath.AddRange(currentPath);
            if (path.ContainsKey(p))
            {
                path.TryGetValue(p, out var nextPath);
                foreach ( var next in nextPath)
                {                    
                    if ( !testPath.Contains(next))
                    {
                        testPath.Add(next);
                        var res2 = GoOn(path, next, testPath);
                        if ( res2 > res)
                        {
                            res = res2;
                        }
                    } else
                    {
                        if ( path.Any(a => a.Value.Contains(p) && !testPath.Contains(a.Key)))
                        {
                            foreach ( var a2 in path.Where(a=> a.Value.Contains(p) && !testPath.Contains(a.Key)).ToList())
                            {
                                testPath.Add(a2.Key);
                                var res2 = GoOn(path, next, testPath);
                                if (res2 > res)
                                {
                                    res = res2;
                                }
                            }
                        }
                        if ( (res == 0 || testPath.Count > res) && Valid(testPath, path))
                        {
                            foreach ( var tt in testPath)
                            {
                                Console.Write(tt + "-");
                            }
                            Console.WriteLine();
                            res = testPath.Count;
                        }
                        
                    }
                }
            }
            return res;
        }

        private static bool Valid(List<string> testPath, Dictionary<string, List<string>> path)
        {
            //Still valid path
            foreach (var t in testPath)
            {
                path.TryGetValue(t, out var test);
                foreach (var t2 in testPath)
                {
                    if (t2 != t && !test.Contains(t2))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
