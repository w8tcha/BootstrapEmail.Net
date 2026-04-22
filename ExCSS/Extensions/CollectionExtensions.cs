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
using System.Collections.Generic;
using System.Linq;

namespace ExCSS.Extensions;

internal static class CollectionExtensions
{
    public static IEnumerable<T> Concat<T>(this IEnumerable<T> items, T element)
    {
        foreach (var item in items) yield return item;

        yield return element;
    }

    public static T GetItemByIndex<T>(this IEnumerable<T> items, int index)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(index);

        var i = 0;

        foreach (var item in items.Where(item => i++ == index))
        {
            return item;
        }

        throw new ArgumentOutOfRangeException(nameof(index));
    }

    public static IEnumerable<object[]> ToObjectArray<T>(this IEnumerable<T> items)
        => items.Select(i => new object[] { i });
}