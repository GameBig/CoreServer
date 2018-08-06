namespace Server.Message
{
    public abstract class Paylod<T> : IPaylodable where T : IPaylodable, new()
    {
        public abstract ushort Type { get; }

        public IPaylodable Clone()
        {
            return new T();
        }
        public virtual void GetFrom(IByteStream stream) { }
        public virtual byte[] ToBytes()
        {
            return null;
        }
    }
}
