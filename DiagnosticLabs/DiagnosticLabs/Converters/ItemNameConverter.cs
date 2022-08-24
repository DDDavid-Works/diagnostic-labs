using DiagnosticLabsBLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DiagnosticLabs.Converters
{
    [ValueConversion(typeof(long), typeof(string))]
    public class ItemNameConverter : IValueConverter
    {
        ItemsBLL itemsBLL = new ItemsBLL(); 
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            long itemId = (long)value;
            string itemName = itemsBLL.GetItem(itemId).ItemName;

            return itemName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return string.Empty;
        }
    }
}
