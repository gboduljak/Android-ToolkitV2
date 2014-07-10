using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using MahApps.Metro.Controls;

namespace AndroidToolkit.Wpf.Presentation.Converters
{
    public class RootParametersConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return new RootParameters()
            {
                SuperUser = (bool)values[0],
                SuperSU = (bool)values[1],
                Window = (MetroWindow)values[2],
                Context = (TextBlock)values[3],
                CreateNoWindow = (bool)values[4],
                Target = (string)values[5]
            };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
