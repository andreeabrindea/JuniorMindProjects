namespace GitClientApp;

public class ModifiedFile
{
    public ModifiedFile(string statusCode, string directory, string fileName)
    {
        Directory = directory;
        StatusCode = statusCode;
        FileName = fileName;
    }

    public string Directory { get; set; }

    public string StatusCode { get; set; }

    public string FileName { get; set; }

    public string PreviousName { get; set; }
}