using AoC.Common.Input;

namespace AoC2023.Day01;

/// <summary>
/// Parser for <see cref="Line"/>.
/// </summary>
public class LineParser : InputParser<IReadOnlyCollection<Line>>
{
    /// <summary>
    /// Constructs a new instance of <see cref="LineParser"/>.
    /// </summary>
    /// <param name="filename">The input file name.</param>
    public LineParser(string filename) : base(filename) { }

    /// <inheritdoc cref="InputParser{T}.Parse"/>
    public override async Task<IReadOnlyCollection<Line>> Parse(CancellationToken token = default)
    {
        return await GetNonEmptyLines(token).Select(line => new Line(line)).ToListAsync(token);
    }
}
