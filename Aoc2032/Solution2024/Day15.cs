







using System.Transactions;

namespace Aoc2023.Solution2024
{
    internal class Day15
    {
        private static string day = "15";
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
            var moves = "";
            var warehouse = new List<string>();
            var startPos = 0;
            var lineNr = 0;
            foreach (var line in lines)
            {
                if (line != "")
                {
                    if (line[0] == '#')
                    {
                        warehouse.Add(line);
                    } else
                    {
                        moves += line;
                    }                    
                }
                if ( line.Contains("@"))
                {
                    startPos = lineNr*100+line.IndexOf("@");
                }
                lineNr++;
            }
            Console.WriteLine("A: " + warehouse.Sum(a => a.Where(b => b == 'O').Count()));
            for (int line = 0; line < warehouse.Count; line++)
            {
                for (int col = 0; col < warehouse[line].Length; col++)
                {
                    if (warehouse[line][col] == 'O')
                    {
                        total += line * 100 + col;
                    }
                }
            }
            Console.WriteLine("Day" + day + "- 1: " + total);
            warehouse = Move(0, moves, warehouse, startPos);
            total = 0;
            for (int line = 0; line < warehouse.Count; line++)
            {
                for (int col = 0; col < warehouse[line].Length; col++)
                {
                    if (warehouse[line][col] == 'O')
                    {
                        total += line * 100 + col;
                    }
                }
            }
            Console.WriteLine("A: " + warehouse.Sum(a => a.Where(b => b == 'O').Count()));
            Console.WriteLine("Day" + day + "- 1: " + total);
        }

        private static List<string> Move(int v, string moves, List<string> warehouse, int startPos)
        {
            if (v == moves.Length)
            {
                return warehouse;
            }
            
            while (v < moves.Length)
            {
                var currentMove = moves[v];
                startPos = getStartPos(warehouse);
                //Console.WriteLine(currentMove);
                switch (currentMove)
                {
                    case '>':
                        warehouse = goRight(warehouse, startPos);
                        break;
                    case '<':
                        warehouse = goLeft(warehouse, startPos);
                        break;
                    case '^':
                        warehouse = goUp(warehouse, startPos);
                        break;
                    case 'v':
                        warehouse = goDown(warehouse, startPos);
                        break;

                }
                v++;
            }


            /**foreach (var line in warehouse) { 
                Console.WriteLine(line); 
            }**/
            //return Move(v+1, moves, warehouse, getStartPos(warehouse));
            return warehouse;
        }

        private static List<string> goRight(List<string> warehouse, int startPos)
        {            
            var currentLineNr = startPos / 100;
            var currentPosNr = startPos % 100;
            var currentLine = warehouse[currentLineNr];
            var nextHash = currentLine.IndexOf("#", currentPosNr);
            //Går det flytta höger då måste det finnas någon . till höger innan näst #
            if (currentLine.Substring(currentPosNr, nextHash-currentPosNr).Contains("."))
            {
                //Är nästa . flytta och returnera
                if (currentLine[currentPosNr+1] == '.')
                {
                    currentLine = currentLine.Remove(currentPosNr,1);
                    currentLine = currentLine.Insert(currentPosNr, ".");
                    currentLine = currentLine.Remove(currentPosNr+1, 1);
                    currentLine = currentLine.Insert(currentPosNr+1, "@");
                }
                else 
                {
                    //var nexEmpty = currentLine.Substring(currentPosNr, nextHash - currentPosNr).IndexOf(".");
                    var nexEmpty = currentLine.IndexOf(".", currentPosNr);
                    currentLine = currentLine.Remove(nexEmpty, 1);
                    currentLine = currentLine.Insert(nexEmpty, "O");
                    currentLine = currentLine.Remove(currentPosNr, 1);
                    currentLine = currentLine.Insert(currentPosNr, ".");
                    currentLine = currentLine.Remove(currentPosNr + 1, 1);
                    currentLine = currentLine.Insert(currentPosNr + 1, "@");
                }
                warehouse.RemoveAt(currentLineNr);
                warehouse.Insert(currentLineNr, currentLine);
            }
            return warehouse;
        }

