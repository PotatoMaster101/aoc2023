using AoC.Common.Grid;
using AoC.Common.Input;

namespace AoC2023.Day11;

/// <summary>
/// Parser for <see cref="Image"/>.
/// </summary>
public class ImageParser : InputParser<Image>
{
    private const char Galaxy = '#';
    private const char Empty = '.';
    private string[] _lines = Array.Empty<string>();

    /// <summary>
    /// Constructs a new instance of <see cref="ImageParser"/>.
    /// </summary>
    /// <param name="filename">The input file name.</param>
    public ImageParser(string filename) : base(filename) { }

    /// <inheritdoc cref="InputParser{T}.Parse"/>
    public override async Task<Image> Parse(CancellationToken token = default)
    {
        _lines = await GetAllNonEmptyLines(token);
        var emptyRows = GetEmptyRows();
        var emptyColumns = GetEmptyColumns();
        var galaxyPairs = GetGalaxyPairs();
        return new Image(emptyRows, emptyColumns, galaxyPairs);
    }

    /// <summary>
    /// Gets the galaxy pairs.
    /// </summary>
    /// <returns>The galaxy pairs.</returns>
    private List<GalaxyPair> GetGalaxyPairs()
    {
        var galaxies = GetGalaxies();
        var result = new HashSet<GalaxyPair>();
        for (var i = 0; i < galaxies.Count; i++)
        {
            for (var j = i + 1; j < galaxies.Count; j++)
                result.Add(new GalaxyPair(galaxies[i], galaxies[j]));
        }
        return result.ToList();
    }

    /// <summary>
    /// Gets the galaxy positions.
    /// </summary>
    /// <returns>The galaxy positions.</returns>
    private List<Position> GetGalaxies()
    {
        var galaxies = new List<Position>();
        for (var r = 0; r < _lines.Length; r++)
        {
            for (var c = 0; c < _lines[r].Length; c++)
            {
                if (_lines[r][c] == Galaxy)
                    galaxies.Add(new Position(r, c));
            }
        }
        return galaxies;
    }

    /// <summary>
    /// Gets the empty rows.
    /// </summary>
    /// <returns>The empty rows.</returns>
    private HashSet<int> GetEmptyRows()
    {
        var result = new HashSet<int>();
        for (var i = 0; i < _lines.Length; i++)
        {
            if (_lines[i].All(x => x == Empty))
                result.Add(i);
        }
        return result;
    }

    /// <summary>
    /// Gets the empty columns.
    /// </summary>
    /// <returns>The empty columns.</returns>
    private HashSet<int> GetEmptyColumns()
    {
        var result = new HashSet<int>();
        for (var i = 0; i < _lines[0].Length; i++)
        {
            if (_lines.All(x => x[i] == Empty))
                result.Add(i);
        }
        return result;
    }
}
