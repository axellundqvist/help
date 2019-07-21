using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC_day03
{
    class Program
    {
        private class Point
        {
            public Point(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            // override object.Equals
            public override bool Equals(Object obj)
            {
                //       
                // See the full list of guidelines at
                //   http://go.microsoft.com/fwlink/?LinkID=85237  
                // and also the guidance for operator== at
                //   http://go.microsoft.com/fwlink/?LinkId=85238
                //

                if (obj == null || GetType() != obj.GetType())
                {
                    return false;
                }

                return this.X == ((Point)obj).X && this.Y == ((Point)obj).Y ? true : false;
            }

            // override object.GetHashCode
            // https://stackoverflow.com/questions/5221396/what-is-an-appropriate-gethashcode-algorithm-for-a-2d-point-struct-avoiding
            public override int GetHashCode()
            {
                unchecked
                {
                    int hash = 17;
                    hash *= 23 + X.GetHashCode();
                    hash *= 23 + Y.GetHashCode();
                    return hash;
                }
            }

            public int X { get; set; }
            public int Y { get; set; }
        }

        static void Main(string[] args)
        {

            string[] lines = System.IO.File.ReadAllLines(@"..\..\..\input_olof.txt");

            // First actual line { "#1 @ 916,616: 21x29" };
            // string[] testLines = { "#1 @ 916,616: 21x29" };
            // string[] testLines = { "#1 @ 1,1: 2x2" };

            List<int> usedSpace = new List<int>();              // To store all used numbers

            Dictionary<Point, int> counts = new Dictionary<Point, int>();

            foreach (string line in lines)
            {
                string[] digits = Regex.Split(line, @"\D+");    // Grab each number in line

                // digits[0] is empty, don't know why.
                int id = int.Parse(digits[1]);
                int fLeft = int.Parse(digits[2]);
                int fTop = int.Parse(digits[3]);
                int width = int.Parse(digits[4]);
                int height = int.Parse(digits[5]);

                for (int i = fLeft; i < fLeft + width; ++i)
                {
                    for (int j = fTop; j < fTop + height; ++j)
                    {
                        Point point = new Point(i, j);
                        if (counts.ContainsKey(point))
                        {
                            counts[point] += 1;
                        }
                        else
                        {
                            counts[point] = 1;
                        }
                    }
                }
            }

            int numOverlappingPatches = 0;

            foreach(var item in counts)
            {
                if (item.Value > 1)
                {
                    ++numOverlappingPatches;
                }
            }

            Console.WriteLine(numOverlappingPatches);
            Console.ReadLine();

        }
    }
}