        private static int getStartPos(List<string> warehouse)
        {
            for (int i = 0; i < warehouse.Count; i++)
            {
                for (int j = 0; j < warehouse[i].Length;j++)
                {
                    if ( warehouse[i][j] == '@')
                    {
                        return i * 100 + j;
                    }
                }
            }
            return 0;
        }

        private static List<string> goLeft(List<string> warehouse, int startPos)
        {
            
            var currentLineNr = startPos / 100;
            var currentPosNr = startPos % 100;
            var currentLine = warehouse[currentLineNr];
            var nextHash = currentLine.LastIndexOf("#", currentPosNr);
            //Går det flytta höger då måste det finnas någon . till höger innan näst #
            if (currentLine.Substring(nextHash, currentPosNr-nextHash).Contains("."))
            {
                //Är nästa . flytta och returnera
                if (currentLine[currentPosNr - 1] == '.')
                {
                    currentLine = currentLine.Remove(currentPosNr, 1);
                    currentLine = currentLine.Insert(currentPosNr, ".");
                    currentLine = currentLine.Remove(currentPosNr - 1, 1);
                    currentLine = currentLine.Insert(currentPosNr - 1, "@");
                }
                else
                {
                    //var nexEmpty = currentLine.Substring(nextHash, currentPosNr).LastIndexOf(".");
                    var nexEmpty = currentLine.LastIndexOf(".", currentPosNr);
                    currentLine = currentLine.Remove(nexEmpty, 1);
                    currentLine = currentLine.Insert(nexEmpty, "O");
                    currentLine = currentLine.Remove(currentPosNr, 1);
                    currentLine = currentLine.Insert(currentPosNr, ".");
                    currentLine = currentLine.Remove(currentPosNr - 1, 1);
                    currentLine = currentLine.Insert(currentPosNr - 1, "@");
                }
                warehouse.RemoveAt(currentLineNr);
                warehouse.Insert(currentLineNr, currentLine);
            }
            return warehouse;
        }

        private static List<string> goUp(List<string> warehouse, int startPos)
        {            
            var currentLineNr = startPos / 100;
            var currentPosNr = startPos % 100;
            var currentLine = warehouse[currentLineNr];
            var nextHash = 0;
            var nextDot = 0;
            for ( int i=currentLineNr;i>0;i--) {
                if (warehouse[i][currentPosNr] == '.' && nextDot == 0)
                {
                    nextDot = i;
                }
                if (warehouse[i][currentPosNr] == '#' && nextHash == 0)
                {
                    nextHash = i;
                }
            }
            //Går det flytta upp då måste det finnas någon . till uppåt innan nästa #
            if (nextDot > nextHash)
            {
                //Är nästa . flytta och returnera
                if (nextDot == currentLineNr-1)
                {
                    var line = warehouse[nextDot];
                    line = line.Remove(currentPosNr, 1);
                    line = line.Insert(currentPosNr, "@");
                    warehouse.RemoveAt(nextDot);
                    warehouse.Insert(nextDot, line);
                    line = warehouse[currentLineNr];
                    line = line.Remove(currentPosNr, 1);
                    line = line.Insert(currentPosNr, ".");
                    warehouse.RemoveAt(currentLineNr);
                    warehouse.Insert(currentLineNr, line);

                }
                else
                {
                    var line = warehouse[nextDot];
                    line = line.Remove(currentPosNr,1);
                    line = line.Insert(currentPosNr, "O");
                    warehouse.RemoveAt(nextDot);
                    warehouse.Insert(nextDot, line);
                    line = warehouse[currentLineNr];
                    line = line.Remove(currentPosNr,1);
                    line = line.Insert(currentPosNr, ".");
                    warehouse.RemoveAt(currentLineNr);
                    warehouse.Insert(currentLineNr, line);
                    line = warehouse[currentLineNr-1];
                    line = line.Remove(currentPosNr,1);
                    line = line.Insert(currentPosNr, "@");
                    warehouse.RemoveAt(currentLineNr-1);
                    warehouse.Insert(currentLineNr-1, line);
                }
            }
            return warehouse;
        }

