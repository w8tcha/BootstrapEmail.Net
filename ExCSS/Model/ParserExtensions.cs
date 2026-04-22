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
using System.Collections.Generic;

using ExCSS.Conditions;
using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Functions;
using ExCSS.Parser;
using ExCSS.Rules;
using ExCSS.Tokens;

namespace ExCSS.Model;

internal static class ParserExtensions
{
    private static readonly Dictionary<string, Func<string, DocumentFunction>> FunctionTypes =
        new(StringComparer.OrdinalIgnoreCase)
        {
            {FunctionNames.Url, url => new UrlFunction(url)},
            {FunctionNames.Domain, url => new DomainFunction(url)},
            {FunctionNames.UrlPrefix, url => new UrlPrefixFunction(url)}
        };

    private static readonly Dictionary<string, Func<IEnumerable<IConditionFunction>, IConditionFunction>>
        GroupCreators =
            new(
                StringComparer.OrdinalIgnoreCase)
            {
                {Keywords.And, CreateAndCondition},
                {Keywords.Or, CreateOrCondition}
            };

    private static IConditionFunction CreateAndCondition(IEnumerable<IConditionFunction> conditions)
    {
        var andCondition = new AndCondition();
        foreach (var condition in conditions)
        {
            andCondition.AppendChild(condition);
        }
        return andCondition;
    }

    private static IConditionFunction CreateOrCondition(IEnumerable<IConditionFunction> conditions)
    {
        var orCondition = new OrCondition();
        foreach (var condition in conditions)
        {
            orCondition.AppendChild(condition);
        }
        return orCondition;
    }

    public static TokenType GetTypeFromName(this string functionName)
    {
        return FunctionTypes.TryGetValue(functionName, out _) 
            ? TokenType.Url 
            : TokenType.Function;
    }

    public static Func<IEnumerable<IConditionFunction>, IConditionFunction> GetCreator(this string functionName)
    {
        GroupCreators.TryGetValue(functionName, out var creator);
        return creator;
    }

    public static int GetCode(this ParseError code)
    {
        return (int) code;
    }

    public static bool Is(this Token token, TokenType a)
    {
        var type = token.Type;
        return type == a;
    }

    public static bool Is(this Token token, TokenType a, TokenType b)
    {
        var type = token.Type;
        return type == a || type == b;
    }

    public static bool IsNot(this Token token, TokenType a, TokenType b)
    {
        var type = token.Type;
        return type != a && type != b;
    }

    public static bool IsNot(this Token token, TokenType a, TokenType b, TokenType c)
    {
        var type = token.Type;
        return type != a && type != b && type != c;
    }

    public static bool IsDeclarationName(this Token token)
    {
        return token.Type != TokenType.EndOfFile &&
               token.Type != TokenType.Colon &&
               token.Type != TokenType.Whitespace &&
               token.Type != TokenType.Comment &&
               token.Type != TokenType.CurlyBracketOpen &&
               token.Type != TokenType.Semicolon;
    }

    public static DocumentFunction ToDocumentFunction(this Token token)
    {
        switch (token.Type)
        {
            case TokenType.Url:
            {
                var functionName = ((UrlToken)token).FunctionName;
                FunctionTypes.TryGetValue(functionName, out var creator);
                return creator(token.Data);
            }
            case TokenType.Function when token.Data.Isi(FunctionNames.Regexp):
            {
                var css = ((FunctionToken) token).ArgumentTokens.ToCssString();
                if (css != null)
                {
                    return new RegexpFunction(css);
                }

                break;
            }
        }

        return null;
    }

    public static Rule CreateRule(this StylesheetParser parser, RuleType type)
    {
        return type switch
        {
            RuleType.Charset => new CharsetRule(parser),
            RuleType.Document => new DocumentRule(parser),
            RuleType.FontFace => new FontFaceRule(parser),
            RuleType.Import => new ImportRule(parser),
            RuleType.Keyframe => new KeyframeRule(parser),
            RuleType.Keyframes => new KeyframesRule(parser),
            RuleType.Media => new MediaRule(parser),
            RuleType.Container => new ContainerRule(parser),
            RuleType.Namespace => new NamespaceRule(parser),
            RuleType.Page => new PageRule(parser),
            RuleType.Style => new StyleRule(parser),
            RuleType.Supports => new SupportsRule(parser),
            RuleType.Viewport => new ViewportRule(parser),
            _ => null
        };
    }
}