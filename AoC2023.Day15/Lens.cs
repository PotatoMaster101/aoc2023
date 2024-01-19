namespace AoC2023.Day15;

/// <summary>
/// Represents a lens.
/// </summary>
public class Lens
{
    private int _boxNumber = -1;

    /// <summary>
    /// Gets the lens label.
    /// </summary>
    public string Label { get; }

    /// <summary>
    /// Gets or sets the lens focal length.
    /// </summary>
    public int Focal { get; set; }

    /// <summary>
    /// Gets whether this lens is suppose to be removed.
    /// </summary>
    public bool IsRemove { get; }

    /// <summary>
    /// Gets the lens box number.
    /// </summary>
    public int BoxNumber => _boxNumber == -1 ? _boxNumber = Convert.ToInt32(Hash.Run(Label)) : _boxNumber;

    /// <summary>
    /// Constructs a new instance of <see cref="Lens"/>.
    /// </summary>
    /// <param name="label">The lens label.</param>
    /// <param name="focal">The lens focal length.</param>
    /// <param name="isRemove">Whether this lens is suppose to be removed.</param>
    private Lens(string label, int focal, bool isRemove)
    {
        Label = label;
        Focal = focal;
        IsRemove = isRemove;
    }

    /// <summary>
    /// Parses a lens from a string.
    /// </summary>
    /// <param name="lens">The string containing the lens.</param>
    /// <returns>The parsed lens.</returns>
    public static Lens Parse(string lens)
    {
        if (lens.EndsWith('-'))
            return new Lens(lens[..^1], -1, true);

        var split = lens.Split('=');
        return new Lens(split[0], int.Parse(split[1]), false);
    }
}
