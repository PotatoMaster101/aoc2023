using AoC.Common.Grid;

namespace AoC2023.Day14;

/// <summary>
/// Represents the platform containing the rocks..
/// </summary>
public class Platform
{
    private readonly HashSet<Position> _roundRocks;
    private readonly List<RockTracker> _tracker = [];

    /// <summary>
    /// Gets the platform boundary.
    /// </summary>
    public Boundary Bounds { get; }

    /// <summary>
    /// Gets the positions of all cube rocks.
    /// </summary>
    public IReadOnlySet<Position> CubeRocks { get; }

    /// <summary>
    /// Gets the position of all round rocks.
    /// </summary>
    public IReadOnlySet<Position> RoundRocks => _roundRocks;

    /// <summary>
    /// Constructs a new instance of <see cref="Platform"/>.
    /// </summary>
    /// <param name="bounds">The platform boundary.</param>
    /// <param name="cubeRocks">The set of cube rock positions.</param>
    /// <param name="roundRocks">The position of all round rocks.</param>
    public Platform(Boundary bounds, IReadOnlySet<Position> cubeRocks, HashSet<Position> roundRocks)
    {
        CubeRocks = cubeRocks;
        _roundRocks = roundRocks;
        Bounds = bounds;

        InitialiseRockTracker();
    }

    /// <summary>
    /// Counts the weight of round rocks on the platform.
    /// </summary>
    /// <returns>The weight of round rocks on the platform.</returns>
    public int CountWeight()
    {
        return RoundRocks.Sum(x => Bounds.RowCount32 - x.Row32);
    }

    /// <summary>
    /// Runs a cycle in up, left, down, right order.
    /// </summary>
    /// <param name="totalCycles">The number of times to run the cycle.</param>
    public void RunCycle(int totalCycles)
    {
        var cyclesRan = 0;
        while (cyclesRan < 50 && !_tracker.All(x => x.InCycle))
        {
            // get all rocks into cycle
            MoveRoundRocks(Direction.Up);
            MoveRoundRocks(Direction.Left);
            MoveRoundRocks(Direction.Down);
            MoveRoundRocks(Direction.Right);
            cyclesRan++;
        }

        // predict all rocks positions
        // TODO fix bug
        var rocks = _roundRocks.ToList();
        foreach (var pos in rocks)
        {
            var tracker = _tracker.First(x => x.CurrentPosition == pos);
            var destination = tracker.PredictCyclePosition(totalCycles - cyclesRan);
            _roundRocks.Remove(pos);
            _roundRocks.Add(destination);
        }
    }

    /// <summary>
    /// Moves round rocks in a specific direction.
    /// </summary>
    /// <param name="direction">The direction to move.</param>
    public void MoveRoundRocks(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                MoveRocksVertically(true);
                return;
            case Direction.Down:
                MoveRocksVertically(false);
                return;
            case Direction.Left:
                MoveRocksHorizontally(true);
                return;
            case Direction.Right:
                MoveRocksHorizontally(false);
                return;
        }
    }

    /// <summary>
    /// Moves all the round rocks up or down.
    /// </summary>
    /// <param name="up">Whether to move the round rocks up.</param>
    private void MoveRocksVertically(bool up)
    {
        var sortedRoundRocks = up ?
            RoundRocks.OrderBy(x => x.Row32).ToList() :
            RoundRocks.OrderByDescending(x => x.Row32).ToList();

        foreach (var pos in sortedRoundRocks)
        {
            var destination = pos;
            while (CanMoveTo(up ? destination.Top : destination.Bottom))
                destination = up ? destination.Top : destination.Bottom;

            if (pos != destination)
            {
                var track = _tracker.First(x => x.CurrentPosition == pos);
                track.CurrentPosition = destination;
                _roundRocks.Remove(pos);
                _roundRocks.Add(destination);
            }
        }
    }

    /// <summary>
    /// Moves all the round rocks left or right.
    /// </summary>
    /// <param name="left">Whether to move the round rocks left.</param>
    private void MoveRocksHorizontally(bool left)
    {
        var sortedRoundRocks = left ?
            RoundRocks.OrderBy(x => x.Column32).ToList() :
            RoundRocks.OrderByDescending(x => x.Column32).ToList();

        foreach (var pos in sortedRoundRocks)
        {
            var destination = pos;
            while (CanMoveTo(left ? destination.Left : destination.Right))
                destination = left ? destination.Left : destination.Right;

            if (pos != destination)
            {
                var track = _tracker.First(x => x.CurrentPosition == pos);
                track.CurrentPosition = destination;
                _roundRocks.Remove(pos);
                _roundRocks.Add(destination);
            }
        }
    }

    /// <summary>
    /// Determines whether a rock can move to a position.
    /// </summary>
    /// <param name="position">The position to move to.</param>
    /// <returns>Whether the rock can move to the position.</returns>
    private bool CanMoveTo(Position position)
    {
        return Bounds.IsValid(position) && !CubeRocks.Contains(position) && !RoundRocks.Contains(position);
    }

    /// <summary>
    /// Initialises the rock tracker.
    /// </summary>
    private void InitialiseRockTracker()
    {
        _tracker.Clear();
        foreach (var pos in _roundRocks)
            _tracker.Add(new RockTracker(pos));
    }
}
