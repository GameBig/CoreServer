using Server;
using WebSocketSharp.Server;

namespace ServerApp
{
    public class WebSocketServerComponent:Component
    {
        public WebSocketServer listener { get; private set; }
        public void Init(int port)
        {
            listener = new WebSocketServer(port);
            listener.Start();
            listener.AddWebSocketService<GameSession>("/",(e)=> 
            {
               // GameSession game = new GameSession();
                e.SetWorld(entity.world);
            });
        }
        public override void Destroy()
        {
            listener.Stop();
        }
    }
}
