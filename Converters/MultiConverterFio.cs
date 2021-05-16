using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace DiplomaData.Converters
{
    public class MultiConverterFio : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) =>
            string.Join(" ", values);

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            var rez = Regex.Replace(value.ToString().Trim(), @"\s+", " ");
            var countrez = rez.Count(c => c == ' ');
            
            if (countrez == 1) return (rez + " ").Split(' ');
            if (countrez == 2) return rez.Split(' ');
            return null;
        }
    }
}