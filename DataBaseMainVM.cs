using DiplomaData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            Report.TFilt = (q, e) => q.Where(w => (w.Student_rus.Имя + " " + w.Student_rus.Отчество + " " + w.Student_rus.Фамилия + " " + w.Thesis_rus.Название_темы).Contains(e));
            Report.Diploms = DiplomaData.Diplom_rus;

            Report.AddFilterList("Группа:", Groups, g => d => d.Student_rus.Group_rus == g);
            Report.AddFilterList("Руководитель:", Lecturers, l => d => d.Lecturer_rus == l);
            Report.AddFilterText("Оценка:", m => d => d.Оценка == m);
            Report.AddFilterDate("Дата сдачи:", date => d => date.Item1 < d.Дата_сдачи && d.Дата_сдачи < date.Item2);

            Report.AddSort("Оценка:", d => d.Оценка);
            Report.AddSort("ФИО:", d => d.Student_rus.Фамилия + d.Student_rus.Имя + d.Student_rus.Отчество);
            Report.AddSort("Руководитель:", d => d.Lecturer_rus.Фамилия + d.Lecturer_rus.Имя + d.Lecturer_rus.Отчество);
            Report.AddSort("Дата:", d => d.Дата_сдачи);
            Report.AddSort("Тема:", d => d.Thesis_rus.Название_темы);

            ReportTabs.Add(Report);
            #endregion

            #region AddTabs

            var student = DiplomaData.Student_rus.CreateTabTable
                (
                    (q, e) => q.Where(w => (w.Имя + " " + w.Отчество + " " + w.Фамилия).Contains(e))
                , s => DiplomaData.Add_Student(s.Фамилия, s.Имя, s.Отчество, s.Номер_группы)
                , s => DiplomaData.Delete_remotelt_student(s.id)
                , "Студент"
                );

            student.AddFilterList("Группа:", Groups, g => s => s.Group_rus == g);
            student.AddFilterList("Руководитель:", Lecturers, l => s => s.Diplom_rus.Lecturer_rus == l);
            student.AddFilterText("Оценка:", m => s => s.Diplom_rus.Оценка == m);
            student.AddFilterDate("Дата сдачи:", d => s => d.Item1 < s.Diplom_rus.Дата_сдачи && s.Diplom_rus.Дата_сдачи < d.Item2);
           
            student.AddSort("Группа:", s => s.Group_rus.Номер_группы);
            student.AddSort("Руководитель:", s => s.Diplom_rus.Lecturer_rus.Фамилия + s.Diplom_rus.Lecturer_rus.Имя + s.Diplom_rus.Lecturer_rus.Отчество);
            student.AddSort("Оценка:", s => s.Diplom_rus.Оценка);
            student.AddSort("Дата:", s => s.Diplom_rus.Дата_сдачи);
            student.AddSort("ФИО:", s => s.Фамилия + s.Имя + s.Отчество);
            student.AddSort("Тема:", s => s.Diplom_rus.Thesis_rus.Название_темы);

            var Lecturer = DiplomaData.Lecturer_rus.CreateTabTable
                (
                (q, e) => q.Where(w => (w.Имя + " " + w.Отчество + " " + w.Фамилия).Contains(e))
                , l => DiplomaData.Add_Lecturer(l.Фамилия, l.Имя, l.Отчество)
                , l => DiplomaData.Delete_remotelt_lecturer(l.id)
                , "Преподователь"
                );
            Lecturer.AddSort("ФИО:", l => l.Фамилия + l.Имя + l.Отчество);


            var Specialty = DiplomaData.Specialty_rus.CreateTabTable
                (
                (q, e) => q.Where(w => (w.Специальность + " " + w.Шифр_специальности).Contains(e))
                , s => DiplomaData.Add_Specialty(s.Шифр_специальности, s.Специальность)
                , s => DiplomaData.Delete_remotelt_specialty(s.Шифр_специальности)
                , "Специальность"
                );
            Specialty.AddSort("Шифр:", s => s.Шифр_специальности);
            Specialty.AddSort("Название:", s => s.Специальность);

            var Thesis = DiplomaData.Thesis_rus.CreateTabTable
               (
               (q, e) => q.Where(w => (w.Название_темы + " " + w.Описание).Contains(e))
                , t => DiplomaData.Add_Thesis(t.Название_темы, t.Описание, t.Дата_выдачи)
                , t => DiplomaData.Delete_remotelt_Thesis(t.id)
               , "Тема диплома"
               );
            Thesis.AddFilterBool("Используемые:", b => t => t.Занята__не_занята == b);
            Thesis.AddFilterDate("Дата создания:", d => t => d.Item1 < t.Дата_выдачи && t.Дата_выдачи < d.Item2);
            Thesis.AddSort("Дата создания:", t => t.Дата_выдачи);
            Thesis.AddSort("Используется:", t => t.Занята__не_занята);
            Thesis.AddSort("Название:", t => t.Название_темы);
            Thesis.Properties.Add(new Property("используются", () => Thesis.SelectData.Where(t => t.Занята__не_занята).Count()));
            Thesis.Properties.Add(new Property("не используются", () => Thesis.SelectData.Where(t => !t.Занята__не_занята).Count()));
          
            var group = DiplomaData.Group_rus.CreateTabTable
                (
                (q, e) => q.Where(w => (w.Номер_группы + " " + w.Lecturer_rus.Фамилия + " " + w.Lecturer_rus.Имя + " " + w.Lecturer_rus.Отчество).Contains(e))
                , g =>
                { DiplomaData.Add_Group(g.Номер_группы, g.Специальность, g.Куратор, g.Форма_обучения); OnPropertyChanged(nameof(Groups)); }
                , g => DiplomaData.Delete_remotelt_group(g.Номер_группы)
                , "Группа"
                );
            group.AddFilterList("Форма обучения:", DiplomaData.Form_of_education_rus, f => g => g.Form_of_education_rus == f);
            group.AddFilterList("Специальность:", Specialties, s => g => g.Specialty_rus == s);
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
