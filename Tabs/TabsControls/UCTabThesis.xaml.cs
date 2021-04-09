using DiplomaData.Tabs.TabTable;
using System.Windows;
using System.Windows.Controls;

namespace DiplomaData.Tabs.TabsControls
{
    /// <summary>
    /// Логика взаимодействия для UCTabThesis.xaml
    /// </summary>
    public partial class UCTabThesis : UserControl
    {
        public static readonly DependencyProperty ThesisProperty = DependencyProperty.Register
                (
            "Thesis"
            ,
            typeof(TabThesis)
            ,
            typeof(UCTabThesis)
                );

        public TabThesis Thesis { get => GetValue(ThesisProperty) as TabThesis; set => SetValue(ThesisProperty, value); }

        public UCTabThesis()
        {
            InitializeComponent();
        }
    }
}