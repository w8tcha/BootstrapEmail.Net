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

namespace ExCSS.Values;

internal static class PortNumbers
{
    private static readonly Dictionary<string, string> Ports = new()
    {
        {ProtocolNames.Http, "80"},
        {ProtocolNames.Https, "443"},
        {ProtocolNames.Ftp, "21"},
        {ProtocolNames.File, ""},
        {ProtocolNames.Ws, "80"},
        {ProtocolNames.Wss, "443"},
        {ProtocolNames.Gopher, "70"},
        {ProtocolNames.Telnet, "23"},
        {ProtocolNames.Ssh, "22"}
    };

    public static string GetDefaultPort(string protocol)
    {
        Ports.TryGetValue(protocol, out var value);
        return value;
    }
}