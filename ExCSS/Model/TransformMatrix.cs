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

namespace ExCSS.Model;

public sealed class TransformMatrix : IEquatable<TransformMatrix>
{
    public static readonly TransformMatrix Zero = new ();
    public static readonly TransformMatrix One = new (1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 0f, 0f);
    private readonly float[,] _matrix;

    public bool Equals(TransformMatrix other)
    {
        var a = _matrix;
        var b = other._matrix;

        for (var i = 0; i < 4; i++)
        {
            for (var j = 0; j < 4; j++)
            {
                if (a[i, j] != b[i, j])
                {
                    return false;
                }
            }
        }

        return true;
    }

    private TransformMatrix()
    {
        _matrix = new float[4, 4];
    }

    public TransformMatrix(float[] values) : this()
    {
        if (values == null)
        {
            throw new ArgumentNullException(nameof(values));
        }

        if (values.Length != 16)
        {
            throw new ArgumentException("You need to provide 16 (4x4) values.", nameof(values));
        }

        for (int i = 0, k = 0; i < 4; i++)
        {
            for (var j = 0; j < 4; j++, k++)
            {
                _matrix[j, i] = values[k];
            }
        }
    }

    public TransformMatrix(
        float m11, float m12, float m13,
        float m21, float m22, float m23,
        float m31, float m32, float m33,
        float tx, float ty, float tz,
        float px, float py, float pz)
        : this()
    {
        _matrix[0, 0] = m11;
        _matrix[0, 1] = m12;
        _matrix[0, 2] = m13;
        _matrix[1, 0] = m21;
        _matrix[1, 1] = m22;
        _matrix[1, 2] = m23;
        _matrix[2, 0] = m31;
        _matrix[2, 1] = m32;
        _matrix[2, 2] = m33;
        _matrix[0, 3] = tx;
        _matrix[1, 3] = ty;
        _matrix[2, 3] = tz;
        _matrix[3, 0] = px;
        _matrix[3, 1] = py;
        _matrix[3, 2] = pz;
        _matrix[3, 3] = 1f;
    }

    public float Tx => _matrix[0, 3];
    public float Ty => _matrix[1, 3];
    public float Tz => _matrix[2, 3];

    public override bool Equals(object obj)
    {
        return obj is TransformMatrix other && Equals(other);
    }

    public override int GetHashCode()
    {
        var sum = 0f;

        for (var i = 0; i < 4; i++)
        {
            for (var j = 0; j < 4; j++)
            {
                sum += _matrix[i, j] * (4 * i + j);
            }
        }

        return (int)sum;
    }
}