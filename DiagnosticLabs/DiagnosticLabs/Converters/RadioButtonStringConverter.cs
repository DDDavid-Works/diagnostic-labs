using System;
using System.Globalization;
using System.Windows.Data;

namespace DiagnosticLabs.Converters
{
    public class RadioButtonStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string val = (string)value;
            if (val.Trim() == parameter.ToString())
                return true;
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return parameter;
        }
    }
}
