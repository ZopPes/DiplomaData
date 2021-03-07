using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Office.Interop.Word;

namespace DiplomaData.Отчёты
{
    /// <summary>
    /// Логика взаимодействия для WordTemplate.xaml
    /// </summary>
    public partial class WordTemplate : System.Windows.Window
    {
        public WordTemplate()
        {
            InitializeComponent();
            
            DataContextChanged += WordTemplate_DataContextChanged;
        }

        private void WordTemplate_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            
            foreach (Microsoft.Office.Interop.Word.ContentControl item in e.NewValue as ContentControls)
            {
                sp.Children.Add(new Label() { Content = item.Range.Text });
            }
        }
    }
}
