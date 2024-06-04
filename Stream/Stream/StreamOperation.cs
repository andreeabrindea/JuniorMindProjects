using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

namespace StreamOperations;

public class StreamOperation
{
    public static readonly Aes Aes = Aes.Create();

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
        WriteStream(content, inputFile, outputFile, true, true);
    }

    public static string ReadStream(Stream stream)
    {
        using var reader = new StreamReader(stream, leaveOpen: true);
        return reader.ReadToEnd();
    }

    public static void WriteStream(string content, Stream input, Stream output, bool gzip = false, bool crypt = false)
    {
        if (crypt)
        {
            Encrypt(input, output);
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

    public static string Decrypt(Stream encryptedStream)
    {
        ICryptoTransform aesDecryptor = Aes.CreateDecryptor();

        using var decryptedStream = new MemoryStream();
        using CryptoStream cryptoStream = new(encryptedStream, aesDecryptor, CryptoStreamMode.Read);
        cryptoStream.CopyTo(decryptedStream);

        return ReadStream(decryptedStream);
    }

    private static void Encrypt(Stream input, Stream output)
    {
        ICryptoTransform aesEncryptor = Aes.CreateEncryptor();

        using CryptoStream cryptoStream = new(output, aesEncryptor, CryptoStreamMode.Write, leaveOpen: true);
        input.CopyTo(cryptoStream);
        cryptoStream.FlushFinalBlock();
    }

    private static void Gzip(string content, string outputPath)
    {
        byte[] bytes = Encoding.ASCII.GetBytes(content);
        using (FileStream fs = new FileStream(outputPath, FileMode.OpenOrCreate))
        using (GZipStream zipStream = new GZipStream(fs, CompressionMode.Compress, false))
        {
            zipStream.Write(bytes, 0, bytes.Length);
        }
    }
}