using System.Windows.Controls;
using System.Windows.Input;

namespace DiplomaData.Tabs.TabsControls
{
    /// <summary>
    /// Логика взаимодействия для UCTabSpecialty.xaml
    /// </summary>
    public partial class UCTabSpecialty : UserControl
    {
        public UCTabSpecialty()
        {
            InitializeComponent();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            for (int i = e.Text.Length - 1; i >= 0; i--)
            {
                if (!(char.IsNumber(e.Text[i]) || char.IsWhiteSpace(e.Text[i])))
                {
                    e.Handled = true;
                    return;
                }
            }
        }
    }
}