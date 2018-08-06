using Server.Message;
class InputMessage : Paylod<InputMessage>
{
    public override ushort Type => 5;
    public uint Id;
    public Vec2Int MoveDir;
    public bool fire;
    public Vec2Int FireDir;
}

