using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;
namespace ServerApp
{
    class TimeCounter : Behavior, IUpdate
    {
        public void Update(float tick)
        {
            foreach (var item in world.GetComponents<TimeCounterComponent>())
            {
                item.add += tick;
                if (item.add >= item.max)
                {
                    var pla = item.Slibing<PlayersComponent>();
                    world.GetBehavior<GameOver>().Run(pla, 1 - pla.current);
                }
            }
        }
    }
}
