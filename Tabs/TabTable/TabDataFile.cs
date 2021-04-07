using System.Data.Linq;

namespace DiplomaData.Tabs.TabTable
{
    public class TabDataFile : TabTable<Model.DataFile>
    {
        public TabDataFile(Table<Model.DataFile> dataFiles) : base(dataFiles, name: "Файлы")
        {
        }
    }
}