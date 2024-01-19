// https://adventofcode.com/2023/day/6

using AoC2023.Day06;

const StringSplitOptions splitOpt = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;
var lines = await File.ReadAllLinesAsync(args[0]);
var timeStr = lines[0].Split(":", splitOpt)[1];
var times = timeStr.Split(" ", splitOpt).Select(long.Parse).ToList();
var distStr = lines[1].Split(":", splitOpt)[1];
var dists = distStr.Split(" ", splitOpt).Select(long.Parse).ToList();

// part 1
long result = 1;
for (var i = 0; i < times.Count; i++)
{
    var race = new Race(times[i], dists[i]);
    result *= race.CountWinnings();
}
Console.WriteLine(result);

// part 2
var combinedTime = long.Parse(timeStr.Replace(" ", string.Empty));
var combinedDist = long.Parse(distStr.Replace(" ", string.Empty));
var longRace = new Race(combinedTime, combinedDist);
Console.WriteLine(longRace.CountWinnings());
