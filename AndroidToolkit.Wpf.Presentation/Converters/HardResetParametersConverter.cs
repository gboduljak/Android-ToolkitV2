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
    public class HardResetParametersConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            HardResetParameters parameters = new HardResetParameters()
            {
                Context = (TextBlock)values[0],
                Text = (string)values[1],
                Text2 = (string)values[2],
                Text3 = (string)values[3],
                Text4 = (string)values[4],
                Bool = (bool)values[5],
                Flyout = values[6] as Flyout
            };
            return parameters;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
