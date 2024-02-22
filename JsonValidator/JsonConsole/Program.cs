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
            Value jsonValidator = new Value();
            
            if (jsonValidator.Match(content).Success() && jsonValidator.Match(content).RemainingText() == string.Empty)
            {
                Console.WriteLine("The file respects the JSON format.");
            }
            else
            {
                Console.WriteLine("The file does not respect the JSON format. Ensure that the content of the file is correct \n " + jsonValidator.Match(content).RemainingText());
            }
        }
    }
}
