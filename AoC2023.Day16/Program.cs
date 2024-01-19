// https://adventofcode.com/2023/day/16

using AoC.Common.Grid;
using AoC2023.Day16;

using var parser = new ContraptionParser(args[0]);
var input = await parser.Parse();

// part 1
Console.WriteLine(input.CountEnergisedTiles(Position.Origin, Direction.Right));

// part 2
var topRow = input.Bounds.GetRowPositions(0).ToList();
var topRowMax = topRow.Max(x => input.CountEnergisedTiles(x, Direction.Down));
var bottomRow = input.Bounds.GetRowPositions(input.Bounds.RowCount32 - 1).ToList();
var bottomRowMax = bottomRow.Max(x => input.CountEnergisedTiles(x, Direction.Up));
var leftCol = input.Bounds.GetColumnPositions(0).ToList();
var leftColMax = leftCol.Max(x => input.CountEnergisedTiles(x, Direction.Right));
var rightCol = input.Bounds.GetColumnPositions(input.Bounds.ColumnCount32 - 1).ToList();
var rightColMax = rightCol.Max(x => input.CountEnergisedTiles(x, Direction.Left));
var results = new[] { topRowMax, bottomRowMax, leftColMax, rightColMax };
Console.WriteLine(results.Max());
