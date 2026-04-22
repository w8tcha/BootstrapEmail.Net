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
using System.Linq;

using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;
using ExCSS.Parser;

namespace ExCSS.Rules;

internal sealed class KeyframesRule : Rule, IKeyframesRule
{
    internal KeyframesRule(StylesheetParser parser)
        : base(RuleType.Keyframes, parser)
    {
        Rules = new RuleList(this);
    }

    protected override void ReplaceWith(IRule rule)
    {
        var newRule = rule as KeyframesRule;
        Name = newRule?.Name;
        base.ReplaceWith(rule);
    }

    public string Name { get; set; }
    public RuleList Rules { get; }
    IRuleList IKeyframesRule.Rules => Rules;

    public void Add(string ruleText)
    {
        var rule = Parser.ParseKeyframeRule(ruleText);
        Rules.Add(rule);
    }

    public void Remove(string key)
    {
        var element = Find(key);
        Rules.Remove(element);
    }

    public KeyframeRule Find(string key)
    {
        return Rules.OfType<KeyframeRule>().FirstOrDefault(m => key.Isi(m.KeyText));
    }

    public override void ToCss(TextWriter writer, IStyleFormatter formatter)
    {
        var rules = formatter.Block(Rules);
        writer.Write(formatter.Rule("@keyframes", Name, rules));
    }

    IKeyframeRule IKeyframesRule.Find(string key)
    {
        return Find(key);
    }
}