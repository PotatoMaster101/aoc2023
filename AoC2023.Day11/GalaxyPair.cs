using AoC.Common.Grid;

namespace AoC2023.Day11;

/// <summary>
/// A pair of galaxies.
/// </summary>
public class GalaxyPair
{
    /// <summary>
    /// Gets the source position.
    /// </summary>
    public Position Source { get; }

    /// <summary>
    /// Gets the destination position.
    /// </summary>
    public Position Destination { get; }

    /// <summary>
    /// Constructs a new instance of <see cref="GalaxyPair"/>.
    /// </summary>
    /// <param name="source">The source position.</param>
    /// <param name="destination">The destination position.</param>
    public GalaxyPair(Position source, Position destination)
    {
        Source = source;
        Destination = destination;
    }

    /// <summary>
    /// Calculates the distance between this pair.
    /// </summary>
    /// <param name="emptyRows">The set of empty row indexes.</param>
    /// <param name="emptyColumns">The set of empty column indexes.</param>
    /// <param name="expandFactor">The expansion factor.</param>
    /// <returns>The distance between this pair.</returns>
    public long GetDistance(IReadOnlySet<int> emptyRows, IReadOnlySet<int> emptyColumns, int expandFactor = 2)
    {
        var sourceExpanded = ExpandPosition(Source, emptyRows, emptyColumns, expandFactor);
        var destinationExpanded = ExpandPosition(Destination, emptyRows, emptyColumns, expandFactor);
        return sourceExpanded.GetManhattanDistance(destinationExpanded);
    }

    /// <summary>
    /// Gets the position after expansion.
    /// </summary>
    /// <param name="position">The position to expand</param>
    /// <param name="emptyRows">The set of empty row indexes.</param>
    /// <param name="emptyColumns">The set of empty column indexes.</param>
    /// <param name="expandFactor">The expansion factor.</param>
    /// <returns>The position after expansion.</returns>
    private static Position ExpandPosition(Position position, IReadOnlySet<int> emptyRows, IReadOnlySet<int> emptyColumns, int expandFactor)
    {
        int row = 0, column = 0;
        for (var i = 0; i < position.Row32; i++)
            row += emptyRows.Contains(i) ? expandFactor : 1;
        for (var i = 0; i < position.Column32; i++)
            column += emptyColumns.Contains(i) ? expandFactor : 1;
        return new Position(row, column);
    }
}
