using AoC.Common.Input;
using AoC.Common.ThreeDimensional;

namespace AoC2023.Day22;

/// <summary>
/// Parser for <see cref="BrickCollectionParser"/>.
/// </summary>
public class BrickCollectionParser : InputParser<BrickCollection>
{
    /// <summary>
    /// Constructs a new instance of <see cref="BrickCollectionParser"/>.
    /// </summary>
    /// <param name="filename">The input file name.</param>
    public BrickCollectionParser(string filename) : base(filename) { }

    /// <inheritdoc cref="InputParser{T}.Parse"/>
    public override async Task<BrickCollection> Parse(CancellationToken token = default)
    {
        return new BrickCollection(await GetNonEmptyLines(token).Select(ParseLine).ToListAsync(token));
    }

    /// <summary>
    /// Parses a coordinate.
    /// </summary>
    /// <param name="str">The string containing the coordinate.</param>
    /// <returns>The parsed coordinate.</returns>
    private static Coordinate ParseCoordinate(string str)
    {
        var splits = str
            .Split(',')
            .Select(int.Parse)
            .ToArray();

        return new Coordinate(splits[0], splits[1], splits[2]);
    }

    /// <summary>
    /// Parses a brick.
    /// </summary>
    /// <param name="line">The line containing the brick.</param>
    /// <returns>The parsed brick.</returns>
    private static Brick ParseLine(string line)
    {
        var splits = line.Split('~');
        return new Brick(ParseCoordinate(splits[0]), ParseCoordinate(splits[1]));
    }
}