        private static List<string> goDown(List<string> warehouse, int startPos)
        {            
            var currentLineNr = startPos / 100;
            var currentPosNr = startPos % 100;
            var currentLine = warehouse[currentLineNr];
            var nextHash = 0;
            var nextDot = 0;
            for (int i = currentLineNr; i < warehouse.Count; i++)
            {
                if (warehouse[i][currentPosNr] == '.' && nextDot == 0)
                {
                    nextDot = i;
                }
                if (warehouse[i][currentPosNr] == '#' && nextHash == 0)
                {
                    nextHash = i;
                }
            }
            //Går det flytta upp då måste det finnas någon . till nedåt innan nästa #
            if (nextDot < nextHash && nextDot != 0)
            {
                //Är nästa . flytta och returnera
                if (nextDot == currentLineNr + 1)
                {
                    var line = warehouse[nextDot];
                    line = line.Remove(currentPosNr,1);
                    line = line.Insert(currentPosNr, "@");
                    warehouse.RemoveAt(nextDot);
                    warehouse.Insert(nextDot, line);
                    line = warehouse[currentLineNr];
                    line = line.Remove(currentPosNr,1);
                    line = line.Insert(currentPosNr, ".");
                    warehouse.RemoveAt(currentLineNr);
                    warehouse.Insert(currentLineNr, line);

                }
                else
                {
                    var line = warehouse[nextDot];
                    line = line.Remove(currentPosNr,1);
                    line = line.Insert(currentPosNr, "O");
                    warehouse.RemoveAt(nextDot);
                    warehouse.Insert(nextDot, line);
                    line = warehouse[currentLineNr];
                    line = line.Remove(currentPosNr, 1);
                    line = line.Insert(currentPosNr, ".");
                    warehouse.RemoveAt(currentLineNr);
                    warehouse.Insert(currentLineNr, line);
                    line = warehouse[currentLineNr + 1];
                    line = line.Remove(currentPosNr, 1);
                    line = line.Insert(currentPosNr, "@");
                    warehouse.RemoveAt(currentLineNr + 1);
                    warehouse.Insert(currentLineNr + 1, line);
                }
            }
            return warehouse;
        }

        public static void solve2()
        {
            var total = 0;
            var lines = Util.readFile(fileName);
            var moves = "";
            var warehouse = new List<string>();
            var startPos = 0;
            var lineNr = 0;
            foreach (var line in lines)
            {
                if (line != "")
                {
                    if (line[0] == '#')
                    {
                        var newline = line.Replace(".", "..").Replace("@", "@.").Replace("#", "##").Replace("O", "[]");
                        warehouse.Add(newline);
                        if (newline.Contains("@"))
                        {
                            startPos = lineNr * 100 + newline.IndexOf("@");
                        }
                    }
                    else
                    {
                        moves += line;
                    }
                }
                
                lineNr++;
            }
            /**foreach (var line in warehouse)
            {
                Console.WriteLine(line);
            }**/
            Console.WriteLine("A: " + warehouse.Sum(a => a.Where(b => b == '[').Count()));
            Console.WriteLine("A: " + warehouse.Sum(a => a.Where(b => b == ']').Count()));
            Console.WriteLine("A: " + warehouse.Sum(a => a.Where(b => b == '#').Count()));
            warehouse = Move2(0, moves, warehouse, startPos);
            Console.WriteLine("A: " + warehouse.Sum(a => a.Where(b => b == '[').Count()));
            Console.WriteLine("A: " + warehouse.Sum(a => a.Where(b => b == ']').Count()));
            Console.WriteLine("A: " + warehouse.Sum(a => a.Where(b => b == '#').Count()));
            total = 0;
            for (int line = 0; line < warehouse.Count; line++)
            {
                for (int col = 0; col < warehouse[line].Length; col++)
                {
                    if (warehouse[line][col] == '[')
                    {
                        total += line * 100 + col;
                    }
                }
            }
            /**foreach (var line in warehouse)
            {
                    Console.WriteLine(line);
            }**/
            Console.WriteLine("Day" + day + "- 2: " + total);
        }

