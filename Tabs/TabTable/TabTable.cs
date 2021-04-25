using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
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

    public class TabTable<T> : Tab where T : class, new()
    {
        #region Table
        /// <summary>Таблица данных</summary>
        //public IBindingList Table { get; }
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


        #region SelectData
        private IQueryable<T> selectData;
        /// <summary>данные для вывода</summary>
        public IQueryable<T> SelectData { get => selectData; set => Set(ref selectData, value); }
        #endregion

        public ICommand InsertTable { get; }
        public ICommand RemoveTable { get; }


        public Table<T> test;

        /// <summary>
        /// Вкладка с таблицей
        /// </summary>
        /// <param name="table">таблица данных</param>
        /// <param name="name">название вкладки</param>
        public TabTable(Table<T> table, string name = "") : base(name)
        {
            test = table;
            SelectData = test;
            Context = table.Context;
            InsertTable = new lamdaCommand(NewMethod);
            RemoveTable = new lamdaCommand<T>(Remove);
            Properties.Add(new Property("Количество строк", () => SelectData.ToString()));
            InsertItem = new T();
            
        }

        private void NewMethod()
        {
            test.InsertOnSubmit(InsertItem);
            try
            {
                Context.SubmitChanges();
                Filter = "";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка");
            }
            InsertItem = new T();
        }


        public void Remove(object value)
        {
            test.DeleteOnSubmit((T)value);
            try
            {
                Context.SubmitChanges();
                Filter = "";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка");
            }
        }



    }
}