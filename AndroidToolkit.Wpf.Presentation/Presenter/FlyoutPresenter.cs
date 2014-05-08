using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls;

namespace AndroidToolkit.Wpf.Presentation.Presenter
{
    public static class FlyoutPresenter
    {
        public static void Present(MetroWindow context, int index)
        {
            Flyout flyout = context.Flyouts.Items[index] as Flyout;
            if (flyout != null)
            {
                flyout.IsOpen = !flyout.IsOpen;
                return;
            }
            return;
        }
    }
}
