using System;
using System.Collections.Generic;
using System.Text;
using Server;
namespace MServer
{
    class AllReady:Behavior
    {
        public bool Run(uint[] player)
        {
            foreach (var item in player)
            {
                if (world.GetComponent<StateComponent>(item).state != StateDefine.Ready)
                    return false;
            }
            return true;
        }
    }
}
