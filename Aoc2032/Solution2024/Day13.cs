using System;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks.Dataflow;

namespace Aoc2023.Solution2024
{
    internal class Day13
    {
        private static string day = "13";
        private static string fileName = "day" + day + ".txt";

        public static void solve()
        {
            solve1();
            solve2();
        }

        public static void solve1()
        {
            var total = new Double();
            total = 0;
            var lines = Util.readFile(fileName);
            var machines = getMachines(lines);            
            
            foreach (var machine in machines) {
                machine.bestMatch = -1;
                //A
                var nextX = machine.buttonA.xMove;
                var nextY = machine.buttonA.yMove;
                var prize = machine.buttonA.prize;
                goNext2(1, nextX, nextY, prize, machine);
                nextX = machine.buttonB.xMove;
                nextY = machine.buttonB.yMove;
                prize = machine.buttonB.prize;
                goNext2(1, nextX, nextY, prize, machine);
                if (machine.bestMatch > 0) {
                    total += machine.bestMatch;
                }
            }

            long total2 = 0;
            foreach (var machine in machines)
            {
                machine.bestMatch = -1;
                total2 += calculate(machine);
            }


            Console.WriteLine("Day"+ day +"- 1: " + total + " - " + total2);
        }

        public static void solve2()
        {
            long total = 0;
            var lines = Util.readFile(fileName);
            var machines = getMachines(lines, true);

            foreach (var machine in machines)
            {
                machine.bestMatch = -1;
                total += calculate(machine);
            }
            Console.WriteLine("Day" + day + "- 2: " + total + " -- " + total.ToString("0." + new string('#', 339)));
        }

        private static void goNext2(int index, Double X, Double Y, int p, Machine machine, bool onlyB = false)
        {
            if (X >= machine.xPrize || Y >= machine.yPrize)
            {
                if (X == machine.xPrize && Y == machine.yPrize)
                {
                    //Console.WriteLine("New match: " + p);
                    if ( machine.bestMatch == -1 || p < machine.bestMatch)
                    {
                        machine.bestMatch = p;
                    }
                }

            }
           if ( index++ > 200)
            {
                return;
            }

            var nextX = X+machine.buttonA.xMove;
            var nextY = Y+machine.buttonA.yMove;
            var prize = p+machine.buttonA.prize;
            if ( !onlyB)
            {
                goNext2(index, nextX, nextY, prize, machine);
            }
            
            nextX = X+machine.buttonB.xMove;
            nextY = Y+machine.buttonB.yMove;
            prize = p+machine.buttonB.prize;
            goNext2(index, nextX, nextY, prize, machine, true);
        }                        

        private static IEnumerable<Machine> getMachines(List<string> lines, bool part2 = false)
        {
            var machines = new List<Machine>();
            for (int i = 0; i < lines.Count; i = i + 4)
            {
                var machine = new Machine();
                var xy = lines[i].Split(':')[1].Split(',');
                var x = Int32.Parse(xy[0].Split("+")[1]);
                var y = Int32.Parse(xy[1].Split("+")[1].Trim());
                machine.buttonA = new Button(x, y, 3);
                xy = lines[i + 1].Split(':')[1].Split(',');
                x = Int32.Parse(xy[0].Split("+")[1]);
                y = Int32.Parse(xy[1].Split("+")[1].Trim());
                machine.buttonB = new Button(x, y, 1);
                xy = lines[i + 2].Split(':')[1].Split(',');
                x = Int32.Parse(xy[0].Split("=")[1]);
                y = Int32.Parse(xy[1].Split("=")[1].Trim());
                if ( part2)
                {
                    machine.xPrize = x + 10000000000000;
                    machine.yPrize = y + 10000000000000;
                } else
                {
                    machine.xPrize = x;
                    machine.yPrize = y;
                }


                machines.Add(machine);
            }
            return machines;
        }

        private static long calculate(Machine machine)
        {
            //X2=(PrizeY*AX-PrizeX*AY)/(BY*AX-BX*AY)
            var button2Count = ((machine.yPrize*machine.buttonA.xMove)-(machine.xPrize*machine.buttonA.yMove))/((machine.buttonB.yMove*machine.buttonA.xMove)-(machine.buttonB.xMove*machine.buttonA.yMove));
            //X1=(PrizeY-BY*X2)/AY;
            var button1Count = (machine.yPrize - (machine.buttonB.yMove * button2Count)) / machine.buttonA.yMove;
            if (Math.Floor(button1Count) == button1Count && Math.Floor(button2Count) == button2Count)
            {                
                return machine.buttonA.prize * (long)button1Count + machine.buttonB.prize*(long)button2Count;
            }
            return 0;
        }
    }

    internal class Machine
    {
        public Button buttonA { get; set; }
        public Button buttonB { get; set; }
        public Double xPrize { get; set; }
        public Double yPrize { get; set; }

        public Double bestMatch { get; set; }


    }

    internal class Button
    {
        public int xMove { get; set; }
        public int yMove { get; set; }
        public int prize { get; set; }
        public Button(int xMove, int yMove, int prize)
        {
            this.xMove = xMove;
            this.yMove = yMove;
            this.prize = prize;
        }
    }
}

    internal class Pathpos
    {
        public int currentX {  get; set; }
        public int currentY { get; set; }

        public int currentCost { get; set; }

        public Pathpos nextA { get; set; }
        public Pathpos nextB { get; set; }
    }