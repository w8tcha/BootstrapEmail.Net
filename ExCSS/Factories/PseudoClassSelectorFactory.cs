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
using ExCSS.Selectors;

namespace ExCSS.Factories;

public sealed class PseudoClassSelectorFactory
{
    private static readonly Lazy<PseudoClassSelectorFactory> Lazy =
        new(() =>
            {
                var factory = new PseudoClassSelectorFactory();
                Selectors.Add(PseudoElementNames.Before,
                    PseudoElementSelectorFactory.Instance.Create(PseudoElementNames.Before));
                Selectors.Add(PseudoElementNames.After,
                    PseudoElementSelectorFactory.Instance.Create(PseudoElementNames.After));
                Selectors.Add(PseudoElementNames.FirstLine,
                    PseudoElementSelectorFactory.Instance.Create(PseudoElementNames.FirstLine));
                Selectors.Add(PseudoElementNames.FirstLetter,
                    PseudoElementSelectorFactory.Instance.Create(PseudoElementNames.FirstLetter));
                return factory;
            }
        );

    #region Selectors

    private static readonly Dictionary<string, ISelector> Selectors =
        new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                PseudoClassNames.Root,
                PseudoClassNames.Scope,
                PseudoClassNames.OnlyType,
                PseudoClassNames.FirstOfType,
                PseudoClassNames.LastOfType,
                PseudoClassNames.OnlyChild,
                PseudoClassNames.FirstChild,
                PseudoClassNames.LastChild,
                PseudoClassNames.Empty,
                PseudoClassNames.AnyLink,
                PseudoClassNames.Link,
                PseudoClassNames.Visited,
                PseudoClassNames.Active,
                PseudoClassNames.Hover,
                PseudoClassNames.Focus,
                PseudoClassNames.FocusVisible,
                PseudoClassNames.FocusWithin,
                PseudoClassNames.Target,
                PseudoClassNames.Enabled,
                PseudoClassNames.Disabled,
                PseudoClassNames.Default,
                PseudoClassNames.Checked,
                PseudoClassNames.Indeterminate,
                PseudoClassNames.PlaceholderShown,
                PseudoClassNames.Unchecked,
                PseudoClassNames.Valid,
                PseudoClassNames.Invalid,
                PseudoClassNames.Required,
                PseudoClassNames.ReadOnly,
                PseudoClassNames.ReadWrite,
                PseudoClassNames.InRange,
                PseudoClassNames.OutOfRange,
                PseudoClassNames.Optional,
                PseudoClassNames.Shadow
            }
            .ToDictionary(x => x, PseudoClassSelector.Create);

    #endregion

    internal static PseudoClassSelectorFactory Instance => Lazy.Value;

    public ISelector Create(string name)
    {
        return Selectors.GetValueOrDefault(name);
    }
}