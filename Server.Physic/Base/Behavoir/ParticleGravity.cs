namespace Server.Physic
{
    class ParticleGravity : Behavior, IUpdate
    {
        public void Update(float tick)
        {
            foreach (var item in world.GetTuples<GravityTuple>())
            {
                if (item.particle.InverseMass == 0) continue;
                item.particle.Addforce(item.gravity.gravity);
            }
        }
    }
}
