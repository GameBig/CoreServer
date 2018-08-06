using System;
using System.Collections.Generic;
using System.Text;
using Server;
namespace Client
{
    class Bullet:Entity
    {
        public override void Start()
        {
            AddComponent<PositionComponent>();
            AddComponent<CycleComponent>().r=2;
            AddComponent<MoveComponent>().speed=50;
            AddComponent<PlayerIDComponent>();
        }
    }
}
