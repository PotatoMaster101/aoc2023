namespace AoC2023.Day04;

/// <summary>
/// Represents a card.
/// </summary>
public class Card
{
    /// <summary>
    /// Gets the set of winning numbers.
    /// </summary>
    public IReadOnlySet<int> WinningNumbers { get; }

    /// <summary>
    /// Gets the list of numbers you have.
    /// </summary>
    public IReadOnlyList<int> NumbersYouHave { get; }

    /// <summary>
    /// Gets or sets the count of this card (original + copies).
    /// </summary>
    public int Count { get; set; } = 1;

    /// <summary>
    /// Constructs a new instance of <see cref="Card"/>.
    /// </summary>
    /// <param name="winningNums">The list of winning numbers.</param>
    /// <param name="nums">The list of numbers you have.</param>
    public Card(IEnumerable<int> winningNums, IEnumerable<int> nums)
    {
        WinningNumbers = new HashSet<int>(winningNums);
        NumbersYouHave = new List<int>(nums);
    }

    /// <summary>
    /// Gets the score for this card.
    /// </summary>
    /// <returns>The score for this card.</returns>
    public int GetScore()
    {
        if (NumbersYouHave.Count == 0)
            return 0;

        var score = 0;
        foreach (var num in NumbersYouHave)
        {
            if (WinningNumbers.Contains(num))
                score = score == 0 ? 1 : score * 2;
        }
        return score;
    }

    /// <summary>
    /// Gets the count of matching cards.
    /// </summary>
    /// <returns>The count of matching cards.</returns>
    public int GetMatchCount()
    {
        return NumbersYouHave.Count == 0 ? 0 : WinningNumbers.Intersect(NumbersYouHave).Count();
    }
}
