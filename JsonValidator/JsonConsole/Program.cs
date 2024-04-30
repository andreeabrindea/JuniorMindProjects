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

            string content = File.ReadAllText(args[0]);
            Value jsonValidator = new();
            StringView input = new(content);
            var match = jsonValidator.Match(input);
            var line = match.Position().ToColumnRow()[0];
            var column = match.Position().ToColumnRow()[1];
            Console.WriteLine(match.Success() && match.RemainingText().IsEmpty()
                ? "The file respects the JSON format "
                : "The file does not respect the JSON format at line " + line + " and column " + column);
        }
    }
}