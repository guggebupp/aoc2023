




namespace Aoc2023.Solution2024
{
    internal class Day18
    {
        private static string day = "18";
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
            var corrupted = new List<int>();
            foreach (var line in lines)
            {
                if (line != "")
                {
                    var x = Int32.Parse(line.Split(',')[0]);
                    var y = Int32.Parse(line.Split(',')[1]);
                    corrupted.Add(y * 1000 + x);
                    if (corrupted.Count == 1024)
                    {
                        Console.WriteLine("Last: {0},{1}", x, y); 
                        break;
                    }
                }
            }
            var size = 71;
            var lines2 = new List<string>();
            for (int i = 0; i < size; i++)
            {
                var l = "";
                for (int j = 0; j < size; j++)
                {
                    if (i == 38 && j == 38)
                    {
                        //Console.WriteLine(l);
                    }
                    if (corrupted.Contains(i * 1000 + j))
                    {
                        l += "#";
                    }
                    else if ( i > 53 && j < 28)
                    {
                        l += "#";
                    }
                    else if (i < 50 && j > 24)
                    {
                        l += "#";
                    }
                    else if (i == 53 && j == 10)
                    {
                        l += "#";
                    }
                    else if (i == 52 && j > 13 && j < 28)
                    {
                        l += "#";
                    }
                    else if (i == 51 && j > 13 && j < 39)
                    {
                        l += "#";
                    }
                    else if (i == 54 && j == 28)
                    {
                        l += "#";
                    }
                    else if (i < 53 && i > 48 && j == 43)
                    {
                        l += "#";
                    }
                    else if (
                        j + 1 < size &&
                        i + 1 < size &&
                        i > 0 &&
                        j > 0 &&
                        !corrupted.Contains(i * 1000 + j - 1) &&
                        !corrupted.Contains(i * 1000 + j + 1) &&
                        !corrupted.Contains((i + 1) * 1000 + j) &&
                        !corrupted.Contains((i - 1) * 1000 + j) &&
                        !corrupted.Contains((i + 1) * 1000 + j + 1) &&
                        !corrupted.Contains((i + 1) * 1000 + j - 1) &&
                        !corrupted.Contains((i - 1) * 1000 + j + 1) &&
                        !corrupted.Contains((i - 1) * 1000 + j - 1))
                    {
                        l += ".";
                    }
                    
                    else
                    {
                        l += ".";
                    }
                }
                lines2.Add(l);
            }
            var handledPos = new List<int>();
            var handledPosSucseed = new List<int>();
            var bestMatchDict = new Dictionary<int, int>();
            var bestMatchDict2 = new Dictionary<int, List<int>>();
            printRes(handledPos, lines2);
            //total = Go3(lines2, 0, 0, size - 1, size - 1, Direction.Right, handledPos, bestMatchDict, bestMatchDict2, 160000, 0, handledPosSucseed);
            total = Go3(lines2, 0, 0, size - 1, size - 1, Direction.Down, handledPos, bestMatchDict, bestMatchDict2, 160000, 0, handledPosSucseed);
            Console.WriteLine("Day{0}-1: {1}", day, total);
        }


        public static void solve2()
        {
            var total = 0;
            var lines = Util.readFile(fileName);
            int val = 2849;
            while(total != 999999)
            {
                var corrupted = new List<int>();
                foreach (var line in lines)
                {
                    if (line != "")
                    {
                        var x = Int32.Parse(line.Split(',')[0]);
                        var y = Int32.Parse(line.Split(',')[1]);
                        corrupted.Add(y * 1000 + x);
                        if (corrupted.Count == val)
                        {
                            Console.WriteLine("Last: {0},{1}", x, y);
                            break;
                        }
                    }
                }
                var size = 71;
                var lines2 = new List<string>();
                for (int i = 0; i < size; i++)
                {
                    var l = "";
                    for (int j = 0; j < size; j++)
                    {
                        if (i == 38 && j == 38)
                        {
                            //Console.WriteLine(l);
                        }
                        if (corrupted.Contains(i * 1000 + j))
                        {
                            l += "#";
                        }
                       /** else if (i > 54 && j < 28)
                        {
                            l += "#";
                        }
                        else if (i < 50 && j > 24)
                        {
                            l += "#";
                        }
                        else if (i == 53 && j == 10)
                        {
                            l += "#";
                        }
                        else if (i == 52 && j > 13 && j < 28)
                        {
                            l += "#";
                        }
                        else if (i == 51 && j > 13 && j < 39)
                        {
                            l += "#";
                        }
                        else if (i == 54 && j == 28)
                        {
                            l += "#";
                        }
                        else if (i < 53 && i > 48 && j == 43)
                        {
                            l += "#";
                        } **/                      

                        else
                        {
                            l += ".";
                        }
                    }
                    lines2.Add(l);
                }
                var handledPos = new List<int>();
                var handledPosSucseed = new List<int>();
                var bestMatchDict = new Dictionary<int, int>();
                var bestMatchDict2 = new Dictionary<int, List<int>>();
                printRes(handledPos, lines2);
                //total = Go3(lines2, 0, 0, size - 1, size - 1, Direction.Right, handledPos, bestMatchDict, bestMatchDict2, 160000, 0, handledPosSucseed);
                total = Go3(lines2, 0, 0, size-1, size-1, Direction.Down, handledPos, bestMatchDict, bestMatchDict2, 160000, 0, handledPosSucseed);
                val++;
            }
            Console.WriteLine("Day{0}-2: {1}", day, val-1);
        }

