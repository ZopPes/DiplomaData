using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
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
    public class TabTable<T> : Tab, ITable<T>, INotifyCollectionChanged where T : class, ICloneable, new()
    {
        #region Table
        /// <summary>Таблица данных</summary>
        public Table<T> Table { get;}
        #endregion Table


        #region InsertItem
        private T insertItem;
        /// <summary>Переменная для добавления</summary>
        public T InsertItem { get => insertItem; set => Set(ref insertItem, value); }
        #endregion

        public ICommand InsertTable { get; }
        public ICommand RemoveTable { get; }

        public Expression Expression => ((IQueryable)Table).Expression;

        public Type ElementType => ((IQueryable)Table).ElementType;

        public IQueryProvider Provider => ((IQueryable)Table).Provider;


        /// <summary>
        /// Вкладка с таблицей
        /// </summary>
        /// <param name="table">таблица данных</param>
        /// <param name="name">название вкладки</param>
        public TabTable(Table<T> table, string name = "") : base(name)
        {
            Table = table;
            
            InsertTable = new lamdaCommand(NewMethod);
            RemoveTable = new lamdaCommand<T>(DeleteOnSubmit);
            Properties.Add(new Property("Количество строк", () => Table.Count()));
            InsertItem = new T();
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private void NewMethod()    
        {
            InsertOnSubmit(InsertItem.Clone() as T);
        }

        public void InsertOnSubmit(T entity)
        {
            ((ITable<T>)Table).InsertOnSubmit(entity);
            CollectionChanged?.Invoke
                (
                this
                , new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, entity)
                );
        }

        public void Attach(T entity)
        {
            ((ITable<T>)Table).Attach(entity);
        }

        public void DeleteOnSubmit(T entity)
        {
            var r = this.ToList().IndexOf(entity);

            ((ITable<T>)Table).DeleteOnSubmit(entity);
            
            CollectionChanged?.Invoke
                (
                this
                , new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove,entity,r)
                );
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)Table).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Table).GetEnumerator();
        }
    }
}