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
using System.Runtime.InteropServices;

namespace ExCSS.Model;

[StructLayout(LayoutKind.Explicit, Pack = 1, CharSet = CharSet.Unicode)]
public readonly struct Priority : IEquatable<Priority>, IComparable<Priority>
{
    [FieldOffset(0)] private readonly uint _priority;

    public static readonly Priority Zero = new (0u);
    public static readonly Priority OneTag = new (0, 0, 0, 1);
    public static readonly Priority OneClass = new (0, 0, 1, 0);
    public static readonly Priority OneId = new (0, 1, 0, 0);
    public static readonly Priority Inline = new (1, 0, 0, 0);

    public Priority(uint priority)
    {
        Inlines = Ids = Classes = Tags = 0;
        _priority = priority;
    }

    public Priority(byte inlines, byte ids, byte classes, byte tags)
    {
        _priority = 0;
        Inlines = inlines;
        Ids = ids;
        Classes = classes;
        Tags = tags;
    }

    [field: FieldOffset(2)]
    public byte Ids { get; }

    [field: FieldOffset(0)]
    public byte Tags { get; }

    [field: FieldOffset(1)]
    public byte Classes { get; }

    [field: FieldOffset(3)]
    public byte Inlines { get; }

    public static Priority operator +(Priority a, Priority b)
    {
        return new(a._priority + b._priority);
    }

    public static bool operator ==(Priority a, Priority b)
    {
        return a._priority == b._priority;
    }
    public static bool operator >(Priority a, Priority b)
    {
        return a._priority > b._priority;
    }

    public static bool operator >=(Priority a, Priority b)
    {
        return a._priority >= b._priority;
    }

    public static bool operator <(Priority a, Priority b)
    {
        return a._priority < b._priority;
    }

    public static bool operator <=(Priority a, Priority b)
    {
        return a._priority <= b._priority;
    }

    public static bool operator !=(Priority a, Priority b)
    {
        return a._priority != b._priority;
    }

    public bool Equals(Priority other)
    {
        return _priority == other._priority;
    }

    public override bool Equals(object obj)
    {
        return obj is Priority priority && Equals(priority);
    }

    public override int GetHashCode()
    {
        return (int) _priority;
    }

    public int CompareTo(Priority other)
    {
        return this == other ? 0 : (this > other ? 1 : -1);
    }

    public override string ToString()
    {
        return $"({Inlines}, {Ids}, {Classes}, {Tags})";
    }
}