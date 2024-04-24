using Xunit;

namespace Json.Facts;

public class ValueFacts
{
    [Fact]
    public void Test()
    {
        Value value = new();
        StringView input = new("{\n \"title\": \"example glossary\"\n, cf\": \"bn\"\n}\n");

        var match = value.Match(input);
        Assert.False(match.Success());
    }

    [Fact]
    public void Test2()
    {
        Value value = new();
        StringView input = new("{\n        \"title\": \"example glossary\"\n        \"cf\": \"bn\"\n}\n");

        var match = value.Match(input);
        Assert.False(match.Success());
        Assert.Equal(46, match.Position().StartIndex());
    }
    
    [Fact]
    public void Test3()
    {
        Value value = new();
        StringView input = new("{\"h\": {\"e\":}}");

        var match = value.Match(input);
        Assert.False(match.Success());
        Assert.Equal(10, match.Position().StartIndex());
    }
    
}