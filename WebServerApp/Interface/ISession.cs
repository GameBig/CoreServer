using System.Threading.Tasks;

namespace Server
{
    public interface ISession
    {
        uint eid { get; }
        void SetWorld(World world);
        void Send(string func, SenderMessage data);
        Task SendAsync(string func, SenderMessage data);
    }
}
