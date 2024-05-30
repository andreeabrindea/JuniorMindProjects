namespace StreamOperations.Facts;

using System.Text;
using Xunit;

public class StreamOpetationFacts
{
    [Fact]
    public void ReadAndWriteStreamWithoutGzipAndEncrypt()
    {
        string inputContent = "Hello, World!";
        using var inputStream = new MemoryStream(Encoding.UTF8.GetBytes(inputContent));
        using var outputStream = new MemoryStream();

        StreamOperation.WriteStream(inputStream, outputStream, gzip: false, crypt: false);

        using var reader = new StreamReader(outputStream, leaveOpen: true);

        Assert.Equal(inputContent, reader.ReadToEnd());
    }
}