using Server;
namespace Client
{
    struct MoveTuple : ITuple
    {
        public MoveComponent move;
        public PositionComponent position;
        public void SetCmps(Entity entity)
        {
            move = entity.GetComponent<MoveComponent>();
            position = entity.GetComponent<PositionComponent>();
        }
    }
}
