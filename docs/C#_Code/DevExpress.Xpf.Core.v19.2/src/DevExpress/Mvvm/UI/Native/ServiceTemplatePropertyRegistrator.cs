namespace DevExpress.Mvvm.UI.Native
{
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public static class ServiceTemplatePropertyRegistrator
    {
        private static bool IsInitialized(DependencyObject d)
        {
            FrameworkElement element = d as FrameworkElement;
            if (element != null)
            {
                return element.IsInitialized;
            }
            FrameworkContentElement element2 = d as FrameworkContentElement;
            return ((element2 == null) || element2.IsInitialized);
        }

        public static DependencyPropertyRegistrator<T> RegisterServiceTemplateProperty<T, TService>(this DependencyPropertyRegistrator<T> registrator, Expression<Func<T, DataTemplate>> property, out DependencyProperty propertyField, out Action<T, Action<TService>> serviceActionExecutor, Action<T> changedCallback = null) where TService: class
        {
            Action<T, Func<T, T>, Action<TService>> serviceActionExecutorCore;
            registrator.RegisterServiceTemplateProperty<T, T, TService>(property, out propertyField, out serviceActionExecutorCore, changedCallback);
            serviceActionExecutor = delegate (T owner, Action<TService> serviceAction) {
                Func<T, T> func1 = <>c__0<T, TService>.<>9__0_1;
                if (<>c__0<T, TService>.<>9__0_1 == null)
                {
                    Func<T, T> local1 = <>c__0<T, TService>.<>9__0_1;
                    func1 = <>c__0<T, TService>.<>9__0_1 = x => x;
                }
                serviceActionExecutorCore(owner, func1, serviceAction);
            };
            return registrator;
        }

        public static DependencyPropertyRegistrator<T> RegisterServiceTemplateProperty<T, TServiceOwner, TService>(this DependencyPropertyRegistrator<T> registrator, Expression<Func<T, DataTemplate>> property, out DependencyProperty propertyField, out Action<T, Func<T, TServiceOwner>, Action<TService>> serviceActionExecutor, Action<T> changedCallback = null) where TService: class
        {
            propertyField = AssignableServiceHelper2<T, TService>.RegisterServiceTemplateProperty<T>(DependencyPropertyRegistrator<T>.GetPropertyName<DataTemplate>(property), changedCallback);
            DependencyProperty propertyFieldRef = propertyField;
            serviceActionExecutor = delegate (T owner, Func<T, TServiceOwner> getServiceOwner, Action<TService> serviceAction) {
                DependencyObject d = (DependencyObject) owner;
                if (!IsInitialized(d))
                {
                    ISupportInitialize initialize = (ISupportInitialize) d;
                    initialize.BeginInit();
                    initialize.EndInit();
                }
                AssignableServiceHelper2<T, TService>.DoServiceAction((DependencyObject) getServiceOwner(owner), (DataTemplate) d.GetValue(propertyFieldRef), serviceAction);
            };
            return registrator;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__0<T, TService> where TService: class
        {
            public static readonly ServiceTemplatePropertyRegistrator.<>c__0<T, TService> <>9;
            public static Func<T, T> <>9__0_1;

            static <>c__0()
            {
                ServiceTemplatePropertyRegistrator.<>c__0<T, TService>.<>9 = new ServiceTemplatePropertyRegistrator.<>c__0<T, TService>();
            }

            internal T <RegisterServiceTemplateProperty>b__0_1(T x) => 
                x;
        }
    }
}

