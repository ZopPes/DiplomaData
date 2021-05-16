using DiplomaData.HelpInstrument;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using WPFMVVMHelper;

namespace DiplomaData
{
    /// <summary>
    /// Класс имя значения
    /// для списка свойств
    /// </summary>
    public class Property : peremlog
    {
        /// <summary>имя</summary>
        public string Name { get; }

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
            Func = () => value;
        }

        /// <summary>
        /// обновление значения
        /// </summary>
        public void ValueOnPropertyChanged() => OnPropertyChanged(nameof(Value));
    }
    /// <summary>
    /// вкладка
    /// </summary>
    public class Tab : peremlog
    {
        #region isVisible

        private Visibility visibility = Visibility.Collapsed;

        /// <summary>Отображение элемента</summary>
        public Visibility IsVisible { get => visibility; set => Set(ref visibility, value); }

        #endregion isVisible


        #region Properties

        /// <summary>Свойства Вкладки</summary>
        public ObservableCollection<Property> Properties { get; }

        #endregion Properties

        /// <summary>
        /// название вкладки
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// комманда для закрытия вкладки
        /// </summary>

        #region Filter

        private string filter;

        /// <summary>Фильтер</summary>
        public string Filter
        {
            get => filter;
            set
            {
                Set(ref filter, value);
                FilterChanged?.Invoke(this, value ?? "");
            }
        }

        #endregion Filter

        public List<IHelpInstrument> FilterParam { get; set; }
        public List<ISortInstument> SortParam { get; set; }

        public event EventHandler<string> FilterChanged;

        /// <summary>
        /// Вкладка
        /// </summary>
        /// <param name="name">Имя Вкладки</param>
        public Tab(string name = "")
        {
            Name = name;
            Properties = new ObservableCollection<Property>();
            FilterParam = new List<IHelpInstrument>();
            SortParam = new List<ISortInstument>();
        }

        public void OnProperties()
        {
            foreach (Property property in Properties)
                property.ValueOnPropertyChanged();
        }
    }
}