using System;
using System.Reflection;
namespace Server
{
    public static class ReflectLoader
    {
        public static void LoadByParent<Base>(Type type, Action<Base> onAdd)
        {
            foreach (var item in type.Assembly.GetTypes())
            {
                if (typeof(Base).IsInterface)
                {
                    if (!typeof(Base).IsAssignableFrom(item)) continue;
                }
                else
                {
                    if (!isExpend(item, typeof(Base))) continue;
                }
                if (item.GetCustomAttribute<NotReflectAttribute>(true) != null) continue;
                if (item.IsAbstract) continue;
                onAdd((Base)Activator.CreateInstance(item));
            }
        }
        public static void LoadByAttribute<Parent, T>(Type type, Action<Parent, T> onAdd) where T : Attribute
        {
            foreach (var item in type.Assembly.GetTypes())
            {
                if (item.IsAbstract) continue;
                if (typeof(Parent).IsInterface)
                {
                    if (!typeof(Parent).IsAssignableFrom(item)) continue;
                }
                else
                {
                    if (!isExpend(item, typeof(Parent))) continue;
                }
                var ats = item.GetCustomAttributes(false);
                if (ats.Length > 0 && ats[0] is T)
                {
                    onAdd((Parent)Activator.CreateInstance(item), ats[0] as T);
                }
            }
        }
        public static bool isExpend(Type cur, Type father)
        {
            Type type = cur.BaseType;
            while (type != null)
            {
                if (type == father)
                {
                    return true;
                }
                type = type.BaseType;
            }
            return false;
        }
    }
}
