using Server;
namespace MServer
{
    class StateComponent: Component
    {
        public StateDefine state;
        public uint map;
        public void Clear()
        {
            map = 0;
            state = StateDefine.Waiting;
        }
    }
}
