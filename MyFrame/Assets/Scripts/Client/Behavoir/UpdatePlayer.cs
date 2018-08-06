using Server;
namespace Client
{
    [NotReflect]
    class UpdatePlayer : Behavior, IUpdate
    {
        public void Update(float tick)
        {
            var fq = world.Globel.GetComponent<FrameQueue>();
            var nid = world.Globel.GetComponent<NIDComponent>();
            if (fq.queue.Count > 0)
            {
                var frame = fq.queue.Dequeue();
                foreach (var item in frame.paylods)
                {
                    Player player = world.Get<Player>(nid.playerMap[item.Id]);
                    player.GetComponent<PositionComponent>().pos += item.MoveDir * 20;
                    
                    if (item.fire)
                    {
                        var b = world.Add<Bullet>();
                        b.GetComponent<PlayerIDComponent>().pid = player.id;
                        b.GetComponent<MoveComponent>().dir = item.FireDir;
                    }
                }
            }
        }
    }
}
