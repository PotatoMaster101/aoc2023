using AoC.Common.Ranges;

namespace AoC2023.Day19;

/// <summary>
/// Represents a range of operations.
/// </summary>
public class OperationRange
{
    /// <summary>
    /// Gets the dictionary of ranges.
    /// </summary>
    public IReadOnlyDictionary<char, IntegerRange> Ranges { get; }

    /// <summary>
    /// Gets the workflow name.
    /// </summary>
    public string WorkflowName { get; }

    /// <summary>
    /// Gets the index of the rule.
    /// </summary>
    public int RuleIndex { get; }

    /// <summary>
    /// Constructs a new instance of <see cref="OperationRange"/>.
    /// </summary>
    /// <param name="ranges">The input ranges.</param>
    /// <param name="workflowName">The name of the workflow.</param>
    /// <param name="ruleIndex">The index of the rule.</param>
    public OperationRange(IReadOnlyDictionary<char, IntegerRange> ranges, string workflowName, int ruleIndex)
    {
        Ranges = ranges;
        WorkflowName = workflowName;
        RuleIndex = ruleIndex;
    }

    /// <summary>
    /// Splits this range based on the specified rule.
    /// </summary>
    /// <param name="rule">The rule to split the range.</param>
    /// <returns>The result of splitting.</returns>
    public IEnumerable<OperationRange> SplitRange(Rule rule)
    {
        if (!rule.IsComparison)
            return new List<OperationRange> { new(Ranges, rule.JumpLabel, 0) };

        var passed = new Dictionary<char, IntegerRange>(Ranges);
        var failed = new Dictionary<char, IntegerRange>(Ranges);
        var range = Ranges[rule.Category];
        if (rule.Operator == '<')
        {
            passed[rule.Category] = range.SplitLowerRange(rule.CompareValue);
            failed[rule.Category] = range.SplitUpperRange(rule.CompareValue, true);
        }
        else
        {
            passed[rule.Category] = range.SplitUpperRange(rule.CompareValue);
            failed[rule.Category] = range.SplitLowerRange(rule.CompareValue, true);
        }

        return new[]
        {
            new OperationRange(passed, rule.JumpLabel, 0),
            new OperationRange(failed, WorkflowName,  RuleIndex + 1)
        };
    }
}
