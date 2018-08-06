using Server.Message;
using Server;
namespace MServer
{
    class InitOkDealer : MsgDealer<InitOK>
    {
        protected override void Handle(uint client, InitOK msg)
        {
            var state = world.GetComponent<StateComponent>(client);
            if (state.state != StateDefine.Readying) return;
            state.state = StateDefine.Ready;
        }
    }
}
