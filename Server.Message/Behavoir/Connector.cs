using System.Threading.Tasks;

namespace Server.Message
{
    static class ConDefine
    {
        public const byte accept = 0;
        public const byte connected = 1;
        public const byte msg = 2;
        public const byte disconnect = 3;
        public const byte disconnected = 4;
    }
    public abstract class ConDealer : Behavior
    {
        public abstract Task Run(UdpResult result, ConnectorsComponent connectors, UdpSender sender);
    }
    class Accept : ConDealer
    {
        public override Task Run(UdpResult result, ConnectorsComponent connectors, UdpSender sender)
        {
            return Task.Factory.StartNew(() =>
            {
                if (!connectors.dictionary.ContainsKey(result.endPoint))
                {
                    Entity e = world.Add<Entity>();
                    e.AddComponent<EndPointComponent>().remote = result.endPoint;
                    connectors.dictionary.Add(result.endPoint, e.id);
                    sender.Send(result.endPoint, ConDefine.connected);
                }
            });
        }
    }
    class Msg : ConDealer
    {
        public override Task Run(UdpResult result, ConnectorsComponent connectors, UdpSender sender)
        {
            return Task.Factory.StartNew(() =>
            {
                if (connectors.dictionary.TryGetValue(result.endPoint, out uint eid))
                {
                    ushort func = result.stream.GetUShort();
                    IPaylodable paylodable = MsgMaper.Get(func);
                    if (paylodable == null) return;
                    paylodable.GetFrom(result.stream);
                    APDealer dealer = world.GetBehavior<APDealer>(MsgMaper.Get(paylodable.GetType().Name));
                    dealer?.Deal(eid, paylodable);
                    ByteStream.Return(result.stream as ByteStream);
                }
            });
        }
    }
    class DisConnect : ConDealer
    {
        public override Task Run(UdpResult result, ConnectorsComponent connectors, UdpSender sender)
        {
            return Task.Factory.StartNew(() =>
            {
                if (connectors.dictionary.TryGetValue(result.endPoint, out uint eid))
                {
                    world.RemoveEntity(eid);
                    connectors.dictionary.Remove(result.endPoint);
                    sender.Send(result.endPoint, ConDefine.disconnected);
                }
            });
        }
    }
    class onConnect : ConDealer
    {
        public override Task Run(UdpResult result, ConnectorsComponent connectors, UdpSender sender)
        {
            connectors.dictionary.TryGetValue(result.endPoint, out uint eid);
            connectors.conTcs.SetResult(eid);
            return Task.CompletedTask;
        }
    }
    class onDisConnect : ConDealer
    {
        public override Task Run(UdpResult result, ConnectorsComponent connectors, UdpSender sender)
        {
            connectors.dictionary.TryGetValue(result.endPoint, out uint eid);
            connectors.conTcs.SetResult(eid);
            return Task.CompletedTask;
        }
    }
}
