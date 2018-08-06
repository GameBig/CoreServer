using System;
using Server.Message;
namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            PaylodList<AA> dd = new PaylodList<AA>();
            dd.ToBytes();
            Console.Read();
        }
    }
    class AA : Paylod<AA>
    {
        public override ushort Type => 1;
    }
}
