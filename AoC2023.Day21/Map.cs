using AoC.Common.Grid;
using AoC.Common.Helpers;

namespace AoC2023.Day21;

/// <summary>
/// Represents a map.
/// </summary>
public class Map
{
    private const char Rock = '#';
    private readonly Boundary _bounds;
    private readonly string[] _content;
    private readonly int _size;
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
        _size = _content.Length;
        _bounds = new Boundary(_content.Length, _content[0].Length);
    }

    /// <summary>
    /// Returns the reachable number of plots in specified number of steps.
    /// </summary>
    /// <param name="steps">The number of steps.</param>
    /// <returns>The reachable number of plots.</returns>
    public int GetPlotsCount(int steps)
    {
        return Fill(_start, steps);
    }

    /// <summary>
    /// Returns the reachable number of plots in a repeated map.
    /// </summary>
    /// <param name="steps">The number of steps.</param>
    /// <returns>The reachable number of plots</returns>
    public long GetRepeatedPlotsCount(long steps)
    {
        var width = steps / _size - 1;
        var oddCount = (long)Math.Pow(width, 2);
        var evenCount = (long)Math.Pow(width + 1, 2);
        var odd = oddCount * Fill(_start, _size * 2 + 1);
        var even = evenCount * Fill(_start, _size * 2);

        var rightCorner = Fill(_start.WithColumn(0), _size - 1);
        var bottomCorner = Fill(_start.WithRow(0), _size - 1);
        var topCorner = Fill(_start.WithRow(_size - 1), _size - 1);
        var leftCorner = Fill(_start.WithColumn(_size - 1), _size - 1);
        var corners = rightCorner + bottomCorner + topCorner + leftCorner;

        var smallSegTopRight = Fill(new Position(_size - 1), _size / 2 - 1);
        var smallSegTopLeft = Fill(new Position(_size - 1, _size - 1), _size / 2 - 1);
        var smallSegBottomRight = Fill(Position.Origin, _size / 2 - 1);
        var smallSegBottomLeft = Fill(new Position(0, _size - 1), _size / 2 - 1);
        var smallSegs = (width + 1) * (smallSegTopRight + smallSegTopLeft + smallSegBottomRight + smallSegBottomLeft);

        var largeSegTopRight = Fill(new Position(_size - 1), _size * 3 / 2 - 1);
        var largeSegTopLeft = Fill(new Position(_size - 1, _size - 1), _size * 3 / 2 - 1);
        var largeSegBottomRight = Fill(Position.Origin, _size * 3 / 2 - 1);
        var largeSegBottomLeft = Fill(new Position(0, _size - 1), _size * 3 / 2 - 1);
        var largeSegs = width * (largeSegTopRight + largeSegTopLeft + largeSegBottomRight + largeSegBottomLeft);

        return odd + even + corners + smallSegs + largeSegs;
    }

    /// <summary>
    /// Fills the map and returns the count of reachable positions.
    /// </summary>
    /// <param name="start">The start point.</param>
    /// <param name="steps">The number of steps to fill.</param>
    /// <returns>The count of reachable positions.</returns>
    private int Fill(Position start, long steps)
    {
        var results = new HashSet<Position>();
        var visited = new HashSet<Position> { start };
        var queue = new Queue<(Position, long)>();
        queue.Enqueue((start, steps));

        while (queue.Count > 0)
        {
            var (pos, currSteps) = queue.Dequeue();
            if (currSteps % 2 == 0)
                results.Add(pos);
            if (currSteps == 0)
                continue;

            foreach (var neighbour in _bounds.GetValidCrossNeighbours(pos))
            {
                if (_content.At(neighbour) != Rock && visited.Add(neighbour))
                    queue.Enqueue((neighbour, currSteps - 1));
            }
        }
        return results.Count;
    }
}
