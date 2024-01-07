using System.Collections;
using AoC.Common.Grid;
using Xunit;

namespace AoC.Common.Test.Grid;

/// <summary>
/// Unit tests for <see cref="Position"/>.
/// </summary>
public class PositionTest
{
    [Theory]
    [InlineData(0, 0, 1, 1, 1)]
    [InlineData(10, 10, 5, 15, 15)]
    [InlineData(10, 10, -5, 5, 5)]
    public void Add_Amount_ReturnsCorrectValue(long row, long column, long amount, long expectedRow, long expectedColumn)
    {
        // arrange
        var sut = new Position(row, column);

        // act
        var result = sut.Add(amount);

        // assert
        Assert.Equal(expectedRow, result.Row);
        Assert.Equal(expectedColumn, result.Column);
    }

    [Theory]
    [InlineData(0, 0, 1, 1, 1, 1)]
    [InlineData(10, 10, 3, 6, 13, 16)]
    [InlineData(10, 10, 1, -1, 11, 9)]
    public void Add_Position_ReturnsCorrectValue(long row1, long column1, long row2, long column2, long expectedRow, long expectedColumn)
    {
        // arrange
        var sut = new Position(row1, column1);
        var another = new Position(row2, column2);

        // act
        var result = sut.Add(another);

        // assert
        Assert.Equal(expectedRow, result.Row);
        Assert.Equal(expectedColumn, result.Column);
    }

    [Theory]
    [InlineData(10, 10, 11, 10)]
    [InlineData(5, 20, 6, 20)]
    public void Bottom_GetsCorrectValue(long row, long column, long expectedRow, long expectedColumn)
    {
        // arrange
        var sut = new Position(row, column);

        // act
        var result = sut.Bottom;

        // assert
        Assert.Equal(expectedRow, result.Row);
        Assert.Equal(expectedColumn, result.Column);
    }

    [Theory]
    [InlineData(10, 10, 11, 9)]
    [InlineData(5, 20, 6, 19)]
    public void BottomLeft_GetsCorrectValue(long row, long column, long expectedRow, long expectedColumn)
    {
        // arrange
        var sut = new Position(row, column);

        // act
        var result = sut.BottomLeft;

        // assert
        Assert.Equal(expectedRow, result.Row);
        Assert.Equal(expectedColumn, result.Column);
    }

    [Theory]
    [InlineData(10, 10, 11, 11)]
    [InlineData(5, 20, 6, 21)]
    public void BottomRight_GetsCorrectValue(long row, long column, long expectedRow, long expectedColumn)
    {
        // arrange
        var sut = new Position(row, column);

        // act
        var result = sut.BottomRight;

        // assert
        Assert.Equal(expectedRow, result.Row);
        Assert.Equal(expectedColumn, result.Column);
    }

    [Theory]
    [InlineData(0, 0, 0, 0, 0)]
    [InlineData(0, 0, 1, 0, -1)]
    [InlineData(0, 0, 0, 1, -1)]
    [InlineData(0, 0, 1, 1, -1)]
    [InlineData(1, 0, 0, 0, 1)]
    [InlineData(0, 1, 0, 0, 1)]
    [InlineData(1, 1, 0, 0, 1)]
    public void CompareTo_ReturnsCorrectValue(long row, long column, long otherRow, long otherColumn, long expected)
    {
        // arrange
        var sut = new Position(row, column);
        var other = new Position(otherRow, otherColumn);

        // act
        var result = sut.CompareTo(other);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(-1, -1, -1, -1)]
    [InlineData(0, 0, 0, 0)]
    [InlineData(10, 5, 10, 5)]
    [InlineData(5, 10, 5, 10)]
    public void Constructor_SetsMembers(long row, long column, long expectedRow32, long expectedColumn32)
    {
        // act
        var sut = new Position(row, column);

        // assert
        Assert.Equal(row, sut.Row);
        Assert.Equal(column, sut.Column);
        Assert.Equal(expectedRow32, sut.Row32);
        Assert.Equal(expectedColumn32, sut.Column32);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(10, 5)]
    [InlineData(-5, -10)]
    public void Deconstruct_ReturnsCorrectValues(long row, long column)
    {
        // arrange
        var sut = new Position(row, column);

        // act
        var (resultRow, resultColumn) = sut;

        // assert
        Assert.Equal(row, resultRow);
        Assert.Equal(column, resultColumn);
    }

