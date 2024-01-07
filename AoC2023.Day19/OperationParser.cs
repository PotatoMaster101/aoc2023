using System.Text.RegularExpressions;
using AoC.Common.Input;

namespace AoC2023.Day19;

/// <summary>
/// Parser for <see cref="Operation"/>.
/// </summary>
public partial class OperationParser : InputParser<Operation>
{
    /// <summary>
    /// Constructs a new instance of <see cref="OperationParser"/>.
    /// </summary>
    /// <param name="filename">The input file name.</param>
    public OperationParser(string filename) : base(filename) { }

    /// <inheritdoc cref="InputParser{T}.Parse"/>
    public override async Task<Operation> Parse(CancellationToken token = default)
    {
        var rules = await ParseWorkflows(token);
        var ratings = await ParseRatings(token);
        return new Operation(rules, ratings);
    }

    /// <summary>
    /// Parses the workflows from the input.
    /// </summary>
    /// <param name="token">The cancellation token for cancelling this operation.</param>
    /// <returns>The workflows from the input.</returns>
    private async Task<IReadOnlyDictionary<string, IReadOnlyList<Rule>>> ParseWorkflows(CancellationToken token = default)
    {
        var workflows = new Dictionary<string, IReadOnlyList<Rule>>();
        var regex = GetWorkflowRegex();
        while (!Reader.EndOfStream)
        {
            var line = await Reader.ReadLineAsync(token);
            if (string.IsNullOrEmpty(line))
                break;

            var groups = regex.Match(line).Groups;
            workflows[groups[1].Value] = ParseRules(groups[2].Value);
        }
        return workflows;
    }

    /// <summary>
    /// Parses the ratings from the input.
    /// </summary>
    /// <param name="token">The cancellation token for cancelling this operation.</param>
    /// <returns>The ratings from the input.</returns>
    private ValueTask<List<Rating>> ParseRatings(CancellationToken token = default)
    {
        var regex = GetRatingRegex();
        return GetNonEmptyLines(token)
            .Select(line => regex.Match(line).Groups)
            .Select(groups => new Rating(
                int.Parse(groups[1].Value),
                int.Parse(groups[2].Value),
                int.Parse(groups[3].Value),
                int.Parse(groups[4].Value)))
            .ToListAsync(token);
    }

    /// <summary>
    /// Parses a list of rules.
    /// </summary>
    /// <param name="rules">The string containing a list of rules.</param>
    /// <returns>The rules to parse.</returns>
    private static IReadOnlyList<Rule> ParseRules(string rules)
    {
        return rules
            .Split(',', SplitOpt)
            .Select(ParseRule)
            .ToList();
    }

    /// <summary>
    /// Parses a rule.
    /// </summary>
    /// <param name="rule">The string containing a rule.</param>
    /// <returns>The rule to parse.</returns>
    private static Rule ParseRule(string rule)
    {
        var colonSplits = rule.Split(':', SplitOpt);
        if (colonSplits.Length == 1)
            return new Rule(colonSplits[0]);

        var compareValue = int.Parse(colonSplits[0][2..]);
        return new Rule(colonSplits[0][0], colonSplits[0][1], compareValue, colonSplits[1]);
    }

    [GeneratedRegex("(.*){(.*)}")]
    private static partial Regex GetWorkflowRegex();

    [GeneratedRegex("{x=([0-9]+),m=([0-9]+),a=([0-9]+),s=([0-9]+)}")]
    private static partial Regex GetRatingRegex();
}
