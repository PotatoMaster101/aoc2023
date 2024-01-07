namespace AoC2023.Day19;

/// <summary>
/// Represents a rule.
/// </summary>
public class Rule
{
    /// <summary>
    /// Gets whether the rule is a comparison rule.
    /// </summary>
    public bool IsComparison { get; }

    /// <summary>
    /// Gets the rule's jump location.
    /// </summary>
    public string JumpLabel { get; }

    /// <summary>
    /// Gets the rule's category.
    /// </summary>
    public char Category { get; } = 'x';

    /// <summary>
    /// Gets the rule's operator.
    /// </summary>
    public char Operator { get; } = '<';

    /// <summary>
    /// Gets the rule's comparison value.
    /// </summary>
    public int CompareValue { get; }

    /// <summary>
    /// Constructs a new instance of <see cref="Rule"/>.
    /// </summary>
    /// <param name="jumpLabel">The jump label.</param>
    public Rule(string jumpLabel)
    {
        IsComparison = false;
        JumpLabel = jumpLabel;
    }

    /// <summary>
    /// Constructs a new instance of <see cref="Rule"/>.
    /// </summary>
    /// <param name="category">The rule's category.</param>
    /// <param name="opr">The rule's operator.</param>
    /// <param name="compareValue">The rule's comparison value.</param>
    /// <param name="jumpLabel">The jump label.</param>
    public Rule(char category, char opr, int compareValue, string jumpLabel)
    {
        IsComparison = true;
        Category = category;
        Operator = opr;
        CompareValue = compareValue;
        JumpLabel = jumpLabel;
    }

    /// <summary>
    /// Determines whether this rule passes for a specific rating.
    /// </summary>
    /// <param name="rating">The rating to check.</param>
    /// <returns>Whether this rule passes for the specified rating.</returns>
    public bool CanPassRule(Rating rating)
    {
        return !IsComparison || rating.CanPassRule(Category, CompareValue, Operator);
    }
}
