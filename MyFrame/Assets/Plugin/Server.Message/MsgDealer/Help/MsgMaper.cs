using System;
using System.Collections.Generic;
using System.Reflection;
namespace Server.Message
{
    static class MsgMaper
    {
        private static readonly Dictionary<ushort, IPaylodable> typeMap = new Dictionary<ushort, IPaylodable>();
        private static readonly Dictionary<string, string> bhrmaper = new Dictionary<string, string>();
        public static IPaylodable Get(ushort funcs)
        {
            IPaylodable paylodable;
            if (!typeMap.TryGetValue(funcs, out paylodable))
            {
                return null;
            }
            return paylodable.Clone();
        }
        public static string Get(string type)
        {
            string m;
            bhrmaper.TryGetValue(type, out m);
            return m;
        }
        public static void Load(Type type)
        {
            foreach (var item in type.Assembly.GetTypes())
            {
                if (item.IsAbstract) continue;

                if (typeof(IPaylodable).IsAssignableFrom(item))
                {
                    var cloneable = Activator.CreateInstance(item) as IPaylodable;
                    typeMap.Add(cloneable.Type, cloneable);
                    //Console.WriteLine(item);
                }
                else
                if (ReflectLoader.isExpend(item, typeof(APDealer)) && item.BaseType.IsGenericType)
                {
                    bhrmaper.Add(item.BaseType.GetGenericArguments()[0].Name, item.Name);
                }
            }
        }
    }
}
