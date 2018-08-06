using Server;
namespace MServer
{
    class MapInit:Behavior
    {
        public void Run(MapInfoComponent map)
        {
            int seed = world.GetBehavior<RandomBhr>().random();
            GameInitMessage message = new GameInitMessage() { seed = seed };
            message.pid.AddRange(map.players);
            foreach (var item in map.players)
            {
                world.GetComponent<StateComponent>(item).state = StateDefine.Readying;
                world.Get<Client>(item).AddComponent<InputComponenet>();
            }
            world.GetBehavior<Notify>().Run(map.players, message);
            map.entity.AddComponent<WaitCounterComponent>();
            map.entity.AddComponent<FrameComponent>();
        }
    }
}
