namespace DevExpress.Mvvm.UI.Native
{
    using System;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public static class BindableReadOnlyPropertyRegistrator
    {
        public static DependencyPropertyRegistrator<T> RegisterBindableReadOnly<T, TProperty>(this DependencyPropertyRegistrator<T> registrator, Expression<Func<T, TProperty>> property, out Action<T, TProperty> propertySeter, out DependencyProperty propertyField, TProperty defaultValue, FrameworkPropertyMetadataOptions frameworkOptions = 0) where T: DependencyObject
        {
            int locked = 0;
            PropertyChangedCallback propertyChangedCallback = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                if (locked == 0)
                {
                    int num = locked + 1;
                    locked = num;
                    try
                    {
                        d.SetCurrentValue(e.Property, e.OldValue);
                    }
                    finally
                    {
                        num = locked - 1;
                        locked = num;
                    }
                }
            };
            DependencyProperty registeredProperty = DependencyProperty.Register(DependencyPropertyRegistrator<T>.GetPropertyName<TProperty>(property), typeof(TProperty), typeof(T), new FrameworkPropertyMetadata(defaultValue, frameworkOptions | FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, propertyChangedCallback));
            propertySeter = delegate (T d, TProperty v) {
                int num = locked + 1;
                locked = num;
                try
                {
                    d.SetCurrentValue(registeredProperty, v);
                }
                finally
                {
                    num = locked - 1;
                    locked = num;
                }
            };
            propertyField = registeredProperty;
            return registrator;
        }
    }
}

