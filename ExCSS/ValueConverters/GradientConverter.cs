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

using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;
using ExCSS.StyleProperties;
using ExCSS.Tokens;
using ExCSS.Values;

namespace ExCSS.ValueConverters;

using static Converters;

internal abstract class GradientConverter : IValueConverter
{
    public IPropertyValue Convert(IEnumerable<Token> value)
    {
        var args = value.ToList();
        var initial = args.Count != 0 ? ConvertFirstArgument(args[0]) : null;
        var offset = initial != null ? 1 : 0;
        var stops = ToGradientStops(args, offset);
        return stops != null ? new GradientValue(initial, stops, value) : null;
    }

    public IPropertyValue Construct(Property[] properties)
    {
        return properties.Guard<GradientValue>();
    }

    private static IPropertyValue[] ToGradientStops(List<List<Token>> values, int offset)
    {
        var stops = new IPropertyValue[values.Count - offset];

        for (int i = offset, k = 0; i < values.Count; i++, k++)
        {
            stops[k] = ToGradientStop(values[i]);

            if (stops[k] == null) return null;
        }

        return stops;
    }

    private static IPropertyValue ToGradientStop(List<Token> value)
    {
        var color = default(IPropertyValue);
        var position = default(IPropertyValue);
        var items = value.ToItems();

        if (items.Count != 0)
        {
            position = LengthOrPercentConverter.Convert(items[^1]);

            if (position != null) items.RemoveAt(items.Count - 1);
        }

        if (items.Count != 0)
        {
            color = ColorConverter.Convert(items[^1]);

            if (color != null) items.RemoveAt(items.Count - 1);
        }

        return items.Count == 0 ? new StopValue(color, position, value) : null;
    }

    protected abstract IPropertyValue ConvertFirstArgument(IEnumerable<Token> value);

    private sealed class StopValue : IPropertyValue
    {
        private readonly IPropertyValue _color;
        private readonly IPropertyValue _position;

        public StopValue(IPropertyValue color, IPropertyValue position, IEnumerable<Token> tokens)
        {
            _color = color;
            _position = position;
            Original = new TokenValue(tokens);
        }

        public string CssText
        {
            get
            {
                if (_color == null && _position != null) return _position.CssText;

                if (_color != null && _position == null) return _color.CssText;

                return string.Concat(_color?.CssText, " ", _position.CssText);
            }
        }

        public TokenValue Original { get; }

        public TokenValue ExtractFor(string name)
        {
            return Original;
        }
    }

    private sealed class GradientValue : IPropertyValue
    {
        private readonly IPropertyValue _initial;
        private readonly IPropertyValue[] _stops;

        public GradientValue(IPropertyValue initial, IPropertyValue[] stops, IEnumerable<Token> tokens)
        {
            _initial = initial;
            _stops = stops;
            Original = new TokenValue(tokens);
        }

        public string CssText
        {
            get
            {
                var count = _stops.Length;

                if (_initial != null) count++;

                var args = new string[count];
                count = 0;

                if (_initial != null) args[count++] = _initial.CssText;

                foreach (var propertyValue in _stops) args[count++] = propertyValue.CssText;

                return string.Join(", ", args);
            }
        }

        public TokenValue Original { get; }

        public TokenValue ExtractFor(string name)
        {
            return Original;
        }
    }
}

internal sealed class LinearGradientConverter : GradientConverter
{
    private readonly IValueConverter _converter;

    public LinearGradientConverter()
    {
        _converter = AngleConverter.Or(
            SideOrCornerConverter.StartsWithKeyword(Keywords.To));
    }

    protected override IPropertyValue ConvertFirstArgument(IEnumerable<Token> value)
    {
        return _converter.Convert(value);
    }
}

internal sealed class RadialGradientConverter : GradientConverter
{
    private readonly IValueConverter _converter;

    public RadialGradientConverter()
    {
        var position = PointConverter.StartsWithKeyword(Keywords.At).Option(Point.Center);
        var circle = WithOrder(WithAny(Assign(Keywords.Circle, true).Option(true),
                LengthConverter.Option()),
            position);

        var ellipse = WithOrder(WithAny(Assign(Keywords.Ellipse, false).Option(false),
                LengthOrPercentConverter.Many(2, 2).Option()),
            position);

        var extents = WithOrder(WithAny(Toggle(Keywords.Circle, Keywords.Ellipse).Option(false),
            Map.RadialGradientSizeModes.ToConverter()), position);

        _converter = circle.Or(ellipse.Or(extents));
    }

    protected override IPropertyValue ConvertFirstArgument(IEnumerable<Token> value)
    {
        return _converter.Convert(value);
    }
}