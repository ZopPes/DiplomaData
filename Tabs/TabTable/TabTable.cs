using DiplomaData.HelpInstrument;
using DiplomaData.HelpInstrument.Filter;
using DiplomaData.HelpInstrument.Sort;
using DiplomaData.Model;
using System;
using System.Collections;
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
    public class TabTable<T> : TabTable where T : class, New<T>, new()
    {
        #region Context

        /// <summary>Контекст данных</summary>
        public DataContext Context { get; }

        #endregion Context

        #region InsertItem

        private T insertItem;

        /// <summary>Переменная для добавления</summary>
        public  T InsertItem { get => insertItem; set => Set(ref insertItem, value); }

        #endregion InsertItem

        #region SelectData

        private IQueryable<T> selectData;

        /// <summary>данные для вывода</summary>
        public  IQueryable<T> SelectData { get => selectData; set =>Set(ref selectData, value); }

        #endregion SelectData


        public ICommand InsertTable { get; }
        public ICommand RemoveTable { get; }
        public ICommand UpdateTable { get; }

        public ICommand RefreshData { get; }


        public Func<IQueryable<T>, string, IQueryable<T>> TFilt { get; }

        public Table<T> Table;


        /// <summary>
        /// Вкладка с таблицей
        /// </summary>
        /// <param name="table">таблица данных</param>
        /// <param name="name">название вкладки</param>
        public TabTable
            (Table<T> table, Func<IQueryable<T>, string, IQueryable<T>> tFilt
            , string name = ""
            ) : base(name)
        {
            Table = table;
            SelectData = Table;
            Context = table.Context;
            InsertTable = new lamdaCommand(NewMethod);
            RemoveTable = new lamdaCommand<T>(Remove);
            UpdateTable = new lamdaCommand<T>(Update);
            RefreshData = new lamdaCommand(
                () =>
                    {
                        Context.Refresh(RefreshMode.OverwriteCurrentValues, Table); SelectData = Table;
                    });
            //Properties.Add(new Property("Количество строк", () => SelectData.Count()));
            InsertItem = new T();
            FilterChanged += TabTable_FilterChanged;
            TFilt = tFilt;
        }


        public void AddFilterList<TFilter>(string name, IQueryable<TFilter> queryable
            , Expression<Func<T, TFilter>> ex) => FilterParams.Add
                (
                    new FilterList<TabTable<T>, TFilter>
                    (
                        queryable, name
                        , t => SelectData = Table.Join
                            (
                                queryable.Where(r => r.Equals(t))
                                , ex, r => r, (d, g) => d)
                    )
                );


        public void AddFilterBool(string name, Func<bool, Expression<Func<T, bool>>> expression)
        {
            var filterList = new FilterBool(name
                , s => SelectData = Table.Where(expression?.Invoke(s)));
            FilterParams.Add
                (
                filterList
                );
        }
        public void AddFilterText(string name, Func<char, Expression<Func<T, bool>>> expression)
        {
            var filterList = new FilterMarcChar(name);
            filterList.SelectedChenget +=
                (obj, s) => SelectData = Table.Where(expression?.Invoke(s));
            FilterParams.Add
                (
               filterList
                );
        }

        public void AddFilterDate(string name, Func<(DateTime date1,DateTime date2),Expression<Func<T,bool>>> expression)
        {
            var filterList = new FilterDate(name
                , d => SelectData = Table.Where(expression?.Invoke(d)));

            FilterParams.Add
                (
                filterList
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
                         SelectData = Table;
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
            SortParams.Add(sort);
        }

        private void TabTable_FilterChanged(object sender, string e)
        {
            SelectData = TFilt?.Invoke(Table, e);
        }

        private void NewMethod()
        {
            try
            {
                Table.InsertOnSubmit(InsertItem);
                Context.SubmitChanges();
                Context.Refresh(RefreshMode.KeepCurrentValues, Table);
                Filter = Filter;
               
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message + "\r\n П ерепроверьте данные", ex.Number.ToString());
            }
            InsertItem =new T().New;
        }

        public void Remove(T value)
        {
            try
            {
                Table.DeleteOnSubmit(value);
                Context.SubmitChanges(ConflictMode.ContinueOnConflict);
                Context.Refresh(RefreshMode.OverwriteCurrentValues, Table);
                Filter = Filter;
            }
            catch (Exception e)
            {
                
                MessageBox.Show(e.Message, "Ошибка");
            }
        }

    }

    public class TabTable : Tab
    {

        public TabTable(string name = "") : base(name)
        {
            
        }



    }
}