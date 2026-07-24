using ExCSS.Model;

namespace ExCSS.Values
{
    public interface ITransform
    {
        TransformMatrix ComputeMatrix();
    }
}