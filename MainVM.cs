using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFMVVMHelper;
using Microsoft.Office.Interop.Word;
using System.Collections.ObjectModel;
using DiplomaData.Отчёты;
using System.Windows.Input;
using DiplomaData.Model;
using System.Data.Linq;

namespace DiplomaData
{
    public static class Extension
    {
        public static bool SetAdd<T>(this ObservableCollection<T> ts,T item)
        {
            if (ts.Contains(item)) return false;
            ts.Add(item);
            return true;
        }
    }



    class MainVM : peremlog, IDisposable
    {
        public lamdaCommand OpenWordTemplate { get; }


        #region DiplomaData
        private DiplomaDataDataContext Data;
        /// <summary>Подключение к базе данных</summary>
        public DiplomaDataDataContext DiplomaData { get => Data; set =>Set(ref Data ,value); }
        #endregion


        #region TabThesis
        private TabTable tabThesis;
        /// <summary>Вкладка тем</summary>
        public TabTable TabThesis { get => tabThesis; set =>Set(ref tabThesis ,value); }
        #endregion


        #region Tabs
        private ObservableCollection<Tab> tabs;
        /// <summary>Вкладки</summary>
        public ObservableCollection<Tab> Tabs { get => tabs; set =>Set(ref tabs ,value); }
        #endregion

        public lamdaCommand UpdateData { get; }
        
        public lamdaCommand AddTabThesis { get; }


        public MainVM()
        {
            OpenWordTemplate = new lamdaCommand(OnOpenWordTemplate);

            DiplomaData = new DiplomaDataDataContext();

            TabThesis =new TabTable(DiplomaData.Thesis);

            UpdateData = new lamdaCommand(DiplomaData.SubmitChanges);

            Tabs = new ObservableCollection<Tab>();
            Tabs.CollectionChanged += Tabs_CollectionChanged;

            AddTabThesis = new lamdaCommand(() => Tabs.SetAdd(TabThesis));


        }



        private void Tabs_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    foreach (Tab item in e.NewItems)
                    {
                        item.AClose = a => Tabs.Remove(a);
                    }

                    break;
                default:
                    break;
            }
        }

        private void OnOpenWordTemplate()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == true)
            {
                var app = new Application();
                Document word = app.Documents.Add(Template: openFile.FileName, Visible: true);
                
                var wt = new WordTemplate()
                {
                    DataContext = ContentControls(word.Range().ContentControls)
                };
                wt.ShowDialog();
               
                try
                {
                    word.Close();
                    app.Quit();
                }
                catch { }

            }
        }

        private IEnumerable<object> ContentControls(ContentControls contentControls)
        {
            foreach (ContentControl item in contentControls)
                yield return new UContentControl(item);
        }

        public void Dispose()
        {
            Tabs.CollectionChanged -= Tabs_CollectionChanged;
            DiplomaData.Dispose();
        }
    }

}
