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

public readonly struct Angle : IEquatable<Angle>, IComparable<Angle>, IFormattable
{
    public static readonly Angle Zero = new(0f, Unit.Rad);
    public static readonly Angle HalfQuarter = new(45f, Unit.Deg);
    public static readonly Angle Quarter = new(90f, Unit.Deg);
    public static readonly Angle TripleHalfQuarter = new(135f, Unit.Deg);
    public static readonly Angle Half = new(180f, Unit.Deg);

    public Angle(float value, Unit unit)
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
                Unit.Deg => UnitNames.Deg,
                Unit.Grad => UnitNames.Grad,
                Unit.Turn => UnitNames.Turn,
                Unit.Rad => UnitNames.Rad,
                _ => string.Empty
            };
        }
    }

    /// <summary>
    ///     Compares the magnitude of two angles.
    /// </summary>
    public static bool operator >=(Angle a, Angle b)
    {
        var result = a.CompareTo(b);
        return result is 0 or 1;
    }

    /// <summary>
    ///     Compares the magnitude of two angles.
    /// </summary>
    public static bool operator >(Angle a, Angle b)
    {
        return a.CompareTo(b) == 1;
    }

    /// <summary>
    ///     Compares the magnitude of two angles.
    /// </summary>
    public static bool operator <=(Angle a, Angle b)
    {
        var result = a.CompareTo(b);
        return result is 0 or -1;
    }

    /// <summary>
    ///     Compares the magnitude of two angles.
    /// </summary>
    public static bool operator <(Angle a, Angle b)
    {
        return a.CompareTo(b) == -1;
    }

    /// <summary>
    ///     Compares the current angle against the given one.
    /// </summary>
    /// <param name="other">The angle to compare to.</param>
    /// <returns>The result of the comparison.</returns>
    public int CompareTo(Angle other)
    {
        return ToRadian().CompareTo(other.ToRadian());
    }

    public static bool TryParse(string s, out Angle result)
    {
        var unit = GetUnit(s.StylesheetUnit(out var value));

        if (unit != Unit.None)
        {
            result = new Angle(value, unit);
            return true;
        }

        result = default;
        return false;
    }

    public static Unit GetUnit(string s)
    {
        return s switch
        {
            "deg" => Unit.Deg,
            "grad" => Unit.Grad,
            "turn" => Unit.Turn,
            "rad" => Unit.Rad,
            _ => Unit.None
        };
    }

    public float ToRadian()
    {
        return Type switch
        {
            Unit.Deg => (float) (Math.PI / 180.0 * Value),
            Unit.Grad => (float) (Math.PI / 200.0 * Value),
            Unit.Turn => (float) (2.0 * Math.PI * Value),
            _ => Value
        };
    }

    public float ToTurns()
    {
        return Type switch
        {
            Unit.Deg => (float) (Value / 360.0),
            Unit.Grad => (float) (Value / 400.0),
            Unit.Rad => (float) (Value / (2.0 * Math.PI)),
            _ => Value
        };
    }

    public bool Equals(Angle other)
    {
        return ToRadian() == other.ToRadian();
    }

    /// <summary>
    ///     An enumeration of angle representations.
    /// </summary>
    public enum Unit : byte
    {
        None,
        Deg,
        Rad,
        Grad,
        Turn
    }

    /// <summary>
    ///     Checks for equality of two angles.
    /// </summary>
    public static bool operator ==(Angle a, Angle b)
    {
        return a.Equals(b);
    }

    /// <summary>
    ///     Checks for inequality of two angles.
    /// </summary>
    public static bool operator !=(Angle a, Angle b)
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
        var other = obj as Angle?;

        return other != null && Equals(other.Value);
    }

    /// <summary>
    ///     Returns a hash code that defines the current angle.
    /// </summary>
    /// <returns>The integer value of the hashcode.</returns>
    public override int GetHashCode()
    {
        return (int) Value;
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