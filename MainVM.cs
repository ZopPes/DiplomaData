using DiplomaData.HelpInstrument;
using DiplomaData.HelpInstrument.Filter;
using DiplomaData.Model;
using DiplomaData.Tabs;
using DiplomaData.Tabs.TabReport;
using DiplomaData.Tabs.TabTable;
using DiplomaData.Отчёты;
using Microsoft.Office.Interop.Word;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Windows;
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

		public static TabTable<T> CreateTabTable<T>
			(this Table<T> table, Func<IQueryable<T>,string, IQueryable<T>> func
			,Action<T> action, Action<T> delete
			,string name) where T : class, new() =>
			new TabTable<T>(table, func, action, delete, name);

	}

	/// <summary>
	/// главный VM класс
	/// </summary>
	internal class MainVM : peremlog, IDisposable
	{
		/// <summary>
		/// имя папки с отчётами
		/// </summary>
		public const string reportPath = "Отчёты";

		#region DiplomaData
		/// <summary>Подключение к базе данных</summary>
		public DiplomasDataContext DiplomaData { get; }

		public IQueryable<Group_rus> Groups => from g in DiplomaData.Group_rus select g;
		public IQueryable<Specialty_rus> Specialties => from s in DiplomaData.Specialty_rus select s;
		public IQueryable<Lecturer_rus> Lecturers => from l in DiplomaData.Lecturer_rus select l;
		public IQueryable<Thesis_rus> Theses => from t in DiplomaData.Thesis_rus select t;
		public IQueryable<Thesis_rus> FreeTheses => from t in DiplomaData.Thesis_rus where !t.Занята__не_занята select t;


		public IEnumerable RPG => DiplomaData.remotely_Group();
		public IEnumerable RL => DiplomaData.remotely_Lecturer();

		#endregion DiplomaData

		#region Tabs
		/// <summary>Вкладки</summary>
		public Tabs.Tabs Tabs { get; }

		#endregion Tabs

		#region TableTabs
		/// <summary>команды для добавления вкладок с таблицами</summary>
		public ObservableCollection<Tab> Tables { get; }

		#endregion TableTabs

		#region ReportTabs
		/// <summary>Команды для добавления вкладок отчёта</summary>
		public ObservableCollection<Tab> ReportTabs { get; }

		public TabReport Report { get; }

		#endregion ReportTabs

		#region Report

		private ObservableCollection<string> reports;

		/// <summary>Список отчётов</summary>
		public ObservableCollection<string> Reports { get => reports; set => Set(ref reports, value); }

		#endregion Report

		public Tab Basket { get; }

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
		public ICommand AddEmptyDiploma { get; }


		#endregion

		/// <summary>
		/// главный VM класс
		/// </summary>
		public MainVM()
		{

			if (!Directory.Exists(reportPath))
				Directory.CreateDirectory(reportPath);
			
			OnUpdateReports();

			DiplomaData = new DiplomasDataContext();

			//OnP(DiplomaData.Diplom_rus);

			ReportTabs = new ObservableCollection<Tab>();
			Report = new TabReport(DiplomaData.Diplom_rus, "Отчет по защите диплома(?)");
			ReportTabs.Add(Report);

			Tables = new ObservableCollection<Tab>();

			#region AddTabs

			var student = DiplomaData.Student_rus.CreateTabTable
				(
					(q, e) => q.Where(w => (w.Имя + " " + w.Отчество + " " + w.Фамилия).Contains(e))
				, s => DiplomaData.Add_Student(s.Фамилия, s.Имя, s.Отчество, s.Номер_группы)
				, s => DiplomaData.Delete_remotelt_student(s.id)
				, "Студент"
				) ;
			student.AddFilterList("Группа:", Groups, g => s => s.Group_rus == g);
			student.AddFilterList("Рукаводитель:", Lecturers, l => s => s.Diplom_rus.Lecturer_rus== l);
			student.AddFilterText("Оценка:", m => s =>s.Diplom_rus.Оценка==m);
			student.AddFilterDate("Дата сдачи:", d => s => d.Item1 < s.Diplom_rus.Дата_сдачи && s.Diplom_rus.Дата_сдачи<d.Item2);
			student.AddSort("Оценка:", s => s.Diplom_rus.Оценка);
			student.AddSort("ФИО:", s => s.Фамилия+s.Имя+s.Отчество);
			student.AddSort("Руководитель:", s => s.Diplom_rus.Lecturer_rus.Фамилия+ s.Diplom_rus.Lecturer_rus.Имя+ s.Diplom_rus.Lecturer_rus.Отчество);
			student.AddSort("дата:", s => s.Diplom_rus.Дата_сдачи);

			var Lecturer = DiplomaData.Lecturer_rus.CreateTabTable
				(
				(q, e) => q.Where(w => (w.Имя + " " + w.Отчество + " " + w.Фамилия + " ").Contains(e))
				, l => DiplomaData.Add_Lecturer(l.Фамилия,l.Имя,l.Отчество)
				, l =>DiplomaData.Delete_remotelt_lecturer(l.id)
				, "Преподователь"
				);

			var Specialty = DiplomaData.Specialty_rus.CreateTabTable
				(
				(q, e) => q.Where(w => (w.Специальность + " " + w.Шифр_специальности).Contains(e))
				, s => DiplomaData.Add_Specialty(s.Шифр_специальности,s.Специальность)
				, s =>DiplomaData.Delete_remotelt_specialty(s.Шифр_специальности)
				, "Специальность"
				);

			var Thesis = DiplomaData.Thesis_rus.CreateTabTable
			   (
			   (q, e) => q.Where(w => (w.Название_темы + " " + w.Описание).Contains(e))
				, t => DiplomaData.Add_Thesis(t.Название_темы,t.Описание,t.Дата_выдачи)
				, t =>DiplomaData.Delete_remotelt_Thesis(t.id)
			   , "Тема диплома"
			   );

			var group = DiplomaData.Group_rus.CreateTabTable
				(
					(q,e)=> q.Where(w=>w.Номер_группы.Contains(e))
				, g => 
				{ DiplomaData.Add_Group(g.Номер_группы, g.Специальность, g.Куратор, g.Форма_обучения); OnPropertyChanged(nameof(Groups)); }
				, g =>DiplomaData.Delete_remotelt_group(g.Номер_группы)
				, "Группа"
				);
		   
			Tables.Add(student);
			Tables.Add(Lecturer);
			Tables.Add(Specialty);
			Tables.Add(Thesis);
			Tables.Add(group);
			#endregion

			

			Tabs = new Tabs.Tabs();

            #region Корзина


            Basket = new TabBasket("Корзина"
				,   new BasketItem<Group_rus>
					(
						DiplomaData.remotely_Group
					,	g=>DiplomaData.Recovery_group(g.Номер_группы)
					,	g=> DiplomaData.Delete_remotelt_group(g.Номер_группы)
					,	"Группы"
					)
				,   new BasketItem<Lecturer_rus>
					(
						DiplomaData.remotely_Lecturer
					,	l=>DiplomaData.Recovery_lecturer(l.id)
					,	l=> DiplomaData.Delete_remotelt_lecturer(l.id)
					,	"Преподователи"
					)
				,   new BasketItem<Commission_rus>
					(
						DiplomaData.remotely_Commission
					,	c=>DiplomaData.Recovery_commission(c.id)
					,	c=> DiplomaData.Delete_remotelt_commision(c.id)
					,	"Коммися"
					)
				,   new BasketItem<Specialty_rus>
					(
						DiplomaData.remotely_Specialty
					,	s=>DiplomaData.Recovery_specialty(s.Шифр_специальности)
					,	s=> DiplomaData.Delete_remotelt_specialty(s.Шифр_специальности)
					,	"Специальность"
					)
				,   new BasketItem<Student_rus>
					(
						DiplomaData.remotely_Student
					,	s=>DiplomaData.Recovery_student(s.id)
					,	s=> DiplomaData.Delete_remotelt_student(s.id)
					,	"Студент")
				,   new BasketItem<Thesis_rus>
					(
						DiplomaData.remotely_Thesis
					,	t=>DiplomaData.Recovery_thesis(t.id)
					,	t=> DiplomaData.Delete_remotelt_Thesis(t.id)
					,	"Тема"
					)
				);

            #endregion

			Tabs.Add(Basket);

			foreach (Tab tab in Tables)
			{
				Tabs.Add(tab);
			}
			foreach (Tab tab in ReportTabs)
			{
				Tabs.Add(tab);
			}

			#region InitCommand
			UpdateData = new lamdaCommand(() =>
			{
				try
				{
					DiplomaData.Refresh(RefreshMode.OverwriteCurrentValues,DiplomaData.Student_rus);
					DiplomaData.Refresh(RefreshMode.OverwriteCurrentValues, DiplomaData.Group_rus);
					DiplomaData.Refresh(RefreshMode.OverwriteCurrentValues, DiplomaData.Diplom_rus);
				}
				catch (Exception e)
				{
					MessageBox.Show(e.Message, "Ошибка");
				}
			});
			OpenWordTemplate = new lamdaCommand(OnOpenWordTemplate);
			UpdateReports = new lamdaCommand(OnUpdateReports);

			AddEmptyDiploma = new lamdaCommand<Student_rus>
				(
				s => 
					{
						foreach (Diplom_rus diplom in DiplomaData.Add_Empty_Diploma(s.id))
							s.Diplom_rus = diplom;
					}
				) ;

			

			#endregion
		}

		private	void OnCreateDocStudent(Student_rus student)
        {
			//открыть шаблон
			var app = new Application();
			Document word = app.Documents.Add(Template: @"C:\Users\zop85\Source\Repos\DiplomaData\bin\Debug\Отчёты\ПРОТОКОЛ.docx", Visible: true);
            //записать данные о студенте
            foreach (ContentControl item in word.Range().ContentControls)
            {
                if (item.Type == WdContentControlType.wdContentControlRichText)
                {
                    switch (item.PlaceholderText.Value)
                    {
						case "ФИО студента":
							item.Range.Text = student.ToString();
							break;
						case "Номер специальности":
							item.Range.Text = student.Group_rus.Specialty_rus.Шифр_специальности;
							break;
						case "наименование специальности":
							item.Range.Text = student.Group_rus.Specialty_rus.Специальность;
							break;
					}
                    
                }
            }
			//показать результат
			app.Visible = true;
		}
		

		private void OnP(IEnumerable<Diplom_rus> diplomas)
        {
            using (var csv=new StreamWriter(reportPath+@"\данные.csv"))
            {
				csv.WriteLine(string.Join(";","ФИО","Номер_специальности","Название_специальности","Название_темы","Руководитель"));
                foreach (Diplom_rus diplom in diplomas)
                {
					csv.WriteLine
						(
						string.Join(";"
							,diplom?.Student_rus?.ToString() ?? ""
							,diplom?.Student_rus.Group_rus.Specialty_rus.FormatШифр_специальности ?? ""
							, diplom?.Student_rus.Group_rus.Specialty_rus.Специальность ?? ""
							, ""
							, "")
						);
                }
            }
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
				word.MailMerge.OpenDataSource(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
				word.MailMerge.Fields.Add(word.Range(), "ФОИ");
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

			Tables.Clear();
			ReportTabs.Clear();
		}

        private object name1;

        public object Name { get => name1; set => Set(ref name1, value); }
    }
}