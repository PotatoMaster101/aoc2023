namespace AoC.Common.Grid;

/// <summary>
/// Represents a position in a grid.
/// </summary>
public readonly record struct Position : IComparable<Position>
{
    /// <summary>
    /// Gets the origin point (0, 0).
    /// </summary>
    public static readonly Position Origin = new();

    /// <summary>
    /// Gets the bottom position.
    /// </summary>
    public Position Bottom => new(Row + 1, Column);

    /// <summary>
    /// Gets the bottom left position.
    /// </summary>
    public Position BottomLeft => new(Row + 1, Column - 1);

    /// <summary>
    /// Gets the bottom right position.
    /// </summary>
    public Position BottomRight => new(Row + 1, Column + 1);

    /// <summary>
    /// Gets the column position.
    /// </summary>
    public long Column { get; }

    /// <summary>
    /// Gets the column position as an <see cref="int"/>.
    /// </summary>
    public int Column32 => (int)Column;

    /// <summary>
    /// Gets the left position.
    /// </summary>
    public Position Left => new(Row, Column - 1);

    /// <summary>
    /// Gets the right position.
    /// </summary>
    public Position Right => new(Row, Column + 1);

    /// <summary>
    /// Gets the row position.
    /// </summary>
    public long Row { get; }

    /// <summary>
    /// Gets the row position as an <see cref="int"/>.
    /// </summary>
    public int Row32 => (int)Row;

    /// <summary>
    /// Gets the top position.
    /// </summary>
    public Position Top => new(Row - 1, Column);

    /// <summary>
    /// Gets the top left position.
    /// </summary>
    public Position TopLeft => new(Row - 1, Column - 1);

    /// <summary>
    /// Gets the top right position.
    /// </summary>
    public Position TopRight => new(Row - 1, Column + 1);

    /// <summary>
    /// Constructs a new instance of <see cref="Position"/>.
    /// </summary>
    /// <param name="row">The row position.</param>
    /// <param name="column">The column position.</param>
    public Position(long row = 0, long column = 0)
    {
        Row = row;
        Column = column;
    }

    /// <summary>
    /// Adds to this position.
    /// </summary>
    /// <param name="another">Another position to add.</param>
    /// <returns>A new <see cref="Position"/> containing the result.</returns>
    public Position Add(Position another)
    {
        return new Position(Row + another.Row, Column + another.Column);
    }

    /// <summary>
    /// Adds to this position.
    /// </summary>
    /// <param name="amount">The amount to add.</param>
    /// <returns>A new <see cref="Position"/> containing the result.</returns>
    public Position Add(long amount)
    {
        return new Position(Row + amount, Column + amount);
    }

    /// <inheritdoc cref="IComparable{T}.CompareTo"/>
    public int CompareTo(Position other)
    {
        return Row == other.Row ? Column.CompareTo(other.Column) : Row.CompareTo(other.Row);
    }

    /// <summary>
    /// Deconstructs this position.
    /// </summary>
    /// <param name="row">The row position.</param>
    /// <param name="column">The column position.</param>
    public void Deconstruct(out long row, out long column)
    {
        row = Row;
        column = Column;
    }

    /// <summary>
    /// Divides this position.
    /// </summary>
    /// <param name="amount">The amount to divide.</param>
    /// <returns>A new <see cref="Position"/> containing the result.</returns>
    public Position Divide(long amount)
    {
        return new Position(Row / amount, Column / amount);
    }

    /// <summary>
    /// Returns a list of neighbouring cross positions (top, bottom, left, right).
    /// </summary>
    /// <returns>A list of neighbouring cross positions (top, bottom, left, right).</returns>
    public Position[] GetCrossNeighbours()
    {
        return [Bottom, Left, Right, Top];
    }

    /// <summary>
    /// Returns the destination position using the given direction and distance.
    /// </summary>
    /// <param name="direction">The direction of the destination.</param>
    /// <param name="distance">The distance to the destination.</param>
    /// <returns>The destination position.</returns>
    public Position GetDestination(Direction direction, long distance)
    {
        return direction switch
        {
            Direction.Down => new Position(Row + distance, Column),
            Direction.Left => new Position(Row, Column - distance),
            Direction.Right => new Position(Row, Column + distance),
            _ => new Position(Row - distance, Column)
        };
    }

    /// <summary>
    /// Returns a list of neighbouring diagonal positions (top left, top right, bottom left, bottom right).
    /// </summary>
    /// <returns>A list of neighbouring diagonal positions (top left, top right, bottom left, bottom right).</returns>
    public Position[] GetDiagonalNeighbours()
    {
        return [BottomLeft, BottomRight, TopLeft, TopRight];
    }

    /// <summary>
    /// Returns the Manhattan distance between this position and another position.
    /// </summary>
    /// <param name="another">The other position to compute the Manhattan distance.</param>
    /// <returns>The Manhattan distance between this position and another position.</returns>
    public long GetManhattanDistance(Position another)
    {
        return Math.Abs(Row - another.Row) + Math.Abs(Column - another.Column);
    }

    /// <summary>
    /// Returns a list of neighbouring positions.
    /// </summary>
    /// <returns>A list of neighbouring positions.</returns>
    public Position[] GetNeighbours()
    {
        return [Bottom, BottomLeft, BottomRight, Left, Right, Top, TopLeft, TopRight];
    }

    /// <summary>
    /// Multiplies this position.
    /// </summary>
    /// <param name="amount">The amount to multiply.</param>
    /// <returns>A new <see cref="Position"/> containing the result.</returns>
    public Position Multiply(long amount)
    {
        return new Position(Row * amount, Column * amount);
    }

    /// <summary>
    /// Negates this position.
    /// </summary>
    /// <returns>A new <see cref="Position"/> containing the result.</returns>
    public Position Negate()
    {
        return new Position(-Row, -Column);
    }

    /// <summary>
    /// Reduces this position so it can fit into a boundary.
    /// </summary>
    /// <param name="totalRows">The total number of rows.</param>
    /// <param name="totalColumns">The total number of columns.</param>
    /// <returns>The reduced position that can fit in a boundary.</returns>
    public Position Reduce(long totalRows, long totalColumns)
    {
        return new Position(Mod(Row, totalRows), Mod(Column, totalColumns));
    }

    /// <summary>
    /// Subtracts from this position.
    /// </summary>
    /// <param name="another">Another position to subtract.</param>
    /// <returns>A new <see cref="Position"/> containing the result.</returns>
    public Position Subtract(Position another)
    {
        return new Position(Row - another.Row, Column - another.Column);
    }

    /// <summary>
    /// Subtracts from this position.
    /// </summary>
    /// <param name="amount">The amount to subtract.</param>
    /// <returns>A new <see cref="Position"/> containing the result.</returns>
    public Position Subtract(long amount)
    {
        return new Position(Row - amount, Column - amount);
    }

    /// <inheritdoc cref="object.ToString"/>
    public override string ToString()
    {
        return $"({Row32}, {Column32})";
    }

    /// <summary>
    /// Returns a position with a new column value.
    /// </summary>
    /// <param name="newColumn">The new column value.</param>
    /// <returns>The position with a new column value.</returns>
    public Position WithColumn(long newColumn)
    {
        return new Position(Row, newColumn);
    }

    /// <summary>
    /// Returns a position with a new row value.
    /// </summary>
    /// <param name="newRow">The new row value.</param>
    /// <returns>The position with a new row value.</returns>
    public Position WithRow(long newRow)
    {
        return new Position(newRow, Column);
    }

    /// <summary>
    /// Performs MOD on 2 numbers.
    /// </summary>
    /// <param name="a">The first number to MOD.</param>
    /// <param name="b">The second number to MOD.</param>
    /// <returns>The MOD result.</returns>
    private static long Mod(long a, long b)
    {
        var result = a % b;
        return result < 0 ? result + b : result;
    }
}
