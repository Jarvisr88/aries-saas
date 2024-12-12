namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;
    using System.Windows.Navigation;

    [RuntimeNameProperty("Name")]
    public abstract class ServiceBaseGeneric<T> : Behavior<T> where T: DependencyObject
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty NameProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty ServicesClientInternalProperty;

        static ServiceBaseGeneric()
        {
            ServiceBaseGeneric<T>.NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(ServiceBaseGeneric<T>), new PropertyMetadata(null));
            ServiceBaseGeneric<T>.ServicesClientInternalProperty = DependencyProperty.Register("ServicesClientInternal", typeof(object), typeof(ServiceBaseGeneric<T>), new PropertyMetadata(null, (d, e) => ((ServiceBaseGeneric<T>) d).OnServicesClientChanged(e.OldValue as ISupportServices, e.NewValue as ISupportServices)));
        }

        protected ServiceBaseGeneric()
        {
            this.ShouldInject = true;
        }

        protected Uri GetBaseUri()
        {
            Uri baseUri = BaseUriHelper.GetBaseUri(this);
            return (((baseUri != null) || (base.AssociatedObject == null)) ? baseUri : BaseUriHelper.GetBaseUri(base.AssociatedObject));
        }

        protected ISupportServices GetServicesClient() => 
            base.GetValue(ServiceBaseGeneric<T>.ServicesClientInternalProperty) as ISupportServices;

        protected override void OnAttached()
        {
            base.OnAttached();
            ServiceInjectionHelper<T>.SetInjectBinding((ServiceBaseGeneric<T>) this);
        }

        protected override void OnDetaching()
        {
            ServiceInjectionHelper<T>.ClearInjectBinding((ServiceBaseGeneric<T>) this);
            base.OnDetaching();
        }

        protected virtual void OnServicesClientChanged(ISupportServices oldServiceClient, ISupportServices newServiceClient)
        {
            oldServiceClient.Do<ISupportServices>(x => x.ServiceContainer.UnregisterService(this));
            newServiceClient.Do<ISupportServices>(x => x.ServiceContainer.RegisterService(base.Name, this, base.YieldToParent));
        }

        public string Name
        {
            get => 
                (string) base.GetValue(ServiceBaseGeneric<T>.NameProperty);
            set => 
                base.SetValue(ServiceBaseGeneric<T>.NameProperty, value);
        }

        public bool YieldToParent { get; set; }

        internal bool ShouldInject { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ServiceBaseGeneric<T>.<>c <>9;

            static <>c()
            {
                ServiceBaseGeneric<T>.<>c.<>9 = new ServiceBaseGeneric<T>.<>c();
            }

            internal void <.cctor>b__20_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ServiceBaseGeneric<T>) d).OnServicesClientChanged(e.OldValue as ISupportServices, e.NewValue as ISupportServices);
            }
        }

        internal static class ServiceInjectionHelper
        {
            public static void ClearInjectBinding(ServiceBaseGeneric<T> service)
            {
                BindingOperations.ClearBinding(service, ServiceBaseGeneric<T>.ServicesClientInternalProperty);
            }

            public static bool IsInjectBindingSet(ServiceBaseGeneric<T> service) => 
                BindingOperations.IsDataBound(service, ServiceBaseGeneric<T>.ServicesClientInternalProperty);

            public static void SetInjectBinding(ServiceBaseGeneric<T> service)
            {
                if (service.ShouldInject)
                {
                    Binding binding = new Binding {
                        Path = new PropertyPath("DataContext", new object[0]),
                        Source = service.AssociatedObject
                    };
                    BindingOperations.SetBinding(service, ServiceBaseGeneric<T>.ServicesClientInternalProperty, binding);
                }
            }
        }
    }
}

