using Server;
using System.Threading.Tasks;

namespace MServer
{
    class RunGame:Behavior
    {
        public async void Run(MapInfoComponent info)
        {
            foreach (var item in info.players)
            {
                world.GetComponent<StateComponent>(item).state = StateDefine.Gaming;
            }
            var fr = info.Slibing<FrameComponent>();
            var pull = world.GetBehavior<InputPull>();
            var notify = world.GetBehavior<Notify>();
            while (true)
            {
                try
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
                    notify.Run(info.players, message);
                    await Task.Delay((int)(1000f / fr.hz));
                    fr.frame++;
                }
                catch (System.Exception e)
                {
                    System.Console.WriteLine("RunGame::Error  " + e);
                }
            }
        }
        private bool isAllOver(MapInfoComponent map)
        {
            foreach (var item in map.players)
            {
                var s = world.GetComponent<StateComponent>(item);
                if (s == null) continue;
                if (s.state != StateDefine.Over)
                    return false;
            }
            return true;
        }
    }
}
