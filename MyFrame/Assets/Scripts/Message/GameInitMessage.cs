using Server.Message;
using System.Collections.Generic;
class GameInitMessage : Paylod<GameInitMessage>
{
    public override ushort Type => 3;
    public int seed;
    public List<uint> pid = new List<uint>();
    public override byte[] ToBytes()
    {
        byte[] data = seed.ToBytes().Add(pid.Count.ToBytes());
        for (int i = 0; i < pid.Count; i++)
        {
            data = data.Add(pid[i].ToBytes());
        }
        return data;
    }
    public override void GetFrom(IByteStream stream)
    {
        seed = stream.GetInt();
        int count = stream.GetInt();
        for (int i = 0; i < count; i++)
        {
            pid.Add(stream.GetUInt());
        }
    }
}

