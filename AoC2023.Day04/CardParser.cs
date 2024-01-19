using AoC.Common.Input;

namespace AoC2023.Day04;

/// <summary>
/// Parser for <see cref="Card"/>.
/// </summary>
public class CardParser : InputParser<IReadOnlyDictionary<int, Card>>
{
    /// <summary>
    /// Constructs a new instance of <see cref="CardParser"/>.
    /// </summary>
    /// <param name="filename">The input file name.</param>
    public CardParser(string filename) : base(filename) { }

    /// <inheritdoc cref="InputParser{T}.Parse"/>
    public override async Task<IReadOnlyDictionary<int, Card>> Parse(CancellationToken token = default)
    {
        var result = new Dictionary<int, Card>();
        await foreach (var line in GetNonEmptyLines(token))
        {
            var colonSplits = line.Split(":");
            var cardNum = int.Parse(colonSplits[0][5..]);       // "Card " ends at index 5
            var barSplits = colonSplits[1].Split("|", SplitOpt);
            var winningNumsStr = barSplits[0].Split(" ", SplitOpt);
            var winningNums = winningNumsStr.Select(int.Parse);
            var numsYouHaveStr = barSplits[1].Split(" ", SplitOpt);
            var numsYouHave = numsYouHaveStr.Select(int.Parse);
            result[cardNum] = new Card(winningNums, numsYouHave);
        }
        return result;
    }
}
