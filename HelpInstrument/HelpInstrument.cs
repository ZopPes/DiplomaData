using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaData.HelpInstrument
{
    public interface IHelpInstrument
    {
        string Name { get; }
    }


    public interface IHelpInstrumentArg
    {

    }

    public interface IHelpInstrumentFilter : IHelpInstrumentArg
    {
        object Select { get; set; }

        event EventHandler<object> SelectedChenget;
    }

    public enum SortStatus
    {
        off,desc,asc
    }

    public interface ISortInstument : IHelpInstrument
    {
        SortStatus Status { get; set; }

        event EventHandler<SortStatus> StatusChenget;

    }
}
