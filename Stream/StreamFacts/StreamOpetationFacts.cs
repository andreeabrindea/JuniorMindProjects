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

        string content;
        using (FileStream reader = File.OpenRead("/Users/andreea/Projects/JSON_Validator/Stream/Stream/output.gz"))
        using (GZipStream zip = new GZipStream(reader, CompressionMode.Decompress, true))
        {
            content = StreamOperation.ReadStream(zip);
        }

        Assert.Equal(inputContent, content);
    }

    [Fact]
    public void ReadAndWriteWithEncryptTrue()
    {
        string inputContent = "hello there";
        using var inputStream = new MemoryStream(Encoding.UTF8.GetBytes(inputContent));
        using var outputStream = new MemoryStream();

        StreamOperation.WriteStream(inputContent, inputStream, outputStream, crypt: true);
        outputStream.Position = 0;
        string decryptedContent = StreamOperation.Decrypt(outputStream);

        Assert.Equal(inputContent, decryptedContent);
    }
}