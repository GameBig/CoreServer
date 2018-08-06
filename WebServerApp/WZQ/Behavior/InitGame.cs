using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;
namespace ServerApp
{
    class InitGame:Behavior
    {
        public async void Run(PlayersComponent players)
        {
            players.entity.AddComponent<ChessComponent>();
            int idx = world.GetBehavior<RandomBhr>().Range(0, 2);
            players.current = idx;
            world.GetComponent<InfoComponent>(players[idx]).color = Define.white;
            world.GetComponent<InfoComponent>(players[1 - idx]).color = Define.black;
            var sender = world.GetBehavior<Sender>();
            foreach (var item in players.Players())
            {
               await sender.SendAsync(item, FuncDefine.onInitGame, new StartGame() { color = world.GetComponent<InfoComponent>(item).color});
            }
        }
    }
}
