using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

namespace StreamOperations;

public class StreamOperation
{
    public static void Main()
    {
        using FileStream inputFile = new(
            "/Users/andreea/Projects/JSON_Validator/Stream/Stream/file.txt",
            FileMode.Open,
            FileAccess.Read,
            FileShare.Read);

        using FileStream outputFile = new(
            "/Users/andreea/Projects/JSON_Validator/Stream/Stream/output.txt",
            FileMode.OpenOrCreate,
            FileAccess.Write,
            FileShare.None);

        string content = ReadStream(inputFile);
        WriteStream(content, inputFile, outputFile, true, false);
    }

    public static string ReadStream(Stream stream)
    {
        using var reader = new StreamReader(stream, leaveOpen: true);
        return reader.ReadToEnd();
    }

    public static void WriteStream(string content, Stream input, Stream output, bool gzip = false, bool crypt = false)
    {
        input.Position = 0;
        if (crypt)
        {
            Aes aes = Aes.Create();
            Encrypt(input, output, aes.Key, aes.IV);
            aes.Dispose();
        }
        else
        {
            using StreamWriter writer = new StreamWriter(output, leaveOpen: true);
            writer.Write(content);
            writer.Flush();
        }

        if (!gzip)
        {
            return;
        }

        Gzip(content, "/Users/andreea/Projects/JSON_Validator/Stream/Stream/output.gz");
    }

    public static void Gzip(string content, string outputPath)
    {
        byte[] bytes = Encoding.ASCII.GetBytes(content);
        using (FileStream fs = new FileStream(outputPath, FileMode.OpenOrCreate))
        using (GZipStream zipStream = new GZipStream(fs, CompressionMode.Compress, false))
        {
            zipStream.Write(bytes, 0, bytes.Length);
        }
    }

    public static void Encrypt(Stream input, Stream output, byte[] key, byte[] iv)
    {
        using Aes aes = Aes.Create();
        aes.Key = key;
        aes.IV = iv;
        aes.Padding = PaddingMode.PKCS7;

        ICryptoTransform aesEncryptor = aes.CreateEncryptor();

        using CryptoStream cryptoStream = new(output, aesEncryptor, CryptoStreamMode.Write);
        input.CopyTo(cryptoStream);
        cryptoStream.FlushFinalBlock();
    }
}