        private static List<string> Move2(int v, string moves, List<string> warehouse, int startPos)
        {
            if (v == moves.Length)
            {
                return warehouse;
            }

            while (v < moves.Length)
            {
                var currentMove = moves[v];
                startPos = getStartPos(warehouse);
                //Console.WriteLine(currentMove);
                var before = warehouse.Sum(a => a.Where(b => b == '#').Count());
                if ( startPos == 1631 && currentMove == '^')
                {
                    Console.WriteLine("A");
                }
                switch (currentMove)
                {
                    case '>':
                        warehouse = goRight2(warehouse, startPos);
                        break;
                    case '<':
                        warehouse = goLeft2(warehouse, startPos);
                        break;
                    case '^':
                        warehouse = goUp2(warehouse, startPos);
                        break;
                    case 'v':
                        warehouse = goDown2(warehouse, startPos);
                        break;

                }
                var after = warehouse.Sum(a => a.Where(b => b == '#').Count());
                if (before != after)
                {
                    Console.WriteLine("A");
                }
                v++;
                /**foreach (var line in warehouse)
                {
                    Console.WriteLine(line);
                }**/
            }


            
            //return Move(v+1, moves, warehouse, getStartPos(warehouse));
            return warehouse;
        }

        private static List<string> goRight2(List<string> warehouse, int startPos)
        {
            var currentLineNr = startPos / 100;
            var currentPosNr = startPos % 100;
            var currentLine = warehouse[currentLineNr];
            var nextHash = currentLine.IndexOf("#", currentPosNr);
            if (currentLine[currentPosNr + 1] == '#')
            {
                return warehouse;
            }
            //Går det flytta höger då måste det finnas någon . till höger innan näst #
            if (currentLine.Substring(currentPosNr, nextHash - currentPosNr).Contains("."))
            {
                //Är nästa . flytta och returnera
                if (currentLine[currentPosNr + 1] == '.')
                {
                    currentLine = currentLine.Remove(currentPosNr, 1);
                    currentLine = currentLine.Insert(currentPosNr, ".");
                    currentLine = currentLine.Remove(currentPosNr + 1, 1);
                    currentLine = currentLine.Insert(currentPosNr + 1, "@");
                }
                else
                {
                    //var nexEmpty = currentLine.Substring(currentPosNr, nextHash - currentPosNr).IndexOf(".");
                    var nexEmpty = currentLine.IndexOf(".", currentPosNr);
                    for (int pos = nexEmpty; pos >= currentPosNr+2; pos=pos-2)
                    {
                        currentLine = currentLine.Remove(pos-1, 1);
                        currentLine = currentLine.Insert(pos-1, "[");
                        currentLine = currentLine.Remove(pos, 1);
                        currentLine = currentLine.Insert(pos, "]");
                    }                    
                    currentLine = currentLine.Remove(currentPosNr, 1);
                    currentLine = currentLine.Insert(currentPosNr, ".");
                    currentLine = currentLine.Remove(currentPosNr + 1, 1);
                    currentLine = currentLine.Insert(currentPosNr + 1, "@");
                }
                warehouse.RemoveAt(currentLineNr);
                warehouse.Insert(currentLineNr, currentLine);
            }
            return warehouse;
        }        

        private static List<string> goLeft2(List<string> warehouse, int startPos)
        {
            
            var currentLineNr = startPos / 100;
            var currentPosNr = startPos % 100;
            var currentLine = warehouse[currentLineNr];
            if (currentLine[currentPosNr-1] == '#')
            {
                return warehouse;
            }
            var nextHash = currentLine.LastIndexOf("#", currentPosNr);
            //Går det flytta höger då måste det finnas någon . till höger innan näst #
            if (currentLine.Substring(nextHash, currentPosNr - nextHash).Contains("."))
            {
                //Är nästa . flytta och returnera
                if (currentLine[currentPosNr - 1] == '.')
                {
                    currentLine = currentLine.Remove(currentPosNr, 1);
                    currentLine = currentLine.Insert(currentPosNr, ".");
                    currentLine = currentLine.Remove(currentPosNr - 1, 1);
                    currentLine = currentLine.Insert(currentPosNr - 1, "@");
                }
                else
                {
                    //var nexEmpty = currentLine.Substring(nextHash, currentPosNr).LastIndexOf(".");
                    var nexEmpty = currentLine.LastIndexOf(".", currentPosNr);
                    for (int pos = nexEmpty; pos <= currentPosNr-2; pos = pos + 2)
                    {
                        currentLine = currentLine.Remove(pos, 1);
                        currentLine = currentLine.Insert(pos, "[");
                        currentLine = currentLine.Remove(pos+1, 1);
                        currentLine = currentLine.Insert(pos+1, "]");
                    }                    
                    currentLine = currentLine.Remove(currentPosNr, 1);
                    currentLine = currentLine.Insert(currentPosNr, ".");
                    currentLine = currentLine.Remove(currentPosNr - 1, 1);
                    currentLine = currentLine.Insert(currentPosNr - 1, "@");
                }
                warehouse.RemoveAt(currentLineNr);
                warehouse.Insert(currentLineNr, currentLine);
            }
            return warehouse;
        }

