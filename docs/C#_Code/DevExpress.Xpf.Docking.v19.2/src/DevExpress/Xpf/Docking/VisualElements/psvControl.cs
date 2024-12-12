namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class psvControl : Control, IDisposable
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        internal static readonly DependencyProperty ActualSizeProperty;
        private int preparingContainerForItem;

        static psvControl()
        {
            new DevExpress.Xpf.Docking.DependencyPropertyRegistrator<psvControl>().Register<Size>("ActualSize", ref ActualSizeProperty, Size.Empty, (dObj, e) => ((psvControl) dObj).OnActualSizeChanged((Size) e.NewValue), null);
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<psvControl>.New().OverrideMetadata(UIElement.FocusableProperty, false, null, FrameworkPropertyMetadataOptions.None).OverrideMetadata(Control.IsTabStopProperty, false, null, FrameworkPropertyMetadataOptions.None);
        }

        public psvControl()
        {
            base.Loaded += new RoutedEventHandler(this.psvControl_Loaded);
            base.Unloaded += new RoutedEventHandler(this.psvControl_Unloaded);
        }

        public void BeginPrepareContainer()
        {
            this.preparingContainerForItem++;
        }

        public void Dispose()
        {
            if (!this.IsDisposing)
            {
                this.IsDisposing = true;
                base.Loaded -= new RoutedEventHandler(this.psvControl_Loaded);
                base.Unloaded -= new RoutedEventHandler(this.psvControl_Unloaded);
                base.ClearValue(ActualSizeProperty);
                this.OnDispose();
                DockLayoutManager.Release(this);
                this.Container = null;
            }
            GC.SuppressFinalize(this);
        }

        public void EndPrepareContainer()
        {
            int num = this.preparingContainerForItem - 1;
            this.preparingContainerForItem = num;
            if (num == 0)
            {
                this.OnPrepareContainerForItemComplete();
            }
        }

        protected virtual void OnActualSizeChanged(Size value)
        {
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.Container = DockLayoutManager.Ensure(this, false);
        }

        protected virtual void OnDispose()
        {
        }

        protected virtual void OnInitialized()
        {
        }

        protected sealed override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            this.IsInitializedCore = true;
            this.OnInitialized();
        }

        protected virtual void OnLoaded()
        {
        }

        protected virtual void OnPrepareContainerForItemComplete()
        {
        }

        protected sealed override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.SetValue(ActualSizeProperty, sizeInfo.NewSize);
            base.OnRenderSizeChanged(sizeInfo);
        }

        protected virtual void OnUnloaded()
        {
        }

        private void psvControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.OnLoaded();
        }

        private void psvControl_Unloaded(object sender, RoutedEventArgs e)
        {
            this.OnUnloaded();
        }

        public bool IsDisposing { get; private set; }

        protected bool IsInitializedCore { get; private set; }

        protected bool IsPreparingContainerForItem =>
            this.preparingContainerForItem > 0;

        protected DockLayoutManager Container { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly psvControl.<>c <>9 = new psvControl.<>c();

            internal void <.cctor>b__1_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((psvControl) dObj).OnActualSizeChanged((Size) e.NewValue);
            }
        }
    }
}

