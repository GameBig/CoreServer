using System;
using System.Collections.Generic;
using System.Text;
using Server;
namespace Client
{
    class Player:Entity
    {
        public override void Start()
        {
            AddComponent<PositionComponent>();
            AddComponent<CycleComponent>();
        }
    }
}
