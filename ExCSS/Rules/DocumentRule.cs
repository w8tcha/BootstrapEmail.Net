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
using ExCSS.Extensions;
using ExCSS.Functions;
using ExCSS.Model;
using ExCSS.Parser;

namespace ExCSS.Rules;

internal sealed class DocumentRule : GroupingRule
{
    internal DocumentRule(StylesheetParser parser)
        : base(RuleType.Document, parser)
    {
    }

    public override void ToCss(TextWriter writer, IStyleFormatter formatter)
    {
        var rules = formatter.Block(Rules);
        writer.Write(formatter.Rule("@document", ConditionText, rules));
    }

    public string ConditionText
    {
        get
        {
            var entries = Conditions.Select(m => m.ToCss());
            return string.Join(", ", entries);
        }
        set
        {
            var conditions = Parser.ParseDocumentRules(value);

            if (conditions == null) throw new ParseException("Unable to parse document rules");

            Clear();

            foreach (var condition in conditions) AppendChild(condition);
        }
    }

    public IEnumerable<IDocumentFunction> Conditions => Children.OfType<IDocumentFunction>();
}