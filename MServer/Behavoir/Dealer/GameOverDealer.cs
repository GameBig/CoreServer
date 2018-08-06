using Server.Message;
using Server;
namespace MServer
{
    class GameOverDealer : MsgDealer<RequestOver>
    {
        protected override void Handle(uint client, RequestOver msg)
        {
            if (world.GetBehavior<GamingChick>().Run(client))
            {
                world.GetComponent<StateComponent>(client).state = StateDefine.Over;
            }
        }
    }
}
