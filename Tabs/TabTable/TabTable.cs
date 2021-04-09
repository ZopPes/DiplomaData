using System.Data.Linq;
using System.Linq;

namespace DiplomaData.Tabs.TabTable
{
    /// <summary>
    /// Вкладка с таблицей
    /// </summary>
    /// <typeparam name="T">тип для таблицы</typeparam>
    public class TabTable<T> : Tab where T : class 
    {
        #region Table
        /// <summary>Таблица данных</summary>
        public Table<T> Table { get;}
        #endregion Table

        /// <summary>
        /// Вкладка с таблицей
        /// </summary>
        /// <param name="table">таблица данных</param>
        /// <param name="name">название вкладки</param>
        public TabTable(Table<T> table, string name = "") : base(name)
        {
            Table = table;
            Properties.Add(new Property("Количество строк", () => Table.Count()));
        }
    }
}