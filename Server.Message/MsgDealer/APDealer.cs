using System;

namespace Server.Message
{
    public abstract class APDealer:Behavior
    {
        public abstract void Deal(uint cient,IPaylodable paylodable);
    }
    public abstract class MsgDealer<T> : APDealer where T : IPaylodable, new()
    {
        public override void Deal(uint client, IPaylodable paylodable)
        {
            T m = (T)paylodable;
            if (m ==null)
            {
                throw new Exception("消息类型转换错误");
            }
            Handle(client, m);
        }
        protected abstract void Handle(uint client, T msg);
    }
}
