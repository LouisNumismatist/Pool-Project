using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGamePool1
{
    /// <summary>
    /// My own implementation of a stack data structure, mainly used for textboxes, 
    /// however as a generic type has been used it could be used for other variable types than strings
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Stack<T>
    {
        private int Pointer;
        private readonly List<T> Contents;

        public Stack(int pointer, List<T> contents)
        {
            Pointer = pointer;
            Contents = contents;
        }

        public int GetLength()
        {
            return Pointer;
        }

        public void Push(T a)
        {
            if (GetLength() < Contents.Count())
            {
                Pointer++;
                Contents[Pointer] = a;
            }
        }

        public T Pop()
        {
            T a = Peek();
            Pointer--;
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
                return Contents[Pointer];
            }
            else
            {
                return default(T);
            }
        }

        public List<T> GetContents()
        {
            return Contents;
        }
        
        public int GetPointer()
        {
            return Pointer;

        }

        /*public void Insert(T item) //NEED TO REMOVE
        {
            for (int x = Contents.Count() - 1; x > Pointer; x--)
            {
                Contents[x] = Contents[x - 1];
            }
            Push(item);
        }*/
    }
}
