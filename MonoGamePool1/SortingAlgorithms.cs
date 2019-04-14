using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGamePool1
{
    /// <summary>
    /// A class for algorithms which sort lists of data
    /// </summary>
    public class Algorithms
    {
        public static T[] MergeGeneric<T>(IEnumerable<T> arr, IComparer<T> comparer) //minor
        {
            return MergeMainGeneric(arr, comparer, 0, arr.Count() - 1);
        }

        public static T[] QuickGeneric<T>(IEnumerable<T> arr, IComparer<T> comparer) //minor
        {
            return QuickMainGeneric(arr, comparer, 0, arr.Count() - 1);
        }

        private static T[] MergeMainGeneric<T>(IEnumerable<T> arr, IComparer<T> comparer, int left, int right) //major
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

        private static T[] QuickMainGeneric<T>(IEnumerable<T> arr, IComparer<T> comparer, int outerLeft, int outerRight) //major
        {
            T[] array = arr.ToArray();
            int left = outerLeft;
            int right = outerRight;
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
            if (right > outerLeft)
            {
                array = QuickMainGeneric(array, comparer, outerLeft, right - 1);
            }
            if (right + 1 < outerRight)
            {
                array = QuickMainGeneric(array, comparer, right + 1, outerRight);
            }

            return array;
        }

        public static T[] BubbleGeneric<T>(IEnumerable<T> arr, IComparer<T> comparer) //major
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

        public static T[] CocktailShakerGeneric<T>(IEnumerable<T> arr, IComparer<T> comparer) //major
        {
            T[] array = arr.ToArray();

            int left = 0;
            int right = array.Count() - 1;

            int comp;

            while (left < right)
            {
                //Left to right sorting
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
                //Right to left sorting
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
    }
}
