using System;
using WPFMVVMHelper;

namespace DiplomaData.HelpInstrument.Command
{
    public class InstrumentProp : lamdaCommand
    {
        public string Name { get; }

        public InstrumentProp(string name, Action Execute, Func<bool> CenExecute = null)
            : base(Execute, CenExecute) =>
            Name = name;
    }

    public class InstrumentProp<T> : lamdaCommand<T>
    {
        public string Name { get; }

        public InstrumentProp(string name, Action<T> Execute, Func<T, bool> CenExecute = null)
            : base(Execute, CenExecute) =>
            Name = name;
    }
}