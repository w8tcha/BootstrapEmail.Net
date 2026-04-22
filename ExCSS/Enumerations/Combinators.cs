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

namespace ExCSS.Enumerations;

public static class Combinators
{
    public static readonly string Exactly = "=";
    public static readonly string Unlike = "!=";
    public static readonly string InList = "~=";
    public static readonly string InToken = "|=";
    public static readonly string Begins = "^=";
    public static readonly string Ends = "$=";
    public static readonly string InText = "*=";
    public static readonly string Column = "||";
    public static readonly string Pipe = "|";
    public static readonly string Adjacent = "+";
    public static readonly string Descendent = " ";
    public static readonly string Deep = ">>>";
    public static readonly string Child = ">";
    public static readonly string Sibling = "~";
}