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

namespace ExCSS.Values;

public struct Point
{
    /// <summary>
    ///     Gets the (50%, 50%) point.
    /// </summary>
    public static readonly Point Center = new(Length.Half, Length.Half);

    /// <summary>
    ///     Gets the (0, 0) point.
    /// </summary>
    public static readonly Point LeftTop = new(Length.Zero, Length.Zero);

    /// <summary>
    ///     Gets the (100%, 0) point.
    /// </summary>
    public static readonly Point RightTop = new(Length.Full, Length.Zero);

    /// <summary>
    ///     Gets the (100%, 100%) point.
    /// </summary>
    public static readonly Point RightBottom = new(Length.Full, Length.Full);

    /// <summary>
    ///     Gets the (0, 100%) point.
    /// </summary>
    public static readonly Point LeftBottom = new(Length.Zero, Length.Full);

    public Point(Length x, Length y)
    {
        X = x;
        Y = y;
    }

    public Length X { get; }
    public Length Y { get; }
}