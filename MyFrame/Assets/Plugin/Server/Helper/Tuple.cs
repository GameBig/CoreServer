using System;
using System.Collections.Generic;
using System.Reflection;
namespace Server
{
    public interface ITuple
    {
        void SetCmps(Entity  entity);
    }
    internal static class TupleTypeMgr
    {
        private static Dictionary<string, string[]> values = new Dictionary<string, string[]>();
        public static void Load(Type type)
        {
            foreach (var item in type.Assembly.GetTypes())
            {
                if (item.IsAbstract) continue;
                if (!typeof(ITuple).IsAssignableFrom(item)) continue;
                var mes = new List<string>();
               
                foreach (var mem in item.GetFields(BindingFlags.Public | BindingFlags.Instance))
                {
                    mes.Add(mem.FieldType.Name);
                }
                values.Add(item.Name, mes.ToArray());
            }
        }
        public static string[] Get(string type)
        {
            return values[type];
        }
    }
    public static class WorldTupleEx
    {
        public static T GetTuple<T>(this World world,uint id) where T : ITuple, new()
        {
            string[] fileds = TupleTypeMgr.Get(typeof(T).Name);
            Entity e = world.Get<Entity>(id);
            if (e != null&&e.HasComponents(fileds))
            {
                T s = new T();
                s.SetCmps(e);
                return s;
            }
            return default(T);
        }
        public static T[] GetTuples<T>(this World world)where T : ITuple, new ()
        {
            List<T> a = new List<T>();
            string[] fileds = TupleTypeMgr.Get(typeof(T).Name);
            foreach (var item in world.Entities())
            {
                if (item.HasComponents(fileds))
                {
                    T aaa = new T();
                    aaa.SetCmps(item);
                    a.Add(aaa);
                }
            }
            return a.ToArray();
        }
    }
}
