using AoC.Common.Grid;
using AoC.Common.Helpers;

namespace AoC2023.Day21;

/// <summary>
/// Represents a map.
/// </summary>
public class Map
{
    private const char Rock = '#';
    private readonly string[] _content;
    private readonly Position _start;

    /// <summary>
    /// Constructs a new instance of <see cref="Map"/>.
    /// </summary>
    /// <param name="content">The content of the map.</param>
    /// <param name="start">The start position.</param>
    public Map(IEnumerable<string> content, Position start)
    {
        _content = content.ToArray();
        _start = start;
    }

    /// <summary>
    /// Returns the reachable number of plots in specified number of steps.
    /// </summary>
    /// <param name="steps">The number of steps.</param>
    /// <returns>The reachable number of plots.</returns>
    public int GetPlotsCount(int steps)
    {
        var positions = new HashSet<Position> { _start };
        for (var i = 0; i < steps; i++)
            positions = GetReachablePositions(positions);
        return positions.Count;
    }

    public long GetRepeatedPlotsCount(long steps)
    {
        // TODO
        var result = 0;
        return result;
    }

    /// <summary>
    /// Returns the set of reachable positions from the current positions.
    /// </summary>
    /// <param name="current">The current positions.</param>
    /// <returns>The set of reachable positions from the current positions.</returns>
    private HashSet<Position> GetReachablePositions(IEnumerable<Position> current)
    {
        var result = new HashSet<Position>();
        foreach (var pos in current)
        {
            foreach (var neighbour in pos.GetCrossNeighbours())
            {
                var reduced = neighbour.Reduce(_content.Length, _content[0].Length);
                if (_content.At(reduced) != Rock)
                    result.Add(reduced);
            }
        }
        return result;
    }
}
