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

    public delegate void EventFilter<Tout,TEventArgs>(Tout sender, TEventArgs e);

    public interface IHelpInstrumentFilter : IHelpInstrumentArg
    {

        event EventFilter<object,object> SelectedChenget;
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

    public interface IStatus : IHelpInstrument
    {
        object Value { get; }
    }
}