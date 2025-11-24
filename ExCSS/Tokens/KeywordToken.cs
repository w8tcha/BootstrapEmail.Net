namespace ExCSS;

internal sealed class KeywordToken : Token
{
    public KeywordToken(TokenType type, string data, TextPosition position)
        : base(type, data, position)
    {
    }

    public override string ToValue()
    {
        return Type switch
        {
            TokenType.Hash => "#" + Data,
            TokenType.AtKeyword => "@" + Data,
            TokenType.Function => Data + "(",
            _ => Data
        };
    }
}