namespace Json
{
    public class Choice : IPattern
    {
        private IPattern[] patterns;

        public Choice(params IPattern[] patterns)
        {
            this.patterns = patterns;
        }

        public IMatch Match(StringView text)
        {
            Console.WriteLine("Choice start " + text.StartIndex() + " " + text.Peek());
            IMatch match = new SuccessMatch(text);
            foreach (var pattern in patterns)
            {
                match = pattern.Match(text);
                Console.WriteLine("Choice foreach " + match.Position().StartIndex() + " " + match.Position().Peek() + match.Success());
                if (match.Success())
                {
                    return match;
                }
            }

            Console.WriteLine("Choice Final " + text.StartIndex() + " " + text.Peek());

            return new FailedMatch(text, match.Position());
        }

        public void Add(IPattern pattern)
        {
            if (pattern == null)
            {
                return;
            }

            Array.Resize(ref patterns, patterns.Length + 1);
            patterns[^1] = pattern;
        }
    }
}