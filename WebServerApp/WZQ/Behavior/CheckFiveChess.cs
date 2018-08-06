using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;
namespace ServerApp
{
    class CheckFiveChess: Behavior
    {
        public bool Run(ChessComponent chess)
        {
            var toIndex = world.GetBehavior<PosToIndex>();
            for (int x = 0; x < chess.size-5; x++)
            {
                for (int y = 0; y< chess.size-5; y++)
                {
                    if (isFiveNDig(chess, x, y))
                    {
                        Console.WriteLine("isFiveNDig");
                        return true;
                    }
                    if (chess[x, y] == 0) continue;
                    if (isFiveH(chess, x, y))
                    {
                        Console.WriteLine("isFiveH");
                        return true;
                    }
                    if (isFiveV(chess, x, y))
                    {
                        Console.WriteLine("isFiveV");
                        return true;
                    }
                    if (isFiveDig(chess, x, y))
                    {
                        Console.WriteLine("isFiveDig");
                        return true;
                    }
                }
            }
            return false;
        }
        public bool isFiveH(ChessComponent chess,int x,int y)
        {
            if (x + 5 >= chess.size) return false;
            for (int i = 0; i < 4; i++)
            {
                if (chess[x+i, y] != chess[x+i + 1, y])
                    return false;
            }
            return true;
        }
        public bool isFiveV(ChessComponent chess, int x, int y)
        {
            if (y + 5 >= chess.size) return false;
            for (int i = 0; i < 4; i++)
            {
                if (chess[x, y+i] != chess[x,y+i+1])
                    return false;
            }
            return true;
        }
        public bool isFiveDig(ChessComponent chess, int x, int y)
        {
            if (x + 5 >= chess.size) return false;
            if (y + 5 >= chess.size) return false;
            for (int i = 0; i < 4; i++)
            {
                if (chess[x+ i, y + i] != chess[x + i + 1, y + i + 1])
                    return false;
            }
            return true;
        }
        public bool isFiveNDig(ChessComponent chess, int x, int y)
        {
            if (x + 5 >= chess.size) return false;
            if (y + 5 >= chess.size) return false;
            for (int i = 0; i <4; i++)
            {
                //Console.WriteLine($"Cur::({x + 4 - i},{ y + i})={chess[x + 4 - i, y + i]},Nx::({x + 4 - i - 1},{ y + i + 1})={chess[x + 4 - i - 1, y + i + 1]}");
                if (chess[x + 4 - i, y + i] == 0) return false;
                if (chess[x + 4 - i, y + i] != chess[x +4- i - 1, y + i + 1])
                    return false;
            }
            return true;
        }
    }
}
