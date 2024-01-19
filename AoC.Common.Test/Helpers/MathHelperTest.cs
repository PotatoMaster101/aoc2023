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
        var result = MathHelper.GreatestCommonFactor(a, b);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0L)]
    [InlineData(16409L, 16409L)]
    [InlineData(269L, 16409L, 19637L, 18023L, 15871L, 14257L, 12643L)]
    public void GreatestCommonFactor_List_ReturnsCorrectValue(long expected, params long[] input)
    {
        // act
        var result = MathHelper.GreatestCommonFactor(input);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(8, 16, 16)]
    [InlineData(12, 15, 60)]
    public void LeastCommonMultiple_ReturnsCorrectValue(long a, long b, long expected)
    {
        // act
        var result = MathHelper.LeastCommonMultiple(a, b);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0L)]
    [InlineData(16409L, 16409L)]
    [InlineData(11795205644011L, 16409L, 19637L, 18023L, 15871L, 14257L, 12643L)]
    public void LeastCommonMultiple_List_ReturnsCorrectValue(long expected, params long[] input)
    {
        // act
        var result = MathHelper.LeastCommonMultiple(input);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, 0, 0, 0)]
    [InlineData(0, 1, 0, 1)]
    [InlineData(-1, 1, -1, 1)]
    [InlineData(-100, -99, -100, -99)]
    [InlineData(100, 99, 99, 100)]
    public void MinMax_ReturnsCorrectValues(long a, long b, long min, long max)
    {
        // act
        var (resultMin, resultMax) = MathHelper.MinMax(a, b);

        // assert
        Assert.Equal(min, resultMin);
        Assert.Equal(max, resultMax);
    }
}
