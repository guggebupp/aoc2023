




namespace Aoc2023.Solution2024
{
    internal class Day14
    {
        private static string day = "14";
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
            var robotPositions = new List<int>();
            var quad1 = 0;
            var quad2 = 0;
            var quad3 = 0;
            var quad4 = 0;
            var seconds = 100;
            var xSize = 101;
            var ySize = 103;
            foreach (var line in lines)
            {
                if (line != "")
                {
                    var currentX = Int32.Parse(line.Split("p=")[1].Split(",")[0]);
                    var currentY = Int32.Parse(line.Split("p=")[1].Split(",")[1].Split("v=")[0].Trim());
                    var speedX = Int32.Parse(line.Split("v=")[1].Split(",")[0]);
                    var speedY = Int32.Parse(line.Split("v=")[1].Split(",")[1]);
                    var newPosX = (currentX + (speedX * seconds)) % xSize;
                    var newPosY = (currentY + (speedY * seconds)) % ySize;
                    if ( newPosX < 0)
                    {
                        newPosX = xSize + newPosX;
                    }
                    if( newPosY < 0) { newPosY = ySize + newPosY; }
                    Console.WriteLine("New post: " + newPosX + "," + newPosY);
                    robotPositions.Add(newPosX * 1000 + newPosY);
                    if (newPosX == xSize / 2 || newPosY == ySize / 2) { 
                    }
                    //Vänstra halvan
                    else if ( newPosX < xSize/2) {
                        //ÖVre halvan
                        if (newPosY < ySize / 2)
                        {
                            quad1++;
                        }
                        else
                        {
                            quad2++;
                        }
                    } else {
                        //ÖVre halvan
                        if (newPosY < ySize / 2)
                        {
                            quad3++;
                        }
                        else
                        {
                            quad4++;
                        }
                    }
                }
            }
            total = quad1*quad2*quad3*quad4;
            
            Console.WriteLine("Day" + day + "- 1: " + total + " - 1:" + quad1 + " - 2:" + quad2 + " - 3:" + quad3 + " - 4:" + quad4);
        }
        

        public static void solve2()
        {
            var total = 0;
            var lines = Util.readFile(fileName);
            var robots = new List<Robot>();
            var xSize = 101;
            var ySize = 103;
            foreach (var line in lines)
            {
                if (line != "")
                {
                    var robot = new Robot();
                    robot.xPos = Int32.Parse(line.Split("p=")[1].Split(",")[0]);
                    robot.yPos = Int32.Parse(line.Split("p=")[1].Split(",")[1].Split("v=")[0].Trim());
                    robot.xVel = Int32.Parse(line.Split("v=")[1].Split(",")[0]);
                    robot.yVel = Int32.Parse(line.Split("v=")[1].Split(",")[1]);
                    //var newPosX = (currentX + (speedX * seconds)) % xSize;
                    //var newPosY = (currentY + (speedY * seconds)) % ySize;
                    robots.Add(robot);
                }
            }
            var index = 0;
            while(!IsChristmasTree(robots, index++))
            {
                Console.Write(".");
                foreach (var robot in robots)
                {
                    var newX = (robot.xPos + robot.xVel)%xSize;                    
                    robot.xPos = newX < 0 ? xSize + newX: newX ;                    
                    var newY = (robot.yPos + robot.yVel) % ySize;
                    robot.yPos = newY < 0 ? ySize + newY : newY;
                }                
            }
            Console.WriteLine("Day" + day + "- 2: " + total);
        }

        private static bool IsChristmasTree(List<Robot> robots, int index)
        {
            var result = false;
            var currentX = 0;
            var currentY = 0;
            /**var lines = new List<string>();
            for (int line = 0; line < 103;line++)
            {
                var newLine = "";
                for (int col = 0; col < 101; col++)
                {
                    newLine += ".";
                }
                lines.Add(newLine);
            }
            foreach (var robot in robots.OrderBy(a => a.yPos).OrderBy(a => a.xPos)) 
            {
                var line = lines[robot.yPos];
                line = line.Remove(robot.xPos, 1);
                line = line.Insert(robot.xPos, "1");
                lines.RemoveAt(robot.yPos);
                lines.Insert(robot.yPos, line);
            }
            foreach (var line in lines) {
                Console.WriteLine(line);
            }**/
            foreach (var robot in robots.OrderBy(a => a.yPos).OrderBy(a => a.xPos))
            {
                if (robots.Any(a => a.yPos == (robot.yPos - 1) && a.xPos == (robot.xPos - 1)))
                {
                    if (robots.Any(a => a.yPos == (robot.yPos + 1) && a.xPos == (robot.xPos - 1)))
                    {
                        if (robots.Any(a => a.yPos == (robot.yPos - 1) && a.xPos == (robot.xPos + 1)))
                        {
                            if (robots.Any(a => a.yPos == (robot.yPos + 1) && a.xPos == (robot.xPos + 1)))
                            {
                                if (robots.Any(a => a.yPos == (robot.yPos + 2) && a.xPos == (robot.xPos + 2)))
                                {
                                    if (robots.Any(a => a.yPos == (robot.yPos -2) && a.xPos == (robot.xPos -2)))
                                    {
                                        if (robots.Any(a => a.yPos == (robot.yPos + 1) && a.xPos == (robot.xPos + 1)))
                                        {
                                            Console.WriteLine(index);
                                            var lines = new List<string>();
                                            for (int line = 0; line < 103; line++)
                                            {
                                                var newLine = "";
                                                for (int col = 0; col < 101; col++)
                                                {
                                                    newLine += ".";
                                                }
                                                lines.Add(newLine);
                                            }
                                            foreach (var robot2 in robots.OrderBy(a => a.yPos).OrderBy(a => a.xPos))
                                            {
                                                var line = lines[robot2.yPos];
                                                line = line.Remove(robot2.xPos, 1);
                                                line = line.Insert(robot2.xPos, "1");
                                                lines.RemoveAt(robot2.yPos);
                                                lines.Insert(robot2.yPos, line);
                                            }
                                            foreach (var line in lines)
                                            {
                                                Console.WriteLine(line);
                                            }
                                            Console.WriteLine("H");
                                        }
                                    }
                                }
                            }
                            
                        }
                    }
                }
            }
            return result;
        }
    }
}
