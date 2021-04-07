using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DiplomaData.импорт
{
    /// <summary>
    /// Логика взаимодействия для UCImport.xaml
    /// </summary>
    public partial class UCImport : UserControl
    {
        public UCImport()
        {
            InitializeComponent();
        }

        private void import_Drop(object sender, DragEventArgs e)
        {
            DropCsv(e.Data, null);
        }

        private void DropCsv(IDataObject data, ItemCollection items)
        {
            items.Clear();

            foreach (var item in data.GetFormats())
            {
                try
                {
                    items.Add(item);
                    items.Add(data.GetData(item));
                }
                catch { }
            }
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DropCsv(Clipboard.GetDataObject(), null);
        }

        private void import_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}