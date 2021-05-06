using System;
using System.Linq;

namespace DiplomaData.HelpInstrument.Filter
{
    public class FilterMarc : IHelpInstrumentFilter
    {

        #region Select
        private object select;
        /// <summary>MyComment</summary>
        public object Select { get => select; set { select = value; SelectedChenget.Invoke(this, Select); } }
        #endregion


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
        public new string Select { get => select.ToString(); 
            set { select =string.IsNullOrEmpty(value) ? ' ' : value[0]; SelectedChenget.Invoke(this, select); } }

        #endregion


        public new event EventHandler<char> SelectedChenget;
        public FilterMarcChar()
        {
        }

    }
}