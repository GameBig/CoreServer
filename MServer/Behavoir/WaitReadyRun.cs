using Server;
namespace MServer
{
    class WaitReadyRun : Behavior, IUpdate
    {
        struct Tuple : ITuple
        {
            public Entity entity;
            public WaitCounterComponent wait;
            public MapInfoComponent info;
            public void SetCmps(Entity entity)
            {
                this.entity = entity;
                wait = entity.GetComponent<WaitCounterComponent>();
                info = entity.GetComponent<MapInfoComponent>();
            }
        }
        public async void Update(float tick)
        {
            var ready = world.GetBehavior<AllReady>();
            var run = world.GetBehavior<RunGame>();
            foreach (var item in world.GetTuples<Tuple>())
            {
                item.wait.add += tick;
                if (ready.Run(item.info.players) || item.wait.add >= item.wait.max)
                {
                    item.entity.RemoveComponent<WaitCounterComponent>();
                    await run.Run(item.info);
                }
            }
        }
    }
}
