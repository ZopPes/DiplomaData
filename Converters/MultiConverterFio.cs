using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DiplomaData.Converters
{
    

    class MultiConverterFio : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            
            return string.Join(" ",values);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            var rez = Regex.Replace(value.ToString().Trim(), @"\s+", " ");
            var countrez = rez.Count(c => c == ' ');

            if (countrez == 1)
            {
                return (rez+" ").Split(' ');
            }
            if (countrez == 2)
            {
                return rez.Split(' ');
            }
            
            return null;
        }
    }
}
