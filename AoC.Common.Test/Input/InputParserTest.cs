using AoC.Common.Input;
using Xunit;

namespace AoC.Common.Test.Input;

/// <summary>
/// Unit tests for <see cref="InputParser{T}"/>
/// </summary>
public class InputParserTest
{
    private const string Filename = "test.txt";
    private readonly string[] _lines = ["abc", "def"];

    [Fact]
    public async Task GetAllNonEmptyLines_ReturnsCorrectValue()
    {
        // arrange
        await File.WriteAllLinesAsync(Filename, new[]
        {
            string.Empty,
            _lines[0],
            string.Empty,
            string.Empty,
            _lines[1]
        });
        using var sut = new DerivedParser(Filename);

        // act
        var result = await sut.InvokeGetAllNonEmptyLines();

        // assert
        Assert.Equal(2, result.Length);
        Assert.Equal(_lines[0], result[0]);
        Assert.Equal(_lines[1], result[1]);
    }

    [Fact]
    public async Task GetNumEmptyLines_ReturnsCorrectValues()
    {
        // arrange
        await File.WriteAllLinesAsync(Filename, new[]
        {
            string.Empty,
            _lines[0],
            string.Empty,
            string.Empty,
            _lines[1]
        });
        using var sut = new DerivedParser(Filename);

        // act
        var result = sut.InvokeGetNonEmptyLines();

        // assert
        var list = await result.ToListAsync();
        Assert.Equal(2, list.Count);
        Assert.Equal(_lines[0], list[0]);
        Assert.Equal(_lines[1], list[1]);
    }

    [Fact]
    public async Task ResetReader_ResetsReader()
    {
        // arrange
        await File.WriteAllLinesAsync(Filename, new[]
        {
            string.Empty,
            _lines[0],
            string.Empty,
            string.Empty,
            _lines[1]
        });
        using var sut = new DerivedParser(Filename);
        await sut.InvokeGetNonEmptyLines().ToListAsync();   // read till end

        // act
        sut.InvokeResetReader();

        // assert
        var content = await sut.InvokeGetNonEmptyLines().ToListAsync();
        Assert.Equal(2, content.Count);
        Assert.Equal(_lines[0], content[0]);
        Assert.Equal(_lines[1], content[1]);
    }

    private class DerivedParser(string filename) : InputParser<string>(filename)
    {
        public override Task<string> Parse(CancellationToken token = default)
        {
            return Task.FromResult(string.Empty);
        }

        public ValueTask<string[]> InvokeGetAllNonEmptyLines(CancellationToken token = default)
        {
            return GetAllNonEmptyLines(token);
        }

        public IAsyncEnumerable<string> InvokeGetNonEmptyLines(CancellationToken token = default)
        {
            return GetNonEmptyLines(token);
        }

        public void InvokeResetReader()
        {
            ResetReader();
        }
    }
}
