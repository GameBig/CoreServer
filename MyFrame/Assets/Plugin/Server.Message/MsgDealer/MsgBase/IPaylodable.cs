namespace Server.Message
{
    public interface IPaylodable : IToBytes
    {
        ushort Type { get; }
        IPaylodable Clone();
    }
}
