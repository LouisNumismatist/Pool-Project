using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGamePool1
{
    /// <summary>
    /// Helper class for the list sorting classes
    /// </summary>
    public class SortingTools
    {
        public static IEnumerable<int> GenerateRandomNums(int n)
        {
            Random random = new Random();
            List<int> integers = new List<int>(0);
            for (int x = 1; x <= n; x++)
            {
                integers.Add(x);
            }
            List<int> randomNums = new List<int>();
            for (int x = 0; x < n; x++)
            {
                int pos = random.Next(integers.Count);
                randomNums.Add(integers[pos]);
                integers.Remove(integers[pos]);
            }
            return randomNums;
        }

        public static void PrintList(IEnumerable<int> list)
        {
            string printedList = "";

            foreach (int x in list)
            {
                printedList += x + " ";
            }
            Console.WriteLine(printedList);
        }

        public static bool IsSorted(IEnumerable<int> arr)
        {
            var nums = arr.ToArray();
            bool stat = true;
            for (int x = 0; x < nums.Count() - 1; x++)
            {
                if (nums[x] > nums[x + 1])
                {
                    stat = false;
                    break;
                }
            }
            return stat;
        }

    }
}
