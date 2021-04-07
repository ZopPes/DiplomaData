using DiplomaData.Model;
using System;
using System.Collections.ObjectModel;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Collections.Generic;
using WPFMVVMHelper;
using System.Windows.Input;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;

namespace DiplomaData.Tabs.TabTable
{
    public class TabThesis : TabTable<Thesis>
    {
        #region SelectedTable

        private Thesis SelectedThesis;

        /// <summary>Выбранная тема</summary>
        public Thesis SelectedTable
        {
            get => SelectedThesis;
            set
            {
                if (Set(ref SelectedThesis, value))
                {
                    foreach (var propertie in Properties)
                    {
                        propertie.ValueOnPropertyChanged();
                    }
                    
                    Specialties =new Con1<Specialty_Thesis>(value.Specialty_Thesis);
                    OnPropertyChanged(nameof(Specialties));
                }

            }
        }

        #endregion SelectedTable

        public Con1<Specialty_Thesis> Specialties { get; set; }

        public TabThesis(Table<Thesis> theses) : base(theses, name: "Темы")
        {
            Properties.Add(new Propertie("Количество занятых тем", () => Table.Where(t => t.used).Count()));
            Properties.Add(new Propertie("Количество свободных тем", () => Table.Where(t => !t.used).Count()));
            
        }

       
        
    }

    public class Con<T> : IList<T> where T:class
    {
        public string name { get; }

        public EntitySet<T> ts { get; set; }

        public ICommand AddCommand { get; }

        public ICommand RemoveCommand { get; }

        public int Count => ts.Count;

        public bool IsReadOnly => false;

        public T this[int index] { get => ts[index]; set => ts[index] = value; }

        public Con(EntitySet<T> ts)
        {
            this.ts = ts;
            AddCommand = new lamdaCommand<T>(Add);
            RemoveCommand = new lamdaCommand<T>(t=>Remove(t));
            name = "Специальность";
        }


        public int IndexOf(T item)
        {
            return ts.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            ts.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            ts.RemoveAt(index);
        }

        public void Add(T item)
        {
            ts.Add(item);
        }

        public void Clear()
        {
            ts.Clear();
        }

        public bool Contains(T item)
        {
            return ts.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            ts.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return ts.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ts.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)ts).GetEnumerator();
        }
    }

    public class Con1<T> : ObservableCollection<T> where T : class
    {
        public string name { get; }


        public ICommand AddCommand { get; }

        public ICommand RemoveCommand { get; }



        public Con1(EntitySet<T> ts):base(ts)
        {
            AddCommand = new lamdaCommand<T>(Add);
            RemoveCommand = new lamdaCommand<T>(t => Remove(t));
            name = "Специальность";
        }


    }
}