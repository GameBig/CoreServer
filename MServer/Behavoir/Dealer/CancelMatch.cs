using Server.Message;
using Server;
namespace MServer
{
    class CancelMatch : MsgDealer<MachingCancel>
    {
        protected override void Handle(uint client, MachingCancel msg)
        {
            
            var state = world.GetComponent<StateComponent>(client);
            if(state!=null&&state.state== StateDefine.Mapping)
            {
                System.Console.WriteLine($"CancelMatch::Handle");
                var map = world.GetComponent<MapInfoComponent>(state.map);
                if (map != null) map.Remove(client);
                state.Clear();
                world.GetBehavior<UdpSender>().Send(client, new CancelOk());
            }
        }
    }
}
