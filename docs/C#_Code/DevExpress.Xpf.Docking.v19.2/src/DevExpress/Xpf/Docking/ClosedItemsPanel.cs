namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Docking.Base;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    [DXToolboxBrowsable(false)]
    public class ClosedItemsPanel : BarContainerControl, IDisposable
    {
        public static readonly DependencyProperty DockProperty;
        private DockLayoutManager container;

        static ClosedItemsPanel()
        {
            new DependencyPropertyRegistrator<ClosedItemsPanel>().Register<System.Windows.Controls.Dock>("Dock", ref DockProperty, System.Windows.Controls.Dock.Top, (dObj, ea) => ((ClosedItemsPanel) dObj).OnDockChanged((System.Windows.Controls.Dock) ea.OldValue, (System.Windows.Controls.Dock) ea.NewValue), null);
        }

        public ClosedItemsPanel()
        {
            BarManagerCategory category1 = new BarManagerCategory();
            category1.Name = DockingLocalizer.GetString(DockingStringId.ClosedPanelsCategory);
            this.Category = category1;
            base.Unloaded += new RoutedEventHandler(this.OnUnloaded);
            FrameworkElementHelper.SetAllowDrop(this, false);
        }

        public void Dispose()
        {
            if (!this.IsDisposing)
            {
                this.IsDisposing = true;
                base.Unloaded -= new RoutedEventHandler(this.OnUnloaded);
                if ((this.Container != null) && !this.Container.IsDisposing)
                {
                    ClosedItemsBar closedItemsBar = this.Container.CustomizationController.ClosedItemsBar;
                    if (closedItemsBar != null)
                    {
                        closedItemsBar.UpdatePanel(null);
                    }
                }
                BarManagerPropertyHelper.ClearBarManager(this);
            }
            GC.SuppressFinalize(this);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.Container = DockLayoutManager.FindManager(this);
            ClosedItemsBar closedItemsBar = this.Container.CustomizationController.ClosedItemsBar;
            if (closedItemsBar != null)
            {
                closedItemsBar.UpdatePanel(this);
            }
        }

        public void OnDockChanged(System.Windows.Controls.Dock oldValue, System.Windows.Controls.Dock newValue)
        {
            DockPanel.SetDock(this, newValue);
            base.ContainerType = newValue.ToBarContainerType();
            if ((this.Container != null) && (this.Container.CustomizationController.ClosedItemsBar != null))
            {
                ClosedItemsBar closedItemsBar = this.Container.CustomizationController.ClosedItemsBar;
                closedItemsBar.DockInfo.ContainerType = newValue.ToBarContainerType();
                closedItemsBar.InvalidateContainerName();
            }
        }

        protected override void OnLoaded(object sender, RoutedEventArgs e)
        {
            base.OnLoaded(sender, e);
            if ((this.Container != null) && !this.Container.IsDisposing)
            {
                this.Container.CustomizationController.UpdateClosedItemsBar();
            }
        }

        protected virtual void OnUnloaded(object sender, RoutedEventArgs e)
        {
            if ((this.Container != null) && !this.Container.IsDisposing)
            {
                ClosedItemsBar closedItemsBar = this.Container.CustomizationController.ClosedItemsBar;
                if (closedItemsBar != null)
                {
                    this.Container.CustomizationController.HideClosedItemsBar();
                    closedItemsBar.UpdatePanel(null);
                }
            }
            BarManagerPropertyHelper.ClearBarManager(this);
        }

        public void SetBarManager(BarManager manager)
        {
            BarManagerPropertyHelper.SetBarManager(this, manager);
        }

        [Obsolete("Use BarManager.Bars property instead.")]
        public ItemCollection Items =>
            null;

        public bool IsDisposing { get; private set; }

        public BarManagerCategory Category { get; private set; }

        public DockLayoutManager Container
        {
            get => 
                this.container ?? DockLayoutManager.GetDockLayoutManager(this);
            set => 
                this.container = value;
        }

        public System.Windows.Controls.Dock Dock
        {
            get => 
                (System.Windows.Controls.Dock) base.GetValue(DockProperty);
            set => 
                base.SetValue(DockProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ClosedItemsPanel.<>c <>9 = new ClosedItemsPanel.<>c();

            internal void <.cctor>b__1_0(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((ClosedItemsPanel) dObj).OnDockChanged((Dock) ea.OldValue, (Dock) ea.NewValue);
            }
        }
    }
}

