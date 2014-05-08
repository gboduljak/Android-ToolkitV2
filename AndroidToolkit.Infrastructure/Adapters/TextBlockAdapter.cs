using System.Windows.Controls;

namespace AndroidToolkit.Infrastructure.Adapters
{
    public class TextBlockAdapter : ITextBlockAdapter
    {
        public async void Adapt(TextBlock context, string text)
        {
            await context.Dispatcher.InvokeAsync(() =>
            {
                context.Text = context.Text + "\n" + text;
            });
        }

       


    }
}
