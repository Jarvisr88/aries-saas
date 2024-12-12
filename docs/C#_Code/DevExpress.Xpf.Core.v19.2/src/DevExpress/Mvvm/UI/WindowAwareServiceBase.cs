namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public abstract class WindowAwareServiceBase : ServiceBase
    {
        public static readonly DependencyProperty WindowSourceProperty;
        public static readonly DependencyProperty WindowProperty;
        private static readonly DependencyPropertyKey ActualWindowPropertyKey;
        public static readonly DependencyProperty ActualWindowProperty;

        static WindowAwareServiceBase()
        {
            WindowSourceProperty = DependencyProperty.Register("WindowSource", typeof(FrameworkElement), typeof(WindowAwareServiceBase), new PropertyMetadata(null, (d, e) => ((WindowAwareServiceBase) d).OnWindowSourceChanged(e)));
            WindowProperty = DependencyProperty.Register("Window", typeof(System.Windows.Window), typeof(WindowAwareServiceBase), new PropertyMetadata(null, (d, e) => ((WindowAwareServiceBase) d).OnWindowChanged(e)));
            ActualWindowPropertyKey = DependencyProperty.RegisterReadOnly("ActualWindow", typeof(System.Windows.Window), typeof(WindowAwareServiceBase), new PropertyMetadata(null, (d, e) => ((WindowAwareServiceBase) d).OnActualWindowChanged((System.Windows.Window) e.OldValue)));
            ActualWindowProperty = ActualWindowPropertyKey.DependencyProperty;
        }

        protected WindowAwareServiceBase()
        {
        }

        private void Attach(FrameworkElement windowSource)
        {
            windowSource.Loaded += new RoutedEventHandler(this.OnWindowSourceIsLoadedChanged);
            windowSource.Unloaded += new RoutedEventHandler(this.OnWindowSourceIsLoadedChanged);
        }

        private void Detach(FrameworkElement windowSource)
        {
            windowSource.Loaded -= new RoutedEventHandler(this.OnWindowSourceIsLoadedChanged);
            windowSource.Unloaded -= new RoutedEventHandler(this.OnWindowSourceIsLoadedChanged);
        }

        private static System.Windows.Window GetWindowEx(DependencyObject d) => 
            (d as System.Windows.Window) ?? System.Windows.Window.GetWindow(d);

        protected abstract void OnActualWindowChanged(System.Windows.Window oldWindow);
        protected override void OnAttached()
        {
            base.OnAttached();
            this.Attach(base.AssociatedObject);
            this.UpdateActualWindow();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.Detach(base.AssociatedObject);
            this.UpdateActualWindow();
        }

        private void OnWindowChanged(DependencyPropertyChangedEventArgs e)
        {
            this.UpdateActualWindow();
        }

        private void OnWindowSourceChanged(DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement oldValue = (FrameworkElement) e.OldValue;
            FrameworkElement newValue = (FrameworkElement) e.NewValue;
            if (oldValue != null)
            {
                this.Detach(oldValue);
            }
            if (newValue != null)
            {
                this.Attach(newValue);
            }
            this.UpdateActualWindow();
        }

        private void OnWindowSourceIsLoadedChanged(object sender, RoutedEventArgs e)
        {
            this.UpdateActualWindow();
        }

        protected void UpdateActualWindow()
        {
            System.Windows.Window window = this.Window;
            System.Windows.Window local4 = window;
            if (window == null)
            {
                System.Windows.Window local1 = window;
                System.Windows.Window local2 = this.WindowSource.With<FrameworkElement, System.Windows.Window>(new Func<FrameworkElement, System.Windows.Window>(WindowAwareServiceBase.GetWindowEx));
                local4 = local2;
                if (local2 == null)
                {
                    System.Windows.Window local3 = local2;
                    local4 = base.AssociatedObject.With<FrameworkElement, System.Windows.Window>(new Func<FrameworkElement, System.Windows.Window>(WindowAwareServiceBase.GetWindowEx));
                }
            }
            this.ActualWindow = local4;
        }

        public FrameworkElement WindowSource
        {
            get => 
                (FrameworkElement) base.GetValue(WindowSourceProperty);
            set => 
                base.SetValue(WindowSourceProperty, value);
        }

        public System.Windows.Window Window
        {
            get => 
                (System.Windows.Window) base.GetValue(WindowProperty);
            set => 
                base.SetValue(WindowProperty, value);
        }

        public System.Windows.Window ActualWindow
        {
            get => 
                (System.Windows.Window) base.GetValue(ActualWindowProperty);
            private set => 
                base.SetValue(ActualWindowPropertyKey, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly WindowAwareServiceBase.<>c <>9 = new WindowAwareServiceBase.<>c();

            internal void <.cctor>b__24_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((WindowAwareServiceBase) d).OnWindowSourceChanged(e);
            }

            internal void <.cctor>b__24_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((WindowAwareServiceBase) d).OnWindowChanged(e);
            }

            internal void <.cctor>b__24_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((WindowAwareServiceBase) d).OnActualWindowChanged((Window) e.OldValue);
            }
        }
    }
}

