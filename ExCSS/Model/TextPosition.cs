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

using System;

namespace ExCSS.Model;

public readonly struct TextPosition : IEquatable<TextPosition>, IComparable<TextPosition>
{
    public static readonly TextPosition Empty = new();

    private readonly ushort _line;
    private readonly ushort _column;

    public TextPosition(ushort line, ushort column, int position)
    {
        _line = line;
        _column = column;
        Position = position;
    }

    public int Line => _line;
    public int Column => _column;
    public int Position { get; }

    public TextPosition Shift(int columns)
    {
        return new(_line, (ushort) (_column + columns), Position + columns);
    }

    public TextPosition After(char chr)
    {
        var line = _line;
        var column = _column;

        if (chr != Symbols.LineFeed) return new TextPosition(line, ++column, Position + 1);

        ++line;
        column = 0;

        return new TextPosition(line, ++column, Position + 1);
    }

    public TextPosition After(string str)
    {
        var line = _line;
        var column = _column;

        foreach (var chr in str)
        {
            if (chr == Symbols.LineFeed)
            {
                ++line;
                column = 0;
            }

            ++column;
        }

        return new TextPosition(line, column, Position + str.Length);
    }

    public override string ToString()
    {
        return $"Line {_line}, Column {_column}, Position {Position}";
    }

    public override int GetHashCode()
    {
        return Position ^ ((_line | _column) + _line);
    }

    public override bool Equals(object obj)
    {
        return obj is TextPosition other && Equals(other);
    }

    public bool Equals(TextPosition other)
    {
        return Position == other.Position &&
               _column == other._column &&
               _line == other._line;
    }

    public static bool operator >(TextPosition a, TextPosition b)
    {
        return a.Position > b.Position;
    }

    public static bool operator <(TextPosition a, TextPosition b)
    {
        return a.Position < b.Position;
    }

    public int CompareTo(TextPosition other)
    {
        return Equals(other) ? 0 : this > other ? 1 : -1;
    }
}