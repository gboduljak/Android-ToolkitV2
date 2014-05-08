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
    public class FileOpsParametersConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return new FileOpsParameters() { Context = values[0] as TextBlock, Text = values[1] as String, Text2 = values[2] as String, Bool = (bool)values[3], Target = (string)values[4] };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
