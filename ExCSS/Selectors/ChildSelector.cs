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

using System.IO;

using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.Selectors;

public abstract class ChildSelector : StylesheetNode, ISelector
{
    private readonly string _name;
    public int Step { get; private set; }
    public int Offset { get; private set; }
    protected ISelector Kind;

    protected ChildSelector(string name)
    {
        _name = name;
    }

    public override void ToCss(TextWriter writer, IStyleFormatter formatter)
    {
        var a = Step.ToString();

        var b = Offset switch
        {
            > 0 => "+" + Offset,
            < 0 => Offset.ToString(),
            _ => string.Empty
        };

        writer.Write(":{0}({1}n{2})", _name, a, b);
    }

    public Priority Specificity => Priority.OneClass;
    public string Text => this.ToCss();

    internal ChildSelector With(int step, int offset, ISelector kind)
    {
        Step = step;
        Offset = offset;
        Kind = kind;
        return this;
    }
}