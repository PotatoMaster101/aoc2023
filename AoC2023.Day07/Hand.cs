namespace AoC2023.Day07;

/// <summary>
/// Represents a hand in Camel Cards.
/// </summary>
public class Hand : IComparable<Hand>
{
    private readonly List<Card> _cards;
    private readonly Dictionary<Card, int> _cardFrequencies = new(5);

    /// <summary>
    /// Gets the bet amount.
    /// </summary>
    public int Bet { get; }

    /// <summary>
    /// Gets whether this hand has been promoted.
    /// </summary>
    public bool Promoted { get; private set; }

    /// <summary>
    /// Constructs a new instance of <see cref="Hand"/>.
    /// </summary>
    /// <param name="cards">The cards in hand.</param>
    /// <param name="bet">The bet amount.</param>
    public Hand(IEnumerable<Card> cards, int bet)
    {
        Bet = bet;
        _cards = cards.ToList();
        PopulateCardFrequencies();
    }

    /// <inheritdoc cref="IComparable{T}.CompareTo"/>
    public int CompareTo(Hand? other)
    {
        if (other is null)
            return -1;

        var thisType = Promoted ? GetPromotedHandType() : GetHandType();
        var otherType = Promoted ? other.GetPromotedHandType() : other.GetHandType();
        if (thisType != otherType)
            return thisType - otherType;

        for (var i = 0; i < _cards.Count; i++)
        {
            var compareCard = _cards[i].CompareTo(other._cards[i]);
            if (compareCard != 0)
                return compareCard;
        }
        return 0;
    }

    /// <inheritdoc cref="object.ToString"/>
    public override string ToString()
    {
        return string.Join(string.Empty, _cards);
    }

    /// <summary>
    /// Promotes this hand and increase the type for joker cards.
    /// </summary>
    public void Promote()
    {
        Promoted = true;
        foreach (var card in _cards.Where(x => x.IsJoker))
            card.Promoted = true;
    }

    /// <summary>
    /// Gets the type of this hand after joker has been promoted.
    /// </summary>
    /// <returns>The type of this hand after joker has been promoted.</returns>
    private HandType GetPromotedHandType()
    {
        if (!_cardFrequencies.TryGetValue(Card.Joker, out var jCount))
            return GetHandType();

        return _cardFrequencies.Count switch
        {
            1 => HandType.FiveOfAKind,
            2 => HandType.FiveOfAKind,
            3 => GetPromotedThreeFrequenciesHandType(jCount),
            4 => GetPromotedFourFrequenciesHandType(jCount),
            _ => HandType.OnePair
        };
    }

    /// <summary>
    /// Gets the type of this hand.
    /// </summary>
    /// <returns>The type of this hand.</returns>
    private HandType GetHandType()
    {
        return _cardFrequencies.Count switch
        {
            1 => HandType.FiveOfAKind,
            2 => GetTwoFrequenciesHandType(),
            3 => GetThreeFrequenciesHandType(),
            4 => HandType.OnePair,
            _ => HandType.HighCard
        };
    }

    /// <summary>
    /// Gets the hand type when there are 2 card frequencies.
    /// </summary>
    /// <returns>The hand type when there are 2 card frequencies.</returns>
    private HandType GetTwoFrequenciesHandType()
    {
        var first = _cardFrequencies[_cards[0]];
        return first is 4 or 1 ? HandType.FourOfAKind : HandType.FullHouse;
    }

    /// <summary>
    /// Gets the hand type when there are 3 card frequencies.
    /// </summary>
    /// <returns>The hand type when there are 3 card frequencies.</returns>
    private HandType GetThreeFrequenciesHandType()
    {
        return _cardFrequencies.Values.Any(x => x == 3) ? HandType.ThreeOfAKind : HandType.TwoPair;
    }

    /// <summary>
    /// Gets the promoted hand type when there are 4 card frequencies.
    /// </summary>
    /// <param name="jCount">The number of joker cards.</param>
    /// <returns>The hand type when there are 4 card frequencies.</returns>
    private HandType GetPromotedFourFrequenciesHandType(int jCount)
    {
        if (jCount == 2)
            return HandType.ThreeOfAKind;   // JJ123 -> 11123

        var mostFrequent = _cardFrequencies.Max(x => x.Value);
        return mostFrequent switch
        {
            2 => HandType.ThreeOfAKind,    // J1123 -> 11123
            _ => HandType.OnePair           // J1234 -> 11234
        };
    }

    /// <summary>
    /// Gets the promoted hand type when there are 3 card frequencies.
    /// </summary>
    /// <param name="jCount">The number of joker cards.</param>
    /// <returns>The hand type when there are 3 card frequencies.</returns>
    private HandType GetPromotedThreeFrequenciesHandType(int jCount)
    {
        if (jCount is 2 or 3)
            return HandType.FourOfAKind;        // JJ112 -> 11112 or JJJ12 -> 11112

        var mostFrequent = _cardFrequencies.Max(x => x.Value);
        return mostFrequent switch
        {
            2 => HandType.FullHouse,    // J1122 -> 11122
            _ => HandType.FourOfAKind   // J1112 -> 11112
        };
    }

    /// <summary>
    /// Populates the card frequencies.
    /// </summary>
    private void PopulateCardFrequencies()
    {
        _cardFrequencies.Clear();
        foreach (var card in _cards)
        {
            if (!_cardFrequencies.TryAdd(card, 1))
                _cardFrequencies[card]++;
        }
    }
}
