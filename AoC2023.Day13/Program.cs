// https://adventofcode.com/2023/day/13

using AoC2023.Day13;

using var parser = new ReflectionParser(args[0]);
var input = await parser.Parse();

// part 1
var result = 0;
foreach (var reflection in input)
{
    result += reflection.CountColumnReflections();
    result += 100 * reflection.CountRowReflections();
}
Console.WriteLine(result);

// part 2
result = 0;
foreach (var reflection in input)
{
    result += reflection.CountColumnReflections(1);
    result += 100 * reflection.CountRowReflections(1);
}
Console.WriteLine(result);
