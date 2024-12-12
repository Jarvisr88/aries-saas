namespace DevExpress.Xpf.Docking
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking.UIAutomation;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Input;

    public class DocumentPanel : LayoutPanel
    {
        public static readonly DependencyProperty MDILocationProperty;
        public static readonly DependencyProperty MDISizeProperty;
        public static readonly DependencyProperty MDIStateProperty;
        public static readonly DependencyProperty MDIMergeStyleProperty;
        public static readonly DependencyProperty RestoreBoundsProperty;
        public static readonly DependencyProperty EnableMouseHoverWhenInactiveProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty CanMergeProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty IsMergingTargetProperty;
        internal static Size DefaultMDISize = new Size(double.NaN, double.NaN);
        private int canMergeCount;
        private bool isMDIChildCore;
        private LockHelper mdiStateLockHelper;
        private readonly Locker canMergeLocker;

        static DocumentPanel()
        {
            DependencyPropertyRegistrator<DocumentPanel> registrator = new DependencyPropertyRegistrator<DocumentPanel>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.OverrideMetadata<bool>(BaseLayoutItem.AllowHideProperty, false, null, new CoerceValueCallback(DocumentPanel.CoerceAllowHide));
            registrator.RegisterAttached<Point>("MDILocation", ref MDILocationProperty, new Point(0.0, 0.0), new PropertyChangedCallback(DocumentPanel.OnMDILocationChanged), new CoerceValueCallback(DocumentPanel.CoerceMDILocation));
            registrator.RegisterAttached<Size>("MDISize", ref MDISizeProperty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, DefaultMDISize, new PropertyChangedCallback(DocumentPanel.OnMDISizeChanged), new CoerceValueCallback(DocumentPanel.CoerceMDISize));
            registrator.RegisterAttached<DevExpress.Xpf.Docking.MDIState>("MDIState", ref MDIStateProperty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, DevExpress.Xpf.Docking.MDIState.Normal, new PropertyChangedCallback(DocumentPanel.OnMDIStateChanged), null);
            Rect defValue = new Rect();
            registrator.RegisterAttached<Rect>("RestoreBounds", ref RestoreBoundsProperty, defValue, null, null);
            registrator.RegisterAttached<DevExpress.Xpf.Bars.MDIMergeStyle>("MDIMergeStyle", ref MDIMergeStyleProperty, DevExpress.Xpf.Bars.MDIMergeStyle.Default, new PropertyChangedCallback(DocumentPanel.OnMDIMergeStyleChanged), null);
            registrator.Register<bool>("EnableMouseHoverWhenInactive", ref EnableMouseHoverWhenInactiveProperty, false, null, null);
            registrator.Register<bool>("CanMerge", ref CanMergeProperty, false, (d, e) => ((DocumentPanel) d).OnCanMergeChanged((bool) e.NewValue), (d, value) => ((DocumentPanel) d).CoerceCanMerge((bool) value));
            registrator.Register<bool>("IsMergingTarget", ref IsMergingTargetProperty, false, null, null);
        }

        public DocumentPanel()
        {
            this.ActivateCommand = DelegateCommandFactory.Create<object>(o => base.IsActive = true, false);
            this.canMergeLocker = new Locker();
            this.canMergeLocker.Unlocked += new EventHandler(this.OnUnlockMergingTarget);
        }

        protected override void ActivateItemCore()
        {
            if (!base.Manager.IsDisposing && this.IsMDIChild)
            {
                base.Manager.MDIController.Activate(this, false);
            }
            base.ActivateItemCore();
        }

        private void ApplyMDIState(DevExpress.Xpf.Docking.MDIState state)
        {
            DockLayoutManager manager = this.FindDockLayoutManager();
            if ((manager == null) || manager.IsDisposing)
            {
                MDIStateHelper.SetMDIState(this, state);
            }
            else
            {
                DevExpress.Xpf.Docking.MDIState mDIState = MDIStateHelper.GetMDIState(this);
                switch (state)
                {
                    case DevExpress.Xpf.Docking.MDIState.Normal:
                        if (mDIState == DevExpress.Xpf.Docking.MDIState.Normal)
                        {
                            break;
                        }
                        manager.MDIController.Restore(this);
                        return;

                    case DevExpress.Xpf.Docking.MDIState.Minimized:
                        if (mDIState == DevExpress.Xpf.Docking.MDIState.Minimized)
                        {
                            break;
                        }
                        manager.MDIController.Minimize(this);
                        return;

                    case DevExpress.Xpf.Docking.MDIState.Maximized:
                        if (mDIState != DevExpress.Xpf.Docking.MDIState.Maximized)
                        {
                            manager.MDIController.Maximize(this);
                        }
                        break;

                    default:
                        return;
                }
            }
        }

        private void CheckIsMergingTarget()
        {
            DockLayoutManager dockLayoutManager = base.Manager;
            if (dockLayoutManager == null)
            {
                LayoutGroup item = base.Parent ?? (LogicalTreeHelper.GetParent(this) as LayoutGroup);
                if (item != null)
                {
                    dockLayoutManager = item.GetRoot().GetDockLayoutManager();
                }
            }
            if (dockLayoutManager != null)
            {
                dockLayoutManager.CheckMergingTarget(this);
            }
        }

        protected override Size CoerceActualMinSize() => 
            base.IsMinimized ? new Size(0.0, 0.0) : base.CoerceActualMinSize();

        protected virtual object CoerceAllowHide(object baseValue) => 
            false;

        private static object CoerceAllowHide(DependencyObject d, object baseValue) => 
            ((DocumentPanel) d).CoerceAllowHide(baseValue);

        private void CoerceCanMerge()
        {
            if (this.canMergeLocker)
            {
                this.canMergeCount++;
            }
            else
            {
                this.canMergeCount = 0;
                base.CoerceValue(CanMergeProperty);
            }
        }

        protected virtual object CoerceCanMerge(bool value) => 
            !base.IsFloating ? (this.IsMDIChild ? (!base.IsSelectedItem ? ((object) 0) : ((object) base.IsMaximized)) : base.IsSelectedItem) : false;

        protected override bool CoerceIsCloseButtonVisible(bool value)
        {
            value = base.CoerceIsCloseButtonVisible(value);
            DocumentGroup parent = base.Parent as DocumentGroup;
            if ((parent != null) && base.IsTabPage)
            {
                switch (parent.ClosePageButtonShowMode)
                {
                    case ClosePageButtonShowMode.Default:
                    case ClosePageButtonShowMode.InTabControlHeader:
                    case ClosePageButtonShowMode.NoWhere:
                        value = false;
                        break;

                    case ClosePageButtonShowMode.InActiveTabPageHeader:
                    case ClosePageButtonShowMode.InActiveTabPageAndTabControlHeader:
                        value &= ReferenceEquals(parent.SelectedItem, this);
                        break;

                    default:
                        break;
                }
            }
            return value;
        }

        protected override bool CoerceIsMinimizeButtonVisible(bool visible) => 
            this.IsMinimizable && (base.AllowMinimize && (base.ShowMinimizeButton && !base.IsMinimized));

        protected override bool CoerceIsTabPage(bool value)
        {
            DocumentGroup parent = base.Parent as DocumentGroup;
            return ((parent != null) && parent.IsTabbed);
        }

        private static object CoerceMDILocation(DependencyObject dObj, object value)
        {
            Point point = (Point) value;
            return new Point(Math.Max(0.0, point.X), Math.Max(0.0, point.Y));
        }

        protected virtual object CoerceMDISize(Size size) => 
            MathHelper.ValidateSize(base.ActualMinSize, base.ActualMaxSize, size);

        private static object CoerceMDISize(DependencyObject dObj, object baseValue) => 
            (dObj is DocumentPanel) ? ((DocumentPanel) dObj).CoerceMDISize((Size) baseValue) : baseValue;

        private void EnsureMDIState()
        {
            base.ParentLockHelper.DoWhenUnlocked(delegate {
                this.ApplyMDIState(this.MDIState);
                base.UpdateMinMaxState();
                this.UpdateButtons();
            });
        }

        internal override bool GetIsMaximized() => 
            this.IsMDIChild ? (this.MDIState == DevExpress.Xpf.Docking.MDIState.Maximized) : base.GetIsMaximized();

        internal override bool GetIsMinimized() => 
            this.IsMDIChild ? (this.MDIState == DevExpress.Xpf.Docking.MDIState.Minimized) : base.GetIsMinimized();

        protected override LayoutItemType GetLayoutItemTypeCore() => 
            LayoutItemType.Document;

        [XtraSerializableProperty]
        public static Point GetMDILocation(DependencyObject dObj) => 
            (Point) dObj.GetValue(MDILocationProperty);

        [XtraSerializableProperty]
        public static DevExpress.Xpf.Bars.MDIMergeStyle GetMDIMergeStyle(DependencyObject target) => 
            (DevExpress.Xpf.Bars.MDIMergeStyle) target.GetValue(MDIMergeStyleProperty);

        [XtraSerializableProperty]
        public static Size GetMDISize(DependencyObject dObj) => 
            (Size) dObj.GetValue(MDISizeProperty);

        [XtraSerializableProperty]
        public static DevExpress.Xpf.Docking.MDIState GetMDIState(DependencyObject dObj) => 
            (DevExpress.Xpf.Docking.MDIState) dObj.GetValue(MDIStateProperty);

        [XtraSerializableProperty]
        public static Rect GetRestoreBounds(DependencyObject dObj) => 
            (Rect) dObj.GetValue(RestoreBoundsProperty);

        internal override IDisposable LockCanMerge() => 
            this.canMergeLocker.Lock();

        protected override void OnActualCaptionChanged(string value)
        {
            base.OnActualCaptionChanged(value);
            if (this.IsMDIChild && (base.IsMaximized && base.IsSelectedItem))
            {
                MDIControllerHelper.MergeMDITitles(this);
            }
        }

        protected override void OnActualMaxSizeChanged()
        {
            base.OnActualMaxSizeChanged();
            base.CoerceValue(MDISizeProperty);
        }

        protected override void OnActualMinSizeChanged()
        {
            base.OnActualMinSizeChanged();
            base.CoerceValue(MDISizeProperty);
        }

        protected virtual void OnCanMergeChanged(bool newValue)
        {
            if (!this.canMergeLocker)
            {
                this.CheckIsMergingTarget();
            }
        }

        protected override double OnCoerceMinHeight(double value) => 
            base.IsMinimized ? 0.0 : base.OnCoerceMinHeight(value);

        protected override double OnCoerceMinWidth(double value) => 
            base.IsMinimized ? 0.0 : base.OnCoerceMinWidth(value);

        internal override void OnControlGotFocus()
        {
            base.OnControlGotFocus();
            this.CoerceCanMerge();
        }

        protected override AutomationPeer OnCreateAutomationPeer() => 
            !this.IsMDIChild ? base.OnCreateAutomationPeer() : new MDIDocumentAutomationPeer(this);

        protected override void OnDockItemStateChanged(DockItemState oldState, DockItemState newState)
        {
            base.OnDockItemStateChanged(oldState, newState);
            this.CoerceCanMerge();
        }

        protected override void OnDockLayoutManagerChanged(DockLayoutManager oldValue, DockLayoutManager newValue)
        {
            base.OnDockLayoutManagerChanged(oldValue, newValue);
            this.CheckIsMergingTarget();
        }

        protected override void OnIsActiveChangedCore()
        {
            base.OnIsActiveChangedCore();
            if (base.IsActive && (base.Parent != null))
            {
                this.CheckIsMergingTarget();
                DocumentGroup parent = base.Parent as DocumentGroup;
                if ((parent != null) && !parent.IsTabbed)
                {
                    BaseLayoutItem[] items = parent.GetItems();
                    if (items.Length != 0)
                    {
                        LayoutItemsHelper.UpdateZIndexes(items, this);
                        if (base.Manager != null)
                        {
                            IUIElement element = parent;
                            element.Children.MakeLast(this.GetUIElement<IUIElement>());
                            IView view = base.Manager.GetView(element.GetRootUIScope());
                            if (view != null)
                            {
                                view.Invalidate();
                            }
                        }
                    }
                }
            }
        }

        protected override void OnIsMaximizedChanged(bool maximized)
        {
            base.OnIsMaximizedChanged(maximized);
            this.CoerceCanMerge();
        }

        protected override void OnIsMinimizedChanged(bool minimized)
        {
            base.OnIsMinimizedChanged(base.IsMinimized);
            base.CoerceValue(FrameworkElement.MinWidthProperty);
            base.CoerceValue(FrameworkElement.MinHeightProperty);
            base.CoerceValue(BaseLayoutItem.ActualMinSizeProperty);
        }

        protected override void OnIsSelectedItemChanged()
        {
            base.OnIsSelectedItemChanged();
            this.CoerceCanMerge();
        }

        protected override void OnIsTabPageChanged()
        {
            base.OnIsTabPageChanged();
            this.UpdateButtons();
            this.CoerceCanMerge();
        }

        private static void OnMDILocationChanged(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
        {
            MDIStateHelper.InvalidateMDIPanel(dObj);
        }

        private static void OnMDIMergeStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is IMDIMergeStyleListener)
            {
                ((IMDIMergeStyleListener) d).OnMDIMergeStyleChanged((DevExpress.Xpf.Bars.MDIMergeStyle) e.OldValue, (DevExpress.Xpf.Bars.MDIMergeStyle) e.NewValue);
            }
        }

        private static void OnMDISizeChanged(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
        {
            MDIStateHelper.InvalidateMDIPanel(dObj);
        }

        protected virtual void OnMDIStateChanged(DevExpress.Xpf.Docking.MDIState oldValue, DevExpress.Xpf.Docking.MDIState newValue)
        {
            if (base.Parent == null)
            {
                this.MdiStateLockHelper.Lock();
            }
            this.MdiStateLockHelper.DoWhenUnlocked(new LockHelper.LockHelperDelegate(this.EnsureMDIState));
        }

        private static void OnMDIStateChanged(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
        {
            if (dObj is DocumentPanel)
            {
                ((DocumentPanel) dObj).OnMDIStateChanged((DevExpress.Xpf.Docking.MDIState) e.OldValue, (DevExpress.Xpf.Docking.MDIState) e.NewValue);
            }
        }

        protected override void OnParentChanged()
        {
            base.OnParentChanged();
            base.CoerceValue(BaseLayoutItem.IsCloseButtonVisibleProperty);
            if (base.Parent != null)
            {
                this.MdiStateLockHelper.Unlock();
            }
        }

        protected internal override void OnParentLoaded()
        {
            base.OnParentLoaded();
            if (this.MergingClient != null)
            {
                this.MergingClient.QueueMerge();
            }
        }

        protected internal override void OnParentUnloaded()
        {
            if (this.MergingClient != null)
            {
                this.MergingClient.QueueUnmerge();
            }
            base.OnParentUnloaded();
        }

        protected override void OnShowRestoreButtonChanged(bool show)
        {
            base.OnShowRestoreButtonChanged(show);
            if (base.Parent != null)
            {
                base.Parent.CoerceValue(BaseLayoutItem.IsRestoreButtonVisibleProperty);
            }
        }

        private void OnUnlockMergingTarget(object sender, EventArgs e)
        {
            if (this.canMergeCount > 0)
            {
                this.CoerceCanMerge();
            }
        }

        protected internal override void SetAutoHidden(bool autoHidden)
        {
        }

        public static void SetMDILocation(DependencyObject dObj, Point value)
        {
            dObj.SetValue(MDILocationProperty, value);
        }

        public static void SetMDIMergeStyle(DependencyObject target, DevExpress.Xpf.Bars.MDIMergeStyle value)
        {
            target.SetValue(MDIMergeStyleProperty, value);
        }

        public static void SetMDISize(DependencyObject dObj, Size value)
        {
            dObj.SetValue(MDISizeProperty, value);
        }

        public static void SetMDIState(DependencyObject dObj, DevExpress.Xpf.Docking.MDIState value)
        {
            dObj.SetValue(MDIStateProperty, value);
        }

        public static void SetRestoreBounds(DependencyObject dObj, Rect value)
        {
            dObj.SetValue(RestoreBoundsProperty, value);
        }

        protected override void SubscribeAutoHideDisplayModeChanged(DockLayoutManager manager)
        {
        }

        protected override void UnsubscribeAutoHideDisplayModeChanged(DockLayoutManager manager)
        {
        }

        protected internal override void UpdateButtons()
        {
            base.CoerceValue(BaseLayoutItem.IsCloseButtonVisibleProperty);
            base.CoerceValue(BaseLayoutItem.IsMinimizeButtonVisibleProperty);
            base.CoerceValue(BaseLayoutItem.IsMaximizeButtonVisibleProperty);
            base.CoerceValue(BaseLayoutItem.IsRestoreButtonVisibleProperty);
            base.CoerceValue(BaseLayoutItem.IsPinButtonVisibleProperty);
            DocumentGroup parent = base.Parent as DocumentGroup;
            if (parent != null)
            {
                parent.UpdateButtons();
            }
        }

        public ICommand ActivateCommand { get; private set; }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public bool EnableMouseHoverWhenInactive
        {
            get => 
                (bool) base.GetValue(EnableMouseHoverWhenInactiveProperty);
            set => 
                base.SetValue(EnableMouseHoverWhenInactiveProperty, value);
        }

        [XtraSerializableProperty]
        public Point MDILocation
        {
            get => 
                (Point) base.GetValue(MDILocationProperty);
            set => 
                base.SetValue(MDILocationProperty, value);
        }

        public DevExpress.Xpf.Bars.MDIMergeStyle MDIMergeStyle
        {
            get => 
                (DevExpress.Xpf.Bars.MDIMergeStyle) base.GetValue(MDIMergeStyleProperty);
            set => 
                base.SetValue(MDIMergeStyleProperty, value);
        }

        [XtraSerializableProperty]
        public Size MDISize
        {
            get => 
                (Size) base.GetValue(MDISizeProperty);
            set => 
                base.SetValue(MDISizeProperty, value);
        }

        [XtraSerializableProperty]
        public DevExpress.Xpf.Docking.MDIState MDIState
        {
            get => 
                (DevExpress.Xpf.Docking.MDIState) base.GetValue(MDIStateProperty);
            set => 
                base.SetValue(MDIStateProperty, value);
        }

        [XtraSerializableProperty(1), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public Rect SerializableMDIBounds
        {
            get => 
                (this.MDIState == DevExpress.Xpf.Docking.MDIState.Normal) ? new Rect(this.MDILocation, this.MDISize) : GetRestoreBounds(this);
            set
            {
                if (this.MDIState != DevExpress.Xpf.Docking.MDIState.Normal)
                {
                    SetRestoreBounds(this, value);
                }
                else
                {
                    this.MDILocation = value.Location();
                    this.MDISize = value.Size();
                }
            }
        }

        internal bool CanMerge =>
            (bool) base.GetValue(CanMergeProperty);

        internal bool IsMergingTarget
        {
            get => 
                (bool) base.GetValue(IsMergingTargetProperty);
            set => 
                base.SetValue(IsMergingTargetProperty, value);
        }

        internal IMergingClient MergingClient { get; set; }

        internal override bool SupportsFloatOrMDIState =>
            this.IsMDIChild || base.SupportsFloatOrMDIState;

        protected internal bool IsMDIChild
        {
            get
            {
                DocumentGroup parent = base.Parent as DocumentGroup;
                return ((parent != null) ? !parent.IsTabbed : this.isMDIChildCore);
            }
            set => 
                this.isMDIChildCore = value;
        }

        protected internal bool IsMinimizedBeforeMaximize { get; set; }

        protected internal Size MDIDocumentSize { get; set; }

        protected internal Point? MinimizeLocation { get; set; }

        private LockHelper MdiStateLockHelper
        {
            get
            {
                LockHelper mdiStateLockHelper = this.mdiStateLockHelper;
                if (this.mdiStateLockHelper == null)
                {
                    LockHelper local1 = this.mdiStateLockHelper;
                    mdiStateLockHelper = this.mdiStateLockHelper = new LockHelper();
                }
                return mdiStateLockHelper;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DocumentPanel.<>c <>9 = new DocumentPanel.<>c();

            internal void <.cctor>b__9_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DocumentPanel) d).OnCanMergeChanged((bool) e.NewValue);
            }

            internal object <.cctor>b__9_1(DependencyObject d, object value) => 
                ((DocumentPanel) d).CoerceCanMerge((bool) value);
        }
    }
}

