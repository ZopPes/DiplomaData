using System.Data.Linq;

namespace DiplomaData.Tabs.TabTable
{
    public class TabDiploma : TabTable<Model.Diploma>
    {
        public TabDiploma(Table<Model.Diploma> diploma) : base(diploma, "Диплом")
        {
        }
    }
}