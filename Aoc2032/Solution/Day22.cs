using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023.Solution
{
    internal class Day22
    {
        private static string day = "22";
        private static string fileName = "day" + day + ".txt";
        public static void solve1()
        {
            long total = 0;
            var lines = Util.readFile(fileName);
            var bricks = new List<Brick>();
            var id = 0;
            foreach (var line in lines)
            {
                var brickInfo = line.Split("~");
                var start = brickInfo[0].ToString().Split(",");
                var end = brickInfo[1].ToString().Split(",");
                bricks.Add(new Brick(id++, start, end));
            }
            var brickToRemove = getBricksToRemove(bricks);
            Console.WriteLine("Day" + day + "-1: " + brickToRemove.Count);
        }
        public static List<Brick> getBricksToRemove(List<Brick> bricks)
        {
            var brickToRemove = new List<Brick>();


            bricks = bricks.OrderBy(a => a.startZ).ThenBy(a => a.startY).ThenBy(a => a.startX).ToList();
            for (int i = 0; i < bricks.Count; i++)
            {

                var brick = bricks[i];
                var match = bricks.Where(a => a.id != brick.id && brick.startZ > a.endZ && InSpanX(brick, a) && InSpanY(brick, a)).ToList();

                var diff = brick.endZ - brick.startZ;
                var newStartZ = match.Count() > 0 ? match.Max(a => a.endZ) + 1 : 1;
                brick.startZ = newStartZ;
                brick.endZ = newStartZ + diff;
            }
            bricks = bricks.OrderBy(a => a.startZ).ThenBy(a => a.startY).ThenBy(a => a.startX).ToList();

            var bricksSupportedby = new Dictionary<Brick, List<Brick>>();
            foreach (var brick in bricks)
            {
                var supportedBy =
                    bricks.Where(b => b.id != brick.id && (brick.startZ == b.endZ + 1))
                .Where(c => InSpanX(brick, c) && InSpanY(brick, c)).ToList();
                bricksSupportedby.Add(brick, supportedBy);
            }
            var bricksSupport = new Dictionary<Brick, List<Brick>>();
            foreach (var brick in bricks)
            {
                var supports =
                    bricks.Where(b => b.id != brick.id && (brick.endZ == b.startZ - 1))
                .Where(c => InSpanX(brick, c) && InSpanY(brick, c)).ToList();
                if (supports.Count == 0)
                {
                    bricksSupport.Add(brick, supports);
                }

            }
            foreach (var bricksSuppor in bricksSupportedby)
            {

                if (bricksSuppor.Value.Count > 1)
                {
                    foreach (var b in bricksSuppor.Value)
                    {

                        if (!brickToRemove.Exists(a => a.id == b.id))
                        {
                            brickToRemove.Add(b);
                        }
                    }
                }
            }
            foreach (var bricksSuppor in bricksSupportedby)
            {
                if (bricksSuppor.Value.Count == 1)
                {
                    foreach (var b in bricksSuppor.Value)
                    {

                        if (brickToRemove.Exists(a => a.id == b.id))
                        {
                            brickToRemove.Remove(b);
                        }
                    }
                }
            }
            bricksSupport.Where(a => a.Value.Count == 0).ToList().ForEach(b => brickToRemove.Add(b.Key));

            return brickToRemove;

        }

        private static bool InSpanY(Brick a, Brick c)
        {
            var inSpan = (a.startY <= c.endY && a.endY >= c.startY) ||
                (c.startY <= a.endY && c.endY >= a.startY);
            //Console.WriteLine("InY: " + a.id + " -- " + c.id + " - " + inSpan);
            return inSpan;
        }

        private static bool InSpanX(Brick a, Brick c)
        {
            var inSpan = (a.startX <= c.endX && a.endX >= c.startX) ||
                (c.startX <= a.endX && c.endX >= a.startX);
            //Console.WriteLine("InX: " + a.id + " -- " + c.id + " - " + inSpan);
            return inSpan;
        }

        public static void solve1_2()
        {
            long total = 0;
            var lines = Util.readFile(fileName);
            var bricks = new List<Brick>();
            var id = 0;
            foreach (var line in lines)
            {
                var brickInfo = line.Split("~");
                var start = brickInfo[0].ToString().Split(",");
                var end = brickInfo[1].ToString().Split(",");
                bricks.Add(new Brick(id++, start, end));
            }

            getBricksToRemove(bricks);

            var tot = bricks.Count;
            for (int i = 0; i < tot; i++)
            {
                bricks = bricks.OrderBy(a => a.id).ToList();
                int fallinBricks = getFallingBricks(bricks[i], bricks);
                if (fallinBricks == 0)
                {
                    total++;
                }


            }

            Console.WriteLine("Day" + day + "-1_2: " + total);
        }

        public static void solve2()
        {
            long total = 0;
            var lines = Util.readFile(fileName);
            var bricks = new List<Brick>();
            var id = 0;
            foreach (var line in lines)
            {
                var brickInfo = line.Split("~");
                var start = brickInfo[0].ToString().Split(",");
                var end = brickInfo[1].ToString().Split(",");
                bricks.Add(new Brick(id++, start, end));
            }

            getBricksToRemove(bricks);

            var tot = bricks.Count;
            for (int i = 0; i < tot; i++)
            {
                bricks = bricks.OrderBy(a => a.id).ToList();
                var fallinBricks = getFallingBricks(bricks[i], bricks);
                total += fallinBricks;
            }

            Console.WriteLine("Day" + day + "-2: " + total);
        }

        private static int getFallingBricks(Brick removebrick, List<Brick> inbricks)
        {
            List<Brick> bricks = new List<Brick>();
            inbricks.Where(a => removebrick != null && a.id != removebrick.id).ToList().ForEach(a => bricks.Add(new Brick(a)));            

            int noFall = 0;
            bricks = bricks.OrderBy(a => a.startZ).ThenBy(a => a.startY).ThenBy(a => a.startX).ToList();
            for (int i = 0; i < bricks.Count; i++)
            {

                var brick = bricks[i];
                var match = bricks.Where(a => a.id != brick.id && brick.startZ > a.endZ && InSpanX(brick, a) && InSpanY(brick, a)).ToList();

                var diff = brick.endZ - brick.startZ;
                var newStartZ = match.Count() > 0 ? match.Max(a => a.endZ) + 1 : 1;
                if (brick.startZ != newStartZ)
                {
                    noFall++;
                }
                brick.startZ = newStartZ;
                brick.endZ = newStartZ + diff;
                bricks[i] = brick;
            }
            return noFall;
        }
        
    }

    internal class Brick
    {
        public int id;
        public int startY;
        public int startX;
        public int startZ;
        public int endY;
        public int endX;
        public int endZ;

        public Brick(Brick a)
        {
            this.id = a.id;
            this.startX = a.startX;
            this.startY = a.startY;
            this.startZ = a.startZ;
            this.endX = a.endX;
            this.endY = a.endY;
            this.endZ = a.endZ;
        }

        public Brick(int id, string[] start, string[] end)
        {
            this.id = id;
            this.startX = int.Parse(start[0]);
            this.startY = int.Parse(start[1]);
            this.startZ = int.Parse(start[2]);
            this.endX = int.Parse(end[0]);
            this.endY = int.Parse(end[1]);
            this.endZ = int.Parse(end[2]);

        }
    }
}

