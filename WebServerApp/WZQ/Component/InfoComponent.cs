using Server;
namespace ServerApp
{
    class InfoComponent:Component
    {
        public bool matching;
        public uint room;
        public bool ready;
        public byte color;
        public void Reset()
        {
            matching = false;
            room = 0;
            ready = false;
            color = Define.no;
        }
    }
}
