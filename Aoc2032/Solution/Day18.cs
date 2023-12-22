using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Aoc2023.Util;

namespace Aoc2023.Solution
{
    internal class Day18
    {
        private static string day = "18";
        private static string fileName = "day" + day + ".txt";
        public static void solve1()
        {
            long total = 0;
            var lines = Util.readFile(fileName);

            var grids = new List<GridCube>();
            var posY = 0;
            var posX = 0;
            grids.Add(new GridCube(posY, posX));
            foreach (var line in lines)
            {
                var p = line.Split(' ');
                if (p[0].Equals("R"))
                {
                    for (int i = 0; i < int.Parse(p[1]); i++)
                    {
                        posX++;
                        grids.Add(new GridCube(posY, posX));
                    }
                }
                if (p[0].Equals("L"))
                {
                    for (int i = 0; i < int.Parse(p[1]); i++)
                    {
                        posX--;
                        grids.Add(new GridCube(posY, posX));
                    }
                }
                if (p[0].Equals("U"))
                {
                    for (int i = 0; i < int.Parse(p[1]); i++)
                    {
                        posY--;
                        grids.Add(new GridCube(posY, posX));
                    }
                }
                if (p[0].Equals("D"))
                {
                    for (int i = 0; i < int.Parse(p[1]); i++)
                    {
                        posY++;
                        grids.Add(new GridCube(posY, posX));
                    }
                }

            }
            var maxX = 0;
            var minX = 0;
            var maxY = 0;
            var minY = 0;
            foreach (var grid in grids)
            {
                if (grid.posY < minY)
                {
                    minY = grid.posY;
                }
                if (grid.posY > maxY)
                {
                    maxY = grid.posY;
                }
                if (grid.posX < minX)
                {
                    minX = grid.posX;
                }
                if (grid.posX > maxX)
                {
                    maxX = grid.posX;
                }
            }

            var lines2 = new List<string>();
            for (int y = minY; y <= maxY; y++)
            {
                var line = "";
                for (int x = minX; x <= maxX; x++)
                {
                    var foundGround = true;
                    foreach (var grid in grids)
                    {
                        if (grid.posY == y && grid.posX == x)
                        {
                            foundGround = false;
                            continue;
                        }
                    }
                    if (foundGround)
                    {
                        line += ".";
                    }
                    else
                    {
                        line += "#";
                    }
                }
                lines2.Add(line);
            }
            foreach (var line in lines2)
            {
                Console.WriteLine(line);
            }
            var foundNo = 0;
            var lines3 = new List<string>();
            //foreach (var line in lines2)
            for (int l = 0; l < lines2.Count; l++)
            {
                var line = lines2[l];
                var prev = '-';
                var inside = false;
                var line3 = "";
                var enterMode = 0; // 1 = above, 2 = down
                for (int col = 0; col < line.Length; col++)
                {
                    var cur = line[col];
                    var next = '-';
                    if (col + 1 < line.Length)
                    {
                        next = line[col + 1];
                    }
                    if (line[col].Equals('#'))
                    {
                        line3 += '#';
                    }
                    if (line[col].Equals('#'))
                    {
                        if (!prev.Equals('#') && !next.Equals('#'))
                        {
                            inside = !inside;
                            enterMode = 0;
                        }
                        else if (!prev.Equals('#'))
                        {
                            var mode = 0;
                            if (l > 0)
                                mode = lines2[l - 1][col].Equals('#') ? 1 : 2;
                            else
                                mode = lines2[l + 1][col].Equals('#') ? 2 : 1;
                            if (enterMode == 0)
                            {
                                inside = !inside;
                                enterMode = mode;
                            }
                        }
                        else if (!next.Equals('#'))
                        {
                            var mode = 0;
                            if (l > 0)
                                mode = lines2[l - 1][col].Equals('#') ? 1 : 2;
                            else
                                mode = lines2[l + 1][col].Equals('#') ? 2 : 1;
                            if (enterMode == mode)
                            {
                                inside = !inside;
                            }
                            enterMode = 0;

                        }

                    }

                    if (line[col].Equals('.') && inside)
                    {
                        foundNo++;
                        line3 += 'X';
                    }
                    else if (line[col].Equals('.'))
                    {
                        line3 += '.';
                    }

                    prev = line[col];
                }
                lines3.Add(line3);
            }
            Console.WriteLine();
            foreach (var line in lines3)
            {
                Console.WriteLine(line);
            }
            total = foundNo + grids.Count - 1;
            Console.WriteLine("Day" + day + "-1: " + total);
        }

        public static void solve2()
        {
            var lines = Util.readFile(fileName);

            var grids = new List<GridCube2>();
            var posY = 0;
            var posX = 0;
            grids.Add(new GridCube2(posY, posX, 0, Direction.Right));
            var edge = 0;
            foreach (var line in lines)
            {
                var c = line.Split(' ')[2];
                var dir = c.Substring(c.Length - 2, 1);
                var length = c.Substring(2, 5);
                int num = Int32.Parse(length, System.Globalization.NumberStyles.HexNumber);
                Console.WriteLine("DD: " + dir + " -- " + num);
                edge += num;
                if (dir.Equals("0"))
                {
                    posX += num;
                    grids.Add(new GridCube2(posY, posX, num, Direction.Right));
                }
                if (dir.Equals("2"))
                {
                    posX -= (num);
                    grids.Add(new GridCube2(posY, posX, num, Direction.Left));
                }
                if (dir.Equals("3"))
                {

                    posY -= (num);
                    grids.Add(new GridCube2(posY, posX, num, Direction.Up));
                }
                if (dir.Equals("1"))
                {
                    posY += num;
                    grids.Add(new GridCube2(posY, posX, num, Direction.Down));
                }
            }

            long area = Math.Abs(grids.Take(grids.Count - 1)
   .Select((p, i) => (long)(grids[i + 1].posX - p.posX) * (grids[i + 1].posY + p.posY))
   .Sum() / 2);
            long area2 = area + edge / 2 + 1;


            Console.WriteLine("Day" + day + "-2: " + area2);
        }        
    }
}
