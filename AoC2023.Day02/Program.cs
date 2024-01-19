// https://adventofcode.com/2023/day/2

using AoC2023.Day02;

using var parser = new CubeShowParser(args[0]);
var input = await parser.Parse();

// part 1
var possibleCount = 0;
foreach (var (gameNum, cubeShows) in input)
{
    if (cubeShows.All(x => x.IsPossible(12, 13, 14)))
        possibleCount += gameNum;
}
Console.WriteLine(possibleCount);

// part 2
long powerSum = 0;
foreach (var (_, cubeShows) in input)
    powerSum += Helper.GetMinPower(cubeShows);

Console.WriteLine(powerSum);
