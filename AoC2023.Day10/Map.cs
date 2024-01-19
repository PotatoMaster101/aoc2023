using AoC.Common.Grid;

namespace AoC2023.Day10;

/// <summary>
/// A map of pipes.
/// </summary>
public class Map
{
    private const int ExpandAmount = 3;
    private const char ExpandedAir = '0';
    private const char ExpandedWall = '#';
    private const char ExpandedStartTile = 'S';
    private readonly HashSet<Position> _visitedPositions;

    /// <summary>
    /// Gets the map content.
    /// </summary>
    public IReadOnlyList<IReadOnlyList<Tile>> Content { get; }

    /// <summary>
    /// Gets the starting point.
    /// </summary>
    public Position StartPosition { get; }

    /// <summary>
    /// Gets the boundary of the map.
    /// </summary>
    public Boundary Boundary { get; }

    /// <summary>
    /// Constructs a new instance of <see cref="Map"/>.
    /// </summary>
    /// <param name="tiles">The collection of tiles.</param>
    public Map(IReadOnlyList<IReadOnlyList<Tile>> tiles)
    {
        Content = tiles;
        Boundary = new Boundary(tiles.Count, tiles[0].Count);
        StartPosition = GetStartPosition();
        _visitedPositions = GetPositionsOnLoopPath();
    }

    /// <summary>
    /// Returns the maximum distance for the loop.
    /// </summary>
    /// <returns>The maximum distance for the loop.</returns>
    public int GetMaxDistance()
    {
        return _visitedPositions.Count / 2;
    }

    /// <summary>
    /// Gets the number of enclosed areas.
    /// </summary>
    /// <returns></returns>
    public int GetEnclosedCount()
    {
        const char visited = '.';
        var expandedMap = GetExpandedMap();
        var queue = new Queue<Position>();
        queue.Enqueue(new Position(0, 1));
        queue.Enqueue(new Position(1));
        queue.Enqueue(new Position(1, 1));

        expandedMap[Position.Origin] = visited;
        while (queue.Count > 0)
        {
            var pos = queue.Dequeue();
            if (expandedMap[pos] != ExpandedAir)
                continue;

            foreach (var neighbour in pos.GetNeighbours())
            {
                var getVal = expandedMap.TryGetValue(neighbour, out var val);
                if (getVal && val == visited)
                    expandedMap[pos] = visited;
                else if (getVal && val == ExpandedAir)
                    queue.Enqueue(neighbour);
            }
        }
        return CountExpandedMapAir(expandedMap);
    }

    /// <summary>
    /// Finds and sets the start index.
    /// </summary>
    private Position GetStartPosition()
    {
        foreach (var row in Content)
        {
            var start = row.FirstOrDefault(x => x.Type == TileType.Start);
            if (start is not null)
                return start.Position;
        }
        return Position.Origin;
    }

