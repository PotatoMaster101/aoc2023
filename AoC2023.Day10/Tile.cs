using AoC.Common.Grid;

namespace AoC2023.Day10;

/// <summary>
/// Represents a tile.
/// </summary>
public class Tile
{
    /// <summary>
    /// Gets the position of this tile.
    /// </summary>
    public Position Position { get; }

    /// <summary>
    /// Gets the type of this tile.
    /// </summary>
    public TileType Type { get; }

    /// <summary>
    /// Gets or sets this tile's distance from start.
    /// </summary>
    public long Distance { get; set; } = -1;

    /// <summary>
    /// Constructs a new instance of <see cref="Tile"/>.
    /// </summary>
    /// <param name="position">The position of this tile.</param>
    /// <param name="tile">The tile character.</param>
    public Tile(Position position, char tile)
    {
        Position = position;
        Type = tile switch
        {
            '-' => TileType.Horizontal,
            'L' => TileType.NorthEast,
            'J' => TileType.NorthWest,
            'F' => TileType.SouthEast,
            '7' => TileType.SouthWest,
            'S' => TileType.Start,
            '|' => TileType.Vertical,
            _ => TileType.Dirt
        };
    }

    /// <summary>
    /// Determines whether this tile can travel to another tile.
    /// </summary>
    /// <param name="other">The other tile to travel.</param>
    /// <param name="direction">The direction of the other tile relative to this tile.</param>
    /// <returns>Whether this tile can travel to another tile.</returns>
    public bool CanTravel(Tile other, Direction direction)
    {
        var otherType = other.Type;
        return Type switch
        {
            TileType.Horizontal => CanTravelHorizontal(otherType, direction),
            TileType.NorthEast => CanTravelNorthEast(otherType, direction),
            TileType.NorthWest => CanTravelNorthWest(otherType, direction),
            TileType.SouthEast => CanTravelSouthEast(otherType, direction),
            TileType.SouthWest => CanTravelSouthWest(otherType, direction),
            TileType.Start => CanTravelFromStart(otherType, direction),
            TileType.Vertical => CanTravelVertical(otherType, direction),
            _ => false
        };
    }

    /// <summary>
    /// Returns all the positions and directions this tile can travel to.
    /// </summary>
    /// <returns>All the positions and directions this tile can travel to.</returns>
    public IEnumerable<(Position, Direction)> GetDirectionalPositions()
    {
        return Type switch
        {
            TileType.Horizontal => new[] { (Position.Right, Direction.Right), (Position.Left, Direction.Left) },
            TileType.NorthEast => new[] { (Position.Right, Direction.Right), (Position.Top, Direction.Up) },
            TileType.NorthWest => new[] { (Position.Top, Direction.Up), (Position.Left, Direction.Left) },
            TileType.SouthEast => new[] { (Position.Right, Direction.Right), (Position.Bottom, Direction.Down) },
            TileType.SouthWest => new[] { (Position.Bottom, Direction.Down), (Position.Left, Direction.Left) },
            TileType.Start => new[] { (Position.Right, Direction.Right), (Position.Top, Direction.Up), (Position.Bottom, Direction.Down), (Position.Left, Direction.Left) },
            TileType.Vertical => new[] { (Position.Top, Direction.Up), (Position.Bottom, Direction.Down) },
            _ => Enumerable.Empty<(Position, Direction)>()
        };
    }

    /// <summary>
    /// Determines whether this tile can travel to another tile horizontally.
    /// </summary>
    /// <param name="other">The other tile type to connect.</param>
    /// <param name="direction">The direction of the other tile relative to this tile.</param>
    /// <returns>Whether this tile can travel to another tile horizontally.</returns>
    private static bool CanTravelHorizontal(TileType other, Direction direction)
    {
        return direction switch
        {
            Direction.Right => other is TileType.NorthWest or TileType.SouthWest or TileType.Horizontal or TileType.Start,
            Direction.Left => other is TileType.NorthEast or TileType.SouthEast or TileType.Horizontal or TileType.Start,
            _ => false
        };
    }

