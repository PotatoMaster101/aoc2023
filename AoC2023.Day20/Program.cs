// https://adventofcode.com/2023/day/20

using AoC2023.Day20;

using var parser = new ModuleCollectionParser(args[0]);
var input = await parser.Parse();

// part 1
Console.WriteLine(input.GetPulseProducts(1000));

// part 2
Console.WriteLine(input.GetRxHandledCount());
