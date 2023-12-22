using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023.Solution
{
    internal class Day16
    {
        private static string day = "16";
        private static string fileName = "day" + day + ".txt";
        public static void solve1()
        {
            long total = 0;
            var lines = Util.readFile(fileName);
            var lines2 = Util.readFile(fileName);
            var dir = Direction.Right;
            var posX = -1;
            var posY = 0;
            var handler = new Handler();
            handler.posX = posX;
            handler.posY = posY;
            handler.dir = dir;
            var handlers = Go(lines, lines2, handler);
            var handledHandlers = new List<Handler>();
            while (handlers.Count > 0)
            {
                var newHandlers = new List<Handler>();
                foreach (var hand in handlers)
                {
                    var h = Go(lines, lines2, hand);
                    newHandlers.AddRange(h);

                }
                handlers.Clear();
                foreach (var h in newHandlers)
                {
                    var alreadyHandled = false;
                    foreach (var h2 in handledHandlers)
                    {
                        if (h.posX == h2.posX && h.posY == h2.posY && h.dir == h2.dir)
                        {
                            alreadyHandled = true;
                        }
                    }
                    if (!alreadyHandled)
                    {
                        var x = new Handler();
                        x.posX = h.posX;
                        x.posY = h.posY;
                        x.dir = h.dir;
                        handlers.Add(x);
                    }
                }
                foreach (var h in newHandlers)
                {
                    var x = new Handler();
                    x.posX = h.posX;
                    x.posY = h.posY;
                    x.dir = h.dir;
                    handledHandlers.Add(x);
                }
            }
            foreach (var line in lines2)
            {
                Console.WriteLine(line);
                foreach (var c in line)
                {
                    if (c == '#')
                    {
                        total++;
                    }
                }
            }

            //total = lines.Sum(a => a.Sum(b => b == '#'));
            Console.WriteLine("Day" + day + "-1: " + total);
        }

        private static List<Handler> Go(List<string> lines, List<string> lines2, Handler handler)
        {
            List<Handler> newHandlers = new List<Handler>();
            if (handler.dir == Direction.Right)
            {
                handler.posX = handler.posX + 1;
            }
            if (handler.dir == Direction.Left)
            {
                handler.posX = handler.posX - 1;
            }
            if (handler.dir == Direction.Down)
            {
                handler.posY = handler.posY + 1;
            }
            if (handler.dir == Direction.Up)
            {
                handler.posY = handler.posY - 1;
            }
            if (handler.posX < 0 || handler.posY < 0 || handler.posX >= lines[0].Length || handler.posY >= lines.Count)
            {
                return newHandlers;
            }
            var next = lines[handler.posY][handler.posX];
            StringBuilder sb2 = new StringBuilder(lines2[handler.posY]);
            sb2[handler.posX] = '#';
            lines2[handler.posY] = sb2.ToString();
            if (next == '.' || next == '#')
            {
                //TODO: Ersätt med #
                StringBuilder sb = new StringBuilder(lines[handler.posY]);
                sb[handler.posX] = '#';
                lines[handler.posY] = sb.ToString();
                //return Go(lines, handler);                                
                newHandlers.Add(handler);
                return newHandlers;
            }
            if (next == '\\')
            {
                var newDir = Direction.Left;
                if (handler.dir == Direction.Right)
                {
                    newDir = Direction.Down;
                }
                if (handler.dir == Direction.Left)
                {
                    newDir = Direction.Up;
                }
                if (handler.dir == Direction.Up)
                {
                    newDir = Direction.Left;
                }
                if (handler.dir == Direction.Down)
                {
                    newDir = Direction.Right;
                }
                handler.dir = newDir;
                //return Go(lines, handler);                
                newHandlers.Add(handler);
                return newHandlers;
            }
            if (next == '/')
            {
                var newDir = Direction.Left;
                if (handler.dir == Direction.Right)
                {
                    newDir = Direction.Up;
                }
                if (handler.dir == Direction.Left)
                {
                    newDir = Direction.Down;
                }
                if (handler.dir == Direction.Up)
                {
                    newDir = Direction.Right;
                }
                if (handler.dir == Direction.Down)
                {
                    newDir = Direction.Left;
                }
                handler.dir = newDir;
                //return Go(lines, handler);
                newHandlers.Add(handler);
                return newHandlers;
            }
            if (next == '-')
            {
                if (handler.dir == Direction.Right || handler.dir == Direction.Left)
                {
                    //return Go(lines, handler);
                    newHandlers.Add(handler);
                    return newHandlers;
                }
                if (handler.dir == Direction.Up || handler.dir == Direction.Down)
                {
                    //handler.dir = Direction.Right;
                    //newHandlers.AddRange(Go(lines, handler));
                    //handler.dir = Direction.Left;
                    //newHandlers.AddRange(Go(lines, handler));
                    //return newHandlers;
                    var handler1 = new Handler();
                    handler1.posX = handler.posX;
                    handler1.posY = handler.posY;
                    handler1.dir = Direction.Right;
                    var handler2 = new Handler();
                    handler2.posX = handler.posX;
                    handler2.posY = handler.posY;
                    handler2.dir = Direction.Left;
                    newHandlers.Add(handler1);
                    newHandlers.Add(handler2);
                    return newHandlers;
                }
            }
            if (next == '|')
            {
                var newDir = Direction.Left;
                if (handler.dir == Direction.Up || handler.dir == Direction.Down)
                {
                    //return Go(lines, handler);
                    newHandlers.Add(handler);
                    return newHandlers;
                }
                if (handler.dir == Direction.Left || handler.dir == Direction.Right)
                {
                    //handler.dir = Direction.Up;
                    //newHandlers.AddRange(Go(lines, handler));
                    //handler.dir = Direction.Down;
                    //newHandlers.AddRange(Go(lines, handler));
                    //return newHandlers;                    
                    var handler1 = new Handler();
                    handler1.posX = handler.posX;
                    handler1.posY = handler.posY;
                    handler1.dir = Direction.Up;
                    var handler2 = new Handler();
                    handler2.posX = handler.posX;
                    handler2.posY = handler.posY;
                    handler2.dir = Direction.Down;
                    newHandlers.Add(handler1);
                    newHandlers.Add(handler2);
                    return newHandlers;
                }

            }
            return newHandlers;
        }

        public static void solve2()
        {
            long total = 0;
            var lines = Util.readFile(fileName);
            var lines2 = Util.readFile(fileName);
            var totals = new List<int>();

            for (int col = 0; col < lines[0].Length; col++)
            {
                lines = Util.readFile(fileName);
                lines2 = Util.readFile(fileName);
                var handler = new Handler();
                handler.posX = col;
                handler.posY = -1;
                handler.dir = Direction.Down;
                totals.Add(Check(lines, lines2, handler));
                lines = Util.readFile(fileName);
                lines2 = Util.readFile(fileName);
                handler.posY = lines.Count;
                handler.dir = Direction.Up;
                totals.Add(Check(lines, lines2, handler));
            }
            for (int row = 0; row < lines.Count; row++)
            {
                lines = Util.readFile(fileName);
                lines2 = Util.readFile(fileName);
                var handler = new Handler();
                handler.posX = -1;
                handler.posY = row;
                handler.dir = Direction.Right;
                totals.Add(Check(lines, lines2, handler));
                lines = Util.readFile(fileName);
                lines2 = Util.readFile(fileName);
                handler.posX = lines[0].Length;
                handler.dir = Direction.Left;
                totals.Add(Check(lines, lines2, handler));
            }

            Console.WriteLine("Day" + day + "-2: " + totals.Max());
        }
        
        private static int Check(List<string> lines, List<string> lines2, Handler handler)
        {
            var total = 0;
            var handlers = Go(lines, lines2, handler);
            var handledHandlers = new List<Handler>();
            while (handlers.Count > 0)
            {
                var newHandlers = new List<Handler>();
                foreach (var hand in handlers)
                {
                    var h = Go(lines, lines2, hand);
                    newHandlers.AddRange(h);

                }
                handlers.Clear();
                foreach (var h in newHandlers)
                {
                    var alreadyHandled = false;
                    foreach (var h2 in handledHandlers)
                    {
                        if (h.posX == h2.posX && h.posY == h2.posY && h.dir == h2.dir)
                        {
                            alreadyHandled = true;
                        }
                    }
                    if (!alreadyHandled)
                    {
                        var x = new Handler();
                        x.posX = h.posX;
                        x.posY = h.posY;
                        x.dir = h.dir;
                        handlers.Add(x);
                    }
                }
                foreach (var h in newHandlers)
                {
                    var x = new Handler();
                    x.posX = h.posX;
                    x.posY = h.posY;
                    x.dir = h.dir;
                    handledHandlers.Add(x);
                }
            }
            foreach (var line in lines2)
            {
                Console.WriteLine(line);
                foreach (var c in line)
                {
                    if (c == '#')
                    {
                        total++;
                    }
                }
            }
            return total;
        }
    }


}

internal enum Direction
{
    Left = 1,
    Right = 2,
    Up = 3,
    Down = 4
}

internal class Handler
{
    public int posX;
    public int posY;
    public Direction dir;
}
