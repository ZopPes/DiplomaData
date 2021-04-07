using System.Data.Linq;

namespace DiplomaData.Tabs.TabTable
{
    public class TabGroup : TabTable<Model.Group>
    {
        public TabGroup(Table<Model.Group> groups) : base(groups, name: "группы")
        {
        }
    }
}