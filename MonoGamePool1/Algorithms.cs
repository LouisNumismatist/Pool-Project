using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting_Algorithms
{
    /// <summary>
    /// A class for algorithms
    /// </summary>
    public class Algorithms
    {
        public static int[] Insertion(IEnumerable<int> arr) //minor
        {
            return InsertionMain(arr, 1, arr.Count());
        }

        public static int[] Merge(IEnumerable<int> arr) //minor
        {
            return MergeMain(arr, 0, arr.Count() - 1);
        }

        public static int[] Quick(IEnumerable<int> arr) //minor
        {
            return QuickMain(arr, 0, arr.Count() - 1);
        }

        public static int[] MergeMain(IEnumerable<int> arr, int left, int right) //DONE
        {
            var nums = arr.ToArray();

            int mid = (int)((right - left) / 2 + left);
            if (right - left > 0)
            {
                nums = MergeMain(nums, left, mid);
                nums = MergeMain(nums, mid + 1, right);
                int a = left;
                int b = mid + 1;
                List<int> temp = new List<int>();
                while (a <= mid && b <= right)
                {
                    if (nums[a] < nums[b])
                    {
                        temp.Add(nums[a]);
                        a += 1;
                    }
                    else
                    {
                        temp.Add(nums[b]);
                        b += 1;
                    }
                }
                while (a <= mid)
                {
                    temp.Add(nums[a]);
                    a += 1;
                }
                while (b <= right)
                {
                    temp.Add(nums[b]);
                    b += 1;
                }
                for (int x = 0; x < temp.Count(); x++)
                {
                    nums[left + x] = temp[x];
                }
            }

            return nums;
        }

        public static int[] InsertionMain(IEnumerable<int> arr, int left, int right) //DONE
        {
            var nums = arr.ToArray();

            for (int x = left; x < right; x++)
            {
                for (int y = x; y >= left; y--)
                {
                    if (nums[y] < nums[y - 1])
                    {
                        int temp = nums[y - 1];
                        nums[y - 1] = nums[y];
                        nums[y] = temp;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return nums;
        }

        public static int[] QuickMain(IEnumerable<int> arr, int OuterLeft, int OuterRight) //DONE
        {
            var nums = arr.ToArray();
            int left = OuterLeft;
            int right = OuterRight;
            int pivot = left;

            while (left < right)
            {
                while (pivot == left)
                {
                    if (nums[pivot] < nums[right])
                    {
                        right -= 1;
                    }
                    else if (nums[pivot] > nums[right])
                    {
                        int temp = nums[pivot];
                        nums[pivot] = nums[right];
                        nums[right] = temp;
                    }
                    else
                    {
                        break;
                    }
                }
                while (pivot == right)
                {
                    if (nums[pivot] > nums[left])
                    {
                        left += 1;
                    }
                    else if (nums[pivot] < nums[left])
                    {
                        int temp = nums[pivot];
                        nums[pivot] = nums[left];
                        nums[left] = temp;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if (right > OuterLeft)
            {
                nums = QuickMain(nums, OuterLeft, right - 1);
            }
            if (right + 1 < OuterRight)
            {
                nums = QuickMain(nums, right + 1, OuterRight);
            }

            return nums;
        }

        public static int[] Bubble(IEnumerable<int> arr) //DONE
        {
            var nums = arr.ToArray();

            for (int x = 0; x < nums.Count(); x++)
            {
                for (int y = 0; y < nums.Count() - (x + 1); y++)
                {
                    if (nums[y] > nums[y + 1])
                    {
                        int temp = nums[y];
                        nums[y] = nums[y + 1];
                        nums[y + 1] = temp;
                    }
                }
            }
            return nums;
        }

        public static int[] CocktailShaker(IEnumerable<int> arr) //DONE
        {
            var nums = arr.ToArray();

            int left = 0;
            int right = nums.Count() - 1;

            while (left < right)
            {
                for (int x = left; x < right; x++)
                {
                    if (nums[x] > nums[x + 1])
                    {
                        int temp = nums[x];
                        nums[x] = nums[x + 1];
                        nums[x + 1] = temp;
                    }
                }
                left += 1;
                if (left == right)
                {
                    break;
                }
                for (int x = right; x >= left; x--)
                {
                    if (nums[x] < nums[x - 1])
                    {
                        int temp = nums[x];
                        nums[x] = nums[x - 1];
                        nums[x - 1] = temp;
                    }
                }
                right -= 1;
            }
            return nums;
        }

        public static int[] Selection(IEnumerable<int> arr) //DONE
        {
            var nums = arr.ToArray();
            int pos;
            for (int x = 0; x < nums.Count(); x++)
            {
                pos = x;
                for (int y = x; y < nums.Count(); y++)
                {
                    if (nums[y] < nums[pos])
                    {
                        pos = y;
                    }
                }
                if (pos != x)
                {
                    int temp = nums[pos];
                    nums[pos] = nums[x];
                    nums[x] = temp;
                }
            }
            return nums;
        }

        public static int[] RadixLSD(IEnumerable<int> arr) //LSD: Least Significant Digit (start with far right digit)
        {
            var nums = arr.ToArray();
            int DigitLength = 0;
            //char[] tempChar = "0".ToCharArray();
            //char zero = tempChar[0];
            foreach (int n in nums)
            {
                int len = (int)Math.Log(n, 10);
                if (len > DigitLength)
                {
                    DigitLength = len;
                }
            }
            Console.WriteLine(DigitLength);
            for (int x = DigitLength; x >= 0; x++) //Digits
            {
                List<List<int>> Sorted = new List<List<int>>();
                for (int a = 0; a < 10; a++) //Make holders
                {
                    Sorted.Add(new List<int>());
                }
                foreach (int n in nums) //Split to digits and add to Sorted
                {
                    List<int> digits = SortingTools.Digits(n, DigitLength);
                    Sorted[digits[x]].Add(n);
                }

                /**foreach (int n in nums)
                {
                    char[] digits = n.ToString().ToCharArray();

                    while (digits.Count() < DigitLength)
                    {
                        digits[0] = zero;
                    }
                    if (n.ToString().Length > DigitLength - x) //Get first digit
                    {
                        char[] digits = nums[y].ToString().ToCharArray();
                        tempA = digits[x];
                    }
                    else
                    {
                        char[] digits = "0".ToCharArray();
                        tempA = digits[0];
                    }
                }**/
                List<int> newNums = new List<int>();
                foreach (List<int> n in Sorted)
                {
                    foreach (int i in n)
                    {
                        newNums.Add(i);
                    }
                }
                nums = newNums.ToArray<int>();
                Sorted.Clear();
            }


            return nums;
        }

        public static int[] RadixMSD(IEnumerable<int> arr) //MSD: Most Significant Digit (start with far left digit)
        {
            var nums = arr.ToArray();
            return nums;
        }

        public static int[] Tim(IEnumerable<int> arr) //Hybrid of merge and insertion (used in Python 3)
        {
            var nums = arr.ToArray();
            return nums;
        }
    }
}
