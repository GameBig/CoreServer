using Server;

namespace ServerApp
{
    class FindEmptyRoom: Behavior
    {
        public PlayersComponent Find()
        {
            foreach (var item in world.GetComponents<PlayersComponent>())
            {
                if (!item.isFull())
                {
                    return item;
                }
            }
            return world.Add<Room>().GetComponent<PlayersComponent>();
        }
    }
}
