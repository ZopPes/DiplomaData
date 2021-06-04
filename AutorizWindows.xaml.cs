using DiplomaData.Model;
using System;
using System.Linq;
using System.Windows;

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
                    (@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Diplomas;" +
                            $"User ID={login.Text};Password={password.Password}");
                var form = context.Form_of_education_rus.First();
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