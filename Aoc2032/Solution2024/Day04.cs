





namespace Aoc2023.Solution2024
{
    internal class Day04
    {
        private static string day = "04";
        private static string fileName = "day" + day + ".txt";

        public static void solve1()
        {
            var total = 0;
            var lines = Util.readFile(fileName);
            var lineNumber = 0;
            foreach (var line in lines)
            {
                if (line != "")
                {
                    for (int i = 0; i < line.Length; i++)
                    {
                        if (line[i] == 'X')
                        {                            
                            var nextPosM = findNext(lines, lineNumber, i, 'M', -1); // Line * 1000 + pos --> -1 fail
                            foreach (var next in nextPosM)
                            {
                                var nextPosA = findNext(lines, (next % 1000000)/1000, next % 1000, 'A', next /1000000); // Line * 1000 + pos --> -1 fail
                                foreach (var nextA in nextPosA)
                                {
                                    var nextPosS = findNext(lines, (nextA % 1000000) / 1000, nextA % 1000, 'S', nextA /1000000); // Line * 1000 + pos --> -1 fail
                                    foreach (var nextS in nextPosS)
                                    {                         
                                        total++;
                                    }
                                }
                            }
                            
                            
                        }
                    }
                    lineNumber++;
                }
            }
            Console.WriteLine("Day04-1: " + total);
        }

        public static void solve2()
        {
            var total = 0;
            var lines = Util.readFile(fileName);
            var lineNumber = 0;
            foreach (var line in lines)
            {
                if (line != "")
                {
                    for (int i = 0; i < line.Length; i++)
                    {
                        if (line[i] == 'M')
                        {
                            var nextPos = i + 1;
                            var nextLine = lineNumber + 1;
                            if ( nextLine < line.Length && nextPos < lines[nextLine].Length && lines[nextLine][nextPos] == 'A')
                            {
                                nextPos = i + 2;
                                nextLine = lineNumber + 2;
                                if (nextLine < line.Length && nextPos < lines[nextLine].Length && lines[nextLine][nextPos] == 'S')
                                {

                                    nextPos = i;
                                    nextLine = lineNumber + 2;
                                    if (nextLine >= 0 && nextPos >= 0 &&nextLine < line.Length && nextPos < lines[nextLine].Length && lines[nextLine][nextPos] == 'M')
                                    {
                                        nextPos = i+2;
                                        nextLine = lineNumber;
                                        if (nextLine >= 0 && nextPos >= 0 && nextLine < line.Length && nextPos < lines[nextLine].Length && lines[nextLine][nextPos] == 'S')
                                        {
                                            total++;

                                        }

                                    }
                                    nextPos = i;
                                    nextLine = lineNumber + 2;
                                    if (nextLine >= 0 && nextPos >= 0 && nextLine < line.Length && nextPos < lines[nextLine].Length && lines[nextLine][nextPos] == 'S')
                                    {
                                        nextPos = i + 2;
                                        nextLine = lineNumber;
                                        if (nextLine >= 0 && nextPos >= 0 && nextLine < line.Length && nextPos < lines[nextLine].Length && lines[nextLine][nextPos] == 'M')
                                        {
                                            total++;

                                        }

                                    }
                                }

                            }
                        }
                        if (line[i] == 'S')
                        {
                            var nextPos = i + 1;
                            var nextLine = lineNumber + 1;
                            if (nextLine < line.Length && nextPos < lines[nextLine].Length && lines[nextLine][nextPos] == 'A')
                            {
                                nextPos = i + 2;
                                nextLine = lineNumber + 2;
                                if (nextLine < line.Length && nextPos < lines[nextLine].Length && lines[nextLine][nextPos] == 'M')
                                {

                                    nextPos = i;
                                    nextLine = lineNumber + 2;
                                    if (nextLine >= 0 && nextPos >= 0 && nextLine < line.Length && nextPos < lines[nextLine].Length && lines[nextLine][nextPos] == 'M')
                                    {
                                        nextPos = i + 2;
                                        nextLine = lineNumber;
                                        if (nextLine >= 0 && nextPos >= 0 && nextLine < line.Length && nextPos < lines[nextLine].Length && lines[nextLine][nextPos] == 'S')
                                        {
                                            total++;

                                        }

                                    }
                                    nextPos = i;
                                    nextLine = lineNumber + 2;
                                    if (nextLine >= 0 && nextPos >= 0 && nextLine < line.Length && nextPos < lines[nextLine].Length && lines[nextLine][nextPos] == 'S')
                                    {
                                        nextPos = i + 2;
                                        nextLine = lineNumber;
                                        if (nextLine >= 0 && nextPos >= 0 && nextLine < line.Length && nextPos < lines[nextLine].Length && lines[nextLine][nextPos] == 'M')
                                        {
                                            total++;

                                        }

                                    }
                                }

                            }
                        }
                    }
                    lineNumber++;
                }
            }
            Console.WriteLine("Day04-2: " + total);
        }

