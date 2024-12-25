




namespace Aoc2023.Solution2024
{
    internal class Day03
    {
        private static string day = "03";
        private static string fileName = "day" + day + ".txt";

        public static void solve1()
        {
            var total = 0;
            var lines = Util.readFile(fileName);
            foreach (var line in lines)
            {
                if (line != "")
                {
                    for (int i = 0; i < line.Length; i++)
                    {
                        if (line[i] == 'm')
                        {
                            i++;
                            if (line[i] == 'u')
                            {
                                i++;
                                if (line[i] == 'l')
                                {
                                    i++;
                                    if (line[i] == '(')
                                    {
                                        i++;
                                        var no1Str = "";
                                        while (line[i] >= '0' && line[i] <= '9' )
                                        {
                                            no1Str += line[i];
                                            i++;
                                        }
                                        if (line[i] == ',')
                                        {
                                            i++;
                                            var no2Str = "";
                                            while (line[i] >= '0' && line[i] <= '9')
                                            {
                                                no2Str += line[i];
                                                i++;
                                            }
                                            if (line[i] == ')')
                                            {
                                                total += int.Parse(no1Str) * int.Parse(no2Str);
                                            }
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
            }
            Console.WriteLine("Day03-1: " + total);
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
                    for (int i = 0; i < line.Length; i++)
                    {
                        if (line[i] == 'd')
                        {
                            i++;
                            if (line[i] == 'o')
                            {
                                i++;
                                if (line[i] == '(')
                                {
                                    i++;
                                    if (line[i] == ')')
                                    {
                                        enabled = true;
                                    }
                                } else if (line[i] == 'n')
                                {
                                    i++;
                                    if (line[i] == '\'')
                                    {
                                        i++;
                                        if (line[i] == 't')
                                        {
                                            i++;
                                            if (line[i] == '(')
                                            {
                                                i++;
                                                if (line[i] == ')')
                                                {
                                                    enabled = false;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (line[i] == 'm')
                        {
                            i++;
                            if (line[i] == 'u')
                            {
                                i++;
                                if (line[i] == 'l')
                                {
                                    i++;
                                    if (line[i] == '(')
                                    {
                                        i++;
                                        var no1Str = "";
                                        while (line[i] >= '0' && line[i] <= '9')
                                        {
                                            no1Str += line[i];
                                            i++;
                                        }
                                        if (line[i] == ',')
                                        {
                                            i++;
                                            var no2Str = "";
                                            while (line[i] >= '0' && line[i] <= '9')
                                            {
                                                no2Str += line[i];
                                                i++;
                                            }
                                            if (line[i] == ')')
                                            {
                                                if ( enabled)
                                                {
                                                    total += int.Parse(no1Str) * int.Parse(no2Str);
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
            Console.WriteLine("Day03-2: " + total);
        }
        
        
    }
}
