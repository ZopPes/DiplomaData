using System;
using System.Collections.Generic;

namespace DiplomaData.Model
{
    partial class Specialty_Thesis
    {
    }

    partial class Group : IEquatable<Group>
    {
        public override bool Equals(object obj) => Equals(obj as Group);

        public bool Equals(Group other) => other != null &&
                   number == other.number;

        public override int GetHashCode() => 769468832 + EqualityComparer<string>.Default.GetHashCode(number);

        public static bool operator ==(Group left, Group right) => EqualityComparer<Group>.Default.Equals(left, right);

        public static bool operator !=(Group left, Group right) => !(left == right);

        public override string ToString()
        => number;
    }

    partial class Form_of_education : IEquatable<Form_of_education>
    {
        public override bool Equals(object obj) => Equals(obj as Form_of_education);

        public bool Equals(Form_of_education other) => other != null &&
                   id == other.id;

        public override int GetHashCode()
        => id;

        public override string ToString()
        => name;

        public static bool operator ==(Form_of_education left, Form_of_education right)
        {
            return EqualityComparer<Form_of_education>.Default.Equals(left, right);
        }

        public static bool operator !=(Form_of_education left, Form_of_education right)
        => !(left == right);
    }

    partial class Specialty : IEquatable<Specialty>
    {
        public override bool Equals(object obj)
        => Equals(obj as Specialty);

        public bool Equals(Specialty other) => other != null &&
                   cipher == other.cipher;

        public override int GetHashCode()
        => 529879002 + EqualityComparer<string>.Default.GetHashCode(cipher);

        public override string ToString()
        => name;

        public static bool operator ==(Specialty left, Specialty right) => EqualityComparer<Specialty>.Default.Equals(left, right);

        public static bool operator !=(Specialty left, Specialty right)
        => !(left == right);
    }

    partial class Lecturer : IEquatable<Lecturer>
    {
        public override bool Equals(object obj)
        => Equals(obj as Lecturer);

        public bool Equals(Lecturer other) => other != null &&
                   id == other.id;

        public override int GetHashCode()
        => id;

        public override string ToString()
        => string.Join(" ", surname, name, patronymic);

        public static bool operator ==(Lecturer left, Lecturer right) => EqualityComparer<Lecturer>.Default.Equals(left, right);

        public static bool operator !=(Lecturer left, Lecturer right)
        => !(left == right);
    }

    partial class Student : IEquatable<Student>
    {
        public override bool Equals(object obj)
        => Equals(obj as Student);

        public bool Equals(Student other) => other != null &&
                   id == other.id;

        public override int GetHashCode()
        => id;

        public override string ToString()
        => string.Join(" ", surname, name, patronymic);

        public static bool operator ==(Student left, Student right) => EqualityComparer<Student>.Default.Equals(left, right);

        public static bool operator !=(Student left, Student right)
        => !(left == right);
    }

    partial class Thesis : IEquatable<Thesis>
    {
        public override bool Equals(object obj)
        => Equals(obj as Thesis);

        public bool Equals(Thesis other) => other != null &&
                   id == other.id;

        public override int GetHashCode()
        => id;

        public override string ToString()
        => name;

        public static bool operator ==(Thesis left, Thesis right) => EqualityComparer<Thesis>.Default.Equals(left, right);

        public static bool operator !=(Thesis left, Thesis right) => !(left == right);
    }
}