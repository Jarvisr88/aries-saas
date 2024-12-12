namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    [DXToolboxBrowsable(false)]
    public class psvItemsControl : ItemsControl, IDisposable
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        internal static readonly DependencyProperty HasItemsInternalProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        internal static readonly DependencyProperty ActualSizeProperty;

        static psvItemsControl()
        {
            DependencyPropertyRegistrator<psvItemsControl> registrator = new DependencyPropertyRegistrator<psvItemsControl>();
            registrator.Register<bool>("HasItemsInternal", ref HasItemsInternalProperty, false, (dObj, e) => ((psvItemsControl) dObj).OnHasItemsChanged((bool) e.NewValue), null);
            registrator.Register<Size>("ActualSize", ref ActualSizeProperty, Size.Empty, (dObj, e) => ((psvItemsControl) dObj).OnActualSizeChanged((Size) e.NewValue), null);
        }

        public psvItemsControl()
        {
            base.Focusable = false;
            base.IsTabStop = false;
            this.StartListen(HasItemsInternalProperty, "HasItems", BindingMode.OneWay);
            base.Loaded += new RoutedEventHandler(this.psvItemsControl_Loaded);
            base.Unloaded += new RoutedEventHandler(this.psvItemsControl_Unloaded);
        }

        public static void Clear(psvItemsControl itemsControl)
        {
            if (itemsControl != null)
            {
                itemsControl.ClearItemsSource();
            }
        }

        protected virtual void ClearContainer(DependencyObject element)
        {
        }

        protected override void ClearContainerForItemOverride(DependencyObject element, object item)
        {
            this.ClearContainer(element);
            base.ClearContainerForItemOverride(element, item);
        }

        protected virtual void ClearItemsSource()
        {
            base.ClearValue(ItemsControl.ItemsSourceProperty);
        }

        protected virtual void ClearTemplateChildren()
        {
            this.UnsubscribeUpdateLayout();
            if ((this.PartItemsPanel != null) && !LayoutItemsHelper.IsTemplateChild<psvItemsControl>(this.PartItemsPanel, this))
            {
                this.ReleaseItemsPanelCore(this.PartItemsPanel);
                this.PartItemsPanel = null;
            }
        }

        public void Dispose()
        {
            if (!this.IsDisposing)
            {
                this.IsDisposing = true;
                base.Loaded -= new RoutedEventHandler(this.psvItemsControl_Loaded);
                base.Unloaded -= new RoutedEventHandler(this.psvItemsControl_Unloaded);
                this.UnsubscribeUpdateLayout();
                base.ClearValue(HasItemsInternalProperty);
                this.OnDispose();
                this.ClearItemsSource();
                base.ClearValue(ActualSizeProperty);
                DockLayoutManager.Release(this);
                if (this.PartItemsPanel != null)
                {
                    this.ReleaseItemsPanelCore(this.PartItemsPanel);
                }
                this.PartItemsPanel = null;
                this.PartItemsPresenter = null;
                this.Container = null;
            }
            GC.SuppressFinalize(this);
        }

        internal void EnsureItemsPanel()
        {
            this.EnsureItemsPanelCore();
        }

        private void EnsureItemsPanelCore()
        {
            if (this.PartItemsPresenter != null)
            {
                Panel templateChild = LayoutItemsHelper.GetTemplateChild<Panel>(this.PartItemsPresenter);
                if (!ReferenceEquals(templateChild, this.PartItemsPanel))
                {
                    this.PartItemsPanel = templateChild;
                }
                if (this.PartItemsPanel != null)
                {
                    this.EnsureItemsPanelCore(this.PartItemsPanel);
                }
            }
        }

        protected virtual void EnsureItemsPanelCore(Panel itemsPanel)
        {
        }

        protected virtual void OnActualSizeChanged(Size value)
        {
        }

        public override void OnApplyTemplate()
        {
            this.ClearTemplateChildren();
            base.OnApplyTemplate();
            this.Container = DockLayoutManager.Ensure(this, false);
            this.PartItemsPresenter = LayoutItemsHelper.GetTemplateChild<ItemsPresenter>(this);
            this.SubscribeUpdateLayout();
        }

        protected virtual void OnDispose()
        {
        }

        protected virtual void OnHasItemsChanged(bool hasItems)
        {
        }

        protected virtual void OnInitialized()
        {
        }

        protected sealed override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            this.OnInitialized();
        }

        private void OnLayoutUpdated(object sender, EventArgs e)
        {
            this.UnsubscribeUpdateLayout();
            this.EnsureItemsPanelCore();
        }

        protected virtual void OnLoaded()
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

        protected virtual void PrepareContainer(DependencyObject element, object item)
        {
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);
            this.PrepareContainer(element, item);
        }

        private void psvItemsControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.OnLoaded();
        }

        private void psvItemsControl_Unloaded(object sender, RoutedEventArgs e)
        {
            this.OnUnloaded();
        }

        protected virtual void ReleaseItemsPanelCore(Panel itemsPanel)
        {
        }

        private void SubscribeUpdateLayout()
        {
            if (this.PartItemsPresenter != null)
            {
                this.PartItemsPresenter.LayoutUpdated += new EventHandler(this.OnLayoutUpdated);
                this.PartItemsPresenter.SizeChanged += new SizeChangedEventHandler(this.OnLayoutUpdated);
            }
        }

        private void UnsubscribeUpdateLayout()
        {
            if (this.PartItemsPresenter != null)
            {
                this.PartItemsPresenter.LayoutUpdated -= new EventHandler(this.OnLayoutUpdated);
                this.PartItemsPresenter.SizeChanged -= new SizeChangedEventHandler(this.OnLayoutUpdated);
            }
        }

        public bool IsDisposing { get; private set; }

        protected ItemsPresenter PartItemsPresenter { get; private set; }

        protected Panel PartItemsPanel { get; private set; }

        protected DockLayoutManager Container { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly psvItemsControl.<>c <>9 = new psvItemsControl.<>c();

            internal void <.cctor>b__2_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((psvItemsControl) dObj).OnHasItemsChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__2_1(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((psvItemsControl) dObj).OnActualSizeChanged((Size) e.NewValue);
            }
        }
    }
}

