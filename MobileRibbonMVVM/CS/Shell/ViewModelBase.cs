using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows;


namespace ERP.Infrastructure.CrossCutting
{
    /// <summary>
    /// Базовая реализация моделей представлений
    /// </summary>
    public class ViewModelBase : DependencyObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        
        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        
        protected bool OnPropertyChanged<TField>(ref TField field, TField value, string propertyName)
        {
            if (Equals(field, value))
                return false;

            field = value;

            OnPropertyChanged(propertyName);
            return true;
        }

        
        protected bool OnPropertyChanged<TField>(ref TField field, TField value, Action onChanged, string propertyName)
        {
            if (OnPropertyChanged(ref field, value, propertyName))
            {
                var evt = onChanged;
                if (evt != null)
                {
                    evt();
                }
                return true;
            }
            return false;
        }

        protected void OnPropertyChanged<T>(Expression<Func<T>> expression)
        {
            OnPropertyChanged(GetPropertyName(expression));
        }

        public static string GetPropertyName<T>(Expression<Func<T>> expression)
        {
            var expressionBody = expression.Body as MemberExpression;
            if (expressionBody != null) return expressionBody.Member.Name;
            var operand = ((UnaryExpression)expression.Body).Operand;
            expressionBody = (MemberExpression)operand;
            return expressionBody.Member.Name;
        }
    }
}