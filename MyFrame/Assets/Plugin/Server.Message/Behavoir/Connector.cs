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
                    System.Console.WriteLine($"Accept::Run::ID={e.id}");
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
                uint eid;
                if (connectors.dictionary.TryGetValue(result.endPoint, out eid))
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
                uint eid;
                if (connectors.dictionary.TryGetValue(result.endPoint, out eid))
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
            uint eid;
            connectors.dictionary.TryGetValue(result.endPoint, out eid);
            connectors.conTcs.SetResult(0);
            world.Get<Entity>(eid)?.RemoveComponent<ConTimeCounter>();
            world.RemoveBehavior<TimeCount>();
            return Task.CompletedTask;
        }
    }
    class onDisConnect : ConDealer
    {
        public override Task Run(UdpResult result, ConnectorsComponent connectors, UdpSender sender)
        {
            uint eid;
            connectors.dictionary.TryGetValue(result.endPoint, out eid);
            connectors.conTcs.SetResult(0);
            world.Get<Entity>(eid)?.RemoveComponent<ConTimeCounter>();
            world.RemoveBehavior<TimeCount>();
            return Task.CompletedTask;
        }
    }
}
