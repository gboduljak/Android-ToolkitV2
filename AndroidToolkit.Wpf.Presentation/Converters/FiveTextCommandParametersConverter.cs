using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace AndroidToolkit.Wpf.Presentation.Converters
{
    public class FiveTextCommandParametersConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            FiveTextCommandParameters parameters = new FiveTextCommandParameters
            {
                Context = (TextBlock)values[0],
                Text = (string)values[1],
                Text2 = (string)values[2],
                Text3 = (string)values[3],
                Text4 = (string)values[4],
                Text5 = (string)values[5],
                Bool = (bool)values[6],
                Target=(string)values[7]
            };
            return parameters;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
