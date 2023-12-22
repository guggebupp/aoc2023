using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023.Solution
{
    internal class Day14
    {
        private static string day = "14";
        private static string fileName = "day" + day + ".txt";
        public static void solve1()
        {
            long total = 0;
            var lines = readFile();
            var noLines = lines.Count;
            //lines = tiltAndGo(lines);
            //lines = rowToCol(lines);
            //lines = rowToCol(lines);
            //lines = rowToCol(lines);
            lines = goNorth(lines);


            //1. Tilt north            
            /**var changed = true;
            while (changed)
            {
                changed = false;
                for (int col = 0; col < lines[0].Length; col++)
                {
                    for (int line = 1; line < noLines; line++)
                    {
                        if (lines[line][col] == 'O' && lines[line - 1][col] == '.')
                        {
                            StringBuilder sb = new StringBuilder(lines[line - 1]);
                            sb[col] = 'O';
                            lines[line - 1] = sb.ToString();
                            sb = new StringBuilder(lines[line]);
                            sb[col] = '.';
                            lines[line] = sb.ToString();
                            changed = true;

                        }
                    }
                }
            }**/
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
            for (int i = 0; i < noLines; i++)
            {
                total += lines[i].Where(a => a == 'O').Count() * (noLines - i);
            }

            Console.WriteLine("Day" + day + "-1: " + total);
        }

        public static void solve2()
        {
            long total = 0;

            var lines = readFile();
            long iterations = 1000000000;
            var sameIndex = new List<long>();
            var before = new List<List<string>>();
            for (long i = 0; i < iterations; i++)
            {
                before.Add(lines);

                lines = tiltAndGo(lines);//north                
                lines = tiltAndGo(lines);//west                
                lines = tiltAndGo(lines);//south                                
                lines = tiltAndGo(lines);//east

                bool same = true;
                foreach (var bef in before)
                {
                    same = true;
                    for (int l = 0; l < lines.Count; l++)
                    {
                        if (!lines[l].Equals(bef[l]))
                        {
                            same = false;
                            break;
                        }
                    }
                    if (same)
                    {
                        break;
                    }
                }

                if (same)
                {
                    before.Clear();
                    sameIndex.Add(i);
                    Console.WriteLine("sameIndex: " + i);

                    if (sameIndex.Count == 2)
                    {
                        i = iterations - ((iterations - sameIndex[0]) % (sameIndex[1] - sameIndex[0]));
                    }
                }

            }


            var noLines = lines.Count();
            for (int i = 0; i < noLines; i++)
            {
                total += lines[i].Where(a => a == 'O').Count() * (noLines - i);
            }

            Console.WriteLine("Day" + day + "-2: " + total);
        }


        private static List<string> rowToCol(List<string> lines)
        {
            var newLines = new List<string>();
            for (int col = 0; col < lines[0].Length; col++)
            {
                var newLine = new StringBuilder();
                for (int line = lines.Count - 1; line >= 0; line--)
                {
                    newLine.Append(lines[line][col]);

                }
                newLines.Add(newLine.ToString());
            }
            return newLines;
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

        private static List<string> tiltAndGo(List<string> lines)
        {
            //1. Tilt north
            var noLines = lines.Count;
            var newCols = new List<string>();
            for (int col = 0; col < lines[0].Length; col++)
            {
                var currentCol = new StringBuilder();
                for (int line = noLines - 1; line >= 0; line--)
                {
                    currentCol.Append(lines[line][col]);

                }
                var parts = currentCol.ToString().Split('#');
                var newCol = new StringBuilder();

                for (int partId = 0; partId < parts.Length; partId++)
                {
                    newCol.Append(parts[partId].Where(a => a == '.').ToArray());
                    newCol.Append(parts[partId].Where(a => a == 'O').ToArray());


                    // Inte sista
                    if (partId != parts.Length - 1)
                    {
                        newCol.Append('#');
                    }
                }
                newCols.Add(newCol.ToString());
            }

            return newCols;
        }


        private static List<string> goNorth(List<string> lines)
        {
            //1. Tilt north
            var noLines = lines.Count;
            var newCols = new List<string>();
            for (int col = 0; col < lines[0].Length; col++)
            {
                var currentCol = new StringBuilder();
                for (int line = 0; line < noLines; line++)
                {
                    currentCol.Append(lines[line][col]);

                }
                var parts = currentCol.ToString().Split('#');
                var newCol = new StringBuilder();

                for (int partId = 0; partId < parts.Length; partId++)
                {
                    newCol.Append(parts[partId].Where(a => a == 'O').ToArray());
                    newCol.Append(parts[partId].Where(a => a == '.').ToArray());

                    // Inte sista
                    if (partId != parts.Length - 1)
                    {
                        newCol.Append('#');
                    }
                }
                newCols.Add(newCol.ToString());
            }
            var newLines = new List<string>();

            for (int col = 0; col < newCols.Count; col++)
            {
                var newLine = new StringBuilder();
                for (int i = 0; i < lines.Count; i++)
                {
                    newLine.Append(newCols[i][col]);
                }
                newLines.Add(newLine.ToString());
            }
            return newLines;
        }

        private static List<string> goSouth(List<string> lines)
        {
            //1. Tilt south
            var noLines = lines.Count;
            var newCols = new List<string>();
            for (int col = 0; col < lines[0].Length; col++)
            {
                var currentCol = new StringBuilder();
                for (int line = 0; line < noLines; line++)
                {
                    currentCol.Append(lines[line][col]);

                }
                var parts = currentCol.ToString().Split('#');
                var newCol = new StringBuilder();
                //foreach (var part in parts)
                for (int partId = 0; partId < parts.Length; partId++)
                {
                    newCol.Append(parts[partId].Where(a => a == '.').ToArray());
                    newCol.Append(parts[partId].Where(a => a == 'O').ToArray());

                    // Inte sista
                    if (partId != parts.Length - 1)
                    {
                        newCol.Append('#');
                    }
                }
                newCols.Add(newCol.ToString());
            }
            var newLines = new List<string>();

            for (int col = 0; col < newCols.Count; col++)
            {
                var newLine = new StringBuilder();
                for (int i = 0; i < lines.Count; i++)
                {
                    newLine.Append(newCols[i][col]);
                }
                newLines.Add(newLine.ToString());
            }
            return newLines;
        }

        private static List<string> goWest(List<string> lines)
        {
            //1. Tilt west
            var newLines = new List<string>();
            var noLines = lines.Count;
            var changed = true;
            while (changed)
            {
                changed = false;
                for (int line = 0; line < noLines; line++)
                {
                    var parts = lines[line].ToString().Split('#');
                    var newLine = new StringBuilder();

                    for (int partId = 0; partId < parts.Length; partId++)
                    {
                        newLine.Append(parts[partId].Where(a => a == 'O').ToArray());
                        newLine.Append(parts[partId].Where(a => a == '.').ToArray());

                        // Inte sista
                        if (partId != parts.Length - 1)
                        {
                            newLine.Append('#');
                        }
                    }

                    newLines.Add(newLine.ToString());
                }
            }
            return newLines;
        }

        private static List<string> goEast(List<string> lines)
        {
            //1. Tilt west
            var newLines = new List<string>();
            var noLines = lines.Count;
            var changed = true;
            while (changed)
            {
                changed = false;
                for (int line = 0; line < noLines; line++)
                {
                    var parts = lines[line].ToString().Split('#');
                    var newLine = new StringBuilder();

                    for (int partId = 0; partId < parts.Length; partId++)
                    {
                        newLine.Append(parts[partId].Where(a => a == '.').ToArray());
                        newLine.Append(parts[partId].Where(a => a == 'O').ToArray());

                        // Inte sista
                        if (partId != parts.Length - 1)
                        {
                            newLine.Append('#');
                        }
                    }
                    newLines.Add(newLine.ToString());
                }
            }
            return newLines;
        }
    }
}
