﻿using DiplomaData.Model;
using System.Data.Linq;
using System.Linq;
namespace DiplomaData.Tabs.TabTable
{
    internal class TabStudent : TabTable<Student>
    {
        public TabStudent(Table<Student> students) : base(students, name: "Студенты")
        {
        }
    }
}