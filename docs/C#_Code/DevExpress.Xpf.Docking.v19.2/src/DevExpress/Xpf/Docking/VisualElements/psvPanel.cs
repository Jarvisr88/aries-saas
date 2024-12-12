namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class psvPanel : Panel, IDisposable
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        internal static readonly DependencyProperty ActualSizeProperty;

        static psvPanel()
        {
            new DependencyPropertyRegistrator<psvPanel>().Register<Size>("ActualSize", ref ActualSizeProperty, Size.Empty, (dObj, e) => ((psvPanel) dObj).OnActualSizeChanged((Size) e.NewValue), null);
            FrameworkElement.DataContextProperty.OverrideMetadata(typeof(psvPanel), new FrameworkPropertyMetadata(null, (dObj, e) => ((psvPanel) dObj).OnDataContextChanged(e.NewValue, e.OldValue)));
        }

        public psvPanel()
        {
            base.Focusable = false;
            base.Loaded += new RoutedEventHandler(this.psvPanel_Loaded);
        }

        public void Dispose()
        {
            if (!this.IsDisposing)
            {
                this.IsDisposing = true;
                base.Loaded -= new RoutedEventHandler(this.psvPanel_Loaded);
                base.ClearValue(ActualSizeProperty);
                this.OnDispose();
            }
            GC.SuppressFinalize(this);
        }

        protected virtual void OnActualSizeChanged(Size value)
        {
        }

        protected virtual void OnDataContextChanged(object newValue, object oldValue)
        {
        }

        protected virtual void OnDispose()
        {
        }

        protected virtual void OnLoaded()
        {
        }

        protected sealed override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.SetValue(ActualSizeProperty, sizeInfo.NewSize);
            base.OnRenderSizeChanged(sizeInfo);
        }

        private void psvPanel_Loaded(object sender, RoutedEventArgs e)
        {
            this.OnLoaded();
        }

        public bool IsDisposing { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly psvPanel.<>c <>9 = new psvPanel.<>c();

            internal void <.cctor>b__1_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((psvPanel) dObj).OnActualSizeChanged((Size) e.NewValue);
            }

            internal void <.cctor>b__1_1(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((psvPanel) dObj).OnDataContextChanged(e.NewValue, e.OldValue);
            }
        }
    }
}

