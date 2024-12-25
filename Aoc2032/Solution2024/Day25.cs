




namespace Aoc2023.Solution2024
{
    internal class Day25
    {
        private static string day = "25";
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
            var locks = new List<List<int>>();
            var keys = new List<List<int>>();
            for (int i = 0; i < lines.Count; i = i + 8)
            {
                //Key
                if (lines[i]== "....." && ((i+8) > lines.Count || lines[i+6] == "#####"))
                {
                    var key1 = 0;
                    var key2 = 0;
                    var key3 = 0;
                    var key4 = 0;
                    var key5 = 0;
                    for ( int j= i+6;j>i;j--)
                    {
                        if (lines[j][0] == '#')
                        {
                            key1++;
                        }
                        if (lines[j][1] == '#')
                        {
                            key2++;
                        }
                        if (lines[j][2] == '#')
                        {
                            key3++;
                        }
                        if (lines[j][3] == '#')
                        {
                            key4++;
                        }
                        if (lines[j][4] == '#')
                        {
                            key5++;
                        }
                    }
                    var key = new List<int>();
                    key.Add(key1);
                    key.Add(key2);
                    key.Add(key3);
                    key.Add(key4);
                    key.Add(key5);
                    keys.Add(key);
                }
                //Lock
                else if (lines[i] == "#####" && lines[i+6] == ".....")
                {
                    var key1 = 0;
                    var key2 = 0;
                    var key3 = 0;
                    var key4 = 0;
                    var key5 = 0;
                    for (int j = i + 6; j > i; j--)
                    {
                        if (lines[j][0] == '.')
                        {
                            key1++;
                        }
                        if (lines[j][1] == '.')
                        {
                            key2++;
                        }
                        if (lines[j][2] == '.')
                        {
                            key3++;
                        }
                        if (lines[j][3] == '.')
                        {
                            key4++;
                        }
                        if (lines[j][4] == '.')
                        {
                            key5++;
                        }
                    }
                    var key = new List<int>();
                    key.Add(key1);
                    key.Add(key2);
                    key.Add(key3);
                    key.Add(key4);
                    key.Add(key5);
                    locks.Add(key);
                } else
                {
                    Console.WriteLine("ERROR");
                }
            }
            foreach (var key in keys) { 
                foreach ( var loc in locks) {
                    var matched = true;
                    for (int i = 0; i < key.Count; i++) {
                        if (loc[i] < key[i]) { 
                            matched = false; break;
                        }
                    }
                    if (matched)
                    {
                        total++;
                    }
                }
            }
            Console.WriteLine("Day25-1: " + total);
        }
        

        public static void solve2()
        {
            var total = 0;
            var lines = Util.readFile(fileName);
            var enabled = true;
            foreach (var line in lines)
            {
                if (line != "")
                {                    
                }
            }
            Console.WriteLine("Day25-2: " + total);
        }
        
        
    }
}
