using Server;
namespace ServerApp
{
    class ChessComponent:Component
    {
        public int size;
        public byte[] chess;
        protected override void Create()
        {
            size = 15;
            chess = new byte[size*size];
        }
        public int ToIndex(int x, int y)
        {
            return x +size * y;
        }
        public int[] ToPos( int index)
        {
            return new int[] { index %size, index /size };
        }
        public byte this[int x,int y]
        {
            get
            {
                return chess[ToIndex(x, y)];
            }
            set
            {
                chess[ToIndex(x, y)] = value;
            }
        }
        public byte this[int index]
        {
            get
            {
                return chess[index];
            }
            set
            {
                chess[index] = value;
            }
        }
        public void Clear()
        {
            for (int i = 0; i < chess.Length; i++)
            {
                chess[i] = 0;
            }
        }
    }
}
