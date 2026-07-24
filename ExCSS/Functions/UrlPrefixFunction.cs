using System;

using ExCSS.Enumerations;
using ExCSS.Model;

namespace ExCSS.Functions
{
    internal sealed class UrlPrefixFunction : DocumentFunction
    {
        public UrlPrefixFunction(string url) : base(FunctionNames.UrlPrefix, url)
        {
        }

        public override bool Matches(Url url)
        {
            return url.Href.StartsWith(Data, StringComparison.OrdinalIgnoreCase);
        }
    }
}