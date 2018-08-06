using Server;
namespace MServer
{
    class AddPlayer: Behavior
    {
        public void Run(MapInfoComponent map,uint client)
        {
            Client player = world.Add<Client>();
            var state = world.GetComponent<StateComponent>(client);
            state.state = StateDefine.Mapping;
            map.Add(player.id);
        }
    }
}
