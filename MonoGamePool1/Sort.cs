using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGamePool1
{
    class Sort
    {
        static IEnumerable<int> Merge(IEnumerable<int> nums)
        {
            Tuple<IEnumerable<int>, IEnumerable<int>> tempTuple = SplitArray(nums);
            IEnumerable<int> iA = tempTuple.Item1;
            IEnumerable<int> iB = tempTuple.Item2;
            var a = iA.ToArray();
            var b = iB.ToArray();

            if (a.Length <= 2)
            {
                
            }
            else
            {
                iA = Merge(a);
                iB = Merge(b);
            }

            return nums;
        }

        static IEnumerable<int> Bubble(IEnumerable<int> arr)
        {
            var nums = arr.ToArray();
            int len = nums.Length;
            for(int x = 0; x < len; x++)
            {
                for (int y = 0; y < len-x; y++)
                {
                    if (nums[x] < nums[x + 1])
                    {
                        int temp = nums[x];
                        nums[x] = nums[x + 1];
                        nums[x + 1] = nums[temp];
                    }
                }
            }
            return nums;
        }

        static Tuple<IEnumerable<int>, IEnumerable<int>> SplitArray(IEnumerable<int> arr)
        {
            var nums = arr.ToArray();
            int length = nums.Count<int>();
            List<int> halfA = new List<int>(0);
            List<int> halfB = new List<int>(0);
            for(int x = 0; x < length; x++)
            {
                if (x < length / 2)
                {
                    halfA.Add(nums[x]);
                }
                else
                {
                    halfB.Add(nums[x]);
                }
            }
            return new Tuple<IEnumerable<int>, IEnumerable<int>>(halfA, halfB);
        }

        static int Len(Array array)
        {
            int length = 0;
            foreach(int x in array)
            {
                length++;
            }
            return length;
        }

        static IEnumerable<int> GenerateRandomNums(int n)
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
            }
            return randomNums;
        }
    }
}
