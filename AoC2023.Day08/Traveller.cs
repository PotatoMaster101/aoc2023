using System.Collections.Concurrent;
using AoC.Common.Helpers;

namespace AoC2023.Day08;

/// <summary>
/// Represents a traveller.
/// </summary>
public class Traveller
{
    private const string Start = "AAA";
    private const string End = "ZZZ";
    private const char EndChar = 'Z';
    private readonly string _header;
    private readonly IReadOnlyDictionary<string, Direction> _map;

    /// <summary>
    /// Constructs a new instance of <see cref="Traveller"/>.
    /// </summary>
    /// <param name="header">The map header.</param>
    /// <param name="map">The map to travel.</param>
    public Traveller(string header, IReadOnlyDictionary<string, Direction> map)
    {
        _header = header;
        _map = map;
    }

    /// <summary>
    /// Gets the total steps from start to end.
    /// </summary>
    /// <returns>The total steps from start to end.</returns>
    public long GetTotalSteps()
    {
        return GetTotalSteps(Start, false);
    }

    /// <summary>
    /// Gets the total steps from A to Z simultaneously.
    /// </summary>
    /// <returns>The total steps from A to Z simultaneously.</returns>
    public long GetSimultaneousSteps()
    {
        var nodes = _map.Keys.Where(x => x.EndsWith('A'));
        var bag = new ConcurrentBag<long>();
        Parallel.ForEach(nodes, node =>
        {
            bag.Add(GetTotalSteps(node, true));
        });
        return MathHelper.LeastCommonMultiple(bag.ToList());
    }

    /// <summary>
    /// Gets the total steps from start to end.
    /// </summary>
    /// <param name="start">The starting node.</param>
    /// <param name="onlyCheckEndingChar">Whether the end node ends with Z character. If not, use ZZZ as end node.</param>
    /// <returns>The total steps from start to end.</returns>
    private long GetTotalSteps(string start, bool onlyCheckEndingChar)
    {
        long steps = 0;
        var headerIdx = 0;
        var node = start;
        while (onlyCheckEndingChar ? !node.EndsWith(EndChar) : node != End)
        {
            if (headerIdx == _header.Length)
                headerIdx = 0;

            node = GetNextNode(_map[node], _header[headerIdx]);
            steps++;
            headerIdx++;
        }
        return steps;
    }

    /// <summary>
    /// Gets the next node.
    /// </summary>
    /// <param name="direction">The current direction from the map.</param>
    /// <param name="instruction">The instruction to turn.</param>
    /// <returns>The next node.</returns>
    private static string GetNextNode(Direction direction, char instruction)
    {
        return instruction switch
        {
            'R' => direction.Right,
            _ => direction.Left
        };
    }
}
