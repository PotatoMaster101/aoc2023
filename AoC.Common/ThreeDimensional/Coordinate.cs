namespace AoC.Common.ThreeDimensional;

/// <summary>
/// Represents a coordinate in a 3D space.
/// </summary>
public readonly record struct Coordinate : IComparable<Coordinate>
{
    /// <summary>
    /// Gets the origin position (0, 0, 0).
    /// </summary>
    public static readonly Coordinate Origin = new();

    /// <summary>
    /// Gets the X position.
    /// </summary>
    public long X { get; }

    /// <summary>
    /// Gets a Y position.
    /// </summary>
    public long Y { get; }

    /// <summary>
    /// Gets a Z position.
    /// </summary>
    public long Z { get; }

    /// <summary>
    /// Constructs a new instance of <see cref="Coordinate"/>.
    /// </summary>
    /// <param name="x">The X position.</param>
    /// <param name="y">The Y position.</param>
    /// <param name="z">The Z position.</param>
    public Coordinate(long x = 0, long y = 0, long z = 0)
    {
        X = x;
        Y = y;
        Z = z;
    }

    /// <inheritdoc cref="IComparable{T}.CompareTo"/>
    public int CompareTo(Coordinate other)
    {
        var xCompare = X.CompareTo(other.X);
        if (xCompare != 0)
            return xCompare;

        var yCompare = Y.CompareTo(other.Y);
        return yCompare != 0 ? yCompare : Z.CompareTo(other.Z);
    }

    /// <summary>
    /// Deconstructs this coordinate.
    /// </summary>
    /// <param name="x">The X position.</param>
    /// <param name="y">The Y position.</param>
    /// <param name="z">The Z position.</param>
    public void Deconstruct(out long x, out long y, out long z)
    {
        x = X;
        y = Y;
        z = Z;
    }

    /// <summary>
    /// Returns the destination coordinate along an axis.
    /// </summary>
    /// <param name="direction">The direction of the destination.</param>
    /// <param name="distance">The distance of the destination.</param>
    /// <returns>The destination coordinate along an axis.</returns>
    public Coordinate GetDestination(Direction direction, long distance = 1)
    {
        return direction switch
        {
            Direction.NegativeX => new Coordinate(X - distance, Y, Z),
            Direction.NegativeY => new Coordinate(X, Y - distance, Z),
            Direction.NegativeZ => new Coordinate(X, Y, Z - distance),
            Direction.PositiveX => new Coordinate(X + distance, Y, Z),
            Direction.PositiveY => new Coordinate(X, Y + distance, Z),
            _ => new Coordinate(X, Y, Z + distance)
        };
    }

    /// <summary>
    /// Returns a list of coordinates for a line in a specific direction.
    /// </summary>
    /// <param name="direction">The direction to get the line.</param>
    /// <param name="distance">The distance of the line.</param>
    /// <returns>A list of coordinates for a line in a specific direction.</returns>
    public IEnumerable<Coordinate> GetLine(Direction direction, long distance)
    {
        distance = Math.Abs(distance);
        return direction switch
        {
            Direction.NegativeX => GetLineForNegativeX(distance),
            Direction.NegativeY => GetLineForNegativeY(distance),
            Direction.NegativeZ => GetLineForNegativeZ(distance),
            Direction.PositiveX => GetLineForPositiveX(distance),
            Direction.PositiveY => GetLineForPositiveY(distance),
            _ => GetLineForPositiveZ(distance)
        };
    }

    /// <inheritdoc cref="object.ToString"/>
    public override string ToString()
    {
        return $"({X}, {Y}, {Z})";
    }

    /// <summary>
    /// Returns a coordinate with a new X position.
    /// </summary>
    /// <param name="newX">The new X position.</param>
    /// <returns>The coordinate with a new X position.</returns>
    public Coordinate WithX(long newX)
    {
        return new Coordinate(newX, Y, Z);
    }

    /// <summary>
    /// Returns a coordinate with a new Y position.
    /// </summary>
    /// <param name="newY">The new Y position.</param>
    /// <returns>The coordinate with a new Y position.</returns>
    public Coordinate WithY(long newY)
    {
        return new Coordinate(X, newY, Z);
    }

    /// <summary>
    /// Returns a coordinate with a new Z position.
    /// </summary>
    /// <param name="newZ">The new Z position.</param>
    /// <returns>The coordinate with a new Z position.</returns>
    public Coordinate WithZ(long newZ)
    {
        return new Coordinate(X, Y, newZ);
    }

    /// <summary>
    /// Returns a list of coordinates for a line in the negative X direction.
    /// </summary>
    /// <param name="distance">The distance of the line.</param>
    /// <returns>A list of coordinates for a line in the negative X direction.</returns>
    private IEnumerable<Coordinate> GetLineForNegativeX(long distance)
    {
        for (var x = X; x >= X - distance; x--)
            yield return new Coordinate(x, Y, Z);
    }

    /// <summary>
    /// Returns a list of coordinates for a line in the negative Y direction.
    /// </summary>
    /// <param name="distance">The distance of the line.</param>
    /// <returns>A list of coordinates for a line in the negative Y direction.</returns>
    private IEnumerable<Coordinate> GetLineForNegativeY(long distance)
    {
        for (var y = Y; y >= Y - distance; y--)
            yield return new Coordinate(X, y, Z);
    }

    /// <summary>
    /// Returns a list of coordinates for a line in the negative Z direction.
    /// </summary>
    /// <param name="distance">The distance of the line.</param>
    /// <returns>A list of coordinates for a line in the negative Z direction.</returns>
    private IEnumerable<Coordinate> GetLineForNegativeZ(long distance)
    {
        for (var z = Z; z >= Z - distance; z--)
            yield return new Coordinate(X, Y, z);
    }

    /// <summary>
    /// Returns a list of coordinates for a line in the positive X direction.
    /// </summary>
    /// <param name="distance">The distance of the line.</param>
    /// <returns>A list of coordinates for a line in the positive X direction.</returns>
    private IEnumerable<Coordinate> GetLineForPositiveX(long distance)
    {
        for (var x = X; x <= X + distance; x++)
            yield return new Coordinate(x, Y, Z);
    }

    /// <summary>
    /// Returns a list of coordinates for a line in the positive Y direction.
    /// </summary>
    /// <param name="distance">The distance of the line.</param>
    /// <returns>A list of coordinates for a line in the positive Y direction.</returns>
    private IEnumerable<Coordinate> GetLineForPositiveY(long distance)
    {
        for (var y = Y; y <= Y + distance; y++)
            yield return new Coordinate(X, y, Z);
    }

    /// <summary>
    /// Returns a list of coordinates for a line in the positive Z direction.
    /// </summary>
    /// <param name="distance">The distance of the line.</param>
    /// <returns>A list of coordinates for a line in the positive Z direction.</returns>
    private IEnumerable<Coordinate> GetLineForPositiveZ(long distance)
    {
        for (var z = Z; z <= Z + distance; z++)
            yield return new Coordinate(X, Y, z);
    }
}
