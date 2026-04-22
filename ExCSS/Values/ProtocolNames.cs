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

using ExCSS.Extensions;

namespace ExCSS.Values;

public static class ProtocolNames
{
    public static readonly string Http = "http";
    public static readonly string Https = "https";
    public static readonly string Ftp = "ftp";
    public static readonly string JavaScript = "javascript";
    public static readonly string Data = "data";
    public static readonly string Mailto = "mailto";
    public static readonly string File = "file";
    public static readonly string Ws = "ws";
    public static readonly string Wss = "wss";
    public static readonly string Telnet = "telnet";
    public static readonly string Ssh = "ssh";
    public static readonly string Gopher = "gopher";
    public static readonly string Blob = "blob";

    private static readonly string[] RelativeProtocols =
    [
        Http,
        Https,
        Ftp,
        File,
        Ws,
        Wss,
        Gopher
    ];

    private static readonly string[] OriginalableProtocols =
    [
        Http,
        Https,
        Ftp,
        Ws,
        Wss,
        Gopher
    ];

    public static bool IsRelative(string protocol)
    {
        return RelativeProtocols.Contains(protocol);
    }

    public static bool IsOriginable(string protocol)
    {
        return OriginalableProtocols.Contains(protocol);
    }
}