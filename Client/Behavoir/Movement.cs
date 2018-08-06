using Server;
namespace Client
{
    class Movement : Behavior, IUpdate
    {
        public void Update(float tick)
        {
            foreach (var item in world.GetTuples<MoveTuple>())
            {
                item.position.pos += item.move.dir * item.move.speed * (int)(tick * 1000);
            }
        }
    }
}
