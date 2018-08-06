namespace Server.Physic
{
    public struct Vec3
    {
        public static Vec3 zero { get { return new Vec3(); } }
        public static Vec3 one { get { return new Vec3(1,1,1); } }
        public static Vec3 up { get { return new Vec3(0, 1,0); } }
        public static Vec3 down { get { return new Vec3(0, -1, 0); } }
        public static Vec3 left { get { return new Vec3(-1, 0, 0); } }
        public static Vec3 right { get { return new Vec3(1, 0, 0); } }
        public static Vec3 forward { get { return new Vec3(0, 0, 1); } }
        public static Vec3 back { get { return new Vec3(0, 0, -1); } }
        public float x;
        public float y;
        public float z;
        public Vec3(float x,float y,float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public Vec3 normalized
        {
            get
            {
                if (magnitude == 0) return new Vec3();
                return this * (1 / magnitude);
            }
        }
        public float magnitude
        {
            get
            {
                return mathf.sqrt(Dot(this, this));
            }
        }
        public float magnitudeSqure { get { return Dot(this, this); } }
        public Vec3 Normalize()
        {
            return this = normalized; 
        }
        public static Vec3 operator +(Vec3 a, Vec3 b)
        {
            a.x += b.x;
            a.y += b.y;
            a.z += b.z;
            return a;
        }
        public static Vec3 operator -(Vec3 a)
        {
            a.x = -a.x;
            a.y = -a.y;
            a.z = -a.z;
            return a;
        }
        public static Vec3 operator -(Vec3 a, Vec3 b)
        {
            a.x -= b.x;
            a.y -= b.y;
            a.z -= b.z;
            return a;
        }
        public static Vec3 operator *(Vec3 a, Vec3 b)
        {
            a.x *= b.x;
            a.y *= b.y;
            a.z *= b.z;
            return a;
        }
        public static Vec3 operator *(Vec3 a, float b)
        {
            a.x *= b;
            a.y *= b;
            a.z *= b;
            return a;
        }
        public static Vec3 operator *(float b,Vec3 a)
        {
            a.x *= b;
            a.y *= b;
            a.z *= b;
            return a;
        }
        public static Vec3 operator %(Vec3 a, Vec3 b)
        {
            return Cross(a, b);
        }
        public static float Dot(Vec3 a,Vec3 b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }
        public static Vec3 Cross(Vec3 a,Vec3 b)
        {
            return new Vec3(a.y * b.z - a.z * b.y,
                            a.z * b.x - a.x * b.z,
                            a.x * b.y - a.y * b.x);
        }
    }
}
