using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Message
{
    [NotReflect]
    class TimeCount : Behavior, IUpdate
    {
        public void Update(float tick)
        {
            foreach (var item in world.GetComponents<ConTimeCounter>())
            {
                item.add += tick;
                if (item.add >= item.time)
                {
                    world.Globel.GetComponent<ConnectorsComponent>().conTcs.SetResult(-1);
                    item.entity.RemoveComponent<ConTimeCounter>();
                }
            }
        }
    }
}
