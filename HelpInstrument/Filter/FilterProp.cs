namespace DiplomaData.HelpInstrument.Filter
{
    public class FilterProp : IHelpInstrument
    {
        public string Name { get; }


        public FilterProp(string name) => Name = name;
    }
}