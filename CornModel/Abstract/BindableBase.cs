using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Threading;


namespace CornModel.Abstract
{
  public  abstract class BindableBase : INotifyPropertyChanged
    {

        protected virtual void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            PropertyChangedEventHandler changedEventHandler = PropertyChanged;
            if (changedEventHandler == null)
                return;
            string propertyName = GetPropertyName(propertyExpression);
            changedEventHandler(this, new PropertyChangedEventArgs(propertyName));
        }

        protected static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
        //    if (propertyExpression == null)
        //        throw new ArgumentNullException("propertyExpression");

            MemberExpression memberExpression = propertyExpression.Body as MemberExpression;
        //    if (memberExpression == null)
        //        throw new ArgumentException(@"Не правильный аргумент", "propertyExpression");

            PropertyInfo propertyInfo = memberExpression.Member as PropertyInfo;
            //if (propertyInfo == null)
            //    throw new ArgumentException(@"Не правильный аргумент", "propertyExpression");
            return propertyInfo.Name;
        }

        protected bool Set<T>(Expression<Func<T>> propertyExpression, ref T field, T newValue)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
                return false;
            field = newValue;
            RaisePropertyChanged(propertyExpression);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

    //    [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        
        /// <summary>
        /// Выполнить в потоке UI
        /// </summary>
        /// <param name="action">Метод, который должен выполниться</param>
        protected virtual void UiInvoke(Action action)
        {
            if (Dispatcher.CurrentDispatcher.CheckAccess()) action();
            else Dispatcher.CurrentDispatcher.BeginInvoke(action);
        }
    }
}