        private static List<int> findNext(List<string> lines, int lineNumber, int pos, char v, int dir)
        {

            var res = new List<int>();

            //Fram dir -> 0
            var testLine = lineNumber;
            var testPos = pos + 1;
            var dirT = 0;
            if ((dir == -1 || dir == dirT) && testLine >= 0 && testLine < lines.Count && testPos < lines[testLine].Length && testPos >= 0 && lines[testLine][testPos] == v)
            {
                res.Add( dirT*1000000 + testLine * 1000 + testPos);
            }

            //Bak dir -> 1
            testLine = lineNumber;
            testPos = pos - 1;
            dirT = 1;
            if ((dir == -1 || dir == dirT) && testLine >= 0 && testLine < lines.Count && testPos < lines[testLine].Length && testPos >= 0 && lines[testLine][testPos] == v)
            {
                res.Add(dirT * 1000000 + testLine * 1000 + testPos);
            }
            //Upp dir -> 2
            testLine = lineNumber-1;
            testPos = pos;
            dirT = 2;
            if ((dir == -1 || dir == dirT) && testLine >= 0 && testLine < lines.Count && testPos < lines[testLine].Length && testPos >= 0 && lines[testLine][testPos] == v)
            {
                res.Add(dirT * 1000000 + testLine * 1000 + testPos);
            }
            //Ner dir -> 3
            testLine = lineNumber+1;
            testPos = pos;
            dirT = 3;
            if ((dir == -1 || dir == dirT) && testLine >= 0 && testLine < lines.Count && testPos < lines[testLine].Length && testPos >= 0 && lines[testLine][testPos] == v)
            {
                res.Add(dirT * 1000000 + testLine * 1000 + testPos);
            }
            //UppF dir -> 4
            testLine = lineNumber-1;
            testPos = pos+1;
            dirT = 4;
            if ((dir == -1 || dir == dirT) && testLine >= 0 && testLine < lines.Count && testPos < lines[testLine].Length && testPos >= 0 && lines[testLine][testPos] == v)
            {
                res.Add(dirT * 1000000 + testLine * 1000 + testPos);
            }
            //UppB dir -> 5
            testLine = lineNumber - 1;
            testPos = pos - 1;
            dirT = 5;
            if ((dir == -1 || dir == dirT) && testLine >= 0 && testLine < lines.Count && testPos < lines[testLine].Length && testPos >= 0 && lines[testLine][testPos] == v)
            {
                res.Add(dirT * 1000000 + testLine * 1000 + testPos);
            }
            //NerF dir -> 6
            testLine = lineNumber + 1;
            testPos = pos + 1;
            dirT = 6;
            if ((dir == -1 || dir == dirT) && testLine >= 0 && testLine < lines.Count && testPos < lines[testLine].Length && testPos >= 0 && lines[testLine][testPos] == v)
            {
                res.Add(dirT * 1000000 + testLine * 1000 + testPos);
            }
            //NerB dir -> 7
            testLine = lineNumber + 1;
            testPos = pos - 1;
            dirT = 7;
            if ((dir == -1 || dir == dirT) && testLine >= 0 && testLine < lines.Count && testPos < lines[testLine].Length && testPos >= 0 && lines[testLine][testPos] == v)
            {
                res.Add(dirT * 1000000 + testLine * 1000 + testPos);
            }
            return res;
        }

        
        
        
    }
}
