namespace AoC.Common.Grid;

/// <summary>
/// Represents the boundary for a grid.
/// </summary>
public readonly record struct Boundary
{
    /// <summary>
    /// Gets the bottom left position.
    /// </summary>
    public Position BottomLeft => new(RowCount - 1);

    /// <summary>
    /// Gets the bottom right position.
    /// </summary>
    public Position BottomRight => new(RowCount - 1, ColumnCount - 1);

    /// <summary>
    /// Gets the number of columns.
    /// </summary>
    public long ColumnCount { get; }

    /// <summary>
    /// Gets the number of columns as an <see cref="int"/>.
    /// </summary>
    public int ColumnCount32 => (int)ColumnCount;

    /// <summary>
    /// Gets the number of rows.
    /// </summary>
    public long RowCount { get; }

    /// <summary>
    /// Gets the number of rows as an <see cref="int"/>.
    /// </summary>
    public int RowCount32 => (int)RowCount;

    /// <summary>
    /// Gets the top left position.
    /// </summary>
    public Position TopLeft => Position.Origin;

    /// <summary>
    /// Gets the top right position.
    /// </summary>
    public Position TopRight => new(0, ColumnCount - 1);

    /// <summary>
    /// Constructs a new <see cref="Boundary"/>.
    /// </summary>
    /// <param name="rowCount">The number of rows.</param>
    /// <param name="columnCount">The number of columns.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="rowCount"/> or <paramref name="columnCount"/> is 0 or negative.</exception>
    public Boundary(long rowCount, long columnCount)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(rowCount);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(columnCount);

        ColumnCount = columnCount;
        RowCount = rowCount;
    }

    /// <summary>
    /// Deconstructs this boundary.
    /// </summary>
    /// <param name="rowCount">The number of rows.</param>
    /// <param name="columnCount">The number of columns.</param>
    public void Deconstruct(out long rowCount, out long columnCount)
    {
        rowCount = RowCount;
        columnCount = ColumnCount;
    }

    /// <summary>
    /// Returns the list of valid column positions.
    /// </summary>
    /// <param name="colIdx">The column index to retrieve the positions.</param>
    /// <returns>The list of valid column positions.</returns>
    public IEnumerable<Position> GetColumnPositions(long colIdx)
    {
        if (colIdx < 0 || colIdx >= ColumnCount)
            return Enumerable.Empty<Position>();

        var result = new List<Position>(RowCount32);
        for (long row = 0; row < RowCount; row++)
            result.Add(new Position(row, colIdx));
        return result;
    }

    /// <summary>
    /// Returns the list of valid row positions.
    /// </summary>
    /// <param name="rowIdx">The row index to retrieve the positions.</param>
    /// <returns>The list of valid row positions.</returns>
    public IEnumerable<Position> GetRowPositions(long rowIdx)
    {
        if (rowIdx < 0 || rowIdx >= RowCount)
            return Enumerable.Empty<Position>();

        var result = new List<Position>(ColumnCount32);
        for (long col = 0; col < ColumnCount; col++)
            result.Add(new Position(rowIdx, col));
        return result;
    }

    /// <summary>
    /// Returns a list of valid neighbouring cross positions (top, bottom, left, right).
    /// </summary>
    /// <param name="center">The center of the neighbouring positions.</param>
    /// <returns>A list of neighbouring cross positions (top, bottom, left, right).</returns>
    public IEnumerable<Position> GetValidCrossNeighbours(Position center)
    {
        var neighbours = center.GetCrossNeighbours();
        var result = new List<Position>(4);

        // avoid LINQ for optimum performance and memory
        // ReSharper disable once ForCanBeConvertedToForeach
        for (var i = 0; i < neighbours.Length; i++)
            if (IsValid(neighbours[i]))
                result.Add(neighbours[i]);

        return result;
    }

    /// <summary>
    /// Returns a list of valid neighbouring diagonal positions (top left, top right, bottom left, bottom right).
    /// </summary>
    /// <param name="center">The center of the neighbouring positions.</param>
    /// <returns>A list of neighbouring diagonal positions (top left, top right, bottom left, bottom right).</returns>
    public IEnumerable<Position> GetValidDiagonalNeighbours(Position center)
    {
        var neighbours = center.GetDiagonalNeighbours();
        var result = new List<Position>(4);

        // avoid LINQ for optimum performance and memory
        // ReSharper disable once ForCanBeConvertedToForeach
        for (var i = 0; i < neighbours.Length; i++)
            if (IsValid(neighbours[i]))
                result.Add(neighbours[i]);

        return result;
    }

    /// <summary>
    /// Returns a list of valid neighbouring positions.
    /// </summary>
    /// <param name="center">The center of the neighbouring positions.</param>
    /// <returns>A list of neighbouring positions.</returns>
    public IEnumerable<Position> GetValidNeighbours(Position center)
    {
        var neighbours = center.GetNeighbours();
        var result = new List<Position>(8);

        // avoid LINQ for optimum performance and memory
        // ReSharper disable once ForCanBeConvertedToForeach
        for (var i = 0; i < neighbours.Length; i++)
            if (IsValid(neighbours[i]))
                result.Add(neighbours[i]);

        return result;
    }

    /// <summary>
    /// Determines whether a grid position is valid.
    /// </summary>
    /// <param name="position">The grid position to check.</param>
    /// <returns>Whether a grid position is valid.</returns>
    public bool IsValid(Position position)
    {
        var (minRow, minCol) = TopLeft;
        return position.Row >= minRow && position.Row < RowCount && position.Column >= minCol && position.Column < ColumnCount;
    }

    /// <inheritdoc cref="object.ToString"/>
    public override string ToString()
    {
        return $"{RowCount} x {ColumnCount}";
    }
}
