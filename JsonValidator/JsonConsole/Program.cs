namespace Json.JsonConsole
{
    class Program
    {
        public static void Main(string[] args)
        {
            string lines;
            try
            {
                lines = File.ReadAllText(args[0]);
                Console.WriteLine(lines);
                Value jsonValidator = new Value();
                
                Console.WriteLine(jsonValidator.Match(lines).Success()
                    ? "The file respects the JSON format."
                    : "The file does not respect the JSON format.");
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
    }
}
