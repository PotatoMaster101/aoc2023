using AoC.Common.Grid;
using AoC.Common.Helpers;
using Xunit;

namespace AoC.Common.Test.Helpers;

/// <summary>
/// Unit tests for <see cref="GridHelper"/>.
/// </summary>
public class GridHelperTest
{
    [Theory]
    [InlineData('a', 0, 0, "abc", "abc")]
    [InlineData('b', 0, 1, "abc", "abc")]
    [InlineData('a', 1, 0, "abc", "abc")]
    public void At_ReturnsCorrectChar(char expected, long row, long column, params string[] sut)
    {
        // arrange
        var pos = new Position(row, column);

        // act
        var result = sut.At(pos);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData('a', 0, 0, "abc", "abc")]
    [InlineData('b', 0, 1, "abc", "abc")]
    [InlineData('a', 1, 0, "abc", "abc")]
    public void At_ReturnsCorrectValue(char expected, long row, long column, params string[] grid)
    {
        // arrange
        var pos = new Position(row, column);
        var sut = new List<List<char>>(grid.Length);
        sut.AddRange(grid.Select(text => (List<char>)[..text]));

        // act
        var result = sut.At(pos);

        // assert
        Assert.Equal(expected, result);
    }
}
