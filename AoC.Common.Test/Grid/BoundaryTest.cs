using System.Collections;
using AoC.Common.Grid;
using Xunit;

namespace AoC.Common.Test.Grid;

/// <summary>
/// Unit tests for <see cref="Boundary"/>
/// </summary>
public class BoundaryTest
{
    [Theory]
    [InlineData(1, 1, 1, 1)]
    [InlineData(10, 5, 10, 5)]
    [InlineData(5, 10, 5, 10)]
    [InlineData(10, 10, 10, 10)]
    public void Constructor_SetsMembers(long rowCount, long columnCount, long expectedRow32, long expectedColumn32)
    {
        // act
        var sut = new Boundary(rowCount, columnCount);

        // assert
        Assert.Equal(rowCount, sut.RowCount);
        Assert.Equal(columnCount, sut.ColumnCount);
        Assert.Equal(expectedRow32, sut.RowCount32);
        Assert.Equal(expectedColumn32, sut.ColumnCount32);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(-1, -1)]
    [InlineData(1, -1)]
    [InlineData(-1, 1)]
    public void Constructor_ThrowsOutOfRange(long rowCount, long columnCount)
    {
        // assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new Boundary(rowCount, columnCount));
    }

    [Theory]
    [InlineData(1, 1, 0, 0)]
    [InlineData(10, 10, 9, 0)]
    public void BottomLeft_GetsCorrectValue(long rowCount, long columnCount, long expectedRow, long expectedColumn)
    {
        // arrange
        var sut = new Boundary(rowCount, columnCount);

        // act
        var result = sut.BottomLeft;

        // assert
        Assert.Equal(expectedRow, result.Row);
        Assert.Equal(expectedColumn, result.Column);
    }

    [Theory]
    [InlineData(1, 1, 0, 0)]
    [InlineData(10, 10, 9, 9)]
    public void BottomRight_GetsCorrectValue(long rowCount, long columnCount, long expectedRow, long expectedColumn)
    {
        // arrange
        var sut = new Boundary(rowCount, columnCount);

        // act
        var result = sut.BottomRight;

        // assert
        Assert.Equal(expectedRow, result.Row);
        Assert.Equal(expectedColumn, result.Column);
    }


    [Theory]
    [InlineData(3, 6)]
    [InlineData(100, 100)]
    public void Deconstruct_ReturnsCorrectValue(long rowCount, long columnCount)
    {
        // arrange
        var sut = new Boundary(rowCount, columnCount);

        // act
        var (resultRows, resultCols) = sut;

        // assert
        Assert.Equal(rowCount, resultRows);
        Assert.Equal(columnCount, resultCols);
    }

    [Theory]
    [ClassData(typeof(GetColumnPositionsTestData))]
    public void GetColumnPositions_ReturnsCorrectValues(long rowCount, long columnCount, long colIdx, Position[] expected)
    {
        // arrange
        var sut = new Boundary(rowCount, columnCount);

        // act
        var result = sut.GetColumnPositions(colIdx);

        // assert
        var resultArray = result.ToArray();
        Assert.Equal(expected.Length, resultArray.Length);
        foreach (var resultPos in resultArray)
            Assert.Contains(expected, x => x == resultPos);
    }

    [Theory]
    [ClassData(typeof(GetRowPositionsTestData))]
    public void GetRowPositions_ReturnsCorrectValues(long rowCount, long columnCount, long rowIdx, Position[] expected)
    {
        // arrange
        var sut = new Boundary(rowCount, columnCount);

        // act
        var result = sut.GetRowPositions(rowIdx);

        // assert
        var resultArray = result.ToArray();
        Assert.Equal(expected.Length, resultArray.Length);
        foreach (var resultPos in resultArray)
            Assert.Contains(expected, x => x == resultPos);
    }

    [Theory]
    [ClassData(typeof(GetValidCrossNeighboursTestData))]
    public void GetValidCrossNeighbours_ReturnsCorrectValues(long rowCount, long columnCount, long row, long column, Position[] expected)
    {
        // arrange
        var pos = new Position(row, column);
        var sut = new Boundary(rowCount, columnCount);

        // act
        var result = sut.GetValidCrossNeighbours(pos);

        // assert
        var resultArray = result.ToArray();
        Assert.Equal(expected.Length, resultArray.Length);
        foreach (var resultPos in resultArray)
            Assert.Contains(expected, x => x == resultPos);
    }

    [Theory]
    [ClassData(typeof(GetValidDiagonalNeighboursTestData))]
    public void GetValidDiagonalNeighbours_ReturnsCorrectValues(long rowCount, long columnCount, long row, long column, Position[] expected)
    {
        // arrange
        var pos = new Position(row, column);
        var sut = new Boundary(rowCount, columnCount);

        // act
        var result = sut.GetValidDiagonalNeighbours(pos);

        // assert
        var resultArray = result.ToArray();
        Assert.Equal(expected.Length, resultArray.Length);
        foreach (var resultPos in resultArray)
            Assert.Contains(expected, x => x == resultPos);
    }

    [Theory]
    [ClassData(typeof(GetValidNeighboursTestData))]
    public void GetValidNeighbours_ReturnsCorrectValue(long rowCount, long columnCount, long row, long column, Position[] expected)
    {
        // arrange
        var pos = new Position(row, column);
        var sut = new Boundary(rowCount, columnCount);

        // act
        var result = sut.GetValidNeighbours(pos);

        // assert
        var resultArray = result.ToArray();
        Assert.Equal(expected.Length, resultArray.Length);
        foreach (var resultPos in resultArray)
            Assert.Contains(expected, x => x == resultPos);
    }

