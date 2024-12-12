namespace DevExpress.Xpf.Docking
{
    using DevExpress.Data.Utils;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.ModuleInjection;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking.Base;
    using DevExpress.Xpf.Docking.VisualElements;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Media;
    using System.Windows.Threading;

    public class LayoutPanel : ContentItem, IGeneratorHost, IClosable, ILayoutContent
    {
        public static readonly DependencyProperty HorizontalScrollBarVisibilityProperty;
        public static readonly DependencyProperty VerticalScrollBarVisibilityProperty;
        public static readonly DependencyProperty ShowBorderProperty;
        public static readonly DependencyProperty LayoutProperty;
        private static readonly DependencyPropertyKey LayoutPropertyKey;
        public static readonly DependencyProperty ControlProperty;
        private static readonly DependencyPropertyKey ControlPropertyKey;
        public static readonly DependencyProperty ContentPresenterProperty;
        private static readonly DependencyPropertyKey ContentPresenterPropertyKey;
        public static readonly DependencyProperty HasBorderProperty;
        private static readonly DependencyPropertyKey HasBorderPropertyKey;
        public static readonly DependencyProperty AllowDockToDocumentGroupProperty;
        public static readonly DependencyProperty ActualTabBackgroundColorProperty;
        private static readonly DependencyPropertyKey ActualTabBackgroundColorPropertyKey;
        public static readonly DependencyProperty TabBackgroundColorProperty;
        public static readonly DependencyProperty UriProperty;
        internal static readonly DependencyPropertyKey UriPropertyKey;
        public static readonly DependencyProperty IsMaximizedProperty;
        protected internal static readonly DependencyPropertyKey IsMaximizedPropertyKey;
        public static readonly DependencyProperty IsMinimizedProperty;
        private static readonly DependencyPropertyKey IsMinimizedPropertyKey;
        private static readonly DependencyPropertyKey FloatStatePropertyKey;
        public static readonly DependencyProperty FloatStateProperty;
        public static readonly DependencyProperty AutoHiddenProperty;
        public static readonly DependencyProperty AutoHideExpandStateProperty;
        public static readonly DependencyProperty DockItemStateProperty;
        private static readonly DependencyPropertyKey DockItemStatePropertyKey;
        [IgnoreDependencyPropertiesConsistencyChecker]
        internal static readonly DependencyProperty TabPinLocationProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        internal static readonly DependencyProperty PinnedProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        internal static readonly DependencyProperty ShowPinButtonInTabProperty;
        public static readonly DependencyProperty ShowHideButtonProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty IsHideButtonVisibleProperty;
        private static readonly DependencyPropertyKey IsHideButtonVisiblePropertyKey;
        public static readonly DependencyProperty ShowExpandButtonProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty IsExpandButtonVisibleProperty;
        private static readonly DependencyPropertyKey IsExpandButtonVisiblePropertyKey;
        public static readonly DependencyProperty ShowCollapseButtonProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty IsCollapseButtonVisibleProperty;
        private static readonly DependencyPropertyKey IsCollapseButtonVisiblePropertyKey;
        public static readonly DependencyProperty ShowInDocumentSelectorProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyPropertyKey IsTouchEnabledPropertyKey;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty IsTouchEnabledProperty;
        internal Locker ExpandAnimationLocker = new Locker();
        private readonly LayoutPanelContentPresenter Presenter;
        private AutoHideMode autoHideDisplayMode;
        private AutoHideDisplayModePropertyChangedWeakEventHandler<LayoutPanel> autoHideDisplayModePropertyChangedHandler;
        private DispatcherOperation checkPinStatusOperation;
        private LockHelper expandStateLocker;
        private bool fClearTemplateRequested;
        private Point floatOffsetBeforeClose;
        private bool _isFloatingRootInTabbedGroup;
        private Point _floatLocationBeforeClose;

        static LayoutPanel()
        {
            DockingStrategyRegistrator.RegisterLayoutPanelStrategy();
            DevExpress.Xpf.Docking.DependencyPropertyRegistrator<LayoutPanel> registrator = new DevExpress.Xpf.Docking.DependencyPropertyRegistrator<LayoutPanel>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.OverrideMetadata<bool>(BaseLayoutItem.AllowSelectionProperty, false, null, null);
            registrator.Register<ScrollBarVisibility>("HorizontalScrollBarVisibility", ref HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Auto, null, null);
            registrator.Register<ScrollBarVisibility>("VerticalScrollBarVisibility", ref VerticalScrollBarVisibilityProperty, ScrollBarVisibility.Auto, null, null);
            registrator.RegisterReadonly<LayoutGroup>("Layout", ref LayoutPropertyKey, ref LayoutProperty, null, (dObj, e) => ((LayoutPanel) dObj).OnLayoutChanged((LayoutGroup) e.NewValue, (LayoutGroup) e.OldValue), null);
            registrator.RegisterReadonly<UIElement>("Control", ref ControlPropertyKey, ref ControlProperty, null, (dObj, e) => ((LayoutPanel) dObj).OnControlChanged((UIElement) e.NewValue, (UIElement) e.OldValue), null);
            registrator.RegisterReadonly<UIElement>("ContentPresenter", ref ContentPresenterPropertyKey, ref ContentPresenterProperty, null, null, null);
            registrator.Register<bool>("ShowBorder", ref ShowBorderProperty, true, (dObj, e) => ((LayoutPanel) dObj).OnShowBorderChanged(), null);
            registrator.RegisterReadonly<bool>("HasBorder", ref HasBorderPropertyKey, ref HasBorderProperty, true, null, (CoerceValueCallback) ((dObj, value) => ((LayoutPanel) dObj).CoerceHasBorder((bool) value)));
            registrator.Register<bool>("AllowDockToDocumentGroup", ref AllowDockToDocumentGroupProperty, true, null, null);
            registrator.Register<Color>("TabBackgroundColor", ref TabBackgroundColorProperty, Colors.Transparent, (dObj, e) => ((LayoutPanel) dObj).OnTabBackgroundColorChanged((Color) e.OldValue, (Color) e.NewValue), null);
            registrator.RegisterReadonly<Color>("ActualTabBackgroundColor", ref ActualTabBackgroundColorPropertyKey, ref ActualTabBackgroundColorProperty, Colors.Transparent, null, (CoerceValueCallback) ((dObj, value) => ((LayoutPanel) dObj).CoerceActualTabBackgroundColor((Color) value)));
            registrator.RegisterReadonly<System.Uri>("Uri", ref UriPropertyKey, ref UriProperty, null, null, null);
            registrator.RegisterReadonly<bool>("IsMaximized", ref IsMaximizedPropertyKey, ref IsMaximizedProperty, false, (dObj, e) => ((LayoutPanel) dObj).OnIsMaximizedChanged((bool) e.NewValue), null);
            registrator.RegisterReadonly<bool>("IsMinimized", ref IsMinimizedPropertyKey, ref IsMinimizedProperty, false, (dObj, e) => ((LayoutPanel) dObj).OnIsMinimizedChanged((bool) e.NewValue), null);
            registrator.RegisterReadonly<DevExpress.Xpf.Docking.FloatState>("FloatState", ref FloatStatePropertyKey, ref FloatStateProperty, DevExpress.Xpf.Docking.FloatState.Normal, (dObj, e) => ((LayoutPanel) dObj).OnFloatStateChanged((DevExpress.Xpf.Docking.FloatState) e.OldValue, (DevExpress.Xpf.Docking.FloatState) e.NewValue), null);
            registrator.Register<bool>("AutoHidden", ref AutoHiddenProperty, false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (dObj, e) => ((LayoutPanel) dObj).OnAutoHiddenChanged((bool) e.OldValue, (bool) e.NewValue), null);
            registrator.Register<DevExpress.Xpf.Docking.Base.AutoHideExpandState>("AutoHideExpandState", ref AutoHideExpandStateProperty, DevExpress.Xpf.Docking.Base.AutoHideExpandState.Hidden, (dObj, e) => ((LayoutPanel) dObj).OnAutoHideExpandStateChanged((DevExpress.Xpf.Docking.Base.AutoHideExpandState) e.OldValue, (DevExpress.Xpf.Docking.Base.AutoHideExpandState) e.NewValue), null);
            registrator.Register<bool>("ShowInDocumentSelector", ref ShowInDocumentSelectorProperty, true, null, null);
            registrator.Register<TabHeaderPinLocation>("TabPinLocation", ref TabPinLocationProperty, TabHeaderPinLocation.Default, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (dObj, e) => ((LayoutPanel) dObj).OnTabPinLocationChanged((TabHeaderPinLocation) e.OldValue, (TabHeaderPinLocation) e.NewValue), null);
            registrator.Register<bool>("Pinned", ref PinnedProperty, false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (dObj, e) => ((LayoutPanel) dObj).OnPinnedChanged((bool) e.OldValue, (bool) e.NewValue), null);
            registrator.RegisterReadonly<DevExpress.Xpf.Docking.DockItemState>("DockItemState", ref DockItemStatePropertyKey, ref DockItemStateProperty, DevExpress.Xpf.Docking.DockItemState.Undefined, (dObj, e) => ((LayoutPanel) dObj).OnDockItemStateChanged((DevExpress.Xpf.Docking.DockItemState) e.OldValue, (DevExpress.Xpf.Docking.DockItemState) e.NewValue), (CoerceValueCallback) ((dObj, value) => ((LayoutPanel) dObj).CoerceDockItemState((DevExpress.Xpf.Docking.DockItemState) value)));
            registrator.Register<bool>("ShowHideButton", ref ShowHideButtonProperty, true, (dObj, e) => ((LayoutPanel) dObj).OnShowHideButtonChanged((bool) e.OldValue, (bool) e.NewValue), null);
            registrator.Register<bool>("ShowExpandButton", ref ShowExpandButtonProperty, true, (dObj, e) => ((LayoutPanel) dObj).OnShowExpandButtonChanged((bool) e.OldValue, (bool) e.NewValue), null);
            registrator.Register<bool>("ShowCollapseButton", ref ShowCollapseButtonProperty, true, (dObj, e) => ((LayoutPanel) dObj).OnShowCollapseButtonChanged((bool) e.OldValue, (bool) e.NewValue), null);
            registrator.Register<bool>("ShowPinButtonInTab", ref ShowPinButtonInTabProperty, false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (dObj, e) => ((LayoutPanel) dObj).OnShowPinButtonInTabChanged((bool) e.OldValue, (bool) e.NewValue), null);
            registrator.RegisterReadonly<bool>("IsHideButtonVisible", ref IsHideButtonVisiblePropertyKey, ref IsHideButtonVisibleProperty, false, null, (dObj, value) => ((LayoutPanel) dObj).CoerceIsHideButtonVisible(value));
            registrator.RegisterReadonly<bool>("IsExpandButtonVisible", ref IsExpandButtonVisiblePropertyKey, ref IsExpandButtonVisibleProperty, false, null, (CoerceValueCallback) ((dObj, value) => ((LayoutPanel) dObj).CoerceIsExpandButtonVisible(value)));
            registrator.RegisterReadonly<bool>("IsCollapseButtonVisible", ref IsCollapseButtonVisiblePropertyKey, ref IsCollapseButtonVisibleProperty, false, null, (CoerceValueCallback) ((dObj, value) => ((LayoutPanel) dObj).CoerceIsCollapseButtonVisible(value)));
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(LayoutPanel), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<LayoutPanel>.New().Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<LayoutPanel, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(LayoutPanel.get_IsTouchEnabled)), parameters), out IsTouchEnabledProperty, false, (d, oldValue, newValue) => d.OnIsTouchEnabledChanged(oldValue, newValue), frameworkOptions);
            try
            {
                IsTouchEnabledPropertyKey = typeof(ThemeManager).GetField("IsTouchEnabledPropertyKey", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static).GetValue(null) as DependencyPropertyKey;
            }
            catch
            {
            }
        }

        public LayoutPanel()
        {
            Binding binding = new Binding();
            binding.Path = new PropertyPath(ThemeManager.IsTouchEnabledProperty);
            binding.Source = this;
            BindingOperations.SetBinding(this, IsTouchEnabledProperty, binding);
            Binding binding2 = new Binding();
            binding2.Path = new PropertyPath(DocumentGroup.PinLocationProperty);
            binding2.Source = this;
            binding2.Mode = BindingMode.TwoWay;
            base.SetBinding(TabPinLocationProperty, binding2);
            Binding binding3 = new Binding();
            binding3.Path = new PropertyPath(DocumentGroup.PinnedProperty);
            binding3.Source = this;
            binding3.Mode = BindingMode.TwoWay;
            base.SetBinding(PinnedProperty, binding3);
            Binding binding4 = new Binding();
            binding4.Path = new PropertyPath(DocumentGroup.ShowPinButtonProperty);
            binding4.Source = this;
            binding4.Mode = BindingMode.TwoWay;
            base.SetBinding(ShowPinButtonInTabProperty, binding4);
            this.Presenter = this.CreatePresenter();
        }

        protected virtual void ActivateItemCore()
        {
            if (!base.Manager.IsDisposing)
            {
                base.Manager.DockController.Activate(this, false);
            }
        }

        internal void ApplyExpandState()
        {
            this.ExpandStateLocker.LockOnce();
            this.CheckExpandState();
        }

        protected override Size CalcMinSizeValue(Size value)
        {
            Size size = base.CalcMinSizeValue(value);
            return (((this.ExpandableChild == null) || double.IsNaN(this.ExpandableChild.CollapseWidth)) ? size : new Size(Math.Max(size.Width, this.GetCollapsedItemWidth()), size.Height));
        }

        private void CheckAutoHiddenState()
        {
            base.ParentLockHelper.DoWhenUnlocked(new LockHelper.LockHelperDelegate(this.CheckAutoHiddenStateCore));
        }

        private void CheckAutoHiddenStateCore()
        {
            DockLayoutManager manager = this.FindDockLayoutManager();
            if ((manager != null) && ((base.Parent != null) && ((this.AutoHidden != base.IsAutoHidden) && (!base.IsInDesignTime && (!base.IsInitializing && !base.IsDeserializing)))))
            {
                manager.CheckAutoHiddenState(this);
            }
        }

        protected virtual void CheckContent(object content)
        {
            content = this.CheckContentAsUri(content);
            LayoutGroup group = content as LayoutGroup;
            if (group == null)
            {
                if (content is UIElement)
                {
                    base.SetValue(ControlPropertyKey, content);
                }
                else
                {
                    base.SetValue(ContentItem.IsDataBoundPropertyKey, content != null);
                }
            }
            else
            {
                group.ParentPanel = this;
                VisitDelegate<BaseLayoutItem> visit = <>c.<>9__217_0;
                if (<>c.<>9__217_0 == null)
                {
                    VisitDelegate<BaseLayoutItem> local1 = <>c.<>9__217_0;
                    visit = <>c.<>9__217_0 = item => item.CoerceValue(BaseLayoutItem.IsControlItemsHostProperty);
                }
                group.Accept(visit);
                base.SetValue(LayoutPropertyKey, group);
            }
            this.SetContentPresenterContainer(content);
        }

        private object CheckContentAsUri(object content)
        {
            System.Uri uri = content as System.Uri;
            if (uri != null)
            {
                base.SetValue(UriPropertyKey, uri);
                object obj2 = XamlLoaderHelper.LoadContentFromUri(uri);
                if (obj2 != null)
                {
                    content = obj2;
                }
            }
            return content;
        }

        private void CheckExpandState()
        {
            if (base.IsVisibleCore && (base.Manager != null))
            {
                if (base.ParentLockHelper.IsLocked)
                {
                    base.ParentLockHelper.AddUnlockAction(new LockHelper.LockHelperDelegate(this.CheckExpandStateCore));
                }
                else if (base.Parent != null)
                {
                    this.CheckExpandStateCore();
                }
            }
        }

        private void CheckExpandStateCore()
        {
            if (this.ExpandStateLocker.IsLocked)
            {
                Func<bool> fallback = <>c.<>9__286_1;
                if (<>c.<>9__286_1 == null)
                {
                    Func<bool> local1 = <>c.<>9__286_1;
                    fallback = <>c.<>9__286_1 = () => false;
                }
                if (this.FindDockLayoutManager().Return<DockLayoutManager, bool>(x => x.CheckAutoHideExpandState(this), fallback))
                {
                    this.ExpandStateLocker.Unlock();
                }
            }
            if (this.AutoHideExpandState != DevExpress.Xpf.Docking.Base.AutoHideExpandState.Hidden)
            {
                this.UpdateAutoHideButtons();
            }
            else
            {
                Func<LayoutGroup, LockHelper> evaluator = <>c.<>9__286_2;
                if (<>c.<>9__286_2 == null)
                {
                    Func<LayoutGroup, LockHelper> local2 = <>c.<>9__286_2;
                    evaluator = <>c.<>9__286_2 = x => x.IsAnimatedLockHelper;
                }
                base.Parent.With<LayoutGroup, LockHelper>(evaluator).Do<LockHelper>(x => x.DoWhenUnlocked(new LockHelper.LockHelperDelegate(this.UpdateAutoHideButtons)));
            }
        }

        private void CheckPinStatusOnParentChanged()
        {
            if (this.Pinned)
            {
                if (base.IsTabDocument)
                {
                    Action<DispatcherOperation> action = <>c.<>9__287_0;
                    if (<>c.<>9__287_0 == null)
                    {
                        Action<DispatcherOperation> local1 = <>c.<>9__287_0;
                        action = <>c.<>9__287_0 = x => x.Abort();
                    }
                    this.checkPinStatusOperation.Do<DispatcherOperation>(action);
                    this.checkPinStatusOperation = null;
                    this.NotifyParentPinStatusChanged();
                }
                else
                {
                    this.checkPinStatusOperation = base.Dispatcher.BeginInvoke(delegate {
                        this.checkPinStatusOperation = null;
                        this.Pinned = false;
                    }, new object[0]);
                }
            }
        }

        protected virtual void ClearContent(object oldContent)
        {
            UIElement element = oldContent as UIElement;
            if (element != null)
            {
                element.ClearValue(DockLayoutManager.LayoutItemProperty);
            }
            base.ClearValue(UriPropertyKey);
            base.ClearValue(LayoutPropertyKey);
            base.ClearValue(ControlPropertyKey);
            base.ClearValue(ContentItem.IsDataBoundPropertyKey);
            LayoutGroup group = oldContent as LayoutGroup;
            if (group != null)
            {
                group.ParentPanel = null;
            }
        }

        protected internal override void ClearTemplate()
        {
            if (!EnvironmentHelper.IsNet45OrNewer)
            {
                base.ClearTemplate();
            }
            else if (!this.fClearTemplateRequested)
            {
                this.fClearTemplateRequested = true;
                base.Dispatcher.BeginInvoke(delegate {
                    if (this.fClearTemplateRequested)
                    {
                        this.ClearTemplateCore();
                    }
                }, new object[0]);
                base.ClearValue(DockLayoutManager.UIScopeProperty);
            }
        }

        protected internal override void ClearTemplateCore()
        {
            this.fClearTemplateRequested = false;
            base.ClearTemplateCore();
            if (this.Layout != null)
            {
                this.Layout.ClearTemplateCore();
            }
        }

        protected virtual Color CoerceActualTabBackgroundColor(Color value) => 
            !(this.TabBackgroundColor != Colors.Transparent) ? ((base.ActualAppearanceObject == null) ? value : base.ActualAppearanceObject.TabBackgroundColor) : this.TabBackgroundColor;

        protected override string CoerceCaptionFormat(string captionFormat) => 
            string.IsNullOrEmpty(captionFormat) ? DockLayoutManagerParameters.LayoutPanelCaptionFormat : captionFormat;

        protected virtual DevExpress.Xpf.Docking.DockItemState CoerceDockItemState(DevExpress.Xpf.Docking.DockItemState dockItemState)
        {
            DevExpress.Xpf.Docking.DockItemState floating = (base.Parent != null) ? DevExpress.Xpf.Docking.DockItemState.Docked : DevExpress.Xpf.Docking.DockItemState.Undefined;
            if (base.IsFloating)
            {
                floating = DevExpress.Xpf.Docking.DockItemState.Floating;
            }
            if (base.IsAutoHidden)
            {
                floating = DevExpress.Xpf.Docking.DockItemState.AutoHidden;
            }
            if (base.IsClosed)
            {
                floating = DevExpress.Xpf.Docking.DockItemState.Closed;
            }
            return floating;
        }

        protected virtual bool CoerceHasBackground(bool value) => 
            base.Background != null;

        protected virtual bool CoerceHasBorder(bool value) => 
            this.ShowBorder;

        protected override bool CoerceIsCaptionVisible(bool visible) => 
            !(base.Parent is DocumentGroup) && base.CoerceIsCaptionVisible(visible);

        protected virtual bool CoerceIsCollapseButtonVisible(object baseValue) => 
            this.IsInlineMode && (this.ShowCollapseButton && (base.IsAutoHidden && (this.AutoHideExpandState == DevExpress.Xpf.Docking.Base.AutoHideExpandState.Expanded)));

        protected override bool CoerceIsControlItemsHost(bool value) => 
            this.Layout != null;

        protected virtual bool CoerceIsExpandButtonVisible(object baseValue) => 
            this.IsInlineMode && (this.ShowExpandButton && (base.IsAutoHidden && (this.AutoHideExpandState != DevExpress.Xpf.Docking.Base.AutoHideExpandState.Expanded)));

        protected virtual object CoerceIsHideButtonVisible(object baseValue) => 
            (!this.IsInlineMode || !this.ShowHideButton) ? ((object) 0) : ((object) base.IsAutoHidden);

        protected override bool CoerceIsMaximizeButtonVisible(bool visible) => 
            this.IsMaximizable && (base.AllowMaximize && (this.ShowMaximizeButton && !this.IsMaximized));

        protected override bool CoerceIsMinimizeButtonVisible(bool visible) => 
            this.IsMinimizable && (base.AllowMinimize && (this.ShowMinimizeButton && !this.IsMinimized));

        protected override bool CoerceIsPinButtonVisible(bool visible) => 
            !base.IsTabDocument ? (this.ShowPinButton && ((base.IsAutoHidden ? (base.AllowDock && base.Parent.AllowDock) : base.AllowHide) && !base.IsFloating)) : this.ShowPinButtonInTab;

        protected override bool CoerceIsRestoreButtonVisible(bool visible) => 
            this.IsMaximizable && (this.ShowRestoreButton && (this.IsMinimized || this.IsMaximized));

        protected override bool CoerceIsTabPage(bool value) => 
            base.Parent is TabbedGroup;

        internal override void Control_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!base.IsActivationCancelled)
            {
                base.Manager.Do<DockLayoutManager>(x => x.QueueFocus(this));
            }
        }

        private LayoutPanelContentPresenter CreatePresenter()
        {
            LayoutPanelContentPresenter target = new LayoutPanelContentPresenter();
            BindingHelper.SetBinding(target, System.Windows.Controls.ContentPresenter.ContentTemplateProperty, this, ContentItem.ContentTemplateProperty, BindingMode.OneWay);
            BindingHelper.SetBinding(target, System.Windows.Controls.ContentPresenter.ContentTemplateSelectorProperty, this, ContentItem.ContentTemplateSelectorProperty, BindingMode.OneWay);
            BindingHelper.SetBinding(target, FrameworkElement.DataContextProperty, this, FrameworkElement.DataContextProperty, BindingMode.OneWay);
            base.SetValue(ContentPresenterPropertyKey, target);
            return target;
        }

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

        void IGeneratorHost.ClearContainer(DependencyObject container, object item)
        {
            if (Equals(this, container))
            {
                base.ClearValue(ContentItem.ContentProperty);
            }
        }

        DependencyObject IGeneratorHost.GenerateContainerForItem(object item, DataTemplate itemTemplate, DataTemplateSelector itemTemplateSelector)
        {
            base.SetValue(ContentItem.ContentProperty, item);
            return this;
        }

        DependencyObject IGeneratorHost.GenerateContainerForItem(object item, int index, DataTemplate itemTemplate, DataTemplateSelector itemTemplateSelector)
        {
            base.SetValue(ContentItem.ContentProperty, item);
            return this;
        }

        DependencyObject IGeneratorHost.LinkContainerToItem(DependencyObject container, object item, DataTemplate itemTemplate, DataTemplateSelector itemTemplateSelector)
        {
            base.SetValue(ContentItem.ContentProperty, item);
            return this;
        }

        protected internal override void EndFloating()
        {
            this.Pinned = false;
        }

        protected internal override IUIElement FindUIScopeCore()
        {
            IUIElement element = base.FindUIScopeCore();
            return (!(element is LayoutPanel) ? element : null);
        }

        internal double GetCollapsedItemWidth()
        {
            if (this.ExpandableChild == null)
            {
                return double.NaN;
            }
            Func<LayoutPanelContentPresenter, double> evaluator = <>c.<>9__138_0;
            if (<>c.<>9__138_0 == null)
            {
                Func<LayoutPanelContentPresenter, double> local1 = <>c.<>9__138_0;
                evaluator = <>c.<>9__138_0 = x => x.RenderSize.Width;
            }
            return ((base.ActualWidth - this.Presenter.Return<LayoutPanelContentPresenter, double>(evaluator, () => base.ActualWidth)) + this.ExpandableChild.CollapseWidth);
        }

        internal virtual bool GetIsMaximized() => 
            this.FloatState == DevExpress.Xpf.Docking.FloatState.Maximized;

        internal virtual bool GetIsMinimized() => 
            this.FloatState == DevExpress.Xpf.Docking.FloatState.Minimized;

        protected override LayoutItemType GetLayoutItemTypeCore() => 
            LayoutItemType.Panel;

        protected override BaseLayoutItem[] GetNodesCore()
        {
            LayoutGroup layout = this.Layout;
            if (layout == null)
            {
                return BaseLayoutItem.EmptyNodes;
            }
            return new BaseLayoutItem[] { layout };
        }

        protected override void InvokeCoerceDockItemStateCore()
        {
            base.CoerceValue(DockItemStateProperty);
        }

        private void NotifyParentPinStatusChanged()
        {
            base.ParentLockHelper.DoWhenUnlocked(delegate {
                if (base.Parent != null)
                {
                    base.Parent.OnItemPinStatusChanged(this);
                }
            });
            this.NotifyViewPinStatusChanged();
        }

        internal void NotifyViewPinStatusChanged()
        {
            base.RaiseVisualChanged();
            base.InvalidateTabHeader();
            if (base.Manager != null)
            {
                base.Manager.InvalidateView(base.Parent);
            }
        }

        protected override void OnActualAppearanceObjectChanged(AppearanceObject newValue)
        {
            base.CoerceValue(ActualTabBackgroundColorProperty);
            base.OnActualAppearanceObjectChanged(newValue);
        }

        protected override void OnActualCaptionChanged(string value)
        {
            base.OnActualCaptionChanged(value);
            if (base.Parent is FloatGroup)
            {
                base.CoerceParentProperty(BaseLayoutItem.ActualCaptionProperty);
            }
        }

        protected override void OnAllowCloseChanged(bool value)
        {
            bool isCloseButtonVisible = base.IsCloseButtonVisible;
            base.OnAllowCloseChanged(value);
            if ((base.IsCloseButtonVisible == isCloseButtonVisible) && (base.Parent != null))
            {
                base.Parent.CoerceValue(BaseLayoutItem.IsCloseButtonVisibleProperty);
            }
        }

        protected override void OnAllowMaximizeChanged(bool value)
        {
            base.OnAllowMaximizeChanged(value);
            if (base.Parent is FloatGroup)
            {
                base.Parent.CoerceValue(FloatGroup.CanMaximizeProperty);
            }
        }

        protected override void OnAllowMinimizeChanged(bool value)
        {
            base.OnAllowMinimizeChanged(value);
            if (base.Parent is FloatGroup)
            {
                base.Parent.CoerceValue(FloatGroup.CanMinimizeProperty);
            }
        }

        protected override void OnAllowSizingChanged(bool value)
        {
            base.OnAllowSizingChanged(value);
            if (base.Parent is FloatGroup)
            {
                base.CoerceParentProperty(BaseLayoutItem.AllowSizingProperty);
            }
        }

        protected virtual void OnAutoHiddenChanged(bool oldValue, bool newValue)
        {
            if (base.IsStyleUpdateInProgress)
            {
                base.Dispatcher.BeginInvoke(new Action(this.CheckAutoHiddenState), new object[0]);
            }
            else
            {
                this.CheckAutoHiddenState();
            }
            base.InvokeCoerceInheritableProperties();
        }

        protected virtual void OnAutoHideExpandStateChanged(DevExpress.Xpf.Docking.Base.AutoHideExpandState oldValue, DevExpress.Xpf.Docking.Base.AutoHideExpandState newValue)
        {
            if (!this.ExpandAnimationLocker)
            {
                this.ApplyExpandState();
            }
        }

        protected virtual void OnClosed()
        {
        }

        protected virtual void OnClosing(CancelEventArgs e)
        {
        }

        protected override void OnContentChanged(object content, object oldContent)
        {
            this.ClearContent(oldContent);
            this.CheckContent(content);
        }

        private void OnControlAdded(UIElement control)
        {
            if (control != null)
            {
                control.SetValue(DockLayoutManager.LayoutItemProperty, this);
                if (!base.IsInDesignTime)
                {
                    this.ExpandableChild = control as IExpandableChild;
                    if (this.ExpandableChild != null)
                    {
                        this.ExpandableChild.IsExpandedChanged += new ValueChangedEventHandler<bool>(this.OnExpandableChildIsExpandedChanged);
                        if (!this.ExpandableChild.IsExpanded)
                        {
                            base.SetCurrentValue(BaseLayoutItem.ItemWidthProperty, GridLength.Auto);
                        }
                        base.CoerceValue(BaseLayoutItem.ActualMinSizeProperty);
                    }
                }
            }
        }

        private void OnControlChanged(UIElement control, UIElement oldControl)
        {
            base.CoerceValue(BaseLayoutItem.IsControlItemsHostProperty);
            if (!base.IsControlItemsHost)
            {
                this.OnControlPropertyChanged(control, oldControl);
            }
        }

        internal virtual void OnControlGotFocus()
        {
            if (!base.IsActive && (base.Manager != null))
            {
                base.Manager.Activate(this, false, true);
            }
        }

        protected virtual void OnControlPropertyChanged(UIElement control, UIElement oldControl)
        {
            this.OnControlRemoved(oldControl);
            this.OnControlAdded(control);
        }

        private void OnControlRemoved(UIElement oldControl)
        {
            if (oldControl != null)
            {
                oldControl.ClearValue(DockLayoutManager.LayoutItemProperty);
            }
            if (this.ExpandableChild != null)
            {
                this.ExpandableChild.IsExpandedChanged -= new ValueChangedEventHandler<bool>(this.OnExpandableChildIsExpandedChanged);
                this.ExpandableChild = null;
                base.CoerceValue(BaseLayoutItem.ActualMinSizeProperty);
            }
        }

        protected internal override void OnDeserializationComplete()
        {
            base.OnDeserializationComplete();
            this.CheckAutoHiddenState();
        }

        protected virtual void OnDockItemStateChanged(DevExpress.Xpf.Docking.DockItemState oldState, DevExpress.Xpf.Docking.DockItemState newState)
        {
            this.UpdateMinMaxState();
            this.UpdateButtons();
        }

        protected override void OnDockLayoutManagerChanged()
        {
            base.OnDockLayoutManagerChanged();
            this.CheckAutoHiddenState();
            this.CheckExpandState();
        }

        protected override void OnDockLayoutManagerChanged(DockLayoutManager oldValue, DockLayoutManager newValue)
        {
            base.OnDockLayoutManagerChanged(oldValue, newValue);
            this.UnsubscribeAutoHideDisplayModeChanged(oldValue);
            this.SubscribeAutoHideDisplayModeChanged(newValue);
            Func<DockLayoutManager, AutoHideMode> evaluator = <>c.<>9__251_0;
            if (<>c.<>9__251_0 == null)
            {
                Func<DockLayoutManager, AutoHideMode> local1 = <>c.<>9__251_0;
                evaluator = <>c.<>9__251_0 = x => x.AutoHideMode;
            }
            this.autoHideDisplayMode = newValue.Return<DockLayoutManager, AutoHideMode>(evaluator, <>c.<>9__251_1 ??= () => AutoHideMode.Default);
        }

        private void OnExpandableChildIsExpandedChanged(object sender, ValueChangedEventArgs<bool> e)
        {
            if (!e.NewValue)
            {
                GridLength length = (this.ExpandableChild != null) ? new GridLength(this.ExpandableChild.CollapseWidth) : GridLength.Auto;
                base.SetCurrentValue(BaseLayoutItem.ItemWidthProperty, length);
                if (base.IsAutoHidden)
                {
                    this.UpdateDockSituationWidth(length);
                }
            }
            else
            {
                if (!base.ResizeLockHelper.IsLocked || base.IsAutoHidden)
                {
                    base.SetCurrentValue(BaseLayoutItem.ItemWidthProperty, GridLength.Auto);
                    if (base.IsAutoHidden)
                    {
                        this.UpdateDockSituationWidth(GridLength.Auto);
                    }
                }
                Action<IExpandableChild> action = <>c.<>9__294_0;
                if (<>c.<>9__294_0 == null)
                {
                    Action<IExpandableChild> local1 = <>c.<>9__294_0;
                    action = <>c.<>9__294_0 = x => x.ExpandedWidth = double.NaN;
                }
                this.ExpandableChild.Do<IExpandableChild>(action);
            }
        }

        protected virtual void OnFloatStateChanged(DevExpress.Xpf.Docking.FloatState oldState, DevExpress.Xpf.Docking.FloatState newState)
        {
            this.UpdateMinMaxState();
            this.UpdateButtons();
            base.RaiseVisualChanged();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            if (this.ExpandableChild != null)
            {
                base.CoerceValue(BaseLayoutItem.ActualMinSizeProperty);
            }
        }

        protected override void OnIsActiveChanged(bool value)
        {
            base.OnIsActiveChanged(value);
            if (value)
            {
                this.LastActivationDateTime = DateTime.Now;
            }
        }

        protected override void OnIsCloseButtonVisibleChanged(bool visible)
        {
            base.OnIsCloseButtonVisibleChanged(visible);
            if (base.Parent != null)
            {
                base.Parent.CoerceValue(BaseLayoutItem.IsCloseButtonVisibleProperty);
            }
        }

        protected override void OnIsMaximizeButtonVisibleChanged(bool visible)
        {
            base.OnIsMaximizeButtonVisibleChanged(visible);
            if (base.Parent != null)
            {
                base.Parent.CoerceValue(BaseLayoutItem.IsMaximizeButtonVisibleProperty);
            }
        }

        protected virtual void OnIsMaximizedChanged(bool maximized)
        {
            base.RaiseVisualChanged();
        }

        protected virtual void OnIsMinimizedChanged(bool minimized)
        {
            base.RaiseVisualChanged();
        }

        protected override void OnIsRestoreButtonVisibleChanged(bool visible)
        {
            base.OnIsRestoreButtonVisibleChanged(visible);
            if (base.Parent != null)
            {
                base.Parent.CoerceValue(BaseLayoutItem.IsRestoreButtonVisibleProperty);
            }
        }

        protected override void OnIsSelectedItemChanged()
        {
            base.OnIsSelectedItemChanged();
            base.RaiseVisualChanged();
            if (base.IsAutoHidden && (!base.IsSelectedItem && !base.IsThemeChangeInProgress))
            {
                base.SetCurrentValue(AutoHideExpandStateProperty, DevExpress.Xpf.Docking.Base.AutoHideExpandState.Hidden);
            }
        }

        private void OnIsTouchEnabledChanged(bool oldValue, bool newValue)
        {
            if (IsTouchEnabledPropertyKey != null)
            {
                this.Presenter.SetValue(IsTouchEnabledPropertyKey, newValue);
            }
        }

        protected override void OnIsVisibleChanged(bool isVisible)
        {
            base.OnIsVisibleChanged(isVisible);
            if (base.IsControlItemsHost)
            {
                this.Layout.CoerceValue(BaseLayoutItem.IsVisibleCoreProperty);
            }
            if (isVisible)
            {
                this.CheckExpandState();
            }
            else if (base.IsAutoHidden && (base.Manager != null))
            {
                base.Manager.HideView(base.Parent);
            }
        }

        private void OnLayoutChanged(LayoutGroup group, LayoutGroup oldGroup)
        {
            base.CoerceValue(BaseLayoutItem.IsControlItemsHostProperty);
            if (oldGroup != null)
            {
                oldGroup.ClearValue(BaseLayoutItem.IsControlItemsHostPropertyKey);
            }
            if (group != null)
            {
                group.SetValue(BaseLayoutItem.IsControlItemsHostPropertyKey, true);
                if (group.ItemType != LayoutItemType.Group)
                {
                    throw new NotSupportedException(DockLayoutManagerHelper.GetRule(DockLayoutManagerRule.WrongLayoutRoot));
                }
            }
            this.OnLayoutPropertyChanged(oldGroup, group);
        }

        protected virtual void OnLayoutPropertyChanged(LayoutGroup oldValue, LayoutGroup newValue)
        {
            if (oldValue != null)
            {
                base.RemoveLogicalChild(oldValue);
            }
            if (newValue != null)
            {
                base.AddLogicalChild(newValue);
            }
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            this.CheckExpandState();
            if (this.ExpandableChild != null)
            {
                base.CoerceValue(BaseLayoutItem.ActualMinSizeProperty);
            }
        }

        private void OnManagerAutoHideDisplayModeChanged(object sender, PropertyChangedEventArgs e)
        {
            Func<DockLayoutManager, AutoHideMode> evaluator = <>c.<>9__297_0;
            if (<>c.<>9__297_0 == null)
            {
                Func<DockLayoutManager, AutoHideMode> local1 = <>c.<>9__297_0;
                evaluator = <>c.<>9__297_0 = x => x.AutoHideMode;
            }
            this.autoHideDisplayMode = base.Manager.Return<DockLayoutManager, AutoHideMode>(evaluator, <>c.<>9__297_1 ??= () => AutoHideMode.Default);
            base.CoerceValue(IsHideButtonVisibleProperty);
            base.CoerceValue(IsExpandButtonVisibleProperty);
            base.CoerceValue(IsCollapseButtonVisibleProperty);
        }

        protected override void OnParentChanged()
        {
            base.OnParentChanged();
            base.CoerceValue(BaseLayoutItem.IsCaptionVisibleProperty);
            if (base.Parent != null)
            {
                base.LogicalTreeLockHelper.DoWhenUnlocked(new LockHelper.LockHelperDelegate(this.UpdateButtons));
                Action<FloatGroup> action = <>c.<>9__264_0;
                if (<>c.<>9__264_0 == null)
                {
                    Action<FloatGroup> local1 = <>c.<>9__264_0;
                    action = <>c.<>9__264_0 = delegate (FloatGroup x) {
                        x.CoerceValue(FloatGroup.CanMaximizeProperty);
                        x.CoerceValue(FloatGroup.CanMinimizeProperty);
                    };
                }
                (base.Parent as FloatGroup).Do<FloatGroup>(action);
            }
            this.LayoutSizeBeforeHide = (base.Parent is AutoHideGroup) ? base.LayoutSize : Size.Empty;
            this.CheckAutoHiddenState();
            this.CheckExpandState();
        }

        protected override void OnParentChanged(LayoutGroup oldParent, LayoutGroup newParent)
        {
            base.OnParentChanged(oldParent, newParent);
            this.CheckPinStatusOnParentChanged();
            base.InvokeCoerceDockItemState();
        }

        protected internal override void OnParentItemsChanged()
        {
            base.OnParentItemsChanged();
            base.InvokeCoerceDockItemState();
            if (base.IsFloating)
            {
                this.UpdateButtons();
            }
        }

        protected internal override void OnParentLoaded()
        {
            base.OnParentLoaded();
            this.CheckExpandState();
        }

        protected virtual void OnPinnedChanged(bool oldValue, bool newValue)
        {
            if (base.IsTabDocument)
            {
                this.NotifyParentPinStatusChanged();
            }
        }

        protected override void OnRenderSizeChangedCore(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChangedCore(sizeInfo);
            if ((this.ExpandableChild != null) && base.IsVisibleCore)
            {
                double value = sizeInfo.NewSize.Width;
                if (!base.ItemWidth.IsAuto || this.ExpandableChild.IsExpanded)
                {
                    bool isLocked = base.ResizeLockHelper.IsLocked;
                    if (value <= this.GetCollapsedItemWidth())
                    {
                        if (this.ExpandableChild.IsExpanded && (!base.IsAutoHidden | isLocked))
                        {
                            this.ExpandableChild.IsExpanded = false;
                        }
                    }
                    else if (!this.ExpandableChild.IsExpanded)
                    {
                        if (!base.IsAutoHidden | isLocked)
                        {
                            this.ExpandableChild.IsExpanded = true;
                        }
                    }
                    else
                    {
                        if (isLocked)
                        {
                            Func<LayoutPanelContentPresenter, double> evaluator = <>c.<>9__268_0;
                            if (<>c.<>9__268_0 == null)
                            {
                                Func<LayoutPanelContentPresenter, double> local1 = <>c.<>9__268_0;
                                evaluator = <>c.<>9__268_0 = x => x.RenderSize.Width;
                            }
                            this.ExpandableChild.ExpandedWidth = this.Presenter.Return<LayoutPanelContentPresenter, double>(evaluator, () => value);
                        }
                        if (base.IsAutoHidden)
                        {
                            this.UpdateDockSituationWidth(new GridLength(value));
                        }
                    }
                }
            }
        }

        protected override void OnRootGroupChanged(LayoutGroup oldValue, LayoutGroup newValue)
        {
            if (!(newValue is FloatGroup))
            {
                base.ClearValue(DevExpress.Xpf.Docking.WindowServiceHelper.IWindowServiceProperty);
            }
            else
            {
                Binding binding = new Binding();
                binding.Path = new PropertyPath(DevExpress.Xpf.Docking.WindowServiceHelper.IWindowServiceProperty);
                binding.Source = newValue;
                base.SetBinding(DevExpress.Xpf.Docking.WindowServiceHelper.IWindowServiceProperty, binding);
            }
        }

        protected virtual void OnShowBorderChanged()
        {
            base.CoerceValue(HasBorderProperty);
            base.RaiseGeometryChanged();
        }

        protected override void OnShowCaptionChanged(bool value)
        {
            base.OnShowCaptionChanged(value);
            if (base.Parent is FloatGroup)
            {
                base.CoerceParentProperty(BaseLayoutItem.ShowCaptionProperty);
            }
        }

        protected override void OnShowCloseButtonChanged(bool show)
        {
            base.OnShowCloseButtonChanged(show);
            if (base.Parent != null)
            {
                base.Parent.CoerceValue(BaseLayoutItem.IsCloseButtonVisibleProperty);
            }
        }

        protected virtual void OnShowCollapseButtonChanged(bool oldValue, bool newValue)
        {
            base.CoerceValue(IsCollapseButtonVisibleProperty);
        }

        protected virtual void OnShowExpandButtonChanged(bool oldValue, bool newValue)
        {
            base.CoerceValue(IsExpandButtonVisibleProperty);
        }

        protected virtual void OnShowHideButtonChanged(bool oldValue, bool newValue)
        {
            base.CoerceValue(IsHideButtonVisibleProperty);
        }

        protected virtual void OnShowPinButtonInTabChanged(bool oldValue, bool newValue)
        {
            base.CoerceValue(BaseLayoutItem.IsPinButtonVisibleProperty);
        }

        protected virtual void OnTabBackgroundColorChanged(Color oldValue, Color newValue)
        {
            base.CoerceValue(ActualTabBackgroundColorProperty);
        }

        protected virtual void OnTabPinLocationChanged(TabHeaderPinLocation oldValue, TabHeaderPinLocation newValue)
        {
            if (this.IsPinnedTab)
            {
                this.NotifyParentPinStatusChanged();
            }
        }

        protected override void OnUnloaded()
        {
            if (this.fClearTemplateRequested)
            {
                this.ClearTemplateCore();
            }
            base.OnUnloaded();
        }

        protected override void OnVisibilityChangedOverride(Visibility visibility)
        {
            base.OnVisibilityChangedOverride(visibility);
            if (base.IsControlItemsHost)
            {
                this.Layout.CoerceValue(BaseLayoutItem.IsVisibleCoreProperty);
            }
        }

        internal void ProcessFocus()
        {
            this.OnControlGotFocus();
        }

        protected internal override void SelectTemplate()
        {
            if (this.fClearTemplateRequested)
            {
                if ((base.PartMultiTemplateControl != null) && ReferenceEquals(base.PartMultiTemplateControl.LayoutItem, this))
                {
                    base.PartMultiTemplateControl.LayoutItem = null;
                }
                this.fClearTemplateRequested = false;
            }
            base.SelectTemplate();
            if (this.Layout != null)
            {
                this.Layout.SelectTemplate();
            }
        }

        protected internal override void SelectTemplateIfNeeded()
        {
            if (this.fClearTemplateRequested || ((base.PartMultiTemplateControl != null) && (base.PartMultiTemplateControl.LayoutItem == null)))
            {
                this.SelectTemplate();
            }
        }

        protected internal override void SetAutoHidden(bool autoHidden)
        {
            this.AutoHidden = autoHidden;
        }

        private void SetContentPresenterContainer(object content)
        {
            if (content != null)
            {
                if (base.IsControlItemsHost)
                {
                    base.RemoveLogicalChild(this.Presenter);
                }
                else if (LogicalTreeHelper.GetParent(this.Presenter) == null)
                {
                    base.AddLogicalChild(this.Presenter);
                }
            }
            this.Presenter.Content = content;
        }

        internal override void SetFloatState(DevExpress.Xpf.Docking.FloatState state)
        {
            if (this.FloatState != state)
            {
                base.SetValue(FloatStatePropertyKey, state);
            }
        }

        protected internal override void SetSelected(DockLayoutManager manager, bool value)
        {
        }

        protected virtual void SubscribeAutoHideDisplayModeChanged(DockLayoutManager manager)
        {
            manager.Do<DockLayoutManager>(delegate (DockLayoutManager x) {
                x.AutoHideDisplayModeChanged += this.AutoHideDisplayModePropertyChangedHandler.Handler;
            });
        }

        protected internal override bool ToggleTabPinStatus()
        {
            this.Pinned = !this.Pinned;
            return true;
        }

        protected virtual void UnsubscribeAutoHideDisplayModeChanged(DockLayoutManager manager)
        {
            manager.Do<DockLayoutManager>(delegate (DockLayoutManager x) {
                x.AutoHideDisplayModeChanged -= this.AutoHideDisplayModePropertyChangedHandler.Handler;
            });
        }

        private void UpdateAutoHideButtons()
        {
            base.CoerceValue(IsHideButtonVisibleProperty);
            base.CoerceValue(IsExpandButtonVisibleProperty);
            base.CoerceValue(IsCollapseButtonVisibleProperty);
        }

        protected internal override void UpdateButtons()
        {
            base.CoerceValue(BaseLayoutItem.IsMaximizeButtonVisibleProperty);
            base.CoerceValue(BaseLayoutItem.IsMinimizeButtonVisibleProperty);
            base.CoerceValue(BaseLayoutItem.IsPinButtonVisibleProperty);
            base.CoerceValue(BaseLayoutItem.IsRestoreButtonVisibleProperty);
        }

        private void UpdateDockSituationWidth(GridLength newWidth)
        {
            base.DockSituation.Do<DockSituation>(x => x.Width = newWidth);
        }

        private void UpdateIsMaximized()
        {
            this.IsMaximized = this.GetIsMaximized();
        }

        private void UpdateIsMinimized()
        {
            this.IsMinimized = this.GetIsMinimized();
        }

        internal void UpdateMinMaxState()
        {
            this.UpdateIsMinimized();
            this.UpdateIsMaximized();
        }

        public Color ActualTabBackgroundColor =>
            (Color) base.GetValue(ActualTabBackgroundColorProperty);

        [XtraSerializableProperty]
        public bool AllowDockToDocumentGroup
        {
            get => 
                (bool) base.GetValue(AllowDockToDocumentGroupProperty);
            set => 
                base.SetValue(AllowDockToDocumentGroupProperty, value);
        }

        public bool AutoHidden
        {
            get => 
                (bool) base.GetValue(AutoHiddenProperty);
            set => 
                base.SetValue(AutoHiddenProperty, value);
        }

        public DevExpress.Xpf.Docking.Base.AutoHideExpandState AutoHideExpandState
        {
            get => 
                (DevExpress.Xpf.Docking.Base.AutoHideExpandState) base.GetValue(AutoHideExpandStateProperty);
            set => 
                base.SetValue(AutoHideExpandStateProperty, value);
        }

        public UIElement ContentPresenter =>
            (UIElement) base.GetValue(ContentPresenterProperty);

        [Description("Gets a UIElement that has been assigned to the ContentItem.Content property.This is a dependency property.")]
        public UIElement Control =>
            (UIElement) base.GetValue(ControlProperty);

        public DevExpress.Xpf.Docking.DockItemState DockItemState
        {
            get => 
                (DevExpress.Xpf.Docking.DockItemState) base.GetValue(DockItemStateProperty);
            private set => 
                base.SetValue(DockItemStatePropertyKey, value);
        }

        public DevExpress.Xpf.Docking.FloatState FloatState
        {
            get => 
                (DevExpress.Xpf.Docking.FloatState) base.GetValue(FloatStateProperty);
            private set => 
                base.SetValue(FloatStatePropertyKey, value);
        }

        [Description("Gets whether the panel's border is visible.This is a dependency property.")]
        public bool HasBorder =>
            (bool) base.GetValue(HasBorderProperty);

        [Description("Gets or sets a horizontal scroll bar's visibility mode. This is a dependency property.")]
        public ScrollBarVisibility HorizontalScrollBarVisibility
        {
            get => 
                (ScrollBarVisibility) base.GetValue(HorizontalScrollBarVisibilityProperty);
            set => 
                base.SetValue(HorizontalScrollBarVisibilityProperty, value);
        }

        [Description("Gets whether the LayoutPanel is maximized. This property is in effect for floating panels.")]
        public bool IsMaximized
        {
            get => 
                (bool) base.GetValue(IsMaximizedProperty);
            private set => 
                base.SetValue(IsMaximizedPropertyKey, value);
        }

        public bool IsMinimized
        {
            get => 
                (bool) base.GetValue(IsMinimizedProperty);
            private set => 
                base.SetValue(IsMinimizedPropertyKey, value);
        }

        [Description("Gets whether the Pin button is visible.")]
        public bool IsPinButtonVisible =>
            (bool) base.GetValue(BaseLayoutItem.IsPinButtonVisibleProperty);

        [Description("Gets a LayoutGroup object that has been assigned to the ContentItem.Content property.This is a dependency property.")]
        public LayoutGroup Layout =>
            (LayoutGroup) base.GetValue(LayoutProperty);

        [XtraSerializableProperty, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public Rect SerializableFloatingBounds
        {
            get => 
                new Rect(this.FloatLocationBeforeClose, this.FloatSizeBeforeClose);
            set
            {
                if (!value.IsEmpty)
                {
                    this.FloatLocationBeforeClose = new Point(value.X, value.Y);
                    this.FloatSizeBeforeClose = new Size(value.Width, value.Height);
                }
            }
        }

        [XtraSerializableProperty, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public Point SerializableFloatingOffset
        {
            get => 
                this.FloatOffsetBeforeClose;
            set => 
                this.FloatOffsetBeforeClose = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool SerializableIsMaximized
        {
            get => 
                this.IsMaximized;
            set
            {
            }
        }

        [Description("Gets or sets whether the panel's border is visible.This is a dependency property."), XtraSerializableProperty, Category("Content")]
        public bool ShowBorder
        {
            get => 
                (bool) base.GetValue(ShowBorderProperty);
            set => 
                base.SetValue(ShowBorderProperty, value);
        }

        public bool ShowCollapseButton
        {
            get => 
                (bool) base.GetValue(ShowCollapseButtonProperty);
            set => 
                base.SetValue(ShowCollapseButtonProperty, value);
        }

        public bool ShowExpandButton
        {
            get => 
                (bool) base.GetValue(ShowExpandButtonProperty);
            set => 
                base.SetValue(ShowExpandButtonProperty, value);
        }

        public bool ShowHideButton
        {
            get => 
                (bool) base.GetValue(ShowHideButtonProperty);
            set => 
                base.SetValue(ShowHideButtonProperty, value);
        }

        public bool ShowInDocumentSelector
        {
            get => 
                (bool) base.GetValue(ShowInDocumentSelectorProperty);
            set => 
                base.SetValue(ShowInDocumentSelectorProperty, value);
        }

        [Description("Gets or sets whether the maximize button is shown within the LayoutPanel. This property is supported for floating panels."), XtraSerializableProperty, Category("Layout")]
        public bool ShowMaximizeButton
        {
            get => 
                (bool) base.GetValue(BaseLayoutItem.ShowMaximizeButtonProperty);
            set => 
                base.SetValue(BaseLayoutItem.ShowMaximizeButtonProperty, value);
        }

        [Description("Gets or sets whether the minimize button is shown in the LayoutPanel. This property is supported for floating panels. This is a dependency property."), XtraSerializableProperty, Category("Layout")]
        public bool ShowMinimizeButton
        {
            get => 
                (bool) base.GetValue(BaseLayoutItem.ShowMinimizeButtonProperty);
            set => 
                base.SetValue(BaseLayoutItem.ShowMinimizeButtonProperty, value);
        }

        [Description("Gets or sets whether the Pin button is visible."), XtraSerializableProperty, Category("Layout")]
        public bool ShowPinButton
        {
            get => 
                (bool) base.GetValue(BaseLayoutItem.ShowPinButtonProperty);
            set => 
                base.SetValue(BaseLayoutItem.ShowPinButtonProperty, value);
        }

        [Description("Gets of sets whether the restore button is displayed within the LayoutPanel. This property is supported for floating panels."), XtraSerializableProperty, Category("Layout")]
        public bool ShowRestoreButton
        {
            get => 
                (bool) base.GetValue(BaseLayoutItem.ShowRestoreButtonProperty);
            set => 
                base.SetValue(BaseLayoutItem.ShowRestoreButtonProperty, value);
        }

        [XtraSerializableProperty, Category("TabHeader")]
        public Color TabBackgroundColor
        {
            get => 
                (Color) base.GetValue(TabBackgroundColorProperty);
            set => 
                base.SetValue(TabBackgroundColorProperty, value);
        }

        [Description("Gets a System.Uri object used to assign the current LayoutPanel's content. This is a dependency property.")]
        public System.Uri Uri =>
            (System.Uri) base.GetValue(UriProperty);

        [Description("Gets or sets a vertical scroll bar's visibility mode. This is a dependency property.")]
        public ScrollBarVisibility VerticalScrollBarVisibility
        {
            get => 
                (ScrollBarVisibility) base.GetValue(VerticalScrollBarVisibilityProperty);
            set => 
                base.SetValue(VerticalScrollBarVisibilityProperty, value);
        }

        internal IExpandableChild ExpandableChild { get; private set; }

        internal LockHelper ExpandStateLocker
        {
            get
            {
                this.expandStateLocker ??= new LockHelper();
                return this.expandStateLocker;
            }
        }

        internal bool HasFloatLocationBeforeClose { get; private set; }

        internal bool HasFloatOffsetBeforeClose { get; private set; }

        internal bool IsDockedAsDocument =>
            base.Parent is DocumentGroup;

        internal bool IsFloatingRootInTabbedGroup
        {
            get => 
                this._isFloatingRootInTabbedGroup;
            set
            {
                if (this._isFloatingRootInTabbedGroup != value)
                {
                    this._isFloatingRootInTabbedGroup = value;
                    base.RaiseGeometryChanged();
                }
            }
        }

        internal bool Pinned
        {
            get => 
                (bool) base.GetValue(PinnedProperty);
            set => 
                base.SetValue(PinnedProperty, value);
        }

        internal bool ShowPinButtonInTab
        {
            get => 
                (bool) base.GetValue(ShowPinButtonInTabProperty);
            set => 
                base.SetValue(ShowPinButtonInTabProperty, value);
        }

        internal override bool SupportsFloatOrMDIState =>
            LayoutItemsHelper.IsFloatingRootItem(this);

        internal override bool SupportsOptimizedLogicalTree =>
            true;

        internal TabHeaderPinLocation TabPinLocation
        {
            get => 
                (TabHeaderPinLocation) base.GetValue(TabPinLocationProperty);
            set => 
                base.SetValue(TabPinLocationProperty, value);
        }

        protected internal Point FloatLocationBeforeClose
        {
            get => 
                this._floatLocationBeforeClose;
            set
            {
                if (this._floatLocationBeforeClose != value)
                {
                    this._floatLocationBeforeClose = value;
                    this.HasFloatLocationBeforeClose = true;
                }
            }
        }

        protected internal Point FloatOffsetBeforeClose
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

        protected internal Size FloatSizeBeforeClose { get; set; }

        protected internal override bool IsPinnedTab =>
            this.Pinned && base.IsTabDocument;

        protected internal DateTime LastActivationDateTime { get; set; }

        protected internal Size LayoutSizeBeforeHide { get; set; }

        private AutoHideDisplayModePropertyChangedWeakEventHandler<LayoutPanel> AutoHideDisplayModePropertyChangedHandler
        {
            get
            {
                if (this.autoHideDisplayModePropertyChangedHandler == null)
                {
                    Action<LayoutPanel, object, PropertyChangedEventArgs> onEventAction = <>c.<>9__188_0;
                    if (<>c.<>9__188_0 == null)
                    {
                        Action<LayoutPanel, object, PropertyChangedEventArgs> local1 = <>c.<>9__188_0;
                        onEventAction = <>c.<>9__188_0 = delegate (LayoutPanel owner, object o, PropertyChangedEventArgs e) {
                            owner.OnManagerAutoHideDisplayModeChanged(o, e);
                        };
                    }
                    this.autoHideDisplayModePropertyChangedHandler = new AutoHideDisplayModePropertyChangedWeakEventHandler<LayoutPanel>(this, onEventAction);
                }
                return this.autoHideDisplayModePropertyChangedHandler;
            }
        }

        private bool IsInlineMode =>
            this.autoHideDisplayMode == AutoHideMode.Inline;

        private bool IsTouchEnabled
        {
            get => 
                (bool) base.GetValue(IsTouchEnabledProperty);
            set => 
                base.SetValue(IsTouchEnabledProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LayoutPanel.<>c <>9 = new LayoutPanel.<>c();
            public static Func<LayoutPanel.LayoutPanelContentPresenter, double> <>9__138_0;
            public static Action<LayoutPanel, object, PropertyChangedEventArgs> <>9__188_0;
            public static VisitDelegate<BaseLayoutItem> <>9__217_0;
            public static Func<DockLayoutManager, AutoHideMode> <>9__251_0;
            public static Func<AutoHideMode> <>9__251_1;
            public static Action<FloatGroup> <>9__264_0;
            public static Func<LayoutPanel.LayoutPanelContentPresenter, double> <>9__268_0;
            public static Func<bool> <>9__286_1;
            public static Func<LayoutGroup, LockHelper> <>9__286_2;
            public static Action<DispatcherOperation> <>9__287_0;
            public static Action<IExpandableChild> <>9__294_0;
            public static Func<DockLayoutManager, AutoHideMode> <>9__297_0;
            public static Func<AutoHideMode> <>9__297_1;

            internal void <.cctor>b__42_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutPanel) dObj).OnLayoutChanged((LayoutGroup) e.NewValue, (LayoutGroup) e.OldValue);
            }

            internal void <.cctor>b__42_1(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutPanel) dObj).OnControlChanged((UIElement) e.NewValue, (UIElement) e.OldValue);
            }

            internal void <.cctor>b__42_10(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutPanel) dObj).OnAutoHideExpandStateChanged((AutoHideExpandState) e.OldValue, (AutoHideExpandState) e.NewValue);
            }

            internal void <.cctor>b__42_11(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutPanel) dObj).OnTabPinLocationChanged((TabHeaderPinLocation) e.OldValue, (TabHeaderPinLocation) e.NewValue);
            }

            internal void <.cctor>b__42_12(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutPanel) dObj).OnPinnedChanged((bool) e.OldValue, (bool) e.NewValue);
            }

            internal void <.cctor>b__42_13(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutPanel) dObj).OnDockItemStateChanged((DockItemState) e.OldValue, (DockItemState) e.NewValue);
            }

            internal object <.cctor>b__42_14(DependencyObject dObj, object value) => 
                ((LayoutPanel) dObj).CoerceDockItemState((DockItemState) value);

            internal void <.cctor>b__42_15(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutPanel) dObj).OnShowHideButtonChanged((bool) e.OldValue, (bool) e.NewValue);
            }

            internal void <.cctor>b__42_16(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutPanel) dObj).OnShowExpandButtonChanged((bool) e.OldValue, (bool) e.NewValue);
            }

            internal void <.cctor>b__42_17(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutPanel) dObj).OnShowCollapseButtonChanged((bool) e.OldValue, (bool) e.NewValue);
            }

            internal void <.cctor>b__42_18(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutPanel) dObj).OnShowPinButtonInTabChanged((bool) e.OldValue, (bool) e.NewValue);
            }

            internal object <.cctor>b__42_19(DependencyObject dObj, object value) => 
                ((LayoutPanel) dObj).CoerceIsHideButtonVisible(value);

            internal void <.cctor>b__42_2(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutPanel) dObj).OnShowBorderChanged();
            }

            internal object <.cctor>b__42_20(DependencyObject dObj, object value) => 
                ((LayoutPanel) dObj).CoerceIsExpandButtonVisible(value);

            internal object <.cctor>b__42_21(DependencyObject dObj, object value) => 
                ((LayoutPanel) dObj).CoerceIsCollapseButtonVisible(value);

            internal void <.cctor>b__42_22(LayoutPanel d, bool oldValue, bool newValue)
            {
                d.OnIsTouchEnabledChanged(oldValue, newValue);
            }

            internal object <.cctor>b__42_3(DependencyObject dObj, object value) => 
                ((LayoutPanel) dObj).CoerceHasBorder((bool) value);

            internal void <.cctor>b__42_4(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutPanel) dObj).OnTabBackgroundColorChanged((Color) e.OldValue, (Color) e.NewValue);
            }

            internal object <.cctor>b__42_5(DependencyObject dObj, object value) => 
                ((LayoutPanel) dObj).CoerceActualTabBackgroundColor((Color) value);

            internal void <.cctor>b__42_6(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutPanel) dObj).OnIsMaximizedChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__42_7(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutPanel) dObj).OnIsMinimizedChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__42_8(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutPanel) dObj).OnFloatStateChanged((FloatState) e.OldValue, (FloatState) e.NewValue);
            }

            internal void <.cctor>b__42_9(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutPanel) dObj).OnAutoHiddenChanged((bool) e.OldValue, (bool) e.NewValue);
            }

            internal void <CheckContent>b__217_0(BaseLayoutItem item)
            {
                item.CoerceValue(BaseLayoutItem.IsControlItemsHostProperty);
            }

            internal bool <CheckExpandStateCore>b__286_1() => 
                false;

            internal LockHelper <CheckExpandStateCore>b__286_2(LayoutGroup x) => 
                x.IsAnimatedLockHelper;

            internal void <CheckPinStatusOnParentChanged>b__287_0(DispatcherOperation x)
            {
                x.Abort();
            }

            internal void <get_AutoHideDisplayModePropertyChangedHandler>b__188_0(LayoutPanel owner, object o, PropertyChangedEventArgs e)
            {
                owner.OnManagerAutoHideDisplayModeChanged(o, e);
            }

            internal double <GetCollapsedItemWidth>b__138_0(LayoutPanel.LayoutPanelContentPresenter x) => 
                x.RenderSize.Width;

            internal AutoHideMode <OnDockLayoutManagerChanged>b__251_0(DockLayoutManager x) => 
                x.AutoHideMode;

            internal AutoHideMode <OnDockLayoutManagerChanged>b__251_1() => 
                AutoHideMode.Default;

            internal void <OnExpandableChildIsExpandedChanged>b__294_0(IExpandableChild x)
            {
                x.ExpandedWidth = double.NaN;
            }

            internal AutoHideMode <OnManagerAutoHideDisplayModeChanged>b__297_0(DockLayoutManager x) => 
                x.AutoHideMode;

            internal AutoHideMode <OnManagerAutoHideDisplayModeChanged>b__297_1() => 
                AutoHideMode.Default;

            internal void <OnParentChanged>b__264_0(FloatGroup x)
            {
                x.CoerceValue(FloatGroup.CanMaximizeProperty);
                x.CoerceValue(FloatGroup.CanMinimizeProperty);
            }

            internal double <OnRenderSizeChangedCore>b__268_0(LayoutPanel.LayoutPanelContentPresenter x) => 
                x.RenderSize.Width;
        }

        private class AutoHideDisplayModePropertyChangedWeakEventHandler<TOwner> : WeakEventHandler<TOwner, PropertyChangedEventArgs, PropertyChangedEventHandler> where TOwner: class
        {
            private static Action<WeakEventHandler<TOwner, PropertyChangedEventArgs, PropertyChangedEventHandler>, object> action;
            private static Func<WeakEventHandler<TOwner, PropertyChangedEventArgs, PropertyChangedEventHandler>, PropertyChangedEventHandler> create;

            static AutoHideDisplayModePropertyChangedWeakEventHandler()
            {
                LayoutPanel.AutoHideDisplayModePropertyChangedWeakEventHandler<TOwner>.action = delegate (WeakEventHandler<TOwner, PropertyChangedEventArgs, PropertyChangedEventHandler> h, object o) {
                    ((DockLayoutManager) o).AutoHideDisplayModeChanged -= h.Handler;
                };
                LayoutPanel.AutoHideDisplayModePropertyChangedWeakEventHandler<TOwner>.create = h => new PropertyChangedEventHandler(h.OnEvent);
            }

            public AutoHideDisplayModePropertyChangedWeakEventHandler(TOwner owner, Action<TOwner, object, PropertyChangedEventArgs> onEventAction) : base(owner, onEventAction, LayoutPanel.AutoHideDisplayModePropertyChangedWeakEventHandler<TOwner>.action, LayoutPanel.AutoHideDisplayModePropertyChangedWeakEventHandler<TOwner>.create)
            {
            }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly LayoutPanel.AutoHideDisplayModePropertyChangedWeakEventHandler<TOwner>.<>c <>9;

                static <>c()
                {
                    LayoutPanel.AutoHideDisplayModePropertyChangedWeakEventHandler<TOwner>.<>c.<>9 = new LayoutPanel.AutoHideDisplayModePropertyChangedWeakEventHandler<TOwner>.<>c();
                }

                internal void <.cctor>b__3_0(WeakEventHandler<TOwner, PropertyChangedEventArgs, PropertyChangedEventHandler> h, object o)
                {
                    ((DockLayoutManager) o).AutoHideDisplayModeChanged -= h.Handler;
                }

                internal PropertyChangedEventHandler <.cctor>b__3_1(WeakEventHandler<TOwner, PropertyChangedEventArgs, PropertyChangedEventHandler> h) => 
                    new PropertyChangedEventHandler(h.OnEvent);
            }
        }

        private class LayoutPanelContentPresenter : ContentPresenter, ILogicalOwner, IInputElement
        {
            [IgnoreDependencyPropertiesConsistencyChecker]
            internal static readonly DependencyProperty ContentInternalProperty;
            private readonly List<object> logicalChildren = new List<object>();

            event RoutedEventHandler ILogicalOwner.Loaded
            {
                add
                {
                    base.Loaded += value;
                }
                remove
                {
                    base.Loaded -= value;
                }
            }

            static LayoutPanelContentPresenter()
            {
                new DevExpress.Xpf.Docking.DependencyPropertyRegistrator<LayoutPanel.LayoutPanelContentPresenter>().Register<object>("ContentInternal", ref ContentInternalProperty, null, (dObj, e) => ((LayoutPanel.LayoutPanelContentPresenter) dObj).OnContentChanged(e.NewValue, e.OldValue), null);
            }

            public LayoutPanelContentPresenter()
            {
                base.Focusable = false;
                this.StartListen(ContentInternalProperty, "Content", BindingMode.OneWay);
            }

            public void AddChild(object child)
            {
                Func<DependencyObject, bool> evaluator = <>c.<>9__6_0;
                if (<>c.<>9__6_0 == null)
                {
                    Func<DependencyObject, bool> local1 = <>c.<>9__6_0;
                    evaluator = <>c.<>9__6_0 = x => ReferenceEquals(LogicalTreeHelper.GetParent(x), null);
                }
                if ((child as DependencyObject).Return<DependencyObject, bool>(evaluator, (<>c.<>9__6_1 ??= () => false)) && !this.logicalChildren.Contains(child))
                {
                    this.logicalChildren.Add(child);
                    base.AddLogicalChild(child);
                }
            }

            protected virtual void OnContentChanged(object content, object oldContent)
            {
                this.RemoveChild(oldContent);
                this.AddChild(content);
            }

            public void RemoveChild(object child)
            {
                this.logicalChildren.Remove(child);
                base.RemoveLogicalChild(child);
            }

            protected override IEnumerator LogicalChildren =>
                this.logicalChildren.GetEnumerator();

            bool ILogicalOwner.IsLoaded =>
                base.IsLoaded;

            double ILogicalOwner.ActualWidth =>
                base.ActualWidth;

            double ILogicalOwner.ActualHeight =>
                base.ActualHeight;

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly LayoutPanel.LayoutPanelContentPresenter.<>c <>9 = new LayoutPanel.LayoutPanelContentPresenter.<>c();
                public static Func<DependencyObject, bool> <>9__6_0;
                public static Func<bool> <>9__6_1;

                internal void <.cctor>b__1_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
                {
                    ((LayoutPanel.LayoutPanelContentPresenter) dObj).OnContentChanged(e.NewValue, e.OldValue);
                }

                internal bool <AddChild>b__6_0(DependencyObject x) => 
                    ReferenceEquals(LogicalTreeHelper.GetParent(x), null);

                internal bool <AddChild>b__6_1() => 
                    false;
            }
        }
    }
}

