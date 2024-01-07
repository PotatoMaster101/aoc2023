using AoC.Common.Grid;

namespace AoC2023.Day18;

/// <summary>
/// Represents a dig instruction.
/// </summary>
public class Instruction
{
    /// <summary>
    /// Gets the colour of the edge.
    /// </summary>
    public string Colour { get; }

    /// <summary>
    /// Gets the dig direction.
    /// </summary>
    public Direction Direction { get; }

    /// <summary>
    /// Gets the dig distance.
    /// </summary>
    public int Distance { get; }

    /// <summary>
    /// Gets the encoded direction from the colour.
    /// </summary>
    public Direction EncodedDirection { get; }

    /// <summary>
    /// Gets the encoded distance from the colour.
    /// </summary>
    public int EncodedDistance { get; }

    /// <summary>
    /// Constructs a new instance of <see cref="Instruction"/>.
    /// </summary>
    /// <param name="colour">The colour of the edge.</param>
    /// <param name="direction">The dig direction.</param>
    /// <param name="distance">The dig distance.</param>
    public Instruction(string colour, Direction direction, int distance)
    {
        Colour = colour;
        Direction = direction;
        Distance = distance;
        EncodedDirection = GetEncodedDirection();
        EncodedDistance = Convert.ToInt32(Colour[1..6], 16);
    }

    private Direction GetEncodedDirection()
    {
        return Colour[^1] switch
        {
            '0' => Direction.Right,
            '1' => Direction.Down,
            '2' => Direction.Left,
            _ => Direction.Up
        };
    }
}