        private static int Go3(List<string> lines, int startLine, int startCol, int endLine, int endCol, Direction currentDirection, List<int> handledPosIn, Dictionary<int, int> bestMatchDict, Dictionary<int, List<int>> bestMatchDict2, int currentBest, int currentRes, List<int> handledPosSucceed, int depp = 0)
        {
            //printRes(handledPosIn, lines);
            var valid = false;
            if (handledPosIn.Contains(startLine * 1000 + startCol))
            {
                return 999999;
            }
            //var key = (int)currentDirection * 1000000 + startLine * 1000 + startCol;
            var key = startLine * 1000 + startCol;

            if (bestMatchDict.TryGetValue(key, out var res2))
            {
                /**if ((res2 + currentRes) < 50000)
                {
                    Console.WriteLine("ERROR: {0} - {1} -  {2}", key, res2, currentRes);
                }**/
                bestMatchDict2.TryGetValue(key, out var l);
                if (l != null)
                {
                    handledPosSucceed.AddRange(l);
                }

                return res2 + currentRes;
            }
            var handledPos = new List<int>();
            handledPos.AddRange(handledPosIn);
            handledPos.Add(startLine * 1000 + startCol);
            var resUp = 999999;
            var resDown = 999999;
            var resRight = 999999;
            var resLeft = 999999;
            var canGo = false;
            //Get Grid size
            /**if (currentDirection == Direction.Right || currentDirection == Direction.Left) {
                if (CanGoRight(lines, startLine, startCol, currentDirection) && CanGoDown(lines, startLine, startCol, currentDirection))
                {
                    if (CanGoDown(lines, startLine, startCol+1, currentDirection))
                    {
                        var gridSize = 1;
                        var gridOk = true;
                        while (gridOk)
                        {
                            for (int gr = 0; gr < gridSize; gr++) {
                                //Höger
                                var testLine = startLine;
                                var testCol = startCol+1;
                                var right = CanGoRight(lines, testLine, testCol, currentDirection);
                                //Ned
                                testLine = startLine+1;
                                testCol = startCol;
                                var down = CanGoDown(lines, testLine, testCol, currentDirection);
                                //Ned, Höger
                                testLine = startLine + 1;
                                testCol = startCol+1;
                                var downDown = CanGoDown(lines, testLine, testCol, currentDirection);
                                var downRight = CanGoRight(lines, testLine, testCol, currentDirection);
                                var downRightDown = CanGoDown(lines, testLine, testCol+1, currentDirection);
                            }
                        }
                    }                    
                }
                    
                
            }**/
            if (CanGoRight(lines, startLine, startCol, currentDirection))
            {
                resRight = currentRes;
                canGo = true;
                var testLine = startLine;
                var testcol = startCol + 1;
                var testDir = Direction.Right;
                resRight += (currentDirection == testDir ? 1 : 1);
                if (resRight > currentBest)
                {
                    resRight = 999999;
                    //Console.Write(".");

                }
                else
                {
                    if (testLine == endLine && testcol == endCol)
                    {
                        Console.WriteLine("END {0} - {1}", resRight, handledPos.Count());
                        printRes(handledPos, lines);
                        if (resRight < currentBest)
                        {
                            handledPosSucceed.Clear();
                            handledPosSucceed.AddRange(handledPos);
                        }
                        if (resRight == currentBest)
                        {
                            handledPosSucceed.AddRange(handledPos);
                        }


                    }
                    else
                    {
                        resRight = Go3(lines, testLine, testcol, endLine, endCol, testDir, handledPos, bestMatchDict, bestMatchDict2, currentBest, resRight, handledPosSucceed);
                    }
                    if (resRight < currentBest)
                    {
                        currentBest = resRight;
                    }
                    if (resRight < 160000)
                    {
                        valid = true;
                    }
                }


            }
            if (CanGoDown(lines, startLine, startCol, currentDirection))
            {
                resDown = currentRes;
                canGo = true;
                var testLine = startLine + 1;
                var testcol = startCol;
                var testDir = Direction.Down;
                resDown += (currentDirection == testDir ? 1 : 1);
                if (resDown > currentBest)
                {
                    resDown = 999999;
                    //Console.Write(".");                    
                }
                else
                {
                    if (testLine == endLine && testcol == endCol)
                    {
                        Console.WriteLine("END {0} - {1}", resRight, handledPos.Count());
                        printRes(handledPos, lines);
                        if (resDown < currentBest)
                        {
                            handledPosSucceed.Clear();
                            handledPosSucceed.AddRange(handledPos);
                        }
                        if (resDown == currentBest)
                        {
                            handledPosSucceed.AddRange(handledPos);
                        }

                    }
                    else
                    {
                        resDown = Go3(lines, testLine, testcol, endLine, endCol, testDir, handledPos, bestMatchDict, bestMatchDict2, currentBest, resDown, handledPosSucceed);
                    }
                    if (resDown < currentBest)
                    {
                        currentBest = resDown;
                    }
                    if (resDown < 160000)
                    {
                        valid = true;
                    }
                }


            }

            if (CanGoUp(lines, startLine, startCol, currentDirection))
            {
                resUp = currentRes;
                canGo = true;
                var testLine = startLine - 1;
                var testcol = startCol;
                var testDir = Direction.Up;
                resUp += (currentDirection == testDir ? 1 : 1);
                if (resUp > currentBest)
                {
                    //Console.Write(".");
                    resUp = 999999;

                }
                else
                {
                    if (testLine == endLine && testcol == endCol)
                    {
                        //Console.WriteLine("END " + res + " -- " + handledPos.Count);

                        if (resUp < currentBest)
                        {
                            printRes(handledPos, lines);
                        }
                        Console.WriteLine("END {0} - {1}", resUp, handledPos.Count());
                        if (resUp < currentBest)
                        {
                            handledPosSucceed.Clear();
                            handledPosSucceed.AddRange(handledPos);
                        }
                        if (resUp == currentBest)
                        {
                            handledPosSucceed.AddRange(handledPos);
                        }



                    }
                    else
                    {
                        resUp = Go3(lines, testLine, testcol, endLine, endCol, testDir, handledPos, bestMatchDict, bestMatchDict2, currentBest, resUp, handledPosSucceed);
                    }
                    if (resUp < currentBest)
                    {
                        currentBest = resUp;
                    }
                    if (resUp < 160000)
                    {
                        valid = true;
                    }
                }


            }


            if (CanGoLeft(lines, startLine, startCol, currentDirection))
            {
                resLeft = currentRes;
                canGo = true;
                var testLine = startLine;
                var testcol = startCol - 1;
                var testDir = Direction.Left;
                resLeft += (currentDirection == testDir ? 1 : 1);
                if (resLeft > currentBest)
                {
                    resLeft = 999999;
                    //Console.Write(".");                    
                }
                else
                {
                    if (testLine == endLine && testcol == endCol)
                    {
                        Console.WriteLine("END {0}", resLeft);

                        if (resLeft < currentBest)
                        {
                            handledPosSucceed.Clear();
                            handledPosSucceed.AddRange(handledPos);
                        }
                        if (resLeft == currentBest)
                        {
                            handledPosSucceed.AddRange(handledPos);
                        }
                    }
                    else
                    {
                        resLeft = Go3(lines, testLine, testcol, endLine, endCol, testDir, handledPos, bestMatchDict, bestMatchDict2, currentBest, resLeft, handledPosSucceed);
                    }
                    if (resLeft < currentBest)
                    {
                        currentBest = resLeft;

                    }
                    if (resLeft < 160000)
                    {
                        valid = true;
                    }
                }

            }

            var res = resRight;
            if (resDown < res)
            {
                res = resDown;
            }
            if (resUp < res)
            {
                res = resUp;
            }
            if (resLeft < res)
            {
                res = resLeft;
            }

            if (canGo && valid)
            {

                if (bestMatchDict.TryGetValue(key, out var res3))
                {
                    if ((res - currentRes) < res3)
                    {
                        bestMatchDict[key] = (res - currentRes);
                    }
                }
                else
                {
                    if (currentBest < 75000)
                    {
                        Console.WriteLine("AA");
                    }
                    Console.WriteLine("Add: {0} - {1} -  {2}", key, res, (res - currentRes));
                    bestMatchDict.Add(key, (res - currentRes));
                    var v = new List<int>();
                    var start = handledPosSucceed.FindIndex(a => a == (startLine * 1000 + startCol));
                    v.AddRange(handledPosSucceed.TakeLast(handledPosSucceed.Count - start));
                    bestMatchDict2.Add(key, v);
                }
            }

            if (!canGo || !valid)
            {
                return 999999;
            }
            if (res < currentBest)
            {
                currentBest = res;
            }
            if (currentBest < 75000)
            {
                Console.WriteLine("AA");
            }
            return res;
        }

