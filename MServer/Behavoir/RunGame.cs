using Server;
using System.Threading.Tasks;

namespace MServer
{
    class RunGame:Behavior
    {
        public async Task Run(MapInfoComponent info)
        {
            var fr = info.Slibing<FrameComponent>();
            var pull = world.GetBehavior<InputPull>();
            var notify = world.GetBehavior<Notify>();
            while (true)
            {
                if (isAllOver(info))
                {
                    notify.Run(info.players, new GameOverMessage());
                    info.Clear();
                    fr.frame = 0;
                    foreach (var item in info.players)
                    {
                        world.GetComponent<StateComponent>(item).Clear();
                        world.GetComponent<InputComponenet>(item).data = null;
                    }
                    return;
                }
                var message = pull.Run(info.players, fr.frame);
                fr.frame++;
                notify.Run(info.players, message);
                await Task.Delay((int)(1000f / fr.hz));
            }
        }
        private bool isAllOver(MapInfoComponent map)
        {
            foreach (var item in map.players)
            {
                if (world.GetComponent<StateComponent>(item).state != StateDefine.Over)
                    return false;
            }
            return true;
        }
    }
}
