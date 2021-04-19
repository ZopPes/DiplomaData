using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Input;
using WPFMVVMHelper;

namespace DiplomaData.Tabs.TabTable
{
    /// <summary>
    /// Вкладка с таблицей
    /// </summary>
    /// <typeparam name="T">тип для таблицы</typeparam>
    //public class TabTable<T> : Tab, ITable<T>, INotifyCollectionChanged where T : class, new()
    //{
    //    #region Table
    //    /// <summary>Таблица данных</summary>
    //    public Table<T> Table { get;}
    //    #endregion Table


    //    #region InsertItem
    //    private T insertItem;
    //    /// <summary>Переменная для добавления</summary>
    //    public T InsertItem { get => insertItem; set => Set(ref insertItem, value); }
    //    #endregion

    //    public ICommand InsertTable { get; }
    //    public ICommand RemoveTable { get; }

    //    public Expression Expression => ((IQueryable)Table).Expression;

    //    public Type ElementType => ((IQueryable)Table).ElementType;

    //    public IQueryProvider Provider => ((IQueryable)Table).Provider;


    //    /// <summary>
    //    /// Вкладка с таблицей
    //    /// </summary>
    //    /// <param name="table">таблица данных</param>
    //    /// <param name="name">название вкладки</param>
    //    public TabTable(Table<T> table, string name = "") : base(name)
    //    {
    //        Table = table;
            
    //        InsertTable = new lamdaCommand(NewMethod);
    //        RemoveTable = new lamdaCommand<T>(DeleteOnSubmit);
    //        Properties.Add(new Property("Количество строк", () => Table.Count()));
    //        InsertItem = new T();
    //    }

    //    public event NotifyCollectionChangedEventHandler CollectionChanged;

    //    private void NewMethod()    
    //    {

    //        InsertOnSubmit(InsertItem);
    //        InsertItem = new T();
            
    //    }

    //    public void InsertOnSubmit(T entity)
    //    {
    //        ((ITable<T>)Table).InsertOnSubmit(entity);
    //        Table.Context.SubmitChanges();
    //        CollectionChanged?.Invoke
    //            (
    //            this
    //            , new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, entity)
    //            );
    //    }

    //    public void Attach(T entity)
    //    {
    //        ((ITable<T>)Table).Attach(entity);
    //    }

    //    public void DeleteOnSubmit(T entity)
    //    {
    //        var r = this.ToList().IndexOf(entity);

    //        ((ITable<T>)Table).DeleteOnSubmit(entity);
    //        CollectionChanged?.Invoke
    //            (
    //            this
    //            , new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove,entity,r)
    //            );
    //        Table.Context.SubmitChanges();
    //    }

    //    public IEnumerator<T> GetEnumerator()
    //    {
    //        return ((IEnumerable<T>)Table).GetEnumerator();
    //    }

    //    IEnumerator IEnumerable.GetEnumerator()
    //    {
    //        return ((IEnumerable)Table).GetEnumerator();
    //    }
    //}

    public class TabTable<T> : Tab, IBindingList where T : class, new()
    {
        #region Table
        /// <summary>Таблица данных</summary>
        public IBindingList Table { get; }
        #endregion Table

        #region Context
        /// <summary>Контекст данных</summary>
        public DataContext Context { get; }
        #endregion

        #region InsertItem
        private T insertItem;
        /// <summary>Переменная для добавления</summary>
        public T InsertItem { get => insertItem; set => Set(ref insertItem, value); }
        #endregion

        public ICommand InsertTable { get; }
        public ICommand RemoveTable { get; }

        public bool AllowNew => Table.AllowNew;

        public bool AllowEdit => Table.AllowEdit;

        public bool AllowRemove => Table.AllowRemove;

        public bool SupportsChangeNotification => Table.SupportsChangeNotification;

        public bool SupportsSearching => Table.SupportsSearching;

        public bool SupportsSorting => Table.SupportsSorting;

        public bool IsSorted => Table.IsSorted;

        public PropertyDescriptor SortProperty => Table.SortProperty;

        public ListSortDirection SortDirection => Table.SortDirection;

        public bool IsReadOnly => Table.IsReadOnly;

        public bool IsFixedSize => Table.IsFixedSize;

        public int Count => Table.Count;

        public object SyncRoot => Table.SyncRoot;

        public bool IsSynchronized => Table.IsSynchronized;

        public object this[int index] { get => Table[index]; set => Table[index] = value; }

        /// <summary>
        /// Вкладка с таблицей
        /// </summary>
        /// <param name="table">таблица данных</param>
        /// <param name="name">название вкладки</param>
        public TabTable(Table<T> table, string name = "") : base(name)
        {
            Table = table.GetNewBindingList();
            Context = table.Context;
            InsertTable = new lamdaCommand(NewMethod);
            RemoveTable = new lamdaCommand<T>(Remove);
            Properties.Add(new Property("Количество строк", () => Table.Count));
            InsertItem = new T();
            
        }

        public event ListChangedEventHandler ListChanged
        {
            add
            {
                Table.ListChanged += value;
            }

            remove
            {
                Table.ListChanged -= value;
            }
        }


        private void NewMethod()
        {
            var nel=Add(InsertItem);
            Context.SubmitChanges();
            InsertItem = new T();
        }

        public object AddNew()
        {
            return Table.AddNew();
        }

        public void AddIndex(PropertyDescriptor property)
        {
            Table.AddIndex(property);
        }

        public void ApplySort(PropertyDescriptor property, ListSortDirection direction)
        {
            Table.ApplySort(property, direction);
        }

        public int Find(PropertyDescriptor property, object key)
        {
            return Table.Find(property, key);
        }

        public void RemoveIndex(PropertyDescriptor property)
        {
            Table.RemoveIndex(property);
        }

        public void RemoveSort()
        {
            Table.RemoveSort();
        }

        public int Add(object value)
        {
            return Table.Add(value);
        }

        public bool Contains(object value)
        {
            return Table.Contains(value);
        }

        public void Clear()
        {
            Table.Clear();
        }

        public int IndexOf(object value)
        {
            return Table.IndexOf(value);
        }

        public void Insert(int index, object value)
        {
            Table.Insert(index, value);
        }

        public void Remove(object value)
        {
            var r = this.IndexOf(value);
            Table.RemoveAt(r);
            Context.SubmitChanges();
        }

        public void RemoveAt(int index)
        {
            Table.RemoveAt(index);
        }

        public void CopyTo(Array array, int index)
        {
            Table.CopyTo(array, index);
        }

        public IEnumerator GetEnumerator()
        {
            return Table.GetEnumerator();
        }
    }
}