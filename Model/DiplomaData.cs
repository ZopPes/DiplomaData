using System;
using System.Collections.Generic;

namespace DiplomaData.Model
{
    partial class Specialty_Thesis
    {
    }

    partial class Group : ICloneable
    {
        public object Clone() => new Group()
        {
            Form_of_education1 = this.Form_of_education1
            ,
            Lecturer = this.Lecturer
            ,
            number = this._number
            ,
            Specialty1 = this.Specialty1
            ,
            Student = this._Student
        };

        public override string ToString()
        => number;
    }

    partial class Form_of_education : ICloneable
    {
        public object Clone() => new Form_of_education()
        {
            Group = this.Group
                ,
            name = this.name
                ,
            id = this.id
 
        };

        public override string ToString()
        => name;
    }

    partial class Specialty : ICloneable
    {
        public object Clone() => new Specialty()
        {
            Group = this.Group
                ,
            cipher = this._cipher
                ,
            Specialty_Thesis = this.Specialty_Thesis
                ,
            name = this.name
                ,
            Commission_Specialty = this.Commission_Specialty
        };

        public override string ToString()
        => name;

    }

    partial class Lecturer : ICloneable
    {
        public object Clone() => new Lecturer()
        {
            Diploma = this.Diploma
               ,
            Group = this.Group
               ,
            name = this.name
               ,
            patronymic = this.patronymic
               ,
            surname = this.surname
        };

        public override string ToString()
        => string.Join(" ", surname, name, patronymic);

    }

    partial class Student 
    {
       

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