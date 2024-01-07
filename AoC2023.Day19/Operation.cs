using AoC.Common.Ranges;

namespace AoC2023.Day19;

/// <summary>
/// Represents the entire operation.
/// </summary>
public class Operation
{
    private const string Accepted = "A";
    private const string Rejected = "R";
    private const string Start = "in";

    /// <summary>
    /// Gets the operation workflows.
    /// </summary>
    public IReadOnlyDictionary<string, IReadOnlyList<Rule>> Workflows { get; }

    /// <summary>
    /// Gets the list of ratings.
    /// </summary>
    public IReadOnlyList<Rating> Ratings { get; }

    /// <summary>
    /// Constructs a new instance of <see cref="Operation"/>.
    /// </summary>
    /// <param name="workflows">The operation workflows.</param>
    /// <param name="ratings">The list of ratings.</param>
    public Operation(IReadOnlyDictionary<string, IReadOnlyList<Rule>> workflows, IReadOnlyList<Rating> ratings)
    {
        Workflows = workflows;
        Ratings = ratings;
    }

    /// <summary>
    /// Returns the sum of passed ratings.
    /// </summary>
    /// <returns>The sum of passed ratings.</returns>
    public long SumPassedRatings()
    {
        return Ratings
            .Where(rating => CheckRatingPasses(rating))
            .Aggregate(0L, (current, rating) => current + rating.Sum());
    }

    /// <summary>
    /// Counts the number of ratings that will pass the rules.
    /// </summary>
    /// <param name="minRating">The minimum rating.</param>
    /// <param name="maxRating">The maximum rating.</param>
    /// <returns>The number of ratings that will pass the rules.</returns>
    public long CountPasses(int minRating, int maxRating)
    {
        var range = new IntegerRange(minRating, maxRating);
        var start = new Dictionary<char, IntegerRange> { ['x'] = range, ['m'] = range, ['a'] = range, ['s'] = range };
        var passed = new List<IReadOnlyDictionary<char, IntegerRange>>();

        var queue = new Queue<OperationRange>();
        queue.Enqueue(new OperationRange(start, Start, 0));
        while (queue.TryDequeue(out var oprRange))
        {
            var rules = Workflows[oprRange.WorkflowName];
            foreach (var split in oprRange.SplitRange(rules[oprRange.RuleIndex]))
            {
                if (split.WorkflowName == Accepted)
                    passed.Add(split.Ranges);
                else if (split.WorkflowName != Rejected)
                    queue.Enqueue(split);
            }
        }
        return passed.Sum(x => x['x'].Count * x['m'].Count * x['a'].Count * x['s'].Count);
    }

    /// <summary>
    /// Checks whether a rating passed the workflow.
    /// </summary>
    /// <param name="rating">The rating to check.</param>
    /// <param name="workflowName">The name of the workflow.</param>
    /// <returns>Whether the rating passes workflow.</returns>
    private bool CheckRatingPasses(Rating rating, string workflowName = Start)
    {
        if (workflowName == Rejected)
            return false;
        if (workflowName == Accepted)
            return true;

        foreach (var rule in Workflows[workflowName])
        {
            if (rule.CanPassRule(rating))
                return CheckRatingPasses(rating, rule.JumpLabel);
        }

        // should not reach here as the last rule will always jump
        return false;
    }
}
