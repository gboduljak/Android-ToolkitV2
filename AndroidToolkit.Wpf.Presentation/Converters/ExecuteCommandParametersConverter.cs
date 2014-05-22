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
    public class ExecuteCommandParametersConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return new ExecuteCommandParameters()
            {
                Context = values[0] as TextBlock,
                Text = values[1] as string,
                Text2 = values[2] as string,
                Text3 = values[3] as string,
                Text4 = values[4] as string,
                Text5 = values[5] as string,
                Text6 = values[6] as string,
                Text7 = values[7] as string,
                Text8 = values[8] as string,
                Text9 = values[9] as string,
                Text10 = values[10] as string,
                Bool = System.Convert.ToBoolean(values[11]),
                Target = values[12] as string
            };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
