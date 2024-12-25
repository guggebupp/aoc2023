




namespace Aoc2023.Solution2024
{
    internal class Day08
    {
        private static string day = "08";
        private static string fileName = "day" + day + ".txt";

        public static void solve1()
        {
            var total = 0;
            var lines = Util.readFile(fileName);
            var handledType = new List<int>();
            for (int line = 0; line < lines.Count; line++) 
            { 
                for ( int col = 0; col < lines[line].Length; col++)
                {
                    var antenna = lines[line][col];
                    if (antenna != '.')
                    {
                        //Console.WriteLine("Searc: " + antenna + " - " + line + " - " + col);
                        //Find next same
                        for (int line2 = line; line2 < lines.Count; line2++)
                        {
                            for (int col2 = 0; col2 < lines[line].Length; col2++)
                            {
                                
                                if (lines[line2][col2] == antenna && (line != line2 || col != col2) && !(line == line2 && col2 < col))                                
                                {
                                    //Console.WriteLine("Test: " + antenna + " - " + line2 + " - " + col2);
                                    //if col2 >= col Leta ner fram och upp bak
                                    if ( col2 >= col)
                                    {
                                        //Ner fram
                                        var nextLine = line2 - line + line2;
                                        var nextCol = col2 - col + col2;
                                        if ( nextCol < lines[line].Length && nextLine < lines.Count && nextCol >= 0 && nextLine >= 0 )
                                        {
                                            if (!handledType.Contains(nextLine * 1000 + nextCol))
                                            {
                                                Console.WriteLine("Add: " + antenna + " - " + nextLine + " - " + nextCol);
                                                handledType.Add(nextLine * 1000 + nextCol);
                                                total++;
                                            }

                                        }
                                        //Upp bak
                                        
                                            nextLine = line -(line2 - line);
                                            nextCol = col - ( col2-col);
                                            if (nextCol < lines[line].Length && nextLine < lines.Count && nextCol >= 0 && nextLine >= 0)
                                            {
                                                if (!handledType.Contains(nextLine * 1000 + nextCol))
                                                {
                                                    Console.WriteLine("Add: " + antenna + " - " + nextLine + " - " + nextCol);
                                                    handledType.Add(nextLine * 1000 + nextCol);
                                                    total++;
                                                }
                                            }
                                        
                                    }

                                    //if col < col2 leta ner bak och upp fram
                                    else
                                    {
                                        //Ner bak
                                        var nextLine = line2 - line + line2;
                                        var nextCol = col2 - (col - col2);
                                        if (nextCol < lines[line].Length && nextLine < lines.Count && nextCol >= 0 && nextLine >= 0)
                                        {
                                            if (!handledType.Contains(nextLine * 1000 + nextCol))
                                            {
                                                Console.WriteLine("Add: " + antenna + " - " + nextLine + " - " + nextCol);
                                                handledType.Add(nextLine * 1000 + nextCol);
                                                total++;
                                            }
                                        }
                                        //Upp fram
                                        
                                            nextLine = line - (line2 - line);
                                            nextCol = col + (col - col2);
                                            if (nextCol < lines[line].Length && nextLine < lines.Count && nextCol >= 0 && nextLine >= 0)
                                            {
                                            if (!handledType.Contains(nextLine * 1000 + nextCol))
                                            {
                                                Console.WriteLine("Add: " + antenna + " - " + nextLine + " - " + nextCol);
                                                handledType.Add(nextLine * 1000 + nextCol);
                                                total++;
                                            }
                                        }
                                        
                                    }

                                    
                                    //Pair down
                                    
                                    /****/
                                    //Pair up
                                    if (!handledType.Contains(antenna))
                                    {
                                        
                                        /**if (line - line2 + line >= 0 && col2 - col + col2 < lines[line].Length)
                                        {
                                            Console.WriteLine("Add: " + antenna + " - " + (line - line2 + line) + " - " + (col2 - col + col2));
                                            total++;
                                        }**/
                                    }
                                    
                                }
                            }
                        }
                        //handledType.Add(antenna);
                    }
                }
            }

            /**foreach (var line in lines)
            {
                if (line != "")
                {
                    
                }
            }**/
            Console.WriteLine("Day08-1: " + total);
        }
        

