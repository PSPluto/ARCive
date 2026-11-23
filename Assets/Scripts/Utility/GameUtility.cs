public static class GameUtility
{
    /// <summary>
    /// 値が範囲内かを調べる
    /// </summary>
    /// <param name="value">値</param>
    /// <param name="min">以上</param>
    /// <param name="max">以下</param>
    /// <returns></returns>
    public static bool IsInRange(int value, int min, int max)
    {
        return (value >= min && value <= max);
    }
}