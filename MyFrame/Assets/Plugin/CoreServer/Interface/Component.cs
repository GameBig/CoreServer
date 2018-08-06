namespace Server
{
    public class Component
    {
        public Entity entity { get; private set; }
        protected World world { get { return entity.world; } }
        internal void SetEntity(Entity entity)
        {
            this.entity = entity;
            Create();
        }
        protected virtual void Create() { }
        public virtual void Destroy() { }
        public T Slibing<T>()where T : Component
        {
            return entity.GetComponent<T>();
        }
    }
}
