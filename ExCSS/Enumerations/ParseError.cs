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

public enum ParseError : byte
{
    EOF = 0,
    InvalidCharacter = 16, // 0x10,
    InvalidBlockStart = 17, // 0x11,
    InvalidToken = 18, // 0x12,
    ColonMissing = 19, // 0x13,
    IdentExpected = 20, // 0x14,
    InputUnexpected = 21, // 0x15,
    LineBreakUnexpected = 22, // 0x16,
    UnknownAtRule = 32, // 0x20,
    InvalidSelector = 48, // 0x30,
    InvalidKeyframe = 49, // 0x31,
    ValueMissing = 64, // 0x40,
    InvalidValue = 65, // 0x41,
    UnknownDeclarationName = 80 // 0x50
}