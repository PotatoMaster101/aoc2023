// https://adventofcode.com/2023/day/10

using AoC2023.Day10;

using var parser = new MapParser(args[0]);
var input = await parser.Parse();

// part 1
Console.WriteLine(input.GetMaxDistance());

// part 2
Console.WriteLine(input.GetEnclosedCount());
