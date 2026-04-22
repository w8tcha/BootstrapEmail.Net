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
using System.IO;
using System.Linq;

using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;
using ExCSS.Parser;
using ExCSS.StyleProperties;

namespace ExCSS.Rules;

internal abstract class DeclarationRule : Rule, IProperties
{
    private readonly string _name;

    internal DeclarationRule(RuleType type, string name, StylesheetParser parser)
        : base(type, parser)
    {
        _name = name;
    }

    internal void SetProperty(Property property)
    {
        foreach (var declaration in Declarations)
        {
            if (!declaration.Name.Is(property.Name)) continue;

            ReplaceChild(declaration, property);
            return;
        }

        AppendChild(property);
    }

    public override void ToCss(TextWriter writer, IStyleFormatter formatter)
    {
        var rules = new FormatTransporter(Declarations);
        var content = formatter.Style("@" + _name, rules);
        writer.Write(content);
    }

    public string this[string propertyName] => GetValue(propertyName);
    public IEnumerable<Property> Declarations => Children.OfType<Property>();
    public int Length => Declarations.Count();

    public string GetPropertyValue(string propertyName)
    {
        return GetValue(propertyName);
    }

    public string GetPropertyPriority(string propertyName)
    {
        return null;
    }

    public void SetProperty(string propertyName, string propertyValue, string priority = null)
    {
        SetValue(propertyName, propertyValue);
    }

    public string RemoveProperty(string propertyName)
    {
        foreach (var declaration in Declarations)
        {
            if (!declaration.HasValue || !declaration.Name.Is(propertyName)) continue;

            var value = declaration.Value;
            RemoveChild(declaration);
            return value;
        }

        return null;
    }

    public IEnumerator<IProperty> GetEnumerator()
    {
        return Declarations.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private readonly struct FormatTransporter : IStyleFormattable
    {
        private readonly IEnumerable<Property> _properties;

        public FormatTransporter(IEnumerable<Property> properties)
        {
            _properties = properties.Where(m => m.HasValue);
        }

        public void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            var properties = _properties.Select(m => m.ToCss(formatter));
            var content = formatter.Declarations(properties);
            writer.Write(content);
        }
    }

    protected abstract Property CreateNewProperty(string name);

    protected string GetValue(string propertyName)
    {
        foreach (var declaration in Declarations)
            if (declaration.HasValue && declaration.Name.Is(propertyName))
                return declaration.Value;

        return string.Empty;
    }

    protected void SetValue(string propertyName, string valueText)
    {
        foreach (var declaration in Declarations)
        {
            if (!declaration.Name.Is(propertyName)) continue;

            var value = Parser.ParseValue(valueText);
            declaration.TrySetValue(value);
            return;
        }

        var property = CreateNewProperty(propertyName);

        if (property == null) return;
        {
            var value = Parser.ParseValue(valueText);
            property.TrySetValue(value);
            AppendChild(property);
        }
    }
}