using DiplomaData.Model;
using DiplomaData.Tabs;
using DiplomaData.Tabs.TabReport;
using DiplomaData.Tabs.TabTable;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Data.Linq;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Input;
using WPFMVVMHelper;

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

        public static TabTable<T> CreateTabTable<T>
            (this Table<T> table, Func<IQueryable<T>, string, IQueryable<T>> func
            , Action<T> action, Action<T> delete
            , string name) where T : class, new() =>
            new TabTable<T>(table, func, action, delete, name);
    }

   

    /// <summary>
    /// главный VM класс
    /// </summary>
    public partial class MainVM : peremlog, IDisposable
    {
        /// <summary>
        /// имя папки с отчётами
        /// </summary>
        public const string reportPath = "Отчёты";
        public const string diplomaFilePath = "файлы Диплома";

        #region Tabs

        /// <summary>Вкладки</summary>
        public Tabs.Tabs Tabs { get; } = new Tabs.Tabs();

        #endregion Tabs

        #region TableTabs

        /// <summary>команды для добавления вкладок с таблицами</summary>
        public ObservableCollection<Tab> Tables { get; } = new ObservableCollection<Tab>();

        #endregion TableTabs

        #region ReportTabs

        /// <summary>Команды для добавления вкладок отчёта</summary>
        public ObservableCollection<Tab> ReportTabs { get; } = new ObservableCollection<Tab>();

        public TabReport Report { get; } = new TabReport("Протокол защиты дипломного проекта");

        #endregion ReportTabs

        #region Report

        private ObservableCollection<string> reports;

        /// <summary>Список отчётов</summary>
        public ObservableCollection<string> Reports { get => reports; set => Set(ref reports, value); }

        #endregion Report

        public TabBasket Basket { get; } = new TabBasket("Корзина");

        #region ICommands


        /// <summary>
        /// обновляет список отчётов
        /// </summary>
        public ICommand UpdateReports { get; set; }
        public ICommand CopyFileDiploma { get; set; }
        public ICommand OpenFileDiploma { get; set; }

       /// <summary>
       /// Добавление пустого диплома
       /// </summary>
        public ICommand AddEmptyDiploma { get; set; }

        #endregion ICommands

        private void initViewModel()
        {
            if (!Directory.Exists(reportPath))
                Directory.CreateDirectory(reportPath);

            if (!Directory.Exists(diplomaFilePath))
                Directory.CreateDirectory(diplomaFilePath);

            OnUpdateReports();


            #region Корзина

            Basket.Kor.Add(new BasketItem<Group_rus>
                    (
                        DiplomaData.remotely_Group
                    , g => DiplomaData.Recovery_group(g.Номер_группы)
                    , g => DiplomaData.Delete_remotelt_group(g.Номер_группы)
                    , "Группы"
                    ));
            Basket.Kor.Add(new BasketItem<Lecturer_rus>
                    (
                        DiplomaData.remotely_Lecturer
                    , l => DiplomaData.Recovery_lecturer(l.id)
                    , l => DiplomaData.Delete_remotelt_lecturer(l.id)
                    , "Преподователи"
                    ));
            Basket.Kor.Add(new BasketItem<Commission_rus>
                    (
                        DiplomaData.remotely_Commission
                    , c => DiplomaData.Recovery_commission(c.id)
                    , c => DiplomaData.Delete_remotelt_commision(c.id)
                    , "Коммися"
                    ));
            Basket.Kor.Add(new BasketItem<Specialty_rus>
                    (
                        DiplomaData.remotely_Specialty
                    , s => DiplomaData.Recovery_specialty(s.Шифр_специальности)
                    , s => DiplomaData.Delete_remotelt_specialty(s.Шифр_специальности)
                    , "Специальность"
                    ));

            Basket.Kor.Add(new BasketItem<Student_rus>
                    (
                        DiplomaData.remotely_Student
                    , s => DiplomaData.Recovery_student(s.id)
                    , s => DiplomaData.Delete_remotelt_student(s.id)
                    , "Студент"
                    ));

            Basket.Kor.Add(new BasketItem<Thesis_rus>
                    (
                        DiplomaData.remotely_Thesis
                    , t => DiplomaData.Recovery_thesis(t.id)
                    , t => DiplomaData.Delete_remotelt_Thesis(t.id)
                    , "Тема"
                    ));
            #endregion Корзина

            

            #region InitCommand

            UpdateReports = new lamdaCommand(OnUpdateReports);

            CopyFileDiploma = new lamdaCommand<Diplom_rus>(OnCopyFileDiploma);
            OpenFileDiploma = new lamdaCommand<Diplom_rus>(OnOpenFileDiploma);
            #endregion InitCommand
        }

        /// <summary>
        /// главный VM класс
        /// </summary>
        public MainVM()
        {
            initViewModel();
            initDataBase();

            Tabs.Add(Basket);

            foreach (Tab tab in Tables)
                Tabs.Add(tab);
            foreach (Tab tab in ReportTabs)
                Tabs.Add(tab);
        }

        private void OnOpenFileDiploma(Diplom_rus obj)
        {
            Process.Start(Path.Combine(diplomaFilePath, obj.DataFile.name));
        }

        private void OnCopyFileDiploma(Diplom_rus obj)
        {
            //пользователь выбирает фаил
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() != true) return;

            //фаил копируется в папку с дипломами
            var newFilePath = Path.Combine(diplomaFilePath, openFile.SafeFileName);
            File.Copy(openFile.FileName, newFilePath);

            //путь к файлу загружается в базу
            obj.DataFile = new DataFile() { name = openFile.SafeFileName };
            //база обновляется
            DiplomaData.SubmitChanges();
        }

        /// <summary>
        /// Обновляет список дипломов
        /// </summary>
        private void OnUpdateReports() =>
            Reports = new ObservableCollection<string>(Directory.EnumerateFiles(reportPath, "*.docx"));

        public void Dispose()
        {
            DiplomaData.Dispose();
            Tabs.Clear();

            Tables.Clear();
            ReportTabs.Clear();
        }


    }
}