    [Theory]
    [InlineData(0, 0, 1, 0, 0)]
    [InlineData(2, 4, 2, 1, 2)]
    public void Divide_ReturnsCorrectValue(long row, long column, long amount, long expectedRow, long expectedColumn)
    {
        // arrange
        var sut = new Position(row, column);

        // act
        var result = sut.Divide(amount);

        // assert
        Assert.Equal(expectedRow, result.Row);
        Assert.Equal(expectedColumn, result.Column);
    }

    [Theory]
    [ClassData(typeof(GetCrossNeighboursTestData))]
    public void GetCrossNeighbours_ReturnsCorrectValues(Position sut, Position[] expected)
    {
        // act
        var result = sut.GetCrossNeighbours();

        // assert
        var resultArray = result.ToArray();
        Assert.Equal(expected.Length, resultArray.Length);
        foreach (var position in resultArray)
            Assert.Contains(expected, x => x == position);
    }

    [Theory]
    [InlineData(10, 10, Direction.Down, 5, 15, 10)]
    [InlineData(10, 10, Direction.Up, 5, 5, 10)]
    [InlineData(10, 10, Direction.Left, 5, 10, 5)]
    [InlineData(10, 10, Direction.Right, 5, 10, 15)]
    public void GetDestination_ReturnsCorrectValue(long row, long column, Direction direction, long distance, long expectedRow, long expectedColumn)
    {
        // arrange
        var sut = new Position(row, column);

        // act
        var result = sut.GetDestination(direction, distance);

        // assert
        Assert.Equal(expectedRow, result.Row);
        Assert.Equal(expectedColumn, result.Column);
    }

    [Theory]
    [ClassData(typeof(GetDiagonalNeighboursTestData))]
    public void GetDiagonalNeighbours_ReturnsCorrectValues(Position sut, Position[] expected)
    {
        // act
        var result = sut.GetDiagonalNeighbours();

        // assert
        var resultArray = result.ToArray();
        Assert.Equal(expected.Length, resultArray.Length);
        foreach (var position in resultArray)
            Assert.Contains(expected, x => x == position);
    }

