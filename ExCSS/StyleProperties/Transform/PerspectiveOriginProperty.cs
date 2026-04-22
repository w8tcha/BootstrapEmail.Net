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
using ExCSS.Values;

namespace ExCSS.StyleProperties.Transform;

using static Converters;

internal sealed class PerspectiveOriginProperty : Property
{
    private static readonly IValueConverter PerspectiveConverter = LengthOrPercentConverter.Or(
        Keywords.Left, new Point(Length.Zero, Length.Half)).Or(
        Keywords.Center, new Point(Length.Half, Length.Half)).Or(
        Keywords.Right, new Point(Length.Full, Length.Half)).Or(
        Keywords.Top, new Point(Length.Half, Length.Zero)).Or(
        Keywords.Bottom, new Point(Length.Half, Length.Full)).Or(
        WithAny(
            LengthOrPercentConverter.Or(Keywords.Left, Length.Zero)
                .Or(Keywords.Right, Length.Full)
                .Or(Keywords.Center, Length.Half)
                .Option(Length.Half),
            LengthOrPercentConverter.Or(Keywords.Top, Length.Zero)
                .Or(Keywords.Bottom, Length.Full)
                .Or(Keywords.Center, Length.Half)
                .Option(Length.Half))).OrDefault(Point.Center);


    internal PerspectiveOriginProperty()
        : base(PropertyNames.PerspectiveOrigin, PropertyFlags.Animatable)
    {
    }

    internal override IValueConverter Converter => PerspectiveConverter;
}