namespace Server.Physic
{
    public class ParticleComponent:Component
    {
        public Vec3 velocity;
        public Vec3 acceleration;
        public float damping = 0.99f;
        public float InverseMass { get { return imass; } }
        public float Mass { get { return imass == 0 ? 0 : 1 / imass; } }
        public Vec3 force;
        public void ClearForce()
        {
            force = Vec3.zero;
        }
        public void Addforce(Vec3 f)
        {
            force += f;
        }
        private float imass;
    }
}
