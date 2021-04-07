using DiplomaData.Model;
using System.Data.Linq;

namespace DiplomaData.Tabs.TabTable
{
    internal class TabLecturer : TabTable<Lecturer>
    {
        public TabLecturer(Table<Lecturer> lecturers) : base(lecturers, name: "преподаватели")
        {
        }
    }
}