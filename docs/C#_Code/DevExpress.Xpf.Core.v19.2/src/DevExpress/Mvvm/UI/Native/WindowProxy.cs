namespace DevExpress.Mvvm.UI.Native
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class WindowProxy : IWindowSurrogate
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty ActualWindowSurrogateProperty = DependencyProperty.RegisterAttached("ActualWindowSurrogate", typeof(IWindowSurrogate), typeof(WindowProxy), new PropertyMetadata(null));

        public event EventHandler Activated
        {
            add
            {
                this.RealWindow.Activated += value;
            }
            remove
            {
                this.RealWindow.Activated -= value;
            }
        }

        public event EventHandler Closed
        {
            add
            {
                this.RealWindow.Closed += value;
            }
            remove
            {
                this.RealWindow.Closed -= value;
            }
        }

        public event CancelEventHandler Closing
        {
            add
            {
                this.RealWindow.Closing += value;
            }
            remove
            {
                this.RealWindow.Closing -= value;
            }
        }

        public event EventHandler Deactivated
        {
            add
            {
                this.RealWindow.Deactivated += value;
            }
            remove
            {
                this.RealWindow.Deactivated -= value;
            }
        }

        public WindowProxy(Window window)
        {
            if (window == null)
            {
                throw new ArgumentNullException("window");
            }
            this.RealWindow = window;
        }

        public bool Activate() => 
            this.RealWindow.Activate();

        public void Close()
        {
            this.RealWindow.Close();
        }

        private static IWindowSurrogate GetActualWindowSurrogate(DependencyObject obj) => 
            (IWindowSurrogate) obj.GetValue(ActualWindowSurrogateProperty);

        public static IWindowSurrogate GetWindowSurrogate(object window)
        {
            Func<DependencyObject, IWindowSurrogate> evaluator = <>c.<>9__3_0;
            if (<>c.<>9__3_0 == null)
            {
                Func<DependencyObject, IWindowSurrogate> local1 = <>c.<>9__3_0;
                evaluator = <>c.<>9__3_0 = x => GetActualWindowSurrogate(x);
            }
            IWindowSurrogate res = (window as DependencyObject).With<DependencyObject, IWindowSurrogate>(evaluator);
            if (res == null)
            {
                IWindowSurrogate surrogate1 = window as IWindowSurrogate;
                IWindowSurrogate surrogate2 = surrogate1;
                if (surrogate1 == null)
                {
                    IWindowSurrogate local2 = surrogate1;
                    surrogate2 = new WindowProxy((Window) window);
                }
                res = surrogate2;
                (window as DependencyObject).Do<DependencyObject>(delegate (DependencyObject x) {
                    SetActualWindowSurrogate(x, res);
                });
            }
            return res;
        }

        public void Hide()
        {
            this.RealWindow.Hide();
        }

        private static void SetActualWindowSurrogate(DependencyObject obj, IWindowSurrogate value)
        {
            obj.SetValue(ActualWindowSurrogateProperty, value);
        }

        public void Show()
        {
            this.RealWindow.Show();
        }

        public bool? ShowDialog() => 
            this.RealWindow.ShowDialog();

        public Window RealWindow { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly WindowProxy.<>c <>9 = new WindowProxy.<>c();
            public static Func<DependencyObject, IWindowSurrogate> <>9__3_0;

            internal IWindowSurrogate <GetWindowSurrogate>b__3_0(DependencyObject x) => 
                WindowProxy.GetActualWindowSurrogate(x);
        }
    }
}

