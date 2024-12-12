namespace DevExpress.Mvvm.UI
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public abstract class ServiceBase : ServiceBaseGeneric<FrameworkElement>
    {
        public static readonly DependencyProperty UnregisterOnUnloadedProperty;
        private bool isLoaded;

        static ServiceBase()
        {
            UnregisterOnUnloadedProperty = DependencyProperty.Register("UnregisterOnUnloaded", typeof(bool), typeof(ServiceBase), new PropertyMetadata(false, (d, e) => ((ServiceBase) d).OnUnregisterOnUnloadedChanged()));
        }

        protected ServiceBase()
        {
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            if (this.UnregisterOnUnloaded)
            {
                this.Subscribe();
            }
        }

        protected override void OnDetaching()
        {
            this.Unsubscribe();
            base.OnDetaching();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (!this.isLoaded)
            {
                this.isLoaded = true;
                if (!ServiceBaseGeneric<FrameworkElement>.ServiceInjectionHelper.IsInjectBindingSet(this))
                {
                    ServiceBaseGeneric<FrameworkElement>.ServiceInjectionHelper.SetInjectBinding(this);
                }
            }
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            if (this.isLoaded)
            {
                this.isLoaded = false;
                ServiceBaseGeneric<FrameworkElement>.ServiceInjectionHelper.ClearInjectBinding(this);
            }
        }

        private void OnUnregisterOnUnloadedChanged()
        {
            if (base.IsAttached)
            {
                if (this.UnregisterOnUnloaded)
                {
                    this.Subscribe();
                }
                else
                {
                    this.Unsubscribe();
                }
            }
        }

        private void Subscribe()
        {
            if (base.AssociatedObject != null)
            {
                base.AssociatedObject.Loaded += new RoutedEventHandler(this.OnLoaded);
                base.AssociatedObject.Unloaded += new RoutedEventHandler(this.OnUnloaded);
            }
        }

        private void Unsubscribe()
        {
            if (base.AssociatedObject != null)
            {
                base.AssociatedObject.Loaded -= new RoutedEventHandler(this.OnLoaded);
                base.AssociatedObject.Unloaded -= new RoutedEventHandler(this.OnUnloaded);
            }
        }

        public bool UnregisterOnUnloaded
        {
            get => 
                (bool) base.GetValue(UnregisterOnUnloadedProperty);
            set => 
                base.SetValue(UnregisterOnUnloadedProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ServiceBase.<>c <>9 = new ServiceBase.<>c();

            internal void <.cctor>b__13_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ServiceBase) d).OnUnregisterOnUnloadedChanged();
            }
        }
    }
}

