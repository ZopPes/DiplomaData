using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Sort= DiplomaData.HelpInstrument.SortStatus;
namespace DiplomaData.UserControls
{
    class SortStatus : ComboBox
    {
        public SortStatus() : base()
            => ItemsSource = Enum.GetValues(typeof(Sort)).Cast<Sort>();
    }
}
