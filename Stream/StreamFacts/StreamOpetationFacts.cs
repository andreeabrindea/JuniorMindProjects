namespace StreamOperations.Facts;

using System.Text;
using Xunit;

public class StreamOperationFacts
{
    [Fact]
    public void ReadAndWriteStreamWithoutGzipAndEncrypt()
    {
        string inputContent = "hello there";
        using var inputStream = new MemoryStream();

        StreamOperation.WriteStream(inputContent, inputStream);
        inputStream.Seek(0, SeekOrigin.Begin);
        Assert.Equal(inputContent, StreamOperation.ReadStream(inputStream));
    }

    [Fact]
    public void ReadAndWriteStreamWithGzip()
    {
        string inputContent = "hello there";
        using var inputStream = new MemoryStream();

        StreamOperation.WriteStream(inputContent, inputStream, gzip: true);
        inputStream.Seek(0, SeekOrigin.Begin);
        Assert.Equal(inputContent, StreamOperation.ReadStream(inputStream, gzip: true));
    }

    [Fact]
    public void ReadAndWriteWithEncryptTrue()
    {
        string inputContent = "hello there";
        using var inputStream = new MemoryStream();

        StreamOperation.WriteStream(inputContent, inputStream, crypt: true);
        inputStream.Seek(0, SeekOrigin.Begin);
        Assert.Equal(inputContent, StreamOperation.ReadStream(inputStream, crypt: true));
    }

    [Fact]
    public void ReadAndWriteWithEncryptAndGZipTrue()
    {
        string inputContent = "hello there";
        using var inputStream = new MemoryStream();

        StreamOperation.WriteStream(inputContent, inputStream, gzip: true, crypt: true);
        inputStream.Seek(0, SeekOrigin.Begin);
        Assert.Equal(inputContent, StreamOperation.ReadStream(inputStream, gzip: true, crypt: true));
    }
}