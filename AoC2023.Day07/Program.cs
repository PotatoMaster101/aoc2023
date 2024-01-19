// https://adventofcode.com/2023/day/7

using AoC2023.Day07;

using var parser = new HandParser(args[0]);
var hands = await parser.Parse();

// part 1
long result = 0;
var handsSorted = hands.OrderBy(x => x).ToList();
for (var i = 0; i < handsSorted.Count; i++)
    result += handsSorted[i].Bet * (i + 1);

Console.WriteLine(result);

// part 2
foreach (var hand in hands)
    hand.Promote();

result = 0;
handsSorted = hands.OrderBy(x => x).ToList();
for (var i = 0; i < handsSorted.Count; i++)
    result += handsSorted[i].Bet * (i + 1);
Console.WriteLine(result);
