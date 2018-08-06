using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;
namespace ServerApp
{
    class Test:Behavior
    {
        public void TT()
        {
            var chs = world.Globel.AddComponent<ChessComponent>();
            chs[4, 0] = 1;
            chs[3, 1] = 1;
            chs[2, 2] = 1;
            chs[1, 3] = 1;
            chs[0, 4] = 1;
            Console.WriteLine(world.GetBehavior<CheckFiveChess>().Run(chs));
        }
    }
}
