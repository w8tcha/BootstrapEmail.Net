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

using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

// ReSharper disable UnusedMember.Global

namespace ExCSS.StyleProperties;

public abstract class Property : StylesheetNode, IProperty
{
    private readonly PropertyFlags _flags;

    internal Property(string name, PropertyFlags flags = PropertyFlags.None)
    {
        Name = name;
        _flags = flags;
    }

    public override void ToCss(TextWriter writer, IStyleFormatter formatter)
    {
        writer.Write(formatter.Declaration(Name, Value, IsImportant));
    }


    internal bool TrySetValue(TokenValue newTokenValue)
    {
        var value = Converter.Convert(newTokenValue ?? TokenValue.Initial);

        if (value == null) return false;
        DeclaredValue = value;
        return true;
    }

    public string Value => DeclaredValue != null ? DeclaredValue.CssText : Keywords.Initial;

    public string Original => DeclaredValue != null ? DeclaredValue.Original.Text : Keywords.Initial;

    public bool IsInherited => (_flags & PropertyFlags.Inherited) == PropertyFlags.Inherited && IsInitial ||
                               DeclaredValue != null && DeclaredValue.CssText.Is(Keywords.Inherit);

    public bool IsAnimatable => (_flags & PropertyFlags.Animatable) == PropertyFlags.Animatable;

    public bool IsInitial => DeclaredValue == null || DeclaredValue.CssText.Is(Keywords.Initial);

    internal bool HasValue => DeclaredValue != null;

    internal bool CanBeHashless => (_flags & PropertyFlags.Hashless) == PropertyFlags.Hashless;

    internal bool CanBeUnitless => (_flags & PropertyFlags.Unitless) == PropertyFlags.Unitless;

    public bool CanBeInherited => (_flags & PropertyFlags.Inherited) == PropertyFlags.Inherited;

    internal bool IsShorthand => (_flags & PropertyFlags.Shorthand) == PropertyFlags.Shorthand;

    public string Name { get; }

    public bool IsImportant { get; set; }

    public string CssText => this.ToCss();

    internal abstract IValueConverter Converter { get; }

    internal IPropertyValue DeclaredValue { get; set; }
}