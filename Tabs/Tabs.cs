using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using WPFMVVMHelper;
using System.Linq;


namespace DiplomaData.Tabs
{
    /// <summary>
    /// Список вкладок
    /// </summary>
    public class Tabs : ObservableCollection<Tab>
    {
        #region SelectedItem

        private Tab selectedTab;

        /// <summary>Выбранная вкладка</summary>
        public Tab SelectedItem
        {
            get => selectedTab; set
            {
                selectedTab = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(SelectedItem)));
            }
        }

        #endregion SelectedItem

        public ICommand Visible { get; }

        public ICommand Collapsed { get; }

        /// <summary>
        /// Список вкладок
        /// </summary>
        public Tabs() : base()
        {
            Visible = new lamdaCommand<Tab>(obj=> 
            { obj.IsVisible = Visibility.Visible; SelectedItem = obj; });
            Collapsed = new lamdaCommand<Tab>(obj=>
            { obj.IsVisible = Visibility.Collapsed; SelectedItem = this.LastOrDefault(t=>t.IsVisible==Visibility.Visible); });
        }

        

        /// <summary>
        /// добавляет новую вкладку
        /// </summary>
        /// <param name="tab">Вкладка</param>
        public new void Add(Tab tab)
        {
            if (!Contains(tab))
            {
                base.Add(tab);
            }
            SelectedItem = tab;
        }
        

    }
}