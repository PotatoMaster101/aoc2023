namespace AoC2023.Day02;

/// <summary>
/// Cubes shown.
/// </summary>
public class CubeShow
{
    /// <summary>
    /// Gets the number of red cubes shown.
    /// </summary>
    public int Red { get; private set; }

    /// <summary>
    /// Gets the number of green cubes shown.
    /// </summary>
    public int Green { get; private set; }

    /// <summary>
    /// Gets the number of blue cubes shown.
    /// </summary>
    public int Blue { get; private set; }

    /// <summary>
    /// Constructs a new instance of <see cref="CubeShow"/>.
    /// </summary>
    /// <param name="shownCubes">A string containing the cubes shown.</param>
    public CubeShow(string shownCubes)
    {
        ParseCubes(shownCubes);
    }

    /// <summary>
    /// Determines whether this cube show is possible.
    /// </summary>
    /// <param name="maxRed">The maximum red cubes count.</param>
    /// <param name="maxGreen">The maximum green cubes count.</param>
    /// <param name="maxBlue">The maximum blue cubes count.</param>
    /// <returns>Whether this cube show is possible.</returns>
    public bool IsPossible(int maxRed, int maxGreen, int maxBlue)
    {
        return Red <= maxRed && Green <= maxGreen && Blue <= maxBlue;
    }

    /// <summary>
    /// Parses the shown cubes and populate colours.
    /// </summary>
    /// <param name="shownCubes">A string containing the cubes shown.</param>
    private void ParseCubes(string shownCubes)
    {
        const StringSplitOptions opt = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;
        var commaSplits = shownCubes.Split(",", opt);
        foreach (var commaSplit in commaSplits)
        {
            var spaceSplits = commaSplit.Split(" ", opt);
            if (spaceSplits.Length != 2)
                continue;

            var count = int.Parse(spaceSplits[0]);
            switch (spaceSplits[1][0])
            {
                case 'r':
                    Red = count;
                    break;
                case 'g':
                    Green = count;
                    break;
                case 'b':
                    Blue = count;
                    break;
            }
        }
    }
}