    [Theory]
    [InlineData(0, 0, 1, 1, 2)]
    [InlineData(1, 1, 0, 0, 2)]
    [InlineData(-1, 0, 0, 1, 2)]
    [InlineData(10, 10, -10, -10, 40)]
    public void GetManhattanDistance_ReturnsCorrectValue(long row1, long column1, long row2, long column2, long expected)
    {
        // arrange
        var sut = new Position(row1, column1);
        var another = new Position(row2, column2);

        // act
        var result = sut.GetManhattanDistance(another);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [ClassData(typeof(GetNeighboursTestData))]
    public void GetNeighbours_ReturnsCorrectValues(Position sut, Position[] expected)
    {
        // act
        var result = sut.GetNeighbours();

        // assert
        var resultArray = result.ToArray();
        Assert.Equal(expected.Length, resultArray.Length);
        foreach (var position in resultArray)
            Assert.Contains(expected, x => x == position);
    }

    [Theory]
    [InlineData(10, 10, 10, 9)]
    [InlineData(5, 20, 5, 19)]
    public void Left_GetsCorrectValue(long row, long column, long expectedRow, long expectedColumn)
    {
        // arrange
        var sut = new Position(row, column);

        // act
        var result = sut.Left;

        // assert
        Assert.Equal(expectedRow, result.Row);
        Assert.Equal(expectedColumn, result.Column);
    }

    [Theory]
    [InlineData(0, 0, 1, 0, 0)]
    [InlineData(1, 1, 3, 3, 3)]
    [InlineData(2, 4, 5, 10, 20)]
    [InlineData(10, 10, 0, 0, 0)]
    public void Multiply_ReturnsCorrectValue(long row, long column, long amount, long expectedRow, long expectedColumn)
    {
        // arrange
        var sut = new Position(row, column);

        // act
        var result = sut.Multiply(amount);

        // assert
        Assert.Equal(expectedRow, result.Row);
        Assert.Equal(expectedColumn, result.Column);
    }

    [Theory]
    [InlineData(0, 0, 0, 0)]
    [InlineData(1, 1, -1, -1)]
    [InlineData(-1, -1, 1, 1)]
    public void Negate_ReturnsCorrectValue(long row, long column, long expectedRow, long expectedColumn)
    {
        // arrange
        var sut = new Position(row, column);

        // act
        var result = sut.Negate();

        // assert
        Assert.Equal(expectedRow, result.Row);
        Assert.Equal(expectedColumn, result.Column);
    }

    [Fact]
    public void Origin_GetsOrigin()
    {
        // act
        var result = Position.Origin;

        // assert
        Assert.Equal(0, result.Row);
        Assert.Equal(0, result.Column);
    }

    [Theory]
    [InlineData(0, 0, 10, 10, 0, 0)]
    [InlineData(5, 5, 10, 10, 5, 5)]
    [InlineData(9, 9, 10, 10, 9, 9)]
    [InlineData(-1, -1, 10, 10, 9, 9)]
    [InlineData(10, 10, 10, 10, 0, 0)]
    public void Reduce_ReturnsCorrectValue(long row, long column, long totalRows, long totalColumns, long expectedRow, long expectedColumn)
    {
        // arrange
        var sut = new Position(row, column);

        // act
        var result = sut.Reduce(totalRows, totalColumns);

        // assert
        Assert.Equal(expectedRow, result.Row);
        Assert.Equal(expectedColumn, result.Column);
    }

    [Theory]
    [InlineData(10, 10, 10, 11)]
    [InlineData(5, 20, 5, 21)]
    public void Right_GetsCorrectValue(long row, long column, long expectedRow, long expectedColumn)
    {
        // arrange
        var sut = new Position(row, column);

        // act
        var result = sut.Right;

        // assert
        Assert.Equal(expectedRow, result.Row);
        Assert.Equal(expectedColumn, result.Column);
    }

    [Theory]
    [InlineData(0, 0, 1, -1, -1)]
    [InlineData(10, 10, 5, 5, 5)]
    [InlineData(10, 10, -5, 15, 15)]
    public void Subtract_Amount_ReturnsCorrectValue(long row, long column, long amount, long expectedRow, long expectedColumn)
    {
        // arrange
        var sut = new Position(row, column);

        // act
        var result = sut.Subtract(amount);

        // assert
        Assert.Equal(expectedRow, result.Row);
        Assert.Equal(expectedColumn, result.Column);
    }

    [Theory]
    [InlineData(0, 0, 1, 1, -1, -1)]
    [InlineData(10, 10, 3, 6, 7, 4)]
    [InlineData(10, 10, 1, -1, 9, 11)]
    public void Subtract_Position_ReturnsCorrectValue(long row1, long column1, long row2, long column2, long expectedRow, long expectedColumn)
    {
        // arrange
        var sut = new Position(row1, column1);
        var another = new Position(row2, column2);

        // act
        var result = sut.Subtract(another);

        // assert
        Assert.Equal(expectedRow, result.Row);
        Assert.Equal(expectedColumn, result.Column);
    }

    [Theory]
    [InlineData(10, 10, 9, 10)]
    [InlineData(5, 20, 4, 20)]
    public void Top_GetsCorrectValue(long row, long column, long expectedRow, long expectedColumn)
    {
        // arrange
        var sut = new Position(row, column);

        // act
        var result = sut.Top;

        // assert
        Assert.Equal(expectedRow, result.Row);
        Assert.Equal(expectedColumn, result.Column);
    }

    [Theory]
    [InlineData(10, 10, 9, 9)]
    [InlineData(5, 20, 4, 19)]
    public void TopLeft_GetsCorrectValue(long row, long column, long expectedRow, long expectedColumn)
    {
        // arrange
        var sut = new Position(row, column);

        // act
        var result = sut.TopLeft;

        // assert
        Assert.Equal(expectedRow, result.Row);
        Assert.Equal(expectedColumn, result.Column);
    }

    [Theory]
    [InlineData(10, 10, 9, 11)]
    [InlineData(5, 20, 4, 21)]
    public void TopRight_GetsCorrectValue(long row, long column, long expectedRow, long expectedColumn)
    {
        // arrange
        var sut = new Position(row, column);

        // act
        var result = sut.TopRight;

        // assert
        Assert.Equal(expectedRow, result.Row);
        Assert.Equal(expectedColumn, result.Column);
    }

    [Theory]
    [InlineData(0, 0, "(0, 0)")]
    [InlineData(10, 5, "(10, 5)")]
    public void ToString_ReturnsCorrectValue(long row, long column, string expected)
    {
        // arrange
        var sut = new Position(row, column);

        // act
        var result = sut.ToString();

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, -1)]
    [InlineData(-1, -10)]
    [InlineData(10, 20)]
    public void WithColumn_ReturnsCorrectValue(long column, long newColumn)
    {
        // arrange
        var sut = new Position(0, column);

        // act
        var result = sut.WithColumn(newColumn);

        // assert
        Assert.Equal(newColumn, result.Column);
    }

