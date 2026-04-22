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

using ExCSS.Enumerations;
using ExCSS.Model;
using ExCSS.Parser;
using ExCSS.Selectors;

namespace ExCSS.Factories;

public sealed class PseudoElementSelectorFactory
{
    private static readonly Lazy<PseudoElementSelectorFactory> Lazy =
        new(() => new PseudoElementSelectorFactory());

    private readonly StylesheetParser _parser;

    #region Selectors

    private readonly Dictionary<string, ISelector> _selectors =
        new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                //TODO some lack implementation (selection, content, ...)
                // some implementations are dubious (first-line, first-letter, ...)
                PseudoElementNames.Before,
                PseudoElementNames.After,
                PseudoElementNames.Selection,
                PseudoElementNames.FirstLine,
                PseudoElementNames.FirstLetter,
                PseudoElementNames.Content
            }
            .ToDictionary(x => x, PseudoElementSelector.Create);

    #endregion

    internal PseudoElementSelectorFactory(StylesheetParser parser = null)
    {
        _parser = parser;
    }

    internal static PseudoElementSelectorFactory Instance => Lazy.Value;

    public ISelector Create(string name)
    {
        return _selectors.TryGetValue(name, out var selector) ? selector :
            ((_parser?.Options.AllowInvalidSelectors ?? false) ?
                PseudoElementSelector.Create(name) : null);
    }
}