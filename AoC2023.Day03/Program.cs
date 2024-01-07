// https://adventofcode.com/2023/day/3

using AoC2023.Day03;

var input = await File.ReadAllLinesAsync(args[0]);
var schematic = new Schematic(input);

// part 1
var parts = schematic.GetPartNumbers();
Console.WriteLine(parts.Sum());

// part 2
var gears = schematic.GetGearNumbers();
Console.WriteLine(gears.Sum());
