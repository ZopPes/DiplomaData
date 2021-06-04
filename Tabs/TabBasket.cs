using DiplomaData.HelpInstrument.Command;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DiplomaData.Tabs
{
    public class TabBasket : Tab
    {
        public ICommand ClearAll { get; }

        public ObservableCollection<IBasket> Kor { get; } = new ObservableCollection<IBasket>();

        public TabBasket(string name = "") : base(name)
        {
            IsVisible = System.Windows.Visibility.Visible;

            ClearAll = new InstrumentProp
                (
                    "Удалить всё"
                    , () =>
                     {
                         foreach (IBasket item in Kor)
                             item.Clear();
                     });
            InstrumentProps.Add(ClearAll);
        }
    }
}