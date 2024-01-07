namespace AoC2023.Day11;

/// <summary>
/// A galaxy image.
/// </summary>
public class Image
{
    /// <summary>
    /// Gets the set of empty row indexes.
    /// </summary>
    public IReadOnlySet<int> EmptyRows { get; }

    /// <summary>
    /// Gets the set of empty column indexes.
    /// </summary>
    public IReadOnlySet<int> EmptyColumns { get; }

    /// <summary>
    /// Gets the set of galaxy pairs.
    /// </summary>
    public IReadOnlyList<GalaxyPair> GalaxyPairs { get; }

    /// <summary>
    /// Constructs a new instance of <see cref="Image"/>.
    /// </summary>
    /// <param name="emptyRows">The set of empty row indexes.</param>
    /// <param name="emptyColumns">The set of empty column indexes.</param>
    /// <param name="galaxyPairs">The list of galaxy pairs</param>
    public Image(IReadOnlySet<int> emptyRows, IReadOnlySet<int> emptyColumns, IReadOnlyList<GalaxyPair> galaxyPairs)
    {
        EmptyRows = emptyRows;
        EmptyColumns = emptyColumns;
        GalaxyPairs = galaxyPairs;
    }

    /// <summary>
    /// Returns the sum of galaxy pair distances.
    /// </summary>
    /// <param name="expandFactor">The expansion factor.</param>
    /// <returns>The sum of galaxy pair distances.</returns>
    public long GetDistanceSum(int expandFactor)
    {
        return GalaxyPairs.Sum(x => x.GetDistance(EmptyRows, EmptyColumns, expandFactor));
    }
}
