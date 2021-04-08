using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaData.Tabs.CommandColection
{
    class CommandCollection : ObservableCollection<AddCommand>
    {
        private Tabs Tabs { get; }

        public CommandCollection(Tabs tabs)
        {
            Tabs = tabs;
        }

        public void Add(Tab item)
        {
            Add(new AddCommand(() => Tabs.Add(item), item.Name));
        }
    }
}
