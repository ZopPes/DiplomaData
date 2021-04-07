using System;
using WPFMVVMHelper;

namespace DiplomaData
{
    public class AddCommand : lamdaCommand
    {
        public string Name { get; set; }

        public AddCommand(Action Execute, string name, Func<bool> CenExecute = null) : base(Execute, CenExecute)
        {
            Name = name;
        }
    }
}