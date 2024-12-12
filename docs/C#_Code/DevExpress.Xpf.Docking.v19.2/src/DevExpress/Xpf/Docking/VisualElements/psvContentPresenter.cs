namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    public class psvContentPresenter : ContentPresenter, IDisposable
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        internal static readonly DependencyProperty ContentInternalProperty;

        static psvContentPresenter()
        {
            new DependencyPropertyRegistrator<psvContentPresenter>().Register<object>("ContentInternal", ref ContentInternalProperty, null, (dObj, e) => ((psvContentPresenter) dObj).OnContentChanged(e.NewValue, e.OldValue), null);
        }

        public psvContentPresenter()
        {
            base.Focusable = false;
            this.StartListen(ContentInternalProperty, "Content", BindingMode.OneWay);
            base.Loaded += new RoutedEventHandler(this.psvContentPresenter_Loaded);
        }

        public void Dispose()
        {
            if (!this.IsDisposing)
            {
                this.IsDisposing = true;
                base.Loaded -= new RoutedEventHandler(this.psvContentPresenter_Loaded);
                base.ClearValue(ContentInternalProperty);
                base.ClearValue(ContentPresenter.ContentProperty);
                this.OnDispose();
                DockLayoutManager.Release(this);
            }
            GC.SuppressFinalize(this);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            DockLayoutManager.Ensure(this, false);
        }

        protected virtual void OnContentChanged(object content, object oldContent)
        {
        }

        protected virtual void OnDispose()
        {
        }

        protected virtual void OnLoaded()
        {
        }

        private void psvContentPresenter_Loaded(object sender, RoutedEventArgs e)
        {
            this.OnLoaded();
        }

        public bool IsDisposing { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly psvContentPresenter.<>c <>9 = new psvContentPresenter.<>c();

            internal void <.cctor>b__1_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((psvContentPresenter) dObj).OnContentChanged(e.NewValue, e.OldValue);
            }
        }
    }
}

