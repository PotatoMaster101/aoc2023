using AoC.Common.Input;

namespace AoC2023.Day05;

/// <summary>
/// Parser for <see cref="Almanac"/>.
/// </summary>
public class AlmanacParser : InputParser<Almanac>
{
    /// <summary>
    /// Constructs a new instance of <see cref="AlmanacParser"/>.
    /// </summary>
    /// <param name="filename">The input file name.</param>
    public AlmanacParser(string filename) : base(filename) { }

    /// <inheritdoc cref="InputParser{T}.Parse"/>
    public override async Task<Almanac> Parse(CancellationToken token = default)
    {
        var seeds = await ParseSeeds(token);

        // skip 2 lines
        await Reader.ReadLineAsync(token);
        await Reader.ReadLineAsync(token);

        var mappers = await ParseMappers(token);
        return new Almanac(seeds, mappers);
    }

    /// <summary>
    /// Parses the seed values.
    /// </summary>
    /// <returns>The parsed seed values.</returns>
    private async Task<IEnumerable<long>> ParseSeeds(CancellationToken token = default)
    {
        var seeds = await Reader.ReadLineAsync(token);
        return string.IsNullOrEmpty(seeds) ?
            Enumerable.Empty<long>() :
            seeds[6..].Split(" ", SplitOpt).Select(long.Parse);
    }

    /// <summary>
    /// Parses the mapper values.
    /// </summary>
    /// <returns>The parsed mapper values.</returns>
    private async Task<IEnumerable<Mapper>> ParseMappers(CancellationToken token = default)
    {
        var mappers = new List<Mapper>();
        var currentMapper = new Mapper();
        await foreach (var line in GetNonEmptyLines(token))
        {
            if (char.IsDigit(line[0]))
            {
                var mapperNums = line.Split(" ", SplitOpt).Select(long.Parse).ToArray();
                if (mapperNums.Length == 3)
                    currentMapper.AddMapperEntry(new MapperEntry(mapperNums[1], mapperNums[0], mapperNums[2]));
            }
            else
            {
                mappers.Add(currentMapper);
                currentMapper = new Mapper();
            }
        }
        mappers.Add(currentMapper);
        return mappers;
    }
}
