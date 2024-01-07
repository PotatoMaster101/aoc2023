using AoC.Common.Grid;
using AoC.Common.Input;

namespace AoC2023.Day14;

/// <summary>
/// Parser for <see cref="Platform"/>.
/// </summary>
public class PlatformParser : InputParser<Platform>
{
    /// <summary>
    /// Constructs a new instance of <see cref="PlatformParser"/>.
    /// </summary>
    /// <param name="filename">The input file name.</param>
    public PlatformParser(string filename) : base(filename) { }

    /// <inheritdoc cref="InputParser{T}.Parse"/>
    public override async Task<Platform> Parse(CancellationToken token = default)
    {
        const char roundRock = 'O';
        const char cubeRock = '#';
        ResetReader();

        int rowIdx = 0, maxCol = 0;
        var cubeRockPos = new HashSet<Position>();
        var roundRocks = new HashSet<Position>();
        await foreach (var line in GetNonEmptyLines(token))
        {
            maxCol = line.Length;
            for (var colIdx = 0; colIdx < line.Length; colIdx++)
            {
                if (line[colIdx] == roundRock)
                    roundRocks.Add(new Position(rowIdx, colIdx));
                else if (line[colIdx] == cubeRock)
                    cubeRockPos.Add(new Position(rowIdx, colIdx));
            }
            rowIdx++;
        }

        var bounds = new Boundary(rowIdx, maxCol);
        return new Platform(bounds, cubeRockPos, roundRocks);
    }
}
