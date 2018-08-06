using Server;
namespace MServer
{
    class InputPull:Behavior
    {
        public FrameMessage Run(uint[] players,uint frame)
        {
            FrameMessage message = new FrameMessage() { frame = frame };
            foreach (var item in players)
            {
                var input = world.GetComponent<InputComponenet>(item);
                if (input != null)
                {
                    message.paylods.Add(input.data);
                    input.data = null;
                }
            }
            return message;
        }
    }
}
