using System;
using Server;
using Server.Message;
namespace Client
{
    class CancelMatch : MsgDealer<CancelOk>
    {
        protected override void Handle(uint client, CancelOk msg)
        {
            UnityEngine.Debug.Log("CancelMatch::Handle");
            world.GetBehavior<EventSender>().Invoke("matchingcancel");
        }
    }
    class MatchingDealer : MsgDealer<Matching>
    {
        protected override void Handle(uint client, Matching msg)
        {
            UnityEngine.Debug.Log("MatchingDealer::Handle");
            world.GetBehavior<EventSender>().Invoke("matching");
        }
    }

    class InitGame : MsgDealer<GameInitMessage>
    {
        protected override void Handle(uint client, GameInitMessage msg)
        {
            UnityEngine.Debug.Log("InitGame::Handle");
            world.Globel.GetComponent<RandomComponent>().random = new Random(msg.seed);
            var ran= world.GetBehavior<RandomBhr>();
            var map = world.Globel.GetComponent<NIDComponent>().playerMap;
            foreach (var item in msg.pid)
            {
                var player = world.Add<Player>();
                map.Add(item, player.id);
                player.GetComponent<CycleComponent>().r = 5;
                player.GetComponent<PositionComponent>().pos = new Vec2Int(ran.Range(100,200),ran.Range(100,200));
            }
            world.Globel.AddComponent<FrameQueue>();
            world.GetBehavior<EventSender>().Invoke("InitGame");
            world.GetBehavior<UdpSender>().Send(client, new InitOK());
            world.Globel.AddComponent<InputComponent>().input = new InputMessage();
            world.AddBehever<UpdateInput>();
            world.AddBehever<UpdatePlayer>();
        }
    }
    class FrameUpdate : MsgDealer<FrameMessage>
    {
        protected override void Handle(uint client, FrameMessage msg)
        {
            UnityEngine.Debug.Log("FrameUpdate::Handle");
            world.Globel.GetComponent<FrameQueue>().queue.Enqueue(msg);
        }
    }
}
