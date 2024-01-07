using System.Text.RegularExpressions;
using AoC.Common.Grid;
using AoC.Common.Input;

namespace AoC2023.Day18;

/// <summary>
/// Parser for <see cref="DigPlan"/>.
/// </summary>
public partial class DigPlanParser : InputParser<DigPlan>
{
    /// <summary>
    /// Constructs a new instance of <see cref="DigPlanParser"/>.
    /// </summary>
    /// <param name="filename">The input file name.</param>
    public DigPlanParser(string filename) : base(filename) { }

    /// <inheritdoc cref="InputParser{T}.Parse"/>
    public override async Task<DigPlan> Parse(CancellationToken token = default)
    {
        var regex = GetInstructionRegex();
        var instructions = await GetNonEmptyLines(token)
            .Select(line => regex.Match(line))
            .Select(match => ParseInstruction(match.Groups))
            .ToListAsync(token);
        return new DigPlan(instructions);
    }

    /// <summary>
    /// Parses an instruction from regex capture groups.
    /// </summary>
    /// <param name="groups">The regex capture groups.</param>
    /// <returns>The parsed instruction.</returns>
    private static Instruction ParseInstruction(GroupCollection groups)
    {
        var direction = groups[1].Value switch
        {
            "R" => Direction.Right,
            "D" => Direction.Down,
            "L" => Direction.Left,
            _ => Direction.Up
        };
        return new Instruction(groups[3].Value, direction, int.Parse(groups[2].Value));
    }

    [GeneratedRegex(@"(.*) (.*) \((.*)\)")]
    private static partial Regex GetInstructionRegex();
}
