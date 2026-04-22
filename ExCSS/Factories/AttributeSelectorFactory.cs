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
using ExCSS.Selectors;

namespace ExCSS.Factories;

public sealed class AttributeSelectorFactory
{
    private static readonly Lazy<AttributeSelectorFactory> Lazy = new(() => new AttributeSelectorFactory());

    private AttributeSelectorFactory()
    {
    }

    internal static AttributeSelectorFactory Instance => Lazy.Value;

    public IAttrSelector Create(string combinator, string match, string value, string prefix)
    {
        var name = match;

        if (!string.IsNullOrEmpty(prefix))
        {
            name = AttributeSelectorFactory.FormFront(prefix, match);
            _ = AttributeSelectorFactory.FormMatch(prefix, match);
        }

        if (combinator == Combinators.Exactly)
            return new AttrMatchSelector(name, value);
        if (combinator == Combinators.InList)
            return new AttrListSelector(name, value);
        if (combinator == Combinators.InToken)
            return new AttrHyphenSelector(name, value);
        if (combinator == Combinators.Begins)
            return new AttrBeginsSelector(name, value);
        if (combinator == Combinators.Ends)
            return new AttrEndsSelector(name, value);
        if (combinator == Combinators.InText)
            return new AttrContainsSelector(name, value);
        if (combinator == Combinators.Unlike)
            return new AttrNotMatchSelector(name, value);
        return new AttrAvailableSelector(name, value);
    }

    private static string FormFront(string prefix, string match)
    {
        return string.Concat(prefix, Combinators.Pipe, match);
    }

    private static string FormMatch(string prefix, string match)
    {
        return prefix.Is(Keywords.Asterisk) ? match : string.Concat(prefix, PseudoClassNames.Separator, match);
    }
}