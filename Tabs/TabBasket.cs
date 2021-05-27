using DiplomaData.HelpInstrument.Command;
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

            ClearAll = new InstrumentProp
                (
                    "Удалить всё"
                    ,() =>
                    {
                        foreach (IBasket item in Kor)
                            item.Clear();
                    });
            InstrumentProps.Add(ClearAll);
        }

        public ObservableCollection<IBasket> Kor { get; set; } = new ObservableCollection<IBasket>();
    }
}