using Newtonsoft.Json.Linq;
using System;
using Server;
namespace ServerApp
{
    public class NullMsg : IRecvMessage { }
    public abstract class ADealer : Behavior
    {
        protected string typeName { get; private set; }
        public ADealer()
        {
            typeName = GetType().Name;
        }
        public abstract void Handle(ISession session,JObject msg);
    }
    public abstract class MsgDealer<T> : ADealer where T : IRecvMessage, new()
    {
        
        public override void Handle(ISession session, JObject msg)
        {
            T m = default(T);
            if (msg != null)
            {
                m = msg.ToObject<T>();
                if (m == null) throw new Exception("消息类型转换错误");
            }
            Deal(session, m);
        }
        protected abstract void Deal(ISession session, T msg);
    }
}
