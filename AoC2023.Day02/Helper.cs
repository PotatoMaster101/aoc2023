namespace AoC2023.Day02;

/// <summary>
/// Helper methods.
/// </summary>
public static class Helper
{
    /// <summary>
    /// Gets the minimum power using the collection of cubes.
    /// </summary>
    /// <param name="cubes">The collection of cubes.</param>
    /// <returns>The minimum power from the collection of cubes.</returns>
    public static long GetMinPower(IEnumerable<CubeShow> cubes)
    {
        int highestRed = 0, highestGreen = 0, highestBlue = 0;
        foreach (var cube in cubes)
        {
            if (cube.Red > highestRed)
                highestRed = cube.Red;
            if (cube.Green > highestGreen)
                highestGreen = cube.Green;
            if (cube.Blue > highestBlue)
                highestBlue = cube.Blue;
        }
        return highestRed * highestGreen * highestBlue;
    }
}
