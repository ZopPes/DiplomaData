using System;

namespace DiplomaData.HelpInstrument.Filter
{
    public class FilterBool : IHelpInstrumentArg
    {
        #region Select

        private bool select;

        /// <summary>MyComment</summary>
        public bool Select { get => select; set { select = value; SelectedChenget.Invoke(this, Select); } }

        #endregion Select

        public event EventHandler<bool> SelectedChenget;

        public FilterBool()
        {
        }
    }
}