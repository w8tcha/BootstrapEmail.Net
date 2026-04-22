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

using System.Collections;
using System.Collections.Generic;
using System.IO;

using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.Selectors;

public sealed class ComplexSelector : StylesheetNode, ISelector, IEnumerable<CombinatorSelector>
{
    private readonly List<CombinatorSelector> _selectors;

    public ComplexSelector()
    {
        _selectors = [];
    }

    public string Text => this.ToCss();
    public int Length => _selectors.Count;
    public bool IsReady { get; private set; }

    public Priority Specificity
    {
        get
        {
            var sum = new Priority();
            var n = _selectors.Count;

            for (var i = 0; i < n; i++) sum += _selectors[i].Selector.Specificity;

            return sum;
        }
    }

    public override void ToCss(TextWriter writer, IStyleFormatter formatter)
    {
        if (_selectors.Count <= 0) return;

        var n = _selectors.Count - 1;

        for (var i = 0; i < n; i++)
        {
            writer.Write(_selectors[i].Selector.Text);
            writer.Write(_selectors[i].Delimiter);
        }

        writer.Write(_selectors[n].Selector.Text);
    }

    public void ConcludeSelector(ISelector selector)
    {
        if (IsReady) return;

        _selectors.Add(new CombinatorSelector
        {
            Selector = selector,
            Delimiter = null
        });
        IsReady = true;
    }

    public void AppendSelector(ISelector selector, Combinator combinator)
    {
        if (!IsReady)
            _selectors.Add(new CombinatorSelector
            {
                Selector = combinator.Change(selector),
                Delimiter = combinator.Delimiter
            });
    }

    public IEnumerator<CombinatorSelector> GetEnumerator()
    {
        return _selectors.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}