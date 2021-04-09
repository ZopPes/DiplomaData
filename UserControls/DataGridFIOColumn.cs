using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DiplomaData.UserControls
{
    class DataGridFIOColumn : DataGridTextColumn
    {
        #region Surname

        public static readonly DependencyProperty SurnameProperty = DependencyProperty.Register
            (
        "Surname"
        ,
        typeof(string)
        ,
        typeof(DataGridFIOColumn)
            );

        public string Surname
        {
            get => GetValue(SurnameProperty) as string;
            set => SetValue(SurnameProperty, value);
        }
        #endregion

        #region Name
        public static readonly DependencyProperty NameProperty = DependencyProperty.Register
            (
        "Name"
        ,
        typeof(string)
        ,
        typeof(DataGridFIOColumn)
            );

        public string Name
        {
            get => GetValue(NameProperty) as string;
            set => SetValue(NameProperty, value);
        }
        #endregion

        #region Patronymic
        public static readonly DependencyProperty PatronymicProperty = DependencyProperty.Register
            (
        "Patronymic"
        ,
        typeof(string)
        ,
        typeof(DataGridFIOColumn)
            );

        public string Patronymic
        {
            get => GetValue(PatronymicProperty) as string;
            set => SetValue(PatronymicProperty, value);
        }
        #endregion

        public DataGridFIOColumn():base()
        {
               
        }
    }
}
