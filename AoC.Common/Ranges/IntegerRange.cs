using System.Collections;

namespace AoC.Common.Ranges;

/// <summary>
/// Represents a range of integers.
/// </summary>
public readonly record struct IntegerRange : IEnumerable<long>
{
    /// <summary>
    /// Gets the range's integer count.
    /// </summary>
    public long Count => Max - Min + 1;

    /// <summary>
    /// Gets the range's increment amount.
    /// </summary>
    public long Increment { get; }

    /// <summary>
    /// Gets the range's maximum integer.
    /// </summary>
    public long Max { get; }

    /// <summary>
    /// Gets the range's minimum integer.
    /// </summary>
    public long Min { get; }

    /// <summary>
    /// Constructs a new instance of <see cref="IntegerRange"/>.
    /// </summary>
    /// <param name="min">The minimum integer.</param>
    /// <param name="max">The maximum integer.</param>
    /// <param name="increment">The increment amount.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="min"/> is greater than <paramref name="max"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="increment"/> is less than or equal to 0.</exception>
    public IntegerRange(long min, long max, long increment = 1)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThan(min, max);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(increment, 0);

        Increment = increment;
        Min = min;
        Max = max;
    }

    /// <summary>
    /// Deconstructs this range.
    /// </summary>
    /// <param name="min">The minimum integer.</param>
    /// <param name="max">The maximum integer.</param>
    public void Deconstruct(out long min, out long max)
    {
        min = Min;
        max = Max;
    }

    /// <inheritdoc cref="IEnumerable{T}.GetEnumerator"/>
    public IEnumerator<long> GetEnumerator()
    {
        for (var i = Min; i <= Max; i += Increment)
            yield return i;
    }

    /// <inheritdoc cref="IEnumerable.GetEnumerator"/>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>
    /// Checks whether an integer is in the range.
    /// </summary>
    /// <param name="integer">The integer to check.</param>
    /// <returns>Whether the integer is in the range.</returns>
    public bool InRange(long integer)
    {
        return integer >= Min && integer <= Max;
    }

    /// <summary>
    /// Splits this range and returns the lower range.
    /// </summary>
    /// <param name="midpoint">The midpoint to split the range.</param>
    /// <param name="includeMidpoint">Whether to include the midpoint.</param>
    /// <returns>The split lower range.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="midpoint"/> is not between <see cref="Min"/> and <see cref="Max"/>.</exception>
    public IntegerRange SplitLowerRange(long midpoint, bool includeMidpoint = false)
    {
        if (midpoint <= Min || midpoint >= Max)
            throw new ArgumentOutOfRangeException(nameof(midpoint));

        var max = includeMidpoint ? midpoint : midpoint - 1;
        return new IntegerRange(Min, max);
    }

    /// <summary>
    /// Splits this range and returns the upper range.
    /// </summary>
    /// <param name="midpoint">The midpoint to split the range.</param>
    /// <param name="includeMidpoint">Whether to include the midpoint.</param>
    /// <returns>The split upper range.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="midpoint"/> is not between <see cref="Min"/> and <see cref="Max"/>.</exception>
    public IntegerRange SplitUpperRange(long midpoint, bool includeMidpoint = false)
    {
        if (midpoint <= Min || midpoint >= Max)
            throw new ArgumentOutOfRangeException(nameof(midpoint));

        var min = includeMidpoint ? midpoint : midpoint + 1;
        return new IntegerRange(min, Max);
    }

    /// <summary>
    /// Tries to get the next value.
    /// </summary>
    /// <param name="current">The current value.</param>
    /// <param name="next">The next value.</param>
    /// <returns>Whether the next value is in the range.</returns>
    public bool TryGetNext(long current, out long next)
    {
        next = current + Increment;
        return next >= Min && next <= Max;
    }

    /// <summary>
    /// Tries to get the previous value.
    /// </summary>
    /// <param name="current">The current value.</param>
    /// <param name="previous">The previous value.</param>
    /// <returns>Whether the previous value is in the range.</returns>
    public bool TryGetPrevious(long current, out long previous)
    {
        previous = current - Increment;
        return previous >= Min && previous <= Max;
    }
}
