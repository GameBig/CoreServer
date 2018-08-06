using Server;
using Server.Message;
namespace MServer
{
    class Notify:Behavior
    {
        public void Run(uint[] players,IPaylodable paylodable)
        {
            var sender= world.GetBehavior<UdpSender>();
            foreach (var item in players)
            {
                sender.Send(item, paylodable);
            }
        }
    }
}
