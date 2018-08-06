using System.Collections;
using System.Collections.Generic;

namespace Server.Message
{
    public class PaylodList<T>:IToBytes, IEnumerable<T> where T : IToBytes,new()
    {
        private List<T> list = new List<T>();
        public void GetFrom(IByteStream stream)
        {
            int count = stream.GetInt();
            for (int i = 0; i < count; i++)
            {
                T s = new T();
                s.GetFrom(stream);
                list.Add(s);
            }
        }
        public void Add(T item)
        {
            list.Add(item);
        }
        public byte[] ToBytes()
        {
            byte[] d = list.Count.ToBytes();
            foreach (var item in list)
            {
                if (item == null) continue;
                d = d.Add(item.ToBytes());
            }
            return d;
        }
        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
