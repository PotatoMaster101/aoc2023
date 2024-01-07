// https://adventofcode.com/2023/day/15

using AoC2023.Day15;

var lines = await File.ReadAllLinesAsync(args[0]);
var line = lines[0];
var splits = line.Split(",");

// part 1
var result = splits.Sum(Hash.Run);
Console.WriteLine(result);

// part 2
var boxes = new Boxes();
foreach (var split in splits)
    boxes.RunOperation(split);

Console.WriteLine(boxes.GetFocalPower());
