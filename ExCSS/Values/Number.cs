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

// ReSharper disable UnusedMember.Global

namespace ExCSS.Values;

public readonly struct Number : IEquatable<Number>, IComparable<Number>, IFormattable
{
    /// <summary>
    ///     Gets a zero value.
    /// </summary>
    public static readonly Number Zero = new(0f, Unit.Integer);

    /// <summary>
    ///     Gets the positive infinite value.
    /// </summary>
    public static readonly Number Infinite = new(float.PositiveInfinity, Unit.Float);

    /// <summary>
    ///     Gets the neutral element.
    /// </summary>
    public static readonly Number One = new(1f, Unit.Integer);

    private readonly Unit _unit;

    public Number(float value, Unit unit)
    {
        Value = value;
        _unit = unit;
    }

    public float Value { get; }
    public bool IsInteger => _unit == Unit.Integer;

    public static bool operator >=(Number a, Number b)
    {
        return a.Value >= b.Value;
    }

    public static bool operator >(Number a, Number b)
    {
        return a.Value > b.Value;
    }

    public static bool operator <=(Number a, Number b)
    {
        return a.Value <= b.Value;
    }

    public static bool operator <(Number a, Number b)
    {
        return a.Value < b.Value;
    }

    public int CompareTo(Number other)
    {
        return Value.CompareTo(other.Value);
    }

    public bool Equals(Number other)
    {
        return Value == other.Value && _unit == other._unit;
    }

    public enum Unit : byte
    {
        Integer,
        Float,
        Percent
    }

    public static bool operator ==(Number a, Number b)
    {
        return a.Value == b.Value;
    }

    public static bool operator !=(Number a, Number b)
    {
        return a.Value != b.Value;
    }

    public override bool Equals(object obj)
    {
        return obj is Number other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override string ToString()
    {
        return Value.ToString() + (_unit == Unit.Percent ? "%" : string.Empty);
    }

    public string ToString(string format, IFormatProvider formatProvider)
    {
        return Value.ToString(format, formatProvider) + (_unit == Unit.Percent ? "%" : string.Empty);
    }
}