using System.Data.Linq;
using WPFMVVMHelper;

namespace DiplomaData.импорт
{
    public class ImportVM : peremlog
    {
        #region Table

        private Table<object> table;

        /// <summary>таблица данных</summary>
        public Table<object> Table { get => table; set => Set(ref table, value); }

        #endregion Table

        #region Select

        private object select;

        /// <summary>выбранные табличные банные</summary>
        public object Select { get => select; set => Set(ref select, value); }

        #endregion Select
    }
}