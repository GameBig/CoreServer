using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Entity
    {
        private bool _isDestroyed;
        public bool isDestroyed { get { return _isDestroyed; } }
        public uint id { get; private set; }
        public World world { get; private set; }
        private Dictionary<string, Component> cmps = new Dictionary<string, Component>();
        public T AddComponent<T>() where T : Component, new()
        {
            string key = typeof(T).Name;
            if (cmps.ContainsKey(key))
            {
                return cmps[key] as T;
            }
            T component = new T();
            component.SetEntity(this);
            cmps.Add(key, component);
            return component;
        }
        internal void AddComponent(Component component)
        {
            string key = component.GetType().Name;
            if (cmps.ContainsKey(key))
                throw new Exception($"all ready ha this key={key}");
            component.SetEntity(this);
            cmps.Add(key, component);
        }
        public T GetComponent<T>() where T : Component
        {
            Component component;
            cmps.TryGetValue(typeof(T).Name, out component);
            return component as T;
        }
       
        internal void Init(uint id, World world)
        {
            this.id = id;
            this.world = world;
            _isDestroyed = false;
        }

        public void RemoveComponent<T>() where T : Component
        {
            T cmp = GetComponent<T>();
            if (cmp != null)
            {
                cmp.Destroy();
                cmps.Remove(typeof(T).Name);
            }
        }

        public void Destroy()
        {
            if (_isDestroyed) return;
            foreach (var item in cmps.Values)
            {
                item.Destroy();
            }
            cmps.Clear();
            this.id = 0;
            this.world = null;
            _isDestroyed = true;
        }
        internal Component GetComponent(string cmp)
        {
            Component component;
            cmps.TryGetValue(cmp, out component);
            return component;
        }
        public bool HasComponents(string[] types)
        {
            foreach (var item in types)
            {
                if (!cmps.ContainsKey(item))
                    return false;
            }
            return true;
        }
        public virtual void Start() { }
    }
}
