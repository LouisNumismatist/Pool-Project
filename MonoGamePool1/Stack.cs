using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGamePool1
{
    /// <summary>
    /// My own implementation of a stack data structure, mainly used for textboxes,
    /// however as it is generic it could be used for other variable types than strings
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Stack<T>
    {
        private int Pointer;
        private readonly T[] Contents;
        private readonly int Max;

        public Stack(T[] contents, int max)
        {
            Pointer = 0;
            Contents = contents;
            Max = max;
        }

        public int GetLength()
        {
            return Pointer;
        }

        public void Push(T a)
        {
            if (Pointer < Max)
            {
                Contents[Pointer] = a;
                Pointer++;
            }
        }

        public T Pop()
        {
            T a = Peek();
            if (Pointer > 0)
            {
                Pointer--;
            }
            return a;
        }

        public void Clear()
        {
            Pointer -= GetLength();
        }

        public bool IsEmpty()
        {
            if (Pointer == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public T Peek()
        {
            if (Pointer > 0)
            {
                return Contents[Pointer - 1];
            }
            else
            {
                return default(T);
            }
        }

        public T[] GetContents()
        {
            return Contents;
        }
    }
}
