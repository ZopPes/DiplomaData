﻿using DiplomaData.HelpInstrument;
using DiplomaData.HelpInstrument.Filter;
using DiplomaData.HelpInstrument.Sort;
using DiplomaData.Model;
using DiplomaData.Tabs.TabTable;
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
	public class TabReport : TabTable<Diplom_rus>
	{

		public static uint INDEX
		{
			get => DiplomaData.Properties.Settings.Default.IndexDiplomaReport;
			set
			{
				DiplomaData.Properties.Settings.Default.IndexDiplomaReport = value;
				DiplomaData.Properties.Settings.Default.Save();
			}
		}


		public ICommand CreateReport { get; }
		public ICommand CreateAllReport { get; }



		public TabReport(
			Func<IQueryable<Diplom_rus>, string, IQueryable<Diplom_rus>> tFilt
			, Table<Diplom_rus> diploms, string name = "")
			: base(diploms,tFilt,name)
		{
			CreateReport = new lamdaCommand<IEnumerable>(OnCreateReport);
			CreateAllReport = new lamdaCommand(() => OnCreateReport(Table));
			//Properties.Add(new Property("Количество строк", () => SelectData.Count()));
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