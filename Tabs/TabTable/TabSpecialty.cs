using System.Data.Linq;

namespace DiplomaData.Tabs.TabTable
{
    public class TabSpecialty : TabTable<Model.Specialty>
    {
        public TabSpecialty(Table<Model.Specialty> specialties) : base(specialties, name: "специальность")
        {
        }
    }
}