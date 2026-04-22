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

using System.Collections.Generic;
using System.Globalization;

using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.Tokens;

internal sealed class RangeToken : Token
{
    private string[] GetRange()
    {
        var index = int.Parse(Start, NumberStyles.HexNumber);

        if (index > Symbols.MaximumCodepoint) return null;

        if (End == null) return [index.ConvertFromUtf32()];

        var list = new List<string>();
        var f = int.Parse(End, NumberStyles.HexNumber);

        if (f > Symbols.MaximumCodepoint) f = Symbols.MaximumCodepoint;

        while (index <= f)
        {
            list.Add(index.ConvertFromUtf32());
            index++;
        }

        return [.. list];
    }

    public RangeToken(string range, TextPosition position)
        : base(TokenType.Range, range, position)
    {
        Start = range.Replace(Symbols.QuestionMark, '0');
        End = range.Replace(Symbols.QuestionMark, 'F');
        SelectedRange = GetRange();
    }

    public RangeToken(string start, string end, TextPosition position)
        : base(TokenType.Range, string.Concat(start, "-", end), position)
    {
        Start = start;
        End = end;
        SelectedRange = GetRange();
    }

    //public bool IsEmpty => (SelectedRange == null) || (SelectedRange.Length == 0);
    public string Start { get; }
    public string End { get; }
    public string[] SelectedRange { get; }
}