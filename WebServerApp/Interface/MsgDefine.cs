using System.Collections.Generic;
using Newtonsoft.Json;
namespace Server
{
    public class SendData
    {
        public static Stack<SendData> queue = new Stack<SendData>();
        public static string Convert(string func, SenderMessage payloda)
        {
            SendData data = new SendData();
            data.func = func;
            data.data = payloda;
            string res= JsonConvert.SerializeObject(data);
            Return(data);
            return res;
        }
        public static SendData Take()
        {
            if (queue.Count > 0)
            {
                return queue.Pop();
            }
            return new SendData();
        }
        public static void Return(SendData data)
        {
            data.func = string.Empty;
            data.data = null;
            queue.Push(data);
        }
        public string func;
        public SenderMessage data;
    }
}
