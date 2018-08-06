using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public static class WorldInitialer
    {
        public static void Load(World world, Type type)
        {
            ReflectLoader.LoadByParent<Behavior>(type, (dealer) =>
            {
                world.AddBehever(dealer);
            });
            ReflectLoader.LoadByAttribute<Component, GlobelAttribute>(type, (cmp, att) =>
            {
                world.Globel.AddComponent(cmp);
            });
            TupleTypeMgr.Load(type);
        }
    }
}
