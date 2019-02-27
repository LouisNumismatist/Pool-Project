using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGamePool1
{
    class Player
    {
        public int ID;
        public string Name;
        public int Shots;

        public Player(int id, string name, int shots)
        {
            ID = id;
            Name = name;
            Shots = shots;
        }
    }
}
