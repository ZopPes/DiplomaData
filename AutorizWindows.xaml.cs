using DiplomaData.Model;
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

namespace DiplomaData
{
    /// <summary>
    /// Логика взаимодействия для AutorizWindows.xaml
    /// </summary>
    public partial class AutorizWindows : Window
    {
        public AutorizWindows()
        {
            InitializeComponent();
        }

        private void Autoriz_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DiplomasDataContext context = new DiplomasDataContext
                    (@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Diplomas;"+
                            $"User ID={login.Text};Password={password.Password}");
                var form= context.Form_of_education_rus.First();
                Hide();
                new MainWindow().Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
