using Server;
using Server.Message;
namespace Client
{
    [NotReflect]
    class UpdateInput : Behavior, IUpdate
    {
        public void Update(float tick)
        {
            var input = world.Globel.GetComponent<InputComponent>();
            input.add += tick;
            var ran = world.GetBehavior<RandomBhr>();
            if (input.add >= input.sendTime)
            {
                input.add = 0;
                input.sendTime = 1;// ran.Range(5,10);
                input.input.fire = ran.Range(0, 5) == 2;
                input.input.FireDir = new Vec2Int(0, 1000);
                input.input.MoveDir = new Vec2Int(ran.Range(-1000, 1000), ran.Range(-1000, 1000));
                world.GetBehavior<UdpSender>().Send("game", input.input);
            }
        }
    }
}
