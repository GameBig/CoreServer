using UnityEngine;

public class InitGame : SMono
{
    public GameObject bg;
    public Matcher matcher;
    protected override void Start()
    {
        base.Start();
        world.GetBehavior<EventSender>().Regist("InitGame", onInitGame);
    }
    
    private void onInitGame()
    {
        bg.SetActive(false);
        matcher.Open(false);
    }
}
