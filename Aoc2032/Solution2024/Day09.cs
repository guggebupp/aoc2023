




namespace Aoc2023.Solution2024
{
    internal class Day09
    {
        private static string day = "09";
        private static string fileName = "day" + day + ".txt";

        public static void solve1()
        {
            var total = new Double();
            total = 0;
            var lines = Util.readFile(fileName);

            //Create filesystem
            var fileSystem = "";
            var fileSystem2 = new List<int>();
            var id = 0;
            foreach (var line in lines)
            {
                if (line != "")
                {
                    for (int i = 0; i < line.Length; i++) {                        
                        var length = Int32.Parse(line[i] + "");
                        if ( i%2 == 0)
                        {
                            for ( int j = 0; j<length;j++)
                            {
                                fileSystem += id;
                                fileSystem2.Add(id);                                
                            }                            
                            id++;
                        } else
                        {
                            for (int j = 0; j < length; j++)
                            {
                                fileSystem += ".";
                                fileSystem2.Add(-1);
                            }
                        }
                    }

                }
            }
            //Kompress filesystem
            for (int i = 0; i < fileSystem2.Count; i++)
            {
                if (fileSystem2[i] == -1)
                {
                    for (int j = fileSystem2.Count - 1; j >= i+1; j--) { 
                        if (fileSystem2[j] != -1)
                        {
                            var newId = fileSystem2[j];
                            fileSystem2.RemoveAt(i);
                            fileSystem2.Insert(i, newId);
                            fileSystem2.RemoveAt(j);
                            fileSystem2.Insert(j, -1);
                            //fileSystem2 = fileSystem2.Remove(i, 1).Insert(i, fileSystem2[j] + "");
                            //fileSystem2 = fileSystem2.Remove(j, 1).Insert(j, ".");
                            j = 0;
                        }
                    }
                }
            }
            //Checksum
            var pos = 0;
            for (int i = 0; i < fileSystem2.Count; i++)
            {
                if (fileSystem2[i] != -1)
                {
                    //var idB = Int32.Parse(fileSystem[i] + "");
                    total += fileSystem2[i] * pos;                    
                }
                pos++;
            }
            //Console.WriteLine("Day09-1: " + fileSystem);
            Console.WriteLine("Day09-1: " + total);
        }
        

        public static void solve2()
        {
            var total = new Double();
            total = 0;
            var lines = Util.readFile(fileName);

            //Create filesystem
            var fileSystem = "";
            var fileSystem2 = new List<int>();
            var id = 0;
            foreach (var line in lines)
            {
                if (line != "")
                {
                    for (int i = 0; i < line.Length; i++)
                    {
                        var length = Int32.Parse(line[i] + "");
                        if (i % 2 == 0)
                        {
                            for (int j = 0; j < length; j++)
                            {
                                fileSystem += id;
                                fileSystem2.Add(id);
                            }                            
                            id++;
                        }
                        else
                        {
                            for (int j = 0; j < length; j++)
                            {
                                fileSystem += ".";
                                fileSystem2.Add(-1);
                            }
                        }
                    }

                }
            }
            //Kompress
            var handledFiles = new List<int>();
            for (int j = fileSystem2.Count - 1; j > 0; j--)
            {
                var currentId = fileSystem2[j];
                if (currentId != -1 && !handledFiles.Contains(currentId))
                {
                    
                    var idLengt = 1;
                    var y = j - 1;
                    while (y > 0 && fileSystem2[y--] == currentId)
                    {
                        idLengt++;
                    }
                    //Find mathcing slot
                    var slotLength = 0;
                    var slotStart = 0;
                    handledFiles.Add(currentId);                    
                    var longestSlog = 0;
                    for (int i = 0; i < (j-idLengt); i++)
                    {
                        slotLength = 0;                        
                        if (fileSystem2[i] == -1)
                        {
                            slotStart = i;                            
                            while (i < fileSystem2.Count && fileSystem2[i] == -1)
                            {
                                slotLength++;
                                i++;
                            }
                            if ( slotLength > longestSlog)
                            {
                                longestSlog = slotLength;
                            }
                            if (idLengt <= slotLength)
                            {                                
                                for (int len = 0; len < idLengt; len++)
                                {

                                    fileSystem2.RemoveAt(slotStart + len);
                                    fileSystem2.Insert(slotStart + len, currentId);
                                    fileSystem2.RemoveAt(j - len);
                                    fileSystem2.Insert(j - len, -1);

                                }
                                i = fileSystem2.Count;
                            }
                        }
                        
                    }                    
                    
                    j -= idLengt - 1;
                }
            }
            
            //Checksum
            var pos = 0;            
            for (int i = 0; i < fileSystem2.Count; i++)
            {
                
                if (fileSystem2[i] != -1)
                {                                     
                    total += fileSystem2[i] * pos;                    
                }
                pos++;

            }
                        
            Console.WriteLine("Day09-2: " + total);
        }
        
        
    }
}
