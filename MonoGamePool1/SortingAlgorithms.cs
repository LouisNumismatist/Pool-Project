using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGamePool1
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

        public static T[] MergeGeneric<T>(IEnumerable<T> arr, IComparer<T> comparer) //minor
        {
            return MergeMainGeneric(arr, comparer, 0, arr.Count() - 1);
        }

        public static int[] Quick(IEnumerable<int> arr) //minor
        {
            return QuickMain(arr, 0, arr.Count() - 1);
        }

        public static T[] QuickGeneric<T>(IEnumerable<T> arr, IComparer<T> comparer) //minor
        {
            return QuickMainGeneric(arr, comparer, 0, arr.Count() - 1);
        }

        private static int[] MergeMain(IEnumerable<int> arr, int left, int right) //DONE
        {
            int[] array = arr.ToArray();

            int mid = (int)((right - left) / 2 + left);
            if (right - left > 0)
            {
                array = MergeMain(array, left, mid);
                array = MergeMain(array, mid + 1, right);
                int a = left;
                int b = mid + 1;
                List<int> temp = new List<int>();
                while (a <= mid && b <= right)
                {
                    if (array[a] < array[b])
                    {
                        temp.Add(array[a]);
                        a += 1;
                    }
                    else
                    {
                        temp.Add(array[b]);
                        b += 1;
                    }
                }
                while (a <= mid)
                {
                    temp.Add(array[a]);
                    a += 1;
                }
                while (b <= right)
                {
                    temp.Add(array[b]);
                    b += 1;
                }
                for (int x = 0; x < temp.Count(); x++)
                {
                    array[left + x] = temp[x];
                }
            }

            return array;
        }

        private static T[] MergeMainGeneric<T>(IEnumerable<T> arr, IComparer<T> comparer, int left, int right) //DONE
        {
            T[] array = arr.ToArray();

            int mid = ((right - left) / 2 + left);
            if (right - left > 0)
            {
                array = MergeMainGeneric(array, comparer, left, mid);
                array = MergeMainGeneric(array, comparer, mid + 1, right);
                int a = left;
                int b = mid + 1;
                List<T> temp = new List<T>();
                while (a <= mid && b <= right)
                {
                    int comp = comparer.Compare(array[a], array[b]);
                    if (comp == -1)
                    {
                        temp.Add(array[a]);
                        a += 1;
                    }
                    else
                    {
                        temp.Add(array[b]);
                        b += 1;
                    }
                }
                while (a <= mid)
                {
                    temp.Add(array[a]);
                    a += 1;
                }
                while (b <= right)
                {
                    temp.Add(array[b]);
                    b += 1;
                }
                for (int x = 0; x < temp.Count(); x++)
                {
                    array[left + x] = temp[x];
                }
            }

            return array;
        }

        public static int[] InsertionMain(IEnumerable<int> arr, int left, int right) //DONE
        {
            var array = arr.ToArray();

            for (int x = left; x < right; x++)
            {
                for (int y = x; y >= left; y--)
                {
                    if (array[y] < array[y - 1])
                    {
                        int temp = array[y - 1];
                        array[y - 1] = array[y];
                        array[y] = temp;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return array;
        }

        public static int[] QuickMain(IEnumerable<int> arr, int OuterLeft, int OuterRight) //DONE
        {
            var array = arr.ToArray();
            int left = OuterLeft;
            int right = OuterRight;
            int pivot = left;

            while (left < right)
            {
                while (pivot == left)
                {
                    if (array[pivot] < array[right])
                    {
                        right -= 1;
                    }
                    else if (array[pivot] > array[right])
                    {
                        int temp = array[pivot];
                        array[pivot] = array[right];
                        array[right] = temp;
                    }
                    else
                    {
                        break;
                    }
                }
                while (pivot == right)
                {
                    if (array[pivot] > array[left])
                    {
                        left += 1;
                    }
                    else if (array[pivot] < array[left])
                    {
                        int temp = array[pivot];
                        array[pivot] = array[left];
                        array[left] = temp;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if (right > OuterLeft)
            {
                array = QuickMain(array, OuterLeft, right - 1);
            }
            if (right + 1 < OuterRight)
            {
                array = QuickMain(array, right + 1, OuterRight);
            }

            return array;
        }

        private static T[] QuickMainGeneric<T>(IEnumerable<T> arr, IComparer<T> comparer, int OuterLeft, int OuterRight) //DONE
        {
            T[] array = arr.ToArray();
            //var array = arr.ToArray();
            int left = OuterLeft;
            int right = OuterRight;
            int pivot = left;

            int comp = comparer.Compare(array[pivot], array[right]);

            while (left < right)
            {
                while (pivot == left)
                {
                    comp = comparer.Compare(array[pivot], array[right]);
                    if (comp == -1)
                    {
                        right -= 1;
                    }
                    else if (comp == 1)
                    {
                        T temp = array[pivot];
                        array[pivot] = array[right];
                        array[right] = temp;
                    }
                    else
                    {
                        break;
                    }
                }
                while (pivot == right)
                {
                    comp = comparer.Compare(array[pivot], array[left]);
                    if (comp == 1)
                    {
                        left += 1;
                    }
                    else if (comp == -1)
                    {
                        T temp = array[pivot];
                        array[pivot] = array[left];
                        array[left] = temp;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if (right > OuterLeft)
            {
                array = QuickMainGeneric(array, comparer, OuterLeft, right - 1);
            }
            if (right + 1 < OuterRight)
            {
                array = QuickMainGeneric(array, comparer, right + 1, OuterRight);
            }

            return array;
        }

        public static int[] Bubble(IEnumerable<int> arr) //DONE
        {
            var array = arr.ToArray();

            for (int x = 0; x < array.Count(); x++)
            {
                for (int y = 0; y < array.Count() - (x + 1); y++)
                {
                    if (array[y] > array[y + 1])
                    {
                        int temp = array[y];
                        array[y] = array[y + 1];
                        array[y + 1] = temp;
                    }
                }
            }
            return array;
        }

        public static T[] BubbleGeneric<T>(IEnumerable<T> arr, IComparer<T> comparer)
        {
            T[] array = arr.ToArray();

            for (int x = 0; x < array.Count(); x++)
            {
                for (int y = 0; y < array.Count() - (x + 1); y++)
                {
                    if (comparer.Compare(array[y], array[y + 1]) == 1)
                    {
                        T temp = array[y];
                        array[y] = array[y + 1];
                        array[y + 1] = temp;
                    }
                }
            }
            return array;
        }

        public static int[] CocktailShaker(IEnumerable<int> arr) //DONE
        {
            var array = arr.ToArray();

            int left = 0;
            int right = array.Count() - 1;

            while (left < right)
            {
                for (int x = left; x < right; x++)
                {
                    if (array[x] > array[x + 1])
                    {
                        int temp = array[x];
                        array[x] = array[x + 1];
                        array[x + 1] = temp;
                    }
                }
                left += 1;
                if (left == right)
                {
                    break;
                }
                for (int x = right; x >= left; x--)
                {
                    if (array[x] < array[x - 1])
                    {
                        int temp = array[x];
                        array[x] = array[x - 1];
                        array[x - 1] = temp;
                    }
                }
                right -= 1;
            }
            return array;
        }

        public static T[] CocktailShakerGeneric<T>(IEnumerable<T> arr, IComparer<T> comparer) //DONE
        {
            T[] array = arr.ToArray();
            //var array = arr.ToArray();

            int left = 0;
            int right = array.Count() - 1;

            int comp;

            while (left < right)
            {
                for (int x = left; x < right; x++)
                {
                    comp = comparer.Compare(array[x], array[x + 1]);
                    if (comp == 1)
                    {
                        T temp = array[x];
                        array[x] = array[x + 1];
                        array[x + 1] = temp;
                    }
                }
                left += 1;
                if (left == right)
                {
                    break;
                }
                for (int x = right; x >= left; x--)
                {
                    comp = comparer.Compare(array[x], array[x - 1]);
                    if (comp == -1)
                    {
                        T temp = array[x];
                        array[x] = array[x - 1];
                        array[x - 1] = temp;
                    }
                }
                right -= 1;
            }
            return array;
        }

        public static int[] Selection(IEnumerable<int> arr) //DONE
        {
            var array = arr.ToArray();
            int pos;
            for (int x = 0; x < array.Count(); x++)
            {
                pos = x;
                for (int y = x; y < array.Count(); y++)
                {
                    if (array[y] < array[pos])
                    {
                        pos = y;
                    }
                }
                if (pos != x)
                {
                    int temp = array[pos];
                    array[pos] = array[x];
                    array[x] = temp;
                }
            }
            return array;
        }

        public static int[] RadixLSD(IEnumerable<int> arr) //LSD: Least Significant Digit (start with far right digit)
        {
            var array = arr.ToArray();
            int DigitLength = 0;
            //char[] tempChar = "0".ToCharArray();
            //char zero = tempChar[0];
            foreach (int n in array)
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
                foreach (int n in array) //Split to digits and add to Sorted
                {
                    List<int> digits = SortingTools.Digits(n, DigitLength);
                    Sorted[digits[x]].Add(n);
                }

                /**foreach (int n in array)
                {
                    char[] digits = n.ToString().ToCharArray();

                    while (digits.Count() < DigitLength)
                    {
                        digits[0] = zero;
                    }
                    if (n.ToString().Length > DigitLength - x) //Get first digit
                    {
                        char[] digits = array[y].ToString().ToCharArray();
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
                array = newNums.ToArray<int>();
                Sorted.Clear();
            }


            return array;
        }

        public static int[] RadixMSD(IEnumerable<int> arr) //MSD: Most Significant Digit (start with far left digit)
        {
            var array = arr.ToArray();
            return array;
        }

        public static int[] Tim(IEnumerable<int> arr) //Hybrid of merge and insertion (used in Python 3)
        {
            var array = arr.ToArray();
            return array;
        }
    }
}
