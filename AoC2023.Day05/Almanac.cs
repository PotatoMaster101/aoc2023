namespace AoC2023.Day05;

/// <summary>
/// Represents an almanac.
/// </summary>
public class Almanac
{
    /// <summary>
    /// Gets the list of seeds.
    /// </summary>
    public IReadOnlyList<long> Seeds { get; }

    /// <summary>
    /// Gets the list of mappers.
    /// </summary>
    public IReadOnlyList<Mapper> Mappers { get; }

    /// <summary>
    /// Constructs a new instance of <see cref="Almanac"/>.
    /// </summary>
    /// <param name="seeds">The list of seeds.</param>
    /// <param name="mappers">The list of mappers.</param>
    public Almanac(IEnumerable<long> seeds, IEnumerable<Mapper> mappers)
    {
        Seeds = seeds.ToList();
        Mappers = mappers.ToList();
    }

    /// <summary>
    /// Gets the minimum seed.
    /// </summary>
    /// <returns>The minimum seed.</returns>
    public long GetMinSeed()
    {
        var min = long.MaxValue;
        foreach (var seed in Seeds)
        {
            var result = seed;
            foreach (var mapper in Mappers)
                result = mapper.Map(result);

            if (result < min)
                min = result;
        }
        return min;
    }

    /// <summary>
    /// Gets the minimum seed location.
    /// </summary>
    /// <param name="startValue">The start location value.</param>
    /// <returns>The minimum seed location.</returns>
    public long GetMinSeedLocation(long startValue = 0)
    {
        var location = startValue;
        var seedRange = GetSeedMapperEntries().ToList();
        while (true)
        {
            var result = location;
            for (var i = Mappers.Count - 1; i >= 0; i--)
                result = Mappers[i].Unmap(result);

            // use index for here for optimised performance and memory
            // ReSharper disable once ForCanBeConvertedToForeach
            // ReSharper disable once LoopCanBeConvertedToQuery
            for (var i = 0; i < seedRange.Count; i++)
            {
                if (seedRange[i].InSourceRange(result))
                    return location;
            }
            location++;
        }
    }

    /// <summary>
    /// Gets the list of seed ranges expressed as <see cref="MapperEntry"/>.
    /// The seed initial value will be in source range.
    /// </summary>
    /// <returns>The list of seed ranges expressed as <see cref="MapperEntry"/>.</returns>
    private IEnumerable<MapperEntry> GetSeedMapperEntries()
    {
        var seedRanges = Seeds.Chunk(2);
        return seedRanges.Select(range => new MapperEntry(range[0], 0, range[1]));
    }
}
