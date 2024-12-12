namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class DOTargetWrapper
    {
        public readonly DependencyObject Object;

        public event DependencyPropertyChangedEventHandler DataContextChanged
        {
            add
            {
                this.FE.Do<FrameworkElement>(delegate (FrameworkElement x) {
                    x.DataContextChanged += value;
                });
                this.FCE.Do<FrameworkContentElement>(delegate (FrameworkContentElement x) {
                    x.DataContextChanged += value;
                });
            }
            remove
            {
                this.FE.Do<FrameworkElement>(delegate (FrameworkElement x) {
                    x.DataContextChanged -= value;
                });
                this.FCE.Do<FrameworkContentElement>(delegate (FrameworkContentElement x) {
                    x.DataContextChanged -= value;
                });
            }
        }

        public event EventHandler Initialized
        {
            add
            {
                this.FE.Do<FrameworkElement>(delegate (FrameworkElement x) {
                    x.Initialized += value;
                });
                this.FCE.Do<FrameworkContentElement>(delegate (FrameworkContentElement x) {
                    x.Initialized += value;
                });
            }
            remove
            {
                this.FE.Do<FrameworkElement>(delegate (FrameworkElement x) {
                    x.Initialized -= value;
                });
                this.FCE.Do<FrameworkContentElement>(delegate (FrameworkContentElement x) {
                    x.Initialized -= value;
                });
            }
        }

        public event RoutedEventHandler Loaded
        {
            add
            {
                this.FE.Do<FrameworkElement>(delegate (FrameworkElement x) {
                    x.Loaded += value;
                });
                this.FCE.Do<FrameworkContentElement>(delegate (FrameworkContentElement x) {
                    x.Loaded += value;
                });
            }
            remove
            {
                this.FE.Do<FrameworkElement>(delegate (FrameworkElement x) {
                    x.Loaded -= value;
                });
                this.FCE.Do<FrameworkContentElement>(delegate (FrameworkContentElement x) {
                    x.Loaded -= value;
                });
            }
        }

        public event RoutedEventHandler Unloaded
        {
            add
            {
                this.FE.Do<FrameworkElement>(delegate (FrameworkElement x) {
                    x.Unloaded += value;
                });
                this.FCE.Do<FrameworkContentElement>(delegate (FrameworkContentElement x) {
                    x.Unloaded += value;
                });
            }
            remove
            {
                this.FE.Do<FrameworkElement>(delegate (FrameworkElement x) {
                    x.Unloaded -= value;
                });
                this.FCE.Do<FrameworkContentElement>(delegate (FrameworkContentElement x) {
                    x.Unloaded -= value;
                });
            }
        }

        public DOTargetWrapper(DependencyObject obj)
        {
            this.Object = obj;
        }

        private FrameworkElement FE =>
            this.Object as FrameworkElement;

        private FrameworkContentElement FCE =>
            this.Object as FrameworkContentElement;

        public bool IsNull =>
            (this.FE == null) && ReferenceEquals(this.FCE, null);

        public bool IsInitialized
        {
            get
            {
                Func<FrameworkElement, bool> evaluator = <>c.<>9__9_0;
                if (<>c.<>9__9_0 == null)
                {
                    Func<FrameworkElement, bool> local1 = <>c.<>9__9_0;
                    evaluator = <>c.<>9__9_0 = x => x.IsInitialized;
                }
                return this.FE.Return<FrameworkElement, bool>(evaluator, () => this.FCE.IsInitialized);
            }
        }

        public bool IsLoaded
        {
            get
            {
                Func<FrameworkElement, bool> evaluator = <>c.<>9__11_0;
                if (<>c.<>9__11_0 == null)
                {
                    Func<FrameworkElement, bool> local1 = <>c.<>9__11_0;
                    evaluator = <>c.<>9__11_0 = x => x.IsLoaded;
                }
                return this.FE.Return<FrameworkElement, bool>(evaluator, () => this.FCE.IsLoaded);
            }
        }

        public object DataContext
        {
            get
            {
                Func<FrameworkElement, object> evaluator = <>c.<>9__13_0;
                if (<>c.<>9__13_0 == null)
                {
                    Func<FrameworkElement, object> local1 = <>c.<>9__13_0;
                    evaluator = <>c.<>9__13_0 = x => x.DataContext;
                }
                return this.FE.Return<FrameworkElement, object>(evaluator, () => this.FCE.DataContext);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DOTargetWrapper.<>c <>9 = new DOTargetWrapper.<>c();
            public static Func<FrameworkElement, bool> <>9__9_0;
            public static Func<FrameworkElement, bool> <>9__11_0;
            public static Func<FrameworkElement, object> <>9__13_0;

            internal object <get_DataContext>b__13_0(FrameworkElement x) => 
                x.DataContext;

            internal bool <get_IsInitialized>b__9_0(FrameworkElement x) => 
                x.IsInitialized;

            internal bool <get_IsLoaded>b__11_0(FrameworkElement x) => 
                x.IsLoaded;
        }
    }
}

