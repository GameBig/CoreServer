using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;
namespace ServerApp
{
    class InteralSetChess:Behavior
    {
        public async void Run(ChessComponent chess,uint cid, int index, byte color)
        {
            if (index < 0 || index > chess.chess.Length) return;
            if (chess.chess[index] != 0) return;
            var players = chess.Slibing<PlayersComponent>();
            if (players[players.current] != cid) return;
            chess.chess[index] = color;
            await world.GetBehavior<Sender>().NotifyAsync(players.Players(), FuncDefine.onPlayed, new SetChessMessage() { color = color, index = index });
            if (world.GetBehavior<CheckFiveChess>().Run(chess))
            {
                world.GetBehavior<GameOver>().Run(players,players.current);
            }
            else
            {
                players.current = 1 - players.current;
                chess.Slibing<TimeCounterComponent>().add = 0;
                world.GetBehavior<Playing>().Run(players);
            }
        }
    }
}
