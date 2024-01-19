using System.Text.RegularExpressions;
using AoC.Common.Input;

namespace AoC2023.Day08;

/// <summary>
/// Parser for <see cref="Traveller"/>.
/// </summary>
public partial class TravelParser : InputParser<Traveller>
{
    /// <summary>
    /// Constructs a new instance of <see cref="TravelParser"/>.
    /// </summary>
    /// <param name="filename">The input file name.</param>
    public TravelParser(string filename) : base(filename) { }

    /// <inheritdoc cref="InputParser{T}.Parse"/>
    public override async Task<Traveller> Parse(CancellationToken token = default)
    {
        var header = await ParseHeader(token);
        var map = await ParseMap(token);
        return new Traveller(header, map);
    }

    /// <summary>
    /// Parses the header line.
    /// </summary>
    /// <returns>The header line.</returns>
    private async Task<string> ParseHeader(CancellationToken token = default)
    {
        while (!Reader.EndOfStream)
        {
            var result = await Reader.ReadLineAsync(token);
            if (!string.IsNullOrEmpty(result))
                return result;
        }
        return string.Empty;
    }

    /// <summary>
    /// Parses the list of map directions.
    /// </summary>
    /// <returns>The list of map directions.</returns>
    private async Task<IReadOnlyDictionary<string, Direction>> ParseMap(CancellationToken token = default)
    {
        var result = new Dictionary<string, Direction>();
        var regex = GetDirectionRegex();
        await foreach (var line in GetNonEmptyLines(token))
        {
            var match = regex.Match(line);
            var groups = match.Groups;
            result[groups[1].Value] = new Direction(groups[2].Value, groups[3].Value);
        }
        return result;
    }

    [GeneratedRegex(@"(.*) = \((.*), (.*)\)")]
    private static partial Regex GetDirectionRegex();
}
