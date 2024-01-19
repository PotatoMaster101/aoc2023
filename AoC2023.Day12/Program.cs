// https://adventofcode.com/2023/day/12

using AoC2023.Day12;

using var parser = new SpringRowParser(args[0]);
var input = await parser.Parse();

// part 1
Console.WriteLine(input.Sum(x => x.GetCount()));

// part 2
Console.WriteLine(input.Sum(x => x.GetCount(5)));
