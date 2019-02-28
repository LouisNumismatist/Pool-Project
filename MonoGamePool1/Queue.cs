using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGamePool1
{
    /// <summary>
    /// My own implementation of a circular queue data structure, used in the MiniGraphs mainly
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Queue<T>
    {
        private int Head;
        private int Tail;
        private readonly T[] Contents;

        public Queue(int length)
        {
            Head = -1;
            Tail = 0;
            Contents = new T[length];
        }

        public int GetLength()
        {
            return Tail - Head;
        }

        public int GetTail()
        {
            return Tail;
        }

        public void Enqueue(T item)
        {
            if (Tail == Contents.Length)
            {
                Tail = 0;
            }
            
            Contents[Tail] = item;
            Tail++;
            if (Tail >= Contents.Length)
            {
                Tail = 0;
            }
        }

        public T Dequeue()
        {
            T a = Peek();
            Head++;
            if (Head >= Contents.Length)
            {
                Head = 0;
            }
            return a;
        }

        public void Clear()
        {
            Head = 0;
            Tail = 0;
        }

        public bool IsEmpty()
        {
            if (Head == Tail)
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
            if (Tail > 0 && Tail < Contents.Length)
            {
                //Console.WriteLine("1 Tail " + Tail);
                return Contents[Tail - 1];
            }
            else
            {
                //Console.WriteLine("2 Tail " + Tail);
                return default(T);
            }
        }

        public T[] GetContents()
        {
            return Contents;
        }
    }
}
