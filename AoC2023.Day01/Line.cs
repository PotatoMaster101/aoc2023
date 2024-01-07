using System.Buffers;

namespace AoC2023.Day01;

/// <summary>
/// A line in the input file.
/// </summary>
public class Line
{
    private static readonly SearchValues<char> NumericDigits = SearchValues.Create("123456789");
    private static readonly IReadOnlyDictionary<string, int> NumericWords = new Dictionary<string, int>
    {
        ["one"] = 1,
        ["two"] = 2,
        ["three"] = 3,
        ["four"] = 4,
        ["five"] = 5,
        ["six"] = 6,
        ["seven"] = 7,
        ["eight"] = 8,
        ["nine"] = 9
    };

    /// <summary>
    /// Gets the line content.
    /// </summary>
    public string Content { get; }

    /// <summary>
    /// Constructs a new instance of <see cref="Line"/>.
    /// </summary>
    /// <param name="content">The content of the line.</param>
    public Line(string content)
    {
        Content = content;
    }

    /// <summary>
    /// Gets the first numeric digit value.
    /// </summary>
    /// <returns>The first numeric digit value.</returns>
    public NumericValue GetFirstNumericDigit()
    {
        var digitIdx = Content.AsSpan().IndexOfAny(NumericDigits);
        return digitIdx == -1 ? NumericValue.Invalid : new NumericValue(digitIdx, Content[digitIdx] - '0');
    }

    /// <summary>
    /// Gets the first numeric word value.
    /// </summary>
    /// <returns>The first numeric word value.</returns>
    public NumericValue GetFirstNumericWord()
    {
        var wordIdxes = GetWordIndexes(true);
        var (word, idx) = wordIdxes.OrderBy(x => x.Value).FirstOrDefault();
        return string.IsNullOrEmpty(word) ? NumericValue.Invalid : new NumericValue(idx, NumericWords[word]);
    }

    /// <summary>
    /// Gets the last numeric digit value.
    /// </summary>
    /// <returns>The last numeric digit value.</returns>
    public NumericValue GetLastNumericDigit()
    {
        var digitIdx = Content.AsSpan().LastIndexOfAny(NumericDigits);
        return digitIdx == -1 ? NumericValue.Invalid : new NumericValue(digitIdx, Content[digitIdx] - '0');
    }

    /// <summary>
    /// Gets the last numeric word value.
    /// </summary>
    /// <returns>The last numeric word value.</returns>
    public NumericValue GetLastNumericWord()
    {
        var wordIdxes = GetWordIndexes(false);
        var (word, idx) = wordIdxes.OrderByDescending(x => x.Value).FirstOrDefault();
        return string.IsNullOrEmpty(word) ? NumericValue.Invalid : new NumericValue(idx, NumericWords[word]);
    }

    /// <summary>
    /// Gets a dictionary containing the numeric words and their position.
    /// </summary>
    /// <param name="searchFromStart">Whether to search the words from the start.</param>
    /// <returns>A dictionary containing the numeric words and their position.</returns>
    private Dictionary<string, int> GetWordIndexes(bool searchFromStart)
    {
        var result = new Dictionary<string, int>();
        foreach (var word in NumericWords.Keys)
        {
            var idx = searchFromStart ?
                Content.IndexOf(word, StringComparison.InvariantCulture) :
                Content.LastIndexOf(word, StringComparison.InvariantCulture);

            if (idx != -1)
                result.Add(word, idx);
        }
        return result;
    }
}