        private static void printRes(List<int> handledPos, List<string> lines)
        {
            Console.WriteLine(handledPos.Count);
            for (int i = 0; i < lines.Count; i++)
            {
                var l = "";
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (lines[i][j] == '#')
                    {
                        l += "#";
                    }
                    else if (lines[i][j] == 'Y')
                    {
                        l += "Y";
                    }
                    else if (handledPos.Contains(i * 1000 + j))
                    {
                        l += 'X';
                    }
                    else
                    {
                        l += ".";
                    }
                }
                Console.WriteLine(l);
            }
        }

        private static void CountRes(List<int> hadledPos)
        {
            var res = 0;
            var start = 139 * 1000 + 1;
            var current = start;
            var goOn = true;
            var currentDir = Direction.Right;
            while (goOn)
            {
                //Up
                if (hadledPos.Contains(start - 1000))
                {
                    res += currentDir == Direction.Up ? 1 : 1;
                }
            }
        }

        private static bool CanGoOnlyUp(List<string> lines, int testLine, int testcol, Direction currentDirection)
        {
            return CanGoUp(lines, testLine, testcol, currentDirection) && !CanGoDown(lines, testLine, testcol, currentDirection) && !CanGoLeft(lines, testLine, testcol, currentDirection) && !CanGoRight(lines, testLine, testcol, currentDirection);
        }

        private static bool CanGoOnlyDown(List<string> lines, int testLine, int testcol, Direction currentDirection)
        {
            return !CanGoUp(lines, testLine, testcol, currentDirection) && CanGoDown(lines, testLine, testcol, currentDirection) && !CanGoLeft(lines, testLine, testcol, currentDirection) && !CanGoRight(lines, testLine, testcol, currentDirection);
        }

        private static bool CanGoOnlyLeft(List<string> lines, int testLine, int testcol, Direction currentDirection)
        {
            return !CanGoUp(lines, testLine, testcol, currentDirection) && !CanGoDown(lines, testLine, testcol, currentDirection) && CanGoLeft(lines, testLine, testcol, currentDirection) && !CanGoRight(lines, testLine, testcol, currentDirection);
        }

        private static bool CanGoOnlyRight(List<string> lines, int testLine, int testcol, Direction currentDirection)
        {
            return !CanGoUp(lines, testLine, testcol, currentDirection) && !CanGoDown(lines, testLine, testcol, currentDirection) && !CanGoLeft(lines, testLine, testcol, currentDirection) && CanGoRight(lines, testLine, testcol, currentDirection);
        }

        private static bool CanGoUp(List<string> lines, int startLine, int startCol, Direction currentDirection)
        {
            if (currentDirection == Direction.Down)
            {
                return false;
            }
            if (startLine == 0 || lines[startLine - 1][startCol] == '#')
            {
                return false;
            }
            return true;
        }

        private static bool CanGoDown(List<string> lines, int startLine, int startCol, Direction currentDirection)
        {
            if (currentDirection == Direction.Up)
            {
                return false;
            }
            if ((startLine + 1) == lines.Count || lines[startLine + 1][startCol] == '#')
            {
                return false;
            }
            return true;
        }

        private static bool CanGoRight(List<string> lines, int startLine, int startCol, Direction currentDirection)
        {
            if (currentDirection == Direction.Left)
            {
                return false;
            }
            if ((startCol + 1) == lines[startLine].Length || lines[startLine][startCol + 1] == '#')
            {
                return false;
            }
            return true;
        }

        private static bool CanGoLeft(List<string> lines, int startLine, int startCol, Direction currentDirection)
        {
            if (currentDirection == Direction.Right)
            {
                return false;
            }
            if (startCol == 0 || lines[startLine][startCol - 1] == '#')
            {
                return false;
            }
            return true;
        }


    }
}
