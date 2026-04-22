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
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Font;

internal sealed class FontFamilyProperty : Property
{
    private static readonly IValueConverter StyleConverter =
        Converters.FontFamiliesConverter.OrDefault("Times New Roman");

    internal FontFamilyProperty()
        : base(PropertyNames.FontFamily, PropertyFlags.Inherited)
    {
    }

    internal override IValueConverter Converter => StyleConverter;

    //private enum SystemFonts : byte
    //{
    //    /// <summary>
    //    ///     Glyphs have finishing strokes, flared or tapering ends, or have actual serifed endings.
    //    ///     E.g.  Palatino, "Palatino Linotype", Palladio, "URW Palladio", serif
    //    /// </summary>
    //    Serif,

    //    /// <summary>
    //    ///     Glyphs have stroke endings that are plain.
    //    ///     E.g. 'Trebuchet MS', 'Liberation Sans', 'Nimbus Sans L', sans-serif
    //    /// </summary>
    //    SansSerif,

    //    /// <summary>
    //    ///     All glyphs have the same fixed width.
    //    ///     E.g. "DejaVu Sans Mono", Menlo, Consolas, "Liberation Mono", Monaco, "Lucida Console", monospace
    //    /// </summary>
    //    Monospace,

    //    /// <summary>
    //    ///     Glyphs in cursive fonts generally have either joining strokes or other cursive characteristics
    //    ///     beyond those of italic typefaces. The glyphs are partially or completely connected, and the
    //    ///     result looks more like handwritten pen or brush writing than printed letterwork.
    //    /// </summary>
    //    Cursive,

    //    /// <summary>
    //    ///     Fantasy fonts are primarily decorative fonts that contain playful representations of characters.
    //    /// </summary>
    //    Fantasy
    //}
}