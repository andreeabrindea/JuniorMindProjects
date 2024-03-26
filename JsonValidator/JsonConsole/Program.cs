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
            
            Console.WriteLine(match.Success() && match.RemainingText().IsEmpty()
                ? "The file respects the JSON format." + match.Position()
                : "The file does not respect the JSON format at " + match.Position());

            // Value value = new();
            // String s = new();
            // StringView input = new("\"abc");
            // var match = s.Match(input);
            //
            // Console.WriteLine(match.Success() + " " + match.Position());
        }
    }
}