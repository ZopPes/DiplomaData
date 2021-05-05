using System;
using System.Linq;

namespace DiplomaData.Tabs.TabTable
{
    public interface IFilterEventQuery
    {
        IQueryable Queryable { get; }
    }
    public class FilterEventQuery : IFilterEventQuery
    {
        public IQueryable Queryable { get; }

        public FilterEventQuery(IQueryable queryable)
        {
            Queryable = queryable;
        }
    }
    


    public interface IFilterParam
    {
        string Name { get; }

        event EventHandler<IFilterEventQuery> FilterChanged;

    }
}