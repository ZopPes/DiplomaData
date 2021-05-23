using System;

namespace DiplomaData.HelpInstrument.Filter
{
    public class FilterBool : FilterProp, IHelpInstrumentFilter
    {
        #region Select

        private bool select;

        /// <summary>MyComment</summary>
        public bool Select { get => select; set { select = value; SelectedChenget.Invoke(this, Select); } }

        #endregion Select

        public event EventFilter<object, object> SelectedChenget;

        public FilterBool(string name,Action<bool> action):base(name)
        {
            SelectedChenget += (o, b) => action?.Invoke((bool)b);
        }

    }
}