using Server;
using Server.Message;
namespace MServer
{
    class InputDealer : MsgDealer<InputMessage>
    {
        protected override void Handle(uint client, InputMessage msg)
        {
            if (world.GetBehavior<GamingChick>().Run(client))
            {
                System.Console.WriteLine("InputDealer::Handle");
                world.GetComponent<InputComponenet>(client).data = msg;
            }
        }
    }
    class GamingChick:Behavior
    {
        public bool Run(uint client)
        {
            return world.GetComponent<StateComponent>(client).state == StateDefine.Gaming;
        }
    }
}
