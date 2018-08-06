using System.Threading.Tasks;
namespace Server
{
    public class Sender:Behavior
    {
        public void Send(uint client,string func, SenderMessage data)
        {
            var cl = world.GetComponent<SessionComponent>(client);
            if (cl == null) return;
            cl.gameSession.Send(func, data);
        }
        public async Task SendAsync(uint client, string func, SenderMessage data)
        {
            var cl = world.GetComponent<SessionComponent>(client);
            if (cl == null) return;
            await cl.gameSession.SendAsync(func, data);
        }
        public void Notify(uint[] ids,string func, SenderMessage payloda)
        {
            foreach (var item in ids)
            {
                Send(item, func, payloda);
            }
        }
        public async Task NotifyAsync(uint[] ids,string func,SenderMessage payloda)
        {
            foreach (var item in ids)
            {
                await SendAsync(item, func, payloda);
            }
        }
    }
}
