using System.Collections.Frozen;

namespace AoC2023.Day07;

/// <summary>
/// Represents a card.
/// </summary>
public class Card : IComparable<Card>, IEquatable<Card>
{
    private const char JokerChar = 'J';
    private static readonly IReadOnlyDictionary<char, int> CardStrength = new Dictionary<char, int>
    {
        ['2'] = 1,
        ['3'] = 2,
        ['4'] = 3,
        ['5'] = 4,
        ['6'] = 5,
        ['7'] = 6,
        ['8'] = 7,
        ['9'] = 8,
        ['T'] = 9,
        ['J'] = 10,
        ['Q'] = 11,
        ['K'] = 12,
        ['A'] = 13
    }.ToFrozenDictionary();

    /// <summary>
    /// Gets the joker card.
    /// </summary>
    public static readonly Card Joker = new(JokerChar);

    /// <summary>
    /// Gets the content of the card.
    /// </summary>
    public char Content { get; }

    /// <summary>
    /// Gets whether this card is a joker.
    /// </summary>
    public bool IsJoker => Content == JokerChar;

    /// <summary>
    /// Gets or sets whether this card has been promoted.
    /// </summary>
    public bool Promoted { get; set; }

    /// <summary>
    /// Constructs a new instance of <see cref="Card"/>.
    /// </summary>
    /// <param name="content">The content of the card.</param>
    public Card(char content)
    {
        Content = content;
    }

    /// <inheritdoc cref="IComparable{T}.CompareTo"/>
    public int CompareTo(Card? other)
    {
        if (other is null)
            return -1;

        return Promoted switch
        {
            false when other.Promoted => 1,
            false when !other.Promoted => CardStrength[Content] - CardStrength[other.Content],
            true when !other.Promoted => -1,
            _ => 0
        };
    }

    /// <inheritdoc cref="IEquatable{T}.Equals(T)"/>
    public bool Equals(Card? other)
    {
        return other is not null && Content == other.Content;
    }

    /// <inheritdoc cref="object.Equals(object?)"/>
    public override bool Equals(object? obj)
    {
        return obj is Card c && Equals(c);
    }

    /// <inheritdoc cref="object.GetHashCode"/>
    public override int GetHashCode()
    {
        return Content.GetHashCode();
    }

    /// <inheritdoc cref="object.ToString"/>
    public override string ToString()
    {
        return Content.ToString();
    }

    public static bool operator ==(Card left, Card right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Card left, Card right)
    {
        return !(left == right);
    }
}
