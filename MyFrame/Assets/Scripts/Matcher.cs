using UnityEngine;
using UnityEngine.UI;
using Server.Message;
public class Matcher : SMono
{
    private Button button;
    private Text buttonText;
    public GameObject waiting;
    private bool matching;
    protected override void Start()
    {
        base.Start();
        button = GetComponent<Button>();
        buttonText = button.GetComponentInChildren<Text>();
        var sender = world.GetBehavior<UdpSender>();
        button.onClick.AddListener(() =>
        {
            if (!matching)
                sender.Send("game", new MatchReq());
            else
                sender.Send("game", new MachingCancel());
            
        });
     
        world.GetBehavior<EventSender>().Regist("matching", onMatching);
        world.GetBehavior<EventSender>().Regist("matchingcancel", onMatchingCancel);
    }
    private void onMatching()
    {
        waiting.SetActive(true);
        buttonText.text = "取消匹配";
        matching = true;
    }
    private void onMatchingCancel()
    {
        matching = false;
        waiting.SetActive(false);
        buttonText.text = "开始匹配";
    }
    public void Open(bool open)
    {
        waiting.SetActive(open);
        button.gameObject.SetActive(open);
    }
}
