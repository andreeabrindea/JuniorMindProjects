using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

class Program
{

    public static void Main()
    {
        FileStream inputFile = new ("/Users/andreea/Projects/JSON_Validator/Stream/Stream/file.txt", 
            FileMode.Open, FileAccess.Read, FileShare.Read);
        
        FileStream outputFile = new("/Users/andreea/Projects/JSON_Validator/Stream/Stream/output.txt", 
            FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
        
        Console.WriteLine(ReadStream(inputFile));
        WriteStream(inputFile, outputFile, true, true);
    }
    public static string ReadStream(Stream stream)
    {
        return new StreamReader(stream).ReadToEnd();
    }

    public static void WriteStream(Stream input, Stream output, bool gzip = false, bool crypt = false)
    {
        input.Position = 0;
        if (crypt)
        {
            using (Aes myAes = Aes.Create())
            {
                Encrypt(input, output, myAes.Key, myAes.IV);
            }
        }
        else
        {
            string content = ReadStream(input);
            StreamWriter writer = new StreamWriter(output);
            writer.Write(content);
            writer.Flush();
        }
        
        
        if (gzip)
        {
            string content = ReadStream(input);
            Gzip(content, "/Users/andreea/Projects/JSON_Validator/Stream/Stream/output.gz");
        }
        
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
    
    public static void Encrypt(Stream input, Stream output, byte[] Key, byte[] IV)
    {
        Aes aes = Aes.Create();
        aes.Key = Key;
        aes.IV = IV;
        aes.Padding = PaddingMode.PKCS7;
       
        ICryptoTransform aesEncryptor = aes.CreateEncryptor();

        using (CryptoStream cryptoStream = new(output, aesEncryptor, CryptoStreamMode.Write))
        {
            input.CopyTo(cryptoStream);
            cryptoStream.FlushFinalBlock();
        }
    }
    
}