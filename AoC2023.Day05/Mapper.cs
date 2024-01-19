namespace AoC2023.Day05;

/// <summary>
/// A collection of <see cref="MapperEntry"/>.
/// </summary>
public class Mapper
{
    private readonly List<MapperEntry> _entries = new();

    /// <summary>
    /// Adds a new <see cref="MapperEntry"/> to this mapper.
    /// </summary>
    /// <param name="entry">The <see cref="MapperEntry"/> to add.</param>
    public void AddMapperEntry(MapperEntry entry)
    {
        _entries.Add(entry);
    }

    /// <summary>
    /// Gets the mapped value.
    /// </summary>
    /// <param name="num">The value to map.</param>
    /// <returns>The mapped value.</returns>
    public long Map(long num)
    {
        var mapEntry = _entries.FirstOrDefault(x => x.InSourceRange(num));
        return mapEntry?.Map(num) ?? num;
    }

    /// <summary>
    /// Gets the source value.
    /// </summary>
    /// <param name="num">The value to unmap.</param>
    /// <returns>The source value.</returns>
    public long Unmap(long num)
    {
        // use index for here for optimised performance and memory
        // ReSharper disable once ForCanBeConvertedToForeach
        for (var i = 0; i < _entries.Count; i++)
        {
            if (_entries[i].InDestinationRange(num))
                return _entries[i].Unmap(num);
        }
        return num;
    }
}