    [Theory]
    [InlineData(0, -1)]
    [InlineData(-1, -10)]
    [InlineData(10, 20)]
    public void WithRow_ReturnsCorrectValue(long row, long newRow)
    {
        // arrange
        var sut = new Position(row);

        // act
        var result = sut.WithRow(newRow);

        // assert
        Assert.Equal(newRow, result.Row);
    }

    private class GetCrossNeighboursTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            var pos1 = new Position();
            var pos2 = new Position(10, 10);
            yield return
            [
                pos1, new[]
                {
                    pos1.Bottom,
                    pos1.Left,
                    pos1.Right,
                    pos1.Top
                }
            ];
            yield return
            [
                pos2, new[]
                {
                    pos2.Bottom,
                    pos2.Left,
                    pos2.Right,
                    pos2.Top
                }
            ];
        }
    }

    private class GetDiagonalNeighboursTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            var pos1 = new Position();
            var pos2 = new Position(10, 10);
            yield return
            [
                pos1, new[]
                {
                    pos1.BottomLeft,
                    pos1.BottomRight,
                    pos1.TopLeft,
                    pos1.TopRight
                }
            ];
            yield return
            [
                pos2, new[]
                {
                    pos2.BottomLeft,
                    pos2.BottomRight,
                    pos2.TopLeft,
                    pos2.TopRight
                }
            ];
        }
    }

    private class GetNeighboursTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            var pos1 = new Position();
            var pos2 = new Position(10, 10);
            yield return
            [
                pos1, new[]
                {
                    pos1.Bottom,
                    pos1.BottomLeft,
                    pos1.BottomRight,
                    pos1.Left,
                    pos1.Right,
                    pos1.Top,
                    pos1.TopLeft,
                    pos1.TopRight
                }
            ];
            yield return
            [
                pos2, new[]
                {
                    pos2.Bottom,
                    pos2.BottomLeft,
                    pos2.BottomRight,
                    pos2.Left,
                    pos2.Right,
                    pos2.Top,
                    pos2.TopLeft,
                    pos2.TopRight
                }
            ];
        }
    }
}
