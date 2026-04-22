// The MIT License (MIT)
//
// Copyright (c) 2024 Tyler Brinks
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Tokens;

namespace ExCSS.Model;

internal sealed class TokenValue : StylesheetNode, IEnumerable<Token>
{
    private readonly List<Token> _tokens;
    public static TokenValue Initial = FromString(Keywords.Initial);
    public static TokenValue Empty = new([]);

    public override void ToCss(TextWriter writer, IStyleFormatter formatter)
    {
        writer.Write(_tokens.ToText());
    }

    private TokenValue(Token token)
    {
        _tokens = [token];
    }

    public TokenValue(IEnumerable<Token> tokens)
    {
        _tokens = new List<Token>(tokens);
    }

    public static TokenValue FromString(string text)
    {
        var token = new Token(TokenType.Ident, text, TextPosition.Empty);
        return new TokenValue(token);
    }

    public static TokenValue FromNumber(int number)
    {
        var token = new NumberToken(number.ToString(CultureInfo.InvariantCulture), TextPosition.Empty);
        return new TokenValue(token);
    }

    public Token this[int index] => _tokens[index];

    public int Count => _tokens.Count;

    public string Text => this.ToCss();

    public IEnumerator<Token> GetEnumerator()
    {
        return _tokens.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}