using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023.Solution
{
    internal class Day7
    {
        public static void solve2()
        {
            var total = 1;
            using (var sr = new StreamReader("../../../Input/day7.txt"))
            {
                var line = sr.ReadLine();
                while (line != null)
                {
                    line = sr.ReadLine();
                }
            }

            Console.WriteLine("Day7-2: " + total);
        }

        public static void solve1()
        {
            var total = 0;
            var plays = new List<Pair>();
            using (var sr = new StreamReader("../../../Input/day7.txt"))
            {
                var line = sr.ReadLine();
                while (line != null)
                {
                    var play = new Pair();
                    play.cards = line.Split(' ')[0];
                    play.bid = int.Parse(line.Split(' ')[1]);
                    plays.Add(play);
                    line = sr.ReadLine();
                }
            }
            var sorted = plays.OrderBy(x => x.cards, new Comparer2()).ToList();
            //Console.WriteLine("sorted");
            for (int i = 0; i < sorted.Count; i++)
            {
                total += sorted[i].bid * (1 + i);
              //  Console.WriteLine("sorted: " + sorted[i].cards + " -- " + i + " -- " + sorted[i].bid + " -- " + sorted[i].bid * (1 + i) + " --  " + total);
            }
            /**foreach (var pair in sorted)
            {
                Console.WriteLine(pair.cards);
            }**/

            Console.WriteLine("Day7-1: " + total);
        }


    }

    internal class Pair
    {
        public string cards;
        public int bid;
    }

    internal class Comparer2 : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            int xRank = getRank(x);
            int yRank = getRank(y);
            if (xRank > yRank)
            {
                return 1;
            }
            else if (xRank < yRank)
            {
                return -1;
            }
            for (int i = 0; i < 5; i++)
            {
                var values = new List<char> { 'A', 'K', 'Q', 'T', '9', '8', '7', '6', '5', '4', '3', '2', 'J' };
                foreach (char c in values)
                {
                    if (x[i] == y[i])
                    {
                        continue;
                    }
                    if (x[i] == c)
                    {
                        return 1;
                    }
                    if (y[i] == c)
                    {
                        return -1;
                    }
                }
            }

            Console.WriteLine("EEROR: " + x + " -- " + y);
            return 1;
        }

        private int getRank(string x)
        {
            int likes = findMaxLike(x);
            bool twoPair = findTwoPai(x);
            bool full = findFull(x);
            if (likes == 5)
            {
                return 6;
            }
            if (likes == 4)
            {
                return 5;
            }
            if (full)
            {
                return 4;
            }
            if (likes == 3)
            {
                return 3;
            }
            if (twoPair)
            {
                return 2;
            }
            if (likes == 2)
            {
                return 1;
            }
            return 0;
        }

        private bool findTwoPai(string x)
        {
            var numOfJ = x.Where(a => a == 'J').Count();
            var values = new List<char> { 'A', 'K', 'Q', 'T', '9', '8', '7', '6', '5', '4', '3', '2' };
            int maxLike = 0;
            char maxLikeChar = 'A';
            foreach (char c in values)
            {
                int like = 0;
                foreach (char card in x.ToCharArray())
                {
                    if (c == card)
                    {
                        like++;
                    }

                }
                if (like > maxLike)
                {
                    maxLike = like;
                    maxLikeChar = c;
                }
            }
            if (maxLike < 2)
            {
                if ((maxLike + numOfJ) >= 2)
                {
                    numOfJ = numOfJ - 2 + maxLike;
                    maxLike = 2;
                }
            }
            if (maxLike != 2)
            {
                return false;
            }
            var secondLike = 0;
            foreach (char c in values)
            {
                int like = 0;
                foreach (char card in x.ToCharArray())
                {
                    if (c == card && c != maxLikeChar)
                    {
                        like++;
                    }

                }
                if (like > secondLike)
                {
                    secondLike = like;
                }

            }
            if (secondLike < 2)
            {
                if ((secondLike + numOfJ) >= 2)
                {

                    secondLike = 2;
                }
            }
            return maxLike == 2 && secondLike == 2;
        }

        private bool findFull(string x)
        {
            var numOfJ = x.Where(a => a == 'J').Count();
            var values = new List<char> { 'A', 'K', 'Q', 'T', '9', '8', '7', '6', '5', '4', '3', '2' };
            int maxLike = 0;
            char maxLikeChar = 'A';
            foreach (char c in values)
            {
                int like = 0;
                foreach (char card in x.ToCharArray())
                {
                    if (c == card)
                    {
                        like++;
                    }

                }
                if (like > maxLike)
                {
                    maxLike = like;
                    maxLikeChar = c;
                }
            }
            if (maxLike < 3)
            {
                if ((maxLike + numOfJ) >= 3)
                {
                    numOfJ = numOfJ - 3 + maxLike;
                    maxLike = 3;
                }
            }
            if (maxLike != 3)
            {
                return false;
            }
            var secondLike = 0;
            foreach (char c in values)
            {
                int like = 0;
                foreach (char card in x.ToCharArray())
                {
                    if (c == card && c != maxLikeChar)
                    {
                        like++;
                    }

                }
                if (like > secondLike)
                {
                    secondLike = like;
                }
            }
            if (secondLike < 2)
            {
                if ((secondLike + numOfJ) >= 2)
                {

                    secondLike = 2;
                }
            }
            return maxLike == 3 && secondLike == 2;
        }

        private int findMaxLike(string x)
        {
            var values = new List<char> { 'A', 'K', 'Q', 'T', '9', '8', '7', '6', '5', '4', '3', '2' };
            int maxLike = 0;
            foreach (char c in values)
            {
                int like = 0;
                foreach (char card in x.ToCharArray())
                {
                    if (c == card)
                    {
                        like++;
                    }

                }
                if (like > maxLike)
                {
                    maxLike = like;
                }
            }
            return maxLike + x.Where(a => a == 'J').Count();

        }
    }
}

