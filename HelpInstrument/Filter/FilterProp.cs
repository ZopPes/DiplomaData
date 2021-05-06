using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaData.HelpInstrument.Filter
{
    class FilterProp :  IHelpInstrument
    {
        public string Name { get; }



        public IHelpInstrumentArg Filter { get; }

        public FilterProp(string name,IHelpInstrumentArg filter)
        {
            Name = name;
            Filter = filter;
        }
    }
}
