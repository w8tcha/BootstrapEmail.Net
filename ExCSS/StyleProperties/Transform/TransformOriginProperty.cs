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

internal sealed class TransformOriginProperty : Property
{
    private static readonly IValueConverter StyleConverter = WithOrder(
        LengthOrPercentConverter.Or(Keywords.Center, Point.Center).Or(WithAny(
            LengthOrPercentConverter.Or(Keywords.Left, Length.Zero)
                .Or(Keywords.Right, Length.Full)
                .Or(Keywords.Center, Length.Half)
                .Option(Length.Half),
            LengthOrPercentConverter.Or(Keywords.Top, Length.Zero)
                .Or(Keywords.Bottom, Length.Full)
                .Or(Keywords.Center, Length.Half)
                .Option(Length.Half))).Or(
            WithAny(
                LengthOrPercentConverter.Or(Keywords.Top, Length.Zero)
                    .Or(Keywords.Bottom, Length.Full)
                    .Or(Keywords.Center, Length.Half)
                    .Option(Length.Half),
                LengthOrPercentConverter.Or(Keywords.Left, Length.Zero)
                    .Or(Keywords.Right, Length.Full)
                    .Or(Keywords.Center, Length.Half)
                    .Option(Length.Half))).Required(),
        LengthConverter.Option(Length.Zero)).OrDefault(Point.Center);

    internal TransformOriginProperty()
        : base(PropertyNames.TransformOrigin, PropertyFlags.Animatable)
    {
    }

    internal override IValueConverter Converter => StyleConverter;
}