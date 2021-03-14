using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFMVVMHelper;
using System.Windows.Input;

namespace DiplomaData
{
    public abstract class Tab : peremlog
    {
        /// <summary>
        /// класс для кправления вкладками приложения
        /// </summary>
        public Tab(int index=0)
        {
            Close = new lamdaCommand(()=>AClose?.Invoke(this));
            Index = index;
        }

        /// <summary>
        /// название вкладки
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// комманда для закрытия вкладки
        /// </summary>
        public ICommand Close { get; }

        public Action<Tab> AClose;

        public int Index { get; }

        public override bool Equals(object obj)
        {
            return obj is Tab tab &&
                   Index == tab.Index;
        }

        public override int GetHashCode()
        {
            return -2134847229 + Index.GetHashCode();
        }
    }
}
