namespace Server
{
    public static class Success
    {
        public const int code = 200;
    }
    public class SenderMessage
    {
        public int code = Success.code;
    }
    public interface IRecvMessage { }
}
