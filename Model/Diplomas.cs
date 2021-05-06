using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DiplomaData.Model
{
    partial class Student_rus
    {
        public override string ToString()
        {
            
            return $"{_�������} {_���} {_��������}";
        }
    }

    partial class Diplom_rus 
    {
        public bool IsError =>
            this.Thesis_rus == null || this.Lecturer_rus == null || ������ == '0';

        public override string ToString()
        {
            return this.����.ToString();
        }
    }

    partial class Lecturer_rus 
    {

        public override string ToString()
        {
            return $"{_�������} {_���} {_��������}";
        }
    }

    partial class Form_of_education_rus
    {
        public override string ToString()
        {
            return this._�����_��������;
        }
    }

    partial class Specialty_rus 
    {
        public string Format����_������������� => _����_�������������.Insert(2, ".").Insert(5, ".");

        public override string ToString()
        {
            return $"{Format����_�������������} {_�������������}";
        }
    }

    partial class Thesis_rus
    {
        public override string ToString()
        {
            return _��������_����;
        }
    }

    partial class Group_rus
    {
        public override string ToString()
        {
            return _�����_������;
        }


    }

    
}