using DiplomaData.Model;
using System.Windows;
using System.Windows.Controls;

namespace DiplomaData.Tabs.TabsControls
{
    /// <summary>
    /// Логика взаимодействия для UCTabStudent.xaml
    /// </summary>
    public partial class UCTabStudent : UserControl
    {
        public static readonly DependencyProperty DataProperty = DependencyProperty.Register
                (
            "Data"
            ,
            typeof(DiplomaDataDataContext)
            ,
            typeof(UCTabStudent)
                );

        public DiplomaDataDataContext Data
        {
            get => GetValue(DataProperty) as DiplomaDataDataContext;
            set => SetValue(DataProperty, value);
        }

        public UCTabStudent()
        {
            InitializeComponent();
        }
    }
}