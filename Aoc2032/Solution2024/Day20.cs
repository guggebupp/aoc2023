








namespace Aoc2023.Solution2024
{
    internal class Day20
    {
        private static string day = "20";
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
            var startX = 0;
            var startY = 0;
            var endX = 0;
            var endY = 0;
            var lineNr = 0;
            foreach (var line in lines)
            {
                if (line != "")
                {
                    if (line.Contains("S"))
                    {
                        startY = lineNr;
                        startX = line.IndexOf("S");
                    }
                    if (line.Contains("E"))
                    {
                        endY = lineNr;
                        endX = line.IndexOf("E");
                    }

                }
                lineNr++;
            }            
            var positions = Go2(lines, startY, startX, endY, endX, Direction.Left, new List<int>());
            getCheetSave(positions, lines);
            total = positions.Count();
            Console.WriteLine("Day20-1: " + total);
        }
        

        public static void solve2()
        {
            var total = 0;
            var lines = Util.readFile(fileName);
            var startX = 0;
            var startY = 0;
            var endX = 0;
            var endY = 0;
            var lineNr = 0;
            foreach (var line in lines)
            {
                if (line != "")
                {
                    if (line.Contains("S"))
                    {
                        startY = lineNr;
                        startX = line.IndexOf("S");
                    }
                    if (line.Contains("E"))
                    {
                        endY = lineNr;
                        endX = line.IndexOf("E");
                    }

                }
                lineNr++;
            }            
            var positions = Go2(lines, startY, startX, endY, endX, Direction.Left, new List<int>());
            /**var res = CheetHandler(positions, lines);
            var p = new Dictionary<int, int>();
            foreach (var line in res)
            {
                foreach (var line2 in line.Value)
                {
                    if (p.ContainsKey(line2))
                    {
                        p[line2]++;
                    } else
                    {
                        p.Add(line2, 1);
                    }
                }
            }**/
            var res = FindCheets2(positions, 20, 100);
            Console.WriteLine("--------------------------------");
            foreach(var line in res.OrderBy(a => a.Key))
            {
                Console.WriteLine(line.Key + " - " + line.Value);
            }            
            foreach (var r in res)
            {
                total += r.Value;
            }
            Console.WriteLine("Day20-2: " + total);
        }

        private static Dictionary<int, List<int>> CheetHandler(List<int> positions, List<string> lines)
        {
            /**
             * 
             */
            var foundCheets = new Dictionary<int, List<int>>();
            foreach (var pos in positions) {
                foundCheets.Add(pos, FindCheets(pos, positions, lines, 20));
            }            
            return foundCheets;
        }

        private static Dictionary<int, int> FindCheets2(List<int> positions, int maxSteps, int minEarned)
        {
            var res = new Dictionary<int, int>();
            for (int startindex = 0; startindex < positions.Count; startindex++) { 
                var startPos = positions[startindex];
                var startY = startPos / 1000;
                var startX = startPos % 1000;
                for ( int testIndex = startindex+ minEarned; testIndex < positions.Count;testIndex++)
                {
                    var testPos = positions[testIndex];
                    var testY = testPos / 1000;
                    var testX = testPos % 1000;
                    var diff = Math.Abs(testY - startY) + Math.Abs(testX-startX);
                    if ( diff > maxSteps)
                    {
                        continue;
                    }
                    var earned = testIndex - startindex - diff;
                    if ( earned >= minEarned)
                    {
                        if ( res.ContainsKey(earned))
                        {
                            res[earned]++;
                        } else
                        {
                            res.Add(earned, 1);
                        }
                    }

                }
            }
            return res;
        }

