using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFMVVMHelper;
using Microsoft.Office.Interop.Word;

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
                Document word = app.Documents.Add(openFile.FileName);
                var wt = new Отчёты.WordTemplate()
                {
                    DataContext = word.Range().ContentControls
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

        
    }

}
