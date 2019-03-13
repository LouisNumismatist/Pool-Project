using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGamePool1
{
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

        public static List<int> Digits(int n, int max)
        {
            List<int> digits = new List<int>();
            int temp = 0;
            for (int pow = max; pow >= 0; pow--)
            {
                int item = (int)(n / Math.Pow(10, pow)) - (temp * 10);
                digits.Add(item);
                temp = item;
            }
            return digits;
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
