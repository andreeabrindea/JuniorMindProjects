namespace StreamOperations.Facts;

using System.Text;
using Xunit;

public class StreamOperationFacts
{
    [Fact]
    public void ReadAndWriteStreamWithoutGzipAndEncrypt()
    {
        string inputContent = "hello there";
        using var inputStream = new MemoryStream(Encoding.UTF8.GetBytes(inputContent));
        using var outputStream = new MemoryStream();

        StreamOperation.WriteStream(inputContent, inputStream, outputStream, gzip: false, crypt: false);

        Assert.Equal(inputContent, StreamOperation.ReadStream(inputStream));
    }
}