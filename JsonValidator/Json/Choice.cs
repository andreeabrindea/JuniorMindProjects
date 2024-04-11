namespace Json
{
    public class Choice : IPattern
    {
        private readonly string name;
        private IPattern[] patterns;

        public Choice(string name = "", params IPattern[] patterns)
        {
            this.patterns = patterns;
            this.name = name;
        }

        public IMatch Match(StringView text)
        {
            int maxMatchIndex = text.StartIndex();
            IMatch maxMatch = new SuccessMatch(text);

            foreach (var pattern in patterns)
            {
                IMatch match = pattern.Match(text);
                if (match.Success())
                {
                    return match;
                }

                if (match.Position().StartIndex() > maxMatchIndex)
                {
                    maxMatch = match;
                    maxMatchIndex = match.Position().StartIndex();
                }
            }

            return new FailedMatch(text, maxMatch.Position());
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