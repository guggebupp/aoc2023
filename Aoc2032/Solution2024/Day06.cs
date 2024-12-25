




namespace Aoc2023.Solution2024
{
    internal class Day06
    {
        private static string day = "06";
        private static string fileName = "day" + day + ".txt";

        public static void solve1()
        {
            var total = 0;
            var lines = Util.readFile(fileName);
            var startRow = -1;
            var startPos = -1;
            var dir = -1; //0->Up, 1->Down, 2->Right, 3->Left
            var pastPos = new List<int>();
            //Find start:
            for (int row = 0; row < lines.Count; row++)
            {
                for (int pos = 0; pos < lines[row].Length; pos++) {
                    if (lines[row][pos] == '^')
                    {
                        dir = 0;
                        startPos = pos;
                        startRow = row;
                    }
                    if (lines[row][pos] == 'V')
                    {
                        dir = 1;
                        startPos = pos;
                        startRow = row;
                    }
                    if (lines[row][pos] == '>')
                    {
                        dir = 2;
                        startPos = pos;
                        startRow = row;
                    }
                    if (lines[row][pos] == '<')
                    {
                        dir = 3;
                        startPos = pos;
                        startRow = row;
                    }
                }
            }
            total++;
            pastPos.Add(startRow*1000+startPos);
            while(startPos > -1 && startRow > -1 && startRow < lines.Count && startPos < lines[startRow].Length)
            {                
                if ( dir == 0) // Upp
                {
                    startRow--;
                    if (!(startPos > -1 && startRow > -1 && startRow < lines.Count && startPos < lines[startRow].Length))
                    {
                        break;
                    }
                    if (lines[startRow][startPos] == '#')
                    {
                        dir = 2;//Right
                        startRow++;
                    } else
                    {
                        if ( !pastPos.Contains(startRow*1000+startPos))
                        {
                            pastPos.Add(startRow * 1000 + startPos);
                            total++;
                        }
                        
                    }
                }
                else if (dir == 1) // Down
                {
                    startRow++;
                    if (!(startPos > -1 && startRow > -1 && startRow < lines.Count && startPos < lines[startRow].Length))
                    {
                        break;
                    }
                    if (lines[startRow][startPos] == '#')
                    {
                        dir = 3;//Left
                        startRow--;
                    }
                    else
                    {
                        if (!pastPos.Contains(startRow * 1000 + startPos))
                        {
                            pastPos.Add(startRow * 1000 + startPos);
                            total++;
                        }
                    }
                }
                else if (dir == 2) // Right
                {
                    startPos++;
                    if (!(startPos > -1 && startRow > -1 && startRow < lines.Count && startPos < lines[startRow].Length))
                    {
                        break;
                    }
                    if (lines[startRow][startPos] == '#')
                    {
                        dir = 1;//Down
                        startPos--;
                    }
                    else
                    {
                        if (!pastPos.Contains(startRow * 1000 + startPos))
                        {
                            pastPos.Add(startRow * 1000 + startPos);
                            total++;
                        }
                    }
                }
                else if (dir == 3) // Left
                {
                    startPos--;
                    if (!(startPos > -1 && startRow > -1 && startRow < lines.Count && startPos < lines[startRow].Length))
                    {
                        break;
                    }
                    if (lines[startRow][startPos] == '#')
                    {
                        dir = 0;//Up
                        startPos++;
                    }
                    else
                    {
                        if (!pastPos.Contains(startRow * 1000 + startPos))
                        {
                            pastPos.Add(startRow * 1000 + startPos);
                            total++;
                        }
                    }
                }
            }
            Console.WriteLine("Day06-1: " + total);
        }
        

