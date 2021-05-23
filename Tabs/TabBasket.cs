using System.Collections.ObjectModel;
using System.Windows.Input;
using WPFMVVMHelper;

namespace DiplomaData.Tabs
{
    public class TabBasket : Tab
    {
        public ICommand ClearAll { get; }

        public TabBasket(string name = "") : base(name)
        {
            IsVisible = System.Windows.Visibility.Visible;

            ClearAll = new lamdaCommand
                (
                    () =>
                    {
                        foreach (IBasket item in Kor)
                            item.Clear();
                    });
        }

        public TabBasket() : this(""){}

        public ObservableCollection<IBasket> Kor { get; set; } = new ObservableCollection<IBasket>();
    }
}