namespace AoC2023.Day13;

/// <summary>
/// Represents a reflection.
/// </summary>
public class Reflection
{
    private readonly string[] _transposedContent;

    /// <summary>
    /// Gets the reflection content.
    /// </summary>
    public string[] Content { get; }

    /// <summary>
    /// Constructs a new instance of <see cref="Reflection"/>.
    /// </summary>
    /// <param name="content">The content of the reflection.</param>
    public Reflection(IEnumerable<string> content)
    {
        Content = content.ToArray();
        _transposedContent = TransposeContent();
    }

    /// <summary>
    /// Counts the number of row reflections.
    /// </summary>
    /// <param name="mismatchTolerance">The number of characters that are allowed to be mismatched.</param>
    /// <returns>The number of row reflections.</returns>
    public int CountRowReflections(int mismatchTolerance = 0)
    {
        return CountRowReflections(Content, mismatchTolerance);
    }

    /// <summary>
    /// Counts the number of column reflections.
    /// </summary>
    /// <param name="mismatchTolerance">The number of characters that are allowed to be mismatched.</param>
    /// <returns>The number of column reflections.</returns>
    public int CountColumnReflections(int mismatchTolerance = 0)
    {
        return CountRowReflections(_transposedContent, mismatchTolerance);
    }

    /// <summary>
    /// Counts the number of row reflections.
    /// </summary>
    /// <param name="lines">The lines to count.</param>
    /// <param name="mismatchTolerance">The number of characters that are allowed to be mismatched.</param>
    /// <returns>The number of row reflections.</returns>
    private static int CountRowReflections(string[] lines, int mismatchTolerance = 0)
    {
        var result = 0;
        for (var r = 0; r < lines.Length - 1; r++)
        {
            var mismatch = 0;
            for (var offset = 0; offset <= r; offset++)
            {
                // indexes of rows to compare
                var prev = r - offset;
                var next = r + offset + 1;
                if (prev < 0 || next >= lines.Length)
                    break;

                // compare rows and count mismatch chars
                for (var c = 0; c < lines[0].Length; c++)
                    mismatch += lines[prev][c] == lines[next][c] ? 0 : 1;
            }

            if (mismatch == mismatchTolerance)
                result += r + 1;
        }
        return result;
    }

    /// <summary>
    /// Transposes the content (rotates the 2D array by 90 degrees).
    /// </summary>
    /// <returns>The transposed content.</returns>
    private string[] TransposeContent()
    {
        var result = new string[Content[0].Length];
        for (var i = 0; i < Content[0].Length; i++)
            result[i] = new string(Content.Select(x => x[i]).ToArray());
        return result;
    }
}
