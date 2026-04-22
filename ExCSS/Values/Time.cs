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

namespace ExCSS.Values;

public readonly struct Time : IEquatable<Time>, IComparable<Time>, IFormattable
{
    public static readonly Time Zero = new(0f, Unit.Ms);

    public Time(float value, Unit unit)
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
                Unit.Ms => UnitNames.Ms,
                Unit.S => UnitNames.S,
                _ => string.Empty
            };
        }
    }

    /// <summary>
    ///     Compares the magnitude of two times.
    /// </summary>
    public static bool operator >=(Time a, Time b)
    {
        var result = a.CompareTo(b);
        return result == 0 || result == 1;
    }

    /// <summary>
    ///     Compares the magnitude of two times.
    /// </summary>
    public static bool operator >(Time a, Time b)
    {
        return a.CompareTo(b) == 1;
    }

    /// <summary>
    ///     Compares the magnitude of two times.
    /// </summary>
    public static bool operator <=(Time a, Time b)
    {
        var result = a.CompareTo(b);
        return result == 0 || result == -1;
    }

    /// <summary>
    ///     Compares the magnitude of two times.
    /// </summary>
    public static bool operator <(Time a, Time b)
    {
        return a.CompareTo(b) == -1;
    }

    public int CompareTo(Time other)
    {
        return ToMilliseconds().CompareTo(other.ToMilliseconds());
    }

       
    public static Unit GetUnit(string s)
    {
        return s switch
        {
            "s" => Unit.S,
            "ms" => Unit.Ms,
            _ => Unit.None
        };
    }

    public float ToMilliseconds()
    {
        return Type == Unit.S ? Value * 1000f : Value;
    }

    public bool Equals(Time other)
    {
        return ToMilliseconds() == other.ToMilliseconds();
    }

    public enum Unit : byte
    {
        None,
        Ms,
        S
    }

    /// <summary>
    ///     Checks for equality of two times.
    /// </summary>
    public static bool operator ==(Time a, Time b)
    {
        return a.Equals(b);
    }

    /// <summary>
    ///     Checks for inequality of two times.
    /// </summary>
    public static bool operator !=(Time a, Time b)
    {
        return !a.Equals(b);
    }

    /// <summary>
    ///     Tests if another object is equal to this object.
    /// </summary>
    /// <param name="obj">The object to test with.</param>
    /// <returns>True if the two objects are equal, otherwise false.</returns>
    public override bool Equals(object obj)
    {
        if (obj is Time other) return Equals(other);

        return false;
    }

    /// <summary>
    ///     Returns a hash code that defines the current time.
    /// </summary>
    /// <returns>The integer value of the hashcode.</returns>
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    /// <summary>
    ///     Returns a string representing the time.
    /// </summary>
    /// <returns>The unit string.</returns>
    public override string ToString()
    {
        return string.Concat(Value.ToString(), UnitString);
    }

    /// <summary>
    ///     Returns a formatted string representing the time.
    /// </summary>
    /// <param name="format">The format of the number.</param>
    /// <param name="formatProvider">The provider to use.</param>
    /// <returns>The unit string.</returns>
    public string ToString(string format, IFormatProvider formatProvider)
    {
        return string.Concat(Value.ToString(format, formatProvider), UnitString);
    }
}