    /// <summary>
    /// Determines whether this tile can travel to another tile vertically.
    /// </summary>
    /// <param name="other">The other tile type to connect.</param>
    /// <param name="direction">The direction of the other tile relative to this tile.</param>
    /// <returns>Whether this tile can travel to another tile vertically.</returns>
    private static bool CanTravelVertical(TileType other, Direction direction)
    {
        return direction switch
        {
            Direction.Up => other is TileType.SouthEast or TileType.SouthWest or TileType.Vertical or TileType.Start,
            Direction.Down => other is TileType.NorthEast or TileType.NorthWest or TileType.Vertical or TileType.Start,
            _ => false
        };
    }

    /// <summary>
    /// Determines whether this tile can travel to another tile in north east direction.
    /// </summary>
    /// <param name="other">The other tile type to connect.</param>
    /// <param name="direction">The direction of the other tile relative to this tile.</param>
    /// <returns>Whether this tile can travel to another tile in north east direction.</returns>
    private static bool CanTravelNorthEast(TileType other, Direction direction)
    {
        return direction switch
        {
            Direction.Right => other is TileType.NorthWest or TileType.SouthWest or TileType.Horizontal or TileType.Start,
            Direction.Up => other is TileType.SouthEast or TileType.SouthWest or TileType.Vertical or TileType.Start,
            _ => false
        };
    }

    /// <summary>
    /// Determines whether this tile can travel to another tile in north west direction.
    /// </summary>
    /// <param name="other">The other tile type to connect.</param>
    /// <param name="direction">The direction of the other tile relative to this tile.</param>
    /// <returns>Whether this tile can travel to another tile in north west direction.</returns>
    private static bool CanTravelNorthWest(TileType other, Direction direction)
    {
        return direction switch
        {
            Direction.Up => other is TileType.SouthEast or TileType.SouthWest or TileType.Vertical or TileType.Start,
            Direction.Left => other is TileType.NorthEast or TileType.SouthEast or TileType.Horizontal or TileType.Start,
            _ => false
        };
    }

    /// <summary>
    /// Determines whether this tile can travel to another tile in south west direction.
    /// </summary>
    /// <param name="other">The other tile type to connect.</param>
    /// <param name="direction">The direction of the other tile relative to this tile.</param>
    /// <returns>Whether this tile can travel to another tile in south west direction.</returns>
    private static bool CanTravelSouthWest(TileType other, Direction direction)
    {
        return direction switch
        {
            Direction.Down => other is TileType.NorthEast or TileType.NorthWest or TileType.Vertical or TileType.Start,
            Direction.Left => other is TileType.NorthEast or TileType.SouthEast or TileType.Horizontal or TileType.Start,
            _ => false
        };
    }

    /// <summary>
    /// Determines whether this tile can travel to another tile in south east direction.
    /// </summary>
    /// <param name="other">The other tile type to connect.</param>
    /// <param name="direction">The direction of the other tile relative to this tile.</param>
    /// <returns>Whether this tile can travel to another tile in south east direction.</returns>
    private static bool CanTravelSouthEast(TileType other, Direction direction)
    {
        return direction switch
        {
            Direction.Right => other is TileType.NorthWest or TileType.SouthWest or TileType.Horizontal or TileType.Start,
            Direction.Down => other is TileType.NorthEast or TileType.NorthWest or TileType.Vertical or TileType.Start,
            _ => false
        };
    }

    /// <summary>
    /// Determines whether this tile can travel to another tile in any direction.
    /// </summary>
    /// <param name="other">The other tile type to connect.</param>
    /// <param name="direction">The direction of the other tile relative to this tile.</param>
    /// <returns>Whether this tile can travel to another tile in any direction.</returns>
    private static bool CanTravelFromStart(TileType other, Direction direction)
    {
        return direction switch
        {
            Direction.Right => other is TileType.NorthWest or TileType.SouthWest or TileType.Horizontal or TileType.Start,
            Direction.Up => other is TileType.SouthEast or TileType.SouthWest or TileType.Vertical or TileType.Start,
            Direction.Down => other is TileType.NorthEast or TileType.NorthWest or TileType.Vertical or TileType.Start,
            Direction.Left => other is TileType.NorthEast or TileType.SouthEast or TileType.Horizontal or TileType.Start,
            _ => false
        };
    }
}
