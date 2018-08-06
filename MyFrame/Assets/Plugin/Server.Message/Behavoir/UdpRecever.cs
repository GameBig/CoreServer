using System;
using System.Threading.Tasks;
using System.Net;
namespace Server.Message
{
    public class UdpRecever : Behavior
    {
        public Task<UdpResult> OnceRun(EndPoint end)
        {
            var Sock = world.Globel.GetComponent<SocketComponent>();
            if (end == null) throw new Exception("can not recv from a null endpoint");
            if(Sock.socket== null) throw new Exception("you socket net inited ,please use Behavior\"UdpInit\" to init it!");
            return Task.Factory.StartNew(() =>
            {
                byte[] buffer = unSafeByteHelper.Take(1024);
                int n = Sock.socket.ReceiveFrom(buffer, ref end);
                ByteStream stream = ByteStream.Take();
                stream.Set(buffer, 0, n);
                UdpResult result = new UdpResult() { endPoint = (IPEndPoint)end, stream = stream };
                unSafeByteHelper.Return(buffer);
                return result;
            }, Sock.cancellation.Token);
        }
        public async void Run(EndPoint end = null)
        {
            if (end == null) end = new IPEndPoint(IPAddress.Any, 0);
            var Sock = world.Globel.GetComponent<SocketComponent>();
            var Pid = world.Globel.GetComponent<PIDComponent>();
            var con = world.Globel.GetComponent<ConnectorsComponent>();
            var sender = world.GetBehavior<UdpSender>();
            while (!Sock.cancellation.Token.IsCancellationRequested)
            {
                try
                {
                    UdpResult result = await OnceRun(end);
                    uint pid = result.stream.GetUInt();
                    if (Pid.Pid != pid) continue;
                    string dealer;
                    if (con.dealers.TryGetValue(result.stream.GetByte(), out dealer))
                    {
                        await world.GetBehavior<ConDealer>(dealer)?.Run(result, con, sender);
                    }
                }
                catch (System.Net.Sockets.SocketException e)
                {
                    if (e.ErrorCode == 10054) continue;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}
