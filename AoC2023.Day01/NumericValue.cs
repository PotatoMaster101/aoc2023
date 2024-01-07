namespace AoC2023.Day01;

/// <summary>
/// A numeric value.
/// </summary>
/// <param name="position">The value position.</param>
/// <param name="value">The numeric value.</param>
public class NumericValue(int position, int value)
{
    /// <summary>
    /// Gets the invalid instance.
    /// </summary>
    public static readonly NumericValue Invalid = new(-1, 0);

    /// <summary>
    /// Gets the value position.
    /// </summary>
    public int Position { get; } = position;

    /// <summary>
    /// Gets the numeric value.
    /// </summary>
    public int Value { get; } = value;

    /// <summary>
    /// Gets whether this value is valid.
    /// </summary>
    public bool Valid => Position != -1;

    /// <summary>
    /// Compares with another value and returns the value that comes after.
    /// </summary>
    /// <param name="another">The other value to compare with.</param>
    /// <returns>The numeric value that comes after.</returns>
    public NumericValue GetAfter(NumericValue another)
    {
        return Valid switch
        {
            true when !another.Valid => this,
            false when another.Valid => another,
            _ => Position > another.Position ? this : another
        };
    }

    /// <summary>
    /// Compares with another value and returns the value that comes before.
    /// </summary>
    /// <param name="another">The other value to compare with.</param>
    /// <returns>The numeric value that comes before.</returns>
    public NumericValue GetBefore(NumericValue another)
    {
        return Valid switch
        {
            true when !another.Valid => this,
            false when another.Valid => another,
            _ => Position < another.Position ? this : another
        };
    }
}
