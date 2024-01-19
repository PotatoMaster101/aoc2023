namespace AoC2023.Day05;

/// <summary>
/// An entry in the mapper.
/// </summary>
public class MapperEntry
{
    /// <summary>
    /// Gets the destination range start.
    /// </summary>
    public long Destination { get; }

    /// <summary>
    /// Gets the source range start.
    /// </summary>
    public long Source { get; }

    /// <summary>
    /// Gets the range length.
    /// </summary>
    public long Length { get; }

    /// <summary>
    /// Constructs a new instance of <see cref="MapperEntry"/>.
    /// </summary>
    /// <param name="src">The source range start.</param>
    /// <param name="dst">The destination range start.</param>
    /// <param name="len">The range length.</param>
    public MapperEntry(long src, long dst, long len)
    {
        Source = src;
        Destination = dst;
        Length = len;
    }

    /// <summary>
    /// Determines whether a value is in the source range.
    /// </summary>
    /// <param name="num">The value to check.</param>
    /// <returns>Whether the value is in the source range.</returns>
    public bool InSourceRange(long num)
    {
        return num >= Source && num <= Source + Length - 1;
    }

    /// <summary>
    /// Determines whether a value is in the destination range.
    /// </summary>
    /// <param name="num">The value to check.</param>
    /// <returns>Whether the value is in the destination range.</returns>
    public bool InDestinationRange(long num)
    {
        return num >= Destination && num <= Destination + Length - 1;
    }

    /// <summary>
    /// Gets the mapped value.
    /// </summary>
    /// <param name="num">The value to map.</param>
    /// <returns>The mapped value.</returns>
    public long Map(long num)
    {
        return InSourceRange(num) ? Destination + (num - Source) : num;
    }

    /// <summary>
    /// Gets the original value using a mapped value.
    /// </summary>
    /// <param name="num">The mapped value.</param>
    /// <returns>The original value.</returns>
    public long Unmap(long num)
    {
        return InDestinationRange(num) ? Source + (num - Destination) : num;
    }
}
