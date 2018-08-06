using System.Collections.Generic;
using Server;
namespace Client
{
    class FrameQueue:Component
    {
        public Queue<FrameMessage> queue = new Queue<FrameMessage>();
    }
}
