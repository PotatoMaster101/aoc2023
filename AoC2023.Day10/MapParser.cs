using AoC.Common.Grid;
using AoC.Common.Input;

namespace AoC2023.Day10;

/// <summary>
/// Parser for <see cref="Map"/>.
/// </summary>
public class MapParser : InputParser<Map>
{
    /// <summary>
    /// Constructs a new instance of <see cref="MapParser"/>.
    /// </summary>
    /// <param name="filename">The input file name.</param>
    public MapParser(string filename) : base(filename) { }

    /// <inheritdoc cref="InputParser{T}.Parse"/>
    public override async Task<Map> Parse(CancellationToken token = default)
    {
        var tiles = new List<IReadOnlyList<Tile>>();
        var row = 0;
        await foreach (var line in GetNonEmptyLines(token))
        {
            var col = 0;
            tiles.Add(line.Select(x => new Tile(new Position(row, col++), x)).ToList());
            row++;
        }
        return new Map(tiles);
    }
}
