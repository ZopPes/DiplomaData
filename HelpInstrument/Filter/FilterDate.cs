using System;
using System.Collections.Generic;

namespace DiplomaData.HelpInstrument.Filter
{
    public class FilterDate : FilterProp,IHelpInstrumentFilter
    {
        #region Date1

        private DateTime date1;

        /// <summary>MyComment</summary>
        public DateTime Date1 { get => date1; set { date1 = value; SelectedChenget?.Invoke(this,(Date1,Date2)); } }

        #endregion Date1

        #region Date2

        private DateTime date2;

        /// <summary>MyComment</summary>
        public DateTime Date2 { get => date2; set { date2 = value; SelectedChenget?.Invoke(this, (Date1, Date2)); } }

        #endregion Date2

        public event EventFilter<object,object> SelectedChenget;

        public FilterDate(string name,Action<(DateTime,DateTime)> action):base(name)
        {
            Date1 = DateTime.Today;
            Date2 = DateTime.Today;
            SelectedChenget += (o, t) =>
            {
                action?.Invoke(((DateTime, DateTime))t);
            };
        }
    }
}