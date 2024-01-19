using AoC.Common.Grid;
using AoC.Common.Helpers;

namespace AoC2023.Day23;

/// <summary>
/// Represents a map.
/// </summary>
public class Map
{
    private const char DownSlope = 'v';
    private const char Forest = '#';
    private const char LeftSlope = '<';
    private const char Path = '.';
    private const char RightSlope = '>';
    private const char UpSlope = '^';

    private readonly Boundary _bounds;
    private readonly string[] _content;
    private readonly Position _end;
    private readonly Position _start;

    /// <summary>
    /// Constructs a new instance of <see cref="Map"/>.
    /// </summary>
    /// <param name="content">The content of the map.</param>
    public Map(string[] content)
    {
        _content = content;
        _bounds = new Boundary(content.Length, content[0].Length);
        _start = new Position(0, content[0].IndexOf(Path));
        _end = new Position(content.Length - 1, content[^1].IndexOf(Path));
    }

    /// <summary>
    /// Returns the longest path for this map.
    /// </summary>
    /// <param name="followSlopes">Whether to follow the slopes.</param>
    /// <returns>The longest path for this map.</returns>
    public int GetLongPath(bool followSlopes)
    {
        var graph = GetSimplifiedGraph(followSlopes);
        return GetLongPath([], graph, _start);
    }

    private int GetLongPath(HashSet<Position> visited, Dictionary<Position, Dictionary<Position, long>> graph, Position pos)
    {
        if (pos == _end)
            return 0;

        var result = double.NegativeInfinity;
        visited.Add(pos);
        foreach (var (dest, dist) in graph[pos])
        {
            if (!visited.Contains(dest))
                result = Math.Max(result, GetLongPath(visited, graph, dest) + dist);
        }

        visited.Remove(pos);
        return (int)result;
    }

    /// <summary>
    /// Builds a simplified graph where the direct paths are cut short.
    /// </summary>
    /// <param name="followSlopes">Whether to follow the slopes.</param>
    /// <returns>The simplified graph.</returns>
    private Dictionary<Position, Dictionary<Position, long>> GetSimplifiedGraph(bool followSlopes)
    {
        var result = new Dictionary<Position, Dictionary<Position, long>>();
        var junctions = GetJunctionPoints();
        foreach (var pos in junctions)
            result[pos] = GetSimplifiedPaths(pos, junctions, followSlopes);
        return result;
    }

    /// <summary>
    /// Returns a dictionary of simplified paths (path from one junction to another junction).
    /// </summary>
    /// <param name="current">The current position.</param>
    /// <param name="junctions">The set of junction points.</param>
    /// <param name="followSlopes">Whether to follow the slopes.</param>
    /// <returns>The dictionary of simplified paths.</returns>
    private Dictionary<Position, long> GetSimplifiedPaths(Position current, IReadOnlySet<Position> junctions, bool followSlopes)
    {
        var result = new Dictionary<Position, long>();
        var visited = new HashSet<Position> { current };
        var stack = new Stack<(Position, long)>();
        stack.Push((current, 0));

        while (stack.Count > 0)
        {
            var (pos, dist) = stack.Pop();
            if (dist > 0 && junctions.Contains(pos))
            {
                result[pos] = dist;     // pos is a junction, record distance
                continue;
            }

            // search for the next junction
            foreach (var neighbour in GetNextPositions(pos, followSlopes))
            {
                if (visited.Add(neighbour))
                    stack.Push((neighbour, dist + 1));
            }
        }
        return result;
    }

    /// <summary>
    /// Returns the set of junction positions.
    /// </summary>
    /// <returns>The set of junction positions.</returns>
    private HashSet<Position> GetJunctionPoints()
    {
        var result = new HashSet<Position> { _start, _end };
        for (var r = 0; r < _bounds.RowCount; r++)
        {
            for (var c = 0; c < _bounds.ColumnCount; c++)
            {
                var current = new Position(r, c);
                if (_content.At(current) == Forest)
                    continue;

                if (IsJunctionPoint(current))
                    result.Add(current);
            }
        }
        return result;
    }

    /// <summary>
    /// Returns whether the specified position is a junction point.
    /// </summary>
    /// <param name="pos">The position to check.</param>
    /// <param name="minNeighbours">The minimum neighbour count to treat this point as a junction.</param>
    /// <returns>Whether the position is a junction point.</returns>
    private bool IsJunctionPoint(Position pos, int minNeighbours = 3)
    {
        return _bounds.GetValidCrossNeighbours(pos).Count(x => _content.At(x) != Forest) >= minNeighbours;
    }

    /// <summary>
    /// Gets the next traversable neighbour positions.
    /// </summary>
    /// <param name="pos">The current position.</param>
    /// <param name="followSlopes">Whether to follow the slopes.</param>
    /// <returns>The list of traversable neighbour positions.</returns>
    private List<Position> GetNextPositions(Position pos, bool followSlopes)
    {
        if (!followSlopes)
            return _bounds.GetValidCrossNeighbours(pos).Where(x => _content.At(x) != Forest).ToList();

        var currTile = _content.At(pos);
        return currTile switch
        {
            DownSlope => _bounds.IsValid(pos.Bottom) ? [pos.Bottom] : [],
            LeftSlope => _bounds.IsValid(pos.Left) ? [pos.Left] : [],
            RightSlope => _bounds.IsValid(pos.Right) ? [pos.Right] : [],
            UpSlope => _bounds.IsValid(pos.Top) ? [pos.Top] : [],
            _ => _bounds.GetValidCrossNeighbours(pos).Where(x => _content.At(x) != Forest).ToList()
        };
    }
}
