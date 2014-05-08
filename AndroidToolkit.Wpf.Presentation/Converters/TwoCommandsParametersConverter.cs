using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace AndroidToolkit.Wpf.Presentation.Converters
{
    public class TwoCommandsParametersConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            TwoCommandParameters parameters = new TwoCommandParameters()
            {
                Context = values[0] as TextBlock,
                Text = values[1] as String,
                Bool = (bool) values[2],
                Bool2 = (bool) values[3],
                Target = (string)values[4]
            };
            
            return parameters;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
