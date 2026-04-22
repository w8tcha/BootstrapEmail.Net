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

using ExCSS.Enumerations;
using ExCSS.Extensions;

// ReSharper disable UnusedMember.Global

namespace ExCSS.Values;

public readonly struct Frequency : IEquatable<Frequency>, IComparable<Frequency>, IFormattable
{
    public Frequency(float value, Unit unit)
    {
        Value = value;
        Type = unit;
    }

    public float Value { get; }
    public Unit Type { get; }

    public string UnitString
    {
        get
        {
            return Type switch
            {
                Unit.Khz => UnitNames.Khz,
                Unit.Hz => UnitNames.Hz,
                _ => string.Empty
            };
        }
    }

    public static bool operator >=(Frequency a, Frequency b)
    {
        var result = a.CompareTo(b);
        return result == 0 || result == 1;
    }

    public static bool operator >(Frequency a, Frequency b)
    {
        return a.CompareTo(b) == 1;
    }

    public static bool operator <=(Frequency a, Frequency b)
    {
        var result = a.CompareTo(b);
        return result == 0 || result == -1;
    }

    public static bool operator <(Frequency a, Frequency b)
    {
        return a.CompareTo(b) == -1;
    }

    public int CompareTo(Frequency other)
    {
        return ToHertz().CompareTo(other.ToHertz());
    }

    public static bool TryParse(string s, out Frequency result)
    {
        var unit = GetUnit(s.StylesheetUnit(out var value));

        if (unit != Unit.None)
        {
            result = new Frequency(value, unit);
            return true;
        }

        result = default;
        return false;
    }

    public static Unit GetUnit(string s)
    {
        return s switch
        {
            "hz" => Unit.Hz,
            "khz" => Unit.Khz,
            _ => Unit.None
        };
    }

    public float ToHertz()
    {
        return Type == Unit.Khz ? Value * 1000f : Value;
    }

    public bool Equals(Frequency other)
    {
        return Value == other.Value && Type == other.Type;
    }

    public enum Unit : byte
    {
        None,
        Hz,
        Khz
    }

    public static bool operator ==(Frequency a, Frequency b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(Frequency a, Frequency b)
    {
        return !a.Equals(b);
    }

    public override bool Equals(object obj)
    {
        var other = obj as Frequency?;

        return other != null && Equals(other.Value);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override string ToString()
    {
        return string.Concat(Value.ToString(), UnitString);
    }

    public string ToString(string format, IFormatProvider formatProvider)
    {
        return string.Concat(Value.ToString(format, formatProvider), UnitString);
    }
}