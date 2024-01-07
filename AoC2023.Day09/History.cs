namespace AoC2023.Day09;

/// <summary>
/// Represents a list of history values.
/// </summary>
public class History
{
    private readonly IReadOnlyList<long> _values;

    /// <summary>
    /// Constructs a new instance of <see cref="History"/>.
    /// </summary>
    /// <param name="values">The list of values in this history.</param>
    public History(IEnumerable<long> values)
    {
        _values = values.ToList();
    }

    /// <summary>
    /// Predicts the next value for this history.
    /// </summary>
    /// <returns>The next value for this history.</returns>
    public long PredictNextValue()
    {
        var diff = GenerateDifferenceMap();
        var nextNum = diff[^1][0];
        for (var i = diff.Count - 2; i >= 0; i--)
            nextNum += diff[i][^1];
        return nextNum;
    }

    /// <summary>
    /// Predicts the previous value for this history.
    /// </summary>
    /// <returns>The previous value for this history.</returns>
    public long PredictPreviousValue()
    {
        var diff = GenerateDifferenceMap();
        var prevNum = diff[^1][0];
        for (var i = diff.Count - 2; i >= 0; i--)
            prevNum = diff[i][0] - prevNum;
        return prevNum;
    }

    /// <summary>
    /// Gets the next row of history values, and whether the row is all 0.
    /// </summary>
    /// <param name="prevRow">The previous row of history values.</param>
    /// <returns>The next row of history values, and whether the row is all 0.</returns>
    private static (IReadOnlyList<long>, bool) GetNextRow(IReadOnlyList<long> prevRow)
    {
        var row = new List<long>(prevRow.Count - 1);
        var zeroCount = 0;
        for (var i = 1; i < prevRow.Count; i++)
        {
            var value = prevRow[i] - prevRow[i - 1];
            row.Add(value);
            if (value == 0)
                zeroCount++;
        }
        return (row, zeroCount == prevRow.Count - 1);
    }

    /// <summary>
    /// Generates the difference map for this history.
    /// </summary>
    /// <returns>The difference map for this history.</returns>
    private List<IReadOnlyList<long>> GenerateDifferenceMap()
    {
        var rows = new List<IReadOnlyList<long>>(_values.Count) { _values };
        var (nextRow, allZero) = GetNextRow(rows[0]);
        while (!allZero)
        {
            rows.Add(nextRow);
            (nextRow, allZero) = GetNextRow(nextRow);
        }
        return rows;
    }
}
