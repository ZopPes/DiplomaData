using DiplomaData.HelpInstrument;
using DiplomaData.HelpInstrument.Filter;
using DiplomaData.HelpInstrument.Sort;
using System;
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

        #endregion Context

        #region InsertItem

        private T insertItem;

        /// <summary>Переменная для добавления</summary>
        public T InsertItem { get => insertItem; set => Set(ref insertItem, value); }

        #endregion InsertItem

        #region SelectData

        private IQueryable<T> selectData;

        /// <summary>данные для вывода</summary>
        public IQueryable<T> SelectData { get => selectData; set { if (Set(ref selectData, value)) OnProperties(); } }

        #endregion SelectData

        #region SortData

        private IQueryable<T> sortData;

        /// <summary>Отсортированный список</summary>
        public IQueryable<T> SortData { get => sortData; set => Set(ref sortData, value); }

        #endregion SortData

        public ICommand InsertTable { get; }
        public ICommand RemoveTable { get; }
        public ICommand UpdateTable { get; }

        public Action<T> InsertData { get; }
        public Action<T> DeleteData { get; }

        public Func<IQueryable<T>, string, IQueryable<T>> TFilt { get; }

        public Table<T> test;

        public IHelpInstrument DeleteMy { get => FilterParam[0]; }

        /// <summary>
        /// Вкладка с таблицей
        /// </summary>
        /// <param name="table">таблица данных</param>
        /// <param name="name">название вкладки</param>
        public TabTable
            (Table<T> table, Func<IQueryable<T>, string, IQueryable<T>> tFilt
            , Action<T> insertData, Action<T> deleteData
            , string name = ""
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
            FilterChanged += TabTable_FilterChanged;
            TFilt = tFilt;
        }

        public void AddFilterList<TFilter>(string name, IQueryable<TFilter> queryable, Func<TFilter, Expression<Func<T, bool>>> expression)
        {
            var filterList = new FilterList(queryable);
            filterList.SelectedChenget += (obj, s) => SelectData = test.Where(expression?.Invoke((TFilter)s));
            FilterParam.Add
                (
                new FilterProp(name, filterList)
                );
        }

        public void AddFilterBool(string name, Func<bool, Expression<Func<T, bool>>> expression)
        {
            var filterList = new FilterBool();
            filterList.SelectedChenget +=
                (obj, s) => SelectData = test.Where(expression?.Invoke(s));
            FilterParam.Add
                (
                new FilterProp(name, filterList)
                );
        }
        public void AddFilterText(string name, Func<char, Expression<Func<T, bool>>> expression)
        {
            var filterList = new FilterMarcChar();
            filterList.SelectedChenget +=
                (obj, s) => SelectData = test.Where(expression?.Invoke(s));
            FilterParam.Add
                (
                new FilterProp(name, filterList)
                );
        }

        public void AddFilterDate(string name, Func<(DateTime, DateTime), Expression<Func<T, bool>>> expression)
        {
            var filterList = new FilterDate();
            filterList.SelectedChenget +=
                (obj, d) => SelectData = test.Where(expression?.Invoke(d));
            FilterParam.Add
                (
                new FilterProp(name, filterList)
                );
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

        internal void AddSort<TKey>(string name, Expression<Func<T, TKey>> expression)
        {
            var sort = new SortProp(name);
            sort.StatusChenget += (o, s) =>
             {
                 switch (s)
                 {
                     case SortStatus.off:
                         SelectData = test;
                         break;

                     case SortStatus.desc:
                         SelectData = selectData.OrderByDescending(expression);
                         break;

                     case SortStatus.asc:
                         SelectData = SelectData.OrderBy(expression);
                         break;

                     default:
                         break;
                 }
             };
            SortParam.Add(sort);
        }

        private void TabTable_FilterChanged(object sender, string e)
        {
            SelectData = TFilt?.Invoke(test, e);
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
                MessageBox.Show(ex.Message + "\r\n П ерепроверьте данные", ex.Number.ToString());
            }
            InsertItem = new T();
        }

        public void Remove(object value)
        {
            try
            {
                DeleteData?.Invoke((T)value);
                Context.Refresh(RefreshMode.OverwriteCurrentValues, test);
                Filter = Filter;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка");
            }
        }

    }
}