using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023
{
    internal class Util
    {

        public static List<string> readFile(string fileName)
        {
            var lines = new List<string>();
            using (var sr = new StreamReader("../../../Input/" + fileName))
            {
                var line = sr.ReadLine();
                while (line != null)
                {
                    lines.Add(line);
                    line = sr.ReadLine();
                }

            }
            return lines;
        }
        internal class GridCube
        {
            public GridCube(int posY, int posX)
            {
                this.posY = posY;
                this.posX = posX;
            }
            public int posX;
            public int posY;
        }

        internal class GridCube2
        {
            public GridCube2(int posY, int posX, int length, Direction dir)
            {
                this.posY = posY;
                this.posX = posX;
                this.length = length;
                this.dir = dir;
            }
            public int posX;
            public int posY;
            public int length;
            public Direction dir;
        }
    }
}
