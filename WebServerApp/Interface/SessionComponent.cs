namespace Server
{
    public class SessionComponent : Component
    {
        public ISession gameSession { get; private set; }
        public void SetSession(ISession session)
        {
            this.gameSession = session;
        }
    }
}
