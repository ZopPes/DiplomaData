using System.Collections.ObjectModel;
using System.ComponentModel;

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

        /// <summary>
        /// Список вкладок
        /// </summary>
        public Tabs() : base()
        {
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
                tab.AClose = a => Remove(a);
            }
            SelectedItem = tab;
        }
        
    }
}