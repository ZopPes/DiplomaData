using DiplomaData.Model;
using DiplomaData.Tabs.TabReport;
using DiplomaData.Tabs.TabTable;
using System;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //// Create OpenFileDialog
            //OpenFileDialog dlg = new OpenFileDialog();

            //// Set filter for file extension and default file extension
            //dlg.DefaultExt = ".doc";
            //dlg.Filter = "Word documents (.doc)|*.doc";
            //// Display OpenFileDialog by calling ShowDialog method

            //bool? result = dlg.ShowDialog();

            //// Get the selected file name and display in a TextBox
            //if (result == true)
            //{
            //    if (dlg.FileName.Length > 0)
            //    {
            //        string newXPSDocumentName = String.Concat(System.IO.Path.GetDirectoryName(dlg.FileName), "\\",
            //                       System.IO.Path.GetFileNameWithoutExtension(dlg.FileName), ".xps");

            //        // Set DocumentViewer.Document to XPS document
            //        ggg.Document =

            //            ConvertWordDocToXPSDoc(dlg.FileName, newXPSDocumentName);

            //    }

            //}
        }

        private void DatePicker_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
                (sender as DatePicker).SelectedDate = DateTime.Now;
            }
        }

        private void list_Loaded(object sender, RoutedEventArgs e)
        {
            var qwe = sender;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (var i in e.Text)
            {
                e.Handled |= !char.IsLetter(i);
            }
        }

        public class TemplateSelector : DataTemplateSelector
        {
            public override DataTemplate SelectTemplate(object item, DependencyObject container)
            {
                //получаем вызывающий контейнер
                FrameworkElement element = container as FrameworkElement;

                if (element != null && item != null && item is int)
                {
                    int currentItem = 0;

                    int.TryParse(item.ToString(), out currentItem);
                    //в зависимости от того, какой вариант выбран, возвращаем конкретный шаблон
                    if (currentItem == 0)
                        return element.FindResource("ButtonTemplate") as DataTemplate;
                    if (currentItem == 1)
                        return element.FindResource("TextBlockTemplate") as DataTemplate;
                    if (currentItem == 2)
                        return element.FindResource("RadioButtonsTemplate") as DataTemplate;
                }
                return null;
            }
        }

        private void TextBox_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsDigit(e.Text[0]);
        }

        private void TextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            DiplomaData.Properties.Settings.Default.Save();
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = e.Key == Key.Space;
        }

        private void AnimatedExpander_Drop(object sender, DragEventArgs e)
        {
            var con = ((Control)sender).DataContext;
            Diplom_rus diplom = (Diplom_rus)con;
            diplom.MyProperty = (string)e.Data.GetData(DataFormats.Text);
        }

        private void DiplomaReportList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox list = (ListBox)sender;
            TabTable report = (TabTable)list.DataContext;
            report.SelectedItems = list.SelectedItems;
        }

        //private IDocumentPaginatorSource ConvertWordDocToXPSDoc(string wordDocName, string xpsDocName)
        //{
        //    Microsoft.Office.Interop.Word.Application
        //        wordApplication = new Microsoft.Office.Interop.Word.Application();

        //    Document doc = wordApplication.Documents.Add(wordDocName);
        //    try
        //    {
        //        doc.SaveAs(xpsDocName, WdSaveFormat.wdFormatXPS);

        //        wordApplication.Quit();
        //        XpsDocument xpsDoc = new XpsDocument(xpsDocName, System.IO.FileAccess.Read);

        //        return xpsDoc.GetFixedDocumentSequence();
        //    }
        //    catch (Exception exp)
        //    {
        //        string str = exp.Message;
        //    }
        //    return null;
        //}
    }
}