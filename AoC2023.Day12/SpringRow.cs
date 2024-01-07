namespace AoC2023.Day12;

/// <summary>
/// Represents a row of springs.
/// </summary>
public class SpringRow
{
    private const char Damaged = '#';
    private const char Operational = '.';
    private const char Unknown = '?';
    private readonly Dictionary<string, long> _cache = new();

    /// <summary>
    /// Gets the row of springs.
    /// </summary>
    public string Springs { get; }

    /// <summary>
    /// Gets the groupings of damaged springs.
    /// </summary>
    public IReadOnlyList<int> DamagedGroups { get; }

    /// <summary>
    /// Constructs a new instance of <see cref="SpringRow"/>.
    /// </summary>
    /// <param name="springs">The row of springs.</param>
    /// <param name="damagedGroups">The damaged springs groupings.</param>
    public SpringRow(string springs, IEnumerable<int> damagedGroups)
    {
        Springs = springs;
        DamagedGroups = damagedGroups.ToList();
    }

    /// <summary>
    /// Counts the spring arrangements.
    /// </summary>
    /// <param name="copies">The number of times to copy the springs and damaged groups.</param>
    /// <returns>The spring arrangements.</returns>
    public long GetCount(int copies = 1)
    {
        if (copies == 1)
            return GetCountRecurse(Springs, DamagedGroups.ToList());

        var springsRepeated = Enumerable.Repeat(Springs, copies);
        var groupsRepeated = Enumerable.Repeat(DamagedGroups, copies);
        return GetCountRecurse(string.Join(Unknown, springsRepeated), groupsRepeated.SelectMany(x => x).ToList());
    }

    /// <summary>
    /// Counts the spring arrangements recursively.
    /// </summary>
    /// <param name="springs">The current arrangement of springs.</param>
    /// <param name="groups">The damaged springs groupings.</param>
    /// <returns>The spring arrangements.</returns>
    private long GetCountRecurse(string springs, List<int> groups)
    {
        var key = $"{springs} {string.Join(",", groups)}";
        if (_cache.TryGetValue(key, out var value))
            return value;

        var result = GetCount(springs, groups);
        _cache[key] = result;
        return result;
    }

    /// <summary>
    /// Counts the spring arrangements.
    /// </summary>
    /// <param name="springs">The current arrangement of springs.</param>
    /// <param name="groups">The damaged springs groupings.</param>
    /// <returns>The spring arrangements.</returns>
    private long GetCount(string springs, List<int> groups)
    {
        while (true)
        {
            if (groups.Count == 0)
                return springs.Contains(Damaged) ? 0 : 1;
            if (string.IsNullOrEmpty(springs))
                return 0;

            if (springs.StartsWith(Operational))
            {
                springs = springs.TrimStart(Operational);
            }
            else if (springs.StartsWith(Unknown))
            {
                var substr = springs[1..];
                return GetCountRecurse($"{Operational}{substr}", groups) + GetCountRecurse($"{Damaged}{substr}", groups);
            }
            else if (springs.StartsWith(Damaged))
            {
                if (groups.Count == 0 || springs.Length < groups[0] || springs[..groups[0]].Contains(Operational))
                    return 0;
                if (groups.Count > 1 && (springs.Length < groups[0] + 1 || springs[groups[0]] == Damaged))
                    return 0;

                springs = groups.Count > 1 ? springs[(groups[0] + 1)..] : springs[groups[0]..];
                groups = groups[1..];
            }
        }
    }
}
