





using System.Data;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;

namespace Aoc2023.Solution2024
{
    internal class Day10
    {
        private static string day = "10";
        private static string fileName = "day" + day + ".txt";
        private static List<int> handledPeak = new List<int>();
        private static List<Dirtection> directions = new List<Dirtection>() { Dirtection.Up, Dirtection.Down, Dirtection.Left, Dirtection.Right};


        public static void solve1()
        {                        
            var lines = Util.readFile(fileName);
            var total = findPath(lines);
            
            Console.WriteLine("Day10-1: " + total);
        }        

        public static void solve2()
        {
            
            var lines = Util.readFile(fileName);
            var total = findPath(lines, true);

            Console.WriteLine("Day10-2: " + total);
        }

        private static int findPath(List<string> lines, bool unique = false)
        {
            var total = 0;
            for (int lineNr = 0; lineNr < lines.Count; lineNr++)
            {
                for (int col = 0; col < lines[lineNr].Length; col++)
                {
                    if (lines[lineNr][col] == '0')
                    {
                        handledPeak = new List<int>();
                        foreach (Dirtection direction in directions)
                        {
                            total += goNext(0, lineNr, col, lines, direction, unique);
                        }
                    }

                }
            }
            return total;
        }

        private static int goNext(int currentLevel, int lineNr, int col, List<string> lines, Dirtection dir, bool unique = false)
        {
            currentLevel++;
            switch(dir)
            {
                case Dirtection.Up:
                    lineNr--;
                    break;
                case Dirtection.Down:
                    lineNr++;
                    break;
                case Dirtection.Left:
                    col--;
                    break;
                case Dirtection.Right:
                    col++;
                    break;
            }            
            var res = 0;
            //if (lineNr >= 0 && col >= 0 && lineNr < lines.Count && col < lines[lineNr].Length && Int32.Parse(lines[lineNr][col] + "") == currentLevel)
            if (Util.isInside(lines, lineNr, col) && Int32.Parse(lines[lineNr][col] + "") == currentLevel)
            {
                if ( currentLevel == 9)
                {
                    if (!handledPeak.Contains(lineNr * 1000 + col))
                    {
                        if (!unique)
                        {
                            handledPeak.Add(lineNr * 1000 + col);
                        }
                        
                        res = 1;
                    } else
                    {
                        return 0;
                    }
                        
                }
                else
                {                    
                    foreach (Dirtection direction in directions)
                    {
                        res += goNext(currentLevel, lineNr, col, lines, direction, unique);
                    }
                }
                
            }
            
            return res; 
        }

        


    }
}
