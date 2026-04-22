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

using ExCSS.Enumerations;
using ExCSS.Factories;
using ExCSS.Parser;
using ExCSS.StyleProperties;

namespace ExCSS.Rules;

internal sealed class FontFaceRule : DeclarationRule, IFontFaceRule
{
    internal FontFaceRule(StylesheetParser parser)
        : base(RuleType.FontFace, RuleNames.FontFace, parser)
    {
    }

    protected override Property CreateNewProperty(string name)
    {
        return PropertyFactory.Instance.CreateFont(name);
    }

    string IFontFaceRule.Family
    {
        get => GetValue(PropertyNames.FontFamily);
        set => SetValue(PropertyNames.FontFamily, value);
    }

    string IFontFaceRule.Source
    {
        get => GetValue(PropertyNames.Src);
        set => SetValue(PropertyNames.Src, value);
    }

    string IFontFaceRule.Style
    {
        get => GetValue(PropertyNames.FontStyle);
        set => SetValue(PropertyNames.FontStyle, value);
    }

    string IFontFaceRule.Weight
    {
        get => GetValue(PropertyNames.FontWeight);
        set => SetValue(PropertyNames.FontWeight, value);
    }

    string IFontFaceRule.Stretch
    {
        get => GetValue(PropertyNames.FontStretch);
        set => SetValue(PropertyNames.FontStretch, value);
    }

    string IFontFaceRule.Range
    {
        get => GetValue(PropertyNames.UnicodeRange);
        set => SetValue(PropertyNames.UnicodeRange, value);
    }

    string IFontFaceRule.Variant
    {
        get => GetValue(PropertyNames.FontVariant);
        set => SetValue(PropertyNames.FontVariant, value);
    }

    string IFontFaceRule.Features
    {
        get => string.Empty;
        set { }
    }
}