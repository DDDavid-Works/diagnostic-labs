using System;
using System.Globalization;
using System.Windows.Data;

namespace DiagnosticLabs.Converters
{
    public class RadioButtonBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool val = (bool)value;
            if (val == bool.Parse(parameter.ToString()))
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
