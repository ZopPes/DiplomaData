using DiplomaData.Model;
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
    public class TabTable<T> : Tab where T : class, new()
    {

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
        public IQueryable<T> SelectData { get => selectData; set { if (Set(ref selectData, value)) OnProperties(); } }
        #endregion

        public ICommand InsertTable { get; }
        public ICommand RemoveTable { get; }
        public ICommand UpdateTable { get; }

        public Action<T> InsertData { get; }
        public Action<T> DeleteData { get; }

        public List<IFilterParam> FilterParam { get; }

        
        public IEnumerable FilterParams { get; }
            
        public Func<IQueryable<T>,string,IQueryable<T>> TFilt { get; }

        public Table<T> test;

        /// <summary>
        /// Вкладка с таблицей
        /// </summary>
        /// <param name="table">таблица данных</param>
        /// <param name="name">название вкладки</param>
        public TabTable
            (Table<T> table, Func<IQueryable<T>, string, IQueryable<T>> tFilt
            ,Action<T> insertData, Action<T> deleteData
            , string name = ""
            ,params IFilterParam[] filterParams
            ) : base(name)
        {
            test = table;
            SelectData = test;
            Context = table.Context;
            InsertTable = new lamdaCommand(NewMethod);
            RemoveTable = new lamdaCommand<T>(Remove);
            UpdateTable = new lamdaCommand<T>(Update);
            Properties.Add(new Property("Количество строк", () => SelectData.Count()));
            InsertItem = new T();
            InsertData = insertData;
            DeleteData = deleteData;

            FilterParam = new List<IFilterParam>();
            if (filterParams != null)
            {

                foreach (var item in filterParams)
                {

                    if (item != null)
                    {
                        FilterParam.Add(item);
                        item.FilterChanged +=
                            (obj, g) => SelectData = g.Queryable as IQueryable<T>;
                    }
                }
            }
            FilterChanged += TabTable_FilterChanged;
            TFilt = tFilt;
        }

        private void Update(T value)
        {
            try
            {
                Context.SubmitChanges();
                Context.Refresh(RefreshMode.OverwriteCurrentValues, value);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка");
            }
        }

        private void TabTable_FilterChanged(object sender, string e)
        {
            SelectData = TFilt?.Invoke(test,e);
        }

        private void NewMethod()
        {
            try
            {
                InsertData?.Invoke(InsertItem);
                Context.Refresh(RefreshMode.KeepCurrentValues, test);
                Filter = Filter;
            }
            catch (SqlException ex)
            {
                    MessageBox.Show(ex.Message+ "\r\n П ерепроверьте данные",ex.Number.ToString());
            }
            InsertItem = new T();
        }


        public void Remove(object value)
        {
            try
            {
                DeleteData?.Invoke((T) value);
                Context.Refresh(RefreshMode.OverwriteCurrentValues,test);
                Filter = Filter;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка");
            }
        }

    }

    
    public class FilterMarcParam : FilterParam<char>
    {
        public FilterMarcParam(Func<char, IQueryable> filter, string name = "")
            :base(filter,name)
        {
            this.Filter = '0';
        }

    }

    public class FilterDateParam :FilterParam<DateTime>
    {
        public FilterDateParam(Func<DateTime, IQueryable> filter, string name = "") 
            :base(filter,name)
        {
            Filter = DateTime.Now;
        }

    }

    public class FilterTableParam :FilterParam
    {
        public IQueryable Tin { get; }
        /// <summary>MyComment</summary>
        public FilterTableParam(IQueryable tin, Func<object, IQueryable> filter, string name = "")
            :base(filter,name)
        {
            Tin = tin;
        }
       
    }

    public class FilterParam : peremlog, IFilterParam
    {

        public event EventHandler<IFilterEventQuery> FilterChanged;
        #region Filter
        private object filter;
        /// <summary>MyComment</summary>
        public object Filter
        {
            get => filter;
            set { if (Set(ref filter, value)) FilterChanged?.Invoke(this, new FilterEventQuery(Func?.Invoke(value))); }
        }
        #endregion


        public string Name { get; }

        public Func<object, IQueryable> Func { get; }

        public FilterParam(Func<object, IQueryable> filter, string name = "")
        {
            Func = filter;
            Name = name;
        }

    }

    public class FilterParam<T> : peremlog, IFilterParam
    {

        public event EventHandler<IFilterEventQuery> FilterChanged;
        #region Filter
        private T filter;
        /// <summary>MyComment</summary>
        public T Filter
        {
            get => filter;
            set { if (Set(ref filter, value)) FilterChanged?.Invoke(this, new FilterEventQuery(Func?.Invoke(value))); }
        }
        #endregion


        public string Name { get; }

        public Func<T, IQueryable> Func { get; }

        public FilterParam(Func<T, IQueryable> filter, string name = "")
        {
            Func = filter;
            Name = name;
        }

    }
}