    /// <summary>
    /// Gets every position that is on the path of loop.
    /// </summary>
    /// <returns>Every position that is on the path of loop.</returns>
    private HashSet<Position> GetPositionsOnLoopPath()
    {
        var result = new HashSet<Position>();
        var start = Content[StartPosition.Row32][StartPosition.Column32];
        var queue = new Queue<Tile>();
        queue.Enqueue(start);
        start.Distance = 0;
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            foreach (var (nextPos, nextDir) in current.GetDirectionalPositions())
            {
                if (!Boundary.IsValid(nextPos) || !current.CanTravel(Content[nextPos.Row32][nextPos.Column32], nextDir))
                    continue;
                if (Content[nextPos.Row32][nextPos.Column32].Distance != -1)
                    continue;

                Content[nextPos.Row32][nextPos.Column32].Distance = current.Distance + 1;
                queue.Enqueue(Content[nextPos.Row32][nextPos.Column32]);
            }
            result.Add(current.Position);
        }
        return result;
    }

    /// <summary>
    /// Generates an expanded map.
    /// </summary>
    /// <returns>A dictionary containing the expanded map coordinates and characters.</returns>
    private Dictionary<Position, char> GetExpandedMap()
    {
        var result = new Dictionary<Position, char>();
        foreach (var row in Content)
        {
            foreach (var tile in row)
            {
                var tiles = GenerateExpandedTile(tile);
                foreach (var (pos, ch) in tiles)
                    result.Add(pos, ch);
            }
        }
        return result;
    }

    /// <summary>
    /// Counts the number of air tiles in the expanded map.
    /// </summary>
    /// <param name="expandedMap">The expanded map to count air.</param>
    /// <returns>The number of air tiles.</returns>
    private int CountExpandedMapAir(IReadOnlyDictionary<Position, char> expandedMap)
    {
        var airCount = 0;
        for (var r = 0; r < Content.Count; r++)
        {
            for (var c = 0; c < Content[r].Count; c++)
            {
                var pos = new Position(r, c);
                if (CheckExpandedAirTile(expandedMap, pos))
                    airCount++;
            }
        }
        return airCount;
    }

    /// <summary>
    /// Checks whether an expanded tile is all air.
    /// </summary>
    /// <param name="map">The expanded map.</param>
    /// <param name="position">The position of the tile.</param>
    /// <returns>Whether the tile is all air.</returns>
    private static bool CheckExpandedAirTile(IReadOnlyDictionary<Position, char> map, Position position)
    {
        var expanded = position.Multiply(ExpandAmount);
        return map[expanded] == ExpandedAir &&
               map[expanded.Right] == ExpandedAir &&
               map[expanded.Right.Right] == ExpandedAir &&
               map[expanded.Bottom] == ExpandedAir &&
               map[expanded.Bottom.Right] == ExpandedAir &&
               map[expanded.Bottom.Right.Right] == ExpandedAir &&
               map[expanded.Bottom.Bottom] == ExpandedAir &&
               map[expanded.Bottom.Bottom.Right] == ExpandedAir &&
               map[expanded.Bottom.Bottom.Right.Right] == ExpandedAir;
    }

    /// <summary>
    /// Generates an expanded tile.
    /// </summary>
    /// <param name="tile">The tile to expand.</param>
    /// <returns>The expanded tile.</returns>
    private Dictionary<Position, char> GenerateExpandedTile(Tile tile)
    {
        var result = new Dictionary<Position, char>();
        var pos = tile.Position.Multiply(ExpandAmount);
        if (!_visitedPositions.Contains(tile.Position))
        {
            result[pos] = ExpandedAir;
            result[pos.Right] = ExpandedAir;
            result[pos.Right.Right] = ExpandedAir;
            result[pos.Bottom] = ExpandedAir;
            result[pos.Bottom.Right] = ExpandedAir;
            result[pos.Bottom.Right.Right] = ExpandedAir;
            result[pos.Bottom.Bottom] = ExpandedAir;
            result[pos.Bottom.Bottom.Right] = ExpandedAir;
            result[pos.Bottom.Bottom.Right.Right] = ExpandedAir;
            return result;
        }

        result[pos] = tile.Position == StartPosition ? ExpandedStartTile : ExpandedAir;
        switch (tile.Type)
        {
            case TileType.Start:
                result[pos.Right] = ExpandedStartTile;
                result[pos.Right.Right] = ExpandedStartTile;
                result[pos.Bottom] = ExpandedStartTile;
                result[pos.Bottom.Right] = ExpandedStartTile;
                result[pos.Bottom.Right.Right] = ExpandedStartTile;
                result[pos.Bottom.Bottom] = ExpandedStartTile;
                result[pos.Bottom.Bottom.Right] = ExpandedStartTile;
                result[pos.Bottom.Bottom.Right.Right] = ExpandedStartTile;
                return result;
            case TileType.Horizontal:
                result[pos.Right] = ExpandedAir;
                result[pos.Right.Right] = ExpandedAir;
                result[pos.Bottom] = ExpandedWall;
                result[pos.Bottom.Right] = ExpandedWall;
                result[pos.Bottom.Right.Right] = ExpandedWall;
                result[pos.Bottom.Bottom] = ExpandedAir;
                result[pos.Bottom.Bottom.Right] = ExpandedAir;
                result[pos.Bottom.Bottom.Right.Right] = ExpandedAir;
                return result;
            case TileType.Vertical:
                result[pos.Right] = ExpandedWall;
                result[pos.Right.Right] = ExpandedAir;
                result[pos.Bottom] = ExpandedAir;
                result[pos.Bottom.Right] = ExpandedWall;
                result[pos.Bottom.Right.Right] = ExpandedAir;
                result[pos.Bottom.Bottom] = ExpandedAir;
                result[pos.Bottom.Bottom.Right] = ExpandedWall;
                result[pos.Bottom.Bottom.Right.Right] = ExpandedAir;
                return result;
            case TileType.NorthEast:
                result[pos.Right] = ExpandedWall;
                result[pos.Right.Right] = ExpandedAir;
                result[pos.Bottom] = ExpandedAir;
                result[pos.Bottom.Right] = ExpandedWall;
                result[pos.Bottom.Right.Right] = ExpandedWall;
                result[pos.Bottom.Bottom] = ExpandedAir;
                result[pos.Bottom.Bottom.Right] = ExpandedAir;
                result[pos.Bottom.Bottom.Right.Right] = ExpandedAir;
                return result;
            case TileType.NorthWest:
                result[pos.Right] = ExpandedWall;
                result[pos.Right.Right] = ExpandedAir;
                result[pos.Bottom] = ExpandedWall;
                result[pos.Bottom.Right] = ExpandedWall;
                result[pos.Bottom.Right.Right] = ExpandedAir;
                result[pos.Bottom.Bottom] = ExpandedAir;
                result[pos.Bottom.Bottom.Right] = ExpandedAir;
                result[pos.Bottom.Bottom.Right.Right] = ExpandedAir;
                return result;
            case TileType.SouthWest:
                result[pos.Right] = ExpandedAir;
                result[pos.Right.Right] = ExpandedAir;
                result[pos.Bottom] = ExpandedWall;
                result[pos.Bottom.Right] = ExpandedWall;
                result[pos.Bottom.Right.Right] = ExpandedAir;
                result[pos.Bottom.Bottom] = ExpandedAir;
                result[pos.Bottom.Bottom.Right] = ExpandedWall;
                result[pos.Bottom.Bottom.Right.Right] = ExpandedAir;
                return result;
            case TileType.SouthEast:
                result[pos.Right] = ExpandedAir;
                result[pos.Right.Right] = ExpandedAir;
                result[pos.Bottom] = ExpandedAir;
                result[pos.Bottom.Right] = ExpandedWall;
                result[pos.Bottom.Right.Right] = ExpandedWall;
                result[pos.Bottom.Bottom] = ExpandedAir;
                result[pos.Bottom.Bottom.Right] = ExpandedWall;
                result[pos.Bottom.Bottom.Right.Right] = ExpandedAir;
                return result;
            default:
                return result;
        }
    }
}
