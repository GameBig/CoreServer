using System;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    public static class WorldEx
    {
        public static T GetComponent<T>(this World world,uint id) where T : Component
        {
            Entity e = world.Get<Entity>(id);
            if (e == null) return null;
            return e.GetComponent<T>();
        }
        public static void Run(this World world,int RunTick = 20,CancellationToken token=default(CancellationToken))
        {
            long previous = DateTime.Now.Ticks / 10000;
            long addTime = 0;
            while (true)
            {
                try
                {
                    if (token.IsCancellationRequested) return;
                    long current = DateTime.Now.Ticks / 10000;
                    long jg = current - previous;
                    previous = current;
                    addTime += jg;
                    while (addTime >= RunTick)
                    {
                        world.Update((float)RunTick / 1000);
                        addTime -= RunTick;
                    }
                    Thread.Sleep(RunTick - (int)addTime);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error::" + e);
                }
            }
        }
        public static async void RunAsync(this World world, int RunTick = 20, CancellationToken token = default(CancellationToken))
        {
            await Task.Factory.StartNew(o => Run(world, RunTick, token), token, TaskCreationOptions.LongRunning);
        }
    }
}
