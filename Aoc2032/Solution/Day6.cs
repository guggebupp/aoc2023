using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023.Solution
{

    internal class Day6
    {
        public static void solve1()
        {
            var total = 1;
            using (var sr = new StreamReader("../../../Input/day6.txt"))
            {
                var line = sr.ReadLine();
                var raceTimes = line.Split(":")[1].Trim().Split(" ").ToList().Where(a => a != "").ToList();
                line = sr.ReadLine();
                var racedists = line.Split(":")[1].Trim().Split(" ").ToList().Where(a => a != "").ToList();
                var raceIndex = 0;
                foreach (var race in raceTimes)
                {
                    var raceTime = int.Parse(race);
                    var raceDist = int.Parse(racedists[raceIndex]);
                    int raceWin = 0;
                    for (int i = 1; i <= raceTime; i++)
                    {
                        var dist = (raceTime - i) * i;
                        if (dist > raceDist)
                        {
                            raceWin++;
                        }
                    }
                    //Console.WriteLine("race: " + raceWin + " -- " + raceTime + " -- " + raceDist);
                    total = raceWin * total;
                    raceIndex++;
                }
            }

            Console.WriteLine("Day6-1: " + total);
        }

        public static void solve2Simple()
        {
            BigInteger racedist = 601116315591300;
            BigInteger racetime = 60808676;
            BigInteger raceWin = 0;
            for (BigInteger i = 1; i <= racetime; i++)
            {
                if ((racetime - i) * i > racedist)
                {
                    raceWin++;
                }
            }
            Console.WriteLine("Day6-1: " + raceWin);
        }
        public static void solve2()
        {
            var total = BigInteger.One;
            using (var sr = new StreamReader("../../../Input/day6.txt"))
            {
                var line = sr.ReadLine();
                var raceTime = BigInteger.Parse(line.Split(":")[1].Trim().Replace(" ", ""));
                line = sr.ReadLine();
                var raceDist = BigInteger.Parse(line.Split(":")[1].Trim().Replace(" ", ""));

                BigInteger raceWin = 0;
                for (BigInteger i = 1; i <= raceTime; i++)
                {
                    var dist = (raceTime - i) * i;
                    if (dist > raceDist)
                    {
                        raceWin++;
                    }
                }
                //Console.WriteLine("race: " + raceWin + " -- " + raceTime + " -- " + raceDist);
                total = raceWin * total;


            }

            Console.WriteLine("Day6-2: " + total);
        }
    }
}
