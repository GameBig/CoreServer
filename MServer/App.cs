using System;
using Server.Message;
using Server;

namespace MServer
{
    class App
    {
        static void Main(string[] args)
        {
            Run();
            Console.WriteLine($"Server Start At::{World.Active.Globel.GetComponent<SocketComponent>().socket.LocalEndPoint}");
            World.Active.RunAsync(20);
            while(Console.ReadLine()!="q")continue; 
        }
        static void Run()
        {
            WorldInitialer.Load(World.Active, typeof(World));
            WorldInitialer.Load(World.Active, typeof(App));
            WorldInitialer.Load(World.Active, typeof(UdpInit));
            World.Active.GetBehavior<UdpInit>().Load(typeof(App)).Init(10000);
            World.Active.Globel.GetComponent<ConnectorsComponent>().AsServer();
            World.Active.GetBehavior<UdpRecever>().Run();
        }
    }
}
