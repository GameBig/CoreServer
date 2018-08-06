using Server;
namespace MServer
{
    class Map:Entity
    {
        public override void Start()
        {
            AddComponent<MapInfoComponent>();
        }
    }
}
