namespace AoC2023.Day08;

/// <summary>
/// Represents a direction to go.
/// </summary>
public class Direction
{
    /// <summary>
    /// Gets the node to the left.
    /// </summary>
    public string Left { get; }

    /// <summary>
    /// Gets the node to the right.
    /// </summary>
    public string Right { get; }

    /// <summary>
    /// Constructs a new instance of <see cref="Direction"/>.
    /// </summary>
    /// <param name="left">The node to the left.</param>
    /// <param name="right">The node to the right.</param>
    public Direction(string left, string right)
    {
        Left = left;
        Right = right;
    }
}
