using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGamePool1
{
    class ExtraStackFunctions
    {
        static string ReturnFullStack(Stack self)
        {
            string tempString = "";
            for (int x = 0; x < Stack.GetLength(self); x++)
            {
                tempString += self.Contents[x];
            }
            return tempString;
        }
        static Stack Insert(Stack self, string item)
        {
            for (int x = self.Contents.Count() - 1; x > self.Pointer; x--)
            {
                self.Contents[x] = self.Contents[x - 1];
            }
            return Stack.Push(self, item);
        }
    }
    class ExtraQueueFunctions
    {

    }
}
