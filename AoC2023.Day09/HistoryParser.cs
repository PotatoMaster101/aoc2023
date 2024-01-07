using AoC.Common.Input;

namespace AoC2023.Day09;

/// <summary>
/// Parser for <see cref="History"/>.
/// </summary>
public class HistoryParser : InputParser<IReadOnlyCollection<History>>
{
    /// <summary>
    /// Constructs a new instance of <see cref="HistoryParser"/>.
    /// </summary>
    /// <param name="filename">The input file name.</param>
    public HistoryParser(string filename) : base(filename) { }

    /// <inheritdoc cref="InputParser{T}.Parse"/>
    public override async Task<IReadOnlyCollection<History>> Parse(CancellationToken token = default)
    {
        var result = new List<History>();
        await foreach (var line in GetNonEmptyLines(token))
        {
            var splits = line.Split(" ", SplitOpt);
            result.Add(new History(splits.Select(long.Parse)));
        }
        return result;
    }
}
