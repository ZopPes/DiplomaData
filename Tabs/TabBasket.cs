using DiplomaData.Model;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFMVVMHelper;

namespace DiplomaData.Tabs
{
    public class TabBasket : Tab
	{
		public ICommand ClearAll { get; }

		public TabBasket(string name = "", params IBasket[] vs) : base(name)
		{

			Kor = new ObservableCollection<IBasket>(vs);
			IsVisible = System.Windows.Visibility.Visible;

			ClearAll = new lamdaCommand
				(
					()=> {
                        foreach (IBasket item in Kor)
							item.Clear();
					});
		}

		public ObservableCollection<IBasket> Kor { get; set; }


	}
}

