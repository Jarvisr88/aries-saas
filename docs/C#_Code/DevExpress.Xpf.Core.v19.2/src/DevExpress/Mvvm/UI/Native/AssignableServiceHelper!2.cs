namespace DevExpress.Mvvm.UI.Native
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public static class AssignableServiceHelper<TOwner, TService> where TOwner: FrameworkElement where TService: class
    {
        public static TService GetService(TOwner owner, DependencyProperty property, object templateKey)
        {
            TService local = (TService) owner.GetValue(property);
            if (local == null)
            {
                local = AssignableServiceHelper<TOwner, TService>.LoadServiceFromTemplate(owner, templateKey);
                owner.SetCurrentValue(property, local);
            }
            return local;
        }

        private static TService LoadServiceFromTemplate(TOwner owner, object templateKey) => 
            TemplateHelper.LoadFromTemplate<TService>((DataTemplate) owner.FindResource(templateKey));

        private static void OnServicePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Action<ServiceBase> action = <>c<TOwner, TService>.<>9__1_0;
            if (<>c<TOwner, TService>.<>9__1_0 == null)
            {
                Action<ServiceBase> local1 = <>c<TOwner, TService>.<>9__1_0;
                action = <>c<TOwner, TService>.<>9__1_0 = x => x.Detach();
            }
            (e.OldValue as ServiceBase).Do<ServiceBase>(action);
            (e.NewValue as ServiceBase).Do<ServiceBase>(delegate (ServiceBase x) {
                x.ShouldInject = false;
                x.Attach(d);
            });
        }

        public static DependencyProperty RegisterServiceProperty(string name) => 
            DependencyProperty.Register(name, typeof(TService), typeof(TOwner), new PropertyMetadata(null, new PropertyChangedCallback(AssignableServiceHelper<TOwner, TService>.OnServicePropertyChanged)));

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly AssignableServiceHelper<TOwner, TService>.<>c <>9;
            public static Action<ServiceBase> <>9__1_0;

            static <>c()
            {
                AssignableServiceHelper<TOwner, TService>.<>c.<>9 = new AssignableServiceHelper<TOwner, TService>.<>c();
            }

            internal void <OnServicePropertyChanged>b__1_0(ServiceBase x)
            {
                x.Detach();
            }
        }
    }
}

