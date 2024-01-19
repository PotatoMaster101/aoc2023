// https://adventofcode.com/2023/day/23

using AoC2023.Day23;

using var parser = new MapParser(args[0]);
var input = await parser.Parse();

// part 1
Console.WriteLine(input.GetLongPath(true));

// part 2
Console.WriteLine(input.GetLongPath(false));
