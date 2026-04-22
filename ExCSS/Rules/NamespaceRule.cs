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

internal sealed class NamespaceRule : Rule, INamespaceRule
{
    private string _namespaceUri;
    private string _prefix;

    internal NamespaceRule(StylesheetParser parser)
        : base(RuleType.Namespace, parser)
    {
    }

    public override void ToCss(TextWriter writer, IStyleFormatter formatter)
    {
        var space = string.IsNullOrEmpty(_prefix) ? string.Empty : " ";
        var value = string.Concat(_prefix, space, _namespaceUri.StylesheetUrl());
        writer.Write(formatter.Rule("@namespace", value));
    }

    protected override void ReplaceWith(IRule rule)
    {
        var newRule = rule as NamespaceRule;
        _namespaceUri = newRule?._namespaceUri;
        _prefix = newRule?._prefix;
        base.ReplaceWith(rule);
    }

    public string NamespaceUri
    {
        get => _namespaceUri;
        set
        {
            CheckValidity();
            _namespaceUri = value ?? string.Empty;
        }
    }

    public string Prefix
    {
        get => _prefix;
        set
        {
            CheckValidity();
            _prefix = value ?? string.Empty;
        }
    }

    private static bool IsNotSupported(RuleType type)
    {
        return type != RuleType.Charset && type != RuleType.Import && type != RuleType.Namespace;
    }

    private void CheckValidity()
    {
        var parent = Owner;
        var list = parent?.Rules;

        if (list == null) return;

        if (list.Any(entry => IsNotSupported(entry.Type))) throw new ParseException("Rule is not supported");
    }
}