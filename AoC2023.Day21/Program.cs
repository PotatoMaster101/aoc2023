// https://adventofcode.com/2023/day/21

using AoC2023.Day21;

using var parser = new MapParser(args[0]);
var input = await parser.Parse();

// part 1
Console.WriteLine(input.GetPlotsCount(64));

// part 2
Console.WriteLine(input.GetRepeatedPlotsCount(26501365));
