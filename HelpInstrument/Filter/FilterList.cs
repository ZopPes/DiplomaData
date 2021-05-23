using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DiplomaData.HelpInstrument.Filter
{
    public class FilterList : FilterProp,IHelpInstrumentFilter
    {
        public IEnumerable InData { get; }

        public event EventFilter<object, object> SelectedChenget;

        #region Select

        private object select;

        /// <summary>Выбранный элемент</summary>
        public object Select { get => select; set { select = value; SelectedChenget.Invoke(this, Select); } }

        #endregion Select

        public FilterList(IEnumerable inData, string name) : base(name)
        {
            InData = inData;
        }
    }

    public class FilterList<Tin,T> : FilterList
    {

       
        public FilterList(IEnumerable<T> inData, string name
            , Action<T> p) : base(inData, name)
        {
            SelectedChenget += (o, t) => p?.Invoke((T)t);
        }
    }
}