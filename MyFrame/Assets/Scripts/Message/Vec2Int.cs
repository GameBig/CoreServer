using Server.Message;
public struct Vec2Int : IToBytes
{
    public int x { get; set; }
    public int y { get; set; }
    public Vec2Int(int x,int y)
    {
        this.x = x;this.y = y;
    }
    public static Vec2Int operator +(Vec2Int a, Vec2Int b)
    {
        a.x += b.x;
        a.y += b.y;
        return a;
    }
    public static Vec2Int operator -(Vec2Int a)
    {
        a.x = -a.x;
        a.y = -a.y;
        return a;
    }
    public static Vec2Int operator *(Vec2Int a, int b)
    {
        a.x = b * a.x;
        a.y = b * a.y;
        return a;
    }
    public static Vec2Int operator *(int b,Vec2Int a)
    {
        a.x = b * a.x;
        a.y = b * a.y;
        return a;
    }
    public void GetFrom(IByteStream stream)
    {
        x = stream.GetInt();
        y = stream.GetInt();
    }
    public byte[] ToBytes()
    {
        return x.ToBytes().Add(y.ToBytes());
    }
}

