using System.Windows.Controls;
using AndroidToolkit.Infrastructure.Helpers;

namespace AndroidToolkit.Infrastructure.Adapters
{
    public class TextBlockAdapter : ITextBlockAdapter
    {
        public async void Adapt(TextBlock context, string text)
        {
            await context.Dispatcher.InvokeAsync(() =>
            {
                context.Text = string.Format("{0}\n{1}", context.Text, text);
            });
        }
    }
}