        public static void solve2()
        {
            var total = 0;
            var total2 = 0;
            var lines = Util.readFile(fileName);
            var startRow = -1;
            var startPos = -1;
            var dir = -1; //0->Up, 1->Down, 2->Right, 3->Left
            var pastPos = new List<int>();
            var pastPos2 = new List<int>();
            //Find start:
            for (int row = 0; row < lines.Count; row++)
            {
                for (int pos = 0; pos < lines[row].Length; pos++)
                {
                    if (lines[row][pos] == '^')
                    {
                        dir = 0;
                        startPos = pos;
                        startRow = row;
                    }
                    if (lines[row][pos] == 'V')
                    {
                        dir = 1;
                        startPos = pos;
                        startRow = row;
                    }
                    if (lines[row][pos] == '>')
                    {
                        dir = 2;
                        startPos = pos;
                        startRow = row;
                    }
                    if (lines[row][pos] == '<')
                    {
                        dir = 3;
                        startPos = pos;
                        startRow = row;
                    }
                }
            }
            total++;
            pastPos.Add(startRow * 1000 + startPos);
            pastPos2.Add(dir*1000000+startRow * 1000 + startPos);
            var loop = -1;
            var startRowBak = startRow;
            var startPosBak = startPos;
            var dirBak = dir;
            var looped = false;
            for (int row = 0; row < lines.Count; row++)
            {
                for (int pos = 0; pos < lines[row].Length; pos++)
                {
                    if (lines[row][pos] != '.')
                    {
                        continue;
                    }
                    loop = 0;
                    startRow = startRowBak;
                    startPos = startPosBak;
                    dir = dirBak;
                    pastPos2.Clear();
                    pastPos2.Add(dir * 1000000 + startRow * 1000 + startPos);
                    looped = false;
                    //Console.WriteLine("B_ " + row + " -- " + pos);
                    while (startPos > -1 && startRow > -1 && startRow < lines.Count && startPos < lines[startRow].Length && loop < 50000 && loop > -1 && !looped)
                    {
                        //Console.WriteLine("A_ " + startRow + " -- " + startPos + " - " + dir);
                        loop++;
                        if (dir == 0) // Upp
                        {
                            startRow--;
                            if (!(startPos > -1 && startRow > -1 && startRow < lines.Count && startPos < lines[startRow].Length))
                            {
                                loop = -1;
                            }
                            else if (lines[startRow][startPos] == '#' || (startRow == row && startPos == pos))
                            {
                                dir = 2;//Right
                                startRow++;
                            }
                            else
                            {
                                if (pastPos2.Contains(dir * 1000000 + startRow * 1000 + startPos))
                                {
                                    looped = true;
                                }
                                else
                                {
                                    pastPos2.Add(dir * 1000000 + startRow * 1000 + startPos);
                                }
                                if (!pastPos.Contains(startRow * 1000 + startPos))
                                {
                                    pastPos.Add(startRow * 1000 + startPos);                                    
                                    total++;
                                }

                            }
                        }
                        else if (dir == 1) // Down
                        {
                            startRow++;
                            if (!(startPos > -1 && startRow > -1 && startRow < lines.Count && startPos < lines[startRow].Length))
                            {
                                loop = -1;
                            }
                            else if (lines[startRow][startPos] == '#' || (startRow == row && startPos == pos))
                            {
                                dir = 3;//Left
                                startRow--;
                            }
                            else
                            {
                                if (pastPos2.Contains(dir * 1000000 + startRow * 1000 + startPos))
                                {
                                    looped = true;
                                }
                                else
                                {
                                    pastPos2.Add(dir * 1000000 + startRow * 1000 + startPos);
                                }
                                if (!pastPos.Contains(startRow * 1000 + startPos))
                                {
                                    pastPos.Add(startRow * 1000 + startPos);                                    
                                    total++;
                                }
                            }
                        }
                        else if (dir == 2) // Right
                        {
                            startPos++;
                            if (!(startPos > -1 && startRow > -1 && startRow < lines.Count && startPos < lines[startRow].Length))
                            {
                                loop = -1;
                            }
                            else if (lines[startRow][startPos] == '#' || (startRow == row && startPos == pos))
                            {
                                dir = 1;//Down
                                startPos--;
                            }
                            else
                            {
                                if (pastPos2.Contains(dir * 1000000 + startRow * 1000 + startPos))
                                {
                                    looped = true;
                                }
                                else
                                {
                                    pastPos2.Add(dir * 1000000 + startRow * 1000 + startPos);
                                }
                                if (!pastPos.Contains(startRow * 1000 + startPos))
                                {
                                    pastPos.Add(startRow * 1000 + startPos);                                    
                                    total++;
                                }
                            }
                        }
                        else if (dir == 3) // Left
                        {
                            startPos--;
                            if (!(startPos > -1 && startRow > -1 && startRow < lines.Count && startPos < lines[startRow].Length))
                            {
                                loop = -1;
                            }
                            else if (lines[startRow][startPos] == '#' || (startRow == row && startPos == pos))
                            {
                                dir = 0;//Up
                                startPos++;
                            }
                            else
                            {
                                if (pastPos2.Contains(dir * 1000000 + startRow * 1000 + startPos))
                                {
                                    looped = true;
                                }
                                else
                                {
                                    pastPos2.Add(dir * 1000000 + startRow * 1000 + startPos);
                                }
                                if (!pastPos.Contains(startRow * 1000 + startPos))
                                {
                                    pastPos.Add(startRow * 1000 + startPos);                                    
                                    total++;
                                }
                            }
                        }
                    }
                    if ( loop >= 49000 || looped)
                    {
                        if ( looped)
                        {
                            Console.WriteLine("L: " + pastPos2.Count + " -- " + loop);
                        }
                        total2++;
                    }
                }
            }
            Console.WriteLine("Day06-2: " + total2);
        }
        
        
    }
}


