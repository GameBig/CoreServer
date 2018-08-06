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
            uint id= await sender.Connect("127.0.0.1", 10000);
            Console.WriteLine("Connected");

            sender.Send(id, new MatchReq());
        }

    }


}
