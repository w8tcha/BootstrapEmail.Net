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

using System.IO;

using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.MediaFeatures;

public abstract class MediaFeature : StylesheetNode, IMediaFeature
{
    private TokenValue _tokenValue;
    private TokenType _constraintDelimiter;

    internal MediaFeature(string name)
    {
        Name = name;
        IsMinimum = name.StartsWith("min-");
        IsMaximum = name.StartsWith("max-");
    }

    internal abstract IValueConverter Converter { get; }

    public bool IsMinimum { get; }

    public bool IsMaximum { get; }

    public string Name { get; }

    public string Value => HasValue ? _tokenValue.Text : string.Empty;

    public bool HasValue => _tokenValue is {Count: > 0};

    public override void ToCss(TextWriter writer, IStyleFormatter formatter)
    {
        var constraintDelimiter = GetConstraintDelimiter();
        var value = HasValue ? Value : null;
        writer.Write(formatter.Constraint(Name, value, GetConstraintDelimiter()));
    }

    private string GetConstraintDelimiter()
    {
        if (_constraintDelimiter == TokenType.Colon)
            return ": ";
        if (_constraintDelimiter == TokenType.GreaterThan)
            return " > ";
        if (_constraintDelimiter == TokenType.LessThan)
            return " < ";
        if (_constraintDelimiter == TokenType.Equal)
            return " = ";
        if (_constraintDelimiter == TokenType.GreaterThanOrEqual)
            return " >= ";
        if (_constraintDelimiter == TokenType.LessThanOrEqual)
            return " <= ";
        return ": ";
    }

    internal bool TrySetValue(TokenValue tokenValue, TokenType constraintDelimiter)
    {
        bool result;

        if (tokenValue == null)
            result = !IsMinimum && !IsMaximum && Converter.ConvertDefault() != null;
        else
            result = Converter.Convert(tokenValue) != null;

        if (result) _tokenValue = tokenValue;

        _constraintDelimiter = constraintDelimiter;

        return result;
    }
}