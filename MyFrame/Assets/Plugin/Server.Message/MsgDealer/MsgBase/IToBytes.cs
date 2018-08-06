namespace Server.Message
{
    public interface IToBytes
    {
        byte[] ToBytes();
        void GetFrom(IByteStream stream);
    }
}
