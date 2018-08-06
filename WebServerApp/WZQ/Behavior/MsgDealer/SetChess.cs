using Server;
namespace ServerApp
{
    class SetChess : MsgDealer<IndexData>
    {
        protected override void Deal(ISession session, IndexData msg)
        {
            var id = world.GetComponent<InfoComponent>(session.eid);
            var chess = world.GetComponent<ChessComponent>(id.room);
            if (!id.ready || chess == null) return;
            world.GetBehavior<InteralSetChess>().Run(chess, session.eid, msg.index, id.color);
        }
    }
}
