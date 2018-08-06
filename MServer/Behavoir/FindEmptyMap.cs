using Server;
namespace MServer
{
    class FindEmptyMap:Behavior
    {
        public  MapInfoComponent Run(int type)
        {
            foreach (var item in world.GetComponents<MapInfoComponent>())
            {
                if (item.member != type) continue;
                if (!item.isFull())
                    return item;
            }
            Map map = world.Add<Map>();
            var info = map.GetComponent<MapInfoComponent>();
            info.Init(type);
            return info;
        }
    }
}
