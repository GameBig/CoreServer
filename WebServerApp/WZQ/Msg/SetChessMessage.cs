using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;
namespace ServerApp
{
    class SetChessMessage:SenderMessage
    {
        public int index;
        public byte color;
    }
}
