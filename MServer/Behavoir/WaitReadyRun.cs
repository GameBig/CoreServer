using Server;
namespace MServer
{
    struct Tuple : ITuple
    {
        //public Entity entity;
        public WaitCounterComponent wait;
        public MapInfoComponent info;
        public void SetCmps(Entity entity)
        {
            //this.entity = entity;
            wait = entity.GetComponent<WaitCounterComponent>();
            info = entity.GetComponent<MapInfoComponent>();
        }
    }
    class WaitReadyRun : Behavior, IUpdate
    {
   
        public void Update(float tick)
        {
            var ready = world.GetBehavior<AllReady>();
          
            foreach (var item in world.GetTuples<Tuple>())
            {
                System.Console.WriteLine("WaitReadyRun::Update");
                item.wait.add += tick;
                if (ready.Run(item.info.players) || item.wait.add >= item.wait.max)
                {
                    item.info.entity.RemoveComponent<WaitCounterComponent>();
                    world.GetBehavior<RunGame>().Run(item.info);
                }
            }
        }
    }
}
