using AoC.Common.Input;

namespace AoC2023.Day23;

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
        var lines = await GetAllNonEmptyLines(token);
        return new Map(lines);
    }
}
