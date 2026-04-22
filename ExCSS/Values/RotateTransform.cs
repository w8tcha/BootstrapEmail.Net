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

using System;

using ExCSS.Model;

namespace ExCSS.Values;

internal sealed class RotateTransform : ITransform
{
    internal RotateTransform(float x, float y, float z, float angle)
    {
        X = x;
        Y = y;
        Z = z;
        Angle = angle;
    }

    public float X { get; }
    public float Y { get; }
    public float Z { get; }
    public float Angle { get; }

    public TransformMatrix ComputeMatrix()
    {
        var norm = 1f / (float) Math.Sqrt(X * X + Y * Y + Z * Z);
        var sina = (float) Math.Sin(Angle);
        var cosa = (float) Math.Cos(Angle);

        var l = X * norm;
        var m = Y * norm;
        var n = Z * norm;
        var omc = 1f - cosa;

        return new TransformMatrix(
            l * l * omc + cosa, m * l * omc - n * sina, n * l * omc + m * sina,
            l * m * omc + n * sina, m * m * omc + cosa, n * m * omc - l * sina,
            l * n * omc - m * sina, m * n * omc + l * sina, n * n * omc + cosa,
            0f, 0f, 0f, 0f, 0f, 0f);
    }
}