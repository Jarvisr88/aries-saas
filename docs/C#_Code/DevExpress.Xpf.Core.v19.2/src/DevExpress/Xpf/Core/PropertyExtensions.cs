namespace DevExpress.Xpf.Core
{
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;

    public static class PropertyExtensions
    {
        public static PropertyChangedEventArgs CreateChangedEventArgs<T>(this Expression<Func<T>> property) => 
            new PropertyChangedEventArgs((property.Body as MemberExpression).Member.Name);

        public static void RaisePropertyChanged<T>(this INotifyPropertyChanged source, PropertyChangedEventHandler propertyChanged, Expression<Func<T>> property)
        {
            if (propertyChanged != null)
            {
                propertyChanged(source, property.CreateChangedEventArgs<T>());
            }
        }
    }
}

