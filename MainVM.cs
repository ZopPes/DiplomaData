using DiplomaData.Model;
using DiplomaData.Tabs;
using DiplomaData.Tabs.TabTable;
using DiplomaData.Отчёты;
using Microsoft.Office.Interop.Word;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using WPFMVVMHelper;

namespace DiplomaData
{
    public static class Extension
    {
        public static bool SetAdd<T>(this ObservableCollection<T> ts, T item)
        {
            if (ts.Contains(item)) return false;
            ts.Add(item);
            return true;
        }
    }

    internal class MainVM : peremlog, IDisposable
    {
        public lamdaCommand OpenWordTemplate { get; }

        #region DiplomaData

        private DiplomaDataDataContext Data;

        /// <summary>Подключение к базе данных</summary>
        public DiplomaDataDataContext DiplomaData { get => Data; set => Set(ref Data, value); }

        #endregion DiplomaData

        #region Tabs

        private Tabs.Tabs tabs;

        /// <summary>Вкладки</summary>
        public Tabs.Tabs Tabs { get => tabs; set => Set(ref tabs, value); }

        #endregion Tabs

        public lamdaCommand UpdateData { get; }
        public lamdaCommand DateNow { get; }

        public MainVM()
        {
            OpenWordTemplate = new lamdaCommand(OnOpenWordTemplate);

            DiplomaData = new DiplomaDataDataContext();

            UpdateData = new lamdaCommand(DiplomaData.SubmitChanges);

            Tabs = new Tabs.Tabs();
            Tabs.AddCommand(new TabCommission(DiplomaData.Commission));
            Tabs.AddCommand(new TabDataFile(DiplomaData.DataFile));
            Tabs.AddCommand(new TabDiploma(DiplomaData.Diploma));
            Tabs.AddCommand(new TabFormOfEducation(DiplomaData.Form_of_education));
            Tabs.AddCommand(new TabGroup(DiplomaData.Group));
            Tabs.AddCommand(new TabLecturer(DiplomaData.Lecturer));
            Tabs.AddCommand(new TabReviewer(DiplomaData.Reviewer));
            Tabs.AddCommand(new TabSpecialty(DiplomaData.Specialty));
            Tabs.AddCommand(new TabStudent(DiplomaData.Student));
            Tabs.AddCommand(new TabThesis(DiplomaData.Thesis));
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

        public void Dispose()
        {
            DiplomaData.Dispose();
        }
    }
}