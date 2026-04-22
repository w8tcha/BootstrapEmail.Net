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

using ExCSS.Rules;

namespace ExCSS.Model;

public abstract class StylesheetNode : IStylesheetNode
{
    private readonly List<IStylesheetNode> _children = [];

    protected void ReplaceAll(IStylesheetNode node)
    {
        Clear();
        StylesheetText = node.StylesheetText;
        foreach (var child in node.Children) AppendChild(child);
    }

    public StylesheetText StylesheetText { get; internal set; }

    public IEnumerable<IStylesheetNode> Children => _children.AsEnumerable();

    public abstract void ToCss(TextWriter writer, IStyleFormatter formatter);

    public void AppendChild(IStylesheetNode child)
    {
        Setup(child);
        _children.Add(child);
    }

    public void ReplaceChild(IStylesheetNode oldChild, IStylesheetNode newChild)
    {
        for (var i = 0; i < _children.Count; i++)
        {   if (ReferenceEquals(oldChild, _children[i]))
            {
                Teardown(oldChild);
                Setup(newChild);
                _children[i] = newChild;
                return;
            }
        }
    }

    public void InsertBefore(IStylesheetNode referenceChild, IStylesheetNode child)
    {
        if (referenceChild != null)
        {
            var index = _children.IndexOf(referenceChild);
            InsertChild(index, child);
        }
        else
        {
            AppendChild(child);
        }
    }

    public void InsertChild(int index, IStylesheetNode child)
    {
        Setup(child);
        _children.Insert(index, child);
    }

    public void RemoveChild(IStylesheetNode child)
    {
        Teardown(child);
        _children.Remove(child);
    }

    public void Clear()
    {
        for (var i = _children.Count - 1; i >= 0; i--)
        {
            var child = _children[i];
            RemoveChild(child);
        }
    }

    private void Setup(IStylesheetNode child)
    {
        if (child is not Rule rule) return;
        rule.Owner = this as Stylesheet;
        rule.Parent = this as IRule;
    }

    private static void Teardown(IStylesheetNode child)
    {
        if (child is not Rule rule) return;
        rule.Parent = null;
        rule.Owner = null;
    }
}