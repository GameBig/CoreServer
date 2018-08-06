using Server;

namespace MServer
{
    class MapInfoComponent : Component
    {
        public int member;
        public int count { get; private set; }
        public bool isFull()
        {
            return count >= member;
        }
        public void Add(uint id)
        {
            players[count++] = id;
        }
        public void Remove(uint id)
        {
            int i = 0;
            for (; i < players.Length; i++)
            {
                if(players[i]==id) break;
            }
            for (; i < players.Length-1; i++)
            {
                players[i] = players[i + 1];
            }
            count--;
        }
        public uint[] players { get; private set; }
        public void Init(int mem)
        {
            this.member = mem;
            this.players = new uint[mem];
        }
        public void Clear()
        {
            count = 0;
            member = 0;
        }
    }
}
