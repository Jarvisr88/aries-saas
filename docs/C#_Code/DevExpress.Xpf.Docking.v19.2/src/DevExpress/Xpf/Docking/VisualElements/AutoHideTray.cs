namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Base;
    using DevExpress.Xpf.Docking.Platform;
    using DevExpress.Xpf.Docking.Platform.Win32;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Threading;

    public class AutoHideTray : psvItemsControl, IUIElement
    {
        public static readonly DependencyProperty OrientationProperty;
        public static readonly DependencyProperty DockTypeProperty;
        private static readonly DependencyPropertyKey DockTypePropertyKey;
        public static readonly DependencyProperty IsExpandedProperty;
        private static readonly DependencyPropertyKey IsExpandedPropertyKey;
        private static readonly DependencyPropertyKey HotItemPropertyKey;
        public static readonly DependencyProperty HotItemProperty;
        public static readonly DependencyProperty IsTopProperty;
        public static readonly DependencyProperty IsLeftProperty;
        public static readonly DependencyProperty IsRightProperty;
        public static readonly DependencyProperty IsBottomProperty;
        private static readonly DependencyPropertyKey IsTopPropertyKey;
        private static readonly DependencyPropertyKey IsLeftPropertyKey;
        private static readonly DependencyPropertyKey IsRightPropertyKey;
        private static readonly DependencyPropertyKey IsBottomPropertyKey;
        private static readonly RoutedEvent ExpandedEvent;
        private static readonly RoutedEvent CollapsedEvent;
        private static readonly RoutedEvent PanelClosedEvent;
        private static readonly RoutedEvent HotItemChangedEvent;
        private static readonly RoutedEvent PanelResizingEvent;
        private static readonly RoutedEvent PanelMaximizedEvent;
        private static readonly RoutedEvent PanelRestoredEvent;
        public static readonly DependencyProperty CheckIntervalProperty;
        public static readonly DependencyProperty ViewStyleProperty;
        public static readonly DependencyProperty ActualBorderThicknessProperty;
        private static readonly DependencyPropertyKey ActualBorderThicknessPropertyKey;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty DockLayoutManagerProperty;
        private UIChildren uiChildren = new UIChildren();
        private readonly DispatcherTimer CloseTimer;
        private IList<UIElement> subscriptions;
        private int lockPanelChanging;
        private Locker ExpandLocker = new Locker();
        private WeakReference lastCollapsedItem;
        private static int lockAutoHide;
        private HitTestHelper.HitCache hitCache;
        private readonly Locker expandingLocker = new Locker();

        public event RoutedEventHandler Collapsed
        {
            add
            {
                base.AddHandler(CollapsedEvent, value);
            }
            remove
            {
                base.RemoveHandler(CollapsedEvent, value);
            }
        }

        public event RoutedEventHandler Expanded
        {
            add
            {
                base.AddHandler(ExpandedEvent, value);
            }
            remove
            {
                base.RemoveHandler(ExpandedEvent, value);
            }
        }

        public event HotItemChangedEventHandler HotItemChanged
        {
            add
            {
                base.AddHandler(HotItemChangedEvent, value);
            }
            remove
            {
                base.RemoveHandler(HotItemChangedEvent, value);
            }
        }

        public event RoutedEventHandler PanelClosed
        {
            add
            {
                base.AddHandler(PanelClosedEvent, value);
            }
            remove
            {
                base.RemoveHandler(PanelClosedEvent, value);
            }
        }

        public event RoutedEventHandler PanelMaximized
        {
            add
            {
                base.AddHandler(PanelMaximizedEvent, value);
            }
            remove
            {
                base.RemoveHandler(PanelMaximizedEvent, value);
            }
        }

        public event PanelResizingEventHandler PanelResizing
        {
            add
            {
                base.AddHandler(PanelResizingEvent, value);
            }
            remove
            {
                base.RemoveHandler(PanelResizingEvent, value);
            }
        }

        public event RoutedEventHandler PanelRestored
        {
            add
            {
                base.AddHandler(PanelRestoredEvent, value);
            }
            remove
            {
                base.RemoveHandler(PanelRestoredEvent, value);
            }
        }

        static AutoHideTray()
        {
            DevExpress.Xpf.Docking.DependencyPropertyRegistrator<AutoHideTray> registrator = new DevExpress.Xpf.Docking.DependencyPropertyRegistrator<AutoHideTray>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.RegisterAttached<Orientation>("Orientation", ref OrientationProperty, Orientation.Vertical, new PropertyChangedCallback(AutoHideTray.OnOrientationChanged), null);
            registrator.RegisterReadonly<Dock>("DockType", ref DockTypePropertyKey, ref DockTypeProperty, Dock.Left, (dObj, e) => ((AutoHideTray) dObj).OnDockTypeChanged((Dock) e.NewValue), null);
            registrator.RegisterReadonly<bool>("IsExpanded", ref IsExpandedPropertyKey, ref IsExpandedProperty, false, (dObj, e) => ((AutoHideTray) dObj).OnIsExpandedChanged((bool) e.NewValue), null);
            registrator.RegisterReadonly<BaseLayoutItem>("HotItem", ref HotItemPropertyKey, ref HotItemProperty, null, (dObj, e) => ((AutoHideTray) dObj).OnHotItemChanged((BaseLayoutItem) e.NewValue, (BaseLayoutItem) e.OldValue), null);
            registrator.Register<TimeSpan>("CheckInterval", ref CheckIntervalProperty, new TimeSpan(0L), (dObj, e) => ((AutoHideTray) dObj).OnCheckIntervalChanged((TimeSpan) e.NewValue), null);
            registrator.RegisterDirectEvent<RoutedEventHandler>("Expanded", ref ExpandedEvent);
            registrator.RegisterDirectEvent<RoutedEventHandler>("Collapsed", ref CollapsedEvent);
            registrator.RegisterDirectEvent<RoutedEventHandler>("PanelClosed", ref PanelClosedEvent);
            registrator.RegisterDirectEvent<HotItemChangedEventHandler>("HotItemChanged", ref HotItemChangedEvent);
            registrator.RegisterDirectEvent<PanelResizingEventHandler>("PanelResizing", ref PanelResizingEvent);
            registrator.RegisterDirectEvent<RoutedEventHandler>("PanelMaximized", ref PanelMaximizedEvent);
            registrator.RegisterDirectEvent<RoutedEventHandler>("PanelRestored", ref PanelRestoredEvent);
            registrator.RegisterAttachedReadonlyInherited<bool>("IsTop", ref IsTopPropertyKey, ref IsTopProperty, false, null, null);
            registrator.RegisterAttachedReadonlyInherited<bool>("IsLeft", ref IsLeftPropertyKey, ref IsLeftProperty, true, null, null);
            registrator.RegisterAttachedReadonlyInherited<bool>("IsRight", ref IsRightPropertyKey, ref IsRightProperty, false, null, null);
            registrator.RegisterAttachedReadonlyInherited<bool>("IsBottom", ref IsBottomPropertyKey, ref IsBottomProperty, false, null, null);
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(AutoHideTray), "d");
            System.Linq.Expressions.Expression[] arguments = new System.Linq.Expressions.Expression[] { expression };
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            expression = System.Linq.Expressions.Expression.Parameter(typeof(AutoHideTray), "d");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<AutoHideTray> registrator1 = DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<AutoHideTray>.New().AddOwner<DockLayoutManager>(System.Linq.Expressions.Expression.Lambda<Func<AutoHideTray, DockLayoutManager>>(System.Linq.Expressions.Expression.Call(null, (MethodInfo) methodof(DockLayoutManager.GetDockLayoutManager), arguments), parameters), out DockLayoutManagerProperty, DockLayoutManager.DockLayoutManagerProperty, null, (d, oldValue, newValue) => d.OnDockLayoutManagerChanged(oldValue, newValue)).OverrideMetadata<Thickness>(Control.BorderThicknessProperty, (d, oldValue, newValue) => d.OnBorderThicknessChanged(oldValue, newValue)).Register<DockingViewStyle>(System.Linq.Expressions.Expression.Lambda<Func<AutoHideTray, DockingViewStyle>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(AutoHideTray.get_ViewStyle)), expressionArray3), out ViewStyleProperty, DockingViewStyle.Default, (d, oldValue, newValue) => d.ViewStyleChanged(oldValue, newValue), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(AutoHideTray), "d");
            ParameterExpression[] expressionArray4 = new ParameterExpression[] { expression };
            Thickness defaultValue = new Thickness();
            frameworkOptions = null;
            registrator1.RegisterReadOnly<Thickness>(System.Linq.Expressions.Expression.Lambda<Func<AutoHideTray, Thickness>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(AutoHideTray.get_ActualBorderThickness)), expressionArray4), out ActualBorderThicknessPropertyKey, out ActualBorderThicknessProperty, defaultValue, (Func<AutoHideTray, Thickness, Thickness>) ((d, value) => d.OnCoerceActualBorderThickness(value)), frameworkOptions);
        }

        public AutoHideTray()
        {
            DockPane.SetHitTestType(this, HitTestType.PageHeaders);
            this.CloseTimer = new DispatcherTimer(DispatcherPriority.Background);
            this.CloseTimer.Tick += new EventHandler(this.OnCloseTimerTick);
        }

        protected void AutoHideByMouse(MouseEventArgs e)
        {
            HitTestResult hit = this.GetHit(this, e);
            HitTestResult panelHit = this.GetHit(this.Panel, e);
            if (!this.TryCollapseByMouse(hit, panelHit) && ((hit != null) && (panelHit == null)))
            {
                DependencyObject visualHit = hit.VisualHit;
                if (visualHit != null)
                {
                    BaseLayoutItem hitItem = GetHitItem(visualHit);
                    if ((hitItem != null) && (!(hitItem is AutoHideGroup) && hitItem.IsEnabled))
                    {
                        if (this.IsExpanded)
                        {
                            if (this.Panel.CanHideCurrentItem)
                            {
                                this.HotItem = hitItem;
                            }
                        }
                        else
                        {
                            Func<WeakReference, object> evaluator = <>c.<>9__153_0;
                            if (<>c.<>9__153_0 == null)
                            {
                                Func<WeakReference, object> local1 = <>c.<>9__153_0;
                                evaluator = <>c.<>9__153_0 = x => x.Target;
                            }
                            if (this.lastCollapsedItem.Return<WeakReference, object>(evaluator, (<>c.<>9__153_1 ??= () => null)) == hitItem)
                            {
                                return;
                            }
                            base.Container.ViewAdapter.ProcessAction(ViewAction.Hiding);
                            this.DoExpand(hitItem);
                        }
                    }
                }
                this.lastCollapsedItem = null;
            }
        }

        private void CheckCloseTimer()
        {
            if (this.IsExpanded && (this.CloseTimer.Interval.Ticks > 0L))
            {
                this.CloseTimer.Start();
            }
            else
            {
                this.CloseTimer.Stop();
            }
        }

        internal void CheckCollapseByMouse(Point? pos = new Point?())
        {
            if (this.IsInVisualTree() && this.Panel.IsInVisualTree())
            {
                Point? nullable = pos;
                Point point = (nullable != null) ? nullable.GetValueOrDefault() : NativeHelper.GetMousePositionSafe();
                HitTestResult trayHit = HitTestHelper.HitTest(this, base.PointFromScreen(point), ref this.hitCache);
                this.TryCollapseByMouse(trayHit, HitTestHelper.HitTest(this.Panel, this.Panel.PointFromScreen(point), ref this.hitCache));
            }
        }

        protected override void ClearContainer(DependencyObject element)
        {
            AutoHideTrayHeadersGroup group = element as AutoHideTrayHeadersGroup;
            if (group != null)
            {
                group.Dispose();
            }
            BaseLayoutItem item = LayoutItemData.ConvertToBaseLayoutItem(element);
            if (item != null)
            {
                item.ClearTemplate();
                AutoHideGroup group2 = item as AutoHideGroup;
                if ((group2 != null) && group2.Items.Contains(this.HotItem))
                {
                    this.ClosePanelCore();
                }
            }
        }

        protected void ClosePanelCore()
        {
            this.lockPanelChanging++;
            this.IsExpanded = false;
            this.RaisePanelEvent(PanelClosedEvent);
            this.HotItem = null;
            this.lockPanelChanging--;
        }

        public void DoClosePanel()
        {
            if (base.HasItems)
            {
                this.ClosePanelCore();
            }
        }

        public void DoCollapse(BaseLayoutItem itemToCollapse = null)
        {
            if (base.HasItems)
            {
                this.LockAutoHide();
                if (this.IsExpanded)
                {
                    this.IsExpanded = false;
                    this.lastCollapsedItem = new WeakReference(itemToCollapse);
                }
                this.UnlockAutoHide();
            }
        }

        public void DoCollapseIfPossible(bool force = false)
        {
            if ((this.Panel.CanHideCurrentItem && (base.Container.AutoHideMode != AutoHideMode.Inline)) | force)
            {
                this.DoCollapse(null);
            }
        }

        public void DoExpand(BaseLayoutItem item)
        {
            if (base.HasItems && !this.ExpandLocker)
            {
                this.ExpandLocker.Lock();
                this.LockAutoHide();
                this.HotItem = item;
                base.Container.CollapseOtherViews(item);
                this.IsExpanded ??= true;
                this.UnlockAutoHide();
                this.ExpandLocker.Unlock();
            }
        }

        public void DoMaximize(BaseLayoutItem item)
        {
            if (!this.IsExpanded)
            {
                this.DoExpand(item);
            }
            else
            {
                this.Maximize(item);
            }
        }

        public void DoResizePanel(double size)
        {
            if (base.HasItems)
            {
                this.LockAutoHide();
                this.RaisePanelResizing(size);
                this.UnlockAutoHide();
            }
        }

        public void DoRestore(BaseLayoutItem item)
        {
            if (this.IsExpanded && ReferenceEquals(this.HotItem, item))
            {
                this.Restore(item);
            }
            else
            {
                this.DoExpand(item);
            }
        }

        protected internal virtual void EnsureAutoHidePanel(AutoHidePane trayPanel)
        {
            this.Panel = trayPanel;
        }

        protected override void EnsureItemsPanelCore(System.Windows.Controls.Panel itemsPanel)
        {
            base.EnsureItemsPanelCore(itemsPanel);
            this.PartHeaderGroupsPanel = itemsPanel as StackPanel;
            if (this.PartHeaderGroupsPanel != null)
            {
                this.PartHeaderGroupsPanel.Orientation = GetOrientation(this);
            }
        }

        private bool EnsureUnSubscribed(UIElement element)
        {
            this.subscriptions ??= new List<UIElement>();
            return !this.subscriptions.Contains(element);
        }

        private void EnsureVisibility(bool hasItems)
        {
            base.Visibility = VisibilityHelper.Convert(hasItems, Visibility.Collapsed);
            if (this.Panel != null)
            {
                this.Panel.IsCollapsed = true;
            }
        }

        protected override DependencyObject GetContainerForItemOverride() => 
            new AutoHideTrayHeadersGroup();

        private HitTestResult GetHit(UIElement element, MouseEventArgs e) => 
            HitTestHelper.HitTest(element, e.GetPosition(element), ref this.hitCache);

        private static BaseLayoutItem GetHitItem(DependencyObject hitObj) => 
            DockLayoutManager.GetLayoutItem(hitObj);

        public static bool GetIsBottom(DependencyObject obj) => 
            (bool) obj.GetValue(IsBottomProperty);

        public static bool GetIsLeft(DependencyObject obj) => 
            (bool) obj.GetValue(IsLeftProperty);

        public static bool GetIsRight(DependencyObject obj) => 
            (bool) obj.GetValue(IsRightProperty);

        public static bool GetIsTop(DependencyObject obj) => 
            (bool) obj.GetValue(IsTopProperty);

        public BaseLayoutItem[] GetItems()
        {
            List<BaseLayoutItem> list = new List<BaseLayoutItem>();
            foreach (AutoHideGroup group in (IEnumerable) base.Items)
            {
                list.AddRange(group.GetItems());
            }
            return list.ToArray();
        }

        public static Orientation GetOrientation(DependencyObject obj) => 
            (Orientation) obj.GetValue(OrientationProperty);

        private AutoHideExpandMode GetRealAutoHideMode() => 
            base.Container.IsInDesignTime ? AutoHideExpandMode.MouseDown : base.Container.AutoHideExpandMode;

        private static void InvalidateView(AutoHideTray tray)
        {
            IView view = tray.Container.GetView(tray);
            if (view != null)
            {
                view.Invalidate();
            }
        }

        protected override bool IsItemItsOwnContainerOverride(object item) => 
            (item is BaseLayoutItem) || (item is AutoHideTrayHeadersGroup);

        public void LockAutoHide()
        {
            lockAutoHide++;
        }

        internal IDisposable LockExpanding() => 
            this.expandingLocker.Lock();

        private void Maximize(BaseLayoutItem item)
        {
            if (base.HasItems)
            {
                this.LockAutoHide();
                this.HotItem = item;
                base.Container.CollapseOtherViews(item);
                this.RaisePanelEvent(PanelMaximizedEvent);
                this.UnlockAutoHide();
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.EnsureVisibility(base.HasItems);
            if (this.Panel != null)
            {
                DockLayoutManager.Ensure(this.Panel, false);
            }
            if (base.Container != null)
            {
                this.View = base.Container.GetView(this) as AutoHideView;
                if (this.View != null)
                {
                    this.View.Initialize(this);
                }
            }
        }

        private void OnBorderThicknessChanged(Thickness oldValue, Thickness newValue)
        {
            base.CoerceValue(ActualBorderThicknessProperty);
        }

        private void OnCheckIntervalChanged(TimeSpan timeSpan)
        {
            this.CloseTimer.Interval = timeSpan;
            this.CheckCloseTimer();
        }

        private void OnCloseTimerTick(object sender, EventArgs eventArgs)
        {
            Point? pos = null;
            this.CheckCollapseByMouse(pos);
        }

        private Thickness OnCoerceActualBorderThickness(Thickness value) => 
            (this.ViewStyle == DockingViewStyle.Light) ? this.DockType.RotateThickness(base.BorderThickness) : value;

        protected override void OnDispose()
        {
            this.CloseTimer.Stop();
            Ref.Dispose<HitTestHelper.HitCache>(ref this.hitCache);
            this.UnSubscribe();
            this.View = null;
            base.OnDispose();
        }

        private void OnDockLayoutManagerChanged(DockLayoutManager oldValue, DockLayoutManager newValue)
        {
            if (newValue != null)
            {
                BindingHelper.SetBinding(this, ViewStyleProperty, newValue, DockLayoutManager.ViewStyleProperty, BindingMode.OneWay);
            }
            else
            {
                base.ClearValue(ViewStyleProperty);
            }
        }

        protected virtual void OnDockTypeChanged(Dock value)
        {
            SetIsLeft(this, value == Dock.Left);
            SetIsRight(this, value == Dock.Right);
            SetIsTop(this, value == Dock.Top);
            SetIsBottom(this, value == Dock.Bottom);
            base.CoerceValue(ActualBorderThicknessProperty);
        }

        protected override void OnHasItemsChanged(bool hasItems)
        {
            this.EnsureVisibility(hasItems);
        }

        protected virtual void OnHotItemChanged(BaseLayoutItem item, BaseLayoutItem oldItem)
        {
            using (this.LockExpanding())
            {
                InvalidateView(this);
                bool isExpanded = this.IsExpanded;
                if (isExpanded)
                {
                    this.IsExpanded = false;
                }
                this.RaiseHotItemChanged(item, oldItem);
                if (isExpanded)
                {
                    this.IsExpanded = true;
                }
            }
        }

        protected virtual void OnIsExpandedChanged(bool value)
        {
            if (!base.IsDisposing)
            {
                InvalidateView(this);
                this.CheckCloseTimer();
                if (value)
                {
                    this.RaiseExpanded();
                }
                else
                {
                    this.RaiseCollapsed();
                }
            }
        }

        protected override void OnLoaded()
        {
            this.Subscribe();
        }

        private static void OnOrientationChanged(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.InvalidateMeasure();
                element.InvalidateArrange();
            }
        }

        protected override void OnUnloaded()
        {
            this.CloseTimer.Stop();
            this.UnSubscribe();
        }

        protected override void PrepareContainer(DependencyObject element, object item)
        {
            AutoHideTrayHeadersGroup group = element as AutoHideTrayHeadersGroup;
            if ((group != null) && DockLayoutManagerExtension.IsInContainer(this))
            {
                group.SetValue(OrientationProperty, GetOrientation(this));
                group.EnsureLayoutItem(item as AutoHideGroup);
            }
            BaseLayoutItem item2 = LayoutItemData.ConvertToBaseLayoutItem(element);
            if (item2 != null)
            {
                item2.SelectTemplate();
            }
        }

        protected virtual void RaiseCollapsed()
        {
            if (this.lockPanelChanging <= 0)
            {
                this.lockPanelChanging++;
                this.RaisePanelEvent(CollapsedEvent);
                this.lockPanelChanging--;
            }
        }

        protected virtual void RaiseExpanded()
        {
            if (this.lockPanelChanging <= 0)
            {
                this.lockPanelChanging++;
                this.RaisePanelEvent(ExpandedEvent);
                this.lockPanelChanging--;
            }
        }

        protected virtual void RaiseHotItemChanged(BaseLayoutItem value, BaseLayoutItem prev)
        {
            if (this.lockPanelChanging <= 0)
            {
                this.lockPanelChanging++;
                HotItemChangedEventArgs e = new HotItemChangedEventArgs(value, prev);
                e.RoutedEvent = HotItemChangedEvent;
                e.Source = this;
                base.RaiseEvent(e);
                this.lockPanelChanging--;
            }
        }

        protected void RaisePanelEvent(RoutedEvent routedEvent)
        {
            RoutedEventArgs e = new RoutedEventArgs();
            e.RoutedEvent = routedEvent;
            e.Source = this;
            base.RaiseEvent(e);
        }

        protected virtual void RaisePanelResizing(double size)
        {
            PanelResizingEventArgs e = new PanelResizingEventArgs(size);
            e.RoutedEvent = PanelResizingEvent;
            e.Source = this;
            base.RaiseEvent(e);
        }

        private void Restore(BaseLayoutItem item)
        {
            if (base.HasItems)
            {
                this.LockAutoHide();
                base.Container.CollapseOtherViews(item);
                this.RaisePanelEvent(PanelRestoredEvent);
                this.UnlockAutoHide();
            }
        }

        private static void SetIsBottom(DependencyObject obj, bool value)
        {
            obj.SetValue(IsBottomPropertyKey, value);
        }

        private static void SetIsLeft(DependencyObject obj, bool value)
        {
            obj.SetValue(IsLeftPropertyKey, value);
        }

        private static void SetIsRight(DependencyObject obj, bool value)
        {
            obj.SetValue(IsRightPropertyKey, value);
        }

        private static void SetIsTop(DependencyObject obj, bool value)
        {
            obj.SetValue(IsTopPropertyKey, value);
        }

        public static void SetOrientation(DependencyObject obj, Orientation value)
        {
            obj.SetValue(OrientationProperty, value);
        }

        protected void Subscribe()
        {
            this.SubscribeTrayMouseMove(this);
            this.SubscribeTrayMouseMove(this.Panel);
            this.SubscribeTrayMouseMove(base.Container);
            Mouse.AddMouseEnterHandler(this, new MouseEventHandler(this.TrayMouseEnter));
        }

        private void SubscribeTrayMouseMove(UIElement element)
        {
            if ((element != null) && this.EnsureUnSubscribed(element))
            {
                Mouse.AddPreviewMouseMoveHandler(element, new MouseEventHandler(this.TrayMouseMove));
                this.subscriptions.Add(element);
            }
        }

        protected void TrayMouseEnter(object sender, MouseEventArgs e)
        {
            if (this.View != null)
            {
                if (this.View.LayoutRoot == null)
                {
                    this.View.EnsureLayoutRoot();
                }
                else if (!this.View.LayoutRoot.IsReady)
                {
                    this.View.LayoutRoot.Invalidate();
                }
            }
            this.lastCollapsedItem = null;
        }

        protected void TrayMouseMove(object sender, MouseEventArgs e)
        {
            if ((PopupMenuManager.TopPopup == null) && (!this.ExpandOnMouseDown && (!this.IsAutoHideLocked && (!base.Container.ViewAdapter.IsInEvent && base.HasItems))))
            {
                this.LockAutoHide();
                this.AutoHideByMouse(e);
                this.UnlockAutoHide();
            }
        }

        private bool TryCollapseByMouse(HitTestResult trayHit, HitTestResult panelHit)
        {
            bool flag = ((trayHit == null) && ((panelHit == null) && (this.IsExpanded && this.Panel.CanCollapse))) && (base.Container.AutoHideMode != AutoHideMode.Inline);
            if (flag)
            {
                this.DoCollapse(null);
                this.lastCollapsedItem = null;
            }
            return flag;
        }

        public void UnlockAutoHide()
        {
            lockAutoHide--;
        }

        protected void UnSubscribe()
        {
            this.UnSubscribeTrayMouseMove(this);
            this.UnSubscribeTrayMouseMove(this.Panel);
            this.UnSubscribeTrayMouseMove(base.Container);
            Mouse.RemoveMouseEnterHandler(this, new MouseEventHandler(this.TrayMouseEnter));
        }

        private void UnSubscribeTrayMouseMove(UIElement element)
        {
            if (element != null)
            {
                if (this.subscriptions != null)
                {
                    this.subscriptions.Remove(element);
                }
                Mouse.RemovePreviewMouseMoveHandler(element, new MouseEventHandler(this.TrayMouseMove));
            }
        }

        private void ViewStyleChanged(DockingViewStyle oldValue, DockingViewStyle newValue)
        {
            base.CoerceValue(ActualBorderThicknessProperty);
        }

        IUIElement IUIElement.Scope =>
            DockLayoutManager.GetDockLayoutManager(this);

        UIChildren IUIElement.Children
        {
            get
            {
                this.uiChildren ??= new UIChildren();
                return this.uiChildren;
            }
        }

        public Thickness ActualBorderThickness =>
            (Thickness) base.GetValue(ActualBorderThicknessProperty);

        public DockingViewStyle ViewStyle
        {
            get => 
                (DockingViewStyle) base.GetValue(ViewStyleProperty);
            set => 
                base.SetValue(ViewStyleProperty, value);
        }

        protected AutoHideView View { get; private set; }

        public bool IsHorizontal =>
            GetOrientation(this) == Orientation.Horizontal;

        protected bool ExpandOnMouseDown =>
            (base.Container != null) && (this.GetRealAutoHideMode() == AutoHideExpandMode.MouseDown);

        protected StackPanel PartHeaderGroupsPanel { get; private set; }

        public TimeSpan CheckInterval
        {
            get => 
                (TimeSpan) base.GetValue(CheckIntervalProperty);
            set => 
                base.SetValue(CheckIntervalProperty, value);
        }

        public Dock DockType
        {
            get => 
                (Dock) base.GetValue(DockTypeProperty);
            internal set => 
                base.SetValue(DockTypePropertyKey, value);
        }

        public bool IsAnimated { get; internal set; }

        public bool IsExpanded
        {
            get => 
                (bool) base.GetValue(IsExpandedProperty);
            private set => 
                base.SetValue(IsExpandedPropertyKey, value);
        }

        public BaseLayoutItem HotItem
        {
            get => 
                (BaseLayoutItem) base.GetValue(HotItemProperty);
            private set => 
                base.SetValue(HotItemPropertyKey, value);
        }

        public AutoHidePane Panel { get; private set; }

        public bool IsAutoHideLocked =>
            lockAutoHide > 0;

        internal bool IsExpandingLocked =>
            (bool) this.expandingLocker;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly AutoHideTray.<>c <>9 = new AutoHideTray.<>c();
            public static Func<WeakReference, object> <>9__153_0;
            public static Func<object> <>9__153_1;

            internal void <.cctor>b__27_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((AutoHideTray) dObj).OnDockTypeChanged((Dock) e.NewValue);
            }

            internal void <.cctor>b__27_1(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((AutoHideTray) dObj).OnIsExpandedChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__27_2(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((AutoHideTray) dObj).OnHotItemChanged((BaseLayoutItem) e.NewValue, (BaseLayoutItem) e.OldValue);
            }

            internal void <.cctor>b__27_3(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((AutoHideTray) dObj).OnCheckIntervalChanged((TimeSpan) e.NewValue);
            }

            internal void <.cctor>b__27_4(AutoHideTray d, DockLayoutManager oldValue, DockLayoutManager newValue)
            {
                d.OnDockLayoutManagerChanged(oldValue, newValue);
            }

            internal void <.cctor>b__27_5(AutoHideTray d, Thickness oldValue, Thickness newValue)
            {
                d.OnBorderThicknessChanged(oldValue, newValue);
            }

            internal void <.cctor>b__27_6(AutoHideTray d, DockingViewStyle oldValue, DockingViewStyle newValue)
            {
                d.ViewStyleChanged(oldValue, newValue);
            }

            internal Thickness <.cctor>b__27_7(AutoHideTray d, Thickness value) => 
                d.OnCoerceActualBorderThickness(value);

            internal object <AutoHideByMouse>b__153_0(WeakReference x) => 
                x.Target;

            internal object <AutoHideByMouse>b__153_1() => 
                null;
        }
    }
}

