// https://adventofcode.com/2023/day/14

using AoC.Common.Grid;
using AoC2023.Day14;

using var parser = new PlatformParser(args[0]);
var input = await parser.Parse();

// part 1
input.MoveRoundRocks(Direction.Up);
Console.WriteLine(input.CountWeight());

// part 2
input = await parser.Parse();
input.RunCycle(1000000000);
Console.WriteLine(input.CountWeight());
