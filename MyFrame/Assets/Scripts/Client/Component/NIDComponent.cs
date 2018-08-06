using Server;
using System.Collections.Generic;
namespace Client
{
    [Globel]
    class NIDComponent:Component
    {
        public Dictionary<uint, uint> playerMap = new Dictionary<uint, uint>();
    }
}
