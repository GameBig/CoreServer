using System.Net;
using System.Threading.Tasks;

namespace Server.Message
{
    public class UdpSender:Behavior
    {
        internal void Send(IPEndPoint endPoint,byte msg)
        {
            uint pid =world.Globel.GetComponent<PIDComponent>().Pid;
            var Sock = world.Globel.GetComponent<SocketComponent>();
            byte[] data = pid.ToBytes().Add(msg);
            Sock.socket.SendTo(data, endPoint);
            unSafeByteHelper.Return(data);
        }
        public void Send(uint client,IPaylodable paylodable)
        {
            var end = world.GetComponent<EndPointComponent>(client);
            if (end == null) return;
            uint pid = world.Globel.GetComponent<PIDComponent>().Pid;
            var Sock = world.Globel.GetComponent<SocketComponent>();
            byte[] data = pid.ToBytes().Add(ConDefine.msg).Add(paylodable.Type.ToBytes()).Add(paylodable.ToBytes());
            Sock.socket.SendTo(data, end.remote);
            unSafeByteHelper.Return(data);
        }
        public Task<uint> Connect(IPEndPoint endPoint)
        {
            var con = world.Globel.GetComponent<ConnectorsComponent>();
            Entity socket = world.Add<Entity>();
            socket.AddComponent<EndPointComponent>().remote = endPoint;
            con.dictionary.Add(endPoint, socket.id);
            con.conTcs = new TaskCompletionSource<uint>();
            Send(endPoint, ConDefine.accept);
            return con.conTcs.Task;
        }
        public Task<uint> Connect(string ip,int port)
        {
            return Connect(new IPEndPoint(IPAddress.Parse(ip), port));
        }
        public Task DisConnect(uint client)
        {
            var end = world.GetComponent<EndPointComponent>(client);
            if (end == null) return Task.CompletedTask;
            var con = world.Globel.GetComponent<ConnectorsComponent>();
            con.conTcs = new TaskCompletionSource<uint>();
            Send(end.remote as IPEndPoint, ConDefine.disconnect);
            return con.conTcs.Task;
        }
    }
}
