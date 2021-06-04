using DiplomaData.Properties;
using DiplomaData.Tabs.TabTable;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DiplomaData
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (var i in e.Text)
                e.Handled |= !char.IsLetter(i);
        }

        private void TextBox_PreviewTextInput_1(object sender, TextCompositionEventArgs e) =>
            e.Handled = !char.IsDigit(e.Text[0]);

        private void TextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e) =>
            Settings.Default.Save();

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e) =>
            e.Handled = e.Key == Key.Space;

        private void DiplomaReportList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox list = (ListBox)sender;
            TabTable report = (TabTable)list.DataContext;
            report.SelectedItems = list.SelectedItems;
        }
    }
}