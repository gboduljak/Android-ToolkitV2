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
    public class ThreeTextCommandParametersConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ThreeTextCommandParameters parameters = new ThreeTextCommandParameters
            {
                Context = (TextBlock) values[0],
                Text = (string) values[1],
                Text2 = (string) values[2],
                Bool = (bool) values[3],
                Target = (string)values[4]
            };
            return parameters;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
