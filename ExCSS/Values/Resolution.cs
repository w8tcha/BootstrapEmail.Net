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

namespace ExCSS.Values;

public readonly struct Resolution : IEquatable<Resolution>, IComparable<Resolution>, IFormattable
{
    public Resolution(float value, Unit unit)
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
                Unit.Dpcm => UnitNames.Dpcm,
                Unit.Dpi => UnitNames.Dpi,
                Unit.Dppx => UnitNames.Dppx,
                _ => string.Empty
            };
        }
    }

    public static bool TryParse(string s, out Resolution result)
    {
        var unit = GetUnit(s.StylesheetUnit(out var value));

        if (unit != Unit.None)
        {
            result = new Resolution(value, unit);
            return true;
        }

        result = default;
        return false;
    }

    public static Unit GetUnit(string s)
    {
        return s switch
        {
            "dpcm" => Unit.Dpcm,
            "dpi" => Unit.Dpi,
            "dppx" => Unit.Dppx,
            _ => Unit.None
        };
    }

    public float ToDotsPerPixel()
    {
        if (Type == Unit.Dpi) return Value / 96f;
        if (Type == Unit.Dpcm) return Value * 127f / (50f * 96f);

        return Value;
    }

    public float To(Unit unit)
    {
        var value = ToDotsPerPixel();

        if (unit == Unit.Dpi) return value * 96f;
        if (unit == Unit.Dpcm) return value * 50f * 96f / 127f;

        return value;
    }

    public bool Equals(Resolution other)
    {
        return Value == other.Value && Type == other.Type;
    }

    public enum Unit : byte
    {
        None,
        Dpi,
        Dpcm,
        Dppx
    }

    public int CompareTo(Resolution other)
    {
        return ToDotsPerPixel().CompareTo(other.ToDotsPerPixel());
    }

    public override bool Equals(object obj)
    {
        if (obj is Resolution other) return Equals(other);

        return false;
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