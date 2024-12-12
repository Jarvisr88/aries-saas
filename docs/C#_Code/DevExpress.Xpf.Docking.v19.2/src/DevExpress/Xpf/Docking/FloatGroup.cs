namespace DevExpress.Xpf.Docking
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Core.Serialization;
    using DevExpress.Xpf.Docking.Platform;
    using DevExpress.Xpf.Docking.VisualElements;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Threading;

    public class FloatGroup : LayoutGroup, IClosable
    {
        public static readonly DependencyProperty IsOpenProperty;
        public static readonly DependencyProperty FloatLocationProperty;
        private static readonly DependencyPropertyKey BorderStylePropertyKey;
        public static readonly DependencyProperty BorderStyleProperty;
        private static readonly DependencyPropertyKey IsMaximizedPropertyKey;
        public static readonly DependencyProperty IsMaximizedProperty;
        public static readonly DependencyProperty FloatStateProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty ActualVisibilityProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        internal static readonly DependencyProperty IsActuallyVisibleProperty;
        private static readonly DependencyPropertyKey IsActuallyVisiblePropertyKey;
        [IgnoreDependencyPropertiesConsistencyChecker]
        internal static readonly DependencyProperty CanMaximizeProperty;
        private static readonly DependencyPropertyKey CanMaximizePropertyKey;
        [IgnoreDependencyPropertiesConsistencyChecker]
        internal static readonly DependencyProperty CanMinimizeProperty;
        private static readonly DependencyPropertyKey CanMinimizePropertyKey;
        public static readonly DependencyProperty SizeToContentProperty;
        public static readonly DependencyProperty WindowTaskbarIconProperty;
        public static readonly DependencyProperty WindowTaskbarTitleProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        internal static readonly DependencyProperty ActualWindowTaskbarIconProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        internal static readonly DependencyProperty ActualWindowTaskbarTitleProperty;
        public static readonly DependencyProperty WindowStyleProperty;
        private readonly LockHelper _visualTreeLocker;
        private readonly Locker checkFloatStateLocker;
        private readonly Locker floatStateLocker;
        private readonly Locker sizeToContentLocker;
        private Point floatOffsetBeforeClose;
        private Locker _AutoSizeLocker;
        private LockHelper _FloatStateLockHelper;
        private DevExpress.Xpf.Docking.FloatState _PreviousFloatState;
        private bool _SerializableIsMaximized;
        private DispatcherOperation _SerializableIsMaximizedOperation;
        private int _SerizalizableZOrder;

        internal event EventHandler WindowLoaded;

        static FloatGroup()
        {
            DevExpress.Xpf.Docking.DependencyPropertyRegistrator<FloatGroup> registrator = new DevExpress.Xpf.Docking.DependencyPropertyRegistrator<FloatGroup>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.OverrideMetadata<bool>(BaseLayoutItem.AllowSelectionProperty, false, null, null);
            registrator.Register<bool>("IsOpen", ref IsOpenProperty, true, null, new CoerceValueCallback(FloatGroup.CoerceIsOpen));
            registrator.Register<Point>("FloatLocation", ref FloatLocationProperty, new Point(0.0, 0.0), new PropertyChangedCallback(FloatGroup.OnFloatLocationChanged), null);
            registrator.RegisterReadonly<FloatGroupBorderStyle>("BorderStyle", ref BorderStylePropertyKey, ref BorderStyleProperty, FloatGroupBorderStyle.Empty, (dObj, e) => ((FloatGroup) dObj).OnBorderStyleChanged(), (CoerceValueCallback) ((dObj, value) => ((FloatGroup) dObj).CoerceBorderStyle((FloatGroupBorderStyle) value)));
            registrator.RegisterReadonly<bool>("IsMaximized", ref IsMaximizedPropertyKey, ref IsMaximizedProperty, false, null, (CoerceValueCallback) ((dObj, value) => ((FloatGroup) dObj).CoerceIsMaximized((bool) value)));
            registrator.RegisterAttached<DevExpress.Xpf.Docking.FloatState>("FloatState", ref FloatStateProperty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, DevExpress.Xpf.Docking.FloatState.Normal, new PropertyChangedCallback(FloatGroup.OnFloatStateChanged), new CoerceValueCallback(FloatGroup.CoerceFloatState));
            registrator.Register<Visibility>("ActualVisibility", ref ActualVisibilityProperty, Visibility.Visible, (dObj, e) => ((FloatGroup) dObj).OnActualVisibilityChanged((Visibility) e.OldValue, (Visibility) e.NewValue), (CoerceValueCallback) ((dObj, value) => ((FloatGroup) dObj).CoerceActualVisibility((Visibility) value)));
            registrator.RegisterReadonly<bool>("IsActuallyVisible", ref IsActuallyVisiblePropertyKey, ref IsActuallyVisibleProperty, true, null, null);
            registrator.RegisterReadonly<bool>("CanMaximize", ref CanMaximizePropertyKey, ref CanMaximizeProperty, true, null, (CoerceValueCallback) ((dObj, value) => ((FloatGroup) dObj).CoerceCanMaximize((bool) value)));
            registrator.RegisterReadonly<bool>("CanMinimize", ref CanMinimizePropertyKey, ref CanMinimizeProperty, true, null, (CoerceValueCallback) ((dObj, value) => ((FloatGroup) dObj).CoerceCanMinimize((bool) value)));
            registrator.RegisterAttached<System.Windows.SizeToContent>("SizeToContent", ref SizeToContentProperty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, System.Windows.SizeToContent.Manual, new PropertyChangedCallback(FloatGroup.OnSizeToContentChanged), new CoerceValueCallback(FloatGroup.CoerceSizeToContent));
            registrator.Register<Style>("WindowStyle", ref WindowStyleProperty, null, (dObj, e) => ((FloatGroup) dObj).OnWindowStyleChanged((Style) e.NewValue), null);
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(DependencyObject), "d");
            System.Linq.Expressions.Expression[] arguments = new System.Linq.Expressions.Expression[] { expression };
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DependencyObject), "d");
            System.Linq.Expressions.Expression[] expressionArray3 = new System.Linq.Expressions.Expression[] { expression };
            ParameterExpression[] expressionArray4 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            expression = System.Linq.Expressions.Expression.Parameter(typeof(FloatGroup), "d");
            ParameterExpression[] expressionArray5 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<FloatGroup> registrator1 = DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<FloatGroup>.New().RegisterAttached<DependencyObject, ImageSource>(System.Linq.Expressions.Expression.Lambda<Func<DependencyObject, ImageSource>>(System.Linq.Expressions.Expression.Call(null, (MethodInfo) methodof(FloatGroup.GetWindowTaskbarIcon), arguments), parameters), out WindowTaskbarIconProperty, null, (d, e) => OnWindowTaskbarIconChanged(d, e), frameworkOptions).RegisterAttached<DependencyObject, string>(System.Linq.Expressions.Expression.Lambda<Func<DependencyObject, string>>(System.Linq.Expressions.Expression.Call(null, (MethodInfo) methodof(FloatGroup.GetWindowTaskbarTitle), expressionArray3), expressionArray4), out WindowTaskbarTitleProperty, string.Empty, (d, e) => OnWindowTaskbarTitleChanged(d, e), frameworkOptions).Register<ImageSource>(System.Linq.Expressions.Expression.Lambda<Func<FloatGroup, ImageSource>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(FloatGroup.get_ActualWindowTaskbarIcon)), expressionArray5), out ActualWindowTaskbarIconProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(FloatGroup), "d");
            ParameterExpression[] expressionArray6 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            registrator1.Register<string>(System.Linq.Expressions.Expression.Lambda<Func<FloatGroup, string>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(FloatGroup.get_ActualWindowTaskbarTitle)), expressionArray6), out ActualWindowTaskbarTitleProperty, string.Empty, frameworkOptions);
        }

        public FloatGroup()
        {
            this._visualTreeLocker = new LockHelper();
            this.checkFloatStateLocker = new Locker();
            this.floatStateLocker = new Locker();
            this.sizeToContentLocker = new Locker();
        }

        internal FloatGroup(params BaseLayoutItem[] items) : base(items)
        {
            this._visualTreeLocker = new LockHelper();
            this.checkFloatStateLocker = new Locker();
            this.floatStateLocker = new Locker();
            this.sizeToContentLocker = new Locker();
        }

        protected internal override void AfterItemAdded(int index, BaseLayoutItem item)
        {
            base.AfterItemAdded(index, item);
            this.UpdateSingleItemState();
        }

        protected internal override void AfterItemRemoved(BaseLayoutItem item)
        {
            base.AfterItemRemoved(item);
            this.UpdateSingleItemState();
        }

        internal override bool AllowSerializeProperty(AllowPropertyEventArgs e) => 
            (e.Property.Name != "SerializableIsMaximized") && base.AllowSerializeProperty(e);

        internal override void ApplySerializationInfo()
        {
            base.ApplySerializationInfo();
            this._PreviousFloatState = this.FloatGroupSerializationInfo.PreviousFloatState;
            this.FloatState = this.FloatGroupSerializationInfo.FloatState;
            Rect windowRestoreBounds = this.FloatGroupSerializationInfo.WindowRestoreBounds;
            if (!windowRestoreBounds.IsEmpty)
            {
                Size size = new Size();
                if ((windowRestoreBounds.Size != size) && (this.FloatGroupSerializationInfo.FloatState == DevExpress.Xpf.Docking.FloatState.Normal))
                {
                    this.FloatLocation = windowRestoreBounds.Location;
                    base.FloatSize = windowRestoreBounds.Size;
                    this.FloatOffsetBeforeClose = this.FloatGroupSerializationInfo.FloatOffsetBeforeClose;
                }
            }
        }

        protected override Size CalcGroupMinSize(Size[] minSizes, bool fHorz)
        {
            Size minSize = base.CalcGroupMinSize(minSizes, fHorz);
            return this.CalcMinSizeWithBorderMargin(minSize);
        }

        protected Rect CalcMaximizedBounds(Rect restoreBounds) => 
            this.CalcMaximizedBounds(base.Manager, restoreBounds);

        private Rect CalcMaximizedBounds(DockLayoutManager manager, Rect restoreBounds)
        {
            Rect maximizeBounds = WindowHelper.GetMaximizeBounds(manager, restoreBounds);
            if ((manager != null) && ((manager.FlowDirection == FlowDirection.RightToLeft) && (manager.GetRealFloatingMode() == DevExpress.Xpf.Core.FloatingMode.Window)))
            {
                maximizeBounds.X = -maximizeBounds.X - maximizeBounds.Width;
            }
            return maximizeBounds;
        }

        private Size CalcMinSizeWithBorderMargin(Size minSize) => 
            (this.BorderStyle != FloatGroupBorderStyle.Single) ? new Size(minSize.Width + 22.0, minSize.Height + 31.0) : new Size(minSize.Width + 14.0, minSize.Height + 14.0);

        internal void CheckFloatState()
        {
            if (((base.Manager != null) && ScreenHelper.IsAttachedToPresentationSource(base.Manager)) && this.checkFloatStateLocker)
            {
                this.checkFloatStateLocker.Unlock();
                if (this._PreviousFloatState != this.FloatState)
                {
                    this.OnFloatStateChangedCore(this._PreviousFloatState, this.FloatState);
                }
            }
        }

        protected override Thickness CoerceActualMargin(Thickness value) => 
            this.HasSingleDocument ? (MathHelper.AreEqual(base.Margin, new Thickness(double.NaN)) ? value : base.Margin) : base.CoerceActualMargin(value);

        protected virtual Visibility CoerceActualVisibility(Visibility visibility)
        {
            DockLayoutManager manager = base.Manager ?? (base.ManagerReference.Return<WeakReference, object>((<>c.<>9__170_0 ??= x => x.Target), (<>c.<>9__170_1 ??= () => null)) as DockLayoutManager);
            return (((manager == null) || (((manager.GetRealFloatingMode() != DevExpress.Xpf.Core.FloatingMode.Adorner) || (this.FloatState != DevExpress.Xpf.Docking.FloatState.Minimized)) && (manager.IsVisible || manager.CanShowFloatGroup))) ? ((!this.HasItems || (!base.HasVisibleItems || !base.HasNotCollapsedItems)) ? Visibility.Collapsed : base.Visibility) : Visibility.Collapsed);
        }

        protected override object CoerceAllowSizing(bool value) => 
            base.HasSingleItem ? base.Items[0].AllowSizing : value;

        protected virtual FloatGroupBorderStyle CoerceBorderStyle(FloatGroupBorderStyle borderStyle) => 
            (base.Items.Count <= 0) ? borderStyle : (!base.HasSingleItem ? FloatGroupBorderStyle.Form : ((base.Items[0] is DocumentPanel) ? FloatGroupBorderStyle.Empty : FloatGroupBorderStyle.Single));

        protected virtual bool CoerceCanMaximize(bool canMaximize) => 
            this.IsMaximizable && (base.AllowMaximize && (!base.HasSingleItem || (base.Items[0].IsMaximizable && base.Items[0].AllowMaximize)));

        protected virtual bool CoerceCanMinimize(bool canMinimize) => 
            this.IsMinimizable && (base.AllowMinimize && (!base.HasSingleItem || (base.Items[0].IsMinimizable && base.Items[0].AllowMinimize)));

        protected override Size CoerceFloatSize(Size value)
        {
            if ((base.Manager != null) && (base.Manager.GetRealFloatingMode() == DevExpress.Xpf.Core.FloatingMode.Window))
            {
                Size minSize = new Size();
                value = MathHelper.MeasureSize(minSize, new Size(SystemParameters.VirtualScreenWidth, SystemParameters.VirtualScreenHeight), value);
            }
            return base.CoerceFloatSize(value);
        }

        protected virtual DevExpress.Xpf.Docking.FloatState CoerceFloatState(DevExpress.Xpf.Docking.FloatState newState)
        {
            DevExpress.Xpf.Docking.FloatState floatState = this.FloatState;
            return (((newState != DevExpress.Xpf.Docking.FloatState.Minimized) || this.CanMinimize) ? (((newState != DevExpress.Xpf.Docking.FloatState.Maximized) || this.CanMaximize) ? newState : floatState) : floatState);
        }

        private static object CoerceFloatState(DependencyObject dObj, object baseValue)
        {
            FloatGroup group = dObj as FloatGroup;
            return ((group != null) ? group.CoerceFloatState((DevExpress.Xpf.Docking.FloatState) baseValue) : baseValue);
        }

        protected override bool CoerceIsCloseButtonVisible(bool visible) => 
            (!base.HasSingleItem || !(base.Items[0] is LayoutPanel)) ? (base.AllowClose && base.ShowCloseButton) : base.Items[0].IsCloseButtonVisible;

        protected override bool CoerceIsMaximizeButtonVisible(bool visible) => 
            this.IsMaximizable && (base.AllowMaximize && !this.IsMaximized);

        protected virtual bool CoerceIsMaximized(bool maximized) => 
            maximized && this.CanMaximize;

        protected override bool CoerceIsMinimizeButtonVisible(bool visible) => 
            this.IsMinimizable && (base.AllowMinimize && (this.FloatState != DevExpress.Xpf.Docking.FloatState.Minimized));

        protected virtual bool CoerceIsOpen(bool baseValue)
        {
            Func<DockLayoutManager, bool> evaluator = <>c.<>9__181_0;
            if (<>c.<>9__181_0 == null)
            {
                Func<DockLayoutManager, bool> local1 = <>c.<>9__181_0;
                evaluator = <>c.<>9__181_0 = x => x.EnableWin32Compatibility;
            }
            return ((!((DockLayoutManager) evaluator).Return<DockLayoutManager, bool>(evaluator, (<>c.<>9__181_1 ??= () => false)) || this.IsActuallyVisible) & baseValue);
        }

        private static object CoerceIsOpen(DependencyObject dObj, object baseValue)
        {
            FloatGroup group = dObj as FloatGroup;
            return ((group != null) ? group.CoerceIsOpen((bool) baseValue) : baseValue);
        }

        protected override bool CoerceIsRestoreButtonVisible(bool visible) => 
            this.FloatState != DevExpress.Xpf.Docking.FloatState.Normal;

        protected virtual object CoerceSizeToContent(System.Windows.SizeToContent baseValue) => 
            (!base.HasSingleItem || !this.sizeToContentLocker) ? baseValue : GetSizeToContent(base.Items[0]);

        private static object CoerceSizeToContent(DependencyObject dObj, object baseValue)
        {
            FloatGroup group = dObj as FloatGroup;
            return ((group != null) ? group.CoerceSizeToContent((System.Windows.SizeToContent) baseValue) : baseValue);
        }

        internal override void CollectSerializationInfo()
        {
            base.CollectSerializationInfo();
            this.FloatGroupSerializationInfo.FloatState = this.FloatState;
            this.FloatGroupSerializationInfo.PreviousFloatState = this._PreviousFloatState;
            this.FloatGroupSerializationInfo.WindowRestoreBounds = (this.Window != null) ? this.Window.RestoreBoundsToFloatBounds() : Rect.Empty;
            this.FloatGroupSerializationInfo.FloatOffsetBeforeClose = this.FloatOffsetBeforeClose;
        }

        protected override BaseLayoutItemSerializationInfo CreateSerializationInfo() => 
            new DevExpress.Xpf.Docking.FloatGroupSerializationInfo(this);

        bool IClosable.CanClose()
        {
            CancelEventArgs e = new CancelEventArgs();
            this.OnClosing(e);
            return !e.Cancel;
        }

        void IClosable.OnClosed()
        {
            this.OnClosed();
        }

        internal void DisableSizeToContent()
        {
            if (this.SizeToContent != System.Windows.SizeToContent.Manual)
            {
                base.SetCurrentValue(SizeToContentProperty, System.Windows.SizeToContent.Manual);
            }
        }

        internal void EnsureFloatLocation(Point floatLocation)
        {
            Point point = new Point(this.FloatLocation.X - floatLocation.X, this.FloatLocation.Y - floatLocation.Y);
            if (this.IsMaximized || this.IsMinimized)
            {
                Rect restoreBounds = DocumentPanel.GetRestoreBounds(this);
                Point location = restoreBounds.Location;
                restoreBounds.Location = new Point(location.X - point.X, location.Y - point.Y);
                DocumentPanel.SetRestoreBounds(this, restoreBounds);
            }
            this.FloatLocation = floatLocation;
        }

        internal void EnsureFloatLocation(Point prevOffset, Point newOffset)
        {
            int num = (base.FlowDirection == FlowDirection.RightToLeft) ? -1 : 1;
            this.EnsureFloatLocation(new Point(this.FloatLocation.X - (num * (newOffset.X - prevOffset.X)), this.FloatLocation.Y - (newOffset.Y - prevOffset.Y)));
        }

        internal void FitSizeToContent(Size screenSize)
        {
            base.FloatSize = SizeToContentHelper.FitSizeToContent(this.SizeToContent, base.FloatSize, screenSize);
        }

        [XtraSerializableProperty(1)]
        public static DevExpress.Xpf.Docking.FloatState GetFloatState(DependencyObject target) => 
            (DevExpress.Xpf.Docking.FloatState) target.GetValue(FloatStateProperty);

        protected override LayoutItemType GetLayoutItemTypeCore() => 
            LayoutItemType.FloatGroup;

        internal int GetSerializableZOrder() => 
            this._SerizalizableZOrder;

        public static System.Windows.SizeToContent GetSizeToContent(DependencyObject target) => 
            (System.Windows.SizeToContent) target.GetValue(SizeToContentProperty);

        public static ImageSource GetWindowTaskbarIcon(DependencyObject dObj) => 
            (ImageSource) dObj.GetValue(WindowTaskbarIconProperty);

        public static string GetWindowTaskbarTitle(DependencyObject dObj) => 
            (string) dObj.GetValue(WindowTaskbarTitleProperty);

        internal override IDisposable LockVisualTree() => 
            this._visualTreeLocker.Lock();

        protected override void OnActualMaxSizeChanged()
        {
            using (this.AutoSizeLocker.Lock())
            {
                base.OnActualMaxSizeChanged();
            }
        }

        protected override void OnActualMinSizeChanged()
        {
            using (this.AutoSizeLocker.Lock())
            {
                base.OnActualMinSizeChanged();
            }
        }

        protected virtual void OnActualVisibilityChanged(Visibility oldValue, Visibility newValue)
        {
            base.SetValue(IsActuallyVisiblePropertyKey, this.ActualVisibility == Visibility.Visible);
            base.CoerceValue(IsOpenProperty);
            if (newValue == Visibility.Visible)
            {
                base.Manager.Do<DockLayoutManager>(x => x.InvalidateView(this));
            }
        }

        protected override void OnAllowMaximizeChanged(bool value)
        {
            base.OnAllowMaximizeChanged(value);
            base.CoerceValue(CanMaximizeProperty);
        }

        protected override void OnAllowMinimizeChanged(bool value)
        {
            base.OnAllowMinimizeChanged(value);
            base.CoerceValue(CanMinimizeProperty);
        }

        protected virtual void OnBorderStyleChanged()
        {
            base.CoerceValue(BaseLayoutItem.ActualMinSizeProperty);
        }

        protected virtual void OnClosed()
        {
        }

        protected virtual void OnClosing(CancelEventArgs e)
        {
        }

        protected override void OnCreate()
        {
            base.OnCreate();
            base.CoerceValue(ActualVisibilityProperty);
        }

        protected override void OnDockLayoutManagerChanged()
        {
            base.OnDockLayoutManagerChanged();
            if (base.Manager != null)
            {
                DockLayoutManager.AddLogicalChild(base.Manager, this);
                base.CoerceValue(BaseLayoutItem.FloatSizeProperty);
                this.CheckFloatState();
            }
            base.CoerceValue(System.Windows.UIElement.IsEnabledProperty);
            base.CoerceValue(IsOpenProperty);
        }

        private static void OnFloatLocationChanged(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
        {
            ((FloatGroup) dObj).OnFloatLocationChanged((Point) e.OldValue, (Point) e.NewValue);
        }

        protected virtual void OnFloatLocationChanged(Point oldValue, Point newValue)
        {
        }

        protected override void OnFloatSizeChanged(Size oldValue, Size newValue)
        {
            base.OnFloatSizeChanged(oldValue, newValue);
            if (!this.AutoSizeLocker && (this.SizeToContent != System.Windows.SizeToContent.Manual))
            {
                Func<DevExpress.Xpf.Docking.Platform.FloatingWindowLock, bool> evaluator = <>c.<>9__197_0;
                if (<>c.<>9__197_0 == null)
                {
                    Func<DevExpress.Xpf.Docking.Platform.FloatingWindowLock, bool> local1 = <>c.<>9__197_0;
                    evaluator = <>c.<>9__197_0 = x => x.IsLocked(DevExpress.Xpf.Docking.Platform.FloatingWindowLock.LockerKey.FloatingBounds);
                }
                if (!this.FloatingWindowLock.Return<DevExpress.Xpf.Docking.Platform.FloatingWindowLock, bool>(evaluator, (<>c.<>9__197_1 ??= () => false)) && (((this.SizeToContent == System.Windows.SizeToContent.WidthAndHeight) || ((oldValue.Width != newValue.Width) && (this.SizeToContent == System.Windows.SizeToContent.Width))) || ((oldValue.Height != newValue.Height) && (this.SizeToContent == System.Windows.SizeToContent.Height))))
                {
                    this.DisableSizeToContent();
                }
            }
        }

        protected virtual void OnFloatStateChanged(DevExpress.Xpf.Docking.FloatState oldState, DevExpress.Xpf.Docking.FloatState newState)
        {
            DockLayoutManager owner = base.Manager ?? (base.ManagerReference.Return<WeakReference, object>((<>c.<>9__198_0 ??= x => x.Target), (<>c.<>9__198_1 ??= () => null)) as DockLayoutManager);
            if (!this.checkFloatStateLocker)
            {
                this._PreviousFloatState = oldState;
            }
            if (newState == DevExpress.Xpf.Docking.FloatState.Maximized)
            {
                this.DisableSizeToContent();
            }
            base.SetValue(IsMaximizedPropertyKey, newState == DevExpress.Xpf.Docking.FloatState.Maximized);
            this.UpdateSingleItemState();
            if (!this.FloatStateLockHelper.IsLocked)
            {
                if ((owner != null) && ScreenHelper.IsAttachedToPresentationSource(owner))
                {
                    this.OnFloatStateChangedCore(oldState, newState);
                }
                else
                {
                    this.checkFloatStateLocker.LockOnce();
                }
            }
            if (oldState == DevExpress.Xpf.Docking.FloatState.Minimized)
            {
                base.OnLayoutChanged();
            }
        }

        private static void OnFloatStateChanged(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
        {
            FloatGroup group = dObj as FloatGroup;
            if (group != null)
            {
                group.OnFloatStateChanged((DevExpress.Xpf.Docking.FloatState) e.OldValue, (DevExpress.Xpf.Docking.FloatState) e.NewValue);
            }
            else
            {
                Action<BaseLayoutItem> action = <>c.<>9__28_0;
                if (<>c.<>9__28_0 == null)
                {
                    Action<BaseLayoutItem> local1 = <>c.<>9__28_0;
                    action = <>c.<>9__28_0 = x => x.UpdateFloatState();
                }
                (dObj as BaseLayoutItem).Do<BaseLayoutItem>(action);
            }
        }

        private void OnFloatStateChangedCore(DevExpress.Xpf.Docking.FloatState oldState, DevExpress.Xpf.Docking.FloatState newState)
        {
            DockLayoutManager owner = base.Manager ?? (base.ManagerReference.Return<WeakReference, object>((<>c.<>9__211_0 ??= x => x.Target), (<>c.<>9__211_1 ??= () => null)) as DockLayoutManager);
            if ((owner != null) && ScreenHelper.IsAttachedToPresentationSource(owner))
            {
                base.CoerceValue(BaseLayoutItem.IsMaximizeButtonVisibleProperty);
                base.CoerceValue(BaseLayoutItem.IsMinimizeButtonVisibleProperty);
                base.CoerceValue(BaseLayoutItem.IsRestoreButtonVisibleProperty);
                Rect restoreBounds = DocumentPanel.GetRestoreBounds(this);
                if (oldState == DevExpress.Xpf.Docking.FloatState.Minimized)
                {
                    this.UpdateAutoFloatingSize(restoreBounds.Size);
                    this.FloatLocation = restoreBounds.Location();
                    if (owner.MinimizedItems.Contains(this))
                    {
                        owner.MinimizedItems.Remove(this);
                    }
                }
                Rect rect3 = new Rect();
                Rect rect2 = (restoreBounds == rect3) ? new Rect(this.FloatLocation, base.FloatSize) : restoreBounds;
                switch (newState)
                {
                    case DevExpress.Xpf.Docking.FloatState.Normal:
                        rect3 = new Rect();
                        DocumentPanel.SetRestoreBounds(this, rect3);
                        this.FloatLocation = restoreBounds.Location;
                        this.UpdateAutoFloatingSize(restoreBounds.Size);
                        break;

                    case DevExpress.Xpf.Docking.FloatState.Minimized:
                        DocumentPanel.SetRestoreBounds(this, rect2);
                        owner.MinimizedItems.Add(this);
                        break;

                    case DevExpress.Xpf.Docking.FloatState.Maximized:
                        restoreBounds = this.CalcMaximizedBounds(owner, rect2);
                        DocumentPanel.SetRestoreBounds(this, rect2);
                        this.FloatLocation = restoreBounds.Location;
                        base.FloatSize = restoreBounds.Size;
                        break;

                    default:
                        break;
                }
                base.CoerceValue(ActualVisibilityProperty);
            }
        }

        private void OnFloatStateUnlock()
        {
            if (this.IsMaximized)
            {
                this.OnFloatStateChangedCore(DevExpress.Xpf.Docking.FloatState.Normal, this.FloatState);
            }
        }

        protected override void OnHasNotCollapsedItemsChanged(bool hasVisibleItems)
        {
            base.OnHasNotCollapsedItemsChanged(hasVisibleItems);
            base.CoerceValue(LayoutGroup.HasVisibleItemsProperty);
            this._visualTreeLocker.DoWhenUnlocked(() => base.CoerceValue(ActualVisibilityProperty));
        }

        protected override void OnHasSingleItemChanged(bool hasSingleItem)
        {
            base.CoerceValue(LayoutGroup.HasNotCollapsedItemsProperty);
            base.CoerceValue(LayoutGroup.HasVisibleItemsProperty);
            base.CoerceValue(ActualVisibilityProperty);
            base.CoerceValue(IsMaximizedProperty);
            base.CoerceValue(BaseLayoutItem.AllowSizingProperty);
            base.CoerceValue(BaseLayoutItem.IsMaximizeButtonVisibleProperty);
            this.UpdateWindowTaskbarIcon();
            this.UpdateWindowTaskbarTitle();
            this.UpdateSizeToContent();
            using (this.floatStateLocker.Lock())
            {
                this.UpdateFloatState();
            }
        }

        protected virtual void OnIsMaximizableChanged(bool maximized)
        {
            base.CoerceValue(BaseLayoutItem.IsMaximizeButtonVisibleProperty);
        }

        protected virtual void OnIsMinimizableChanged(bool minimized)
        {
            base.CoerceValue(BaseLayoutItem.IsMinimizeButtonVisibleProperty);
        }

        protected override void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsCollectionChanged(sender, e);
            this._visualTreeLocker.DoWhenUnlocked(() => base.CoerceValue(BorderStyleProperty));
        }

        protected internal override void OnItemVisibilityChanged(BaseLayoutItem item, Visibility visibility)
        {
            base.OnItemVisibilityChanged(item, visibility);
            base.CoerceValue(LayoutGroup.HasNotCollapsedItemsProperty);
            if (base.HasSingleItem)
            {
                base.CoerceValue(ActualVisibilityProperty);
            }
        }

        protected override void OnLayoutChangedCore()
        {
            base.OnLayoutChangedCore();
            base.CoerceValue(CanMaximizeProperty);
            base.CoerceValue(CanMinimizeProperty);
        }

        protected override void OnLoaded()
        {
        }

        internal void OnOwnerCollectionChanged()
        {
            this.OnParentChanged();
            if (base.Manager != null)
            {
                if (base.Manager.FloatGroups.Contains(this))
                {
                    DockLayoutManager.AddLogicalChild(base.Manager, this);
                }
                else if (!base.LogicalTreeLockHelper)
                {
                    DockLayoutManager.RemoveLogicalChild(base.Manager, this);
                }
            }
        }

        private static void OnSizeToContentChanged(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
        {
            FloatGroup group = dObj as FloatGroup;
            if (group != null)
            {
                group.OnSizeToContentChanged((System.Windows.SizeToContent) e.OldValue, (System.Windows.SizeToContent) e.NewValue);
            }
            else
            {
                Action<BaseLayoutItem> action = <>c.<>9__24_0;
                if (<>c.<>9__24_0 == null)
                {
                    Action<BaseLayoutItem> local1 = <>c.<>9__24_0;
                    action = <>c.<>9__24_0 = x => x.UpdateSizeToContent();
                }
                (dObj as BaseLayoutItem).Do<BaseLayoutItem>(action);
            }
        }

        protected virtual void OnSizeToContentChanged(System.Windows.SizeToContent oldValue, System.Windows.SizeToContent newValue)
        {
            if (base.HasSingleItem)
            {
                (base.Items[0] as LayoutPanel).Do<LayoutPanel>(x => SetSizeToContent(x, newValue));
            }
        }

        protected override void OnVisibilityChangedOverride(Visibility visibility)
        {
            base.OnVisibilityChangedOverride(visibility);
            base.CoerceValue(ActualVisibilityProperty);
        }

        private void OnWindowStyleChanged(Style style)
        {
            if (this.Window != null)
            {
                this.Window.Style = style;
            }
        }

        private static void OnWindowTaskbarIconChanged(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
        {
            Action<BaseLayoutItem> action = <>c.<>9__36_0;
            if (<>c.<>9__36_0 == null)
            {
                Action<BaseLayoutItem> local1 = <>c.<>9__36_0;
                action = <>c.<>9__36_0 = x => x.UpdateWindowTaskbarIcon();
            }
            (dObj as BaseLayoutItem).Do<BaseLayoutItem>(action);
        }

        private static void OnWindowTaskbarTitleChanged(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
        {
            Action<BaseLayoutItem> action = <>c.<>9__33_0;
            if (<>c.<>9__33_0 == null)
            {
                Action<BaseLayoutItem> local1 = <>c.<>9__33_0;
                action = <>c.<>9__33_0 = x => x.UpdateWindowTaskbarTitle();
            }
            (dObj as BaseLayoutItem).Do<BaseLayoutItem>(action);
        }

        internal override void PrepareForModification(bool isDeserializing)
        {
            base.PrepareForModification(isDeserializing);
            if (isDeserializing)
            {
                this.LockInheritableProperties();
                base.Manager = null;
                base.Dispatcher.BeginInvoke(new Action(this.UnlockInheritableProperties), DispatcherPriority.Loaded, new object[0]);
            }
        }

        internal void RaiseWindowLoaded()
        {
            if (this.WindowLoaded == null)
            {
                EventHandler windowLoaded = this.WindowLoaded;
            }
            else
            {
                this.WindowLoaded(this, EventArgs.Empty);
            }
        }

        internal bool RequestFloatingBoundsUpdate()
        {
            FloatingWindowPresenter element = base.UIElements.GetElement<FloatingWindowPresenter>();
            bool flag = (element != null) && (element.Window != null);
            if (flag)
            {
                element.Window.UpdateFloatGroupBounds();
            }
            return flag;
        }

        protected internal void ResetMaximized()
        {
            if ((base.Items.Count == 1) && (base.Items[0] is DocumentPanel))
            {
                Rect rect = new Rect();
                DocumentPanel.SetRestoreBounds(this, rect);
                MDIStateHelper.SetMDIState(base.Items[0], MDIState.Normal);
                base.CoerceValue(IsMaximizedProperty);
            }
        }

        protected internal void ResetMaximized(Point location)
        {
            if (!this.IsMaximized)
            {
                this.ResetMaximized();
            }
            else
            {
                Rect restoreBounds = DocumentPanel.GetRestoreBounds(this);
                DocumentPanel.SetRestoreBounds(this, new Rect(new Point(location.X - 15.0, location.Y - 15.0), MathHelper.IsEmpty(restoreBounds.Size()) ? base.FloatSize : restoreBounds.Size()));
                if ((base.Items.Count == 1) && (base.Items[0] is DocumentPanel))
                {
                    MDIStateHelper.SetMDIState(base.Items[0], MDIState.Normal);
                }
                Func<FloatingPaneWindow, IDisposable> evaluator = <>c.<>9__163_0;
                if (<>c.<>9__163_0 == null)
                {
                    Func<FloatingPaneWindow, IDisposable> local1 = <>c.<>9__163_0;
                    evaluator = <>c.<>9__163_0 = x => x.LockHelper.Lock(DevExpress.Xpf.Docking.Platform.FloatingWindowLock.LockerKey.ResetMaximized);
                }
                using (this.Window.With<FloatingPaneWindow, IDisposable>(evaluator))
                {
                    this.SetFloatState(DevExpress.Xpf.Docking.FloatState.Normal);
                }
                base.CoerceValue(IsMaximizedProperty);
                this.Update();
            }
        }

        internal override void SetFloatState(DevExpress.Xpf.Docking.FloatState state)
        {
            if (this.FloatState != state)
            {
                this.FloatState = (state != DevExpress.Xpf.Docking.FloatState.Normal) ? state : ((this._PreviousFloatState == DevExpress.Xpf.Docking.FloatState.Maximized) ? DevExpress.Xpf.Docking.FloatState.Maximized : DevExpress.Xpf.Docking.FloatState.Normal);
            }
        }

        public static void SetFloatState(DependencyObject target, DevExpress.Xpf.Docking.FloatState value)
        {
            target.SetValue(FloatStateProperty, value);
        }

        private void SetSerializableIsMaximized()
        {
            DockLayoutManager manager = base.Manager ?? (base.ManagerReference.Return<WeakReference, object>((<>c.<>9__213_0 ??= x => x.Target), (<>c.<>9__213_1 ??= () => null)) as DockLayoutManager);
            if ((manager != null) && !manager.IsDisposing)
            {
                if (this._SerializableIsMaximized)
                {
                    manager.MDIController.Maximize(this);
                }
                else
                {
                    manager.MDIController.Restore(this);
                }
            }
        }

        public static void SetSizeToContent(DependencyObject target, System.Windows.SizeToContent value)
        {
            target.SetValue(SizeToContentProperty, value);
        }

        public static void SetWindowTaskbarIcon(DependencyObject dObj, ImageSource value)
        {
            dObj.SetValue(WindowTaskbarIconProperty, value);
        }

        public static void SetWindowTaskbarTitle(DependencyObject dObj, string value)
        {
            dObj.SetValue(WindowTaskbarTitleProperty, value);
        }

        internal bool TryGetActualAutoSize(out Size autoSize)
        {
            autoSize = new Size();
            FloatPanePresenter uIElement = this.GetUIElement<FloatPanePresenter>();
            return ((this.SizeToContent != System.Windows.SizeToContent.Manual) && ((this.FloatState == DevExpress.Xpf.Docking.FloatState.Normal) && ((uIElement != null) && uIElement.TryGetActualRenderSize(out autoSize))));
        }

        internal void UpdateAutoFloatingSize(Size floatSize)
        {
            if (base.FloatSize != floatSize)
            {
                using (this.AutoSizeLocker.Lock())
                {
                    base.FloatSize = floatSize;
                }
            }
        }

        protected internal override void UpdateFloatState()
        {
            if (base.HasSingleItem && ((GetFloatState(base.Items[0]) != DevExpress.Xpf.Docking.FloatState.Normal) || !this.floatStateLocker))
            {
                this.FloatState = GetFloatState(base.Items[0]);
            }
            else
            {
                base.CoerceValue(FloatStateProperty);
            }
        }

        protected internal void UpdateMaximizedBounds()
        {
            if (this.IsMaximized)
            {
                Rect rect = this.CalcMaximizedBounds(DocumentPanel.GetRestoreBounds(this));
                this.FloatLocation = new Point(rect.X, rect.Y);
                base.FloatSize = new Size(rect.Width, rect.Height);
            }
        }

        private void UpdateSingleItemState()
        {
            if (base.IsDeserializing)
            {
                base.UpdateHasSingleItemProperty();
            }
            if (base.HasSingleItem)
            {
                base.Items[0].SetFloatState(this.FloatState);
                (base.Items[0] as LayoutPanel).Do<LayoutPanel>(x => x.SetCurrentValue(FloatStateProperty, this.FloatState));
            }
        }

        protected internal override void UpdateSizeToContent()
        {
            using (this.sizeToContentLocker.Lock())
            {
                base.CoerceValue(SizeToContentProperty);
            }
        }

        internal override void UpdateWindowTaskbarIcon()
        {
            BaseLayoutItem actualLayoutItem = this.GetActualLayoutItem();
            this.ActualWindowTaskbarIcon = actualLayoutItem.IsPropertySet(WindowTaskbarIconProperty) ? GetWindowTaskbarIcon(actualLayoutItem) : actualLayoutItem.CaptionImage;
        }

        internal override void UpdateWindowTaskbarTitle()
        {
            BaseLayoutItem actualLayoutItem = this.GetActualLayoutItem();
            this.ActualWindowTaskbarTitle = actualLayoutItem.IsPropertySet(WindowTaskbarTitleProperty) ? GetWindowTaskbarTitle(actualLayoutItem) : (actualLayoutItem.ActualCaption ?? string.Empty);
        }

        public Visibility ActualVisibility =>
            (Visibility) base.GetValue(ActualVisibilityProperty);

        [Description("Gets the FloatGroup's border style.This is a dependency property."), Category("Content")]
        public FloatGroupBorderStyle BorderStyle
        {
            get => 
                (FloatGroupBorderStyle) base.GetValue(BorderStyleProperty);
            internal set => 
                base.SetValue(BorderStylePropertyKey, value);
        }

        [Description("Gets or sets the location of the FloatGroup object, relative to the top left corner of the DockLayoutManager container.This is a dependency property."), XtraSerializableProperty, Category("Layout")]
        public Point FloatLocation
        {
            get => 
                (Point) base.GetValue(FloatLocationProperty);
            set => 
                base.SetValue(FloatLocationProperty, value);
        }

        public DevExpress.Xpf.Docking.FloatState FloatState
        {
            get => 
                (DevExpress.Xpf.Docking.FloatState) base.GetValue(FloatStateProperty);
            set => 
                base.SetValue(FloatStateProperty, value);
        }

        [Description("Gets whether the FloatGroup displays a panel that is currently maximized to the window's size.This is a dependency property.")]
        public bool IsMaximized
        {
            get => 
                (bool) base.GetValue(IsMaximizedProperty);
            private set => 
                base.SetValue(IsMaximizedPropertyKey, value);
        }

        [Description("Gets or sets whether the current FloatGroup object is visible.This is a dependency property."), XtraSerializableProperty]
        public bool IsOpen
        {
            get => 
                (bool) base.GetValue(IsOpenProperty);
            set => 
                base.SetValue(IsOpenProperty, value);
        }

        [XtraSerializableProperty, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool SerializableIsMaximized
        {
            get => 
                this.IsMaximized;
            set
            {
                this._SerializableIsMaximized = value;
                Action<DispatcherOperation> action1 = <>c.<>9__72_0;
                if (<>c.<>9__72_0 == null)
                {
                    Action<DispatcherOperation> local1 = <>c.<>9__72_0;
                    action1 = <>c.<>9__72_0 = x => x.Abort();
                }
                this._SerializableIsMaximizedOperation.Do<DispatcherOperation>(action1);
                Action method = new Action(this.SetSerializableIsMaximized);
                if (value)
                {
                    this._SerializableIsMaximizedOperation = base.Dispatcher.BeginInvoke(method, new object[0]);
                }
                else
                {
                    method();
                }
            }
        }

        [XtraSerializableProperty, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public int SerializableZOrder
        {
            get
            {
                FloatPanePresenter element = base.UIElements.GetElement<FloatPanePresenter>();
                IView view = base.Manager.GetView(element);
                return ((view != null) ? view.ZOrder : 0);
            }
            set => 
                this._SerizalizableZOrder = value;
        }

        [XtraSerializableProperty, Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ShouldRestoreOnActivate { get; set; }

        public System.Windows.SizeToContent SizeToContent
        {
            get => 
                (System.Windows.SizeToContent) base.GetValue(SizeToContentProperty);
            set => 
                base.SetValue(SizeToContentProperty, value);
        }

        [Description("")]
        public Style WindowStyle
        {
            get => 
                (Style) base.GetValue(WindowStyleProperty);
            set => 
                base.SetValue(WindowStyleProperty, value);
        }

        public ImageSource WindowTaskbarIcon
        {
            get => 
                (ImageSource) base.GetValue(WindowTaskbarIconProperty);
            set => 
                base.SetValue(WindowTaskbarIconProperty, value);
        }

        public string WindowTaskbarTitle
        {
            get => 
                (string) base.GetValue(WindowTaskbarTitleProperty);
            set => 
                base.SetValue(WindowTaskbarTitleProperty, value);
        }

        internal ImageSource ActualWindowTaskbarIcon
        {
            get => 
                (ImageSource) base.GetValue(ActualWindowTaskbarIconProperty);
            set => 
                base.SetValue(ActualWindowTaskbarIconProperty, value);
        }

        internal string ActualWindowTaskbarTitle
        {
            get => 
                (string) base.GetValue(ActualWindowTaskbarTitleProperty);
            set => 
                base.SetValue(ActualWindowTaskbarTitleProperty, value);
        }

        internal Locker AutoSizeLocker
        {
            get
            {
                Locker locker2 = this._AutoSizeLocker;
                if (this._AutoSizeLocker == null)
                {
                    Locker local1 = this._AutoSizeLocker;
                    locker2 = this._AutoSizeLocker = new Locker();
                }
                return locker2;
            }
        }

        internal bool CanMaximize =>
            (bool) base.GetValue(CanMaximizeProperty);

        internal bool CanMinimize =>
            (bool) base.GetValue(CanMinimizeProperty);

        internal Rect FloatBounds =>
            new Rect(this.FloatLocation, base.FloatSize);

        internal DevExpress.Xpf.Docking.Platform.FloatingWindowLock FloatingWindowLock
        {
            get
            {
                Func<FloatingWindowPresenter, FloatingPaneWindow> evaluator = <>c.<>9__107_0;
                if (<>c.<>9__107_0 == null)
                {
                    Func<FloatingWindowPresenter, FloatingPaneWindow> local1 = <>c.<>9__107_0;
                    evaluator = <>c.<>9__107_0 = x => x.Window;
                }
                Func<FloatingPaneWindow, DevExpress.Xpf.Docking.Platform.FloatingWindowLock> func2 = <>c.<>9__107_1;
                if (<>c.<>9__107_1 == null)
                {
                    Func<FloatingPaneWindow, DevExpress.Xpf.Docking.Platform.FloatingWindowLock> local2 = <>c.<>9__107_1;
                    func2 = <>c.<>9__107_1 = x => x.LockHelper;
                }
                return base.UIElements.GetElement<FloatingWindowPresenter>().With<FloatingWindowPresenter, FloatingPaneWindow>(evaluator).With<FloatingPaneWindow, DevExpress.Xpf.Docking.Platform.FloatingWindowLock>(func2);
            }
        }

        internal Point FloatOffsetBeforeClose
        {
            get => 
                this.floatOffsetBeforeClose;
            set
            {
                if (this.floatOffsetBeforeClose != value)
                {
                    this.floatOffsetBeforeClose = value;
                    this.HasFloatOffsetBeforeClose = true;
                }
            }
        }

        internal LockHelper FloatStateLockHelper
        {
            get
            {
                LockHelper helper2 = this._FloatStateLockHelper;
                if (this._FloatStateLockHelper == null)
                {
                    LockHelper local1 = this._FloatStateLockHelper;
                    helper2 = this._FloatStateLockHelper = new LockHelper(new LockHelper.LockHelperDelegate(this.OnFloatStateUnlock));
                }
                return helper2;
            }
        }

        internal bool HasFloatOffsetBeforeClose { get; private set; }

        internal bool IsActuallyVisible =>
            (bool) base.GetValue(IsActuallyVisibleProperty);

        internal bool IsDocumentHost
        {
            get
            {
                Func<BaseLayoutItem, bool> predicate = <>c.<>9__120_0;
                if (<>c.<>9__120_0 == null)
                {
                    Func<BaseLayoutItem, bool> local1 = <>c.<>9__120_0;
                    predicate = <>c.<>9__120_0 = x => x is DocumentPanel;
                }
                return (this.GetNestedItems().Count<BaseLayoutItem>(predicate) > 0);
            }
        }

        internal bool IsMinimized =>
            this.FloatState == DevExpress.Xpf.Docking.FloatState.Minimized;

        internal Point? ScreenLocationBeforeClose { get; set; }

        internal FrameworkElement UIElement
        {
            get
            {
                Func<FloatPanePresenter, FrameworkElement> evaluator = <>c.<>9__128_0;
                if (<>c.<>9__128_0 == null)
                {
                    Func<FloatPanePresenter, FrameworkElement> local1 = <>c.<>9__128_0;
                    evaluator = <>c.<>9__128_0 = x => x.Element as FrameworkElement;
                }
                return base.UIElements.GetElement<FloatPanePresenter>().With<FloatPanePresenter, FrameworkElement>(evaluator);
            }
        }

        internal FloatingPaneWindow Window
        {
            get
            {
                Func<FloatingWindowPresenter, FloatingPaneWindow> evaluator = <>c.<>9__130_0;
                if (<>c.<>9__130_0 == null)
                {
                    Func<FloatingWindowPresenter, FloatingPaneWindow> local1 = <>c.<>9__130_0;
                    evaluator = <>c.<>9__130_0 = x => x.Window;
                }
                return base.UIElements.GetElement<FloatingWindowPresenter>().With<FloatingWindowPresenter, FloatingPaneWindow>(evaluator);
            }
        }

        protected internal override bool IsMaximizable =>
            true;

        protected internal override bool IsMinimizable =>
            true;

        protected override bool IsEnabledCore
        {
            get
            {
                if (!base.IsEnabledCore)
                {
                    return false;
                }
                Func<DockLayoutManager, bool> evaluator = <>c.<>9__136_0;
                if (<>c.<>9__136_0 == null)
                {
                    Func<DockLayoutManager, bool> local1 = <>c.<>9__136_0;
                    evaluator = <>c.<>9__136_0 = x => x.IsEnabled;
                }
                return base.Manager.Return<DockLayoutManager, bool>(evaluator, (<>c.<>9__136_1 ??= () => true));
            }
        }

        private DevExpress.Xpf.Docking.FloatGroupSerializationInfo FloatGroupSerializationInfo =>
            base.SerializationInfo as DevExpress.Xpf.Docking.FloatGroupSerializationInfo;

        private bool HasSingleDocument =>
            base.HasSingleItem && (base.Items[0] is DocumentPanel);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FloatGroup.<>c <>9 = new FloatGroup.<>c();
            public static Action<BaseLayoutItem> <>9__24_0;
            public static Action<BaseLayoutItem> <>9__28_0;
            public static Action<BaseLayoutItem> <>9__33_0;
            public static Action<BaseLayoutItem> <>9__36_0;
            public static Action<DispatcherOperation> <>9__72_0;
            public static Func<FloatingWindowPresenter, FloatingPaneWindow> <>9__107_0;
            public static Func<FloatingPaneWindow, FloatingWindowLock> <>9__107_1;
            public static Func<BaseLayoutItem, bool> <>9__120_0;
            public static Func<FloatPanePresenter, FrameworkElement> <>9__128_0;
            public static Func<FloatingWindowPresenter, FloatingPaneWindow> <>9__130_0;
            public static Func<DockLayoutManager, bool> <>9__136_0;
            public static Func<bool> <>9__136_1;
            public static Func<FloatingPaneWindow, IDisposable> <>9__163_0;
            public static Func<WeakReference, object> <>9__170_0;
            public static Func<object> <>9__170_1;
            public static Func<DockLayoutManager, bool> <>9__181_0;
            public static Func<bool> <>9__181_1;
            public static Func<FloatingWindowLock, bool> <>9__197_0;
            public static Func<bool> <>9__197_1;
            public static Func<WeakReference, object> <>9__198_0;
            public static Func<object> <>9__198_1;
            public static Func<WeakReference, object> <>9__211_0;
            public static Func<object> <>9__211_1;
            public static Func<WeakReference, object> <>9__213_0;
            public static Func<object> <>9__213_1;

            internal void <.cctor>b__20_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((FloatGroup) dObj).OnBorderStyleChanged();
            }

            internal object <.cctor>b__20_1(DependencyObject dObj, object value) => 
                ((FloatGroup) dObj).CoerceBorderStyle((FloatGroupBorderStyle) value);

            internal object <.cctor>b__20_2(DependencyObject dObj, object value) => 
                ((FloatGroup) dObj).CoerceIsMaximized((bool) value);

            internal void <.cctor>b__20_3(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((FloatGroup) dObj).OnActualVisibilityChanged((Visibility) e.OldValue, (Visibility) e.NewValue);
            }

            internal object <.cctor>b__20_4(DependencyObject dObj, object value) => 
                ((FloatGroup) dObj).CoerceActualVisibility((Visibility) value);

            internal object <.cctor>b__20_5(DependencyObject dObj, object value) => 
                ((FloatGroup) dObj).CoerceCanMaximize((bool) value);

            internal object <.cctor>b__20_6(DependencyObject dObj, object value) => 
                ((FloatGroup) dObj).CoerceCanMinimize((bool) value);

            internal void <.cctor>b__20_7(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((FloatGroup) dObj).OnWindowStyleChanged((Style) e.NewValue);
            }

            internal void <.cctor>b__20_8(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                FloatGroup.OnWindowTaskbarIconChanged(d, e);
            }

            internal void <.cctor>b__20_9(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                FloatGroup.OnWindowTaskbarTitleChanged(d, e);
            }

            internal object <CoerceActualVisibility>b__170_0(WeakReference x) => 
                x.Target;

            internal object <CoerceActualVisibility>b__170_1() => 
                null;

            internal bool <CoerceIsOpen>b__181_0(DockLayoutManager x) => 
                x.EnableWin32Compatibility;

            internal bool <CoerceIsOpen>b__181_1() => 
                false;

            internal FloatingPaneWindow <get_FloatingWindowLock>b__107_0(FloatingWindowPresenter x) => 
                x.Window;

            internal FloatingWindowLock <get_FloatingWindowLock>b__107_1(FloatingPaneWindow x) => 
                x.LockHelper;

            internal bool <get_IsDocumentHost>b__120_0(BaseLayoutItem x) => 
                x is DocumentPanel;

            internal bool <get_IsEnabledCore>b__136_0(DockLayoutManager x) => 
                x.IsEnabled;

            internal bool <get_IsEnabledCore>b__136_1() => 
                true;

            internal FrameworkElement <get_UIElement>b__128_0(FloatPanePresenter x) => 
                x.Element as FrameworkElement;

            internal FloatingPaneWindow <get_Window>b__130_0(FloatingWindowPresenter x) => 
                x.Window;

            internal bool <OnFloatSizeChanged>b__197_0(FloatingWindowLock x) => 
                x.IsLocked(FloatingWindowLock.LockerKey.FloatingBounds);

            internal bool <OnFloatSizeChanged>b__197_1() => 
                false;

            internal object <OnFloatStateChanged>b__198_0(WeakReference x) => 
                x.Target;

            internal object <OnFloatStateChanged>b__198_1() => 
                null;

            internal void <OnFloatStateChanged>b__28_0(BaseLayoutItem x)
            {
                x.UpdateFloatState();
            }

            internal object <OnFloatStateChangedCore>b__211_0(WeakReference x) => 
                x.Target;

            internal object <OnFloatStateChangedCore>b__211_1() => 
                null;

            internal void <OnSizeToContentChanged>b__24_0(BaseLayoutItem x)
            {
                x.UpdateSizeToContent();
            }

            internal void <OnWindowTaskbarIconChanged>b__36_0(BaseLayoutItem x)
            {
                x.UpdateWindowTaskbarIcon();
            }

            internal void <OnWindowTaskbarTitleChanged>b__33_0(BaseLayoutItem x)
            {
                x.UpdateWindowTaskbarTitle();
            }

            internal IDisposable <ResetMaximized>b__163_0(FloatingPaneWindow x) => 
                x.LockHelper.Lock(FloatingWindowLock.LockerKey.ResetMaximized);

            internal void <set_SerializableIsMaximized>b__72_0(DispatcherOperation x)
            {
                x.Abort();
            }

            internal object <SetSerializableIsMaximized>b__213_0(WeakReference x) => 
                x.Target;

            internal object <SetSerializableIsMaximized>b__213_1() => 
                null;
        }
    }
}

