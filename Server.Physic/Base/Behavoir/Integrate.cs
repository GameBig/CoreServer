namespace Server.Physic
{
    class Integrate : Behavior, IUpdate
    {
        public void Update(float tick)
        {
            foreach (var item in world.GetTuples<IntegrateTuple>())
            {
                item.transform.position += item.particle.velocity * tick;
                Vec3 acc = item.particle.acceleration;
                acc += item.particle.force * item.particle.InverseMass;
                item.particle.velocity += item.particle.acceleration * tick*mathf.pow(item.particle.damping,tick);
                item.particle.ClearForce();
            }
        }
    }
}
