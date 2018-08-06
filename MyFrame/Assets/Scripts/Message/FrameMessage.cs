using Server.Message;
class FrameMessage : Paylod<FrameMessage>
{
    public override ushort Type => 6;
    public uint frame;
    public PaylodList<InputMessage> paylods = new PaylodList<InputMessage>();
    public override byte[] ToBytes()
    {
        return frame.ToBytes().Add(paylods.ToBytes());
    }
    public override void GetFrom(IByteStream stream)
    {
        frame = stream.GetUInt();
        paylods.GetFrom(stream);
    }
}

