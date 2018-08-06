namespace Server.Physic
{
    class ParticleDrag : Behavior, IUpdate
    {
        public void Update(float tick)
        {
            foreach (var item in world.GetTuples<DragTuple>())
            {
                float m = item.Particle.velocity.magnitude;
                float drag = item.Drag.K1 * m + item.Drag.K2 * m * m;
                Vec3 f = -drag * item.Particle.velocity.normalized;
                item.Particle.Addforce(f);
            }
        }
    }
}
