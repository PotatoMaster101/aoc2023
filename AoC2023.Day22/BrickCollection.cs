namespace AoC2023.Day22;

/// <summary>
/// A collection of bricks.
/// </summary>
public class BrickCollection
{
    private readonly List<Brick> _bricks;
    private readonly Dictionary<int, HashSet<int>> _supportBricksAbove = [];
    private readonly Dictionary<int, HashSet<int>> _standingBricksBelow = [];

    /// <summary>
    /// Constructs a new instance of <see cref="BrickCollection"/>.
    /// </summary>
    /// <param name="bricks">The list of bricks.</param>
    public BrickCollection(IEnumerable<Brick> bricks)
    {
        _bricks = bricks.OrderBy(x => x).ToList();
        for (var i = 0; i < _bricks.Count; i++)
        {
            _supportBricksAbove.Add(i, []);
            _standingBricksBelow.Add(i, []);
        }

        FallBricks();
        _bricks = _bricks.OrderBy(x => x).ToList();
        InitialiseSupports();
    }

    /// <summary>
    /// Counts the number of bricks that can be disintegrated.
    /// </summary>
    /// <returns>The number of bricks that can be disintegrated.</returns>
    public int CountDisintegrate()
    {
        var result = 0;
        for (var i = 0; i < _bricks.Count; i++)
        {
            var supporters = _supportBricksAbove[i];
            if (supporters.All(x => _standingBricksBelow[x].Count > 1))
                result++;
        }
        return result;
    }

    /// <summary>
    /// Counts the falling bricks after disintegrating.
    /// </summary>
    /// <returns>The falling bricks after disintegrating.</returns>
    public int CountDisintegrateFalling()
    {
        var result = 0;
        for (var i = 0; i < _bricks.Count; i++)
            result += CountFallDependencies(i);
        return result;
    }

    /// <summary>
    /// Counts the number of dependencies on a specific brick if it were to fall.
    /// </summary>
    /// <param name="brickIdx">The index of the specific brick.</param>
    /// <returns>The number of dependencies on a specific brick if it were to fall.</returns>
    private int CountFallDependencies(int brickIdx)
    {
        var queue = new Queue<int>();
        foreach (var sup in _supportBricksAbove[brickIdx].Where(x => _standingBricksBelow[x].Count == 1))
            queue.Enqueue(sup);

        var falling = new HashSet<int>(queue) { brickIdx };
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            foreach (var support in _supportBricksAbove[current])
            {
                if (falling.Contains(support))
                    continue;
                if (!_standingBricksBelow[support].IsSubsetOf(falling))
                    continue;

                queue.Enqueue(support);
                falling.Add(support);
            }
        }
        return falling.Count - 1;
    }

    /// <summary>
    /// Make the bricks fall.
    /// </summary>
    private void FallBricks()
    {
        for (var i = 0; i < _bricks.Count; i++)
        {
            var current = _bricks[i];
            var fallDistance = current.MinZ - GetFallZPosition(i);
            current.Fall(fallDistance);
        }
    }

    /// <summary>
    /// Gets the Z value for the current brick after falling.
    /// </summary>
    /// <param name="brickIdx">The index of the current brick.</param>
    /// <returns>The Z value for the current brick after falling.</returns>
    private long GetFallZPosition(int brickIdx)
    {
        var current = _bricks[brickIdx];
        long result = 1;
        for (var i = 0; i < brickIdx; i++)
        {
            if (current.Collides(_bricks[i]))
                result = Math.Max(result, _bricks[i].MaxZ + 1);
        }
        return result;
    }

    /// <summary>
    /// Initialises the support mappings.
    /// </summary>
    private void InitialiseSupports()
    {
        for (var i = 0; i < _bricks.Count; i++)
        {
            var current = _bricks[i];
            for (var j = 0; j < i; j++)
            {
                var previous = _bricks[j];
                if (!current.Collides(previous) || current.MinZ != previous.MaxZ + 1)
                    continue;

                _supportBricksAbove[j].Add(i);
                _standingBricksBelow[i].Add(j);
            }
        }
    }
}
