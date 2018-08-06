namespace Server.Physic
{
    struct DragTuple : ITuple
    {
        public ParticleComponent Particle;
        public DragComponent Drag;
        public void SetCmps(Entity entity)
        {
            Particle = entity.GetComponent<ParticleComponent>();
            Drag = entity.GetComponent<DragComponent>();
        }
    }
}
