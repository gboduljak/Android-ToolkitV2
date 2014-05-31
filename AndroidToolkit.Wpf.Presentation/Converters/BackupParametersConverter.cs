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
    public class BackupParametersConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return new BackupParameters()
            {
                Context = values[0] as TextBlock,
                Context2 = values[1] as ComboBox,
                Text = values[2] as string,
                Text2 = values[3] as string,
                Bool = System.Convert.ToBoolean(values[4]),
                Target = values[5] as string
            };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
