using System;

namespace ExCSS.Model
{
    public class ParseException : Exception
    {
        public ParseException(string message) : base(message)
        {
        }
    }
}