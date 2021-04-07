using DiplomaData.Model;
using DiplomaData.Tabs.TabTable;
using System.Data.Linq;

namespace DiplomaData.Tabs
{
    public class TabCommission : TabTable<Commission>
    {
        public TabCommission(Table<Commission> commissions) : base(commissions, name: "коммисия")
        {
        }
    }
}