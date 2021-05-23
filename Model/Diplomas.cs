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
        public override string ToString() => $"{_�������} {_���} {_��������}";

        public Student_rus(object newid) : this() => id =(int) newid;

    }

    partial class Diplom_rus : New<Diplom_rus>
    {
        public bool IsError =>
            this.Thesis_rus == null || this.Lecturer_rus == null || ������ == '0';
        public string MyProperty { get; set; }

        public Diplom_rus New { get { id = GetHashCode();return this; } }

        public override string ToString()
        {
            return this.����.ToString();
        }
    }

    partial class Lecturer_rus :New<Lecturer_rus>
    {
        public Lecturer_rus New { get { id = GetHashCode();return this; } }

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

    partial class Specialty_rus : New<Specialty_rus>
    {
        public string Format����_������������� => _����_�������������.Insert(2, ".").Insert(5, ".");

        public Specialty_rus New => this;

        public override string ToString()
        {
            return $"{Format����_�������������} {_�������������}";
        }
    }

    partial class Thesis_rus : New<Thesis_rus>
    {
        public Thesis_rus New { get { id = GetHashCode();return this; } }

        public override string ToString()
        {
            return _��������_����;
        }
    }

    partial class Group_rus : New<Group_rus>
    {
        public Group_rus New => this;

        public override string ToString()
        {
            return _�����_������;
        }
    }
}