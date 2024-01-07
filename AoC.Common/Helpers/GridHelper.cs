using AoC.Common.Grid;

namespace AoC.Common.Helpers;

/// <summary>
/// Helper methods for a grid.
/// </summary>
public static class GridHelper
{
    /// <summary>
    /// Returns a character at the specified position in a character grid.
    /// </summary>
    /// <param name="grid">The character grid.</param>
    /// <param name="position">The position of the character.</param>
    /// <returns>The character at the specified position.</returns>
    public static char At(this IReadOnlyList<string> grid, Position position)
    {
        return grid[position.Row32][position.Column32];
    }

    /// <summary>
    /// Returns an object at the specified position in a grid.
    /// </summary>
    /// <param name="grid">The grid.</param>
    /// <param name="position">The position of the object.</param>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <returns>The object at the specified position.</returns>
    public static T At<T>(this IReadOnlyList<IReadOnlyList<T>> grid, Position position)
    {
        return grid[position.Row32][position.Column32];
    }
}
