using AoC.Common.Grid;

namespace AoC2023.Day18;

/// <summary>
/// Represents a dig plan.
/// </summary>
public class DigPlan
{
    /// <summary>
    /// Gets the dig plan instructions.
    /// </summary>
    public IReadOnlyList<Instruction> Instructions { get; }

    /// <summary>
    /// Constructs a new instance of <see cref="DigPlan"/>.
    /// </summary>
    /// <param name="instructions">The dig plan instructions.</param>
    public DigPlan(IReadOnlyList<Instruction> instructions)
    {
        Instructions = instructions;
    }

    /// <summary>
    /// Gets the area to dig.
    /// </summary>
    /// <returns>The area to dig.</returns>
    public long GetDigArea()
    {
        var area = GetShoelaceArea(GetTurningPositions(false));

        // use Pick's theorem
        var boundaryArea = Instructions.Sum(x => x.Distance);
        var interiorArea = area - boundaryArea / 2 + 1;
        return interiorArea + boundaryArea;
    }

    /// <summary>
    /// Gets the encoded area to dig.
    /// </summary>
    /// <returns>The encoded area to dig.</returns>
    public long GetEncodedArea()
    {
        var area = GetShoelaceArea(GetTurningPositions(true));

        // use Pick's theorem
        long boundaryArea = Instructions.Sum(x => x.EncodedDistance);
        var interiorArea = area - boundaryArea / 2 + 1;
        return interiorArea + boundaryArea;
    }

    /// <summary>
    /// Returns the area to dig using the shoelace formula.
    /// </summary>
    /// <param name="positions">The turning positions.</param>
    /// <returns>The area to dig using the shoelace formula.</returns>
    private static long GetShoelaceArea(IReadOnlyList<Position> positions)
    {
        long area = 0;
        for (var i = 0; i < positions.Count; i++)
        {
            var curr = positions[i];
            var next = positions[(i + 1) % positions.Count];
            area += (long)curr.Row32 * next.Column32 - (long)curr.Column32 * next.Row32;
        }
        return Math.Abs(area) / 2;
    }

    /// <summary>
    /// Gets the list of turning positions.
    /// </summary>
    /// <param name="encoded">Whether the direction and distance are encoded.</param>
    /// <returns>The list of turning positions.</returns>
    private List<Position> GetTurningPositions(bool encoded)
    {
        var result = new List<Position>(Instructions.Count + 1) { Position.Origin };
        foreach (var instruction in Instructions)
        {
            var dir = encoded ? instruction.EncodedDirection : instruction.Direction;
            var dist = encoded ? instruction.EncodedDistance : instruction.Distance;
            result.Add(result[^1].GetDestination(dir, dist));
        }
        return result;
    }
}
