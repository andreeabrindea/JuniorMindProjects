class Program
{

    public static void Main()
    {
        
    }
    public static string ReadStream(Stream stream)
    {
        return new StreamReader(stream).ReadToEnd();
    }

    public static void WriteStream(Stream stream, bool gzip = false, bool crypt = false)
    {
        string content = ReadStream(stream);
        StreamWriter writer = new StreamWriter(content);
        writer.Write(content);
        writer.Flush();
    }
    
}