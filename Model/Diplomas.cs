using System;
using System.Collections.Generic;

namespace DiplomaData.Model
{
   public interface New<out T>
    {
         T New { get; }
    }

    partial class Commission_rus
    {
    }

    public partial class Student_rus : New<Student_rus>
    {
        public Student_rus New { get { id = GetHashCode(); return this; } }
        public override string ToString() => $"{_Фамилия} {_Имя} {_Отчество}";

        public Student_rus(object newid) : this() => id =(int) newid;

    }

    partial class Diplom_rus : New<Diplom_rus>
    {
        public bool IsError =>
            this.Thesis_rus == null || this.Lecturer_rus == null || Оценка == '0';
        public string MyProperty { get; set; }

        public Diplom_rus New { get { id = GetHashCode();return this; } }

        public override string ToString()
        {
            return this.Тема.ToString();
        }
    }

    partial class Lecturer_rus :New<Lecturer_rus>
    {
        public Lecturer_rus New { get { id = GetHashCode();return this; } }

        public override string ToString()
        {
            return $"{_Фамилия} {_Имя} {_Отчество}";
        }
    }

    partial class Form_of_education_rus
    {
        public override string ToString()
        {
            return this._Форма_обучения;
        }
    }

    partial class Specialty_rus : New<Specialty_rus>
    {
        public string FormatШифр_специальности => _Шифр_специальности.Insert(2, ".").Insert(5, ".");

        public Specialty_rus New => this;

        public override string ToString()
        {
            return $"{FormatШифр_специальности} {_Специальность}";
        }
    }

    partial class Thesis_rus : New<Thesis_rus>
    {
        public Thesis_rus New { get { id = GetHashCode();return this; } }

        public override string ToString()
        {
            return _Название_темы;
        }
    }

    partial class Group_rus : New<Group_rus>
    {
        public Group_rus New => this;

        public override string ToString()
        {
            return _Номер_группы;
        }
    }
}