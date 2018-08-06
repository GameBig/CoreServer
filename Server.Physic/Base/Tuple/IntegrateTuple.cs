namespace Server.Physic
{
    struct IntegrateTuple : ITuple
    {
        public TransformComponent transform;
        public ParticleComponent particle;
        public void SetCmps(Entity entity)
        {
            transform = entity.GetComponent<TransformComponent>();
            particle = entity.GetComponent<ParticleComponent>();
        }
    }
}
