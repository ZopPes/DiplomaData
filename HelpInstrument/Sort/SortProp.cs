using System;

namespace DiplomaData.HelpInstrument.Sort
{
    public class SortProp : ISortInstument
    {
        #region Status

        private SortStatus status;

        /// <summary>Состояние сортировки</summary>
        public SortStatus Status { get => status; set { status = value; StatusChenget.Invoke(this, value); } }

        #endregion Status

        public string Name { get; }

        public event EventHandler<SortStatus> StatusChenget;

        public SortProp(string name)
        {
            Name = name;
        }
    }
}