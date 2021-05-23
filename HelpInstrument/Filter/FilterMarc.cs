using System;

namespace DiplomaData.HelpInstrument.Filter
{
    public class FilterMarc : FilterProp,IHelpInstrumentFilter
    {
        #region Select

        private string select;

        /// <summary>значение фильтрации</summary>
        public string Select { get => select; set { select = value; SelectedChenget.Invoke(this, Select); } }

        #endregion Select

        public event EventFilter<object, object> SelectedChenget;

        public FilterMarc(string name):base(name)
        {
        }
    }

    public class FilterMarcChar : FilterMarc
    {
        #region Select

        private char select;

        /// <summary>MyComment</summary>
        public new string Select
        {
            get => select.ToString();
            set { select = string.IsNullOrEmpty(value) ? ' ' : value[0]; SelectedChenget.Invoke(this, select); }
        }

        #endregion Select

        public new event EventHandler<char> SelectedChenget;

        public FilterMarcChar(string name):base(name)
        {
        }
    }
}