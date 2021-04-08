using DiplomaData.Tabs.CommandColection;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace DiplomaData.Tabs
{
    public class Tabs : ObservableCollection<Tab>, INotifyPropertyChanged
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

        /// <summary>Список команд</summary>
        public ObservableCollection<ICommand> CommandCollection { get; set; }

        public Tabs() : base()
        {
            CommandCollection = new ObservableCollection<ICommand>();
        }

        public Tabs(IEnumerable<AddCommand> commands) : base()
        {
            CommandCollection = new ObservableCollection<ICommand>(commands);
        }

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