using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class World:IUpdate
    {
        private static Dictionary<string, World> worlds = new Dictionary<string, World>();
        public static World Active { get; private set; }
        static World()
        {
            Active = Create("Default");
        }
        public static World Create(string name)
        {
            if (worlds.ContainsKey(name))
                return worlds[name];
            World world = new World();
            world.name = name;
            worlds.Add(name, world);
            return world;
        }
        private string name;
        private Dictionary<string, Behavior> behs = new Dictionary<string, Behavior>();
        private List<IUpdate> updates = new List<IUpdate>();
        private Dictionary<uint, Entity> ets = new Dictionary<uint, Entity>();
        public Entity Globel { get; private set; }
        private uint baseID;
        private uint GenID()
        {
            return baseID++;
        }
        public World()
        {
            Globel = new Entity();
            Globel.Init(0, this);
        }
        public T Add<T>() where T : Entity, new()
        {
            T e = new T();
            e.Init(GenID(), this);
            ets.Add(e.id, e);
            e.Start();
            return e;
        }
        public T Get<T>(uint id)where T : Entity
        {
            Entity e;
            ets.TryGetValue(id, out e);
            return e as T;
        }
        public void RemoveEntity(uint id)
        {
            Get<Entity>(id)?.Destroy();
            ets.Remove(id);
        }
        public T[] GetComponents<T>()where T : Component
        {
            List<T> list = new List<T>();
            foreach (var item in this.ets.Values.ToArray())
            {
                T cmp = item.GetComponent<T>();
                if (cmp != null)
                    list.Add(cmp);
            }
            return list.ToArray();
        }
       
        internal void AddBehever(Behavior behavior)
        {
            string key = behavior.GetType().Name;
            if (!behs.ContainsKey(key))
            {
                behavior.SetWorld(this);
                behs.Add(key, behavior);
                IUpdate update = behavior as IUpdate;
                if (update != null) updates.Add(update);
            }
        }
        public T GetBehavior<T>(string name = null) where T : Behavior
        {
            string key = name;
            if (key == null) key = typeof(T).Name;
            Behavior behavior;
            behs.TryGetValue(key, out behavior);
            return behavior as T;
        }
        public void Update(float tick)
        {
            for (int i = 0; i < updates.Count; i++)
            {
                updates[i].Update(tick);
            }
        }
        public Entity[] Entities()
        {
            return ets.Values.ToArray();
        }
        public void Destroy()
        {
            Globel.Destroy();
            foreach (var item in ets.Values.ToArray())
            {
                item.Destroy();
            }
            ets.Clear();
            behs.Clear();
            worlds.Remove(name);
        }
    }
}
