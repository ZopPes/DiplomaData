using DiplomaData.Model;
using System.Data.Linq;

namespace DiplomaData.Tabs.TabTable
{
    public class TabReviewer : TabTable<Reviewer>
    {
        public TabReviewer(Table<Reviewer> reviewers) : base(reviewers, name: "рецензенты")
        {
        }
    }
}