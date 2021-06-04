using DiplomaData.HelpInstrument;
using DiplomaData.HelpInstrument.Command;
using DiplomaData.HelpInstrument.Filter;
using DiplomaData.HelpInstrument.Sort;
using DiplomaData.HelpInstrument.Status;
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
        #region InsertItem
        private T insertItem;
        /// <summary>Переменная для добавления</summary>
        public T InsertItem { get => insertItem; set => Set(ref insertItem, value); }
        #endregion InsertItem

        #region SelectData
        private IQueryable<T> selectData;
        /// <summary>данные для вывода</summary>
        public IQueryable<T> SelectData
        {
            get => selectData;
            set {if (Set(ref selectData, value)) OnPropertyChangedAllStatusProps();}
        }
        #endregion SelectData

        public ICommand InsertTable { get; }
        public ICommand RemoveTable { get; }
        public ICommand RefreshData { get; }

        public IQueryable<T> TableT => Table.OfType<T>();

        /// <summary>
        /// Вкладка с таблицей
        /// </summary>
        /// <param name="table">таблица данных</param>
        /// <param name="name">название вкладки</param>
        public TabTable
            (Table<T> table, Func<IQueryable<T>, string, IQueryable<T>> tFilt
            , string name = ""
            ) : base(table, name: name)
        {
            SelectData = TableT;
            InsertTable = new lamdaCommand(OnInsert);
            RemoveTable = new lamdaCommand<T>(Remove);
            RefreshData = new lamdaCommand(
                () =>
                    {
                        Context.Refresh(RefreshMode.OverwriteCurrentValues, Table);
                        SelectData = TableT;
                    });

            SatusProps.Add(new SatusProp("Количество строк", () => SelectData.Count()));
            InsertItem = new T();

            FilterChanged += (sender, e) => SelectData = tFilt?.Invoke(TableT, e);
        }

        public void AddFilterList<TFilter>(string name, IQueryable<TFilter> queryable
            , Expression<Func<T, TFilter>> ex) => FilterParams.Add
                (
                    new FilterList<TabTable<T>, TFilter>
                    (
                        queryable, name
                        , t => SelectData = TableT.Join
                            (
                                queryable.Where(r => r.Equals(t))
                                , ex, r => r, (d, g) => d)
                    )
                );

        public void AddFilterBool(string name, Func<bool, Expression<Func<T, bool>>> expression)
        {
            var filterList = new FilterBool(name
                , s => SelectData = TableT.Where(expression?.Invoke(s)));
            FilterParams.Add
                (
                filterList
                );
        }

        public void AddFilterText(string name, Func<char, Expression<Func<T, bool>>> expression)
        {
            var filterList = new FilterMarcChar(name);
            filterList.SelectedChenget +=
                (obj, s) => SelectData = TableT.Where(expression?.Invoke(s));
            FilterParams.Add
                (
               filterList
                );
        }

        public void AddFilterDate(string name, Func<(DateTime date1, DateTime date2), Expression<Func<T, bool>>> expression)
        {
            var filterList = new FilterDate(name
                , d => SelectData = TableT.Where(expression?.Invoke(d)));

            FilterParams.Add
                (
                filterList
                );
        }

        internal void AddSort<TKey>(string name, Expression<Func<T, TKey>> expression)
        {
            var sort = new SortProp(name);
            sort.StatusChenget += (o, s) =>
             {
                 switch (s)
                 {
                     case SortStatus.off:
                         SelectData = TableT;
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

        private void OnInsert()
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
                MessageBox.Show(ex.Message + "\r\n Перепроверьте данные"
                    , ex.Number.ToString());
            }
            InsertItem = new T().New;
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
                {MessageBox.Show(e.Message, "Ошибка");}
        }
    }

    public class TabTable : Tab
    {
        #region SelectedItems
        private IEnumerable selectedItems;
        /// <summary>Выбранные дипломы</summary>
        public IEnumerable SelectedItems
        {
            get => selectedItems;
            set => Set(ref selectedItems, value);
        }
        #endregion SelectedItems

        /// <summary>Контекст данных</summary>
        public DataContext Context { get; }

        public ICommand UpdateTable { get; }
        public ICommand RemoveSelectItems { get; }
        
        public ITable Table { get; }

        public TabTable(ITable table, string name = "") : base(name)
        {
            Table = table;
            Context = table.Context;
            UpdateTable = new InstrumentProp("записать все данные", Update);
            RemoveSelectItems = new InstrumentProp("удалить выделенное", OnRemoveSelectItems);
            InstrumentProps.Add(UpdateTable);
            InstrumentProps.Add(RemoveSelectItems);
        }

        private void OnRemoveSelectItems()
        {
            Table.DeleteAllOnSubmit(SelectedItems);
            Context.SubmitChanges(ConflictMode.ContinueOnConflict);
            Context.Refresh(RefreshMode.OverwriteCurrentValues, Table);
            Filter = Filter;
        }

        private void Update()
        {
            try
            {
                Context.SubmitChanges();
                Context.Refresh(RefreshMode.OverwriteCurrentValues, Table);
            }
            catch (Exception e)
                {MessageBox.Show(e.Message, "Ошибка");}
        }
    }
}