using System.Collections.ObjectModel;

namespace DiplomaData.Tabs.CommandColection
{
    /// <summary>
    /// Список команд для добавления новой вкладки
    /// </summary>
    public class CommandCollection : ObservableCollection<AddCommand>
    {
        /// <summary>
        /// Изменяемый список вкладок
        /// </summary>
        private Tabs Tabs { get; }

        /// <summary>
        /// Список команд для добавления новой вкладки
        /// </summary>
        /// <param name="tabs">Список вкладок</param>
        public CommandCollection(Tabs tabs) => Tabs = tabs;

        /// <summary>
        /// Создаёт команду для добавления вкладки
        /// </summary>
        /// <param name="item">Вкладка</param>
        public void Add(Tab item) => Add(new AddCommand(() => Tabs.Add(item), item.Name));
    }
}