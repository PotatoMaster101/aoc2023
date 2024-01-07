using AoC.Common.Input;

namespace AoC2023.Day07;

/// <summary>
/// Parser for <see cref="Hand"/>.
/// </summary>
public class HandParser : InputParser<IReadOnlyCollection<Hand>>
{
    /// <summary>
    /// Constructs a new instance of <see cref="HandParser"/>.
    /// </summary>
    /// <param name="filename">The input file name.</param>
    public HandParser(string filename) : base(filename) { }

    /// <inheritdoc cref="InputParser{T}.Parse"/>
    public override async Task<IReadOnlyCollection<Hand>> Parse(CancellationToken token = default)
    {
        var result = new List<Hand>();
        await foreach (var line in GetNonEmptyLines(token))
        {
            var spaceSplits = line.Split(" ", SplitOpt);
            var cards = spaceSplits[0].Select(x => new Card(x));
            result.Add(new Hand(cards, int.Parse(spaceSplits[1])));
        }
        return result;
    }
}
