using System;
using WebSocketSharp;
using WebSocketSharp.Server;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.IO;
using Server;
namespace ServerApp
{
    public class GameSession: WebSocketBehavior,ISession
    {
        private World world;
        public uint eid { get; private set; }
        protected override void OnMessage(MessageEventArgs e)
        {
            try
            {
                Log.Debug("OnMessage::" + e.Data);
                JObject job = JsonConvert.DeserializeObject<JObject>(e.Data);
                if (job.TryGetValue("func", out JToken s))
                {
                    var func = s.ToObject<string>();
                    var dealer = world.GetBehavior<ADealer>(func);
                    if (dealer == null)
                    {
                        Log.Warn("Can nor deal this func::" + func);
                        return;
                    }
                    JObject data = null;
                    if (job.TryGetValue("data", out JToken d))
                        data = d.ToObject<JObject>();
                    dealer.Handle(this, data);
                }
                else
                {
                    Log.Warn("Message Format Error");
                }
            }
            catch (Exception a)
            {
                Log.Debug(a.Message);
            }
        }
        protected override void OnOpen()
        {
            Entity client = world.Add<Entity>();
            client.AddComponent<SessionComponent>().SetSession(this);
            eid = client.id;
            Log.Level = LogLevel.Debug;
            Log.Debug("OnOpen");
        }
        protected override void OnClose(CloseEventArgs e)
        {
            Log.Debug("OnClose");
            world.RemoveEntity(eid);
            this.eid = 0;
        }
        protected override void OnError(WebSocketSharp.ErrorEventArgs e)
        {
            Log.Error("OnError::"+e.Message);
        }
        public Task SendAsync(FileInfo file)
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            SendAsync(file, (s) => { tcs.SetResult(s);});
            return tcs.Task;
        }
        public Task SendAsync(Stream stream, int length)
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            SendAsync(stream, length,(s) => { tcs.SetResult(s); });
            return tcs.Task;
        }
        protected Task SendAsync(string data)
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            SendAsync(data, (s) => { tcs.SetResult(s); });
            Log.Debug(data);
            return tcs.Task;
        }

        public void Send(string func, SenderMessage data)
        {
            Send(SendData.Convert(func, data));
        }
        public async Task SendAsync(string func, SenderMessage data)
        {
            await SendAsync(SendData.Convert(func, data));
        }

        public void SetWorld(World world)
        {
            this.world = world;
        }
    }
}
