using Server;
namespace ServerApp
{
    class Room:Entity
    {
        public override void Start()
        {
            AddComponent<PlayersComponent>();
        }
    }
}
