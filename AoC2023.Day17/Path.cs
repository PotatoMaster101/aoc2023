using AoC.Common.Grid;

namespace AoC2023.Day17;

/// <summary>
/// Represents a path.
/// </summary>
public readonly record struct Path
{
    /// <summary>
    /// The possible directions.
    /// </summary>
    public static readonly Position[] Directions = { new(1), new(0, 1), new(-1), new(0, -1) };

    /// <summary>
    /// Gets the current position.
    /// </summary>
    public Position Current { get; }

    /// <summary>
    /// Gets the path direction.
    /// </summary>
    public Position Direction { get; }

    /// <summary>
    /// Gets the destination position.
    /// </summary>
    public Position Destination => Current.Add(Direction);

    /// <summary>
    /// Gets the number of steps.
    /// </summary>
    public int Steps { get; }

    /// <summary>
    /// Constructs a new instance of <see cref="Path"/>.
    /// </summary>
    /// <param name="current">The current position.</param>
    /// <param name="direction">The path direction.</param>
    /// <param name="steps">The number of steps.</param>
    public Path(Position current, Position direction, int steps = 0)
    {
        Current = current;
        Direction = direction;
        Steps = steps;
    }
}