        private static List<int> FindCheets(int pos, List<int> positions, List<string> lines, int maxSteps)
        {
            var result = new List<int>();
            var save = new List<int>();
            /**for (int i = 0; i < maxSteps; i=i+2) {
                Cheatable(pos%1000, pos/1000, positions, lines, i, result, Direction.None, new List<int>());
            }**/
            int currentIndex = positions.IndexOf(pos);
            Cheatable(pos % 1000, pos / 1000, positions, lines, 20, result, Direction.None, currentIndex);
            var currentIdex = positions.FindIndex(a => a==pos);
            result.Distinct().ToList().ForEach(x => { 
                var testIndex = positions.IndexOf(x);
                if ( testIndex > currentIdex)
                {
                    var startY = pos % 1000;
                    var startX = pos / 1000;
                    var endY = x % 1000;
                    var endX = x / 1000;
                    var dist = testIndex - currentIdex -(Math.Abs(startY-endY)) - Math.Abs(startX-endX);
                    save.Add(dist);
                }                
            });
            Console.WriteLine(pos + " - " + save.Where(a => a >= 50 ).Count());
            return save.Where(a => a >= 50).ToList();
        }

        private static List<int> Cheatable(int currentLine, int currentPos, List<int> positions, List<string> lines, int steps, List<int> res, Direction dir, int currentIndex, int currentStep = 0)
        {
            var testLine = currentLine;
            var testPos = currentPos;
            //var index = positions.IndexOf(testLine * 1000 + testPos);
            if (currentStep == steps )
            {
                    //No cheat
                    //res.Add(-1);
                    return res;
            }

            
            /**if (index != -1 && index < currentIndex)
            {
                return res;
            }**/
            //visited.Add(testLine*1000+testPos);
            //Gå höger
            if (dir != Direction.Left)
            {

                testLine = currentLine;
                testPos = currentPos + 1;
                var testDir = Direction.Right;
                /**if ((currentStep + 1) == steps && positions.Contains(testLine * 1000 + testPos))
                {
                    var key = testLine * 1000 + testPos;
                    if (!res.Contains(key))
                    {
                        res.Add(key);
                    }
                }
                else if (Util.isInside(lines, testLine, testPos) && lines[testLine][testPos] == '#')
                {
                    var visited2 = new List<int>();
                    visited2.AddRange(visited);
                    Cheatable(testLine, testPos, positions, lines, steps, res, testDir, visited2, currentStep + 1);
                }**/
                var key = testLine * 1000 + testPos;
                if ((currentStep + 1) == steps && positions.Contains(testLine * 1000 + testPos))
                {
                    
                    if (!res.Contains(key))
                    {
                        res.Add(key);
                    }
                }
                else if (Util.isInside(lines, testLine, testPos))
                {
                    if (positions.Contains(testLine * 1000 + testPos))
                    {                        
                        if (!res.Contains(key))
                        {
                            res.Add(key);
                        }
                    }                    
                    Cheatable(testLine, testPos, positions, lines, steps, res, testDir, currentIndex, currentStep + 1);
                }
            }
            if (dir != Direction.Right)
            {
                //Gå vänster
                testLine = currentLine;
                testPos = currentPos - 1;
                var testDir = Direction.Left;
                /**if ((currentStep + 1) == steps && positions.Contains(testLine * 1000 + testPos))
                {
                    var key = testLine * 1000 + testPos;
                    if (!res.Contains(key))
                    {
                        res.Add(key);
                    }
                }
                else if (Util.isInside(lines, testLine, testPos) && lines[testLine][testPos] == '#')
                {
                    var visited2 = new List<int>();
                    visited2.AddRange(visited);
                    Cheatable(testLine, testPos, positions, lines, steps, res, testDir, visited2, currentStep + 1);
                }**/
                var key = testLine * 1000 + testPos;
                if ((currentStep + 1) == steps && positions.Contains(testLine * 1000 + testPos))
                {

                    if (!res.Contains(key))
                    {
                        res.Add(key);
                    }
                }
                else if (Util.isInside(lines, testLine, testPos))
                {
                    if (positions.Contains(testLine * 1000 + testPos))
                    {
                        if (!res.Contains(key))
                        {
                            res.Add(key);
                        }
                    }
                    Cheatable(testLine, testPos, positions, lines, steps, res, testDir, currentIndex, currentStep + 1);
                }
            }
            if (dir != Direction.Down)
            {
                //Gå upp            
                testLine = currentLine - 1;
                testPos = currentPos;
                var testDir = Direction.Up;
                /**if ((currentStep + 1) == steps && positions.Contains(testLine * 1000 + testPos))
                {
                    var key = testLine * 1000 + testPos;
                    if (!res.Contains(key))
                    {
                        res.Add(key);
                    }
                }
                else if (Util.isInside(lines, testLine, testPos) && lines[testLine][testPos] == '#')
                {
                    var visited2 = new List<int>();
                    visited2.AddRange(visited);
                    Cheatable(testLine, testPos, positions, lines, steps, res, testDir, visited2, currentStep + 1);
                }**/
                var key = testLine * 1000 + testPos;
                if ((currentStep + 1) == steps && positions.Contains(testLine * 1000 + testPos))
                {

                    if (!res.Contains(key))
                    {
                        res.Add(key);
                    }
                }
                else if (Util.isInside(lines, testLine, testPos))
                {
                    if (positions.Contains(testLine * 1000 + testPos))
                    {
                        if (!res.Contains(key))
                        {
                            res.Add(key);
                        }
                    }
                    Cheatable(testLine, testPos, positions, lines, steps, res, testDir, currentIndex, currentStep + 1);
                }
            }
            if (dir != Direction.Up) {
                //Gå ned
                testLine = currentLine + 1;
                testPos = currentPos;
                var testDir = Direction.Down;
                /**if ((currentStep + 1) == steps && positions.Contains(testLine * 1000 + testPos))
                {
                    var key = testLine * 1000 + testPos;
                    if (!res.Contains(key))
                    {
                        res.Add(key);
                    }
                }
                else if (Util.isInside(lines, testLine, testPos) && lines[testLine][testPos] == '#')
                {
                    var visited2 = new List<int>();
                    visited2.AddRange(visited);
                    Cheatable(testLine, testPos, positions, lines, steps, res, testDir, visited2, currentStep + 1);
                }**/
                var key = testLine * 1000 + testPos;
                if ((currentStep + 1) == steps && positions.Contains(testLine * 1000 + testPos))
                {

                    if (!res.Contains(key))
                    {
                        res.Add(key);
                    }
                }
                else if (Util.isInside(lines, testLine, testPos))
                {
                    if (positions.Contains(testLine * 1000 + testPos))
                    {
                        if (!res.Contains(key))
                        {
                            res.Add(key);
                        }
                    }
                    Cheatable(testLine, testPos, positions, lines, steps, res, testDir, currentIndex, currentStep + 1);
                }
            }
            return res;
        }
        

