using System.ComponentModel;
using System.Data.Linq;
using System.Linq;
using System.Windows.Data;

namespace DiplomaData.Tabs.TabTable
{
    public class TabTable<T> : Tab where T : class
    {
        #region Table

        private Table<T> table;

        /// <summary>Таблица данных</summary>
        public Table<T> Table { get => table; set => Set(ref table, value); }

        #endregion Table
      
        public TabTable(Table<T> table, string name = "") : base(name)
        {
            Table = table;
            Properties.Add(new Propertie("Количество строк", () => Table.Count()));
        }
    }
}