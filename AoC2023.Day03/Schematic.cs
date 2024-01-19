using AoC.Common.Grid;
using AoC.Common.Helpers;

namespace AoC2023.Day03;

/// <summary>
/// Represents an engine schematic.
/// </summary>
public class Schematic
{
    /// <summary>
    /// Boundary for this schematic.
    /// </summary>
    private readonly Boundary _boundary;

    /// <summary>
    /// Gets the contents of the schematic.
    /// </summary>
    public string[] Content { get; }

    /// <summary>
    /// Constructs a new instance of <see cref="Schematic"/>.
    /// </summary>
    /// <param name="content">The contents of the schematic.</param>
    public Schematic(string[] content)
    {
        Content = content;
        _boundary = new Boundary(content.Length, content[0].Length);
    }

    /// <summary>
    /// Gets all part numbers from this schematic.
    /// </summary>
    /// <returns>All part numbers from this schematic.</returns>
    public IEnumerable<int> GetPartNumbers()
    {
        var result = new List<int>();
        for (var r = 0; r < Content.Length; r++)
        {
            for (var c = 0; c < Content[r].Length; c++)
            {
                var ch = Content[r][c];
                if (ch != '.' && !char.IsDigit(ch))
                    result.AddRange(GetPartNumbers(new Position(r, c)));
            }
        }
        return result;
    }

    /// <summary>
    /// Gets all gear numbers from this schematic.
    /// </summary>
    /// <returns>All gear numbers from this schematic.</returns>
    public IEnumerable<int> GetGearNumbers()
    {
        var result = new List<int>();
        for (var r = 0; r < Content.Length; r++)
        {
            for (var c = 0; c < Content[r].Length; c++)
            {
                var ch = Content[r][c];
                if (ch != '*')
                    continue;

                var gears = GetPartNumbers(new Position(r, c)).ToList();
                if (gears.Count == 2)
                    result.Add(gears[0] * gears[1]);
            }
        }
        return result;
    }

    /// <summary>
    /// Gets all part numbers from a specific position.
    /// </summary>
    /// <param name="pos">The grid position.</param>
    /// <returns>All part numbers from a specific position.</returns>
    private IEnumerable<int> GetPartNumbers(Position pos)
    {
        return GetHorizontalPartNumbers(pos).Concat(GetTopPartNumbers(pos)).Concat(GetBottomPartNumbers(pos));
    }

    /// <summary>
    /// Gets the horizontal neighbouring part numbers.
    /// </summary>
    /// <param name="pos">The grid position.</param>
    /// <returns>The horizontal neighbouring part numbers.</returns>
    private List<int> GetHorizontalPartNumbers(Position pos)
    {
        var result = new List<int>(2);
        if (_boundary.IsValid(pos.Left) && TryGetPartNumber(pos.Left, out var left))
            result.Add(left);
        if (_boundary.IsValid(pos.Right) && TryGetPartNumber(pos.Right, out var right))
            result.Add(right);
        return result;
    }

    /// <summary>
    /// Gets the top neighbouring part numbers.
    /// </summary>
    /// <param name="pos">The grid position.</param>
    /// <returns>The top neighbouring part numbers.</returns>
    private IEnumerable<int> GetTopPartNumbers(Position pos)
    {
        // if top position has a number, we do not need to check top left and top right
        if (_boundary.IsValid(pos.Top) && TryGetPartNumber(pos.Top, out var top))
            return new List<int> { top };

        var result = new List<int>(2);
        if (_boundary.IsValid(pos.TopLeft) && TryGetPartNumber(pos.TopLeft, out var topLeft))
            result.Add(topLeft);
        if (_boundary.IsValid(pos.TopRight) && TryGetPartNumber(pos.TopRight, out var topRight))
            result.Add(topRight);
        return result;
    }

    /// <summary>
    /// Gets the bottom neighbouring part numbers.
    /// </summary>
    /// <param name="pos">The grid position.</param>
    /// <returns>The bottom neighbouring part numbers.</returns>
    private IEnumerable<int> GetBottomPartNumbers(Position pos)
    {
        // if bottom position has a number, we do not need to check bottom left and bottom right
        if (_boundary.IsValid(pos.Bottom) && TryGetPartNumber(pos.Bottom, out var bottom))
            return new List<int> { bottom };

        var result = new List<int>(2);
        if (_boundary.IsValid(pos.BottomLeft) && TryGetPartNumber(pos.BottomLeft, out var bottomLeft))
            result.Add(bottomLeft);
        if (_boundary.IsValid(pos.BottomRight) && TryGetPartNumber(pos.BottomRight, out var bottomRight))
            result.Add(bottomRight);
        return result;
    }

    /// <summary>
    /// Tries to get a number at the specified position.
    /// </summary>
    /// <param name="pos">The grid position.</param>
    /// <param name="number">The number at the specified position.</param>
    /// <returns>Whether there is a number at the specified position.</returns>
    private bool TryGetPartNumber(Position pos, out int number)
    {
        number = 0;
        if (!char.IsDigit(Content.At(pos)))
            return false;

        var startCol = pos.Column32;
        while (startCol > 0 && char.IsDigit(Content.At(pos.WithColumn(startCol - 1))))
            startCol--;

        var numStr = Content[pos.Row32][startCol..].TakeWhile(char.IsDigit).ToArray();
        return int.TryParse(numStr, out number);
    }
}
