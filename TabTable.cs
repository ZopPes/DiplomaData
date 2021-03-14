using DiplomaData.Model;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaData
{


    public class TabTable : Tab
    {


        #region Table
        private Table<Thesis> table;
        /// <summary>Таблица данных</summary>
        public Table<Thesis> Table { get => table; set =>Set(ref table ,value); }
        #endregion



        public TabTable(Table<Thesis> theses)
        {
            Table = theses;
            Name = "Темы";
        }



    }
}
