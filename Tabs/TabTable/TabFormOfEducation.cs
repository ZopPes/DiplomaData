using System.Data.Linq;

namespace DiplomaData.Tabs.TabTable
{
    public class TabFormOfEducation : TabTable<Model.Form_of_education>
    {
        public TabFormOfEducation(Table<Model.Form_of_education> form_Of_Educations) : base(form_Of_Educations, name: "форма обучения")
        {
        }
    }
}