// https://adventofcode.com/2023/day/18

using AoC2023.Day18;

using var parser = new DigPlanParser(args[0]);
var input = await parser.Parse();

// part 1
Console.WriteLine(input.GetDigArea());

// part 2
Console.WriteLine(input.GetEncodedArea());
