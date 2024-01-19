using AoC.Common.Input;

namespace AoC2023.Day12;

/// <summary>
/// Parser for <see cref="SpringRow"/>.
/// </summary>
public class SpringRowParser : InputParser<IReadOnlyList<SpringRow>>
{
    /// <summary>
    /// Constructs a new instance of <see cref="SpringRowParser"/>.
    /// </summary>
    /// <param name="filename">The input file name.</param>
    public SpringRowParser(string filename) : base(filename) { }

    /// <inheritdoc cref="InputParser{T}.Parse"/>
    public override async Task<IReadOnlyList<SpringRow>> Parse(CancellationToken token = default)
    {
        var result = new List<SpringRow>();
        await foreach (var line in GetNonEmptyLines(token))
        {
            var splits = line.Split(" ", SplitOpt);
            var damageGroups = splits[1].Split(",", SplitOpt).Select(int.Parse);
            result.Add(new SpringRow(splits[0], damageGroups));
        }
        return result;
    }
}
