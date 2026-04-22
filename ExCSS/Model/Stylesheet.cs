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

using ExCSS.Enumerations;
using ExCSS.Parser;
using ExCSS.Rules;

// ReSharper disable UnusedMember.Global

namespace ExCSS.Model;

public sealed class Stylesheet : StylesheetNode
{
    private readonly StylesheetParser _parser;

    internal Stylesheet(StylesheetParser parser)
    {
        _parser = parser;
        Rules = new RuleList(this);
    }

    internal RuleList Rules { get; }

    public IEnumerable<ICharsetRule> CharacterSetRules => Rules.Where(r => r is CharsetRule).Cast<ICharsetRule>();
    public IEnumerable<IFontFaceRule> FontfaceSetRules => Rules.Where(r => r is FontFaceRule).Cast<IFontFaceRule>();
    public IEnumerable<IMediaRule> MediaRules => Rules.Where(r => r is MediaRule).Cast<IMediaRule>();
    public IEnumerable<IContainerRule> ContainerRules => Rules.Where(r => r is ContainerRule).Cast<IContainerRule>();
    public IEnumerable<IImportRule> ImportRules => Rules.Where(r => r is ImportRule).Cast<IImportRule>();

    public IEnumerable<INamespaceRule> NamespaceRules =>
        Rules.Where(r => r is NamespaceRule).Cast<INamespaceRule>();

    public IEnumerable<IPageRule> PageRules => Rules.Where(r => r is PageRule).Cast<IPageRule>();
    public IEnumerable<IStyleRule> StyleRules => Rules.Where(r => r is StyleRule).Cast<IStyleRule>();

    public IRule Add(RuleType ruleType)
    {
        var rule = _parser.CreateRule(ruleType);
        Rules.Add(rule);
        return rule;
    }

    public void RemoveAt(int index)
    {
        Rules.RemoveAt(index);
    }

    public int Insert(string ruleText, int index)
    {
        var rule = _parser.ParseRule(ruleText);
        rule.Owner = this;
        Rules.Insert(index, rule);

        return index;
    }

    public override void ToCss(TextWriter writer, IStyleFormatter formatter)
    {
        writer.Write(formatter.Sheet(Rules));
    }
}