namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Threading;

    [TemplatePart(Name="PART_HeadersPanelPresenter", Type=typeof(ItemsPresenter)), TemplatePart(Name="PART_SelectedPage", Type=typeof(FrameworkElement)), TemplatePart(Name="PART_ControlBox", Type=typeof(BaseControlBoxControl))]
    public abstract class LayoutTabControl : psvContentSelectorControl<BaseLayoutItem>
    {
        public static readonly DependencyProperty LayoutItemProperty;
        public static readonly DependencyProperty CaptionLocationProperty;
        public static readonly DependencyProperty CaptionOrientationProperty;
        public static readonly DependencyProperty TabHeaderLayoutTypeProperty;
        public static readonly DependencyProperty IsAutoFillHeadersProperty;
        public static readonly DependencyProperty ScrollIndexProperty;
        public static readonly DependencyProperty DestroyContentOnTabSwitchingProperty;
        public static readonly DependencyProperty TabContentCacheModeProperty;
        public static readonly DependencyProperty ActualTabContentCacheModeProperty;
        public static readonly DependencyProperty ShowTabForSinglePageProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty VisiblePagesCountProperty;
        public static readonly DependencyProperty FixedMultiLineTabHeadersProperty;
        public static readonly DependencyProperty ClipMarginProperty;
        public static readonly DependencyProperty ActualClipMarginProperty;
        private static readonly DependencyPropertyKey ActualClipMarginPropertyKey;
        public static readonly DependencyProperty TabPanelMarginProperty;
        public static readonly DependencyProperty ActualTabPanelMarginProperty;
        private static readonly DependencyPropertyKey ActualTabPanelMarginPropertyKey;
        public static readonly DependencyProperty AutoScrollOnOverflowProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty HasScrollProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty ShowTabHeadersProperty;
        public static readonly DependencyProperty ViewStyleProperty;
        public static readonly DependencyProperty ArrangeAllCachedTabsProperty;
        public static readonly DependencyProperty TabbedGroupDisplayModeProperty;
        public static readonly DependencyProperty AreTabHeadersVisibleProperty;
        private static readonly DependencyPropertyKey AreTabHeadersVisiblePropertyKey;
        private readonly EventHandler weakItemsChangedHandler;
        private TabHeadersPanel partHeadersPanel;

        public event NotifyCollectionChangedEventHandler ItemsChanged;

        public event LayoutTabControlSelectionChangedEventHandler SelectionChanged;

        static LayoutTabControl()
        {
            DevExpress.Xpf.Docking.DependencyPropertyRegistrator<LayoutTabControl> registrator = new DevExpress.Xpf.Docking.DependencyPropertyRegistrator<LayoutTabControl>();
            registrator.Register<BaseLayoutItem>("LayoutItem", ref LayoutItemProperty, null, (dObj, e) => ((LayoutTabControl) dObj).OnLayoutItemChanged((BaseLayoutItem) e.OldValue, (BaseLayoutItem) e.NewValue), null);
            registrator.Register<DevExpress.Xpf.Docking.CaptionLocation>("CaptionLocation", ref CaptionLocationProperty, DevExpress.Xpf.Docking.CaptionLocation.Default, (dObj, e) => ((LayoutTabControl) dObj).OnCaptionLocationChanged((DevExpress.Xpf.Docking.CaptionLocation) e.NewValue), null);
            registrator.Register<Orientation>("CaptionOrientation", ref CaptionOrientationProperty, Orientation.Horizontal, (dObj, e) => ((LayoutTabControl) dObj).OnCaptionOrientationChanged((Orientation) e.NewValue), null);
            registrator.Register<DevExpress.Xpf.Layout.Core.TabHeaderLayoutType>("TabHeaderLayoutType", ref TabHeaderLayoutTypeProperty, DevExpress.Xpf.Layout.Core.TabHeaderLayoutType.Default, (dObj, e) => ((LayoutTabControl) dObj).OnTabHeaderLayoutTypeChanged((DevExpress.Xpf.Layout.Core.TabHeaderLayoutType) e.NewValue), null);
            registrator.Register<bool>("IsAutoFillHeaders", ref IsAutoFillHeadersProperty, false, (dObj, e) => ((LayoutTabControl) dObj).OnIsAutoFillHeadersChanged((bool) e.NewValue), null);
            registrator.Register<int>("ScrollIndex", ref ScrollIndexProperty, 0, (dObj, e) => ((LayoutTabControl) dObj).OnScrollIndexChanged((int) e.NewValue), null);
            registrator.Register<bool>("ShowTabForSinglePage", ref ShowTabForSinglePageProperty, true, (dObj, e) => ((LayoutTabControl) dObj).OnShowTabForSinglePageChanged((bool) e.OldValue, (bool) e.NewValue), null);
            registrator.Register<int>("VisiblePagesCount", ref VisiblePagesCountProperty, -1, (dObj, e) => ((LayoutTabControl) dObj).OnVisiblePagesCountChanged((int) e.OldValue, (int) e.NewValue), null);
            registrator.Register<bool>("DestroyContentOnTabSwitching", ref DestroyContentOnTabSwitchingProperty, true, (dObj, e) => ((LayoutTabControl) dObj).OnTabCacheModeChanged(), null);
            registrator.Register<DevExpress.Xpf.Core.TabContentCacheMode>("TabContentCacheMode", ref TabContentCacheModeProperty, DevExpress.Xpf.Core.TabContentCacheMode.None, (dObj, e) => ((LayoutTabControl) dObj).OnTabCacheModeChanged(), null);
            registrator.Register<DevExpress.Xpf.Core.TabContentCacheMode>("ActualTabContentCacheMode", ref ActualTabContentCacheModeProperty, DevExpress.Xpf.Core.TabContentCacheMode.None, (dObj, e) => ((LayoutTabControl) dObj).OnActualTabCacheModeChanged((DevExpress.Xpf.Core.TabContentCacheMode) e.OldValue, (DevExpress.Xpf.Core.TabContentCacheMode) e.NewValue), null);
            registrator.Register<bool>("FixedMultiLineTabHeaders", ref FixedMultiLineTabHeadersProperty, false, (dObj, e) => ((LayoutTabControl) dObj).OnFixedMultiLineTabHeadersChanged((bool) e.NewValue), null);
            registrator.Register<Thickness>("ClipMargin", ref ClipMarginProperty, new Thickness(0.0, -2.0, 0.0, -2.0), (dObj, ea) => ((LayoutTabControl) dObj).OnClipMarginChanged((Thickness) ea.OldValue, (Thickness) ea.NewValue), null);
            Thickness defValue = new Thickness();
            registrator.RegisterReadonly<Thickness>("ActualClipMargin", ref ActualClipMarginPropertyKey, ref ActualClipMarginProperty, defValue, null, (dObj, value) => ((LayoutTabControl) dObj).CoerceActualClipMargin((Thickness) value));
            defValue = new Thickness();
            registrator.Register<Thickness>("TabPanelMargin", ref TabPanelMarginProperty, defValue, (dObj, ea) => ((LayoutTabControl) dObj).OnTabPanelMarginChanged((Thickness) ea.OldValue, (Thickness) ea.NewValue), null);
            defValue = new Thickness();
            registrator.RegisterReadonly<Thickness>("ActualTabPanelMargin", ref ActualTabPanelMarginPropertyKey, ref ActualTabPanelMarginProperty, defValue, null, (dObj, value) => ((LayoutTabControl) dObj).CoerceActualTabPanelMargin((Thickness) value));
            registrator.Register<DevExpress.Xpf.Docking.AutoScrollOnOverflow>("AutoScrollOnOverflow", ref AutoScrollOnOverflowProperty, DevExpress.Xpf.Docking.AutoScrollOnOverflow.AnyItem, null, null);
            registrator.Register<bool>("HasScroll", ref HasScrollProperty, false, (dObj, ea) => ((LayoutTabControl) dObj).OnHasScrollChanged((bool) ea.OldValue, (bool) ea.NewValue), null);
            registrator.Register<bool>("ShowTabHeaders", ref ShowTabHeadersProperty, true, (dObj, ea) => ((LayoutTabControl) dObj).OnShowTabHeadersChanged((bool) ea.NewValue), null);
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(LayoutTabControl), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<LayoutTabControl> registrator1 = DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<LayoutTabControl>.New().Register<DockingViewStyle>(System.Linq.Expressions.Expression.Lambda<Func<LayoutTabControl, DockingViewStyle>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(LayoutTabControl.get_ViewStyle)), parameters), out ViewStyleProperty, DockingViewStyle.Default, (d, oldValue, newValue) => d.OnViewStyleChanged(oldValue, newValue), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(LayoutTabControl), "d");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<LayoutTabControl> registrator2 = registrator1.Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<LayoutTabControl, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(LayoutTabControl.get_ArrangeAllCachedTabs)), expressionArray2), out ArrangeAllCachedTabsProperty, false, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(LayoutTabControl), "d");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<LayoutTabControl> registrator3 = registrator2.Register<DevExpress.Xpf.Docking.TabbedGroupDisplayMode>(System.Linq.Expressions.Expression.Lambda<Func<LayoutTabControl, DevExpress.Xpf.Docking.TabbedGroupDisplayMode>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(LayoutTabControl.get_TabbedGroupDisplayMode)), expressionArray3), out TabbedGroupDisplayModeProperty, DevExpress.Xpf.Docking.TabbedGroupDisplayMode.Default, (d, oldValue, newValue) => d.OnTabbedGroupDisplayModeChanged(oldValue, newValue), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(LayoutTabControl), "d");
            ParameterExpression[] expressionArray4 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            registrator3.RegisterReadOnly<bool>(System.Linq.Expressions.Expression.Lambda<Func<LayoutTabControl, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(LayoutTabControl.get_AreTabHeadersVisible)), expressionArray4), out AreTabHeadersVisiblePropertyKey, out AreTabHeadersVisibleProperty, false, (d, oldValue, newValue) => d.OnAreTabHeadersVisibleChanged(oldValue, newValue), frameworkOptions);
        }

        public LayoutTabControl()
        {
            base.CoerceValue(ActualClipMarginProperty);
            this.weakItemsChangedHandler = new EventHandler(this.OnGroupItemsChanged);
        }

        protected void CheckSelectionInGroup()
        {
            DevExpress.Xpf.Docking.LayoutGroup layoutItem = DockLayoutManager.GetLayoutItem(this) as DevExpress.Xpf.Docking.LayoutGroup;
            if (layoutItem != null)
            {
                int selectedIndex = base.SelectedIndex;
                layoutItem.CoerceValue(DevExpress.Xpf.Docking.LayoutGroup.SelectedItemProperty);
                TabbedGroup group2 = layoutItem as TabbedGroup;
                if ((group2 != null) && (group2.SelectedTabIndex != selectedIndex))
                {
                    group2.SelectedTabIndex = selectedIndex;
                }
            }
        }

        protected virtual void ClearGroupBindings(DevExpress.Xpf.Docking.LayoutGroup oldValue)
        {
            base.ClearValue(psvSelector<BaseLayoutItem>.SelectedIndexProperty);
            base.ClearValue(TabHeaderLayoutTypeProperty);
            base.ClearValue(CaptionLocationProperty);
            base.ClearValue(CaptionOrientationProperty);
            base.ClearValue(IsAutoFillHeadersProperty);
            base.ClearValue(ScrollIndexProperty);
            base.ClearValue(DestroyContentOnTabSwitchingProperty);
            base.ClearValue(TabContentCacheModeProperty);
            base.ClearValue(ItemsControl.ItemContainerStyleProperty);
            base.ClearValue(ItemsControl.ItemContainerStyleSelectorProperty);
            base.ClearValue(ShowTabForSinglePageProperty);
            base.ClearValue(VisiblePagesCountProperty);
            base.ClearValue(FixedMultiLineTabHeadersProperty);
            base.ClearValue(ShowTabHeadersProperty);
            base.ClearValue(ViewStyleProperty);
            base.ClearValue(ArrangeAllCachedTabsProperty);
            base.ClearValue(TabbedGroupDisplayModeProperty);
        }

        protected override void ClearSelectorItem(psvSelectorItem selectorItem)
        {
            if (selectorItem.LayoutItem != null)
            {
                selectorItem.LayoutItem.ClearTemplate();
            }
            base.ClearSelectorItem(selectorItem);
        }

        private object CoerceActualClipMargin(Thickness value)
        {
            double num = (ScreenHelper.DpiThicknessCorrection < 1.0) ? 1.0 : ScreenHelper.DpiThicknessCorrection;
            return this.RotateMargin(this.ClipMargin).Multiply(num);
        }

        private object CoerceActualTabPanelMargin(Thickness value) => 
            this.RotateMargin(this.TabPanelMargin).Multiply(ScreenHelper.DpiThicknessCorrection);

        protected override void EnsureItemsPanelCore(Panel itemsPanel)
        {
            base.EnsureItemsPanelCore(itemsPanel);
            this.partHeadersPanel = itemsPanel as TabHeadersPanel;
            this.EnsureTabHeadersPanel();
        }

        protected void EnsureSelectedContent()
        {
            if (base.SelectedContent != null)
            {
                base.SelectedContent.SelectTemplate();
            }
        }

        protected virtual void EnsureTabHeadersPanel()
        {
            if (this.PartHeadersPanel != null)
            {
                this.PartHeadersPanel.Orientation = HeadersPanelHelper.GetOrientation(this.CaptionLocation);
                this.PartHeadersPanel.TabHeaderLayoutType = this.TabHeaderLayoutType;
                this.PartHeadersPanel.IsAutoFillHeaders = this.IsAutoFillHeaders;
                this.PartHeadersPanel.FixedMultiLineTabHeaders = this.FixedMultiLineTabHeaders;
                this.Forward(this.PartHeadersPanel, BaseHeadersPanel.ClipMarginProperty, "ActualClipMargin", BindingMode.OneWay);
                this.Forward(this.PartHeadersPanel, BaseHeadersPanel.AutoScrollOnOverflowProperty, "AutoScrollOnOverflow", BindingMode.OneWay);
                this.PartHeadersPanel.ScrollIndex = this.ScrollIndex;
                base.Dispatcher.BeginInvoke(delegate {
                    this.PartHeadersPanel.SelectedIndex = base.SelectedIndex;
                    base.Dispatcher.BeginInvoke(() => this.PartHeadersPanel.AllowAnimation = true, DispatcherPriority.Render, new object[0]);
                }, new object[0]);
                this.UpdatePanelMeasure();
            }
        }

        protected abstract IView GetView(DockLayoutManager container);
        protected virtual void OnActualTabCacheModeChanged(DevExpress.Xpf.Core.TabContentCacheMode oldValue, DevExpress.Xpf.Core.TabContentCacheMode newValue)
        {
            if (this.PartFastRenderPanel != null)
            {
                this.PartFastRenderPanel.Initialize(this, newValue);
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.PartHeadersPresenter = base.GetTemplateChild("PART_HeadersPanelPresenter") as ItemsPresenter;
            this.PartSelectedPage = base.GetTemplateChild("PART_SelectedPage") as FrameworkElement;
            this.PartControlBox = base.GetTemplateChild("PART_ControlBox") as BaseControlBoxControl;
            if ((this.PartFastRenderPanel != null) && !LayoutItemsHelper.IsTemplateChild<LayoutTabControl>(this.PartFastRenderPanel, this))
            {
                this.PartFastRenderPanel.Dispose();
            }
            this.PartFastRenderPanel = base.GetTemplateChild("PART_FastRenderPanel") as LayoutTabFastRenderPanel;
            if (this.PartFastRenderPanel != null)
            {
                this.PartFastRenderPanel.Initialize(this, this.ActualTabContentCacheMode);
            }
            this.UpdateVisualState();
        }

        protected virtual void OnAreTabHeadersVisibleChanged(bool oldValue, bool newValue)
        {
            this.UpdatePanelMeasure();
            base.InvalidateMeasure();
            this.UpdateVisualState();
        }

        protected virtual void OnCaptionLocationChanged(DevExpress.Xpf.Docking.CaptionLocation captionLocation)
        {
            if (this.PartHeadersPanel != null)
            {
                this.PartHeadersPanel.Orientation = HeadersPanelHelper.GetOrientation(this.CaptionLocation);
                this.PartHeadersPanel.InvalidateMeasure();
            }
            base.CoerceValue(ActualTabPanelMarginProperty);
            base.CoerceValue(ActualClipMarginProperty);
            this.UpdateItemsLocation();
        }

        protected virtual void OnCaptionOrientationChanged(Orientation orientation)
        {
            base.CoerceValue(ActualTabPanelMarginProperty);
            base.CoerceValue(ActualClipMarginProperty);
            this.UpdateItemsOrientation();
        }

        private void OnClipMarginChanged(Thickness oldValue, Thickness newValue)
        {
            base.CoerceValue(ActualClipMarginProperty);
            base.InvalidateMeasure();
            base.InvalidateArrange();
        }

        protected override void OnDispose()
        {
            if (this.PartFastRenderPanel != null)
            {
                this.PartFastRenderPanel.Dispose();
                this.PartFastRenderPanel = null;
            }
            base.ClearValue(LayoutItemProperty);
            base.OnDispose();
        }

        protected virtual void OnFixedMultiLineTabHeadersChanged(bool newValue)
        {
            if (this.PartHeadersPanel != null)
            {
                this.PartHeadersPanel.FixedMultiLineTabHeaders = newValue;
            }
        }

        private void OnGroupItemsChanged(object sender, EventArgs ea)
        {
            NotifyCollectionChangedEventArgs e = (ea as NotifyCollectionChangedEventArgs) ?? new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
            base.CheckSelectedItemRemoved(e);
            if (this.ActualTabContentCacheMode == DevExpress.Xpf.Core.TabContentCacheMode.CacheAllTabs)
            {
                foreach (object obj2 in (IEnumerable) base.Items)
                {
                    BaseLayoutItem item = obj2 as BaseLayoutItem;
                    if (item != null)
                    {
                        item.SelectTemplate();
                    }
                }
            }
            this.RaiseItemsChanged(e);
            this.UpdatePanelMeasure();
            this.UpdateVisualState();
        }

        protected virtual void OnHasScrollChanged(bool oldValue, bool newValue)
        {
            if (newValue && (this.PartControlBox != null))
            {
                Action<FrameworkElement> method = <>c.<>9__139_0;
                if (<>c.<>9__139_0 == null)
                {
                    Action<FrameworkElement> local1 = <>c.<>9__139_0;
                    method = <>c.<>9__139_0 = delegate (FrameworkElement x) {
                        x.InvalidateMeasure();
                        Func<FrameworkElement, FrameworkElement> evaluator = <>c.<>9__139_1;
                        if (<>c.<>9__139_1 == null)
                        {
                            Func<FrameworkElement, FrameworkElement> local1 = <>c.<>9__139_1;
                            evaluator = <>c.<>9__139_1 = y => y.Parent as FrameworkElement;
                        }
                        Action<FrameworkElement> action = <>c.<>9__139_2;
                        if (<>c.<>9__139_2 == null)
                        {
                            Action<FrameworkElement> local2 = <>c.<>9__139_2;
                            action = <>c.<>9__139_2 = y => y.InvalidateMeasure();
                        }
                        x.With<FrameworkElement, FrameworkElement>(evaluator).Do<FrameworkElement>(action);
                    };
                }
                object[] args = new object[] { this.PartControlBox };
                base.Dispatcher.BeginInvoke(method, args);
            }
        }

        protected virtual void OnIsAutoFillHeadersChanged(bool autoFill)
        {
            if (this.PartHeadersPanel != null)
            {
                this.PartHeadersPanel.IsAutoFillHeaders = autoFill;
            }
        }

        protected virtual void OnLayoutGroupChanged(DevExpress.Xpf.Docking.LayoutGroup group)
        {
        }

        protected virtual void OnLayoutGroupChanged(DevExpress.Xpf.Docking.LayoutGroup oldValue, DevExpress.Xpf.Docking.LayoutGroup value)
        {
            this.OnLayoutGroupChanged(value);
            if (oldValue != null)
            {
                this.ClearGroupBindings(oldValue);
                oldValue.WeakItemsChanged -= this.weakItemsChangedHandler;
                base.ClearValue(DockLayoutManager.LayoutItemProperty);
                if (!base.IsDisposing)
                {
                    this.ClearItemsSource();
                }
            }
            if (value != null)
            {
                base.SetValue(DockLayoutManager.LayoutItemProperty, value);
                base.SetValue(ItemsControl.ItemsSourceProperty, value.Items);
                value.WeakItemsChanged += this.weakItemsChangedHandler;
                this.SetGroupBindings(value);
            }
        }

        protected virtual void OnLayoutItemChanged(BaseLayoutItem item)
        {
        }

        protected virtual void OnLayoutItemChanged(BaseLayoutItem oldValue, BaseLayoutItem value)
        {
            this.OnLayoutItemChanged(value);
            this.OnLayoutGroupChanged(oldValue as DevExpress.Xpf.Docking.LayoutGroup, value as DevExpress.Xpf.Docking.LayoutGroup);
        }

        protected virtual void OnScrollIndexChanged(int index)
        {
            if (this.PartHeadersPanel != null)
            {
                this.PartHeadersPanel.ScrollIndex = index;
            }
        }

        protected override void OnSelectedContentChanged(BaseLayoutItem newValue, BaseLayoutItem oldValue)
        {
            base.OnSelectedContentChanged(newValue, oldValue);
            if (base.Container != null)
            {
                base.Container.InvalidateView(this.LayoutGroup);
            }
            if ((oldValue != null) && (this.ActualTabContentCacheMode == DevExpress.Xpf.Core.TabContentCacheMode.None))
            {
                BaseLayoutItem[] items = new BaseLayoutItem[] { oldValue };
                using (new LogicalTreeLocker(base.Container, items))
                {
                    oldValue.ClearTemplate();
                }
            }
            if (newValue != null)
            {
                newValue.SelectTemplate();
            }
            this.RaiseSelectedContentChanged(oldValue, newValue);
        }

        protected override void OnSelectedIndexChanged(int index, int oldIndex)
        {
            base.OnSelectedIndexChanged(index, oldIndex);
            if (this.PartHeadersPanel != null)
            {
                this.PartHeadersPanel.SelectedIndex = index;
            }
        }

        protected override void OnSelectedItemChanged(BaseLayoutItem item, BaseLayoutItem oldItem)
        {
            base.OnSelectedItemChanged(item, oldItem);
            if (this.PartHeadersPanel != null)
            {
                this.PartHeadersPanel.InvalidateMeasure();
            }
        }

        protected virtual void OnShowTabForSinglePageChanged(bool oldValue, bool newValue)
        {
            this.UpdateHeaderVisibility();
        }

        private void OnShowTabHeadersChanged(bool newValue)
        {
            this.UpdateHeaderVisibility();
        }

        protected virtual void OnTabbedGroupDisplayModeChanged(DevExpress.Xpf.Docking.TabbedGroupDisplayMode oldValue, DevExpress.Xpf.Docking.TabbedGroupDisplayMode newValue)
        {
            this.UpdateHeaderVisibility();
        }

        protected virtual void OnTabCacheModeChanged()
        {
            this.ActualTabContentCacheMode = !this.DestroyContentOnTabSwitching ? DevExpress.Xpf.Core.TabContentCacheMode.CacheTabsOnSelecting : this.TabContentCacheMode;
        }

        protected virtual void OnTabHeaderLayoutTypeChanged(DevExpress.Xpf.Layout.Core.TabHeaderLayoutType type)
        {
            if (this.PartHeadersPanel != null)
            {
                this.PartHeadersPanel.TabHeaderLayoutType = type;
            }
        }

        protected virtual void OnTabPanelMarginChanged(Thickness oldValue, Thickness newValue)
        {
            base.CoerceValue(ActualTabPanelMarginProperty);
            base.InvalidateMeasure();
        }

        protected virtual void OnViewStyleChanged(DockingViewStyle oldValue, DockingViewStyle newValue)
        {
        }

        protected virtual void OnVisiblePagesCountChanged(int oldValue, int newValue)
        {
            this.UpdateHeaderVisibility();
        }

        protected override void PrepareSelectorItem(psvSelectorItem selectorItem, BaseLayoutItem item)
        {
            base.PrepareSelectorItem(selectorItem, item);
            TabbedPaneItem item2 = selectorItem as TabbedPaneItem;
            if (item2 != null)
            {
                item2.CaptionOrientation = this.CaptionOrientation;
                item2.CaptionLocation = this.CaptionLocation;
            }
            item.EnsureTemplate();
        }

        protected virtual void RaiseItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            if (this.ItemsChanged != null)
            {
                this.ItemsChanged(this, e);
            }
        }

        protected virtual void RaiseSelectedContentChanged(object oldContent, object content)
        {
            if (this.SelectionChanged != null)
            {
                this.SelectionChanged(this, new LayoutTabControlSelectionChangedEventArgs(oldContent, content));
            }
        }

        private Thickness RotateMargin(Thickness thickness)
        {
            switch (this.CaptionLocation)
            {
                case DevExpress.Xpf.Docking.CaptionLocation.Left:
                    return new Thickness(thickness.Top, thickness.Right, thickness.Bottom, thickness.Left);

                case DevExpress.Xpf.Docking.CaptionLocation.Right:
                    return new Thickness(thickness.Bottom, thickness.Left, thickness.Top, thickness.Right);

                case DevExpress.Xpf.Docking.CaptionLocation.Bottom:
                    return new Thickness(thickness.Right, thickness.Bottom, thickness.Left, thickness.Top);
            }
            return thickness;
        }

        protected virtual void SetGroupBindings(DevExpress.Xpf.Docking.LayoutGroup group)
        {
            BindingHelper.SetBinding(this, psvSelector<BaseLayoutItem>.SelectedIndexProperty, group, DevExpress.Xpf.Docking.LayoutGroup.SelectedTabIndexProperty, BindingMode.TwoWay);
            BindingHelper.SetBinding(this, TabHeaderLayoutTypeProperty, group, DevExpress.Xpf.Docking.LayoutGroup.TabHeaderLayoutTypeProperty, BindingMode.OneWay);
            BindingHelper.SetBinding(this, CaptionLocationProperty, group, BaseLayoutItem.CaptionLocationProperty, BindingMode.OneWay);
            BindingHelper.SetBinding(this, CaptionOrientationProperty, group, DevExpress.Xpf.Docking.LayoutGroup.CaptionOrientationProperty, BindingMode.OneWay);
            BindingHelper.SetBinding(this, IsAutoFillHeadersProperty, group, DevExpress.Xpf.Docking.LayoutGroup.TabHeadersAutoFillProperty, BindingMode.OneWay);
            BindingHelper.SetBinding(this, ScrollIndexProperty, group, DevExpress.Xpf.Docking.LayoutGroup.TabHeaderScrollIndexProperty, BindingMode.OneWay);
            BindingHelper.SetBinding(this, DestroyContentOnTabSwitchingProperty, this.LayoutGroup, DevExpress.Xpf.Docking.LayoutGroup.DestroyContentOnTabSwitchingProperty, BindingMode.OneWay);
            BindingHelper.SetBinding(this, TabContentCacheModeProperty, this.LayoutGroup, DevExpress.Xpf.Docking.LayoutGroup.TabContentCacheModeProperty, BindingMode.OneWay);
            BindingHelper.SetBinding(this, ItemsControl.ItemContainerStyleProperty, group, DevExpress.Xpf.Docking.LayoutGroup.TabItemContainerStyleProperty, BindingMode.OneWay);
            BindingHelper.SetBinding(this, ItemsControl.ItemContainerStyleSelectorProperty, group, DevExpress.Xpf.Docking.LayoutGroup.TabItemContainerStyleSelectorProperty, BindingMode.OneWay);
            BindingHelper.SetBinding(this, FixedMultiLineTabHeadersProperty, group, DevExpress.Xpf.Docking.LayoutGroup.FixedMultiLineTabHeadersProperty, BindingMode.OneWay);
            BindingHelper.SetBinding(this, VisiblePagesCountProperty, group, DevExpress.Xpf.Docking.LayoutGroup.VisiblePagesCountProperty, BindingMode.OneWay);
            BindingHelper.SetBinding(this, AutoScrollOnOverflowProperty, group, DevExpress.Xpf.Docking.LayoutGroup.AutoScrollOnOverflowProperty, BindingMode.OneWay);
            BindingHelper.SetBinding(this, HasScrollProperty, group, DevExpress.Xpf.Docking.LayoutGroup.TabHeaderHasScrollProperty, BindingMode.OneWay);
            BindingHelper.SetBinding(this, ShowTabHeadersProperty, group, DevExpress.Xpf.Docking.LayoutGroup.ShowTabHeadersProperty, BindingMode.OneWay);
            BindingHelper.SetBinding(this, ViewStyleProperty, group, BaseLayoutItem.DockingViewStyleProperty, BindingMode.OneWay);
            BindingHelper.SetBinding(this, ArrangeAllCachedTabsProperty, group, DevExpress.Xpf.Docking.LayoutGroup.ArrangeAllCachedTabsProperty, BindingMode.OneWay);
            BindingHelper.SetBinding(this, TabbedGroupDisplayModeProperty, group, DevExpress.Xpf.Docking.LayoutGroup.TabbedGroupDisplayModeCoreProperty, BindingMode.OneWay);
            if (group is TabbedGroup)
            {
                BindingHelper.SetBinding(this, ShowTabForSinglePageProperty, group, TabbedGroup.ShowTabForSinglePageProperty, BindingMode.OneWay);
            }
        }

        private void UpdateHeaderVisibility()
        {
            this.AreTabHeadersVisible = (this.TabbedGroupDisplayMode == DevExpress.Xpf.Docking.TabbedGroupDisplayMode.Default) && (this.ShowTabHeaders && ((this.VisiblePagesCount == 1) ? this.ShowTabForSinglePage : (this.VisiblePagesCount > 1)));
        }

        private void UpdateItemsLocation()
        {
            for (int i = 0; i < base.Items.Count; i++)
            {
                TabbedPaneItem item = base.ItemContainerGenerator.ContainerFromIndex(i) as TabbedPaneItem;
                if (item != null)
                {
                    item.CaptionLocation = this.CaptionLocation;
                }
            }
        }

        private void UpdateItemsOrientation()
        {
            for (int i = 0; i < base.Items.Count; i++)
            {
                TabbedPaneItem item = base.ItemContainerGenerator.ContainerFromIndex(i) as TabbedPaneItem;
                if (item != null)
                {
                    item.CaptionOrientation = this.CaptionOrientation;
                }
            }
        }

        private void UpdatePanelMeasure()
        {
            if (this.PartHeadersPanel != null)
            {
                this.PartHeadersPanel.AllowChildrenMeasure = this.AreTabHeadersVisible;
                this.PartHeadersPanel.InvalidateMeasure();
            }
        }

        protected virtual void UpdateVisualState()
        {
            VisualStateManager.GoToState(this, this.AreTabHeadersVisible ? "HeaderVisible" : "HeaderHidden", false);
        }

        public Thickness ActualClipMargin
        {
            get => 
                (Thickness) base.GetValue(ActualClipMarginProperty);
            private set => 
                base.SetValue(ActualClipMarginProperty, value);
        }

        public DevExpress.Xpf.Core.TabContentCacheMode ActualTabContentCacheMode
        {
            get => 
                (DevExpress.Xpf.Core.TabContentCacheMode) base.GetValue(ActualTabContentCacheModeProperty);
            set => 
                base.SetValue(ActualTabContentCacheModeProperty, value);
        }

        public Thickness ActualTabPanelMargin
        {
            get => 
                (Thickness) base.GetValue(ActualTabPanelMarginProperty);
            private set => 
                base.SetValue(ActualTabPanelMarginProperty, value);
        }

        public bool AreTabHeadersVisible
        {
            get => 
                (bool) base.GetValue(AreTabHeadersVisibleProperty);
            private set => 
                base.SetValue(AreTabHeadersVisiblePropertyKey, value);
        }

        public bool ArrangeAllCachedTabs
        {
            get => 
                (bool) base.GetValue(ArrangeAllCachedTabsProperty);
            set => 
                base.SetValue(ArrangeAllCachedTabsProperty, value);
        }

        public DevExpress.Xpf.Docking.AutoScrollOnOverflow AutoScrollOnOverflow
        {
            get => 
                (DevExpress.Xpf.Docking.AutoScrollOnOverflow) base.GetValue(AutoScrollOnOverflowProperty);
            set => 
                base.SetValue(AutoScrollOnOverflowProperty, value);
        }

        public DevExpress.Xpf.Docking.CaptionLocation CaptionLocation
        {
            get => 
                (DevExpress.Xpf.Docking.CaptionLocation) base.GetValue(CaptionLocationProperty);
            set => 
                base.SetValue(CaptionLocationProperty, value);
        }

        public Orientation CaptionOrientation
        {
            get => 
                (Orientation) base.GetValue(CaptionOrientationProperty);
            set => 
                base.SetValue(CaptionOrientationProperty, value);
        }

        public Thickness ClipMargin
        {
            get => 
                (Thickness) base.GetValue(ClipMarginProperty);
            set => 
                base.SetValue(ClipMarginProperty, value);
        }

        public bool DestroyContentOnTabSwitching
        {
            get => 
                (bool) base.GetValue(DestroyContentOnTabSwitchingProperty);
            set => 
                base.SetValue(DestroyContentOnTabSwitchingProperty, value);
        }

        public DockingViewStyle ViewStyle
        {
            get => 
                (DockingViewStyle) base.GetValue(ViewStyleProperty);
            set => 
                base.SetValue(ViewStyleProperty, value);
        }

        public bool FixedMultiLineTabHeaders
        {
            get => 
                (bool) base.GetValue(FixedMultiLineTabHeadersProperty);
            set => 
                base.SetValue(FixedMultiLineTabHeadersProperty, value);
        }

        public bool IsAutoFillHeaders
        {
            get => 
                (bool) base.GetValue(IsAutoFillHeadersProperty);
            set => 
                base.SetValue(IsAutoFillHeadersProperty, value);
        }

        public DevExpress.Xpf.Docking.LayoutGroup LayoutGroup =>
            this.LayoutItem as DevExpress.Xpf.Docking.LayoutGroup;

        public BaseLayoutItem LayoutItem
        {
            get => 
                (BaseLayoutItem) base.GetValue(LayoutItemProperty);
            set => 
                base.SetValue(LayoutItemProperty, value);
        }

        public BaseControlBoxControl PartControlBox { get; private set; }

        public LayoutTabFastRenderPanel PartFastRenderPanel { get; private set; }

        public TabHeadersPanel PartHeadersPanel =>
            this.partHeadersPanel ?? (base.PartItemsPanel as TabHeadersPanel);

        public ItemsPresenter PartHeadersPresenter { get; private set; }

        public FrameworkElement PartSelectedPage { get; private set; }

        public int ScrollIndex
        {
            get => 
                (int) base.GetValue(ScrollIndexProperty);
            set => 
                base.SetValue(ScrollIndexProperty, value);
        }

        public bool ShowTabForSinglePage
        {
            get => 
                (bool) base.GetValue(ShowTabForSinglePageProperty);
            set => 
                base.SetValue(ShowTabForSinglePageProperty, value);
        }

        public DevExpress.Xpf.Docking.TabbedGroupDisplayMode TabbedGroupDisplayMode
        {
            get => 
                (DevExpress.Xpf.Docking.TabbedGroupDisplayMode) base.GetValue(TabbedGroupDisplayModeProperty);
            set => 
                base.SetValue(TabbedGroupDisplayModeProperty, value);
        }

        public DevExpress.Xpf.Core.TabContentCacheMode TabContentCacheMode
        {
            get => 
                (DevExpress.Xpf.Core.TabContentCacheMode) base.GetValue(TabContentCacheModeProperty);
            set => 
                base.SetValue(TabContentCacheModeProperty, value);
        }

        public DevExpress.Xpf.Layout.Core.TabHeaderLayoutType TabHeaderLayoutType
        {
            get => 
                (DevExpress.Xpf.Layout.Core.TabHeaderLayoutType) base.GetValue(TabHeaderLayoutTypeProperty);
            set => 
                base.SetValue(TabHeaderLayoutTypeProperty, value);
        }

        public Thickness TabPanelMargin
        {
            get => 
                (Thickness) base.GetValue(TabPanelMarginProperty);
            set => 
                base.SetValue(TabPanelMarginProperty, value);
        }

        protected override bool AllowsInvalidSelectedIndex =>
            true;

        private bool ShowTabHeaders
        {
            get => 
                (bool) base.GetValue(ShowTabHeadersProperty);
            set => 
                base.SetValue(ShowTabHeadersProperty, value);
        }

        private int VisiblePagesCount
        {
            get => 
                (int) base.GetValue(VisiblePagesCountProperty);
            set => 
                base.SetValue(VisiblePagesCountProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LayoutTabControl.<>c <>9 = new LayoutTabControl.<>c();
            public static Func<FrameworkElement, FrameworkElement> <>9__139_1;
            public static Action<FrameworkElement> <>9__139_2;
            public static Action<FrameworkElement> <>9__139_0;

            internal void <.cctor>b__26_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutTabControl) dObj).OnLayoutItemChanged((BaseLayoutItem) e.OldValue, (BaseLayoutItem) e.NewValue);
            }

            internal void <.cctor>b__26_1(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutTabControl) dObj).OnCaptionLocationChanged((CaptionLocation) e.NewValue);
            }

            internal void <.cctor>b__26_10(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutTabControl) dObj).OnActualTabCacheModeChanged((TabContentCacheMode) e.OldValue, (TabContentCacheMode) e.NewValue);
            }

            internal void <.cctor>b__26_11(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutTabControl) dObj).OnFixedMultiLineTabHeadersChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__26_12(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((LayoutTabControl) dObj).OnClipMarginChanged((Thickness) ea.OldValue, (Thickness) ea.NewValue);
            }

            internal object <.cctor>b__26_13(DependencyObject dObj, object value) => 
                ((LayoutTabControl) dObj).CoerceActualClipMargin((Thickness) value);

            internal void <.cctor>b__26_14(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((LayoutTabControl) dObj).OnTabPanelMarginChanged((Thickness) ea.OldValue, (Thickness) ea.NewValue);
            }

            internal object <.cctor>b__26_15(DependencyObject dObj, object value) => 
                ((LayoutTabControl) dObj).CoerceActualTabPanelMargin((Thickness) value);

            internal void <.cctor>b__26_16(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((LayoutTabControl) dObj).OnHasScrollChanged((bool) ea.OldValue, (bool) ea.NewValue);
            }

            internal void <.cctor>b__26_17(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((LayoutTabControl) dObj).OnShowTabHeadersChanged((bool) ea.NewValue);
            }

            internal void <.cctor>b__26_18(LayoutTabControl d, DockingViewStyle oldValue, DockingViewStyle newValue)
            {
                d.OnViewStyleChanged(oldValue, newValue);
            }

            internal void <.cctor>b__26_19(LayoutTabControl d, TabbedGroupDisplayMode oldValue, TabbedGroupDisplayMode newValue)
            {
                d.OnTabbedGroupDisplayModeChanged(oldValue, newValue);
            }

            internal void <.cctor>b__26_2(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutTabControl) dObj).OnCaptionOrientationChanged((Orientation) e.NewValue);
            }

            internal void <.cctor>b__26_20(LayoutTabControl d, bool oldValue, bool newValue)
            {
                d.OnAreTabHeadersVisibleChanged(oldValue, newValue);
            }

            internal void <.cctor>b__26_3(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutTabControl) dObj).OnTabHeaderLayoutTypeChanged((TabHeaderLayoutType) e.NewValue);
            }

            internal void <.cctor>b__26_4(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutTabControl) dObj).OnIsAutoFillHeadersChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__26_5(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutTabControl) dObj).OnScrollIndexChanged((int) e.NewValue);
            }

            internal void <.cctor>b__26_6(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutTabControl) dObj).OnShowTabForSinglePageChanged((bool) e.OldValue, (bool) e.NewValue);
            }

            internal void <.cctor>b__26_7(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutTabControl) dObj).OnVisiblePagesCountChanged((int) e.OldValue, (int) e.NewValue);
            }

            internal void <.cctor>b__26_8(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutTabControl) dObj).OnTabCacheModeChanged();
            }

            internal void <.cctor>b__26_9(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutTabControl) dObj).OnTabCacheModeChanged();
            }

            internal void <OnHasScrollChanged>b__139_0(FrameworkElement x)
            {
                x.InvalidateMeasure();
                Func<FrameworkElement, FrameworkElement> evaluator = <>9__139_1;
                if (<>9__139_1 == null)
                {
                    Func<FrameworkElement, FrameworkElement> local1 = <>9__139_1;
                    evaluator = <>9__139_1 = y => y.Parent as FrameworkElement;
                }
                Action<FrameworkElement> action = <>9__139_2;
                if (<>9__139_2 == null)
                {
                    Action<FrameworkElement> local2 = <>9__139_2;
                    action = <>9__139_2 = y => y.InvalidateMeasure();
                }
                x.With<FrameworkElement, FrameworkElement>(evaluator).Do<FrameworkElement>(action);
            }

            internal FrameworkElement <OnHasScrollChanged>b__139_1(FrameworkElement y) => 
                y.Parent as FrameworkElement;

            internal void <OnHasScrollChanged>b__139_2(FrameworkElement y)
            {
                y.InvalidateMeasure();
            }
        }
    }
}

