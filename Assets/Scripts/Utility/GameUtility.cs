public static class GameUtility
{
    public static bool IsBetweenInclusive(int value, int min, int max)
    {
        return (value >= min && value <= max);
    }
}