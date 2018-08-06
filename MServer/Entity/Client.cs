using Server;
namespace MServer
{
    class Client: Entity
    {
        public override void Start()
        {
            AddComponent<StateComponent>();
        }
    }
}
