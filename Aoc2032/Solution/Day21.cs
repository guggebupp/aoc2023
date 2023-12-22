using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023.Solution
{
    internal class Day21
    {
        private static string day = "21";
        private static string fileName = "day" + day + ".txt";
        public static void solve1()
        {
            long total = 0;
            var lines = Util.readFile(fileName);
            var startPos = 0;
            for (int line = 0; line < lines.Count; line++)
            {
                for (int col = 0; col < lines[0].Length; col++)
                {
                    if (lines[line][col] == 'S')
                    {
                        startPos = line * 1000 + col;
                    }
                }
            }
            var currentPos = new List<int>();
            currentPos.Add(startPos);
            var nexPos = new List<int>();
            for (int step = 0; step < 64; step++)
            {
                foreach (var pos in currentPos)
                {
                    var posX = pos % 1000;
                    var posY = pos / 1000;
                    //Above
                    if (posY - 1 >= 0 && lines[posY - 1][posX] != '#')
                    {
                        var nex = (posY - 1) * 1000 + posX;
                        if (!nexPos.Contains(nex))
                            nexPos.Add(nex);
                    }
                    //Right
                    if (posX + 1 < lines[0].Length && lines[posY][posX + 1] != '#')
                    {
                        var nex = (posY) * 1000 + posX + 1;
                        if (!nexPos.Contains(nex))
                            nexPos.Add(nex);
                    }
                    //Left
                    if (posX - 1 >= 0 && lines[posY][posX - 1] != '#')
                    {
                        var nex = posY * 1000 + posX - 1;
                        if (!nexPos.Contains(nex))
                            nexPos.Add(nex);
                    }
                    //Down
                    if (posY + 1 < lines.Count && lines[posY + 1][posX] != '#')
                    {
                        var nex = (posY + 1) * 1000 + posX;
                        if (!nexPos.Contains(nex))
                            nexPos.Add(nex);
                    }
                }
                foreach (var pos in currentPos)
                {
                    StringBuilder sb = new StringBuilder(lines[pos / 1000]);
                    sb[pos % 1000] = '.';
                    lines[pos / 1000] = sb.ToString();
                }
                currentPos.Clear();
                foreach (var pos in nexPos)
                {
                    currentPos.Add(pos);
                    StringBuilder sb = new StringBuilder(lines[pos / 1000]);
                    sb[pos % 1000] = 'O';
                    lines[pos / 1000] = sb.ToString();
                }
                nexPos.Clear();
            }
            StringBuilder sb2 = new StringBuilder(lines[startPos / 1000]);
            sb2[startPos % 1000] = 'O';
            lines[startPos / 1000] = sb2.ToString();
            foreach (var line in lines)
            {                
                total += line.Count(a => a == 'O');
            }

            Console.WriteLine("Day" + day + "-1: " + total);
        }

        public static void solve2()
        {
            long total = 0;
            var lines = Util.readFile(fileName);
            var startPos = 0;
            var mapYOffset = 100000;
            var mapXOffset = 100000;
            var mapXYMult = 1000000;
            for (int line = 0; line < lines.Count; line++)
            {
                for (int col = 0; col < lines[0].Length; col++)
                {
                    if (lines[line][col] == 'S')
                    {                        
                        startPos = line * 1000 + col;
                    }
                }
            }
            
            var currentPos = new Dictionary<int, List<int>>();
            var mapPositions = new List<int>();
            mapPositions.Add(mapYOffset * mapXYMult + mapXOffset);
            currentPos.Add(startPos, mapPositions);            
            var nexPos = new Dictionary<int, List<int>>();
            var maxY = lines.Count;
            var maxX = lines[0].Length;            
            var steps = 26501365;
            var s = lines[0].Length == lines.Count ? lines[0].Length : -1;
            var a = steps % s;
            var b = lines.Count;
            var c = steps / s;
            var res = new List<long>();
            for (int step = 0; step < b * 3 + a + 1; step++)
            {                
                foreach (var pos in currentPos)
                {
                    var posX = pos.Key % 1000;
                    var posY = pos.Key / 1000;
                    var mapX = 0;
                    var mapY = 0;

                    //Above
                    if (posY - 1 < 0)
                    {
                        mapY = mapY - 1;
                        posY = posY - 1 + maxY;
                    }
                    else
                    {
                        posY = posY - 1;
                    }
                    if (lines[posY][posX] != '#')
                    {
                        var posKey = posY * 1000 + posX;


                        if (!nexPos.ContainsKey(posKey))
                        {
                            if (mapY == 0 && mapX == 0)
                            {
                                nexPos.Add(posKey, pos.Value);
                            }
                            else
                            {
                                var mapKeys = new List<int>();                                
                                pos.Value.ForEach(a => mapKeys.Add(a + mapY * mapXYMult + mapX));

                                nexPos.Add(posKey, mapKeys);
                            }
                        }
                        else
                        {
                            var currentMapKeys = new List<int>();
                            nexPos[posKey].ForEach(a => currentMapKeys.Add(a));
                            if (mapY == 0 && mapX == 0)
                            {
                                pos.Value.Where(a => !currentMapKeys.Contains(a)).ToList().ForEach(a => currentMapKeys.Add(a));
                            }
                            else
                            {
                                pos.Value.
                                    Where(a => !currentMapKeys.Contains(a + mapY * mapXYMult + mapX)).ToList().
                                    ForEach(a => currentMapKeys.Add(a + mapY * mapXYMult + mapX));
                            }

                            nexPos[posKey] = currentMapKeys;
                        }
                    }
                    //Down
                    posX = pos.Key % 1000;
                    posY = pos.Key / 1000;
                    mapX = 0;
                    mapY = 0;
                    if (posY + 1 >= maxY)
                    {
                        mapY = mapY + 1;
                        posY = posY + 1 - maxY;
                    }
                    else
                    {
                        posY = posY + 1;
                    }
                    if (lines[posY][posX] != '#')
                    {

                        var posKey = posY * 1000 + posX;


                        if (!nexPos.ContainsKey(posKey))
                        {
                            if (mapY == 0 && mapX == 0)
                            {
                                nexPos.Add(posKey, pos.Value);
                            }
                            else
                            {
                                var mapKeys = new List<int>();
                                pos.Value.ForEach(a => mapKeys.Add(a + mapY * mapXYMult + mapX));

                                nexPos.Add(posKey, mapKeys);
                            }
                        }
                        else
                        {
                            var currentMapKeys = new List<int>();
                            nexPos[posKey].ForEach(a => currentMapKeys.Add(a));
                            if (mapY == 0 && mapX == 0)
                            {
                                pos.Value.Where(a => !currentMapKeys.Contains(a)).ToList().ForEach(a => currentMapKeys.Add(a));
                            }
                            else
                            {
                                pos.Value.
                                    Where(a => !currentMapKeys.Contains(a + mapY * mapXYMult + mapX)).ToList().
                                    ForEach(a => currentMapKeys.Add(a + mapY * mapXYMult + mapX));
                            }

                            nexPos[posKey] = currentMapKeys;
                        }
                    }
                    //Right
                    posX = pos.Key % 1000;
                    posY = pos.Key / 1000;
                    mapX = 0;
                    mapY = 0;
                    if (posX + 1 >= maxX)
                    {
                        mapX = mapX + 1;
                        posX = posX + 1 - maxX;
                    }
                    else
                    {
                        posX = posX + 1;
                    }
                    if (lines[posY][posX] != '#')
                    {

                        var posKey = posY * 1000 + posX;


                        if (!nexPos.ContainsKey(posKey))
                        {
                            if (mapY == 0 && mapX == 0)
                            {
                                nexPos.Add(posKey, pos.Value);
                            }
                            else
                            {
                                var mapKeys = new List<int>();
                                pos.Value.ForEach(a => mapKeys.Add(a + mapY * mapXYMult + mapX));

                                nexPos.Add(posKey, mapKeys);
                            }
                        }
                        else
                        {
                            var currentMapKeys = new List<int>();
                            nexPos[posKey].ForEach(a => currentMapKeys.Add(a));
                            if (mapY == 0 && mapX == 0)
                            {
                                pos.Value.Where(a => !currentMapKeys.Contains(a)).ToList().ForEach(a => currentMapKeys.Add(a));
                            }
                            else
                            {
                                pos.Value.
                                    Where(a => !currentMapKeys.Contains(a + mapY * mapXYMult + mapX)).ToList().
                                    ForEach(a => currentMapKeys.Add(a + mapY * mapXYMult + mapX));
                            }

                            nexPos[posKey] = currentMapKeys;
                        }
                    }
                    //Left
                    posX = pos.Key % 1000;
                    posY = pos.Key / 1000;
                    mapX = 0;
                    mapY = 0;
                    if (posX - 1 < 0)
                    {
                        mapX = mapX - 1;
                        posX = posX - 1 + maxX;
                    }
                    else
                    {
                        posX = posX - 1;
                    }
                    if (lines[posY][posX] != '#')
                    {

                        var posKey = posY * 1000 + posX;


                        if (!nexPos.ContainsKey(posKey))
                        {
                            if (mapY == 0 && mapX == 0)
                            {
                                nexPos.Add(posKey, pos.Value);
                            }
                            else
                            {
                                var mapKeys = new List<int>();
                                pos.Value.ForEach(a => mapKeys.Add(a + mapY * mapXYMult + mapX));

                                nexPos.Add(posKey, mapKeys);
                            }
                        }
                        else
                        {
                            var currentMapKeys = new List<int>();
                            nexPos[posKey].ForEach(a => currentMapKeys.Add(a));
                            if (mapY == 0 && mapX == 0)
                            {
                                pos.Value.Where(a => !currentMapKeys.Contains(a)).ToList().ForEach(a => currentMapKeys.Add(a));
                            }
                            else
                            {
                                pos.Value.
                                    Where(a => !currentMapKeys.Contains(a + mapY * mapXYMult + mapX)).ToList().
                                    ForEach(a => currentMapKeys.Add(a + mapY * mapXYMult + mapX));
                            }

                            nexPos[posKey] = currentMapKeys;
                        }
                    }
                }

                if ((step - a) % b == 0)
                {
                    var currentTot = 0;
                    foreach (var ca in currentPos)
                    {
                        currentTot += ca.Value.Count;
                    }
                    res.Add(currentTot);                                        

                }
                currentPos.Clear();
                foreach (var pos in nexPos)
                {
                    currentPos.Add(pos.Key, pos.Value);
                }
                nexPos.Clear();


            }
            foreach (var ca in currentPos)
            {
                total += ca.Value.Count;
            }

            var c1 = res[0];// res[0] Värde när vi kommit till kanten första gången från S
            var a1 = (res[2] - 2 * res[1] + c1) / 2; // res[2] Värde när vi kommer till kanten tredje gången
            var b1 = res[1] - a1 - c1;// res[1] Värde när vi kommer till kanten andra gången
            // c är antalet rader/kolumner i grid

            var result = a1 * c * c + b1 * c + c1;

            Console.WriteLine("Day" + day + "-2: " + total + " -- " + currentPos.Count + " - " + result);
        }        

    }
    internal class MapPos
    {
        public MapPos()
        {
            this.mapX = 0;
            this.mapY = 0;
            this.posX = 0;
            this.posY = 0;
        }
        public MapPos(int mapX, int mapY, int posX, int posY)
        {
            this.mapX = mapX;
            this.mapY = mapY;
            this.posX = posX;
            this.posY = posY;
        }
        public int mapX;
        public int mapY;
        public int posX;
        public int posY;

        public bool Equals(MapPos? other)
        {
            return (mapX == other.mapX && mapY == other.mapY && posX == other.posX && posY == other.posY);
        }

        public override int GetHashCode()
        {
            return mapX * 17*17*17 + mapY * 17*17 + posX * 17 + posY;
        }
    }
}
