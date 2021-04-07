using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DiplomaData
{
    /// <summary>
    /// Логика взаимодействия для UCTopic.xaml
    /// </summary>
    public partial class UCTopic : UserControl
    {
        //public static readonly DependencyProperty TopicProperty = DependencyProperty.Register
        //        (
        //    "Topic"
        //    ,
        //    typeof(object)
        //    ,
        //    typeof(UCTopic)
        //        );

        //public Topic Topic { get => GetValue(TopicProperty) as Topic; set => SetValue(TopicProperty, value); }

        public UCTopic()
        {
            InitializeComponent();
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Test");
        }

        private void userControl_GotFocus_1(object sender, RoutedEventArgs e)
        {
        }
    }
}