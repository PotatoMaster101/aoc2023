using AoC.Common.Helpers;
using Xunit;

namespace AoC.Common.Test.Helpers;

/// <summary>
/// Unit tests for <see cref="MathHelper"/>.
/// </summary>
public class MathHelperTest
{
    [Theory]
    [InlineData(8, 16, 8)]
    [InlineData(7, 16, 1)]
    public void GreatestCommonFactor_ReturnsCorrectValue(long a, long b, long expected)
    {
        // act
        var resLt = MathHelper.GreatestCommonFactor(a, b);

        // assert
        Assert.Equal(expected, resLt);
    }

    [Theory]
    [InlineData(0L)]
    [InlineData(16409L, 16409L)]
    [InlineData(269L, 16409L, 19637L, 18023L, 15871L, 14257L, 12643L)]
    public void GreatestCommonFactor_List_ReturnsCorrectValue(long expected, params long[] input)
    {
        // act
        var resLt = MathHelper.GreatestCommonFactor(input);

        // assert
        Assert.Equal(expected, resLt);
    }

    [Theory]
    [InlineData(8, 16, 16)]
    [InlineData(12, 15, 60)]
    public void LeastCommonMultiple_ReturnsCorrectValue(long a, long b, long expected)
    {
        // act
        var resLt = MathHelper.LeastCommonMultiple(a, b);

        // assert
        Assert.Equal(expected, resLt);
    }

    [Theory]
    [InlineData(0L)]
    [InlineData(16409L, 16409L)]
    [InlineData(11795205644011L, 16409L, 19637L, 18023L, 15871L, 14257L, 12643L)]
    public void LeastCommonMultiple_List_ReturnsCorrectValue(long expected, params long[] input)
    {
        // act
        var resLt = MathHelper.LeastCommonMultiple(input);

        // assert
        Assert.Equal(expected, resLt);
    }
}
