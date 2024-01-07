// https://adventofcode.com/2023/day/11

using AoC2023.Day11;

using var parser = new ImageParser(args[0]);
var input = await parser.Parse();

// part 1
Console.WriteLine(input.GetDistanceSum(2));

// part 2
Console.WriteLine(input.GetDistanceSum(1000000));
