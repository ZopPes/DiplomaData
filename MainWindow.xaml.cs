using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DiplomaData
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
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