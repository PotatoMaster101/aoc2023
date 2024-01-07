namespace AoC2023.Day15;

/// <summary>
/// A hashing algorithm.
/// </summary>
public static class Hash
{
    /// <summary>
    /// Runs the hashing algorithm.
    /// </summary>
    /// <returns>The result of hashing algorithm.</returns>
    public static long Run(string content)
    {
        long current = 0;
        foreach (var ch in content)
        {
            current += ch;
            current *= 17;
            current %= 256;
        }
        return current;
    }
}
