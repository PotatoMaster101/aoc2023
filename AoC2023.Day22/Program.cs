// https://adventofcode.com/2023/day/22

using AoC2023.Day22;

using var parser = new BrickCollectionParser(args[0]);
var input = await parser.Parse();

// part 1
Console.WriteLine(input.CountDisintegrate());

// part 2
Console.WriteLine(input.CountDisintegrateFalling());
