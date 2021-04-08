using DiplomaData.Model;
using DiplomaData.Tabs;
using DiplomaData.Tabs.CommandColection;
using DiplomaData.Tabs.TabReport;
using DiplomaData.Tabs.TabTable;
using DiplomaData.Отчёты;
using Microsoft.Office.Interop.Word;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using WPFMVVMHelper;
using Application = Microsoft.Office.Interop.Word.Application;

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

        private const string reportPath = "Отчёты";

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


        #region TableCommand
        private CommandCollection tableCollection;
        /// <summary>Список команд для добавления вкладок</summary>
        public CommandCollection TableCommand { get => tableCollection; set =>Set(ref tableCollection ,value); }
        #endregion


        #region ReportCommands
        /// <summary>Команды для добавления вкладок отчёта</summary>
        public CommandCollection ReportCommands { get; set; }
        #endregion

        #region Report
        private ObservableCollection<string> reports;
        /// <summary>Список отчётов</summary>
        public ObservableCollection<string> Reports { get => reports; set =>Set(ref reports ,value); }
        #endregion

        public lamdaCommand UpdateData { get; }
        public lamdaCommand DateNow { get; }

        public lamdaCommand UpdateReports { get; }

        public MainVM()
        {
            if (!Directory.Exists(reportPath))
                Directory.CreateDirectory(reportPath);

            UpdateReports = new lamdaCommand(OnUpdateReports);
           
            OnUpdateReports();

            OpenWordTemplate = new lamdaCommand(OnOpenWordTemplate);

            DiplomaData = new DiplomaDataDataContext();

            UpdateData = new lamdaCommand(()=>
            {
                try
                {
                    DiplomaData.SubmitChanges();
                }
                catch (Exception e)
                {
                    
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
            );
            Tabs = new Tabs.Tabs();
            #region AddTabsTable
            TableCommand = new CommandCollection(Tabs);
            TableCommand.Add(new TabCommission(DiplomaData.Commission));
            TableCommand.Add(new TabDataFile(DiplomaData.DataFile));
            TableCommand.Add(new TabDiploma(DiplomaData.Diploma));
            TableCommand.Add(new TabFormOfEducation(DiplomaData.Form_of_education));
            TableCommand.Add(new TabGroup(DiplomaData.Group));
            TableCommand.Add(new TabLecturer(DiplomaData.Lecturer));
            TableCommand.Add(new TabReviewer(DiplomaData.Reviewer));
            TableCommand.Add(new TabSpecialty(DiplomaData.Specialty));
            TableCommand.Add(new TabStudent(DiplomaData.Student));
            TableCommand.Add(new TabThesis(DiplomaData.Thesis));
            #endregion

            ReportCommands = new CommandCollection(Tabs);
            ReportCommands.Add(new TabReport("Отчёт"));
        }

        private void OnUpdateReports() => 
            Reports = new ObservableCollection<string>(Directory.EnumerateFiles(reportPath, "*.docx"));

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
            Tabs.Clear();
        }
    }
}