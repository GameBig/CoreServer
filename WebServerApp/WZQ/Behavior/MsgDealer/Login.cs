using System;
using Newtonsoft.Json.Linq;
using Server;
namespace ServerApp
{
    class Login : MsgDealer<NullMsg>
    {
        protected override void Deal(ISession session, NullMsg msg)
        {
            session.Send(typeName, new IdData { id = session.eid });
        }
    }
}
