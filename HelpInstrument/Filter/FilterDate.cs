using System;

namespace DiplomaData.HelpInstrument.Filter
{
    public class FilterDate : IHelpInstrumentArg
    {
        #region Date1

        private DateTime date1;

        /// <summary>MyComment</summary>
        public DateTime Date1 { get => date1; set { date1 = value; SelectedChenget?.Invoke(this, (date1, date2)); } }

        #endregion Date1

        #region Date2

        private DateTime date2;

        /// <summary>MyComment</summary>
        public DateTime Date2 { get => date2; set { date2 = value; SelectedChenget?.Invoke(this, (date1, date2)); } }

        #endregion Date2

        public event EventHandler<(DateTime, DateTime)> SelectedChenget;

        public FilterDate()
        {
            date1 = DateTime.Today;
            date2 = DateTime.Now;
        }
    }
}