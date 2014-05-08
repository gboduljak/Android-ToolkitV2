using System;
using System.Windows.Controls;
using System.Windows.Data;

namespace AndroidToolkit.Wpf.Presentation.Converters
{
    public class ThreeCommandParametersConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ThreeCommandParameters parameters = new ThreeCommandParameters();
            foreach (var obj in values)
            {
                if (obj is string) parameters.Text = (string)obj;
                else if (obj is bool) parameters.Bool = (bool)obj;
                else
                {
                    parameters.Context = (TextBlock)obj;
                }
            }
            return parameters;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
