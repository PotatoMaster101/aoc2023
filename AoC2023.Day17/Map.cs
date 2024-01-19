using AoC.Common.Grid;

namespace AoC2023.Day17;

/// <summary>
/// Represents the puzzle input map.
/// </summary>
public class Map
{
    private readonly Boundary _bounds;

    /// <summary>
    /// Gets the map layout.
    /// </summary>
    public int[][] Layout { get; }

    /// <summary>
    /// Constructs a new instance of <see cref="Map"/>.
    /// </summary>
    /// <param name="layout">The map layout.</param>
    public Map(int[][] layout)
    {
        Layout = layout;
        _bounds = new Boundary(layout.Length, layout[0].Length);
    }

    /// <summary>
    /// Gets the minimum heat loss.
    /// </summary>
    /// <param name="minSteps">The minimum number of steps before turning.</param>
    /// <param name="maxSteps">The maximum number of steps before turning.</param>
    /// <returns>The minimum heat loss.</returns>
    public int GetMinHeatLoss(int minSteps, int maxSteps)
    {
        var visited = new HashSet<Path>();
        var queue = new PriorityQueue<Path, int>();
        queue.Enqueue(new Path(Position.Origin, Position.Origin), 0);
        while (queue.TryDequeue(out var path, out var dist))
        {
            if (path.Current == _bounds.BottomRight && path.Steps >= minSteps)
                return dist;
            if (!visited.Add(path))
                continue;   // path already visited before, skip

            var dst = path.Destination;
            if (path.Steps < maxSteps && dst != Position.Origin && _bounds.IsValid(dst))
                queue.Enqueue(new Path(dst, path.Direction, path.Steps + 1), dist + Layout[dst.Row32][dst.Column32]);
            if (path.Direction != Position.Origin && path.Steps < minSteps)
                continue;   // do not turn on first iteration or if min steps not reached

            // enqueue left/right directions
            foreach (var direction in Path.Directions)
            {
                if (path.Direction == direction || path.Direction == direction.Negate())
                    continue;   // don't go forward or backward, only enqueue left/right turns

                var next = path.Current.Add(direction);
                if (_bounds.IsValid(next))
                    queue.Enqueue(new Path(next, direction, 1), dist + Layout[next.Row32][next.Column32]);
            }
        }
        return int.MaxValue;
    }
}
