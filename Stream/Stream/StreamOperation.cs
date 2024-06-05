using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

namespace StreamOperations
{
    public class StreamOperation
    {
        public static readonly Aes Aes = Aes.Create();

        public static void Main()
        {
            const string outputFilePath = "/Users/andreea/Projects/JSON_Validator/Stream/Stream/output.gz";

            const string inputContent = "abcdefghijklmnopqrstuvwxyz";
            using var outputStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write, FileShare.None);
            WriteStream(inputContent, outputStream, gzip: true, crypt: true);

            using var inputStream = new FileStream(outputFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            string content = ReadStream(inputStream, gzip: true, crypt: true);
            Console.WriteLine(content);
        }

        public static string ReadStream(Stream inputStream, bool gzip = false, bool crypt = false)
        {
            Stream readStream = inputStream;

            if (gzip)
            {
                readStream = new GZipStream(readStream, CompressionMode.Decompress);
            }

            if (crypt)
            {
                readStream = new CryptoStream(readStream, Aes.CreateDecryptor(), CryptoStreamMode.Read);
            }

            using var reader = new StreamReader(readStream);
            return reader.ReadToEnd();
        }

        public static void WriteStream(string content, Stream outputStream, bool gzip = false, bool crypt = false)
        {
            Stream writeStream = outputStream;

            if (gzip)
            {
                writeStream = new GZipStream(writeStream, CompressionMode.Compress, leaveOpen: true);
            }

            if (crypt)
            {
                writeStream = new CryptoStream(writeStream, Aes.CreateEncryptor(), CryptoStreamMode.Write, leaveOpen: true);
            }

            using var writer = new StreamWriter(writeStream, Encoding.UTF8, leaveOpen: true);
            writer.Write(content);
            writer.Flush();

            if (crypt)
            {
                ((CryptoStream)writeStream).FlushFinalBlock();
            }

            if (!gzip)
            {
                return;
            }

            writeStream.Flush();
        }
    }
}
