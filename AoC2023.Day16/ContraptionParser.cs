using AoC.Common.Input;

namespace AoC2023.Day16;

/// <summary>
/// Parser for <see cref="Contraption"/>.
/// </summary>
public class ContraptionParser : InputParser<Contraption>
{
    /// <summary>
    /// Constructs a new instance of <see cref="ContraptionParser"/>.
    /// </summary>
    /// <param name="filename">The input file name.</param>
    public ContraptionParser(string filename) : base(filename) { }

    /// <inheritdoc cref="InputParser{T}.Parse"/>
    public override async Task<Contraption> Parse(CancellationToken token = default)
    {
        var lines = await GetAllNonEmptyLines(token);
        return new Contraption(lines);
    }
}
