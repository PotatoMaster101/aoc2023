using System.Runtime.CompilerServices;

namespace AoC.Common.Input;

/// <summary>
/// A base implementation of all input file parsers.
/// </summary>
/// <typeparam name="T">The parser output type.</typeparam>
public abstract class InputParser<T> : IDisposable
{
    /// <summary>
    /// <see cref="StringSplitOptions"/> for trimmed entries.
    /// </summary>
    protected const StringSplitOptions SplitOpt = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;

    /// <summary>
    /// Gets the file reader.
    /// </summary>
    protected StreamReader Reader { get; }

    /// <summary>
    /// Constructs a new instance of <see cref="InputParser{T}"/>.
    /// </summary>
    /// <param name="filename">The input file name.</param>
    protected InputParser(string filename)
    {
        Reader = new StreamReader(filename);
    }

    /// <inheritdoc cref="IDisposable.Dispose"/>
    public virtual void Dispose()
    {
        GC.SuppressFinalize(this);
        Reader.Dispose();
    }

    /// <summary>
    /// Parses the input file.
    /// </summary>
    /// <param name="token">The cancellation token for cancelling this operation.</param>
    /// <returns>The parsed output.</returns>
    public abstract Task<T> Parse(CancellationToken token = default);

    /// <summary>
    /// Gets all the non-empty lines.
    /// </summary>
    /// <param name="token">The cancellation token for cancelling this operation.</param>
    /// <returns>All the non-empty lines.</returns>
    protected ValueTask<string[]> GetAllNonEmptyLines(CancellationToken token = default)
    {
        return GetNonEmptyLines(token).ToArrayAsync(token);
    }

    /// <summary>
    /// Gets the non-empty lines.
    /// </summary>
    /// <param name="token">The cancellation token for cancelling this operation.</param>
    /// <returns>The non-empty lines.</returns>
    protected async IAsyncEnumerable<string> GetNonEmptyLines([EnumeratorCancellation] CancellationToken token = default)
    {
        while (!Reader.EndOfStream)
        {
            var line = await Reader.ReadLineAsync(token).ConfigureAwait(false);
            if (!string.IsNullOrEmpty(line))
                yield return line;
        }
    }

    /// <summary>
    /// Resets the reader by moving the position to 0.
    /// </summary>
    protected void ResetReader()
    {
        Reader.BaseStream.Position = 0;
        Reader.DiscardBufferedData();
    }
}
