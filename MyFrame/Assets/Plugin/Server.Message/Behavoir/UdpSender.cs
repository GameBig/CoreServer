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
        public void Send(string nName,IPaylodable paylodable)
        {
            var con = world.Globel.GetComponent<ConnectorsComponent>();
            uint id;
            if (con.connectionNames.TryGetValue(nName, out id))
            {
                Send(id, paylodable);
            }
        }
        public Task<int> Connect(string nName,IPEndPoint endPoint)
        {
            var con = world.Globel.GetComponent<ConnectorsComponent>();
            Entity socket = world.Add<Entity>();
            socket.AddComponent<EndPointComponent>().remote = endPoint;
            con.dictionary.Add(endPoint, socket.id);
            con.connectionNames.Add(nName, socket.id);
            con.conTcs = new TaskCompletionSource<int>();
            socket.AddComponent<ConTimeCounter>();
            world.AddBehever<TimeCount>();
            Send(endPoint, ConDefine.accept);
            return con.conTcs.Task;
        }
        public Task<int> Connect(string nName,string ip,int port)
        {
            return Connect(nName,new IPEndPoint(IPAddress.Parse(ip), port));
        }
        public Task DisConnect(uint client)
        {
            var end = world.GetComponent<EndPointComponent>(client);
            if (end == null) return Task.CompletedTask;
            var con = world.Globel.GetComponent<ConnectorsComponent>();
            con.conTcs = new TaskCompletionSource<int>();
            Send(end.remote as IPEndPoint, ConDefine.disconnect);
            return con.conTcs.Task;
        }
        public Task DisConnect(string name)
        {
            var con = world.Globel.GetComponent<ConnectorsComponent>();
            uint id;
            if(con.connectionNames.TryGetValue(name,out id))
            {
               return DisConnect(id);
            }
            return Task.CompletedTask;
        }
    }
}
