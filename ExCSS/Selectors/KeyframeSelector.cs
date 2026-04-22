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
using System.IO;

using ExCSS.Extensions;
using ExCSS.Model;
using ExCSS.Values;

namespace ExCSS.Selectors;

public sealed class KeyframeSelector : StylesheetNode
{
    private readonly List<Percent> _stops;

    public KeyframeSelector(IEnumerable<Percent> stops)
    {
        _stops = new List<Percent>(stops);
    }

    public override void ToCss(TextWriter writer, IStyleFormatter formatter)
    {
        if (_stops.Count <= 0) return;

        writer.Write(_stops[0].ToString());
        for (var i = 1; i < _stops.Count; i++)
        {
            writer.Write(", ");
            writer.Write(_stops[i].ToString());
        }
    }

    public IEnumerable<Percent> Stops => _stops;
    public string Text => this.ToCss();
}

public sealed class PageSelector : StylesheetNode, ISelector
{
    private readonly string _name;

    public PageSelector(string name)
    {
        _name = name;
    }

    public PageSelector() : this(string.Empty)
    {
    }

    public override void ToCss(TextWriter writer, IStyleFormatter formatter)
    {
        var pseudo = _name == string.Empty ? "" : ":";
        writer.Write($"{pseudo}{_name}");
    }

    public Priority Specificity => Priority.Inline;
    public string Text => this.ToCss();
}