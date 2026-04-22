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
using System.Text;
using System.Threading;

using ExCSS.Factories;
using ExCSS.Parser;

namespace ExCSS.Model;

internal static class Pool
{
    private static readonly Stack<StringBuilder> Builder = new();
    private static readonly Stack<SelectorConstructor> Selector = new();
    private static readonly Stack<ValueBuilder> Value = new();
    private static readonly Lock Lock = new();

    public static StringBuilder NewStringBuilder()
    {
        lock (Lock)
        {
            return Builder.Count == 0 ? new StringBuilder(1024) : Builder.Pop().Clear();
        }
    }

    public static SelectorConstructor NewSelectorConstructor(AttributeSelectorFactory attributeSelector,
        PseudoClassSelectorFactory pseudoClassSelector, PseudoElementSelectorFactory pseudoElementSelector)
    {
        lock (Lock)
        {
            return Selector.Count == 0 
                ? new SelectorConstructor(attributeSelector, pseudoClassSelector, pseudoElementSelector) 
                : Selector.Pop().Reset(attributeSelector, pseudoClassSelector, pseudoElementSelector);
        }
    }

    public static ValueBuilder NewValueBuilder()
    {
        lock (Lock)
        {
            return Value.Count == 0 
                ? new ValueBuilder()
                : Value.Pop().Reset();
        }
    }

    public static string ToPool(this StringBuilder sb)
    {
        var result = sb.ToString();

        lock (Lock)
        {
            Builder.Push(sb);
        }

        return result;
    }

    public static ISelector ToPool(this SelectorConstructor ctor)
    {
        var result = ctor.GetResult();

        lock (Lock)
        {
            Selector.Push(ctor);
        }

        return result;
    }

    public static TokenValue ToPool(this ValueBuilder vb)
    {
        var result = vb.GetResult();

        lock (Lock)
        {
            Value.Push(vb);
        }

        return result;
    }
}