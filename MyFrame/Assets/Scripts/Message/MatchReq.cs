using Server.Message;
public class MatchReq : Paylod<MatchReq>
{
    public override ushort Type => 0;
    public int mapType;
    public override byte[] ToBytes()
    {
        return mapType.ToBytes();
    }
    public override void GetFrom(IByteStream stream)
    {
        mapType = stream.GetInt();
    }
}
