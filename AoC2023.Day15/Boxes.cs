namespace AoC2023.Day15;

/// <summary>
/// Represents a list of boxes storing lenses.
/// </summary>
public class Boxes
{
    private const int Capacity = 256;
    private readonly Dictionary<int, List<Lens>> _map = new(Capacity);

    /// <summary>
    /// Constructs a new instance of <see cref="Boxes"/>.
    /// </summary>
    public Boxes()
    {
        for (var i = 0; i < Capacity; i++)
            _map[i] = [];
    }

    /// <summary>
    /// Runs a box operation.
    /// </summary>
    /// <param name="operation">The operation to run.</param>
    public void RunOperation(string operation)
    {
        var lens = Lens.Parse(operation);
        if (lens.IsRemove)
            RunRemoveOperation(lens);
        else
            RunAddOperation(lens);
    }

    /// <summary>
    /// Computes the focal power of this box.
    /// </summary>
    /// <returns>The focal power of this box.</returns>
    public long GetFocalPower()
    {
        long result = 0;
        foreach (var (boxNum, lenses) in _map)
        {
            for (var i = 0; i < lenses.Count; i++)
                result += (boxNum + 1) * (i + 1) * lenses[i].Focal;
        }
        return result;
    }

    /// <summary>
    /// Runs the addition operation.
    /// </summary>
    /// <param name="lens">The lens to run the operation.</param>
    private void RunAddOperation(Lens lens)
    {
        var existing = _map[lens.BoxNumber].FirstOrDefault(x => x.Label == lens.Label);
        if (existing is null)
            _map[lens.BoxNumber].Add(lens);
        else
            existing.Focal = lens.Focal;
    }

    /// <summary>
    /// Runs the remove operation.
    /// </summary>
    /// <param name="lens">The lens to run the operation.</param>
    private void RunRemoveOperation(Lens lens)
    {
        var existing = _map[lens.BoxNumber].FirstOrDefault(x => x.Label == lens.Label);
        if (existing is not null)
            _map[lens.BoxNumber].Remove(existing);
    }
}
