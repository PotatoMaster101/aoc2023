// https://adventofcode.com/2023/day/5

using AoC2023.Day05;

using var parser = new AlmanacParser(args[0]);
var almanac = await parser.Parse();

// part 1
Console.WriteLine(almanac.GetMinSeed());

// part 2
Console.WriteLine(almanac.GetMinSeedLocation(1000000));
