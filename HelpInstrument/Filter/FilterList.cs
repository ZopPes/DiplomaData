using System;
using System.Collections;

namespace DiplomaData.HelpInstrument.Filter
{
    public class FilterList : IHelpInstrumentFilter
    {
        public IEnumerable InData { get; }

        public event EventHandler<object> SelectedChenget;

        #region Select

        private object select;

        /// <summary>Выбранный элемент</summary>
        public object Select { get => select; set { select = value; SelectedChenget.Invoke(this, Select); } }

        #endregion Select

        public FilterList(IEnumerable inData)
        {
            InData = inData;
        }
    }
}