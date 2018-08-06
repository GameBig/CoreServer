using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;
namespace ServerApp
{
    class PlayersComponent:Component
    {
        private uint[] players = new uint[2];
        private int count = 0;
        public bool isFull()
        {
            return count == 2;
        }
        public int current;
        public void Add(uint id)
        {
            if (isFull()) return;
            if (players.Contains(id)) return;
            players[count++] = id;
        }
        public void Clear()
        {
            for (int i = 0; i < players.Length; i++)
            {
                players[i] = 0;
            }
            count = 0;
            current = 0;
        }
        public uint[] Players()
        {
            return players;
        }
        public uint this[int index]
        {
            get
            {
                return players[index];
            }
        }
    }
}