        private static List<int> Go2(List<string> lines, int startLine, int startCol, int endLine, int endCol, Direction currentDirection, List<int> handledPos)
        {
            handledPos.Add(startLine* 1000 + startCol);
            while(startLine != endLine || startCol != endCol)
            {
                if ( CanGoRight(lines, startLine, startCol, currentDirection))
                {
                    startCol++;
                    currentDirection = Direction.Right;
                }
                else if (CanGoLeft(lines, startLine, startCol, currentDirection))
                {
                    startCol--;
                    currentDirection = Direction.Left;
                }
                else if (CanGoDown(lines, startLine, startCol, currentDirection))
                {
                    startLine++;
                    currentDirection = Direction.Down;
                }
                else if (CanGoUp(lines, startLine, startCol, currentDirection))
                {
                    startLine--;
                    currentDirection = Direction.Up;
                }
                handledPos.Add(startLine * 1000 + startCol);
            }
            printRes(handledPos, lines);            
            return handledPos;
        }
    

        private static void getCheetSave(List<int> handledPos, List<string> lines)
        {
            var saves = new List<int>();
            //För varje position, finns det en position längre fram som är två eller en pos bort i x eller y led
            foreach (var pos in handledPos)
            {
                //X
                var currentIndex = handledPos.FindIndex(a => a == pos);
                var xOffset = 2;
                if (!handledPos.Contains(pos + xOffset-1))
                {
                    if (handledPos.Contains(pos + xOffset))
                    {
                        var testIndex = handledPos.FindIndex(a => a == pos + xOffset);
                        if (currentIndex < testIndex)
                        {
                            var res = (testIndex - currentIndex - 2);
                            Console.WriteLine("A1: " + pos + " - " + res);
                            saves.Add(res);
                        }

                    }
                    else
                    {

                        xOffset = 3;
                        if (handledPos.Contains(pos + xOffset) && !handledPos.Contains(pos + xOffset - 1))
                        {
                            var testIndex = handledPos.FindIndex(a => a == pos + xOffset);
                            if (currentIndex < testIndex)
                            {
                                var res = (testIndex - currentIndex - 3);
                                Console.WriteLine("A2: " + pos + " - " + res);
                                saves.Add(res);                                
                            }
                        }
                    }
                }
                
               
                xOffset = -2;
                if ( !handledPos.Contains(pos + xOffset+1))
                {
                    if (handledPos.Contains(pos + xOffset))
                    {
                        var testIndex = handledPos.FindIndex(a => a == pos + xOffset);
                        if (currentIndex < testIndex)
                        {
                            var res = (testIndex - currentIndex - 2);
                            Console.WriteLine("A3: " + pos + " - " + res);
                            saves.Add(res);
                            
                        }
                    }
                    else
                    {

                        xOffset = -3;
                        if (handledPos.Contains(pos + xOffset) && !handledPos.Contains(pos + xOffset + 1))
                        {
                            var testIndex = handledPos.FindIndex(a => a == pos + xOffset);
                            if (currentIndex < testIndex)
                            {
                                var res = (testIndex - currentIndex - 3);
                                Console.WriteLine("A4: " + pos + " - " + res);
                                saves.Add(res);
                            }
                        }
                    }
                }
                    

                var yOffset = 2;
                if (  !handledPos.Contains(pos + 1000*(yOffset-1)))
                {
                    if (handledPos.Contains(pos + (1000 * yOffset)))
                    {
                        var testIndex = handledPos.FindIndex(a => a == pos + (1000 * yOffset));
                        if (currentIndex < testIndex)
                        {
                            var res = (testIndex - currentIndex - 2);
                            Console.WriteLine("B1: " + pos + " - " + res);
                            saves.Add(res);
                        }
                    }
                    else
                    {
                        yOffset = 3;
                        if (handledPos.Contains(pos + (1000 * yOffset)) && !handledPos.Contains(pos + (1000 * (yOffset - 1))))
                        {
                            var testIndex = handledPos.FindIndex(a => a == pos + (1000 * yOffset));
                            if (currentIndex < testIndex)
                            {
                                var res = (testIndex - currentIndex - 3);
                                Console.WriteLine("B2: " + pos + " - " + res);
                                saves.Add(res);
                            }
                        }
                    }
                }
                    
                    yOffset = -2;
                if (!handledPos.Contains(pos + 1000 * (yOffset + 1)))
                {
                    if (handledPos.Contains(pos + (1000 * yOffset)))
                    {
                        var testIndex = handledPos.FindIndex(a => a == pos + (1000 * yOffset));
                        if (currentIndex < testIndex)
                        {
                            var res = (testIndex - currentIndex - 2);
                            Console.WriteLine("B3: " + pos + " - " + res);
                            saves.Add(res);
                        }
                    }
                    else
                    {
                        yOffset = -3;
                        if (handledPos.Contains(pos + (1000 * yOffset)) && !handledPos.Contains(pos + (1000 * (yOffset + 1))))
                        {
                            var testIndex = handledPos.FindIndex(a => a == pos + (1000 * yOffset));
                            if (currentIndex < testIndex)
                            {
                                var res = (testIndex - currentIndex - 3);
                                Console.WriteLine("B4: " + pos + " - " + res);
                                saves.Add(res);
                            }
                        }
                    }
                }


            }
            Console.WriteLine("AA: "  + saves.Where(a => a!= 0).Count() + " - " +saves.Where(a => a >= 100).Count());
            return;
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
