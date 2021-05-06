using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFMVVMHelper;

namespace DiplomaData.HelpInstrument.Sort
{
    public class SortProp : ISortInstument
    {

        #region Status
        private SortStatus status;
        /// <summary>Состояние сортировки</summary>
        public SortStatus Status { get => status; set { status = value; StatusChenget.Invoke(this, value); } }
        #endregion

        public string Name { get; }

        public event EventHandler<SortStatus> StatusChenget;

        public SortProp(string name)
        {
            Name = name;
        }
    }
}
