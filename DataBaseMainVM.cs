using DiplomaData.HelpInstrument.Filter;
using DiplomaData.Model;
using DiplomaData.Tabs.TabTable;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WPFMVVMHelper;

namespace DiplomaData
{

    public partial class MainVM
    {
        #region DiplomaData

         
        /// <summary>Подключение к базе данных</summary>
        public DiplomasDataContext DiplomaData { get; } = new DiplomasDataContext();

        public IQueryable<Group_rus> Groups => from g in DiplomaData.Group_rus select g;
        public IQueryable<Specialty_rus> Specialties => from s in DiplomaData.Specialty_rus select s;
        public IQueryable<Lecturer_rus> Lecturers => from l in DiplomaData.Lecturer_rus select l;
        public IQueryable<Thesis_rus> Theses => from t in DiplomaData.Thesis_rus select t;
        public IQueryable<Thesis_rus> FreeTheses => from t in DiplomaData.Thesis_rus where !t.Занята__не_занята select t;

        #endregion DiplomaData

        private void initDataBase()
        {
            #region InitReport
            Report = new Tabs.TabReport.TabReport
                ((q, e) => q.Where(w => (w.Student_rus.Имя + " " + w.Student_rus.Отчество + " " + w.Student_rus.Фамилия + " " + w.Thesis_rus.Название_темы).Contains(e))
                ,DiplomaData.Diplom_rus
                , "Протокол защиты дипломного проекта"
                );

            

            Report.AddFilterList("Группа:", Groups, d=>d.Student_rus.Group_rus);
            Report.AddFilterList("Руководитель:", Lecturers, d => d.Lecturer_rus);
            Report.AddFilterText("Оценка:", m => d => d.Оценка == m);
            Report.AddFilterDate("Дата сдачи:", d => s => d.date1 < s.Дата_сдачи && s.Дата_сдачи < d.date2);

            Report.AddSort("Руководитель:", d => d.Lecturer_rus.Фамилия + d.Lecturer_rus.Имя + d.Lecturer_rus.Отчество);
            Report.AddSort("Оценка:", d => d.Оценка);
            Report.AddSort("Дата:", d => d.Дата_сдачи);
            Report.AddSort("ФИО:", d => d.Student_rus.Фамилия + d.Student_rus.Имя + d.Student_rus.Отчество);
            Report.AddSort("Тема:", d => d.Thesis_rus.Название_темы);
            
            ReportTabs.Add(Report);
            #endregion

            #region AddTabs
            var student = DiplomaData.Student_rus.CreateTab
                (
                    (q, e) => q.Where(w => (w.Имя + " " + w.Отчество + " " + w.Фамилия).Contains(e))
                , "Студент"
                );

            student.AddFilterList("Группа:", Groups, s => s.Group_rus);
            student.AddFilterList("Руководитель:", Lecturers, s => s.Diplom_rus.Lecturer_rus);
            student.AddFilterText("Оценка:", m => s => s.Diplom_rus.Оценка == m);
            student.AddFilterDate("Дата сдачи:", d => s => d.date1 < s.Diplom_rus.Дата_сдачи && s.Diplom_rus.Дата_сдачи < d.date2);
           
            student.AddSort("Группа:", s => s.Group_rus.Номер_группы);
            student.AddSort("Руководитель:", s => s.Diplom_rus.Lecturer_rus.Фамилия + s.Diplom_rus.Lecturer_rus.Имя + s.Diplom_rus.Lecturer_rus.Отчество);
            student.AddSort("Оценка:", s => s.Diplom_rus.Оценка);
            student.AddSort("Дата:", s => s.Diplom_rus.Дата_сдачи);
            student.AddSort("ФИО:", s => s.Фамилия + s.Имя + s.Отчество);
            student.AddSort("Тема:", s => s.Diplom_rus.Thesis_rus.Название_темы);

            var Lecturer = DiplomaData.Lecturer_rus.CreateTab
                (
                (q, e) => q.Where(w => (w.Имя + " " + w.Отчество + " " + w.Фамилия).Contains(e))
                , "Преподаватель"
                );
            Lecturer.AddSort("ФИО:", l => l.Фамилия + l.Имя + l.Отчество);


            var Specialty = DiplomaData.Specialty_rus.CreateTab
                (
                (q, e) => q.Where(w => (w.Специальность + " " + w.Шифр_специальности).Contains(e))
                , "Специальность"
                );
            Specialty.AddSort("Шифр:", s => s.Шифр_специальности);
            Specialty.AddSort("Название:", s => s.Специальность);

            var Thesis = DiplomaData.Thesis_rus.CreateTab
               (
               (q, e) => q.Where(w => (w.Название_темы + " " + w.Описание).Contains(e))
               , "Тема диплома"
               );
            Thesis.AddFilterBool("Используемые:", b => t => t.Занята__не_занята == b);
            Thesis.AddFilterDate("Дата создания:", d => t => d.Item1 < t.Дата_выдачи && t.Дата_выдачи < d.Item2);
            Thesis.AddSort("Дата создания:", t => t.Дата_выдачи);
            Thesis.AddSort("Используется:", t => t.Занята__не_занята);
            Thesis.AddSort("Название:", t => t.Название_темы);
            //Thesis.Properties.Add(new Property("используются", () => Thesis.SelectData.Where(t => t.Занята__не_занята).Count()));
            //Thesis.Properties.Add(new Property("не используются", () => Thesis.SelectData.Where(t => !t.Занята__не_занята).Count()));
          
            var group = DiplomaData.Group_rus.CreateTab
                (
                (q, e) => q.Where(w => (w.Номер_группы + " " + w.Lecturer_rus.Фамилия + " " + w.Lecturer_rus.Имя + " " + w.Lecturer_rus.Отчество).Contains(e))
                , "Группа"
                );
            group.AddFilterList("Форма обучения:", DiplomaData.Form_of_education_rus, g => g.Form_of_education_rus);
            group.AddFilterList("Специальность:", Specialties,g => g.Specialty_rus);
            group.AddSort("Номер:", g => g.Номер_группы);
            group.AddSort("Куратор:", g => g.Lecturer_rus.Фамилия + g.Lecturer_rus.Имя + g.Lecturer_rus.Отчество);
            group.AddSort("Специальность", g => g.Specialty_rus.Специальность);

            Tables.Add(student);
            Tables.Add(Lecturer);
            Tables.Add(Specialty);
            Tables.Add(Thesis);
            Tables.Add(group);
            #endregion AddTabs

            AddEmptyDiploma = new lamdaCommand<Student_rus>
                (
                s =>
                {
                    foreach (Diplom_rus diplom in DiplomaData.Add_Empty_Diploma(s.id))
                        s.Diplom_rus = diplom;
                }
                );

        }
    }
}