        private static List<string> goUp2(List<string> warehouse, int startPos)
        {
            var currentLineNr = startPos / 100;
            var currentPosNr = startPos % 100;
            var currentLine = warehouse[currentLineNr];

            if (warehouse[currentLineNr - 1][currentPosNr] == '#')
            {
                return warehouse;
            }

            //1. Om pos upp är . bara flytta
            if (warehouse[currentLineNr-1][currentPosNr] == '.') {
                var line = warehouse[currentLineNr-1];
                line = line.Remove(currentPosNr, 1);
                line = line.Insert(currentPosNr, "@");
                warehouse.RemoveAt(currentLineNr - 1);
                warehouse.Insert(currentLineNr - 1, line);
                line = warehouse[currentLineNr];
                line = line.Remove(currentPosNr, 1);
                line = line.Insert(currentPosNr, ".");
                warehouse.RemoveAt(currentLineNr);
                warehouse.Insert(currentLineNr, line);
            }
            else
            {
                var testLine = currentLineNr - 1;
                var colsToMove = new List<int>();
                var testColStart = warehouse[testLine][currentPosNr] == '[' ? currentPosNr : currentPosNr - 1;
                var testColEnd = testColStart+1;
                colsToMove.Add(testColStart);
                colsToMove.Add(testColEnd);
                var toMove = new Dictionary<int, List<int>>();
                var canMove = false;
                var hashFound = false;
                toMove.Add(testLine, colsToMove);
                while (testLine > 0 && !canMove && !hashFound) {
                    var nextTestLine = warehouse[testLine - 1];
                    canMove = true;
                    
                    foreach (var col in colsToMove) {
                        if (nextTestLine[col] == '#')
                        {
                            hashFound = true;
                            canMove = false;
                            break;
                        }
                        if (nextTestLine[col] != '.')
                        {                            
                            canMove = false;
                            
                        }                        
                    }
                    if ( !canMove && !hashFound)
                    {
                        var newCols = new List<int>();
                        foreach (var col in colsToMove)
                        {
                            if (nextTestLine[col] == '[')
                            {
                                newCols.Add(col);
                                newCols.Add(col+1);
                            }
                            else if (nextTestLine[col] == ']')
                            {
                                newCols.Add(col);
                                newCols.Add(col - 1);
                            }
                        }
                        colsToMove = newCols;
                        testLine--;
                        toMove.Add(testLine, colsToMove);
                    }
                }
                if (canMove)
                {
                    //Flytta enligt toMoveDict
                    foreach(var move in toMove.OrderBy(a => a.Key))
                    {
                        var newLine = warehouse[move.Key-1];
                        var oldLine = warehouse[move.Key];
                        foreach ( var col in move.Value.Distinct())
                        {
                            newLine = newLine.Remove(col, 1).Insert(col, oldLine[col].ToString());
                            oldLine = oldLine.Remove(col, 1).Insert(col, ".");
                        }
                        warehouse.RemoveAt(move.Key);
                        warehouse.Insert(move.Key, oldLine);
                        warehouse.RemoveAt(move.Key-1);
                        warehouse.Insert(move.Key-1, newLine);
                    }
                    var line = warehouse[currentLineNr - 1];
                    line = line.Remove(currentPosNr, 1);
                    line = line.Insert(currentPosNr, "@");
                    warehouse.RemoveAt(currentLineNr - 1);
                    warehouse.Insert(currentLineNr - 1, line);
                    line = warehouse[currentLineNr];
                    line = line.Remove(currentPosNr, 1);
                    line = line.Insert(currentPosNr, ".");
                    warehouse.RemoveAt(currentLineNr);
                    warehouse.Insert(currentLineNr, line);
                }
                

            }
            
            return warehouse;
        }

