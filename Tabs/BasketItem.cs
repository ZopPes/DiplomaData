using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WPFMVVMHelper;

namespace DiplomaData.Tabs
{
    public interface IBasket
    {
        string Name { get; set; }

        ICommand Recovery { get; }
        ICommand Delete { get; }

        void Clear();
    }

    public class BasketItem<T> : peremlog, IBasket
    {
        public IEnumerable<T> Data { get => dateFunc?.Invoke() ?? null; }

        private Func<IEnumerable<T>> dateFunc;

        public ICommand Recovery { get; }
        public ICommand Delete { get; }

        public string Name { get; set; }

        public BasketItem(Func<IEnumerable<T>> dateFunc, Action<T> recovery, Action<T> delete, string name)
        {
            this.dateFunc = dateFunc;

            Name = name;

            Recovery = new lamdaCommand<IEnumerable>(t =>
               {
                   foreach (T item in t)
                       recovery?.Invoke(item);
                   OnPropertyChanged(nameof(Data));
               });

            Delete = new lamdaCommand<IEnumerable>(t =>
            {
                try
                {
                    foreach (T item in t)
                        delete?.Invoke(item);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                OnPropertyChanged(nameof(Data));
            });
        }

        public void Clear() => 
            Delete.Execute(Data);
    }
}