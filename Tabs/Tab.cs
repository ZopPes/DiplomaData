﻿using DiplomaData.HelpInstrument;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using WPFMVVMHelper;
using DiplomaData.HelpInstrument.Status;

namespace DiplomaData
{
    
    /// <summary>
    /// вкладка
    /// </summary>
    public class Tab : peremlog
    {
        #region isVisible

        private Visibility visibility = Visibility.Collapsed;

        /// <summary>Отображение элемента</summary>
        public Visibility IsVisible { get => visibility; set => Set(ref visibility, value); }

        #endregion isVisible



        /// <summary>
        /// название вкладки
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// комманда для закрытия вкладки
        /// </summary>

        #region Filter

        private string filter;

        /// <summary>Фильтер</summary>
        public string Filter
        {
            get => filter;
            set
            {
                Set(ref filter, value);
                FilterChanged?.Invoke(this, value ?? "");
            }
        }

        #endregion Filter

        public Collection<IHelpInstrumentArg> FilterParams { get; }
        public Collection<ISortInstument> SortParams { get; }
        public Collection<object> StatusProps { get; }

        public event EventHandler<string> FilterChanged;

        /// <summary>
        /// Вкладка
        /// </summary>
        /// <param name="name">Имя Вкладки</param>
        public Tab(string name = "")
        {
            Name = name;
            FilterParams = new Collection<IHelpInstrumentArg>();
            SortParams = new Collection<ISortInstument>();
            StatusProps = new Collection<object>();
            
        }

     
    }
}