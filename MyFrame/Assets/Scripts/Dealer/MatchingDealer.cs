using Server.Message;
class MatchingDealer : MsgDealer<Matching>
{
    protected override void Handle(uint client, Matching msg)
    {
        world.GetBehavior<EventSender>().Invoke("matching");
    }
}
