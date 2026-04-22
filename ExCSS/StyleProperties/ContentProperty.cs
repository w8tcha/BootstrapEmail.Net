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

using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties;

using static Converters;

internal sealed class ContentProperty : Property
{
    internal ContentProperty() : base(PropertyNames.Content)
    {
    }

    internal override IValueConverter Converter => StyleConverter;

    private static readonly Dictionary<string, ContentMode> ContentModes =
        new(StringComparer.OrdinalIgnoreCase)
        {
            {Keywords.OpenQuote, new OpenQuoteContentMode()},
            {Keywords.NoOpenQuote, new NoOpenQuoteContentMode()},
            {Keywords.CloseQuote, new CloseQuoteContentMode()},
            {Keywords.NoCloseQuote, new NoCloseQuoteContentMode()}
        };

    private static readonly ContentMode[] Default = [new NormalContentMode()];

    private static readonly IValueConverter StyleConverter = Assign(Keywords.Normal, Default).OrNone().Or(
        ContentModes.ToConverter().Or(
            UrlConverter).Or(
            StringConverter).Or(
            AttrConverter).Or(
            CounterConverter).Many()).OrDefault();

    private abstract class ContentMode
    {
    }

    private sealed class NormalContentMode : ContentMode
    {
    }

    private sealed class OpenQuoteContentMode : ContentMode
    {
    }

    private sealed class CloseQuoteContentMode : ContentMode
    {
    }

    private sealed class NoOpenQuoteContentMode : ContentMode
    {
    }

    private sealed class NoCloseQuoteContentMode : ContentMode
    {
    }
}