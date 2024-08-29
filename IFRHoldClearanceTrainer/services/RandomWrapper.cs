namespace IFRHoldClearanceTrainer.services;

public interface IRandom
{
    public int Next();
    public int Next(int max);
    public int Next(int min, int max);
}

public class RandomWrapper : IRandom
{
    private Random random;

    public RandomWrapper()
    {
        random = new Random();
    }
    public RandomWrapper(int seed)
    {
        random = new Random(seed);
    }

    public int Next()
    {
        return random.Next();
    }

    public int Next(int max)
    {
        return random.Next(max);
    }

    public int Next(int min, int max)
    {
        return random.Next(min, max);
    }
}