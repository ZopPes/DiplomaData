using DiplomaData.Model;
using DiplomaData.Tabs.TabTable;
using System.Windows;
using System.Windows.Controls;

namespace DiplomaData.Tabs.TabsControls
{
    /// <summary>
    /// Логика взаимодействия для UCTabGroup.xaml
    /// </summary>
    public partial class UCTabGroup : UserControl
    {
        public UCTabGroup()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TableProperty = DependencyProperty.Register
             (
         "Table"
         ,
         typeof(TabGroup)
         ,
         typeof(UCTabGroup)
             );

        public TabGroup Table
        {
            get => GetValue(TableProperty) as TabGroup;
            set => SetValue(TableProperty, value);
        }

        public static readonly DependencyProperty DataProperty = DependencyProperty.Register
               (
           "Data"
           ,
           typeof(DiplomaDataDataContext)
           ,
           typeof(UCTabGroup)
               );

        public DiplomaDataDataContext Data
        {
            get => GetValue(DataProperty) as DiplomaDataDataContext;
            set => SetValue(DataProperty, value);
        }
    }
}