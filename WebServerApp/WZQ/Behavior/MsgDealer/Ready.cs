using Server;
using System;

namespace ServerApp
{
    class Ready : MsgDealer<NullMsg>
    {
        protected override void Deal(ISession session, NullMsg msg)
        {
            var id = world.GetComponent<InfoComponent>(session.eid);
            if (id.ready) return;
            id.ready = true;
            var idcmp = world.GetComponent<PlayersComponent>(id.room);
            if (idcmp == null)
            {
                id.ready = false;
                Console.WriteLine("Ready::Null Room");
                return;
            }
            foreach (var item in idcmp.Players())
            {
                if (!world.GetComponent<InfoComponent>(item).ready) return;
            }
            idcmp.entity.AddComponent<TimeCounterComponent>().max=5;
            world.GetBehavior<Playing>().Run(idcmp);
        }
    }
}
