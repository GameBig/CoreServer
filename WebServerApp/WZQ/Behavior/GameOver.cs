using Server;
namespace ServerApp
{
    class GameOver: Behavior
    {
        public async void Run(PlayersComponent players,int win)
        {
            players.entity.RemoveComponent<TimeCounterComponent>();
            foreach (var item in players.Players())
            {
                world.GetComponent<InfoComponent>(item)?.Reset();
            }
            players.Slibing<ChessComponent>().Clear();
            await world.GetBehavior<Sender>().NotifyAsync(players.Players(), FuncDefine.onOver, new IdData { id = players[win] });
            players.Clear();
        }
    }
}
