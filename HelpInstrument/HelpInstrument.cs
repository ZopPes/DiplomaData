using System;
using System.ComponentModel;

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
        [Description("")]
        off
            ,
        [Description("⇑")]
        desc
            ,
        [Description("⇓")]
        asc
    }

    public interface ISortInstument : IHelpInstrument
    {
        SortStatus Status { get; set; }

        event EventHandler<SortStatus> StatusChenget;
    }
}