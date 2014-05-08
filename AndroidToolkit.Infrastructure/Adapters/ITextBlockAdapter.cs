using System.Windows.Controls;

namespace AndroidToolkit.Infrastructure.Adapters
{
    public interface ITextBlockAdapter 
    {
        void Adapt(TextBlock context, string text);
    }
}
