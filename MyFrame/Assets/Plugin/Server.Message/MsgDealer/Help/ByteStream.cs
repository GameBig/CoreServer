using System;
using System.Text;
using System.Collections.Concurrent;
namespace Server.Message
{
    class ByteStream :IByteStream
    {
        private static readonly ConcurrentStack<ByteStream> stack = new ConcurrentStack<ByteStream>();
        public static ByteStream Take()
        {
            ByteStream stream;
            if(!stack.TryPop(out stream))
            {
                stream = new ByteStream();
            }
            return stream;
        }
        public static void Return(ByteStream stream)
        {
            stream.Destroy();
            stack.Push(stream);
        }
        public ByteStream(byte[] buffer, int startIndex, int lenght)
        {
            Set(buffer, startIndex, lenght);
        }
        private ByteStream() { }
        private byte[] _buffer;
        public byte[] Buffer
        {
            get { return _buffer; }
        }
        private int index;
        public void Set(byte[] buffer, int startIndex, int lenght)
        {
            index = 0;
            _buffer = unSafeByteHelper.Take(lenght);
            Array.Copy(buffer, startIndex, _buffer, 0, lenght);
        }
        //public void Set(byte[] buffer)
        //{
        //    index = 0;
        //    _buffer = buffer;
        //}
        public bool GetBool(bool peek = false)
        {
            int idx = GetIndex(peek, sizeof(bool));
            if (idx == -1) return false;
            return BitConverter.ToBoolean(_buffer, idx);
        }

        public byte GetByte(bool peek = false)
        {
            int idx = GetIndex(peek, sizeof(byte));
            if (idx == -1) return 0;
            return _buffer[idx];
        }
        public double GetDouble(bool peek = false)
        {
            int idx = GetIndex(peek, sizeof(double));
            if (idx == -1) return 0;
            return BitConverter.ToDouble(_buffer, idx);
        }

        public float GetFloat(bool peek = false)
        {
            int idx = GetIndex(peek, sizeof(float));
            if (idx == -1) return 0;
            return BitConverter.ToSingle(_buffer, idx);
        }

        public int GetInt(bool peek = false)
        {
            int idx = GetIndex(peek, sizeof(int));
            if (idx == -1) return 0;
            return BitConverter.ToInt32(_buffer, idx);
        }

        public long GetLong(bool peek = false)
        {
            int idx = GetIndex(peek, sizeof(long));
            if (idx == -1) return 0;
            return BitConverter.ToInt64(_buffer, idx);
        }
        public short GetShort(bool peek = false)
        {
            int idx = GetIndex(peek, sizeof(short));
            if (idx == -1) return 0;
            return BitConverter.ToInt16(_buffer, idx);
        }

        public uint GetUInt(bool peek = false)
        {
            int idx = GetIndex(peek, sizeof(uint));
            if (idx == -1) return 0;
            return BitConverter.ToUInt32(_buffer, idx);
        }

        public ulong GetULong(bool peek = false)
        {
            int idx = GetIndex(peek, sizeof(ulong));
            if (idx == -1) return 0;
            return BitConverter.ToUInt64(_buffer, idx);
        }

        public ushort GetUShort(bool peek = false)
        {
            int idx = GetIndex(peek, sizeof(ushort));
            if (idx == -1) return 0;
            return BitConverter.ToUInt16(_buffer, idx);
        }

        public void MoveIndex(int length)
        {
            index += length;
        }
        public bool HasMore()
        {
            return index <= _buffer.Length - 1;
        }
        internal int GetIndex(bool peek, int size)
        {
            int idx = -1;
            if (Enough(size))
            {
                idx = index;
                _MoveIndex(peek, size);
            }
            else
            {
                throw new ArgumentException(string.Format( "you want to convert basic value type error ,stream lastLenght = {0} ,this typeLenght = {1} ",_buffer.Length - index, size));
            }
            return idx;
        }
        //public string GetStringToEnd(bool peek = false)
        //{
        //    return _GetString(_buffer.Length - index,peek);
        //}
        public string GetString(bool peek = false)
        {
            int len = GetUShort();
            return _GetString(len);
        }
        private string _GetString(int length, bool peek = false)
        {
            int idx = GetIndex(peek, length);
            if (idx == -1) return string.Empty;
            return Encoding.Default.GetString(_buffer, idx, length);
        }
        private void _MoveIndex(bool peek,int lenght)
        {
            if (!peek)
            {
                MoveIndex(lenght);
            }
        }

        public bool Enough(int count)
        {
            return !(index + count > _buffer.Length);
        }

        public void Destroy()
        {
            index = 0;
            if (this._buffer == null) return;
            unSafeByteHelper.Return(this._buffer);
            this._buffer = null;
        }
    }
}
