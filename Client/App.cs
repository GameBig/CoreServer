using System;
using Server;
using Server.Message;
using System.Threading.Tasks;
namespace Client
{
    class App
    {
        static void Main(string[] args)
        {
            Run();
            World.Active.Run(20);
            Console.Read();
        }
        static async void Run()
        {
            WorldInitialer.Load(World.Active, typeof(World));
            WorldInitialer.Load(World.Active, typeof(App));
            WorldInitialer.Load(World.Active, typeof(UdpInit));
            World.Active.GetBehavior<UdpInit>().Load(typeof(App)).Init();
            World.Active.Globel.GetComponent<ConnectorsComponent>().AsClient();
            World.Active.GetBehavior<UdpRecever>().Run();

            var sender = World.Active.GetBehavior<UdpSender>();
            if(await sender.Connect("game","127.0.0.1", 10000) >= 0)
            {
                Console.WriteLine("Connected");

                sender.Send("game", new MatchReq());
            }
            
        }
    }

    class matching : MsgDealer<Matching>
    {
        protected override void Handle(uint client, Matching msg)
        {
            System.Console.WriteLine("matching::Handle");
            Console.WriteLine("Waiting");
        }
    }
    class InitGame : MsgDealer<GameInitMessage>
    {
        protected override void Handle(uint client, GameInitMessage msg)
        {
            System.Console.WriteLine("InitGame::Handle");
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
            System.Console.WriteLine("FrameUpdate::Handle");
            world.Globel.GetComponent<FrameQueue>().queue.Enqueue(msg);
        }
    }
}
