using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaData.HelpInstrument.Filter
{
    public class FilterList:IHelpInstrumentFilter
    {
        
        public IEnumerable InData { get; }

        public event EventHandler<object> SelectedChenget;

        #region Select
        private object select;

        /// <summary>Выбранный элемент</summary>
        public object Select { get => select; set { select = value; SelectedChenget.Invoke(this,Select); } }
        #endregion

        public FilterList(IEnumerable inData)
        {
            InData = inData;
        }
    }
}
