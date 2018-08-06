using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;
namespace Server.Message
{
    public class SocketComponent:Component
    {
        public Socket socket;
        public CancellationTokenSource cancellation = new CancellationTokenSource();
    }
}
