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
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Axel\Desktop\learn_C#\advent_of_code\AoC_day03\input.txt");

            // First actual line { "#1 @ 916,616: 21x29" };
            // string[] testLines = { "#1 @ 916,616: 21x29" };
            // string[] testLines = { "#1 @ 1,1: 2x2" };

            List<int> usedSpace = new List<int>();              // To store all used numbers

            foreach (string line in lines)
            {
                string[] digits = Regex.Split(line, @"\D+");    // Grab each number in line
                                    
                // digits[0] is empty, don't know why.
                int id = int.Parse(digits[1]);
                int fLeft = int.Parse(digits[2]);
                int fTop = int.Parse(digits[3]);
                int width = int.Parse(digits[4]);
                int height = int.Parse(digits[5]);

                // Console.WriteLine(id + " " + fLeft + " " + fTop + " " + width + " " + height);


                
                // #1 @ 916,616: 21x29
                                                                 
                for (int i = 1; i < width + 1; i++)      // For every dot in width  
                {                                                                   
                    for (int j = 1; j < height + 1; j++) // For every dot in height. Count from the top left corner down to height                           
                    {
                        // Beacuse the distances we get from left to left and top to top is the dots inbetween, the box starts at the next dot
                        // Thats why I set i and j to 1.
                        int partA = fTop + j;   
                        int partB = fLeft + i;
                        string partAString = partA.ToString();
                        string partBString = partB.ToString();
                        string usedPointString = partBString + partAString; // left + top

                        // I convert the ints to strings just to put them togheter, if I were to add them togheter as ints I would get a number that might..
                        // - appear again out of coincidence but isn't actually at the same dot. But if I put them togheter as a string, ex, "917" + "617", I get the unique value 917617 that could represent a dot.
                        // Then I Parse that string back to an int and add it to usedSpace 

                        int usedPoint = int.Parse(usedPointString);
                        usedSpace.Add(usedPoint);
                    }
                }
            }



            int finalPart1 = 0;     // Count all the duplicates.

            var q = from x in usedSpace
                    group x by x into g
                    let count = g.Count()
                    orderby count descending
                    select new { Value = g.Key, Count = count };
            foreach (var x in q)
            {
                if (x.Count >= 2) // If there are duplicates or triplets or more of a number in usedSpace
                {
                    finalPart1++;
                }
            }


            // With the test line { "#1 @ 1,1: 2x2" };
            // I get these four dots 22, 23, 32, 33, which are correct.
            // So there might be something wrong with the counter above.
            // usedSpace.ForEach(i => Console.Write("{0}\n", i));

            Console.WriteLine(finalPart1);
            Console.WriteLine("Done");

            Console.ReadLine();
        } 
    }
}
