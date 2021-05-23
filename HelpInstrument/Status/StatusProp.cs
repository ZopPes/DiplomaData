using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFMVVMHelper;

namespace DiplomaData.HelpInstrument.Status
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
}
