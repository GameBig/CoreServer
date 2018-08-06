using UnityEngine;
using Server;
using Server.Message;
public class Client : MonoBehaviour
{
    public uint client { get; private set; }
    public World world { get; private set; }
    async void Awake()
    {
        world = World.Active;
        DontDestroyOnLoad(gameObject);
        WorldInitialer.Load(world, typeof(World));
        WorldInitialer.Load(world, typeof(Client));
        WorldInitialer.Load(world, typeof(UdpInit));
        world.GetBehavior<UdpInit>().Load(typeof(Client)).Init();
        world.Globel.GetComponent<ConnectorsComponent>().AsClient();
        world.GetBehavior<UdpRecever>().Run();
        var send = world.GetBehavior<UdpSender>();
        client =await send.Connect("192.168.2.108", 10000);
        Debug.Log("connected");
        //send.Send(cid, new MatchReq());
    }
    // Update is called once per frame
    void Update()
    {
        world.Update(Time.deltaTime);
    }
    private async void OnDestroy()
    {
        await world.GetBehavior<UdpSender>().DisConnect(client);
        Debug.Log("disconnecten");
    }
}
