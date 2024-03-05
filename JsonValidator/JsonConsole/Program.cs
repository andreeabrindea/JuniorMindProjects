using static Json.JsonNumber;

namespace Json.JsonConsole
{
    class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("No file provided.");
                return;
            }
            
            if (!File.Exists(args[0]))
            {
                Console.WriteLine("No file with the given path was found. Ensure that you enter the path to the file correctly.");
                return;
            }

            string content;
            content = File.ReadAllText(args[0]);

            var jsonValidator = IsJsonNumber(content);
            if (jsonValidator)
            {
                Console.WriteLine("The file respects the JSON format.");
            }
            else
            {
                Console.WriteLine("The file does not respect the JSON format. Ensure that the content of the file is correct \n ");
            }
        }
    }
}