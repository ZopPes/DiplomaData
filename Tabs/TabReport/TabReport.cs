using DiplomaData.HelpInstrument.Command;
using DiplomaData.Model;
using DiplomaData.Properties;
using DiplomaData.Tabs.TabTable;
using Microsoft.Office.Interop.Word;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Application = Microsoft.Office.Interop.Word.Application;

namespace DiplomaData.Tabs.TabReport
{
    public class TabReport : TabTable<Diplom_rus>
    {
        public static uint INDEX
        {
            get => Settings.Default.IndexDiplomaReport;
            set
            {
                Settings.Default.IndexDiplomaReport = value;
                Settings.Default.Save();
            }
        }

        public ICommand CreateReport { get; }
        public ICommand CreateAllReport { get; }

        public TabReport(
            Func<IQueryable<Diplom_rus>, string, IQueryable<Diplom_rus>> tFilt
            , Table<Diplom_rus> diploms, string name = "")
            : base(diploms, tFilt, name)
        {
            CreateReport = new InstrumentProp("печатать выбранные"
                , () => OnCreateReport(SelectedItems)
                , () => SelectedItems != null);
            CreateAllReport = new InstrumentProp("печатать всё", () => OnCreateReport(TableT));
            InstrumentProps.Add(CreateReport);
            InstrumentProps.Add(CreateAllReport);
        }

        private void OnCreateReport(IEnumerable Diploms)
        {
            var file = CopyReport();
            if (string.IsNullOrEmpty(file)) return;
            var path = Path.GetDirectoryName(file);
            var dataPath = path + @"\данные.csv";
            CreateDataFile(dataPath, Diploms.OfType<Diplom_rus>()
                , diplom => new object[]
                                 {
                                INDEX++
                                , diplom?.Student_rus?.ToString() ?? ""
                                , diplom?.Student_rus.Group_rus.Specialty_rus.FormatШифр_специальности ?? ""
                                , diplom?.Student_rus.Group_rus.Specialty_rus.Специальность ?? ""
                                , diplom?.Thesis_rus?.ToString() ?? ""
                                , diplom?.Lecturer_rus?.ToString() ?? ""
                                 }
                , "Номер_Документа"
                , "ФИО"
                , "Номер_специальности"
                , "Название_специальности"
                , "Название_темы"
                , "Руководитель");

            ConectWordCSV(file, dataPath);
        }

        private void ConectWordCSV(string file, string dataPath)
        {
            var app = new Application();
            Document word = app.Documents.Open(file, Visible: true);
            word.MailMerge.MainDocumentType = WdMailMergeMainDocType.wdFormLetters;
            word.MailMerge.OpenDataSource(Name: dataPath, ConfirmConversions: false, ReadOnly: false, LinkToSource: true, Format: WdOpenFormat.wdOpenFormatAuto, SubType: WdOpenFormat.wdOpenFormatAuto);
            //показать результат
            app.Visible = true;
        }

        private void CreateDataFile<T>(string path, IEnumerable<T> diploms, Func<T, object[]> selectData, params string[] titles)
        {
            using (var csv = new StreamWriter(path))
            {
                csv.WriteLine(string.Join(";", titles));
                foreach (object[] diplom in diploms.Select(selectData))
                    try
                        {csv.WriteLine(string.Join(";", diplom));}
                    catch (Exception)
                        {MessageBox.Show("Не корректные данные", "Ошибка");}
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