using AoC.Common.Input;

namespace AoC2023.Day13;

/// <summary>
/// Parser for <see cref="Reflection"/>.
/// </summary>
public class ReflectionParser : InputParser<IReadOnlyList<Reflection>>
{
    /// <summary>
    /// Constructs a new instance of <see cref="ReflectionParser"/>.
    /// </summary>
    /// <param name="filename">The input file name.</param>
    public ReflectionParser(string filename) : base(filename) { }

    /// <inheritdoc cref="InputParser{T}.Parse"/>
    public override async Task<IReadOnlyList<Reflection>> Parse(CancellationToken token = default)
    {
        var result = new List<Reflection>();
        var lines = new List<string>();
        while (!Reader.EndOfStream)
        {
            var line = await Reader.ReadLineAsync(token);
            if (string.IsNullOrEmpty(line))
            {
                result.Add(new Reflection(lines));
                lines.Clear();
                continue;
            }
            lines.Add(line);
        }
        result.Add(new Reflection(lines));
        return result;
    }
}
