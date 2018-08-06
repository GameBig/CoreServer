using Server;

namespace MServer
{
    class MapInfoComponent : Component
    {
        public int number;
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
