using System.Collections.Generic;
using System.Linq;

using ExCSS.Model;
using ExCSS.StyleProperties;
using ExCSS.Tokens;

namespace ExCSS.ValueConverters
{
    internal sealed class RequiredValueConverter : IValueConverter
    {
        private readonly IValueConverter _converter;

        public RequiredValueConverter(IValueConverter converter)
        {
            _converter = converter;
        }

        public IPropertyValue Convert(IEnumerable<Token> value)
        {
            return value.Any() ? _converter.Convert(value) : null;
        }

        public IPropertyValue Construct(Property[] properties)
        {
            return _converter.Construct(properties);
        }
    }
}