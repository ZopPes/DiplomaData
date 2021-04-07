using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WPFMVVMHelper;

namespace DiplomaData
{
    public class Tab : peremlog
    {
        /// <summary>
        /// Класс имя значения
        /// для списка свойств
        /// </summary>
        public class Propertie : peremlog
        {
            /// <summary>имя</summary>
            public string Name { get; set; }


            public object Value { get => Func?.Invoke();}

            public Func<object> Func { get; }

            public Propertie(string name,Func<object> value)
            {
                Name = name;
                Func = value;
            }

            public void ValueOnPropertyChanged()
            {
                OnPropertyChanged(nameof(Value));
            }
        }

        #region Properties

        private ObservableCollection<Propertie> properties;

        /// <summary>Войства объекта</summary>
        public ObservableCollection<Propertie> Properties
        {
            get => properties;
            set => Set(ref properties, value);
        }

        #endregion Properties

        /// <summary>
        /// класс для кправления вкладками приложения
        /// </summary>
        public Tab(string name = "")
        {
            Close = new lamdaCommand(() => AClose?.Invoke(this));
            Name = name;
            Properties = new ObservableCollection<Propertie>();
        }


       

        /// <summary>
        /// название вкладки
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// комманда для закрытия вкладки
        /// </summary>
        public ICommand Close { get; }

        public Action<Tab> AClose;
    }
}