        private static List<string> goDown2(List<string> warehouse, int startPos)
        {
            var currentLineNr = startPos / 100;
            var currentPosNr = startPos % 100;
            var currentLine = warehouse[currentLineNr];
            if (warehouse[currentLineNr + 1][currentPosNr] == '#')
            {
                return warehouse;
            }

            //1. Om pos ner är . bara flytta
            if (warehouse[currentLineNr + 1][currentPosNr] == '.')
            {
                var line = warehouse[currentLineNr + 1];
                line = line.Remove(currentPosNr, 1);
                line = line.Insert(currentPosNr, "@");
                warehouse.RemoveAt(currentLineNr + 1);
                warehouse.Insert(currentLineNr + 1, line);
                line = warehouse[currentLineNr];
                line = line.Remove(currentPosNr, 1);
                line = line.Insert(currentPosNr, ".");
                warehouse.RemoveAt(currentLineNr);
                warehouse.Insert(currentLineNr, line);
            }
            else
            {
                var testLine = currentLineNr + 1;
                var colsToMove = new List<int>();
                var testColStart = warehouse[testLine][currentPosNr] == '[' ? currentPosNr : currentPosNr - 1;
                var testColEnd = testColStart + 1;
                colsToMove.Add(testColStart);
                colsToMove.Add(testColEnd);
                var toMove = new Dictionary<int, List<int>>();
                var canMove = false;
                var hashFound = false;
                toMove.Add(testLine, colsToMove);
                while (testLine > 0 && !canMove && !hashFound)
                {
                    var nextTestLine = warehouse[testLine + 1];
                    canMove = true;

                    foreach (var col in colsToMove)
                    {
                        if (nextTestLine[col] == '#')
                        {
                            hashFound = true;
                            canMove = false;
                            break;
                        }
                        if (nextTestLine[col] != '.')
                        {
                            canMove = false;                            
                        }
                    }
                    if (!canMove && !hashFound)
                    {
                        var newCols = new List<int>();
                        foreach (var col in colsToMove)
                        {
                            if (nextTestLine[col] == '[')
                            {
                                newCols.Add(col);
                                newCols.Add(col + 1);
                            }
                            else if (nextTestLine[col] == ']')
                            {
                                newCols.Add(col);
                                newCols.Add(col - 1);
                            }
                        }
                        colsToMove = newCols;
                        testLine++;
                        toMove.Add(testLine, colsToMove);
                    }
                }
                if (canMove)
                {
                    //Flytta enligt toMoveDict
                    foreach (var move in toMove.OrderByDescending(a => a.Key))
                    {
                        var newLine = warehouse[move.Key + 1];
                        var oldLine = warehouse[move.Key];
                        foreach (var col in move.Value.Distinct())
                        {
                            newLine = newLine.Remove(col, 1).Insert(col, oldLine[col].ToString());
                            oldLine = oldLine.Remove(col, 1).Insert(col, ".");
                        }
                        warehouse.RemoveAt(move.Key);
                        warehouse.Insert(move.Key, oldLine);
                        warehouse.RemoveAt(move.Key + 1);
                        warehouse.Insert(move.Key + 1, newLine);
                    }
                    var line = warehouse[currentLineNr + 1];
                    line = line.Remove(currentPosNr, 1);
                    line = line.Insert(currentPosNr, "@");
                    warehouse.RemoveAt(currentLineNr + 1);
                    warehouse.Insert(currentLineNr + 1, line);
                    line = warehouse[currentLineNr];
                    line = line.Remove(currentPosNr, 1);
                    line = line.Insert(currentPosNr, ".");
                    warehouse.RemoveAt(currentLineNr);
                    warehouse.Insert(currentLineNr, line);
                }


            }
            return warehouse;
        }


    }
}
