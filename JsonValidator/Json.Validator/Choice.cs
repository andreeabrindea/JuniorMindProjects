namespace Json
{
    public class Choice : IPattern
    {
        private readonly IPattern[] patterns;

        public Choice(params IPattern[] patterns)
        {
            this.patterns = patterns;
        }

        public IMatch Match(string text)
        {
            foreach (var pattern in patterns)
            {
                IMatch match = pattern.Match(text);

                if (match.Success())
                {
                    return match;
                }
            }

            return new FailedMatch(text);
        }
}
}