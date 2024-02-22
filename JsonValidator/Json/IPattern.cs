namespace Json;

public interface IMatch
{
    bool Success();

    string RemainingText();
}

public interface IPattern
{
    IMatch Match(string text);
}