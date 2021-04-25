using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WPFMVVMHelper;

namespace DiplomaData
{
    /// <summary>
    /// вкладка
    /// </summary>
    public class Tab : peremlog
    {
        /// <summary>
        /// Класс имя значения
        /// для списка свойств
        /// </summary>
        public class Property : peremlog
        {
            /// <summary>имя</summary>
            public string Name { get;}

            /// <summary>
            /// значение
            /// </summary>
            public object Value { get => Func?.Invoke(); }

            /// <summary>
            /// Функция для получения значения
            /// </summary>
            public Func<object> Func { get; }

            /// <summary>
            /// свойство вкладки
            /// </summary>
            /// <param name="name">имя свойства</param>
            /// <param name="value">функция для получения значения</param>
            public Property(string name, Func<object> value)
            {
                Name = name;
                Func = value;
            }

            /// <summary>
            /// свойство вкладки
            /// </summary>
            /// <param name="name">имя свойства</param>
            /// <param name="value">значение</param>
            public Property(string name, object value)
            {
                Name = name;
                Func =()=> value;
            }
            /// <summary>
            /// обновление значения
            /// </summary>
            public void ValueOnPropertyChanged() => OnPropertyChanged(nameof(Value));
        }

        #region Properties
        /// <summary>Свойства Вкладки</summary>
        public ObservableCollection<Property> Properties { get; }
        #endregion Properties

        /// <summary>
        /// название вкладки
        /// </summary>
        public string Name { get;}

        /// <summary>
        /// комманда для закрытия вкладки
        /// </summary>
        public ICommand Close { get; }


        #region Filter
        private string filter;
        /// <summary>Фильтер</summary>
        public string Filter 
        {
            get => filter; 
            set 
            {
                Set(ref filter, value);
                    FilterChanged?.Invoke(this,value);
            } 
        }
        #endregion



        public event EventHandler<string> FilterChanged;

        public Action<Tab> AClose;

        /// <summary>
        /// Вкладка
        /// </summary>
        /// <param name="name">Имя Вкладки</param>
        public Tab(string name = "")
        {
            Close = new lamdaCommand(() => AClose?.Invoke(this));
            Name = name;
            Properties = new ObservableCollection<Property>();
        }

        

        public void OnProperties()
        {
            foreach (Property property in Properties)
                property.ValueOnPropertyChanged();
        }
    }
}