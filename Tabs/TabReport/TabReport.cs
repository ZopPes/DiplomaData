using DiplomaData.HelpInstrument;
using DiplomaData.HelpInstrument.Filter;
using DiplomaData.HelpInstrument.Sort;
using DiplomaData.Model;
using Microsoft.Office.Interop.Word;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Input;
using WPFMVVMHelper;
using Application = Microsoft.Office.Interop.Word.Application;

namespace DiplomaData.Tabs.TabReport
{
    internal class TabReport : Tab
    {
        public Table<Diplom_rus> Diploms { get; }


        #region SelectData
        private IQueryable<Diplom_rus> selectData;
        /// <summary>список дипломов</summary>
        public IQueryable<Diplom_rus> SelectData { get => selectData; set { if (Set(ref selectData, value)) OnProperties(); } }
        #endregion

        public static uint INDEX
        {
            get => DiplomaData.Properties.Settings.Default.IndexDiplomaReport;
            set
            {
                DiplomaData.Properties.Settings.Default.IndexDiplomaReport = value;
                DiplomaData.Properties.Settings.Default.Save();
            }
        }

        public ICommand Update { get; }

        public ICommand CreateReport { get; }
        public ICommand CreateAllReport { get; }

        public Func<IQueryable<Diplom_rus>, string, IQueryable<Diplom_rus>> TFilt { get; }


        public TabReport(
            Func<IQueryable<Diplom_rus>, string, IQueryable<Diplom_rus>> tFilt
            , Table<Diplom_rus> diploms, string name = "") 
            : base(name)
        {
            TFilt = tFilt;
            FilterChanged += TabTable_FilterChanged;

            Diploms = diploms;
            SelectData = Diploms;
            CreateReport = new lamdaCommand<IEnumerable>(OnCreateReport);
            CreateAllReport = new lamdaCommand(() => OnCreateReport(Diploms));
            Update = new lamdaCommand(OnUpdate);
            Properties.Add(new Property("Количество строк",()=> SelectData.Count()));
        }

        private void TabTable_FilterChanged(object sender, string e)
        {
            SelectData = TFilt?.Invoke(Diploms, e);
        }

        public void AddFilterList<TFilter>(string name, IQueryable<TFilter> queryable, Func<TFilter, Expression<Func<Diplom_rus, bool>>> expression)
        {
            var filterList = new FilterList(queryable);
            filterList.SelectedChenget += (obj, s) => SelectData = Diploms.Where(expression?.Invoke((TFilter)s));
            FilterParam.Add
                (
                new FilterProp(name, filterList)
                );
        }

        public void AddFilterText(string name, Func<char, Expression<Func<Diplom_rus, bool>>> expression)
        {
            var filterList = new FilterMarcChar();
            filterList.SelectedChenget +=
                (obj, s) => SelectData = Diploms.Where(expression?.Invoke(s));
            FilterParam.Add
                (
                new FilterProp(name, filterList)
                );
        }

        public void AddFilterDate(string name, Func<(DateTime, DateTime), Expression<Func<Diplom_rus, bool>>> expression)
        {
            var filterList = new FilterDate();
            filterList.SelectedChenget +=
                (obj, d) => SelectData = Diploms.Where(expression?.Invoke(d));
            FilterParam.Add
                (
                new FilterProp(name, filterList)
                );
        }

        internal void AddSort<TKey>(string name, Expression<Func<Diplom_rus, TKey>> expression)
        {
            var sort = new SortProp(name);
            sort.StatusChenget += (o, s) =>
            {
                switch (s)
                {
                    case SortStatus.off:
                        SelectData = Diploms;
                        break;

                    case SortStatus.desc:
                        SelectData = SelectData.OrderByDescending(expression);
                        break;

                    case SortStatus.asc:
                        SelectData = SelectData.OrderBy(expression);
                        break;

                    default:
                        break;
                }
            };
            SortParam.Add(sort);
        }

        private void OnUpdate()
        {
            try
            {
                Diploms.Context.SubmitChanges();
                Diploms.Context.Refresh(RefreshMode.OverwriteCurrentValues, Diploms);
                OnPropertyChanged(nameof(Diploms));
            }
            catch (System.Exception)
            {
                MessageBox.Show("НЕ ВЕРНОЕ ЗНАЧЕНИЕ", "Ошибка Ввода");
            }
        }

        private void OnCreateReport(IEnumerable obj)
        {
            var file = CopyReport();
            if (file == string.Empty) return;
            var path = Path.GetDirectoryName(file);
            var dataPath = path + @"\данные.csv";
            CreateDataFile(dataPath, obj);
            ConectWordCSV(file, dataPath, obj);
        }

        private void ConectWordCSV(string file, string dataPath, IEnumerable diploms)
        {
            var app = new Application();
            Document word = app.Documents.Open(file, Visible: true);
            word.MailMerge.MainDocumentType = WdMailMergeMainDocType.wdFormLetters;
            word.MailMerge.OpenDataSource(Name: dataPath, ConfirmConversions: false, ReadOnly: false, LinkToSource: true, Format: WdOpenFormat.wdOpenFormatAuto, SubType: WdOpenFormat.wdOpenFormatAuto);

            //показать результат
            app.Visible = true;
        }

        private void CreateDataFile(string path, IEnumerable diploms)
        {
            using (var csv = new StreamWriter(path))
            {
                csv.WriteLine(string.Join(";"
                    , "Номер_Документа"
                    , "ФИО"
                    , "Номер_специальности"
                    , "Название_специальности"
                    , "Название_темы"
                    , "Руководитель"));
                foreach (Diplom_rus diplom in diploms)
                {
                    try
                    {
                        csv.WriteLine
                            (
                            string.Join(";"
                                , INDEX++
                                , diplom?.Student_rus?.ToString() ?? ""
                                , diplom?.Student_rus.Group_rus.Specialty_rus.FormatШифр_специальности ?? ""
                                , diplom?.Student_rus.Group_rus.Specialty_rus.Специальность ?? ""
                                , diplom?.Thesis_rus?.ToString() ?? ""
                                , diplom?.Lecturer_rus?.ToString() ?? "")
                            );
                    }
                    catch (System.Exception)
                    {
                        MessageBox.Show("Не корректные данные", "Ошибка");
                    }
                }
            }
        }

        private string CopyReport()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() != true) return string.Empty;
            SaveFileDialog saveFile = new SaveFileDialog();
            if (saveFile.ShowDialog() != true) return string.Empty;
            File.Copy(openFile.FileName, saveFile.FileName, true);
            return saveFile.FileName;
        }
    }
}