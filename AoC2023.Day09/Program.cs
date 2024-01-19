// https://adventofcode.com/2023/day/9

using AoC2023.Day09;

using var parser = new HistoryParser(args[0]);
var input = await parser.Parse();

// part 1
Console.WriteLine(input.Sum(x => x.PredictNextValue()));

// part 2
Console.WriteLine(input.Sum(x => x.PredictPreviousValue()));
