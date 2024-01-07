using AoC.Common.Grid;

namespace AoC2023.Day16;

/// <summary>
/// Represents a light beam.
/// </summary>
public class LightBeam
{
    private HashSet<Position> _splitPoints = [];

    /// <summary>
    /// Gets or sets the beam direction.
    /// </summary>
    public Direction Direction { get; set; }

    /// <summary>
    /// Gets or sets the beam position.
    /// </summary>
    public Position Position { get; private set; }

    /// <summary>
    /// Constructs a new instance of <see cref="LightBeam"/>.
    /// </summary>
    /// <param name="direction">The beam direction.</param>
    /// <param name="position">The beam position.</param>
    public LightBeam(Direction direction, Position position)
    {
        Direction = direction;
        Position = position;
    }

    /// <summary>
    /// Adds a position that caused this beam to split.
    /// </summary>
    /// <param name="position">The position that caused this beam to split.</param>
    /// <returns>Whether the position already exists.</returns>
    public bool AddSplitPoint(Position position)
    {
        return _splitPoints.Add(position);
    }

    /// <summary>
    /// Returns the next position that this beam will visit.
    /// </summary>
    /// <returns>The next position that this beam will visit.</returns>
    public Position GetNextPosition()
    {
        return Direction switch
        {
            Direction.Down => Position.Bottom,
            Direction.Up => Position.Top,
            Direction.Left => Position.Left,
            _ => Position.Right
        };
    }

    /// <summary>
    /// Moves this beam to the next position.
    /// </summary>
    public void MoveToNextPosition()
    {
        Position = GetNextPosition();
    }

    /// <summary>
    /// Clones this beam to another beam.
    /// </summary>
    /// <param name="direction">The beam direction.</param>
    /// <param name="position">The beam position.</param>
    /// <returns>The new beam cloned.</returns>
    public LightBeam Clone(Direction direction, Position position)
    {
        var newBeam = new LightBeam(direction, position)
        {
            _splitPoints = _splitPoints
        };
        return newBeam;
    }
}
