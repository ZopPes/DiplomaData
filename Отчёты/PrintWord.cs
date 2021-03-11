using System.Collections.ObjectModel;
using WPFMVVMHelper;

namespace DiplomaData.Отчёты
{
    internal class PrintWord : peremlog
    {

        #region Controls
        private ObservableCollection<Control> controls;
        /// <summary>список контролов</summary>
        public ObservableCollection<Control> Controls { get => controls; set => Set(ref controls ,value); }
        #endregion
    }
}