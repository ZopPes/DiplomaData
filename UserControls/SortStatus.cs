using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using Sort = DiplomaData.HelpInstrument.SortStatus;
namespace DiplomaData.UserControls
{
    public static class Ex
    {
        public static string GetPs(this Enum value)
        {
            var attributes = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Any())
                return (attributes.First() as DescriptionAttribute).Description;

            // If no description is found, the least we can do is replace underscores with spaces
            // You can add your own custom default formatting logic here
            var ti = CultureInfo.CurrentCulture.TextInfo;
            return ti.ToTitleCase(ti.ToLower(value.ToString().Replace("_", " ")));
        }

    }

    class SortStatus : ComboBox
    {
        public SortStatus() : base()
        {
            ItemsSource = Enum.GetValues(typeof(Sort)).Cast<Enum>().Select(e => new { sort = e, name = e.GetPs() });
            DisplayMemberPath = "name";
            SelectedValuePath = "sort";
        }

    }
}
