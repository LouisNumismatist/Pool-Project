using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGamePool1
{
    public struct Stack
    {
        public int Pointer;
        public List<object> Contents;

        public Stack(int pointer, List<object> contents)
        {
            Pointer = pointer;
            Contents = contents;
        }
        public static int GetLength(Stack self)
        {
            return self.Pointer;
        }
        public static Stack Push(Stack self, object a)
        {
            self.Pointer++;
            self.Contents[self.Pointer] = a;
            return self;
        }
        static Tuple<Stack, object> Pop(Stack self)
        {
            object a = Peek(self);
            self.Pointer--;
            return new Tuple<Stack, object>(self, a);
        }
        public static Stack Clear(Stack self)
        {
            self.Pointer -= GetLength(self);
            return self;
        }
        static bool IsEmpty(Stack self)
        {
            if (self.Pointer == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static object Peek(Stack self)
        {
            return self.Contents[self.Pointer];
        }
    }
    public struct Queue
    {
        public int Head;
        public int Tail;
        public object[] Contents;

        public Queue(int head, int tail, object[] contents)
        {
            Head = head;
            Tail = tail;
            Contents = contents;
        }

        public static int GetLength(Queue self)
        {
            return self.Tail-self.Head;
        }
        public static Queue Push(Queue self, Ball ball)
        {
            self.Tail++;
            self.Contents[self.Tail] = ball;
            return self;
        }
        public static Tuple<Queue, object> Pop(Queue self)
        {
            object a = Peek(self);
            self.Head++;
            return new Tuple<Queue, object>(self, a);
        }
        public static Queue Clear(Queue self)
        {
            self.Head = 0;
            self.Tail = 0;
            return self;
        }
        public static bool IsEmpty(Queue self)
        {
            if (self.Head == self.Tail)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static object Peek(Queue self)
        {
            return self.Contents[self.Head];
        }
    }
}
