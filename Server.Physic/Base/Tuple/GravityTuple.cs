using System;
namespace Server.Physic
{
    struct GravityTuple : ITuple
    {
        public GravityComponent gravity;
        public ParticleComponent particle;
        public void SetCmps(Entity entity)
        {
            gravity = entity.GetComponent<GravityComponent>();
            particle = entity.GetComponent<ParticleComponent>();
        }
    }
}
