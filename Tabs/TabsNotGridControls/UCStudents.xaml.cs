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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DiplomaData.Tabs.TabsNotGridControls
{
    /// <summary>
    /// Логика взаимодействия для UCStudents.xaml
    /// </summary>
    public partial class UCStudents : UserControl
    {

        public static readonly DependencyProperty DataProperty = DependencyProperty.Register
               (
           "Data"
           ,
           typeof(DiplomaDataDataContext)
           ,
           typeof(UCStudents)
               );

        public DiplomaDataDataContext Data
        {
            get => GetValue(DataProperty) as DiplomaDataDataContext;
            set => SetValue(DataProperty, value);
        }

        public UCStudents()
        {
            InitializeComponent();
        }
    }
}
