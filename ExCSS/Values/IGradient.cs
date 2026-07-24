using System.Collections.Generic;

namespace ExCSS.Values
{
    public interface IGradient : IImageSource
    {
        IEnumerable<GradientStop> Stops { get; }
        bool IsRepeating { get; }
    }
}