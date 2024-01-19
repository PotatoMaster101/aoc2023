using AoC.Common.Input;

namespace AoC2023.Day02;

/// <summary>
/// Parser for <see cref="CubeShow"/>.
/// </summary>
public class CubeShowParser : InputParser<IReadOnlyDictionary<int, IReadOnlyCollection<CubeShow>>>
{
    /// <summary>
    /// Constructs a new instance of <see cref="CubeShowParser"/>.
    /// </summary>
    /// <param name="filename">The input file name.</param>
    public CubeShowParser(string filename) : base(filename) { }

    /// <inheritdoc cref="InputParser{T}.Parse"/>
    public override async Task<IReadOnlyDictionary<int, IReadOnlyCollection<CubeShow>>> Parse(CancellationToken token = default)
    {
        var result = new Dictionary<int, IReadOnlyCollection<CubeShow>>();
        await foreach (var line in GetNonEmptyLines(token))
        {
            var splits = line.Split(":", SplitOpt);
            var gameNum = int.Parse(splits[0].Split(" ", SplitOpt)[1]);
            var semiColonSplits = splits[1].Split(";", SplitOpt);
            var cubeEntries = semiColonSplits.Select(split => new CubeShow(split)).ToList();
            result[gameNum] = cubeEntries;
        }
        return result;
    }
}
