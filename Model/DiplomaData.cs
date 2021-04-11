using System;
using System.Collections.Generic;

namespace DiplomaData.Model
{
    partial class Specialty_Thesis
    {
    }

    partial class Group
    {
        public override string ToString()
        => number;
    }

    partial class Form_of_education 
    {
        public override string ToString()
        => name;
    }

    partial class Specialty
    {

        public override string ToString()
        => name;

    }

    partial class Lecturer 
    {



        public override string ToString()
        => string.Join(" ", surname, name, patronymic);

    }

    partial class Student : ICloneable
    {
        public object Clone() =>  new Student()
        {
            Diploma = this.Diploma
                ,
            Group = this.Group
                ,
            Group_number = this.Group_number
                ,
            patronymic = this.patronymic
                ,
            name = this.name
                ,
            surname = this.surname
        };

        public override string ToString()
        => string.Join(" ", surname, name, patronymic);

      
    }

    partial class Thesis
    {
      
        public override int GetHashCode()
        => id;

        public override string ToString()
        => name;

    }
}