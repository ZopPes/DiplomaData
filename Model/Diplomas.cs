using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DiplomaData.Model
{
    partial class Student_rus
    {
        public override string ToString()
        {
            
            return $"{_Фамилия} {_Имя} {_Отчество}";
        }
    }

    partial class Diplom_rus 
    {
        public bool IsError =>
            this.Thesis_rus == null || this.Lecturer_rus == null || Оценка == '0';

        public override string ToString()
        {
            return this.Тема.ToString();
        }
    }

    partial class Lecturer_rus 
    {

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

    partial class Specialty_rus 
    {
        public string FormatШифр_специальности => _Шифр_специальности.Insert(2, ".").Insert(5, ".");

        public override string ToString()
        {
            return $"{FormatШифр_специальности} {_Специальность}";
        }
    }

    partial class Thesis_rus
    {
        public override string ToString()
        {
            return _Название_темы;
        }
    }

    partial class Group_rus
    {
        public override string ToString()
        {
            return _Номер_группы;
        }


    }

    
}