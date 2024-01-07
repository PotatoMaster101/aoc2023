// https://adventofcode.com/2023/day/1

using AoC2023.Day01;

using var parser = new LineParser(args[0]);
var input = await parser.Parse();

// part 1
var sum = 0;
foreach (var line in input)
{
    var firstVal = line.GetFirstNumericDigit().Value;
    var lastVal = line.GetLastNumericDigit().Value;
    sum += firstVal * 10 + lastVal;
}
Console.WriteLine(sum);

// part 2
sum = 0;
foreach (var line in input)
{
    var firstDigit = line.GetFirstNumericDigit();
    var firstWord = line.GetFirstNumericWord();
    var lastDigit = line.GetLastNumericDigit();
    var lastWord = line.GetLastNumericWord();
    var firstVal = firstDigit.GetBefore(firstWord).Value;
    var lastVal = lastDigit.GetAfter(lastWord).Value;
    sum += firstVal * 10 + lastVal;
}
Console.WriteLine(sum);