        public static void solve2()
        {
            var total = 0;
            var lines = Util.readFile(fileName);
            var handledType = new List<int>();
            for (int line = 0; line < lines.Count; line++)
            {
                for (int col = 0; col < lines[line].Length; col++)
                {
                    var antenna = lines[line][col];
                    if (antenna != '.')
                    {
                        //Console.WriteLine("Searc: " + antenna + " - " + line + " - " + col);
                        //Find next same
                        for (int line2 = line; line2 < lines.Count; line2++)
                        {
                            for (int col2 = 0; col2 < lines[line].Length; col2++)
                            {

                                if (lines[line2][col2] == antenna && (line != line2 || col != col2) && !(line == line2 && col2 < col))
                                {
                                    if (!handledType.Contains(line * 1000 + col))
                                    {
                                        handledType.Add(line * 1000 + col);
                                        total++;
                                    }
                                    if (!handledType.Contains(line2 * 1000 + col2))
                                    {
                                        handledType.Add(line2 * 1000 + col2);
                                        total++;
                                    }
                                    //Console.WriteLine("Test: " + antenna + " - " + line2 + " - " + col2);
                                    //if col2 >= col Leta ner fram och upp bak
                                    if (col2 >= col)
                                    {
                                        //Ner fram
                                        var lineDiff = line2 - line;
                                        var colDif = col2 - col;
                                        var nextLine = line2 - line + line2;
                                        var nextCol = col2 - col + col2;
                                        for (int line3 = nextLine; line3 < lines.Count && line3 >= 0; line3 = line3 + lineDiff)
                                        {
                                            if (nextCol < lines[line3].Length && nextCol >= 0 && !handledType.Contains(line3 * 1000 + nextCol))
                                            {
                                                Console.WriteLine("Add: " + antenna + " - " + line3 + " - " + nextCol);
                                                handledType.Add(line3 * 1000 + nextCol);
                                                total++;
                                            }
                                            nextCol += colDif;
                                            
                                            
                                        }
                                        //Upp bak
                                        lineDiff = line - line2;
                                        colDif = col - col2;
                                        nextLine = line - (line2 - line);
                                        nextCol = col - (col2 - col);
                                        for (int line3 = nextLine; line3 < lines.Count && line3 >= 0; line3 = line3 + lineDiff)
                                        {
                                            if (nextCol < lines[line3].Length && nextCol >= 0 && !handledType.Contains(line3 * 1000 + nextCol))
                                            {
                                                Console.WriteLine("Add: " + antenna + " - " + line3 + " - " + nextCol);
                                                handledType.Add(line3 * 1000 + nextCol);
                                                total++;
                                            }
                                            nextCol += colDif;
                                        }

                                    }

                                    //if col2 < col leta ner bak och upp fram
                                    else
                                    {
                                        //Ner bak
                                        var lineDiff = line2 - line;
                                        var colDif = col2 - col;
                                        var nextLine = line2 - line + line2;
                                        var nextCol = col2 - (col - col2);
                                        for (int line3 = nextLine; line3 < lines.Count && line3 >= 0; line3 = line3 + lineDiff)
                                        {
                                            if (nextCol < lines[line3].Length && nextCol >= 0 && !handledType.Contains(line3 * 1000 + nextCol))
                                            {
                                                Console.WriteLine("Add: " + antenna + " - " + line3 + " - " + nextCol);
                                                handledType.Add(line3 * 1000 + nextCol);
                                                total++;
                                            }
                                            nextCol += colDif;
                                        }
                                        //Upp fram
                                        lineDiff = line - line2;
                                        colDif = col - col2;
                                        nextLine = line - (line2 - line);
                                        nextCol = col + (col - col2);
                                        for (int line3 = nextLine; line3 < lines.Count && line3 >= 0; line3 = line3 + lineDiff)
                                        {
                                            if (nextCol < lines[line3].Length && nextCol >= 0 && !handledType.Contains(line3 * 1000 + nextCol))
                                            {
                                                Console.WriteLine("Add: " + antenna + " - " + line3 + " - " + nextCol);
                                                handledType.Add(line3 * 1000 + nextCol);
                                                total++;
                                            }
                                            nextCol += colDif;
                                        }

                                    }                                    

                                }
                            }
                        }
                        //handledType.Add(antenna);
                    }
                }
            }
            Console.WriteLine("Day08-2: " + total);
        }
        
        
    }
}
