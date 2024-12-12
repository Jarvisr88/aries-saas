namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking.VisualElements;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    public abstract class psvFrameworkElement : Control
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty WidthInternalProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty HeightInternalProperty;

        static psvFrameworkElement()
        {
            DependencyPropertyRegistrator<psvFrameworkElement> registrator = new DependencyPropertyRegistrator<psvFrameworkElement>();
            registrator.Register<double>("WidthInternal", ref WidthInternalProperty, double.NaN, (dObj, e) => ((psvFrameworkElement) dObj).OnWidthInternalChanged((double) e.OldValue, (double) e.NewValue), null);
            registrator.Register<double>("HeightInternal", ref HeightInternalProperty, double.NaN, (dObj, e) => ((psvFrameworkElement) dObj).OnHeightInternalChanged((double) e.OldValue, (double) e.NewValue), null);
        }

        public psvFrameworkElement()
        {
            base.Loaded += new RoutedEventHandler(this.psvFrameworkElement_Loaded);
            base.Unloaded += new RoutedEventHandler(this.psvFrameworkElement_Unloaded);
            this.StartListen(WidthInternalProperty, "Width", BindingMode.OneWay);
            this.StartListen(HeightInternalProperty, "Height", BindingMode.OneWay);
        }

        protected abstract void OnHeightInternalChanged(double oldValue, double newValue);
        protected virtual void OnLoaded()
        {
        }

        protected virtual void OnUnloaded()
        {
        }

        protected abstract void OnWidthInternalChanged(double oldValue, double newValue);
        private void psvFrameworkElement_Loaded(object sender, RoutedEventArgs e)
        {
            this.OnLoaded();
        }

        private void psvFrameworkElement_Unloaded(object sender, RoutedEventArgs e)
        {
            this.OnUnloaded();
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly psvFrameworkElement.<>c <>9 = new psvFrameworkElement.<>c();

            internal void <.cctor>b__2_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((psvFrameworkElement) dObj).OnWidthInternalChanged((double) e.OldValue, (double) e.NewValue);
            }

            internal void <.cctor>b__2_1(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((psvFrameworkElement) dObj).OnHeightInternalChanged((double) e.OldValue, (double) e.NewValue);
            }
        }
    }
}

