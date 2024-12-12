namespace DevExpress.Mvvm.UI.Native
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public static class AssignableServiceHelper2<TOwner, TService> where TService: class
    {
        public static void DoServiceAction(DependencyObject owner, DataTemplate template, Action<TService> action)
        {
            TService local = TemplateHelper.LoadFromTemplate<TService>(template);
            (local as ServiceBase).Do<ServiceBase>(delegate (ServiceBase x) {
                x.ShouldInject = false;
                x.Attach(owner);
            });
            try
            {
                action(local);
            }
            finally
            {
                Action<ServiceBase> action1 = <>c<TOwner, TService>.<>9__2_1;
                if (<>c<TOwner, TService>.<>9__2_1 == null)
                {
                    Action<ServiceBase> local1 = <>c<TOwner, TService>.<>9__2_1;
                    action1 = <>c<TOwner, TService>.<>9__2_1 = x => x.Detach();
                }
                (local as ServiceBase).Do<ServiceBase>(action1);
            }
        }

        public static void DoServiceAction(DependencyObject owner, TService service, Action<TService> action)
        {
            (service as ServiceBase).Do<ServiceBase>(delegate (ServiceBase x) {
                x.ShouldInject = false;
                x.Attach(owner);
            });
            try
            {
                action(service);
            }
            finally
            {
                Action<ServiceBase> action1 = <>c<TOwner, TService>.<>9__3_1;
                if (<>c<TOwner, TService>.<>9__3_1 == null)
                {
                    Action<ServiceBase> local1 = <>c<TOwner, TService>.<>9__3_1;
                    action1 = <>c<TOwner, TService>.<>9__3_1 = x => x.Detach();
                }
                (service as ServiceBase).Do<ServiceBase>(action1);
            }
        }

        public static DependencyProperty RegisterServiceTemplateProperty(string name) => 
            AssignableServiceHelper2<TOwner, TService>.RegisterServiceTemplateProperty<DependencyObject>(name, null);

        public static DependencyProperty RegisterServiceTemplateProperty<T>(string name, Action<T> onChanged)
        {
            PropertyMetadata typeMetadata = (onChanged == null) ? new PropertyMetadata(null) : new PropertyMetadata(null, delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                onChanged((T) d);
            });
            return DependencyProperty.Register(name, typeof(DataTemplate), typeof(TOwner), typeMetadata);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly AssignableServiceHelper2<TOwner, TService>.<>c <>9;
            public static Action<ServiceBase> <>9__2_1;
            public static Action<ServiceBase> <>9__3_1;

            static <>c()
            {
                AssignableServiceHelper2<TOwner, TService>.<>c.<>9 = new AssignableServiceHelper2<TOwner, TService>.<>c();
            }

            internal void <DoServiceAction>b__2_1(ServiceBase x)
            {
                x.Detach();
            }

            internal void <DoServiceAction>b__3_1(ServiceBase x)
            {
                x.Detach();
            }
        }
    }
}

