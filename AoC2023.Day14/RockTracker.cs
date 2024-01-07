using AoC.Common.Grid;

namespace AoC2023.Day14;

public class RockTracker
{
    private Position _currentPosition;
    private bool _inCycle;
    private readonly List<Position> _previousPositions = [];

    /// <summary>
    /// Gets the rock's start position.
    /// </summary>
    public Position StartPosition { get; }

    /// <summary>
    /// Gets or sets the rock's current position.
    /// </summary>
    public Position CurrentPosition
    {
        get => _currentPosition;
        set
        {
            _currentPosition = value;
            UpdatePreviousPositions(value);
        }
    }

    /// <summary>
    /// Gets whether this rock is currently in a cycle.
    /// </summary>
    public bool InCycle => _inCycle;

    /// <summary>
    /// Constructs a new instance of <see cref="RockTracker"/>.
    /// </summary>
    /// <param name="startPosition">The starting position.</param>
    public RockTracker(Position startPosition)
    {
        StartPosition = startPosition;
        CurrentPosition = startPosition;
    }

    /// <summary>
    /// Predicts the cycle position.
    /// </summary>
    /// <param name="count">The number of times to run the cycle.</param>
    /// <returns>The cycle position.</returns>
    public Position PredictCyclePosition(int count)
    {
        const int maxPositions = 4;
        var cycleMembers = _previousPositions.TakeLast(maxPositions).ToList();
        var limit = Math.Min(maxPositions, cycleMembers.Count);
        if (count < limit)
            return cycleMembers[count];

        var idx = count % (limit + 1);
        return cycleMembers[idx];
    }

    /// <summary>
    /// Updates the previous positions.
    /// </summary>
    /// <param name="previous">The previous position.</param>
    private void UpdatePreviousPositions(Position previous)
    {
        if (_previousPositions.Count > 4 && _previousPositions.Contains(previous))
            _inCycle = true;
        else if (!_inCycle)
            _previousPositions.Add(previous);
    }
}
