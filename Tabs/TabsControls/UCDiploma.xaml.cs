using DiplomaData.Model;
using DiplomaData.Tabs.TabTable;
using System.Windows;
using System.Windows.Controls;

namespace DiplomaData.Tabs.TabsControls
{
    /// <summary>
    /// Логика взаимодействия для UCDiploma.xaml
    /// </summary>
    public partial class UCDiploma : UserControl
    {
        public UCDiploma()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TableProperty = DependencyProperty.Register
              (
          "Table"
          ,
          typeof(TabDiploma)
          ,
          typeof(UCDiploma)
              );

        public TabDiploma Table
        {
            get => GetValue(TableProperty) as TabDiploma;
            set => SetValue(TableProperty, value);
        }

        public static readonly DependencyProperty DataProperty = DependencyProperty.Register
                (
            "Data"
            ,
            typeof(DiplomaDataDataContext)
            ,
            typeof(UCDiploma)
                );

        public DiplomaDataDataContext Data
        {
            get => GetValue(DataProperty) as DiplomaDataDataContext;
            set => SetValue(DataProperty, value);
        }
    }
}