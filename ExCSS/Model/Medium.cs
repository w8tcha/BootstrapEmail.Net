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

using ExCSS.Extensions;
using ExCSS.MediaFeatures;

namespace ExCSS.Model;

public sealed class Medium : StylesheetNode
{
    public IEnumerable<MediaFeature> Features => Children.OfType<MediaFeature>();
    public string Type { get; internal set; }
    public bool IsExclusive { get; internal set; }
    public bool IsInverse { get; internal set; }

    public string Constraints
    {
        get
        {
            var constraints = Features.Select(m => m.ToCss());
            return string.Join(" and ", constraints);
        }
    }

    public override bool Equals(object obj)
    {
        if (obj is Medium other &&
            other.IsExclusive == IsExclusive &&
            other.IsInverse == IsInverse &&
            other.Type.Is(Type) &&
            other.Features.Count() == Features.Count())
        { 
            return other.Features.Select(feature =>
                Features.Any(m => m.Name.Is(feature.Name) && m.Value.Is(feature.Value))).All(isShared => isShared);
        }

        return false;
    }

    public override int GetHashCode()
    {
        // ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode
        return base.GetHashCode();
    }

    public override void ToCss(TextWriter writer, IStyleFormatter formatter)
    {
        writer.Write(formatter.Medium(IsExclusive, IsInverse, Type, Features));
    }
}