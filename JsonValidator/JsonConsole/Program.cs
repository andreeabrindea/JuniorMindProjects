namespace Json.JsonConsole
{
    class Program
    {
        public static void Main(string[] args)
        {
            string content;
            try
            {
                content = File.ReadAllText(args[0]);

                Value jsonValidator = new Value();
                Console.WriteLine(jsonValidator.Match(content).Success()
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
