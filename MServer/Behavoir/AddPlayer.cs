using Server;
namespace MServer
{
    class AddPlayer: Behavior
    {
        public void Run(MapInfoComponent map,uint client)
        {
            var state = world.GetComponent<StateComponent>(client);
            state.state = StateDefine.Mapping;
            map.Add(client);
            System.Console.WriteLine("AddPlayer::Run");
        }
    }
}
