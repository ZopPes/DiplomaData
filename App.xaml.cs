using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;

namespace DiplomaData
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
    }
}

namespace System.Windows.Controls
{
    public class CipherConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = value?.ToString() ?? string.Empty;
            if (v.Length < 6)
            {
                v = v.PadRight(6, '0');
            }
            if (v.Length > 6)
            {
                v = v.Remove(6);
            }
            return v.Insert(2, ".")
                .Insert(5, ".");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString().Replace(".", "");
        }
    }

    public class NumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Regex.Replace(value?.ToString() ?? string.Empty, @"[\D]", "0");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Regex.Replace(value?.ToString() ?? string.Empty, @"[\D]", "0");
        }
    }
}