namespace AoC2023.Day06;

/// <summary>
/// Represents a boat race.
/// </summary>
public readonly struct Race
{
    /// <summary>
    /// Gets the maximum time allowed for this race.
    /// </summary>
    public long Time { get; }

    /// <summary>
    /// Gets the highest recorded distance for this race.
    /// </summary>
    public long RecordDistance { get; }

    /// <summary>
    /// Constructs a new instance of <see cref="Race"/>.
    /// </summary>
    /// <param name="time">The maximum time allowed for this race.</param>
    /// <param name="recordDistance">The highest recorded distance for this race.</param>
    public Race(long time, long recordDistance)
    {
        Time = time;
        RecordDistance = recordDistance;
    }

    /// <summary>
    /// Counts the number of times the race can be won.
    /// </summary>
    /// <returns>The number of times the race can be won.</returns>
    public long CountWinnings()
    {
        var firstWin = GetFirstWin();
        var lastWin = Time - firstWin;
        return lastWin - firstWin + 1;
    }

    /// <summary>
    /// Gets the first winning button pressing time.
    /// </summary>
    /// <returns>The first winning button pressing time.</returns>
    private long GetFirstWin()
    {
        for (long i = 1; i < Time; i++)
        {
            var dist = i * (Time - i);
            if (dist > RecordDistance)
                return i;
        }
        return 0;
    }
}
