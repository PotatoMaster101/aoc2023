// https://adventofcode.com/2023/day/4

using AoC2023.Day04;

using var parser = new CardParser(args[0]);
var input = await parser.Parse();

// part 1
Console.WriteLine(input.Values.Sum(x => x.GetScore()));

// part 2
foreach (var (cardNum, card) in input.OrderBy(x => x.Key))
{
    var nextCardsRange = card.GetMatchCount() + cardNum + 1;
    for (var nextCardNum = cardNum + 1; nextCardNum < nextCardsRange; nextCardNum++)
    {
        if (input.TryGetValue(nextCardNum, out var nextCard))
            nextCard.Count += card.Count;
    }
}
Console.WriteLine(input.Sum(x => x.Value.Count));
