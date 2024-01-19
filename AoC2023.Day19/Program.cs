// https://adventofcode.com/2023/day/19

using AoC2023.Day19;

using var parser = new OperationParser(args[0]);
var input = await parser.Parse();

// part 1
Console.WriteLine(input.SumPassedRatings());

// part 2
Console.WriteLine(input.CountPasses(1, 4000));
