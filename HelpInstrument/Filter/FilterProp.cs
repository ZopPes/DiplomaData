namespace DiplomaData.HelpInstrument.Filter
{
    internal class FilterProp : IHelpInstrument
    {
        public string Name { get; }

        public IHelpInstrumentArg Filter { get; }

        public FilterProp(string name, IHelpInstrumentArg filter)
        {
            Name = name;
            Filter = filter;
        }
    }
}