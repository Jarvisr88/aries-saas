namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Collections;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class AutoHideTrayHeadersGroup : psvItemsControl
    {
        public static readonly DependencyProperty LayoutItemProperty;
        public static readonly DependencyProperty DockTypeProperty;
        private AutoHideTray trayCore;

        static AutoHideTrayHeadersGroup()
        {
            DevExpress.Xpf.Docking.DependencyPropertyRegistrator<AutoHideTrayHeadersGroup> registrator = new DevExpress.Xpf.Docking.DependencyPropertyRegistrator<AutoHideTrayHeadersGroup>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.Register<BaseLayoutItem>("LayoutItem", ref LayoutItemProperty, null, (dObj, ea) => ((AutoHideTrayHeadersGroup) dObj).OnLayoutItemChanged((BaseLayoutItem) ea.OldValue, (BaseLayoutItem) ea.NewValue), null);
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(AutoHideTrayHeadersGroup), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<AutoHideTrayHeadersGroup>.New().Register<Dock>(System.Linq.Expressions.Expression.Lambda<Func<AutoHideTrayHeadersGroup, Dock>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(AutoHideTrayHeadersGroup.get_DockType)), parameters), out DockTypeProperty, Dock.Left, (d, oldValue, newValue) => d.OnDockTypeChanged(oldValue, newValue), frameworkOptions);
        }

        public AutoHideTrayHeadersGroup()
        {
        }

        public AutoHideTrayHeadersGroup(AutoHideTray tray) : this()
        {
            this.trayCore = tray;
        }

        protected void AcceptHeaderItems(Action<AutoHidePaneHeaderItem, BaseLayoutItem> action)
        {
            foreach (BaseLayoutItem item in (IEnumerable) base.Items)
            {
                AutoHidePaneHeaderItem item = LayoutHelper.FindElement(this, element => this.IsHeaderForItem(element, item)) as AutoHidePaneHeaderItem;
                if (item != null)
                {
                    action(item, item);
                }
            }
        }

        protected override void ClearContainer(DependencyObject element)
        {
            AutoHidePaneHeaderItem item = element as AutoHidePaneHeaderItem;
            if (item != null)
            {
                item.LayoutItem.ClearTemplate();
                item.ClearValue(AutoHideTray.OrientationProperty);
                item.Dispose();
            }
            base.ClearContainer(element);
        }

        protected override void EnsureItemsPanelCore(Panel itemsPanel)
        {
            base.EnsureItemsPanelCore(itemsPanel);
            this.PartHeadersPanel = itemsPanel as AutoHideTrayHeadersPanel;
        }

        protected internal virtual void EnsureLayoutItem(AutoHideGroup aGroup)
        {
            if (aGroup != null)
            {
                DockLayoutManager.SetLayoutItem(this, aGroup);
                base.ItemsSource = aGroup.Items;
                BindingHelper.SetBinding(this, DockTypeProperty, aGroup, "DockType");
            }
        }

        protected override DependencyObject GetContainerForItemOverride() => 
            new AutoHidePaneHeaderItem(this);

        private bool IsHeaderForItem(FrameworkElement element, BaseLayoutItem item)
        {
            AutoHidePaneHeaderItem item2 = element as AutoHidePaneHeaderItem;
            return ((item2 != null) && ReferenceEquals(item2.LayoutItem, item));
        }

        protected override bool IsItemItsOwnContainerOverride(object item) => 
            item is AutoHidePaneHeaderItem;

        protected override void OnDispose()
        {
            this.UnSubscribeTray();
            base.ClearValue(AutoHideTray.OrientationProperty);
            base.ClearValue(DockTypeProperty);
            base.ClearValue(LayoutItemProperty);
            base.OnDispose();
        }

        protected virtual void OnDockTypeChanged(object oldValue, object newValue)
        {
            foreach (object obj2 in (IEnumerable) base.Items)
            {
                AutoHidePaneHeaderItem item = base.ItemContainerGenerator.ContainerFromItem(obj2) as AutoHidePaneHeaderItem;
                if (item != null)
                {
                    item.SetValue(AutoHideTray.OrientationProperty, this.DockType.ToOrthogonalOrientation());
                    item.Location = this.DockType;
                }
            }
        }

        protected virtual void OnLayoutItemChanged(BaseLayoutItem oldValue, BaseLayoutItem newValue)
        {
            this.EnsureLayoutItem(newValue as AutoHideGroup);
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            this.SubscribeTray();
            if (this.trayCore != null)
            {
                this.AcceptHeaderItems((headerItem, item) => headerItem.IsSelected = ReferenceEquals(this.trayCore.HotItem, item));
                this.AcceptHeaderItems((headerItem, item) => headerItem.IsSelected = ReferenceEquals(this.trayCore.HotItem, item));
            }
        }

        private void OnTrayCollapsed(object sender, RoutedEventArgs e)
        {
            Action<AutoHidePaneHeaderItem, BaseLayoutItem> action = <>c.<>9__33_0;
            if (<>c.<>9__33_0 == null)
            {
                Action<AutoHidePaneHeaderItem, BaseLayoutItem> local1 = <>c.<>9__33_0;
                action = <>c.<>9__33_0 = (headerItem, item) => headerItem.IsSelected = false;
            }
            this.AcceptHeaderItems(action);
        }

        private void OnTrayExpanded(object sender, RoutedEventArgs e)
        {
            this.AcceptHeaderItems((headerItem, item) => headerItem.IsSelected = ReferenceEquals(this.Tray.HotItem, item));
        }

        private void OnTrayHotItemChanged(object sender, HotItemChangedEventArgs e)
        {
            this.AcceptHeaderItems((headerItem, item) => headerItem.IsSelected = ReferenceEquals(e.Hot, item));
        }

        private void OnTrayPanelClosed(object sender, RoutedEventArgs e)
        {
            Action<AutoHidePaneHeaderItem, BaseLayoutItem> action = <>c.<>9__36_0;
            if (<>c.<>9__36_0 == null)
            {
                Action<AutoHidePaneHeaderItem, BaseLayoutItem> local1 = <>c.<>9__36_0;
                action = <>c.<>9__36_0 = (headerItem, item) => headerItem.IsSelected = false;
            }
            this.AcceptHeaderItems(action);
        }

        protected override void OnUnloaded()
        {
            this.UnSubscribeTray();
            base.OnUnloaded();
        }

        protected override void PrepareContainer(DependencyObject element, object item)
        {
            AutoHidePaneHeaderItem item2 = element as AutoHidePaneHeaderItem;
            if (item2 != null)
            {
                item2.SetValue(AutoHideTray.OrientationProperty, this.DockType.ToOrthogonalOrientation());
                item2.Location = this.DockType;
            }
            BaseLayoutItem objB = item as BaseLayoutItem;
            if (objB != null)
            {
                objB.ParentLockHelper.Lock();
                if ((this.Tray != null) && ReferenceEquals(this.Tray.HotItem, objB))
                {
                    objB.SelectTemplateIfNeeded();
                }
                objB.ParentLockHelper.Unlock();
            }
        }

        protected void SubscribeTray()
        {
            this.trayCore = this.Tray;
            if (this.trayCore != null)
            {
                this.trayCore.HotItemChanged += new HotItemChangedEventHandler(this.OnTrayHotItemChanged);
                this.trayCore.Expanded += new RoutedEventHandler(this.OnTrayExpanded);
                this.trayCore.Collapsed += new RoutedEventHandler(this.OnTrayCollapsed);
                this.trayCore.PanelClosed += new RoutedEventHandler(this.OnTrayPanelClosed);
            }
        }

        protected void UnSubscribeTray()
        {
            if (this.trayCore != null)
            {
                this.trayCore.HotItemChanged -= new HotItemChangedEventHandler(this.OnTrayHotItemChanged);
                this.trayCore.Expanded -= new RoutedEventHandler(this.OnTrayExpanded);
                this.trayCore.Collapsed -= new RoutedEventHandler(this.OnTrayCollapsed);
                this.trayCore.PanelClosed -= new RoutedEventHandler(this.OnTrayPanelClosed);
                this.trayCore = null;
            }
        }

        public Dock DockType
        {
            get => 
                (Dock) base.GetValue(DockTypeProperty);
            set => 
                base.SetValue(DockTypeProperty, value);
        }

        public BaseLayoutItem LayoutItem
        {
            get => 
                (BaseLayoutItem) base.GetValue(LayoutItemProperty);
            set => 
                base.SetValue(LayoutItemProperty, value);
        }

        public AutoHideTrayHeadersPanel PartHeadersPanel { get; private set; }

        public AutoHideTray Tray =>
            this.trayCore ?? LayoutHelper.FindParentObject<AutoHideTray>(this);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly AutoHideTrayHeadersGroup.<>c <>9 = new AutoHideTrayHeadersGroup.<>c();
            public static Action<AutoHidePaneHeaderItem, BaseLayoutItem> <>9__33_0;
            public static Action<AutoHidePaneHeaderItem, BaseLayoutItem> <>9__36_0;

            internal void <.cctor>b__2_0(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((AutoHideTrayHeadersGroup) dObj).OnLayoutItemChanged((BaseLayoutItem) ea.OldValue, (BaseLayoutItem) ea.NewValue);
            }

            internal void <.cctor>b__2_1(AutoHideTrayHeadersGroup d, Dock oldValue, Dock newValue)
            {
                d.OnDockTypeChanged(oldValue, newValue);
            }

            internal void <OnTrayCollapsed>b__33_0(AutoHidePaneHeaderItem headerItem, BaseLayoutItem item)
            {
                headerItem.IsSelected = false;
            }

            internal void <OnTrayPanelClosed>b__36_0(AutoHidePaneHeaderItem headerItem, BaseLayoutItem item)
            {
                headerItem.IsSelected = false;
            }
        }
    }
}

