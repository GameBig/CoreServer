using Server;
namespace ServerApp
{
    class MatchGame : MsgDealer<NullMsg>
    {
        protected override async void Deal(ISession session, NullMsg msg)
        {
            var id = world.Get<Entity>(session.eid).AddComponent<InfoComponent>();
            if (id.matching) return;
            id.matching = true;
            var find = world.GetBehavior<FindEmptyRoom>().Find();
            find.Add(session.eid);
            id.room = find.entity.id;
            await session.SendAsync(typeName, new SenderMessage());
            if (find.isFull())
            {
                world.GetBehavior<InitGame>().Run(find);
            }
        }
    }
}
