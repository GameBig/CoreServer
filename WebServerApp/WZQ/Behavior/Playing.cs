using Server;
namespace ServerApp
{
    class Playing:Behavior
    {
        public async void Run(PlayersComponent players)
        {
           
            await world.GetBehavior<Sender>().NotifyAsync(players.Players(), FuncDefine.onPlaying, new IdData() { id = players[players.current] });
        }
    }
}
