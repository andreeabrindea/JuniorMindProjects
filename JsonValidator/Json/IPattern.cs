namespace Json;

public interface IMatch
{
    bool Success();

    StringView RemainingText();
}

public interface IPattern
{
    IMatch Match(StringView text);
}