namespace Server
{
    public class RandomBhr: Behavior
    {
        public int random()
        {
            var g= world.Globel.GetComponent<RandomComponent>();
            return g.random.Next();
        }
        public int Range(int min,int max)
        {
            var g = world.Globel.GetComponent<RandomComponent>();
            return g.random.Next(min, max);
        }
    }
}
