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
using System.Linq;

using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.Selectors;

public abstract class Selectors : StylesheetNode, IEnumerable<ISelector>
{
    protected readonly List<ISelector> _selectors;

    protected Selectors()
    {
        _selectors = [];
    }

    public Priority Specificity
    {
        get
        {
            var sum = new Priority();

            return _selectors.Aggregate(sum, (current, t) => current + t.Specificity);
        }
    }

    public string Text => this.ToCss();
    public int Length => _selectors.Count;

    public ISelector this[int index]
    {
        get => _selectors[index];
        set => _selectors[index] = value;
    }

    public void Add(ISelector selector)
    {
        _selectors.Add(selector);
    }

    public void Remove(ISelector selector)
    {
        _selectors.Remove(selector);
    }

    public IEnumerator<ISelector> GetEnumerator()
    {
        return _selectors.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}