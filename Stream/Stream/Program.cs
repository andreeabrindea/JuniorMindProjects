class Program
{

    public static void Main()
    {
        FileStream inputFile = new ("/Users/andreea/Projects/JSON_Validator/Stream/Stream/file.txt", 
            FileMode.Open, FileAccess.Read, FileShare.Read);
        
        FileStream outputFile = new("/Users/andreea/Projects/JSON_Validator/Stream/Stream/output.txt", 
            FileMode.Open, FileAccess.Write, FileShare.None);
        
        Console.WriteLine(ReadStream(inputFile));
        inputFile.Position = 0;
        WriteStream(inputFile, outputFile);
    }
    public static string ReadStream(Stream stream)
    {
        return new StreamReader(stream).ReadToEnd();
    }

    public static void WriteStream(Stream stream, Stream output, bool gzip = false, bool crypt = false)
    {
        string content = ReadStream(stream);
        StreamWriter writer = new StreamWriter(output);
        writer.Write(content);
        writer.Flush();
    }
}