using AoC.Common.Grid;
using AoC.Common.Input;

namespace AoC2023.Day21;

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
        var lines = new List<string>();
        var start = Position.Origin;
        var row = 0;
        await foreach (var line in GetNonEmptyLines(token))
        {
            lines.Add(line);
            var startIdx = line.IndexOf('S');
            if (startIdx > 0)
                start = new Position(row, startIdx);

            row++;
        }
        return new Map(lines, start);
    }
}
