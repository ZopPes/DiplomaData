using System;
using WPFMVVMHelper;

namespace DiplomaData.HelpInstrument.Status
{
    /// <summary>
    /// Класс имя значения
    /// для списка свойств
    /// </summary>
    public class SatusProp : peremlog
    {
        /// <summary>имя</summary>
        public string Name { get; }

        /// <summary>
        /// значение
        /// </summary>
        public object Value => Func?.Invoke();

        /// <summary>
        /// Функция для получения значения
        /// </summary>
        public Func<object> Func { get; }

        /// <summary>
        /// свойство вкладки
        /// </summary>
        /// <param name="name">имя свойства</param>
        /// <param name="value">функция для получения значения</param>
        public SatusProp(string name, Func<object> value)
        {
            Name = name;
            Func = value;
        }

        /// <summary>
        /// свойство вкладки
        /// </summary>
        /// <param name="name">имя свойства</param>
        /// <param name="value">значение</param>
        public SatusProp(string name, object value)
        {
            Name = name;
            Func = () => value;
        }

        /// <summary>
        /// обновление значения
        /// </summary>
        public void ValueOnPropertyChanged() => OnPropertyChanged(nameof(Value));
    }
}