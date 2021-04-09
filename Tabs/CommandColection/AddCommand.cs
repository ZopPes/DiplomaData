using System;
using WPFMVVMHelper;

namespace DiplomaData.Tabs.CommandColection
{
    /// <summary>
    /// Команда для добавления
    /// </summary>
    public class AddCommand : lamdaCommand
    {
        /// <summary>
        /// имя метода
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Команда для добавления
        /// </summary>
        /// <param name="Execute">выполняемый метод</param>
        /// <param name="name">имя метода</param>
        /// <param name="CenExecute">условия выполнения</param>
        public AddCommand(Action Execute, string name, Func<bool> CenExecute = null) : base(Execute, CenExecute) => Name = name;
    }
}