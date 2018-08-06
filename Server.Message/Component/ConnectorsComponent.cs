using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
namespace Server.Message
{
    public class ConnectorsComponent:Component
    {
        public Dictionary<IPEndPoint, uint> dictionary = new Dictionary<IPEndPoint, uint>();
        public Dictionary<byte, string> dealers = new Dictionary<byte, string>();
        internal TaskCompletionSource<uint> conTcs;
        public void AsClient()
        {
            dealers.Add(ConDefine.connected, typeof(onConnect).Name);
            dealers.Add(ConDefine.disconnected, typeof(onDisConnect).Name);
            dealers.Add(ConDefine.msg, typeof(Msg).Name);
        }
        public void AsServer()
        {
            dealers.Add(ConDefine.accept, typeof(Accept).Name);
            dealers.Add(ConDefine.disconnect, typeof(DisConnect).Name);
            dealers.Add(ConDefine.msg, typeof(Msg).Name);
        }
    }
}
