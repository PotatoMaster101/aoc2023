using AoC.Common.Ranges;
using Xunit;

namespace AoC.Common.Test.Ranges;

/// <summary>
/// Unit tests for <see cref="IntegerRange"/>
/// </summary>
public class IntegerRangeTest
{
    [Theory]
    [InlineData(0, 10, 5)]
    [InlineData(-10, 0, 1)]
    [InlineData(-10, 10, 2)]
    [InlineData(0, 0, 3)]
    public void Constructor_SetsMembers(long min, long max, long increment)
    {
        // act
        var sut = new IntegerRange(min, max, increment);

        // assert
        Assert.Equal(min, sut.Min);
        Assert.Equal(max, sut.Max);
        Assert.Equal(increment, sut.Increment);
    }

    [Theory]
    [InlineData(10, 0)]
    [InlineData(0, -10)]
    [InlineData(10, -10)]
    public void Constructor_ThrowsOnMinMaxOutOfRange(long min, long max)
    {
        // assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new IntegerRange(min, max));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_ThrowsOnIncrementOutOfRange(long increment)
    {
        // assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new IntegerRange(0, 10, increment));
    }

    [Theory]
    [InlineData(0, 10, 11)]
    [InlineData(-10, 10, 21)]
    public void Count_GetsCorrectValue(long min, long max, long expected)
    {
        // arrange
        var sut = new IntegerRange(min, max);

        // act
        var result = sut.Count;

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, 10)]
    [InlineData(-10, 0)]
    [InlineData(-10, 10)]
    [InlineData(0, 0)]
    public void Deconstruct_ReturnsCorrectValue(long min, long max)
    {
        // arrange
        var sut = new IntegerRange(min, max);

        // act
        var (resultMin, resultMax) = sut;

        // assert
        Assert.Equal(min, resultMin);
        Assert.Equal(max, resultMax);
    }

    [Theory]
    [InlineData(0, 5, 1, 0L, 1L, 2L, 3L, 4L, 5L)]
    [InlineData(-5, 0, 1, -5L, -4L, -3L, -2L, -1L, 0L)]
    [InlineData(0, 10, 3, 0L, 3L, 6L, 9L)]
    [InlineData(-10, 0, 3, -10L, -7L, -4L, -1L)]
    public void GetEnumerator_ReturnsCorrectValue(long min, long max, long increment, params long[] expected)
    {
        // arrange
        var sut = new IntegerRange(min, max, increment);

        // act
        var result = sut.ToArray();

        // assert
        Assert.Equal(expected.Length, result.Length);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, 10, 0, true)]
    [InlineData(0, 10, 10, true)]
    [InlineData(0, 10, 5, true)]
    [InlineData(0, 10, -1, false)]
    [InlineData(0, 10, 11, false)]
    public void InRange_ReturnsCorrectValue(long min, long max, long element, bool expected)
    {
        // arrange
        var sut = new IntegerRange(min, max);

        // act
        var result = sut.InRange(element);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, 10, 5, false, 0, 4)]
    [InlineData(0, 10, 5, true, 0, 5)]
    [InlineData(-10, 0, -5, false, -10, -6)]
    [InlineData(-10, 0, -5, true, -10, -5)]
    [InlineData(0, 2, 1, false, 0, 0)]
    [InlineData(0, 2, 1, true, 0, 1)]
    public void SplitLowerRange_ReturnsCorrectValue(long min, long max, long midPoint, bool includeMidpoint, long expectedMin, long expectedMax)
    {
        // arrange
        var sut = new IntegerRange(min, max);

        // act
        var result = sut.SplitLowerRange(midPoint, includeMidpoint);

        // assert
        Assert.Equal(expectedMin, result.Min);
        Assert.Equal(expectedMax, result.Max);
    }

    [Theory]
    [InlineData(0, 10, 0)]
    [InlineData(0, 10, 10)]
    [InlineData(0, 10, -1)]
    [InlineData(0, 10, 11)]
    [InlineData(-10, 0, -11)]
    [InlineData(-10, 0, 0)]
    public void SplitLowerRange_ThrowsOnOutOfRange(long min, long max, long midPoint)
    {
        // arrange
        var sut = new IntegerRange(min, max);

        // assert
        Assert.Throws<ArgumentOutOfRangeException>(() => sut.SplitLowerRange(midPoint));
    }

    [Theory]
    [InlineData(0, 10, 5, false, 6, 10)]
    [InlineData(0, 10, 5, true, 5, 10)]
    [InlineData(-10, 0, -5, false, -4, 0)]
    [InlineData(-10, 0, -5, true, -5, 0)]
    [InlineData(0, 2, 1, false, 2, 2)]
    [InlineData(0, 2, 1, true, 1, 2)]
    public void SplitUpperRange_ReturnsCorrectValue(long min, long max, long midPoint, bool includeMidpoint, long expectedMin, long expectedMax)
    {
        // arrange
        var sut = new IntegerRange(min, max);

        // act
        var result = sut.SplitUpperRange(midPoint, includeMidpoint);

        // assert
        Assert.Equal(expectedMin, result.Min);
        Assert.Equal(expectedMax, result.Max);
    }

    [Theory]
    [InlineData(0, 10, 0)]
    [InlineData(0, 10, 10)]
    [InlineData(0, 10, -1)]
    [InlineData(0, 10, 11)]
    [InlineData(-10, 0, -11)]
    [InlineData(-10, 0, 0)]
    public void SplitUpperRange_ThrowsOnOutOfRange(long min, long max, long midPoint)
    {
        // arrange
        var sut = new IntegerRange(min, max);

        // assert
        Assert.Throws<ArgumentOutOfRangeException>(() => sut.SplitUpperRange(midPoint));
    }

    [Theory]
    [InlineData(0, 10, 1, 0, 1, true)]
    [InlineData(0, 10, 1, 9, 10, true)]
    [InlineData(0, 10, 1, 10, 11, false)]
    [InlineData(0, 10, 1, 11, 12, false)]
    [InlineData(0, 10, 1, -10, -9, false)]
    [InlineData(0, 10, 3, 0, 3, true)]
    public void TryGetNext_ReturnsCorrectValue(long min, long max, long increment, long current, long expectedLong, bool expectedBool)
    {
        // arrange
        var sut = new IntegerRange(min, max, increment);

        // act
        var result = sut.TryGetNext(current, out var resultLong);

        // assert
        Assert.Equal(result, expectedBool);
        Assert.Equal(expectedLong, resultLong);
    }

    [Theory]
    [InlineData(0, 10, 1, 10, 9, true)]
    [InlineData(0, 10, 1, 1, 0, true)]
    [InlineData(0, 10, 1, 0, -1, false)]
    [InlineData(0, 10, 1, -1, -2, false)]
    [InlineData(0, 10, 1, 15, 14, false)]
    [InlineData(0, 10, 3, 5, 2, true)]
    public void TryGetPrevious_ReturnsCorrectValue(long min, long max, long increment, long current, long expectedLong, bool expectedBool)
    {
        // arrange
        var sut = new IntegerRange(min, max, increment);

        // act
        var result = sut.TryGetPrevious(current, out var resultLong);

        // assert
        Assert.Equal(result, expectedBool);
        Assert.Equal(expectedLong, resultLong);
    }
}
