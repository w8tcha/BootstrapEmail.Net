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
using System.Linq;

using ExCSS.Extensions;
using ExCSS.Parser;

namespace ExCSS.Model;

public sealed class MediaList : StylesheetNode
{
    private readonly StylesheetParser _parser;

    internal MediaList(StylesheetParser parser)
    {
        _parser = parser;
    }

    public string this[int index] => Media.GetItemByIndex(index).ToCss();
    public IEnumerable<Medium> Media => Children.OfType<Medium>();
    public int Length => Media.Count();

    public string MediaText
    {
        get => this.ToCss();
        set
        {
            Clear();

            foreach (var medium in _parser.ParseMediaList(value))
            {
                if (medium == null) throw new ParseException("Unable to parse media list element");
                AppendChild(medium);
            }
        }
    }

    public override void ToCss(TextWriter writer, IStyleFormatter formatter)
    {
        var parts = Media.ToArray();
        if (parts.Length <= 0) return;
        parts[0].ToCss(writer, formatter);

        for (var i = 1; i < parts.Length; i++)
        {
            writer.Write(", ");
            parts[i].ToCss(writer, formatter);
        }
    }

    public void Add(string newMedium)
    {
        var medium = _parser.ParseMedium(newMedium);
        if (medium == null) throw new ParseException("Unable to parse medium");
        AppendChild(medium);
    }

    public void Remove(string oldMedium)
    {
        var medium = _parser.ParseMedium(oldMedium);
        if (medium == null) throw new ParseException("Unable to parse medium");

        foreach (var item in Media)
        {
            if (!item.Equals(medium)) continue;

            RemoveChild(item);
            return;
        }

        throw new ParseException("Media list element not found");
    }

    public IEnumerator<Medium> GetEnumerator()
    {
        return Media.GetEnumerator();
    }
}