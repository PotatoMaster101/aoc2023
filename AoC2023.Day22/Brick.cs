using AoC.Common.ThreeDimensional;

namespace AoC2023.Day22;

/// <summary>
/// Represents a brick.
/// </summary>
public record Brick : IComparable<Brick>
{
    /// <summary>
    /// Gets the maximum Z position for this brick.
    /// </summary>
    public long MaxZ => Math.Max(Point1.Z, Point2.Z);

    /// <summary>
    /// Gets the minimum Z position for this brick.
    /// </summary>
    public long MinZ => Math.Min(Point1.Z, Point2.Z);

    /// <summary>
    /// Gets the coordinate of the first end.
    /// </summary>
    public Coordinate Point1 { get; private set; }

    /// <summary>
    /// Gets the coordinate of the second end.
    /// </summary>
    public Coordinate Point2 { get; private set; }

    /// <summary>
    /// Constructs an new instance of <see cref="Brick"/>.
    /// </summary>
    /// <param name="point1">The first end of the brick.</param>
    /// <param name="point2">The second end of the brick.</param>
    public Brick(Coordinate point1, Coordinate point2)
    {
        Point1 = point1;
        Point2 = point2;
    }

    /// <inheritdoc cref="IComparable{T}.CompareTo"/>
    public int CompareTo(Brick? other)
    {
        if (other is null)
            return 1;

        // we only care about Z axis
        return Point1.Z == other.Point1.Z ? Point2.Z.CompareTo(other.Point2.Z) : Point1.Z.CompareTo(other.Point1.Z);
    }

    /// <summary>
    /// Make this brick fall.
    /// </summary>
    /// <param name="distance">The fall distance.</param>
    public void Fall(long distance)
    {
        Point1 = Point1.GetDestination(Direction.NegativeZ, distance);
        Point2 = Point2.GetDestination(Direction.NegativeZ, distance);
    }

    /// <summary>
    /// Determines whether this brick collides with another brick.
    /// </summary>
    /// <param name="other">The other brick to check.</param>
    /// <returns>Whether this brick collides with another brick.</returns>
    public bool Collides(Brick other)
    {
        return Math.Max(Point1.X, other.Point1.X) <= Math.Min(Point2.X, other.Point2.X) &&
               Math.Max(Point1.Y, other.Point1.Y) <= Math.Min(Point2.Y, other.Point2.Y);
    }
}
