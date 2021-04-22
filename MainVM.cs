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
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFMVVMHelper;
using Application = Microsoft.Office.Interop.Word.Application;

namespace DiplomaData
{
    /// <summary>
    /// Сахар
    /// </summary>
    public static class Extension
    {
        public static bool SetAdd<T>(this ObservableCollection<T> ts, T item)
        {
            if (ts.Contains(item)) return false;
            ts.Add(item);
            return true;
        }

        public static CommandCollection CreateCommands(this Tabs.Tabs tabs) => new CommandCollection(tabs);
        public static TabTable<T> CreateTabTable<T>(this Table<T> table, string name) where T : class, new() => new TabTable<T>(table, name);

        public static CommandCollection AddTable<T>(this CommandCollection tabs, TabTable<T> ts) where T : class, new()
        {
            tabs.Add(ts);
            return tabs;
        }

        public static CommandCollection AddTable<T>(this CommandCollection tabs, Table<T> ts, string name) where T : class, new()
        {
            tabs.Add(new TabTable<T>(ts, name));
            return tabs;
        }

        public static CommandCollection CreateCommands(this Tabs.Tabs tabs, params Tab[] tab)
        {
            var r = new CommandCollection(tabs);
            foreach (var item in tab)
            {
                r.Add(item);
            }
            return r;
        }


    }

    /// <summary>
    /// главный VM класс
    /// </summary>
    internal class MainVM : peremlog, IDisposable
    {
        /// <summary>
        /// имя папки с отчётами
        /// </summary>
        private const string reportPath = "Отчёты";

        #region DiplomaData
        /// <summary>Подключение к базе данных</summary>
        public DiplomaDataDataContext DiplomaData { get; }

        #endregion DiplomaData

        #region Tabs
        /// <summary>Вкладки</summary>
        public Tabs.Tabs Tabs { get; }

        #endregion Tabs

        #region TableCommand
        /// <summary>команды для добавления вкладок с таблицами</summary>
        public CommandCollection TableCommand { get; }

        #endregion TableCommand

        #region ReportCommands
        /// <summary>Команды для добавления вкладок отчёта</summary>
        public CommandCollection ReportCommands { get; }

        #endregion ReportCommands

        #region Report

        private ObservableCollection<string> reports;

        /// <summary>Список отчётов</summary>
        public ObservableCollection<string> Reports { get => reports; set => Set(ref reports, value); }

        #endregion Report

        #region ICommands
        /// <summary>
        /// Загружает все данные в базу
        /// </summary>
        public ICommand UpdateData { get; }
        /// <summary>
        /// открытие отчёты
        /// </summary>
        public ICommand OpenWordTemplate { get; }
        /// <summary>
        /// обновляет список отчётов
        /// </summary>
        public ICommand UpdateReports { get; }
        #endregion

        /// <summary>
        /// главный VM класс
        /// </summary>
        public MainVM()
        {
            if (!Directory.Exists(reportPath))
                Directory.CreateDirectory(reportPath);

            OnUpdateReports();

            DiplomaData = new DiplomaDataDataContext();
            Tabs = new Tabs.Tabs();

            #region AddTabsTable

            var s = DiplomaData.Student.CreateTabTable("Студент");
            s.FilterChanged += S_FilterChanged;
            TableCommand = Tabs.CreateCommands
                (
                 s 
                , DiplomaData.Group.CreateTabTable("Группа")
                , DiplomaData.Specialty.CreateTabTable("Специальность")
                , DiplomaData.Lecturer.CreateTabTable("Преподователь")
                , DiplomaData.Thesis.CreateTabTable("Темы дипломов")
                );
            #endregion AddTabsTable


            ReportCommands = Tabs.CreateCommands();
            ReportCommands.Add(new TabReport("Отчёт"));

            #region InitCommand
            UpdateData = new lamdaCommand(() =>
            {
                try
                {
                    DiplomaData.SubmitChanges();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            });
            OpenWordTemplate = new lamdaCommand(OnOpenWordTemplate);
            UpdateReports = new lamdaCommand(OnUpdateReports);
            #endregion
        }

        private void S_FilterChanged(object sender, string e)
        {
            var r = (sender as TabTable<Student>);
            r.SelectData = r.test.Where(w =>
                (w.name + w.surname+ w.patronymic+w.Group.number).Contains(e)
            );
        }

        /// <summary>
        /// Обновляет список дипломов
        /// </summary>
        private void OnUpdateReports() =>
            Reports = new ObservableCollection<string>(Directory.EnumerateFiles(reportPath, "*.docx"));

        /// <summary>
        /// открывает фаил ворд
        /// </summary>
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

            TableCommand.Clear();
            ReportCommands.Clear();
        }
    }
}