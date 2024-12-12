namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;

    public static class PropertyChangedTracker
    {
        public static PropertyChangedTracker<T, TProperty> GetPropertyChangedTracker<T, TProperty>(this T obj, Expression<Func<T, TProperty>> propertyExpression, Action changedCallBack) where T: class => 
            new PropertyChangedTracker<T, TProperty>(obj, propertyExpression, changedCallBack);
    }
}

