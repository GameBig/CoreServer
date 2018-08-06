using System;
using Server;
using System.Threading;
namespace ServerApp
{
    class App
    {
        private static int RunTick = 20;
        private static CancellationTokenSource token;
        static void Main(string[] args)
        {
            token = new  CancellationTokenSource();
            token.Token.Register(()=> 
            {
                Console.WriteLine("App::Quit");
            });
            WorldInitialer.Load(World.Active, typeof(World));
            WorldInitialer.Load(World.Active, typeof(App));
            var socket = World.Active.Globel.AddComponent<WebSocketServerComponent>();
            socket.Init(10000);
            Console.WriteLine("Start::ws://" + socket.listener.Address + ":" + socket.listener.Port + "/");
            World.Active.RunAsync(RunTick, token.Token);
            while (true)
            {
                string cos = Console.ReadLine();
                switch (cos)
                {
                    case "quit":
                        token.Cancel();
                        return;
                }
            }
        }
    }
}
