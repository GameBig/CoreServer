using Server.Message;
namespace MServer
{
    using Server;
    class MatchGame : MsgDealer<MatchReq>
    {
        protected override void Handle(uint client, MatchReq msg)
        {
            System.Console.WriteLine($"MatchGame::Handle::Type={(msg.mapType == 0 ? 1 : msg.mapType) * 2}");
            world.Get<Entity>(client).AddComponent<StateComponent>();
            if (!CheckState(client)) return;
            var mapinfo = world.GetBehavior<FindEmptyMap>().Run((msg.mapType==0?1: msg.mapType) * 2);
            world.GetBehavior<AddPlayer>().Run(mapinfo, client);
            world.GetBehavior<UdpSender>().Send(client, new Matching());
            if (mapinfo.isFull())
            {
                world.GetBehavior<MapInit>().Run(mapinfo);
            }
        }
        private bool CheckState(uint client)
        {
            return world.GetComponent<StateComponent>(client).state == StateDefine.Waiting;
        }
    }
}
