namespace StreamOperations.Facts;

using System.IO.Compression;
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

        StreamOperation.WriteStream(inputContent, inputStream, outputStream);

        Assert.Equal(inputContent, StreamOperation.ReadStream(inputStream));
    }

    [Fact]
    public void ReadAndWriteStreamWithGzip()
    {
        string inputContent = "hello there";
        using var inputStream = new MemoryStream(Encoding.UTF8.GetBytes(inputContent));
        using var outputStream = new MemoryStream();

        StreamOperation.WriteStream(inputContent, inputStream, outputStream, gzip: true);

        Assert.Equal(inputContent, StreamOperation.ReadStream(inputStream, gzip: true));
    }

    [Fact]
    public void ReadAndWriteWithEncryptTrue()
    {
        string inputContent = "hello there";
        using var inputStream = new MemoryStream(Encoding.UTF8.GetBytes(inputContent));
        using var outputStream = new MemoryStream();

        StreamOperation.WriteStream(inputContent, inputStream, outputStream, crypt: true);

        Assert.Equal(inputContent, StreamOperation.ReadStream(outputStream, crypt: true));
    }
}