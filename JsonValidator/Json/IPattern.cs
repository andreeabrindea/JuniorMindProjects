namespace Json;

public interface IMatch
{
    bool Success();

    StringView RemainingText();

    StringView Position();
}

public interface IPattern
{
    IMatch Match(StringView text);
}