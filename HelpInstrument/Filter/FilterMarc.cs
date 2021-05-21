using System;

namespace DiplomaData.HelpInstrument.Filter
{
    public class FilterMarc : IHelpInstrumentFilter
    {
        #region Select

        private object select;

        /// <summary>значение фильтрации</summary>
        public object Select { get => select; set { select = value; SelectedChenget.Invoke(this, Select); } }

        #endregion Select

        public event EventHandler<object> SelectedChenget;

        public FilterMarc()
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

        public FilterMarcChar()
        {
        }
    }
}