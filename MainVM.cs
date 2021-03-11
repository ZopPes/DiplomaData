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

namespace DiplomaData
{
    class MainVM : peremlog
    {
        public lamdaCommand OpenWordTemplate { get; }


        public MainVM()
        {
            OpenWordTemplate = new lamdaCommand(OnOpenWordTemplate);
            
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
    }

}
