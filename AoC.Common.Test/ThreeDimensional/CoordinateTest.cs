using System.Collections;
using AoC.Common.ThreeDimensional;
using Xunit;

namespace AoC.Common.Test.ThreeDimensional;

/// <summary>
/// Unit tests for <see cref="Coordinate"/>.
/// </summary>
public class CoordinateTest
{
    [Theory]
    [InlineData(0, 0, 0, 10, 10, 10, -1)]
    [InlineData(0, 0, 0, -10, -10, -10, 1)]
    [InlineData(10, 10, 10, 10, 10, 10, 0)]
    [InlineData(10, 20, 30, 10, 20, 31, -1)]
    [InlineData(10, 20, 30, 10, 20, 29, 1)]
    [InlineData(10, 20, 30, 10, 21, 30, -1)]
    [InlineData(10, 20, 30, 10, 19, 30, 1)]
    [InlineData(10, 20, 30, 11, 20, 30, -1)]
    [InlineData(10, 20, 30, 9, 20, 30, 1)]
    public void CompareTo_ReturnsCorrectValue(long x1, long y1, long z1, long x2, long y2, long z2, int expected)
    {
        // arrange
        var sut = new Coordinate(x1, y1, z1);
        var other = new Coordinate(x2, y2, z2);

        // act
        var result = sut.CompareTo(other);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(10, 20, 30)]
    [InlineData(-10, -20, -30)]
    public void Constructor_SetsMembers(long x, long y, long z)
    {
        // act
        var result = new Coordinate(x, y, z);

        // assert
        Assert.Equal(x, result.X);
        Assert.Equal(y, result.Y);
        Assert.Equal(z, result.Z);
    }

    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(10, 20, 30)]
    [InlineData(-10, -20, -30)]
    public void Deconstruct_ReturnsCorrectValues(long x, long y, long z)
    {
        // arrange
        var sut = new Coordinate(x, y, z);

        // act
        var (resultX, resultY, resultZ) = sut;

        // assert
        Assert.Equal(x, resultX);
        Assert.Equal(y, resultY);
        Assert.Equal(z, resultZ);
    }

    [Theory]
    [InlineData(0, 0, 0, Direction.NegativeX, 10, -10, 0, 0)]
    [InlineData(0, 0, 0, Direction.NegativeY, 10, 0, -10, 0)]
    [InlineData(0, 0, 0, Direction.NegativeZ, 10, 0, 0, -10)]
    [InlineData(0, 0, 0, Direction.PositiveX, 10, 10, 0, 0)]
    [InlineData(0, 0, 0, Direction.PositiveY, 10, 0, 10, 0)]
    [InlineData(0, 0, 0, Direction.PositiveZ, 10, 0, 0, 10)]
    public void GetDestination_ReturnsCorrectValue(long x, long y, long z, Direction dir, long dist, long expectedX, long expectedY, long expectedZ)
    {
        // arrange
        var sut = new Coordinate(x, y, z);

        // act
        var result = sut.GetDestination(dir, dist);

        // assert
        Assert.Equal(expectedX, result.X);
        Assert.Equal(expectedY, result.Y);
        Assert.Equal(expectedZ, result.Z);
    }

    [Theory]
    [ClassData(typeof(GetLineTestData))]
    public void GetLine_ReturnsCorrectValues(long x, long y, long z, Direction dir, long dist, Coordinate[] expected)
    {
        // arrange
        var sut = new Coordinate(x, y, z);

        // act
        var result = sut.GetLine(dir, dist);

        // assert
        var resultArray = result.ToArray();
        Assert.Equal(expected.Length, resultArray.Length);
        foreach (var resultCoor in resultArray)
            Assert.Contains(expected, c => c == resultCoor);
    }

    [Fact]
    public void Origin_GetsCorrectValue()
    {
        // act
        var result = Coordinate.Origin;

        // assert
        Assert.Equal(0, result.X);
        Assert.Equal(0, result.Y);
        Assert.Equal(0, result.Z);
    }

    [Theory]
    [InlineData(0, 0, 0, "(0, 0, 0)")]
    [InlineData(10, 20, 30, "(10, 20, 30)")]
    [InlineData(-10, -20, -30, "(-10, -20, -30)")]
    public void ToString_ReturnsCorrectValue(long x, long y, long z, string expected)
    {
        // arrange
        var sut = new Coordinate(x, y, z);

        // act
        var result = sut.ToString();

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, 0, 0, 5)]
    [InlineData(10, 20, 30, 0)]
    [InlineData(-10, -20, -30, 0)]
    public void WithX_ReturnsCorrectValue(long x, long y, long z, long newX)
    {
        // arrange
        var sut = new Coordinate(x, y, z);

        // act
        var result = sut.WithX(newX);

        // assert
        Assert.Equal(newX, result.X);
        Assert.Equal(y, result.Y);
        Assert.Equal(z, result.Z);
    }

    [Theory]
    [InlineData(0, 0, 0, 5)]
    [InlineData(10, 20, 30, 0)]
    [InlineData(-10, -20, -30, 0)]
    public void WithY_ReturnsCorrectValue(long x, long y, long z, long newY)
    {
        // arrange
        var sut = new Coordinate(x, y, z);

        // act
        var result = sut.WithY(newY);

        // assert
        Assert.Equal(x, result.X);
        Assert.Equal(newY, result.Y);
        Assert.Equal(z, result.Z);
    }

    [Theory]
    [InlineData(0, 0, 0, 5)]
    [InlineData(10, 20, 30, 0)]
    [InlineData(-10, -20, -30, 0)]
    public void WithZ_ReturnsCorrectValue(long x, long y, long z, long newZ)
    {
        // arrange
        var sut = new Coordinate(x, y, z);

        // act
        var result = sut.WithZ(newZ);

        // assert
        Assert.Equal(x, result.X);
        Assert.Equal(y, result.Y);
        Assert.Equal(newZ, result.Z);
    }

    private class GetLineTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return
            [
                0, 0, 0, Direction.NegativeX, 5, new[]
                {
                    Coordinate.Origin,
                    new Coordinate(-1),
                    new Coordinate(-2),
                    new Coordinate(-3),
                    new Coordinate(-4),
                    new Coordinate(-5)
                }
            ];
            yield return
            [
                0, 0, 0, Direction.NegativeY, 5, new[]
                {
                    Coordinate.Origin,
                    new Coordinate(0, -1),
                    new Coordinate(0, -2),
                    new Coordinate(0, -3),
                    new Coordinate(0, -4),
                    new Coordinate(0, -5)
                }
            ];
            yield return
            [
                0, 0, 0, Direction.NegativeZ, 5, new[]
                {
                    Coordinate.Origin,
                    new Coordinate(0, 0, -1),
                    new Coordinate(0, 0, -2),
                    new Coordinate(0, 0, -3),
                    new Coordinate(0, 0, -4),
                    new Coordinate(0, 0, -5)
                }
            ];
            yield return
            [
                0, 0, 0, Direction.PositiveX, 5, new[]
                {
                    Coordinate.Origin,
                    new Coordinate(1),
                    new Coordinate(2),
                    new Coordinate(3),
                    new Coordinate(4),
                    new Coordinate(5)
                }
            ];
            yield return
            [
                0, 0, 0, Direction.PositiveY, 5, new[]
                {
                    Coordinate.Origin,
                    new Coordinate(0, 1),
                    new Coordinate(0, 2),
                    new Coordinate(0, 3),
                    new Coordinate(0, 4),
                    new Coordinate(0, 5)
                }
            ];
            yield return
            [
                0, 0, 0, Direction.PositiveZ, 5, new[]
                {
                    Coordinate.Origin,
                    new Coordinate(0, 0, 1),
                    new Coordinate(0, 0, 2),
                    new Coordinate(0, 0, 3),
                    new Coordinate(0, 0, 4),
                    new Coordinate(0, 0, 5)
                }
            ];
        }
    }
}
