namespace AoC2023.Day19;

/// <summary>
/// Represents a part rating.
/// </summary>
public readonly struct Rating
{
    /// <summary>
    /// Gets the X rating.
    /// </summary>
    public int X { get; }

    /// <summary>
    /// Gets the M rating.
    /// </summary>
    public int M { get; }

    /// <summary>
    /// Gets the A rating.
    /// </summary>
    public int A { get; }

    /// <summary>
    /// Gets the S rating.
    /// </summary>
    public int S { get; }

    /// <summary>
    /// Constructs a new instance of <see cref="Rating"/>.
    /// </summary>
    /// <param name="x">The X rating.</param>
    /// <param name="m">The M rating.</param>
    /// <param name="a">The A rating.</param>
    /// <param name="s">The S rating.</param>
    public Rating(int x, int m, int a, int s)
    {
        X = x;
        M = m;
        A = a;
        S = s;
    }

    /// <summary>
    /// Returns the sum of ratings.
    /// </summary>
    /// <returns>The sum of ratings.</returns>
    public int Sum()
    {
        return X + M + A + S;
    }

    /// <summary>
    /// Determines whether a rule passes for this rating.
    /// </summary>
    /// <param name="category">The category for the value.</param>
    /// <param name="comparison">The value to compare.</param>
    /// <param name="opr">The comparison operator.</param>
    /// <returns>Whether a rule passes for this rating.</returns>
    public bool CanPassRule(char category, int comparison, char opr)
    {
        var value = GetValue(category);
        return opr switch
        {
            '>' => value > comparison,
            _ => value < comparison
        };
    }

    /// <summary>
    /// Gets the value corresponding to the category.
    /// </summary>
    /// <param name="category">The category to get the value.</param>
    /// <returns>The value corresponding to the category.</returns>
    private int GetValue(char category)
    {
        return category switch
        {
            'x' => X,
            'm' => M,
            'a' => A,
            _ => S
        };
    }
}
