






using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aoc2023.Solution2024
{
    internal class Day16
    {
        private static string day = "16";
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
            var startCol = 0;
            var startLine =  0;
            var endCol = 0;
            var endLine = 0;
            for (int line = 0; line < lines.Count; line++) { 
                if (lines[line].Contains("S"))
                {
                    startLine = line;
                    startCol = lines[line].IndexOf("S");
                }
                if (lines[line].Contains("E"))
                {
                    endLine = line;
                    endCol = lines[line].IndexOf("E");
                }
            }
            var dirRes = new List<int>();
            total = -1;
            var handledPos = new List<int>();
            var handledPosSucseed = new List<int>();
            var bestMatchDict = new Dictionary<int, int>();
            var bestMatchDict2 = new Dictionary<int, List<int>>();
            total = Go3(lines, startLine, startCol, endLine, endCol, Direction.Right, handledPos, bestMatchDict, bestMatchDict2, 160000, 0, handledPosSucseed);

            printRes(handledPosSucseed.Distinct().ToList(), lines);
            Console.WriteLine("Day" + day + "- 1: " + total + " - " + (handledPosSucseed.Distinct().Count()+1));
        }

        private static int Go(List<string> lines, int startLine, int startCol, int endLine, int endCol, Direction currentDirection, List<int> handledPosIn)
        {
            var handledPos = new List<int>();
            handledPos.AddRange(handledPosIn);
            var result = 999999;
            if ( handledPos.Contains(startLine*1000+startCol))
            {
                return 999999;
            }
            handledPos.Add(startLine*1000+startCol);
            
            var res = new List<int>();
            if (CanGoUp(lines, startLine, startCol, currentDirection)) {
                var testLine = startLine - 1;
                var testcol = startCol;
                var testDir = Direction.Up;
                if (testLine == endLine && testcol == endCol)
                {
                    res.Add(1 + (currentDirection == testDir ?  0 : 1000));                    
                } else
                {
                    res.Add(1+ Go(lines, testLine, testcol, endLine, endCol, testDir, handledPos) + (currentDirection == testDir ? 0 : 1000));
                }
            }
            if (CanGoDown(lines, startLine, startCol, currentDirection)) {
                var testLine = startLine + 1;
                var testcol = startCol;
                var testDir = Direction.Down;
                if (testLine == endLine && testcol == endCol)
                {
                    res.Add(1 + (currentDirection == testDir ? 0 : 1000));
                }
                else
                {
                    res.Add(1 + Go(lines, testLine, testcol, endLine, endCol, testDir, handledPos) + (currentDirection == testDir ? 0 : 1000));
                }
            }
            if (CanGoLeft(lines, startLine, startCol, currentDirection)) {
                var testLine = startLine;
                var testcol = startCol-1;
                var testDir = Direction.Left;
                if (testLine == endLine && testcol == endCol)
                {
                    res.Add(1 + (currentDirection == testDir ? 0 : 1000));
                }
                else
                {
                    res.Add(1 + Go(lines, testLine, testcol, endLine, endCol, testDir, handledPos) + (currentDirection == testDir ? 0 : 1000));
                }
            }
            if (CanGoRight(lines, startLine, startCol, currentDirection)) {
                var testLine = startLine;
                var testcol = startCol+1;
                var testDir = Direction.Right;
                if (testLine == endLine && testcol == endCol)
                {
                    res.Add(1 + (currentDirection == testDir ? 0 : 1000));
                }
                else
                {
                    res.Add(1 + Go(lines, testLine, testcol, endLine, endCol, testDir, handledPos) + (currentDirection == testDir ? 0 : 1000));
                }
            }
            foreach (var re in res) { 
                if ( result == 999999 || re < result)
                {
                    result = re;
                }
            }
            return result;
            
        }

        private static int Go2(List<string> lines, int startLine, int startCol, int endLine, int endCol, Direction currentDirection, List<int> handledPosIn, Dictionary<int, int>  bestMatchDict, int depp = 0)
        {
            //Console.WriteLine(depp);
            
            var result = 999999;
            if ( startLine == 119 && startCol == 1 )
            {
                Console.WriteLine("A");
            }
            if (handledPosIn.Contains(startLine * 1000 + startCol))
            {
                return 999999;
            }
            var handledPos = new List<int>();
            handledPos.AddRange(handledPosIn);
            handledPos.Add(startLine * 1000 + startCol);
            

            //var res = new List<int>();
            int resUp = 999999;
            int resDown = 999999;
            int resLeft = 999999;
            int resRight = 999999;

            if (CanGoRight(lines, startLine, startCol, currentDirection))
            {
                handledPos.Clear();
                handledPos.AddRange(handledPosIn);
                handledPos.Add(startLine * 1000 + startCol);
                var testLine = startLine;
                var testcol = startCol + 1;
                var testDir = Direction.Right;
                var steps = 0;
                var directionValue = 3;
                if (bestMatchDict.ContainsKey(directionValue * 1000000 + startLine * 1000 + startCol))
                {
                    var bestMatch = bestMatchDict.GetValueOrDefault(directionValue * 1000000 + startLine * 1000 + startCol);
                    if (bestMatch < 999999)
                    {
                        //  printRes(handledPos, lines);
                    }
                    return bestMatch;
                }
                while (CanGoOnlyRight(lines, testLine, testcol, currentDirection))
                {
                    handledPos.Add(testLine * 1000 + testcol);
                    testcol++;
                    steps++;
                }
                if (testLine == endLine && testcol == endCol)
                {
                    resRight = steps + 1 + (currentDirection == testDir ? 0 : 1000);
                }
                else
                {
                    resRight = steps + 1 + Go2(lines, testLine, testcol, endLine, endCol, testDir, handledPos, bestMatchDict, depp++) + (currentDirection == testDir ? 0 : 1000);
                }

                if (bestMatchDict.ContainsKey(directionValue * 1000000 + startLine * 1000 + startCol))
                {
                    var val = bestMatchDict.GetValueOrDefault(directionValue * 1000000 + startLine * 1000 + startCol);
                    if (resRight < val)
                    {
                        val = resRight;
                    }
                }
                else
                {
                    //Console.WriteLine("Add: {0} - {1} - {2}",startLine, startCol, resRight);
                    bestMatchDict.Add(directionValue * 1000000 + startLine * 1000 + startCol, resRight);
                }
            }
            if (CanGoUp(lines, startLine, startCol, currentDirection))
            {
                var testLine = startLine - 1;
                var testcol = startCol;
                var testDir = Direction.Up;
                var steps = 0;
                var directionValue = 0;
                if (bestMatchDict.ContainsKey(directionValue * 1000000 + startLine * 1000 + startCol))
                {
                    var bestMatch = bestMatchDict.GetValueOrDefault(directionValue * 1000000 + startLine * 1000 + startCol);
                    if ( bestMatch < 999999)
                    {
                        //printRes(handledPos, lines);
                    }
                    return bestMatch;
                }
                while (CanGoOnlyUp(lines, testLine, testcol, currentDirection)) {
                    handledPos.Add(testLine * 1000 + testcol);
                    testLine--;
                    steps++;
                }             
                if (testLine == endLine && testcol == endCol)
                {
                    resUp = steps + 1 + (currentDirection == testDir ? 0 : 1000);
                    //printRes(handledPos, lines);
                    
                }
                else
                {
                    resUp = steps + 1 + Go2(lines, testLine, testcol, endLine, endCol, testDir, handledPos, bestMatchDict, depp++) + (currentDirection == testDir ? 0 : 1000);
                }
                if (bestMatchDict.ContainsKey(directionValue * 1000000 + startLine * 1000 + startCol))
                {
                    var val = bestMatchDict.GetValueOrDefault(directionValue * 1000000 + startLine * 1000 + startCol);
                    if (resUp < val)
                    {
                        val = resUp;
                    }
                }
                else
                {
                    // Console.WriteLine("Add: " + startLine + " - " + startCol + " - " + resUp);
                    //Console.WriteLine("Add: {0} - {1} - {2}", startLine, startCol, resUp);
                    bestMatchDict.Add(directionValue * 1000000 + startLine * 1000 + startCol, resUp);
                }
            }
            if (CanGoDown(lines, startLine, startCol, currentDirection))
            {
                handledPos.Clear();
                handledPos.AddRange(handledPosIn);
                handledPos.Add(startLine * 1000 + startCol);
                var testLine = startLine + 1;
                var testcol = startCol;
                var testDir = Direction.Down;
                var steps = 0;
                var directionValue = 1;
                if (bestMatchDict.ContainsKey(directionValue * 1000000 + startLine * 1000 + startCol))
                {
                    var bestMatch = bestMatchDict.GetValueOrDefault(directionValue * 1000000 + startLine * 1000 + startCol);
                    if (bestMatch < 999999)
                    {
                        //printRes(handledPos, lines);
                    }
                    return bestMatch;
                }
                while (CanGoOnlyDown(lines, testLine, testcol, currentDirection))
                {
                    handledPos.Add(testLine * 1000 + testcol);
                    testLine++;
                    steps++;
                }
                if (testLine == endLine && testcol == endCol)
                {
                    resDown = steps + 1 + (currentDirection == testDir ? 0 : 1000);
                }
                else
                {
                    resDown = steps + 1 + Go2(lines, testLine, testcol, endLine, endCol, testDir, handledPos, bestMatchDict, depp++) + (currentDirection == testDir ? 0 : 1000);
                }
                if (bestMatchDict.ContainsKey(directionValue * 1000000 + startLine * 1000 + startCol))
                {
                    var val = bestMatchDict.GetValueOrDefault(directionValue * 1000000 + startLine * 1000 + startCol);
                    if (resDown < val)
                    {
                        val = resDown;
                    }
                }
                else
                {
                 //   Console.WriteLine("Add: " + startLine + " - " + startCol + " - " + resDown);
                    bestMatchDict.Add(directionValue * 1000000 + startLine * 1000 + startCol, resDown);
                }
            }
            if (CanGoLeft(lines, startLine, startCol, currentDirection))
            {
                handledPos.Clear();
                handledPos.AddRange(handledPosIn);
                handledPos.Add(startLine * 1000 + startCol);
                var testLine = startLine;
                var testcol = startCol - 1;
                var testDir = Direction.Left;
                var steps = 0;
                var directionValue = 2;
                if (bestMatchDict.ContainsKey(directionValue * 1000000 + startLine * 1000 + startCol))
                {
                    var bestMatch = bestMatchDict.GetValueOrDefault(directionValue * 1000000 + startLine * 1000 + startCol);
                    if (bestMatch < 999999)
                    {
                        //printRes(handledPos, lines);
                    }
                    return bestMatch;
                }
                while (CanGoOnlyLeft(lines, testLine, testcol, currentDirection))
                {
                    handledPos.Add(testLine * 1000 + testcol);
                    testcol--;
                    steps++;
                }
                if (testLine == endLine && testcol == endCol)
                {
                    resLeft = steps + 1 + (currentDirection == testDir ? 0 : 1000);
                }
                else
                {
                    resLeft = steps + 1 + Go2(lines, testLine, testcol, endLine, endCol, testDir, handledPos, bestMatchDict, depp++) + (currentDirection == testDir ? 0 : 1000);
                }
                if (bestMatchDict.ContainsKey(directionValue * 1000000 + startLine * 1000 + startCol))
                {
                    var val = bestMatchDict.GetValueOrDefault(directionValue * 1000000 + startLine * 1000 + startCol);
                    if (resLeft < val)
                    {
                        val = resLeft;
                    }
                }
                else
                {
                    //Console.WriteLine("Add: " + startLine + " - " + startCol + " - " + resLeft);
                    bestMatchDict.Add(directionValue * 1000000 + startLine * 1000 + startCol, resLeft);
                }
            }
            
            /**foreach (var re in res)
            {
                if (result == 999999 || re < result)
                {
                    result = re;
                }
            }**/
            if ( resUp < result)
            {
                result = resUp;
            }
            if (resDown < result)
            {
                result = resDown;
            }
            if (resRight < result)
            {
                result = resRight;
            }
            if (resLeft < result)
            {
                result = resLeft;
            }
           /** var match = 0;
            if ( result < 999999)
            {
                if ( resUp == result )
                {
                match++; 
                }
                if (resDown == result)
                {
                    match++;
                }
                if (resRight == result)
                {
                    match++;
                }
                if (resLeft == result)
                {
                    match++;
                }
                if (match > 1)
                {
                    Console.WriteLine("Mulit");
                }

                //Console.WriteLine("Current: " + result + " - " + startLine + " - " + startCol);
                //Console.WriteLine("Current: {0} - {1} - {2}",result, startLine, startCol);                
            }**/
            
            return result;

        }

        private static int Go3(List<string> lines, int startLine, int startCol, int endLine, int endCol, Direction currentDirection, List<int> handledPosIn, Dictionary<int, int> bestMatchDict, Dictionary<int, List<int>> bestMatchDict2, int currentBest, int currentRes, List<int> handledPosSucceed, int depp = 0)
        {            
            var valid = false;
            if (handledPosIn.Contains(startLine * 1000 + startCol))
            {
                return 999999;
            }
            var key = (int)currentDirection * 1000000 + startLine * 1000 + startCol;            
            
            if (bestMatchDict.TryGetValue(key, out var res2))
            { 
                if ( (res2+currentRes) < 50000)
                {
                    Console.WriteLine("ERROR: {0} - {1} -  {2}", key, res2, currentRes);
                }                
                bestMatchDict2.TryGetValue(key, out var l);
                if (l != null)
                {
                    handledPosSucceed.AddRange(l);
                }
                
                return res2+currentRes;
            }
            var handledPos = new List<int>();
            handledPos.AddRange(handledPosIn);
            handledPos.Add(startLine * 1000 + startCol);
            var resUp = 999999;
            var resDown = 999999;
            var resRight = 999999;
            var resLeft = 999999;
            var canGo = false;
            if (CanGoRight(lines, startLine, startCol, currentDirection))
            {
                resRight = currentRes;
                canGo = true;
                var testLine = startLine;
                var testcol = startCol + 1;
                var testDir = Direction.Right;
                resRight += (currentDirection == testDir ? 1 : 1001);
                if (resRight > currentBest)
                {
                    resRight = 999999;
                    //Console.Write(".");
                    
                }else
                {
                    if (testLine == endLine && testcol == endCol)
                    {
                        Console.WriteLine("END {0} - {1}", resRight, handledPos.Count());
                        //printRes(handledPos, lines);
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
            if (CanGoUp(lines, startLine, startCol, currentDirection))
            {
                resUp = currentRes;
                canGo = true;
                var testLine = startLine - 1;
                var testcol = startCol;
                var testDir = Direction.Up;
                resUp += (currentDirection == testDir ? 1 : 1001);
                if (resUp > currentBest)
                {
                    //Console.Write(".");
                    resUp = 999999;

                } else
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
            if (CanGoDown(lines, startLine, startCol, currentDirection))
            {
                resDown = currentRes;
                canGo = true;
                var testLine = startLine + 1;
                var testcol = startCol;
                var testDir = Direction.Down;
                resDown += (currentDirection == testDir ? 1 : 1001);
                if (resDown > currentBest)
                {
                    resDown = 999999;
                    //Console.Write(".");                    
                } else
                {
                    if (testLine == endLine && testcol == endCol)
                    {
                        Console.WriteLine("END {0}", resDown);
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
            
            if (CanGoLeft(lines, startLine, startCol, currentDirection))
            {
                resLeft = currentRes;
                canGo = true;
                var testLine = startLine;
                var testcol = startCol - 1;
                var testDir = Direction.Left;
                resLeft += (currentDirection == testDir ? 1 : 1001);
                if (resLeft > currentBest)
                {
                    resLeft = 999999;
                    //Console.Write(".");                    
                } else
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
            if (  resDown < res)
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
            
            if (canGo && valid )
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
                    v.AddRange(handledPosSucceed.TakeLast(handledPosSucceed.Count-start));
                    bestMatchDict2.Add(key, v);
                }
            }
                
            if ( !canGo || !valid )
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
            while (goOn) { 
                //Up
                if ( hadledPos.Contains(start-1000))
                {
                    res += currentDir == Direction.Up ? 1 : 1001;
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
            if (currentDirection == Direction.Down) {
                return false;
            }
            if ( startLine == 0 || lines[startLine - 1][startCol] == '#') {
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
            if ((startLine+1) == lines.Count || lines[startLine + 1][startCol] == '#') {
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
            if ((startCol + 1) == lines[startLine].Length || lines[startLine][startCol+1] == '#') {
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
            if (startCol == 0 || lines[startLine][startCol - 1] == '#') {
                return false;
            }
            return true;
        }

        public static void solve2()
        {
            var total = 0;
            var lines = Util.readFile(fileName);            
            foreach (var line in lines)
            {
                if (line != "")
                {                    
                }
            }
            Console.WriteLine("Day" + day + "- 2: " + total);
        }
        
        
    }
}
