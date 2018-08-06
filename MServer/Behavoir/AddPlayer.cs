using Server;
namespace MServer
{
    class AddPlayer: Behavior
    {
        public void Run(MapInfoComponent map,uint client)
        {
            var state = world.GetComponent<StateComponent>(client);
            state.state = StateDefine.Mapping;
            state.map = map.entity.id;
            map.Add(client);
            System.Console.WriteLine("AddPlayer::Run");
        }
    }
}
