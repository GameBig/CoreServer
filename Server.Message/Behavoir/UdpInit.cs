using System;
using System.Net.Sockets;
using System.Net;
namespace Server.Message
{
    public class UdpInit:Behavior
    {
        public UdpInit Load(Type type)
        {
            world.Globel.AddComponent<PIDComponent>();
            world.Globel.AddComponent<ConnectorsComponent>();
            world.Globel.AddComponent<SocketComponent>();
            MsgMaper.Load(type);
            return this;
        }
        public void Init(IPEndPoint endPoint)
        {
            var socket = world.Globel.GetComponent<SocketComponent>();
            socket.socket = new Socket(AddressFamily.InterNetwork,SocketType.Dgram, ProtocolType.Udp);
            socket.socket.Bind(endPoint);
        }
        public void Init(int port)
        {
            Init(new IPEndPoint(IPAddress.Any, port));
        }
        public void Init()
        {
            Init(new IPEndPoint(IPAddress.Any, 0));
        }
    }
}
