// https://adventofcode.com/2023/day/8

using AoC2023.Day08;

using var parser = new TravelParser(args[0]);
var traveller = await parser.Parse();

// part 1
Console.WriteLine(traveller.GetTotalSteps());

// part 2
Console.WriteLine(traveller.GetSimultaneousSteps());
