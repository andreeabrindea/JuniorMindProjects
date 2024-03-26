namespace Json;

public interface IMatch
{
    bool Success();

    StringView RemainingText();

    int Position();
}

public interface IPattern
{
    IMatch Match(StringView text);
}