    [Theory]
    [InlineData(10, 10, 0, 0, true)]
    [InlineData(10, 10, 3, 6, true)]
    [InlineData(10, 10, 10, 0, false)]
    [InlineData(10, 10, 0, 10, false)]
    [InlineData(10, 10, 10, 10, false)]
    [InlineData(10, 10, -1, -1, false)]
    [InlineData(10, 10, -1, 0, false)]
    [InlineData(10, 10, 0, -1, false)]
    public void IsValid_ReturnsCorrectValue(long rowCount, long columnCount, long row, long column, bool expected)
    {
        // arrange
        var pos = new Position(row, column);
        var sut = new Boundary(rowCount, columnCount);

        // act
        var result = sut.IsValid(pos);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(1, 1, 0, 0)]
    [InlineData(10, 10, 0, 0)]
    public void TopLeft_GetsCorrectValue(long rowCount, long columnCount, long expectedRow, long expectedColumn)
    {
        // arrange
        var sut = new Boundary(rowCount, columnCount);

        // act
        var result = sut.TopLeft;

        // assert
        Assert.Equal(expectedRow, result.Row);
        Assert.Equal(expectedColumn, result.Column);
    }

    [Theory]
    [InlineData(1, 1, 0, 0)]
    [InlineData(10, 10, 0, 9)]
    public void TopRight_GetsCorrectValue(long rowCount, long columnCount, long expectedRow, long expectedColumn)
    {
        // arrange
        var sut = new Boundary(rowCount, columnCount);

        // act
        var result = sut.TopRight;

        // assert
        Assert.Equal(expectedRow, result.Row);
        Assert.Equal(expectedColumn, result.Column);
    }

    [Theory]
    [InlineData(1, 1, "1 x 1")]
    [InlineData(100, 50, "100 x 50")]
    [InlineData(50, 100, "50 x 100")]
    public void ToString_ReturnsCorrectValue(long rowCount, long columnCount, string expected)
    {
        // arrange
        var sut = new Boundary(rowCount, columnCount);

        // act
        var result = sut.ToString();

        // assert
        Assert.Equal(expected, result);
    }

    private class GetColumnPositionsTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return
            [
                5, 5, 0, new Position[]
                {
                    new(0),
                    new(1),
                    new(2),
                    new(3),
                    new(4)
                }
            ];
            yield return
            [
                5, 5, 4, new Position[]
                {
                    new(0, 4),
                    new(1, 4),
                    new(2, 4),
                    new(3, 4),
                    new(4, 4)
                }
            ];
            yield return [5, 5, 5, Array.Empty<Position>()];
            yield return [5, 5, -1, Array.Empty<Position>()];
        }
    }

    private class GetRowPositionsTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return
            [
                5, 5, 0, new Position[]
                {
                    new(0),
                    new(0, 1),
                    new(0, 2),
                    new(0, 3),
                    new(0, 4)
                }
            ];
            yield return
            [
                5, 5, 4, new Position[]
                {
                    new(4),
                    new(4, 1),
                    new(4, 2),
                    new(4, 3),
                    new(4, 4)
                }
            ];
            yield return [5, 5, 5, Array.Empty<Position>()];
            yield return [5, 5, -1, Array.Empty<Position>()];
        }
    }

    private class GetValidCrossNeighboursTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return
            [
                10, 10, 5, 5, new Position[]
                {
                    new(4, 5),
                    new(6, 5),
                    new(5, 4),
                    new(5, 6)
                }
            ];
            yield return
            [
                10, 10, 0, 0, new Position[]
                {
                    new(1),
                    new(0, 1)
                }
            ];
            yield return
            [
                10, 10, 9, 0, new Position[]
                {
                    new(8),
                    new(9, 1)
                }
            ];
            yield return
            [
                10, 10, 0, 9, new Position[]
                {
                    new(1, 9),
                    new(0, 8)
                }
            ];
            yield return
            [
                10, 10, 9, 9, new Position[]
                {
                    new(8, 9),
                    new(9, 8)
                }
            ];
        }
    }

    private class GetValidDiagonalNeighboursTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return
            [
                10, 10, 5, 5, new Position[]
                {
                    new(4, 4),
                    new(4, 6),
                    new(6, 4),
                    new(6, 6)
                }
            ];
            yield return
            [
                10, 10, 0, 0, new Position[]
                {
                    new(1, 1)
                }
            ];
            yield return
            [
                10, 10, 9, 0, new Position[]
                {
                    new(8, 1)
                }
            ];
            yield return
            [
                10, 10, 0, 9, new Position[]
                {
                    new(1, 8),
                }
            ];
            yield return
            [
                10, 10, 9, 9, new Position[]
                {
                    new(8, 8),
                }
            ];
        }
    }

    private class GetValidNeighboursTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return
            [
                10, 10, 5, 5, new Position[]
                {
                    new(4, 5),
                    new(6, 5),
                    new(5, 4),
                    new(5, 6),
                    new(4, 4),
                    new(4, 6),
                    new(6, 4),
                    new(6, 6)
                }
            ];
            yield return
            [
                10, 10, 0, 0, new Position[]
                {
                    new(1),
                    new(0, 1),
                    new(1, 1)
                }
            ];
            yield return
            [
                10, 10, 9, 0, new Position[]
                {
                    new(8),
                    new(9, 1),
                    new(8, 1)
                }
            ];
            yield return
            [
                10, 10, 0, 9, new Position[]
                {
                    new(1, 9),
                    new(0, 8),
                    new(1, 8)
                }
            ];
            yield return
            [
                10, 10, 9, 9, new Position[]
                {
                    new(8, 9),
                    new(9, 8),
                    new(8, 8)
                }
            ];
        }
    }
}
