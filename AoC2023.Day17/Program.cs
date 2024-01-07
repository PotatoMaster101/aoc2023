// https://adventofcode.com/2023/day/17

using AoC2023.Day17;

using var parser = new MapParser(args[0]);
var input = await parser.Parse();

// part 1
Console.WriteLine(input.GetMinHeatLoss(0, 3));

// part 2
Console.WriteLine(input.GetMinHeatLoss(4, 10));
