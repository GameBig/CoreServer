using UnityEngine;
using Server;
using Server.Message;
public class ClientWorld : MonoBehaviour
{
    public World world { get; private set; }
    async void Awake()
    {
        world = World.Active;
        DontDestroyOnLoad(gameObject);
        WorldInitialer.Load(world, typeof(ClientWorld));
        //WorldInitialer.Load(world, typeof(UdpInit));
        world.GetBehavior<UdpInit>().Load(typeof(ClientWorld)).Init();
        world.Globel.GetComponent<ConnectorsComponent>().AsClient();
        world.GetBehavior<UdpRecever>().Run();
        var send = world.GetBehavior<UdpSender>();
        if (await send.Connect("game", "127.0.0.1", 10000)==0)
        {
            Debug.Log("connected");
        }
       
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        world.Update(Time.deltaTime);
    }

    private async void OnDestroy()
    {
        await world.GetBehavior<UdpSender>().DisConnect("game");
        Debug.Log("disconnecten");
    }
}
