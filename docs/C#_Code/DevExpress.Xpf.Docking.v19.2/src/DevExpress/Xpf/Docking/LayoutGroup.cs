namespace DevExpress.Xpf.Docking
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.ModuleInjection;
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf.Bars.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking.Base;
    using DevExpress.Xpf.Docking.Internal;
    using DevExpress.Xpf.Docking.UIAutomation;
    using DevExpress.Xpf.Docking.VisualElements;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Markup;

    [ContentProperty("Items")]
    public class LayoutGroup : BaseLayoutItem, IGeneratorHost, ILogicalOwner, IInputElement, ISupportOriginalSerializableName, IItemContainer
    {
        public static readonly DependencyProperty IsLayoutRootProperty;
        private static readonly DependencyPropertyKey IsLayoutRootPropertyKey;
        public static readonly DependencyProperty HasAccentProperty;
        public static readonly DependencyProperty ControlItemsHostProperty;
        public static readonly DependencyProperty OrientationProperty;
        public static readonly DependencyProperty LayoutItemIntervalProperty;
        public static readonly DependencyProperty LayoutGroupIntervalProperty;
        public static readonly DependencyProperty DockItemIntervalProperty;
        private static readonly DependencyPropertyKey ActualLayoutItemIntervalPropertyKey;
        private static readonly DependencyPropertyKey ActualLayoutGroupIntervalPropertyKey;
        private static readonly DependencyPropertyKey ActualDockItemIntervalPropertyKey;
        public static readonly DependencyProperty ActualLayoutItemIntervalProperty;
        public static readonly DependencyProperty ActualLayoutGroupIntervalProperty;
        public static readonly DependencyProperty ActualDockItemIntervalProperty;
        public static readonly DependencyProperty DestroyContentOnTabSwitchingProperty;
        public static readonly DependencyProperty TabContentCacheModeProperty;
        public static readonly DependencyProperty AllowSplittersProperty;
        public static readonly DependencyProperty HasSingleItemProperty;
        private static readonly DependencyPropertyKey HasSingleItemPropertyKey;
        public static readonly DependencyProperty GroupBorderStyleProperty;
        private static readonly DependencyPropertyKey IsSplittersEnabledPropertyKey;
        public static readonly DependencyProperty IsSplittersEnabledProperty;
        public static readonly DependencyProperty AllowExpandProperty;
        public static readonly DependencyProperty ExpandedProperty;
        public static readonly DependencyProperty IsExpandedProperty;
        internal static readonly DependencyPropertyKey IsExpandedPropertyKey;
        public static readonly DependencyProperty GroupTemplateProperty;
        public static readonly DependencyProperty GroupTemplateSelectorProperty;
        public static readonly DependencyProperty ActualGroupTemplateSelectorProperty;
        private static readonly DependencyPropertyKey ActualGroupTemplateSelectorPropertyKey;
        public static readonly DependencyProperty DestroyOnClosingChildrenProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty HasNotCollapsedItemsProperty;
        private static readonly DependencyPropertyKey HasNotCollapsedItemsPropertyKey;
        public static readonly DependencyProperty HasVisibleItemsProperty;
        private static readonly DependencyPropertyKey HasVisibleItemsPropertyKey;
        public static readonly DependencyProperty CaptionOrientationProperty;
        public static readonly RoutedEvent SelectedItemChangedEvent;
        private static readonly DependencyPropertyKey SelectedItemPropertyKey;
        public static readonly DependencyProperty SelectedItemProperty;
        public static readonly DependencyProperty SelectedTabIndexProperty;
        public static readonly DependencyProperty TabHeaderLayoutTypeProperty;
        public static readonly DependencyProperty TabHeadersAutoFillProperty;
        public static readonly DependencyProperty TabHeaderHasScrollProperty;
        private static readonly DependencyPropertyKey TabHeaderHasScrollPropertyKey;
        public static readonly DependencyProperty TabHeaderScrollIndexProperty;
        private static readonly DependencyPropertyKey TabHeaderScrollIndexPropertyKey;
        public static readonly DependencyProperty TabHeaderMaxScrollIndexProperty;
        private static readonly DependencyPropertyKey TabHeaderMaxScrollIndexPropertyKey;
        public static readonly DependencyProperty TabHeaderCanScrollPrevProperty;
        private static readonly DependencyPropertyKey TabHeaderCanScrollPrevPropertyKey;
        public static readonly DependencyProperty TabHeaderCanScrollNextProperty;
        private static readonly DependencyPropertyKey TabHeaderCanScrollNextPropertyKey;
        public static readonly DependencyProperty FixedMultiLineTabHeadersProperty;
        public static readonly DependencyProperty IsAnimatedProperty;
        private static readonly DependencyPropertyKey IsAnimatedPropertyKey;
        public static readonly DependencyProperty ItemsAppearanceProperty;
        public static readonly DependencyProperty VisiblePagesCountProperty;
        private static readonly DependencyPropertyKey VisiblePagesCountPropertyKey;
        public static readonly DependencyProperty ItemsSourceProperty;
        public static readonly DependencyProperty ItemStyleProperty;
        public static readonly DependencyProperty ItemCaptionTemplateProperty;
        public static readonly DependencyProperty ItemContentTemplateProperty;
        public static readonly DependencyProperty ItemCaptionTemplateSelectorProperty;
        public static readonly DependencyProperty ItemContentTemplateSelectorProperty;
        public static readonly DependencyProperty TabItemContainerStyleProperty;
        public static readonly DependencyProperty TabItemContainerStyleSelectorProperty;
        public static readonly DependencyProperty LastChildFillProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty OwnerGroupProperty;
        public static readonly DependencyProperty AutoScrollOnOverflowProperty;
        public static readonly DependencyProperty ShowTabHeadersProperty;
        public static readonly DependencyProperty SelectionOnTabRemovalProperty;
        public static readonly DependencyProperty ItemTemplateProperty;
        public static readonly DependencyProperty ItemTemplateSelectorProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyPropertyKey IsTouchEnabledPropertyKey;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty IsTouchEnabledProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        protected internal static readonly DependencyProperty ArrangeAllCachedTabsProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        protected internal static readonly DependencyProperty TabbedGroupDisplayModeCoreProperty;
        internal int moveItemLock;
        protected bool fClearTemplateRequested;
        private readonly Locker clearContainerLocker;
        private readonly Locker containerGenerationLocker;
        private readonly WeakList<EventHandler> handlersWeakItemsChanged;
        private readonly WeakList<EventHandler> handlersWeakSelectedItemChanged;
        private readonly DelayedActionsHelper initilizedActionsHelper;
        private readonly RebuildQueryLocker rebuildQueryLocker;
        private readonly LockHelper SelectedTabIndexLocker;
        private readonly Locker setSelectionLocker;
        private Appearance _DefaultItemsAppearance;
        private Appearance actualItemsAppearance;
        private BindingExpressionBase DataContextBinding;
        private LockHelper isAnimatedLockHelper;
        private int itemsAppearanceUpdatesCount;
        private LockHelper itemTemplateLockHelper;
        private int lockItemsAppearanceUpdateCounter;
        private int lockLayoutChanging;
        private LockHelper selectedTabIndexLockHelper;
        private int serializableSelectedTabPageIndexCore;
        private int updateCount;

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

        public event EventHandler LayoutChanged;

        [Description("Fires when a new child item is selected within the current group. This event is supported for LayoutGroups representing its children as tabs.")]
        public event SelectedItemChangedEventHandler SelectedItemChanged
        {
            add
            {
                base.AddHandler(SelectedItemChangedEvent, value);
            }
            remove
            {
                base.RemoveHandler(SelectedItemChangedEvent, value);
            }
        }

        internal event EventHandler WeakItemsChanged
        {
            add
            {
                this.handlersWeakItemsChanged.Add(value);
            }
            remove
            {
                this.handlersWeakItemsChanged.Remove(value);
            }
        }

        internal event EventHandler WeakSelectedItemChanged
        {
            add
            {
                this.handlersWeakSelectedItemChanged.Add(value);
            }
            remove
            {
                this.handlersWeakSelectedItemChanged.Remove(value);
            }
        }

        static LayoutGroup()
        {
            DockingStrategyRegistrator.RegisterLayoutGroupStrategy();
            DependencyPropertyRegistrator<LayoutGroup> registrator = new DependencyPropertyRegistrator<LayoutGroup>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.OverrideMetadata<bool>(BaseLayoutItem.ShowCaptionProperty, false, null, null);
            registrator.OverrideMetadata<bool>(BaseLayoutItem.AllowFloatProperty, false, null, null);
            registrator.RegisterAttached<LayoutGroup>("OwnerGroup", ref OwnerGroupProperty, null, null, null);
            registrator.Register<System.Windows.Controls.Orientation>("Orientation", ref OrientationProperty, System.Windows.Controls.Orientation.Horizontal, (dObj, e) => ((LayoutGroup) dObj).OnOrientationChanged((System.Windows.Controls.Orientation) e.NewValue), null);
            registrator.Register<bool>("DestroyOnClosingChildren", ref DestroyOnClosingChildrenProperty, true, null, (CoerceValueCallback) ((dObj, value) => ((LayoutGroup) dObj).CoerceDestroyOnClosingChildren((bool) value)));
            registrator.RegisterReadonly<bool>("IsLayoutRoot", ref IsLayoutRootPropertyKey, ref IsLayoutRootProperty, false, (dObj, e) => ((LayoutGroup) dObj).OnIsLayoutRootChanged((bool) e.NewValue), (CoerceValueCallback) ((dObj, value) => ((LayoutGroup) dObj).CoerceIsLayoutRoot((bool) value)));
            bool? defValue = null;
            registrator.Register<bool?>("HasAccent", ref HasAccentProperty, defValue, (dObj, e) => ((LayoutGroup) dObj).OnHasAccentChanged((bool?) e.NewValue), (CoerceValueCallback) ((dObj, value) => ((LayoutGroup) dObj).CoerceHasAccent((bool?) value)));
            defValue = null;
            registrator.Register<bool?>("ControlItemsHost", ref ControlItemsHostProperty, defValue, (dObj, e) => ((LayoutGroup) dObj).OnControlItemsHostChanged((bool?) e.NewValue), null);
            registrator.Register<double>("LayoutItemInterval", ref LayoutItemIntervalProperty, double.NaN, (dObj, e) => ((LayoutGroup) dObj).OnLayoutItemIntervalChanged((double) e.NewValue), null);
            registrator.Register<double>("LayoutGroupInterval", ref LayoutGroupIntervalProperty, double.NaN, (dObj, e) => ((LayoutGroup) dObj).OnLayoutGroupIntervalChanged((double) e.NewValue), null);
            registrator.Register<double>("DockItemInterval", ref DockItemIntervalProperty, double.NaN, (dObj, e) => ((LayoutGroup) dObj).OnDockItemIntervalChanged((double) e.NewValue), null);
            registrator.RegisterReadonly<double>("ActualLayoutItemInterval", ref ActualLayoutItemIntervalPropertyKey, ref ActualLayoutItemIntervalProperty, 0.0, (dObj, e) => ((LayoutGroup) dObj).OnActualLayoutItemIntervalChanged((double) e.NewValue), (CoerceValueCallback) ((dObj, value) => ((LayoutGroup) dObj).CoerceActualLayoutItemInterval((double) value)));
            registrator.RegisterReadonly<double>("ActualLayoutGroupInterval", ref ActualLayoutGroupIntervalPropertyKey, ref ActualLayoutGroupIntervalProperty, 0.0, (dObj, e) => ((LayoutGroup) dObj).OnActualLayoutGroupIntervalChanged((double) e.NewValue), (CoerceValueCallback) ((dObj, value) => ((LayoutGroup) dObj).CoerceActualLayoutGroupInterval((double) value)));
            registrator.RegisterReadonly<double>("ActualDockItemInterval", ref ActualDockItemIntervalPropertyKey, ref ActualDockItemIntervalProperty, 0.0, (dObj, e) => ((LayoutGroup) dObj).OnActualDockItemIntervalChanged((double) e.NewValue), (CoerceValueCallback) ((dObj, value) => ((LayoutGroup) dObj).CoerceActualDockItemInterval((double) value)));
            registrator.Register<bool>("DestroyContentOnTabSwitching", ref DestroyContentOnTabSwitchingProperty, true, null, null);
            registrator.Register<DevExpress.Xpf.Core.TabContentCacheMode>("TabContentCacheMode", ref TabContentCacheModeProperty, DevExpress.Xpf.Core.TabContentCacheMode.None, null, null);
            defValue = null;
            registrator.Register<bool?>("AllowSplitters", ref AllowSplittersProperty, defValue, (dObj, e) => ((LayoutGroup) dObj).OnAllowSplittersChanged((bool?) e.NewValue), (CoerceValueCallback) ((dObj, value) => ((LayoutGroup) dObj).CoerceAllowSplitters((bool?) value)));
            registrator.RegisterReadonly<bool>("IsSplittersEnabled", ref IsSplittersEnabledPropertyKey, ref IsSplittersEnabledProperty, true, null, null);
            registrator.Register<DevExpress.Xpf.Docking.GroupBorderStyle>("GroupBorderStyle", ref GroupBorderStyleProperty, DevExpress.Xpf.Docking.GroupBorderStyle.NoBorder, (dObj, e) => ((LayoutGroup) dObj).OnGroupBorderStyleChanged((DevExpress.Xpf.Docking.GroupBorderStyle) e.NewValue), (CoerceValueCallback) ((dObj, value) => ((LayoutGroup) dObj).CoerceGroupBorderStyle((DevExpress.Xpf.Docking.GroupBorderStyle) value)));
            registrator.Register<bool>("AllowExpand", ref AllowExpandProperty, true, (dObj, e) => ((LayoutGroup) dObj).OnAllowExpandChanged((bool) e.NewValue), (CoerceValueCallback) ((dObj, value) => ((LayoutGroup) dObj).CoerceAllowExpand((bool) value)));
            registrator.Register<bool>("Expanded", ref ExpandedProperty, true, (dObj, e) => ((LayoutGroup) dObj).OnExpandedChanged((bool) e.NewValue), null);
            registrator.RegisterReadonly<bool>("IsExpanded", ref IsExpandedPropertyKey, ref IsExpandedProperty, true, (dObj, e) => ((LayoutGroup) dObj).OnIsExpandedChanged((bool) e.NewValue), (CoerceValueCallback) ((dObj, value) => ((LayoutGroup) dObj).CoerceIsExpanded((bool) value)));
            registrator.RegisterReadonly<bool>("HasSingleItem", ref HasSingleItemPropertyKey, ref HasSingleItemProperty, false, (dObj, e) => ((LayoutGroup) dObj).OnHasSingleItemChanged((bool) e.NewValue), null);
            registrator.Register<DataTemplate>("GroupTemplate", ref GroupTemplateProperty, null, (dObj, e) => ((LayoutGroup) dObj).OnGroupTemplateChanged(), null);
            registrator.Register<DataTemplateSelector>("GroupTemplateSelector", ref GroupTemplateSelectorProperty, new DefaultTemplateSelector(), (dObj, e) => ((LayoutGroup) dObj).OnGroupTemplateChanged(), null);
            registrator.RegisterReadonly<DataTemplateSelector>("ActualGroupTemplateSelector", ref ActualGroupTemplateSelectorPropertyKey, ref ActualGroupTemplateSelectorProperty, null, null, null);
            registrator.RegisterReadonly<bool>("HasNotCollapsedItems", ref HasNotCollapsedItemsPropertyKey, ref HasNotCollapsedItemsProperty, false, (dObj, ea) => ((LayoutGroup) dObj).OnHasNotCollapsedItemsChanged((bool) ea.NewValue), (CoerceValueCallback) ((dObj, value) => ((LayoutGroup) dObj).CoerceHasNotCollapsedItems((bool) value)));
            registrator.RegisterReadonly<bool>("HasVisibleItems", ref HasVisibleItemsPropertyKey, ref HasVisibleItemsProperty, false, null, (CoerceValueCallback) ((dObj, value) => ((LayoutGroup) dObj).CoerceHasVisibleItems((bool) value)));
            registrator.Register<System.Windows.Controls.Orientation>("CaptionOrientation", ref CaptionOrientationProperty, System.Windows.Controls.Orientation.Horizontal, null, null);
            registrator.RegisterReadonly<BaseLayoutItem>("SelectedItem", ref SelectedItemPropertyKey, ref SelectedItemProperty, null, (dObj, ea) => ((LayoutGroup) dObj).OnSelectedItemChanged((BaseLayoutItem) ea.NewValue, (BaseLayoutItem) ea.OldValue), (CoerceValueCallback) ((dObj, value) => ((LayoutGroup) dObj).CoerceSelectedItem((BaseLayoutItem) value)));
            registrator.Register<int>("SelectedTabIndex", ref SelectedTabIndexProperty, -1, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (dObj, ea) => ((LayoutGroup) dObj).OnSelectedTabIndexChanged((int) ea.NewValue, (int) ea.OldValue), (dObj, value) => ((LayoutGroup) dObj).CoerceSelectedTabIndex((int) value));
            registrator.Register<DevExpress.Xpf.Layout.Core.TabHeaderLayoutType>("TabHeaderLayoutType", ref TabHeaderLayoutTypeProperty, DevExpress.Xpf.Layout.Core.TabHeaderLayoutType.Default, (dObj, ea) => ((LayoutGroup) dObj).OnTabHeaderLayoutTypeChanged((DevExpress.Xpf.Layout.Core.TabHeaderLayoutType) ea.NewValue), (CoerceValueCallback) ((dObj, value) => ((LayoutGroup) dObj).CoerceTabHeaderLayoutType((DevExpress.Xpf.Layout.Core.TabHeaderLayoutType) value)));
            registrator.Register<bool>("TabHeadersAutoFill", ref TabHeadersAutoFillProperty, false, (dObj, ea) => ((LayoutGroup) dObj).OnTabHeadersAutoFillChanged((bool) ea.NewValue), (CoerceValueCallback) ((dObj, value) => ((LayoutGroup) dObj).CoerceTabHeadersAutoFill((bool) value)));
            registrator.RegisterReadonly<bool>("TabHeaderHasScroll", ref TabHeaderHasScrollPropertyKey, ref TabHeaderHasScrollProperty, false, (dObj, ea) => ((LayoutGroup) dObj).OnTabHeaderHasScrollChanged((bool) ea.NewValue), null);
            registrator.RegisterReadonly<int>("TabHeaderScrollIndex", ref TabHeaderScrollIndexPropertyKey, ref TabHeaderScrollIndexProperty, 0, null, (CoerceValueCallback) ((dObj, value) => ((LayoutGroup) dObj).CoerceTabHeaderScrollIndex((int) value)));
            registrator.RegisterReadonly<int>("TabHeaderMaxScrollIndex", ref TabHeaderMaxScrollIndexPropertyKey, ref TabHeaderMaxScrollIndexProperty, -1, null, (CoerceValueCallback) ((dObj, value) => ((LayoutGroup) dObj).CoerceTabHeaderMaxScrollIndex((int) value)));
            registrator.RegisterReadonly<bool>("TabHeaderCanScrollNext", ref TabHeaderCanScrollNextPropertyKey, ref TabHeaderCanScrollNextProperty, false, null, null);
            registrator.RegisterReadonly<bool>("TabHeaderCanScrollPrev", ref TabHeaderCanScrollPrevPropertyKey, ref TabHeaderCanScrollPrevProperty, false, null, null);
            registrator.Register<bool>("FixedMultiLineTabHeaders", ref FixedMultiLineTabHeadersProperty, false, null, null);
            registrator.RegisterReadonly<bool>("IsAnimated", ref IsAnimatedPropertyKey, ref IsAnimatedProperty, false, (dObj, e) => ((LayoutGroup) dObj).OnIsAnimatedChanged((bool) e.OldValue, (bool) e.NewValue), null);
            registrator.Register<Appearance>("ItemsAppearance", ref ItemsAppearanceProperty, null, (dObj, e) => ((LayoutGroup) dObj).OnItemsAppearanceChanged((Appearance) e.NewValue), (dObj, value) => ((LayoutGroup) dObj).CoerceItemsAppearance((Appearance) value));
            registrator.RegisterReadonly<int>("VisiblePagesCount", ref VisiblePagesCountPropertyKey, ref VisiblePagesCountProperty, 0, null, (CoerceValueCallback) ((dObj, value) => ((LayoutGroup) dObj).CoerceVisiblePagesCount((int) value)));
            registrator.Register<IEnumerable>("ItemsSource", ref ItemsSourceProperty, null, (dObj, e) => ((LayoutGroup) dObj).OnItemsSourceChanged((IEnumerable) e.NewValue, (IEnumerable) e.OldValue), null);
            registrator.Register<Style>("ItemStyle", ref ItemStyleProperty, null, (dObj, e) => ((LayoutGroup) dObj).OnItemStyleChanged((Style) e.NewValue, (Style) e.OldValue), null);
            registrator.Register<Style>("TabItemContainerStyle", ref TabItemContainerStyleProperty, null, null, null);
            registrator.Register<StyleSelector>("TabItemContainerStyleSelector", ref TabItemContainerStyleSelectorProperty, null, null, null);
            registrator.Register<DataTemplate>("ItemCaptionTemplate", ref ItemCaptionTemplateProperty, null, (dObj, e) => ((LayoutGroup) dObj).OnItemTemplatePropertyChanged(), null);
            registrator.Register<DataTemplate>("ItemContentTemplate", ref ItemContentTemplateProperty, null, (dObj, e) => ((LayoutGroup) dObj).OnItemTemplatePropertyChanged(), null);
            registrator.Register<bool>("LastChildFill", ref LastChildFillProperty, true, null, null);
            registrator.Register<DataTemplateSelector>("ItemCaptionTemplateSelector", ref ItemCaptionTemplateSelectorProperty, null, (dObj, e) => ((LayoutGroup) dObj).OnItemTemplatePropertyChanged(), null);
            registrator.Register<DataTemplateSelector>("ItemContentTemplateSelector", ref ItemContentTemplateSelectorProperty, null, (dObj, e) => ((LayoutGroup) dObj).OnItemTemplatePropertyChanged(), null);
            registrator.Register<DevExpress.Xpf.Docking.AutoScrollOnOverflow>("AutoScrollOnOverflow", ref AutoScrollOnOverflowProperty, DevExpress.Xpf.Docking.AutoScrollOnOverflow.AnyItem, null, null);
            registrator.Register<bool>("ShowTabHeaders", ref ShowTabHeadersProperty, true, null, null);
            registrator.Register<DevExpress.Xpf.Docking.SelectionOnTabRemoval>("SelectionOnTabRemoval", ref SelectionOnTabRemovalProperty, DevExpress.Xpf.Docking.SelectionOnTabRemoval.PreviousSelection, null, null);
            registrator.Register<bool>("ArrangeAllCachedTabs", ref ArrangeAllCachedTabsProperty, false, null, null);
            registrator.Register<TabbedGroupDisplayMode>("TabbedGroupDisplayModeCore", ref TabbedGroupDisplayModeCoreProperty, TabbedGroupDisplayMode.Default, null, null);
            registrator.Register<bool>("IsTouchEnabled", ref IsTouchEnabledProperty, false, (d, e) => ((LayoutGroup) d).OnIsTouchEnabledChanged((bool) e.OldValue, (bool) e.NewValue), null);
            registrator.Register<DataTemplate>("ItemTemplate", ref ItemTemplateProperty, null, (dObj, e) => ((LayoutGroup) dObj).OnItemTemplatePropertyChanged(), null);
            registrator.Register<DataTemplateSelector>("ItemTemplateSelector", ref ItemTemplateSelectorProperty, null, (dObj, e) => ((LayoutGroup) dObj).OnItemTemplatePropertyChanged(), null);
            try
            {
                IsTouchEnabledPropertyKey = typeof(ThemeManager).GetField("IsTouchEnabledPropertyKey", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static).GetValue(null) as DependencyPropertyKey;
            }
            catch
            {
            }
            SelectedItemChangedEvent = EventManager.RegisterRoutedEvent("SelectedItemChanged", RoutingStrategy.Direct, typeof(SelectedItemChangedEventHandler), typeof(LayoutGroup));
        }

        public LayoutGroup()
        {
            this.clearContainerLocker = new Locker();
            this.containerGenerationLocker = new Locker();
            this.handlersWeakItemsChanged = new WeakList<EventHandler>();
            this.handlersWeakSelectedItemChanged = new WeakList<EventHandler>();
            this.initilizedActionsHelper = new DelayedActionsHelper();
            this.SelectedTabIndexLocker = new LockHelper();
            this.setSelectionLocker = new Locker();
            this.serializableSelectedTabPageIndexCore = -1;
            this.rebuildQueryLocker = new RebuildQueryLocker(this);
            Binding binding = new Binding();
            binding.Path = new PropertyPath(ThemeManager.IsTouchEnabledProperty);
            binding.Source = this;
            BindingOperations.SetBinding(this, IsTouchEnabledProperty, binding);
            base.CoerceValue(BaseLayoutItem.IsCaptionVisibleProperty);
            base.CoerceValue(DestroyOnClosingChildrenProperty);
            base.CoerceValue(ActualLayoutItemIntervalProperty);
            base.CoerceValue(ActualLayoutGroupIntervalProperty);
            base.CoerceValue(ActualDockItemIntervalProperty);
            base.CoerceValue(AllowExpandProperty);
            if (!base.IsInDesignTime)
            {
                base.CoerceValue(TabHeaderLayoutTypeProperty);
            }
        }

        internal LayoutGroup(params BaseLayoutItem[] items) : this()
        {
            this.Add(items);
        }

        public override void Accept(IVisitor<BaseLayoutItem> visitor)
        {
            base.Accept(visitor);
            this.AcceptItems(new VisitDelegate<BaseLayoutItem>(visitor.Visit));
        }

        public override void Accept(VisitDelegate<BaseLayoutItem> visit)
        {
            base.Accept(visit);
            this.AcceptItems(visit);
        }

        private void AcceptItems(VisitDelegate<BaseLayoutItem> visit)
        {
            foreach (BaseLayoutItem item in this.Items)
            {
                item.Accept(visit);
            }
        }

        public void Add(BaseLayoutItem item)
        {
            this.Items.Add(item);
        }

        public void Add(params BaseLayoutItem[] items)
        {
            this.AddRange(items);
        }

        protected internal virtual void AddContainer(BaseLayoutItem item)
        {
            this.AddContainerCore(item);
        }

        protected internal virtual void AddContainer(ContentItem item)
        {
            this.AddContainerCore(item);
        }

        internal void AddContainer(int index, BaseLayoutItem item)
        {
            this.AddContainerCore(index, item);
        }

        private void AddContainerCore(BaseLayoutItem container)
        {
            LayoutGroup containerHost = this.GetContainerHost(container);
            if ((containerHost.Manager != null) && !containerHost.IsInTree())
            {
                containerHost.Manager.DockControllerImpl.RestoreGroupAndInsertItem(this.PlaceHolderHelper.Count, container, containerHost);
            }
            else
            {
                containerHost.Add(container);
            }
        }

        private void AddContainerCore(int index, BaseLayoutItem container)
        {
            LayoutGroup containerHost = this.GetContainerHost(container);
            if ((containerHost.Manager != null) && !containerHost.IsInTree())
            {
                containerHost.Manager.DockControllerImpl.RestoreGroupAndInsertItem(index, container, containerHost);
            }
            else
            {
                containerHost.Insert(index, container);
            }
        }

        protected void AddItemInItemsInternal(BaseLayoutItem item, int index)
        {
            if (!this.ItemsInternal.Contains(item))
            {
                int indexInItemsInternal = this.GetIndexInItemsInternal(index);
                if (indexInItemsInternal >= this.ItemsInternal.Count)
                {
                    this.ItemsInternal.Add(item);
                }
                else
                {
                    this.ItemsInternal.Insert(indexInItemsInternal, item);
                }
                if (((indexInItemsInternal - 1) >= 0) && !(this.ItemsInternal[indexInItemsInternal - 1] is Splitter))
                {
                    Splitter splitter = this.CreateSplitterItem(this);
                    if (!this.IsSplittersEnabled)
                    {
                        splitter.IsEnabled = false;
                    }
                    this.ItemsInternal.Insert(indexInItemsInternal, splitter);
                }
                else if (((indexInItemsInternal + 1) <= (this.ItemsInternal.Count - 1)) && !(this.ItemsInternal[indexInItemsInternal + 1] is Splitter))
                {
                    Splitter splitter2 = this.CreateSplitterItem(this);
                    if (!this.IsSplittersEnabled)
                    {
                        splitter2.IsEnabled = false;
                    }
                    this.ItemsInternal.Insert(indexInItemsInternal + 1, splitter2);
                }
            }
        }

        protected void AddLogicalChildren(IEnumerable children)
        {
            if (this.CanAddLogicalChild && (children != null))
            {
                foreach (DependencyObject obj2 in children)
                {
                    Func<BaseLayoutItem, bool> evaluator = <>c.<>9__428_0;
                    if (<>c.<>9__428_0 == null)
                    {
                        Func<BaseLayoutItem, bool> local1 = <>c.<>9__428_0;
                        evaluator = <>c.<>9__428_0 = x => x.SupportsOptimizedLogicalTree;
                    }
                    if ((obj2 as BaseLayoutItem).Return<BaseLayoutItem, bool>(evaluator, (<>c.<>9__428_1 ??= () => false)) && ((base.DockLayoutManagerCore != null) && base.DockLayoutManagerCore.OptimizedLogicalTree))
                    {
                        DockLayoutManager.AddLogicalChild(base.DockLayoutManagerCore, obj2);
                    }
                    else
                    {
                        DependencyObject parent = LogicalTreeHelper.GetParent(obj2);
                        if (parent != null)
                        {
                            if (!(parent is ILogicalOwner) || ReferenceEquals(parent, this))
                            {
                                continue;
                            }
                            ((ILogicalOwner) parent).RemoveChild(obj2);
                        }
                        base.AddLogicalChild(obj2);
                    }
                }
            }
        }

        public void AddRange(BaseLayoutItem[] items)
        {
            using (new UpdateBatch(this.GetDockLayoutManager()))
            {
                Array.ForEach<BaseLayoutItem>(items, new Action<BaseLayoutItem>(this.Add));
            }
        }

        private void AddToPinned(LayoutPanel panel)
        {
            this.RemoveFromPinned(panel);
            bool flag2 = panel.TabPinLocation == TabHeaderPinLocation.Far;
            if (panel.TabPinLocation != TabHeaderPinLocation.Far)
            {
                this.PinnedLeftItems.Add(panel);
            }
            if (flag2)
            {
                this.PinnedRightItems.Add(panel);
            }
        }

        protected internal virtual void AfterItemAdded(int index, BaseLayoutItem item)
        {
            this.PlaceHolderHelper.InsertItem(index, item);
            this.UpdateIsTouchEnabled(item, this.IsTouchEnabled);
        }

        protected internal virtual void AfterItemRemoved(BaseLayoutItem item)
        {
            item.DockLayoutManagerCore = null;
            this.PlaceHolderHelper.Remove(item);
            this.ClearIsTouchEnabled(item);
        }

        internal override void ApplySerializationInfo()
        {
            base.ApplySerializationInfo();
            this.IsAutoGenerated = !((LayoutGroupSerializationInfo) base.SerializationInfo).IsUserDefined;
        }

        protected internal virtual void BeforeItemAdded(BaseLayoutItem item)
        {
            item.DockLayoutManagerCore = base.DockLayoutManagerCore;
        }

        protected virtual void BeforeItemRemoved(BaseLayoutItem item)
        {
            item.TabIndexBeforeRemove = this.TabIndexFromItem(item);
        }

        private void BeginContainerGeneration()
        {
            this.BeginUpdate();
            this.containerGenerationLocker.Lock();
        }

        public override void BeginInit()
        {
            base.BeginInit();
            this.BeginUpdate();
        }

        internal void BeginUpdate()
        {
            this.updateCount++;
        }

        private Size CalcGroupMinSize(Size[] minSizes) => 
            MathHelper.MeasureMinSize(minSizes);

        protected virtual Size CalcGroupMinSize(Size[] minSizes, bool fHorz)
        {
            Size minSize = MathHelper.MeasureMinSize(minSizes, fHorz);
            minSize = this.CalcMinSizeWitnIntervals(fHorz, minSize);
            return this.CalcMinSizeWitnMargins(minSize, base.ActualMargin);
        }

        private bool CalcIsLayoutRoot() => 
            base.IsControlItemsHost || this.Items.ContainsNestedControlItemHostItems();

        protected override Size CalcMaxSizeValue(Size value)
        {
            Size[] sizeArray;
            Size[] sizeArray2;
            this.Items.CollectConstraints(out sizeArray, out sizeArray2);
            bool fHorz = this.Orientation == System.Windows.Controls.Orientation.Horizontal;
            return MathHelper.MeasureSize(this.IgnoreOrientation ? this.CalcGroupMinSize(sizeArray) : this.CalcGroupMinSize(sizeArray, fHorz), MathHelper.MeasureMaxSize(sizeArray2, fHorz), value);
        }

        protected override Size CalcMinSizeValue(Size value)
        {
            Size[] sizeArray;
            Size[] sizeArray2;
            if (!this.IsExpanded)
            {
                return value;
            }
            this.Items.CollectConstraints(out sizeArray, out sizeArray2);
            bool fHorz = this.Orientation == System.Windows.Controls.Orientation.Horizontal;
            Size size = this.IgnoreOrientation ? this.CalcGroupMinSize(sizeArray) : this.CalcGroupMinSize(sizeArray, fHorz);
            return new Size(Math.Max(size.Width, value.Width), Math.Max(size.Height, value.Height));
        }

        private Size CalcMinSizeWitnIntervals(bool fHorz, Size minSize)
        {
            double intervals = this.GetIntervals();
            return new Size(fHorz ? (minSize.Width + intervals) : minSize.Width, fHorz ? minSize.Height : (minSize.Height + intervals));
        }

        private Size CalcMinSizeWitnMargins(Size minSize, Thickness margin) => 
            new Size((margin.Left + minSize.Width) + margin.Right, (margin.Top + minSize.Height) + margin.Bottom);

        protected virtual bool CanCreateItemsInternal() => 
            true;

        protected internal virtual bool CanShowItemsInSelectorMenu() => 
            (base.ItemType != LayoutItemType.Group) && ((base.ItemType != LayoutItemType.FloatGroup) && this.IsExpanded);

        public void Clear()
        {
            BaseLayoutItem[] itemArray = this.Items.ToArray<BaseLayoutItem>();
            for (int i = 0; i < itemArray.Length; i++)
            {
                this.Remove(itemArray[i]);
            }
        }

        protected virtual void ClearContainerCore(object item, BaseLayoutItem container)
        {
            IItemContainer container1 = container as IItemContainer;
            if (container1 == null)
            {
                IItemContainer local1 = container1;
            }
            else
            {
                container1.ClearContainer(item);
            }
        }

        protected internal virtual void ClearContainerForItem(BaseLayoutItem container, object item)
        {
            if (container != null)
            {
                using (this.clearContainerLocker.Lock())
                {
                    LayoutPanel panel = container as LayoutPanel;
                    if (panel != null)
                    {
                        Func<DockLayoutManager, IDockController> evaluator = <>c.<>9__410_0;
                        if (<>c.<>9__410_0 == null)
                        {
                            Func<DockLayoutManager, IDockController> local1 = <>c.<>9__410_0;
                            evaluator = <>c.<>9__410_0 = x => x.DockController;
                        }
                        base.Manager.With<DockLayoutManager, IDockController>(evaluator).Do<IDockController>(x => x.RemovePanel(panel));
                    }
                    container.Parent.Do<LayoutGroup>(x => x.Remove(container));
                    this.ClearContainerCore(item, container);
                }
            }
        }

        private void ClearIsTouchEnabled(DependencyObject target)
        {
            if (IsTouchEnabledPropertyKey != null)
            {
                target.ClearValue(IsTouchEnabledPropertyKey);
            }
        }

        protected internal override void ClearTemplate()
        {
            if (!this.fClearTemplateRequested)
            {
                this.fClearTemplateRequested = true;
                base.Dispatcher.BeginInvoke(delegate {
                    if (this.fClearTemplateRequested)
                    {
                        this.ClearTemplateCore();
                    }
                }, new object[0]);
                if (base.VisualParent == null)
                {
                    base.ClearValue(DockLayoutManager.UIScopeProperty);
                }
            }
        }

        protected internal override void ClearTemplateCore()
        {
            this.fClearTemplateRequested = false;
            base.ClearTemplateCore();
        }

        protected virtual double CoerceActualDockItemInterval(double value) => 
            double.IsNaN(this.DockItemInterval) ? (((base.DockingViewStyle != DockingViewStyle.Light) || ((this is FloatGroup) && this.HasSingleItem)) ? ((this.Orientation == System.Windows.Controls.Orientation.Horizontal) ? DockLayoutManagerParameters.DockingItemIntervalHorz : DockLayoutManagerParameters.DockingItemIntervalVert) : 1.0) : this.DockItemInterval;

        protected virtual double CoerceActualLayoutGroupInterval(double value) => 
            double.IsNaN(this.LayoutGroupInterval) ? ((this.Orientation == System.Windows.Controls.Orientation.Horizontal) ? DockLayoutManagerParameters.LayoutGroupIntervalHorz : DockLayoutManagerParameters.LayoutGroupIntervalVert) : this.LayoutGroupInterval;

        protected virtual double CoerceActualLayoutItemInterval(double value) => 
            double.IsNaN(this.LayoutItemInterval) ? ((this.Orientation == System.Windows.Controls.Orientation.Horizontal) ? DockLayoutManagerParameters.LayoutItemIntervalHorz : DockLayoutManagerParameters.LayoutItemIntervalVert) : this.LayoutItemInterval;

        protected override Thickness CoerceActualMargin(Thickness value)
        {
            if (!MathHelper.AreEqual(base.Margin, new Thickness(double.NaN)))
            {
                return base.Margin;
            }
            if (this.IsControlItemsRoot() && (this.GroupBorderStyle == DevExpress.Xpf.Docking.GroupBorderStyle.NoBorder))
            {
                return DockLayoutManagerParameters.LayoutRootMargin;
            }
            if (this.IsDockingItemsRoot() && (this.GroupBorderStyle == DevExpress.Xpf.Docking.GroupBorderStyle.NoBorder))
            {
                if (this.IsMDIItemsRoot())
                {
                    return new Thickness();
                }
                if ((base.DockingViewStyle != DockingViewStyle.Light) || ((this is FloatGroup) && this.HasSingleItem))
                {
                    return DockLayoutManagerParameters.DockingRootMargin;
                }
            }
            return new Thickness();
        }

        protected virtual bool CoerceAllowExpand(bool value) => 
            (this.GroupBorderStyle == DevExpress.Xpf.Docking.GroupBorderStyle.GroupBox) & value;

        protected virtual bool? CoerceAllowSplitters(bool? value)
        {
            LayoutGroup root = this.GetRoot();
            DockLayoutManager manager = this.FindDockLayoutManager();
            return (((manager == null) || !manager.IsCustomization) ? ((value == null) ? (((root == null) || root.Items.ContainsLayoutControlItemOrGroup()) ? ((bool?) false) : ((bool?) true)) : value) : ((bool?) true));
        }

        protected override string CoerceCaptionFormat(string captionFormat) => 
            string.IsNullOrEmpty(captionFormat) ? DockLayoutManagerParameters.LayoutGroupCaptionFormat : captionFormat;

        protected virtual bool CoerceDestroyOnClosingChildren(bool value) => 
            (this.GroupBorderStyle == DevExpress.Xpf.Docking.GroupBorderStyle.NoBorder) & value;

        protected virtual DevExpress.Xpf.Docking.GroupBorderStyle CoerceGroupBorderStyle(DevExpress.Xpf.Docking.GroupBorderStyle value) => 
            (this.IsInitializedAsDockingElement || base.IsTabPage) ? DevExpress.Xpf.Docking.GroupBorderStyle.NoBorder : value;

        protected virtual bool? CoerceHasAccent(bool? value) => 
            (value == null) ? new bool?(this.GroupBorderStyle != DevExpress.Xpf.Docking.GroupBorderStyle.NoBorder) : value;

        protected virtual bool CoerceHasNotCollapsedItems(bool value) => 
            VisibilityHelper.ContainsNotCollapsedItems(this);

        protected virtual bool CoerceHasVisibleItems(bool hasVisibleItems) => 
            VisibilityHelper.HasVisibleItems(this);

        protected override GridLength CoerceHeight(GridLength value) => 
            (!value.IsStar || this.IsExpanded) ? base.CoerceHeight(value) : new GridLength(1.0, GridUnitType.Auto);

        protected override bool CoerceIsCloseButtonVisible(bool visible) => 
            false;

        protected override bool CoerceIsControlItemsHost(bool value) => 
            (this.ControlItemsHost == null) ? ((this.ParentPanel == null) ? ((((base.Parent != null) && base.Parent.IsControlItemsHost) | value) || this.Items.ContainsLayoutControlItem()) : true) : this.ControlItemsHost.Value;

        protected virtual bool CoerceIsExpanded(bool value) => 
            this.AllowExpand ? this.Expanded : value;

        protected virtual bool CoerceIsLayoutRoot(bool value) => 
            (base.Parent == null) && this.CalcIsLayoutRoot();

        protected override bool CoerceIsScrollNextButtonVisible(bool visible) => 
            this.HasTabHeader() && (this.HasScrollableHeader() && this.ShowScrollNextButton);

        protected override bool CoerceIsScrollPrevButtonVisible(bool visible) => 
            this.HasTabHeader() && (this.HasScrollableHeader() && this.ShowScrollPrevButton);

        protected virtual object CoerceItemsAppearance(Appearance value) => 
            value ?? this.DefaultItemsAppearance;

        protected virtual BaseLayoutItem CoerceSelectedItem(BaseLayoutItem item) => 
            this.IsTabHost ? (this.IsValid(this.SelectedTabIndex) ? this.Items[this.SelectedTabIndex] : null) : item;

        protected virtual object CoerceSelectedTabIndex(int index)
        {
            if (!this.IsTabHost)
            {
                return index;
            }
            if (this.setSelectionLocker)
            {
                return -1;
            }
            if (this.IsValid(index) && ((this.Items[index].Visibility == Visibility.Visible) && (this.moveItemLock == 0)))
            {
                return index;
            }
            BaseLayoutItem item = ((this.SelectedItem == null) || (this.SelectedItem.Visibility != Visibility.Visible)) ? null : this.SelectedItem;
            return (((item == null) || !this.Items.Contains(item)) ? ((this.VisiblePagesCount > 0) ? this.Items.IndexOf(this.VisiblePages[0]) : -1) : this.Items.IndexOf(item));
        }

        protected virtual DevExpress.Xpf.Layout.Core.TabHeaderLayoutType CoerceTabHeaderLayoutType(DevExpress.Xpf.Layout.Core.TabHeaderLayoutType value) => 
            (value == DevExpress.Xpf.Layout.Core.TabHeaderLayoutType.Default) ? DevExpress.Xpf.Layout.Core.TabHeaderLayoutType.Trim : value;

        protected virtual int CoerceTabHeaderMaxScrollIndex(int index) => 
            (index != -1) ? index : this.Items.Count;

        protected virtual bool CoerceTabHeadersAutoFill(bool value) => 
            value;

        protected virtual int CoerceTabHeaderScrollIndex(int index) => 
            index;

        protected virtual int CoerceVisiblePagesCount(int value) => 
            this.VisiblePages.Count;

        internal override void CollectSerializationInfo()
        {
            base.CollectSerializationInfo();
            ((LayoutGroupSerializationInfo) base.SerializationInfo).IsUserDefined = !this.IsAutoGenerated;
        }

        internal bool ContainsPinnedItem(LayoutPanel panel) => 
            this.PinnedLeftItems.Contains(panel) || this.PinnedRightItems.Contains(panel);

        internal virtual ContentItem CreateContentItem(object content) => 
            !base.IsControlItemsHost ? ((base.Manager != null) ? ((ContentItem) base.Manager.CreateLayoutPanel()) : ((ContentItem) new LayoutPanel())) : ((base.Manager != null) ? ((ContentItem) base.Manager.CreateLayoutControlItem()) : ((ContentItem) new LayoutControlItem()));

        protected virtual DataTemplateSelector CreateDefaultItemTemplateSelector() => 
            new DefaultItemTemplateSelectorWrapper(this.GroupTemplateSelector, this.GroupTemplate);

        private BaseLayoutItem CreateItem(object item, DataTemplate itemTemplate, DataTemplateSelector itemTemplateSelector)
        {
            DataTemplateSelector selector = itemTemplateSelector;
            DataTemplate template = (selector != null) ? selector.SelectTemplate(item, this) : itemTemplate;
            if (template == null)
            {
                return null;
            }
            BaseLayoutItem content = null;
            DependencyObject obj2 = template.LoadContent();
            if (obj2 != null)
            {
                if (obj2 is BaseLayoutItem)
                {
                    content = (BaseLayoutItem) obj2;
                }
                else if (obj2 is ContentControl)
                {
                    content = (BaseLayoutItem) ((ContentControl) obj2).Content;
                    ((ContentControl) obj2).Content = null;
                }
                else if (obj2 is ContentPresenter)
                {
                    content = (BaseLayoutItem) ((ContentPresenter) obj2).Content;
                    ((ContentPresenter) obj2).Content = null;
                }
            }
            return content;
        }

        protected virtual BaseLayoutItemCollection CreateItems() => 
            new BaseLayoutItemCollection(this);

        protected virtual ObservableCollection<object> CreateItemsInternal() => 
            new ObservableCollection<object>();

        private DevExpress.Xpf.Docking.Internal.PlaceHolderHelper CreatePlaceHolderHelper() => 
            new DevExpress.Xpf.Docking.Internal.PlaceHolderHelper(this);

        protected override BaseLayoutItemSerializationInfo CreateSerializationInfo() => 
            new LayoutGroupSerializationInfo(this);

        protected virtual Splitter CreateSplitterItem(LayoutGroup group) => 
            new Splitter(group);

        void ILogicalOwner.AddChild(object child)
        {
            base.AddLogicalChild(child);
        }

        void ILogicalOwner.RemoveChild(object child)
        {
            base.RemoveLogicalChild(child);
        }

        void IGeneratorHost.ClearContainer(DependencyObject container, object item)
        {
            this.ClearContainerForItem(container as BaseLayoutItem, item);
        }

        DependencyObject IGeneratorHost.GenerateContainerForItem(object item, DataTemplate itemTemplate, DataTemplateSelector itemTemplateSelector) => 
            this.GenerateContainerForItem(item, itemTemplate, itemTemplateSelector);

        DependencyObject IGeneratorHost.GenerateContainerForItem(object item, int index, DataTemplate itemTemplate, DataTemplateSelector itemTemplateSelector) => 
            this.GenerateContainerForItem(item, index, itemTemplate, itemTemplateSelector);

        DependencyObject IGeneratorHost.LinkContainerToItem(DependencyObject container, object item, DataTemplate itemTemplate, DataTemplateSelector itemTemplateSelector) => 
            this.LinkContainerToItem(container, item, itemTemplate, itemTemplateSelector);

        void IItemContainer.ClearContainer(object item)
        {
            BindingExpression bindingExpression = base.GetBindingExpression(FrameworkElement.DataContextProperty);
            BindingHelper.ClearBinding(this, FrameworkElement.DataContextProperty);
            if ((bindingExpression != null) && ReferenceEquals(bindingExpression, this.DataContextBinding))
            {
                BindingHelper.ClearBinding(this, FrameworkElement.DataContextProperty);
            }
            this.DataContextBinding = null;
        }

        void IItemContainer.PrepareContainer(object item)
        {
            BindingExpression bindingExpression = base.GetBindingExpression(FrameworkElement.DataContextProperty);
            if (!this.IsPropertySet(FrameworkElement.DataContextProperty) || ((bindingExpression != null) && ReferenceEquals(bindingExpression, this.DataContextBinding)))
            {
                this.DataContextBinding = BindingHelper.SetBinding(this, FrameworkElement.DataContextProperty, item, string.Empty);
            }
        }

        private void EndContainerGeneration()
        {
            this.containerGenerationLocker.Unlock();
            this.EndUpdate();
        }

        public override void EndInit()
        {
            this.initilizedActionsHelper.DoDelayedActions();
            base.EndInit();
            this.EndUpdate();
        }

        internal void EndUpdate()
        {
            this.updateCount--;
        }

        private void EnsureItemsAppearance()
        {
            base.CoerceValue(ItemsAppearanceProperty);
        }

        private void EnsureLogicalChildren(NotifyCollectionChangedEventArgs e, bool onAdd)
        {
            bool flag = e.Action == NotifyCollectionChangedAction.Reset;
            IList list1 = flag ? base.GetLogicalChildren().OfType<BaseLayoutItem>().Except<BaseLayoutItem>(this.Items).ToList<BaseLayoutItem>() : e.OldItems;
            IEnumerable children = list1;
            IList list2 = flag ? this.Items : e.NewItems;
            IEnumerable enumerable2 = list2;
            if (!onAdd)
            {
                this.RemoveLogicalChildren(children);
            }
            if (onAdd)
            {
                this.AddLogicalChildren(enumerable2);
            }
        }

        internal void EnsureTabHeaderScrollIndex(BaseLayoutItem item)
        {
            if (item == null)
            {
                this.TabHeaderScrollIndex = -1;
            }
            else if (!item.IsPinnedTab)
            {
                Func<BaseLayoutItem, bool> predicate = <>c.<>9__386_0;
                if (<>c.<>9__386_0 == null)
                {
                    Func<BaseLayoutItem, bool> local1 = <>c.<>9__386_0;
                    predicate = <>c.<>9__386_0 = x => !x.IsPinnedTab;
                }
                this.TabHeaderScrollIndex = this.Items.Where<BaseLayoutItem>(predicate).ToList<BaseLayoutItem>().IndexOf(item);
            }
        }

        protected internal override IUIElement FindUIScopeCore() => 
            this.ParentPanel ?? base.FindUIScopeCore();

        protected virtual DependencyObject GenerateContainerForItem(object item, DataTemplate itemTemplate = null, DataTemplateSelector itemTemplateSelector = null)
        {
            this.BeginContainerGeneration();
            BaseLayoutItem item2 = this.GetContainerForItem(item, itemTemplate, itemTemplateSelector);
            if (item2 != null)
            {
                ContentItem item3 = item2 as ContentItem;
                if (item3 != null)
                {
                    this.AddContainer(item3);
                }
                else
                {
                    this.AddContainer(item2);
                }
            }
            this.EndContainerGeneration();
            return item2;
        }

        protected virtual DependencyObject GenerateContainerForItem(object item, int index, DataTemplate itemTemplate = null, DataTemplateSelector itemTemplateSelector = null)
        {
            this.BeginContainerGeneration();
            BaseLayoutItem item2 = this.GetContainerForItem(item, itemTemplate, itemTemplateSelector);
            if (item2 != null)
            {
                this.AddContainer(index, item2);
            }
            this.EndContainerGeneration();
            return item2;
        }

        internal override bool GetAllowDockToCurrentItem()
        {
            if (!base.GetAllowDockToCurrentItem())
            {
                return false;
            }
            Func<BaseLayoutItem, bool> evaluator = <>c.<>9__387_0;
            if (<>c.<>9__387_0 == null)
            {
                Func<BaseLayoutItem, bool> local1 = <>c.<>9__387_0;
                evaluator = <>c.<>9__387_0 = x => x.GetAllowDockToCurrentItem();
            }
            return this.SelectedItem.Return<BaseLayoutItem, bool>(evaluator, (<>c.<>9__387_1 ??= () => true));
        }

        public int GetChildrenCount()
        {
            int result = 0;
            this.AcceptItems(delegate (BaseLayoutItem item) {
                int num = result;
                result = num + 1;
            });
            return result;
        }

        protected internal virtual BaseLayoutItem GetContainerForItem(object item, DataTemplate itemTemplate = null, DataTemplateSelector itemTemplateSelector = null)
        {
            BaseLayoutItem item2 = null;
            if (this.IsItemItsOwnContainer(item))
            {
                item2 = item as BaseLayoutItem;
            }
            if ((this.ItemTemplate != null) || (this.ItemTemplateSelector != null))
            {
                itemTemplate = this.ItemTemplate;
                itemTemplateSelector = this.ItemTemplateSelector;
            }
            if ((item2 == null) && ((itemTemplate != null) || (itemTemplateSelector != null)))
            {
                item2 = this.CreateItem(item, itemTemplate, itemTemplateSelector);
            }
            item2 ??= this.GetContainerForItemCore(item, itemTemplate, itemTemplateSelector);
            this.PrepareContainerForItem(item2, item);
            return item2;
        }

        protected virtual BaseLayoutItem GetContainerForItemCore(object content, DataTemplate itemTemplate = null, DataTemplateSelector itemTemplateSelector = null)
        {
            ContentItem item = this.CreateContentItem(content);
            if (item != null)
            {
                if (itemTemplate != null)
                {
                    item.ContentTemplate = itemTemplate;
                }
                if (itemTemplateSelector != null)
                {
                    item.ContentTemplateSelector = itemTemplateSelector;
                }
            }
            return item;
        }

        protected virtual LayoutGroup GetContainerHost(BaseLayoutItem container) => 
            this;

        protected int GetIndexInItemsInternal(int index) => 
            (index != 0) ? (((index != 1) || (this.ItemsInternal.Count != 1)) ? ((index * 2) - 1) : 1) : 0;

        private double GetIntervals()
        {
            Func<BaseLayoutItem, bool> predicate = <>c.<>9__564_0;
            if (<>c.<>9__564_0 == null)
            {
                Func<BaseLayoutItem, bool> local1 = <>c.<>9__564_0;
                predicate = <>c.<>9__564_0 = x => x.Visibility != Visibility.Collapsed;
            }
            List<BaseLayoutItem> list = this.Items.Where<BaseLayoutItem>(predicate).ToList<BaseLayoutItem>();
            double num = 0.0;
            for (int i = 1; i < list.Count; i++)
            {
                num += (double) base.GetValue(IntervalHelper.GetTargetProperty(list[i - 1], list[i]));
            }
            return num;
        }

        protected internal virtual bool GetIsDocumentHost() => 
            (base.Manager != null) && ((base.Manager.DockingStyle != DockingStyle.Default) && this.Items.IsDocumentHost(true));

        protected override bool GetIsPermanent() => 
            base.GetIsPermanent() || (!this.DestroyOnClosingChildren || this.clearContainerLocker);

        protected override LayoutItemType GetLayoutItemTypeCore() => 
            LayoutItemType.Group;

        protected virtual int GetNextTabIndex(BaseLayoutItem removedItem)
        {
            int tabIndexBeforeRemove = removedItem.TabIndexBeforeRemove;
            int tabIndex = (tabIndexBeforeRemove > 0) ? (tabIndexBeforeRemove - 1) : 0;
            if (this.SelectionOnTabRemoval == DevExpress.Xpf.Docking.SelectionOnTabRemoval.Following)
            {
                tabIndex = (tabIndexBeforeRemove < this.Items.Count) ? tabIndexBeforeRemove : --tabIndexBeforeRemove;
            }
            if (this.SelectionOnTabRemoval == DevExpress.Xpf.Docking.SelectionOnTabRemoval.PreviousSelection)
            {
                Func<BaseLayoutItem, DateTime> keySelector = <>c.<>9__474_0;
                if (<>c.<>9__474_0 == null)
                {
                    Func<BaseLayoutItem, DateTime> local1 = <>c.<>9__474_0;
                    keySelector = <>c.<>9__474_0 = x => x.LastSelectionDateTime;
                }
                BaseLayoutItem item2 = this.Items.OrderBy<BaseLayoutItem, DateTime>(keySelector, ListSortDirection.Descending).FirstOrDefault<BaseLayoutItem>();
                if ((item2 != null) && (item2.LastSelectionDateTime != DateTime.MinValue))
                {
                    tabIndex = this.TabIndexFromItem(item2);
                }
            }
            BaseLayoutItem item = this.ItemFromTabIndex(tabIndex);
            return this.IndexFromItem(item);
        }

        protected override BaseLayoutItem[] GetNodesCore() => 
            this.Items.ToArray<BaseLayoutItem>();

        internal static LayoutGroup GetOwnerGroup(DependencyObject target) => 
            (LayoutGroup) target.GetValue(OwnerGroupProperty);

        protected internal int GetSerializableSelectedTabPageIndex() => 
            this.serializableSelectedTabPageIndexCore;

        private Splitter GetSplitter(int index) => 
            ((index < 0) || (index >= this.ItemsInternal.Count)) ? null : (this.ItemsInternal[index] as Splitter);

        protected internal virtual BaseLayoutItem[] GetVisibleItems()
        {
            List<BaseLayoutItem> list = new List<BaseLayoutItem>();
            foreach (BaseLayoutItem item in this.Items)
            {
                if (item.IsVisibleCore)
                {
                    list.Add(item);
                }
            }
            return list.ToArray();
        }

        protected internal virtual int GetVisibleItemsCount()
        {
            int num = 0;
            foreach (BaseLayoutItem item in this.Items)
            {
                if (item.IsVisibleCore)
                {
                    num++;
                }
            }
            return num;
        }

        protected bool HasScrollableHeader() => 
            this.TabHeaderHasScroll && ((this.TabHeaderLayoutType == DevExpress.Xpf.Layout.Core.TabHeaderLayoutType.Default) || (this.TabHeaderLayoutType == DevExpress.Xpf.Layout.Core.TabHeaderLayoutType.Scroll));

        protected virtual bool HasTabHeader() => 
            this.GroupBorderStyle == DevExpress.Xpf.Docking.GroupBorderStyle.Tabbed;

        internal int IndexFromItem(BaseLayoutItem item) => 
            this.Items.IndexOf(item);

        public void Insert(int index, BaseLayoutItem item)
        {
            this.Items.Insert(index, item);
        }

        protected virtual void InsertContainer(int index, BaseLayoutItem container)
        {
            this.GetContainerHost(container).Insert(index, container);
        }

        internal void InvalidateActualItemsAppearance()
        {
            this.LockItemsAppearanceUpdate();
            AppearanceHelper.UpdateAppearance(this.ActualItemsAppearance, base.Parent?.ItemsAppearance, this.ItemsAppearance);
            this.UnlockItemsAppearanceUpdate();
        }

        protected bool IsControlItemsRoot() => 
            (base.Parent == null) && ((this.Items.Count > 0) && this.Items.ContainsOnlyControlItemsOrItsHosts());

        protected bool IsDockingItemsRoot() => 
            (base.Parent == null) && ((this.Items.Count > 0) && !this.Items.ContainsOnlyControlItemsOrItsHosts());

        protected virtual bool IsItemItsOwnContainer(object item) => 
            item is ContentItem;

        protected bool IsMDIItemsRoot() => 
            (base.Parent == null) && ((this.Items.Count == 1) && ((this.Items[0] is DocumentGroup) && !((DocumentGroup) this.Items[0]).IsTabbed));

        protected bool IsValid(int index) => 
            (index >= 0) && (index < this.Items.Count);

        internal BaseLayoutItem ItemFromIndex(int index) => 
            this.IsValid(index) ? this.Items[index] : null;

        internal BaseLayoutItem ItemFromTabIndex(int tabIndex)
        {
            if (!this.IsValid(tabIndex))
            {
                return null;
            }
            Func<BaseLayoutItem, bool> predicate = <>c.<>9__391_0;
            if (<>c.<>9__391_0 == null)
            {
                Func<BaseLayoutItem, bool> local1 = <>c.<>9__391_0;
                predicate = <>c.<>9__391_0 = x => !x.IsPinnedTab;
            }
            return this.PinnedLeftItems.Concat<BaseLayoutItem>(this.Items.Where<BaseLayoutItem>(predicate)).Concat<BaseLayoutItem>(this.PinnedRightItems).ToList<BaseLayoutItem>()[tabIndex];
        }

        private DependencyObject LinkContainerToItem(DependencyObject oldContainer, object item, DataTemplate itemTemplate, DataTemplateSelector itemTemplateSelector)
        {
            BaseLayoutItem container = null;
            BaseLayoutItem item3 = oldContainer as BaseLayoutItem;
            if ((item3 != null) && (item3.Parent != null))
            {
                LayoutGroup parent = item3.Parent;
                this.BeginContainerGeneration();
                parent.BeginContainerGeneration();
                int index = parent.Items.IndexOf(item3);
                container = this.GetContainerForItem(item, itemTemplate, itemTemplateSelector);
                if (container != null)
                {
                    parent.InsertContainer(index, container);
                    this.ClearContainerForItem(item3, item);
                }
                parent.EndContainerGeneration();
                this.EndContainerGeneration();
            }
            return container;
        }

        private void LockItemsAppearanceUpdate()
        {
            if (!this.IsItemsAppearanceUpdateLocked)
            {
                this.itemsAppearanceUpdatesCount = 0;
            }
            this.lockItemsAppearanceUpdateCounter++;
        }

        internal virtual IDisposable LockVisualTree() => 
            null;

        internal bool MoveItem(int index, BaseLayoutItem item)
        {
            LayoutPanel panel = item as LayoutPanel;
            if ((panel != null) && panel.IsPinnedTab)
            {
                LayoutPanel panel2 = this.ItemFromIndex(index) as LayoutPanel;
                if ((panel2 != null) && (panel2.IsPinnedTab && (panel2.TabPinLocation == panel.TabPinLocation)))
                {
                    return this.MovePinnedPanel(index, panel);
                }
                panel.ToggleTabPinStatus();
                index = (panel2 != null) ? this.IndexFromItem(panel2) : (this.Items.Count - 1);
            }
            int oldIndex = this.Items.IndexOf(item);
            if ((oldIndex == index) || (this.Items.Count <= 1))
            {
                return false;
            }
            if (index == this.Items.Count)
            {
                index = this.Items.Count - 1;
            }
            this.Items.Move(oldIndex, index);
            this.PlaceHolderHelper.MoveItem(index, item);
            return true;
        }

        internal void MoveItemsTo(LayoutGroup group)
        {
            foreach (BaseLayoutItem item in this.GetItems())
            {
                this.Items.Remove(item);
                group.Items.Add(item);
            }
            this.PlaceHolderHelper.MoveItemsTo(group);
        }

        private bool MovePinnedPanel(int index, LayoutPanel panel)
        {
            int num1;
            BaseLayoutItem objB = this.Items[index];
            if ((panel == null) || (!panel.IsPinnedTab || ReferenceEquals(panel, objB)))
            {
                return false;
            }
            int num = this.TabIndexFromItem(objB);
            bool flag = panel.TabPinLocation != TabHeaderPinLocation.Far;
            if (flag)
            {
                num1 = num;
            }
            else
            {
                Func<BaseLayoutItem, bool> predicate = <>c.<>9__568_0;
                if (<>c.<>9__568_0 == null)
                {
                    Func<BaseLayoutItem, bool> local1 = <>c.<>9__568_0;
                    predicate = <>c.<>9__568_0 = x => !x.IsPinnedTab;
                }
                num1 = (num - this.Items.Count<BaseLayoutItem>(predicate)) - this.PinnedLeftItems.Count;
            }
            int num2 = num1;
            ObservableCollection<BaseLayoutItem> collection = flag ? this.PinnedLeftItems : this.PinnedRightItems;
            if (!collection.IsValidIndex<BaseLayoutItem>(num2))
            {
                return false;
            }
            int oldIndex = collection.IndexOf(panel);
            if (num2 == oldIndex)
            {
                return false;
            }
            collection.Move(oldIndex, num2);
            panel.NotifyViewPinStatusChanged();
            return true;
        }

        protected void NotifyItems()
        {
            foreach (BaseLayoutItem item in this.Items)
            {
                item.OnParentItemsChanged();
                if (item is LayoutGroup)
                {
                    ((LayoutGroup) item).NotifyItems();
                }
            }
        }

        protected virtual void OnActualDockItemIntervalChanged(double interval)
        {
            base.CoerceValue(BaseLayoutItem.ActualMinSizeProperty);
        }

        protected virtual void OnActualItemsAppearanceChanged()
        {
            VisitDelegate<BaseLayoutItem> visit = <>c.<>9__486_0;
            if (<>c.<>9__486_0 == null)
            {
                VisitDelegate<BaseLayoutItem> local1 = <>c.<>9__486_0;
                visit = <>c.<>9__486_0 = delegate (BaseLayoutItem item) {
                    item.CoerceValue(BaseLayoutItem.ActualAppearanceProperty);
                    if (item is LayoutGroup)
                    {
                        ((LayoutGroup) item).InvalidateActualItemsAppearance();
                    }
                };
            }
            this.AcceptItems(visit);
        }

        private void OnActualItemsAppearanceChanged(object sender, EventArgs e)
        {
            this.itemsAppearanceUpdatesCount++;
            if (!this.IsItemsAppearanceUpdateLocked)
            {
                try
                {
                    this.OnActualItemsAppearanceChanged();
                }
                finally
                {
                    this.itemsAppearanceUpdatesCount = 0;
                }
            }
        }

        protected virtual void OnActualLayoutGroupIntervalChanged(double interval)
        {
            base.CoerceValue(BaseLayoutItem.ActualMinSizeProperty);
        }

        protected virtual void OnActualLayoutItemIntervalChanged(double interval)
        {
            base.CoerceValue(BaseLayoutItem.ActualMinSizeProperty);
        }

        protected override void OnActualMarginChanged(Thickness value)
        {
            base.OnActualMarginChanged(value);
            base.CoerceValue(BaseLayoutItem.ActualMinSizeProperty);
        }

        protected virtual void OnAllowExpandChanged(bool allow)
        {
            base.CoerceValue(IsExpandedProperty);
        }

        protected virtual void OnAllowSplittersChanged(bool? allow)
        {
            bool? nullable = allow;
            this.SetValue(IsSplittersEnabledPropertyKey, (nullable != null) ? ((object) nullable.GetValueOrDefault()) : ((object) 1));
            LayoutGroup group1 = this;
            if (<>c.<>9__491_0 == null)
            {
                group1 = (LayoutGroup) (<>c.<>9__491_0 = delegate (BaseLayoutItem item) {
                    if (item is LayoutGroup)
                    {
                        item.CoerceValue(AllowSplittersProperty);
                    }
                });
            }
            <>c.<>9__491_0.AcceptItems((VisitDelegate<BaseLayoutItem>) group1);
            if (this.ItemsInternal != null)
            {
                Func<object, bool> predicate = <>c.<>9__491_1;
                if (<>c.<>9__491_1 == null)
                {
                    Func<object, bool> local2 = <>c.<>9__491_1;
                    predicate = <>c.<>9__491_1 = x => x is Splitter;
                }
                this.ItemsInternal.Where<object>(predicate).Cast<Splitter>().ForEach<Splitter>(x => x.IsEnabled = this.IsSplittersEnabled);
            }
        }

        protected internal override void OnAppearanceObjectPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnAppearanceObjectPropertyChanged(e);
            this.InvalidateActualItemsAppearance();
        }

        protected override void OnCaptionAlignModeChanged(CaptionAlignMode oldValue, CaptionAlignMode value)
        {
            base.OnCaptionAlignModeChanged(oldValue, value);
            this.NotifyItems();
        }

        protected override void OnCaptionWidthChanged(double value)
        {
            base.OnCaptionWidthChanged(value);
            this.NotifyItems();
        }

        protected override double OnCoerceMinHeight(double value) => 
            ((this.GroupBorderStyle != DevExpress.Xpf.Docking.GroupBorderStyle.GroupBox) || this.IsExpanded) ? base.OnCoerceMinHeight(value) : 0.0;

        protected virtual void OnControlItemsHostChanged(bool? value)
        {
            base.CoerceValue(BaseLayoutItem.IsControlItemsHostProperty);
        }

        protected override void OnCreate()
        {
            base.OnCreate();
            this.Items = this.CreateItems();
            this.PinnedLeftItems = new ObservableCollection<BaseLayoutItem>();
            this.PinnedRightItems = new ObservableCollection<BaseLayoutItem>();
            this.PlaceHolderHelper = this.CreatePlaceHolderHelper();
            this.Items.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnItemsCollectionChanged);
            if (this.CanCreateItemsInternal())
            {
                this.ItemsInternal = this.CreateItemsInternal();
            }
            this.ActualGroupTemplateSelector = new DefaultItemTemplateSelectorWrapper(this.GroupTemplateSelector, this.GroupTemplate);
            this.EnsureItemsAppearance();
            this.VisiblePages = new ObservableCollection<BaseLayoutItem>();
            this.selectedTabIndexLockHelper = new LockHelper(() => base.CoerceValue(SelectedTabIndexProperty));
            this.itemTemplateLockHelper = new LockHelper(new LockHelper.LockHelperDelegate(this.ResetItemsSource));
        }

        protected override AutomationPeer OnCreateAutomationPeer() => 
            new LayoutGroupAutomationPeer(this);

        protected internal override void OnDeserializationComplete()
        {
            base.OnDeserializationComplete();
            this.OnLayoutChanged();
        }

        protected override void OnDockingViewStyleChanged(DockingViewStyle oldValue, DockingViewStyle newValue)
        {
            base.OnDockingViewStyleChanged(oldValue, newValue);
            base.CoerceValue(ActualDockItemIntervalProperty);
            base.CoerceValue(BaseLayoutItem.ActualMarginProperty);
        }

        protected virtual void OnDockItemIntervalChanged(double interval)
        {
            base.CoerceValue(ActualDockItemIntervalProperty);
        }

        internal override void OnDockLayoutManagerCoreChanged(DockLayoutManager oldValue, DockLayoutManager newValue)
        {
            base.OnDockLayoutManagerCoreChanged(oldValue, newValue);
            this.SyncLogicalTreeWithManager(oldValue, newValue);
            this.Items.ToList<BaseLayoutItem>().ForEach(x => x.DockLayoutManagerCore = newValue);
        }

        protected virtual void OnExpandedChanged(bool expanded)
        {
            base.CoerceValue(IsExpandedProperty);
        }

        protected virtual void OnGroupBorderStyleChanged(DevExpress.Xpf.Docking.GroupBorderStyle style)
        {
            base.CoerceValue(HasAccentProperty);
            base.CoerceValue(AllowExpandProperty);
            base.CoerceValue(DestroyOnClosingChildrenProperty);
            base.CoerceValue(SelectedItemProperty);
            foreach (BaseLayoutItem item in this.Items)
            {
                item.CoerceValue(BaseLayoutItem.IsTabPageProperty);
            }
            VisitDelegate<BaseLayoutItem> visit = <>c.<>9__501_0;
            if (<>c.<>9__501_0 == null)
            {
                VisitDelegate<BaseLayoutItem> local1 = <>c.<>9__501_0;
                visit = <>c.<>9__501_0 = delegate (BaseLayoutItem item) {
                    if (item is LayoutGroup)
                    {
                        item.CoerceValue(GroupBorderStyleProperty);
                    }
                };
            }
            this.AcceptItems(visit);
            this.UpdateVisiblePages();
            this.OnLayoutChanged();
        }

        protected virtual void OnGroupTemplateChanged()
        {
            this.ActualGroupTemplateSelector = this.CreateDefaultItemTemplateSelector();
        }

        protected virtual void OnHasAccentChanged(bool? hasAccent)
        {
            this.OnLayoutChanged();
        }

        protected override void OnHasCaptionChanged(bool hasCaption)
        {
            base.OnHasCaptionChanged(hasCaption);
            base.RaiseVisualChanged();
        }

        protected override void OnHasCaptionTemplateChanged(bool hasCaptionTemplate)
        {
            base.OnHasCaptionTemplateChanged(hasCaptionTemplate);
            base.RaiseVisualChanged();
        }

        protected virtual void OnHasNotCollapsedItemsChanged(bool hasVisibleItems)
        {
            if (base.Parent != null)
            {
                base.Parent.QueryRebuildLayout(RebuildLayoutOptions.UpdateLayout);
            }
            this.QueryRebuildLayout(RebuildLayoutOptions.UpdateLayout);
        }

        protected virtual void OnHasSingleItemChanged(bool hasSingleItem)
        {
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            this.IsInitializedAsDockingElement = !this.Items.ContainsOnlyControlItemsOrItsHosts();
            this.OnLayoutChanged();
            base.CoerceValue(SelectedTabIndexProperty);
        }

        protected virtual void OnIsAnimatedChanged(bool oldValue, bool newValue)
        {
            if (newValue)
            {
                this.IsAnimatedLockHelper.Lock();
            }
            else
            {
                this.IsAnimatedLockHelper.Unlock();
            }
        }

        protected override void OnIsCaptionVisibleChanged(bool isCaptionVisible)
        {
            base.OnIsCaptionVisibleChanged(isCaptionVisible);
            base.RaiseVisualChanged();
        }

        protected override void OnIsControlItemsHostChanged(bool value)
        {
            this.OnLayoutChanged();
            if (value && !this.Items.ContainsOnlyLayoutControlItemOrGroup())
            {
                throw new NotSupportedException(DockLayoutManagerHelper.GetRule(DockLayoutManagerRule.InconsistentLayout));
            }
        }

        internal override void OnIsCustomizationChanged(bool isCustomization)
        {
            base.OnIsCustomizationChanged(isCustomization);
            if (!base.IsInDesignTime)
            {
                base.CoerceValue(AllowSplittersProperty);
            }
        }

        protected virtual void OnIsExpandedChanged(bool expanded)
        {
            base.CoerceValue(BaseLayoutItem.ItemHeightProperty);
            base.Manager.Do<DockLayoutManager>(x => x.InvalidateView(this.GetRoot()));
            if (!base.IsAutoHidden && (this.GroupBorderStyle == DevExpress.Xpf.Docking.GroupBorderStyle.GroupBox))
            {
                base.CoerceValue(FrameworkElement.MinHeightProperty);
                base.CoerceValue(BaseLayoutItem.ActualMinSizeProperty);
                if (base.Manager != null)
                {
                    base.Manager.Update();
                }
            }
            base.RaiseVisualChanged();
        }

        protected virtual void OnIsLayoutRootChanged(bool value)
        {
            if (!base.IsInDesignTime)
            {
                base.CoerceValue(AllowSplittersProperty);
            }
        }

        protected override void OnIsTabPageChanged()
        {
            base.OnIsTabPageChanged();
            base.CoerceValue(GroupBorderStyleProperty);
        }

        protected virtual void OnIsTouchEnabledChanged(bool oldValue, bool newValue)
        {
            this.Items.ForEach<BaseLayoutItem>(x => this.UpdateIsTouchEnabled(x, newValue));
        }

        protected override void OnIsVisibleChanged(bool isVisible)
        {
            base.OnIsVisibleChanged(isVisible);
            using (this.selectedTabIndexLockHelper.Lock())
            {
                foreach (BaseLayoutItem item in this.Items)
                {
                    item.CoerceValue(BaseLayoutItem.IsVisibleCoreProperty);
                }
            }
        }

        protected internal virtual void OnItemIsVisibleChanged(BaseLayoutItem item)
        {
            using (this.rebuildQueryLocker.Lock())
            {
                this.OnLayoutChanged();
                this.GetRoot().NotifyItems();
                this.QueryRebuildLayout(RebuildLayoutOptions.None);
            }
        }

        internal void OnItemPinStatusChanged(LayoutPanel panel)
        {
            if (panel.Pinned)
            {
                this.AddToPinned(panel);
            }
            else
            {
                this.RemoveFromPinned(panel);
                if (this.Items.Contains(panel))
                {
                    this.Items.Move(this.Items.IndexOf(panel), (panel.TabPinLocation == TabHeaderPinLocation.Far) ? (this.Items.Count - 1) : 0);
                }
            }
        }

        protected virtual void OnItemsAppearanceChanged(Appearance newValue)
        {
            this.InvalidateActualItemsAppearance();
            newValue.Owner = this;
        }

        protected virtual void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.UpdateHasSingleItemProperty();
            this.EnsureLogicalChildren(e, true);
            if (this.ItemsInternal != null)
            {
                this.UpdateItemsInternal(e);
            }
            this.QueryRebuildLayout(RebuildLayoutOptions.None);
            base.CoerceValue(BaseLayoutItem.IsControlItemsHostProperty);
            this.GetRoot().NotifyItems();
            this.OnLayoutChanged();
            if (this.IsTabHost)
            {
                int selectedTabIndex;
                this.UpdateVisiblePages();
                if (e.Action == NotifyCollectionChangedAction.Move)
                {
                    base.CoerceValue(SelectedTabIndexProperty);
                    return;
                }
                if (((e.Action == NotifyCollectionChangedAction.Add) && ((this.SelectedTabIndex >= e.NewStartingIndex) || (this.SelectedTabIndex < 0))) && !base.IsInitializing)
                {
                    if (this.SelectedTabIndexLocker)
                    {
                        this.SelectedTabIndexLocker.AddUnlockAction(() => base.CoerceValue(SelectedTabIndexProperty));
                    }
                    else
                    {
                        selectedTabIndex = this.SelectedTabIndex;
                        this.SelectedTabIndex = selectedTabIndex + 1;
                    }
                }
                if ((e.Action == NotifyCollectionChangedAction.Remove) && (e.OldItems != null))
                {
                    foreach (object obj2 in e.OldItems)
                    {
                        this.RemoveFromPinned(obj2 as LayoutPanel);
                    }
                    if (this.SelectedTabIndex != e.OldStartingIndex)
                    {
                        if ((this.SelectedTabIndex > e.OldStartingIndex) && (this.SelectedTabIndex > 0))
                        {
                            selectedTabIndex = this.SelectedTabIndex;
                            this.SelectedTabIndex = selectedTabIndex - 1;
                        }
                    }
                    else
                    {
                        BaseLayoutItem removedItem = e.OldItems.Cast<object>().FirstOrDefault<object>() as BaseLayoutItem;
                        if (removedItem != null)
                        {
                            this.SelectedTabIndex = this.GetNextTabIndex(removedItem);
                        }
                    }
                }
                if (e.Action == NotifyCollectionChangedAction.Reset)
                {
                    base.CoerceValue(SelectedTabIndexProperty);
                }
                base.CoerceValue(SelectedItemProperty);
            }
            if (this.IsUpdating && (e.NewItems != null))
            {
                foreach (BaseLayoutItem item2 in e.NewItems)
                {
                    this.PrepareContainerForItem(item2);
                }
            }
            e.NewItems.Do<IList>(x => x.Cast<BaseLayoutItem>().ForEach<BaseLayoutItem>(y => y.Parent = this));
            this.RaiseWeakItemsChanged(e);
            this.EnsureLogicalChildren(e, false);
        }

        protected virtual void OnItemsSourceChanged(IEnumerable value, IEnumerable oldValue)
        {
            if (base.IsInitializing)
            {
                this.initilizedActionsHelper.AddDelayedAction(new Action(this.SetItemsSourceCore));
            }
            else
            {
                this.SetItemsSourceCore();
            }
        }

        protected virtual void OnItemStyleChanged(Style value, Style oldValue)
        {
            if (base.IsInitializing)
            {
                foreach (BaseLayoutItem item in this.Items)
                {
                    this.PrepareContainerForItem(item);
                }
            }
            this.OnItemTemplatePropertyChanged();
        }

        protected virtual void OnItemTemplatePropertyChanged()
        {
            if (!this.itemTemplateLockHelper.IsLocked)
            {
                this.itemTemplateLockHelper.Lock();
                BackgroundHelper.DoWithDispatcher(base.Dispatcher, () => this.itemTemplateLockHelper.Unlock());
            }
        }

        protected internal virtual void OnItemVisibilityChanged(BaseLayoutItem item, Visibility visibility)
        {
            if (this.IsTabHost)
            {
                int index = this.VisiblePages.IndexOf(item);
                this.UpdateVisiblePages();
                if (!this.selectedTabIndexLockHelper.IsLocked)
                {
                    if (item.Visibility == Visibility.Visible)
                    {
                        if (this.VisiblePagesCount == 1)
                        {
                            this.SelectedTabIndex = this.Items.IndexOf(this.VisiblePages[0]);
                        }
                    }
                    else if (ReferenceEquals(this.SelectedItem, item))
                    {
                        if (index > 0)
                        {
                            index--;
                        }
                        BaseLayoutItem item2 = (this.VisiblePagesCount > 0) ? this.VisiblePages[index] : null;
                        this.SelectedTabIndex = this.Items.IndexOf(item2);
                    }
                    base.CoerceValue(SelectedItemProperty);
                }
            }
        }

        protected void OnLayoutChanged()
        {
            base.LogicalTreeLockHelper.DoWhenUnlocked(new LockHelper.LockHelperDelegate(this.OnLayoutChangedAsync));
        }

        protected void OnLayoutChangedAsync()
        {
            if (!base.IsInitializing && (this.lockLayoutChanging <= 0))
            {
                this.lockLayoutChanging++;
                this.OnLayoutChangedCore();
                if (base.Parent != null)
                {
                    base.Parent.OnLayoutChanged();
                }
                else
                {
                    this.RaiseLayoutChangedCore();
                }
                this.lockLayoutChanging--;
            }
        }

        protected virtual void OnLayoutChangedCore()
        {
            using (this.rebuildQueryLocker.Lock())
            {
                base.CoerceValue(IsLayoutRootProperty);
                base.CoerceValue(GroupBorderStyleProperty);
                base.CoerceValue(BaseLayoutItem.ActualMarginProperty);
                base.CoerceValue(BaseLayoutItem.ActualMinSizeProperty);
                base.CoerceValue(BaseLayoutItem.ActualMaxSizeProperty);
                base.CoerceValue(HasNotCollapsedItemsProperty);
                base.CoerceValue(HasVisibleItemsProperty);
                this.UpdateButtons();
            }
        }

        protected virtual void OnLayoutGroupIntervalChanged(double interval)
        {
            base.CoerceValue(ActualLayoutGroupIntervalProperty);
        }

        protected virtual void OnLayoutItemIntervalChanged(double interval)
        {
            base.CoerceValue(ActualLayoutItemIntervalProperty);
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            this.RegisterView();
            foreach (BaseLayoutItem item in this.Items)
            {
                item.OnParentLoaded();
            }
            if (this.IsRootGroup)
            {
                base.Manager.Do<DockLayoutManager>(x => x.UpdateSelection(this));
            }
        }

        protected override void OnNameChanged()
        {
            base.OnNameChanged();
            DockLayoutManager manager = base.Manager;
            if (manager == null)
            {
                DockLayoutManager local1 = manager;
            }
            else
            {
                manager.OnGroupNameChanged(this);
            }
        }

        protected virtual void OnOrientationChanged(System.Windows.Controls.Orientation orientation)
        {
            base.CoerceValue(ActualLayoutItemIntervalProperty);
            base.CoerceValue(ActualLayoutGroupIntervalProperty);
            base.CoerceValue(ActualDockItemIntervalProperty);
            VisitDelegate<BaseLayoutItem> visit = <>c.<>9__529_0;
            if (<>c.<>9__529_0 == null)
            {
                VisitDelegate<BaseLayoutItem> local1 = <>c.<>9__529_0;
                visit = <>c.<>9__529_0 = delegate (BaseLayoutItem item) {
                    if (item is SeparatorItem)
                    {
                        item.CoerceValue(SeparatorItem.OrientationProperty);
                    }
                    if (item is LayoutSplitter)
                    {
                        item.CoerceValue(LayoutSplitter.OrientationProperty);
                    }
                };
            }
            this.AcceptItems(visit);
            this.OnLayoutChanged();
        }

        protected override void OnParentChanged()
        {
            base.OnParentChanged();
            base.CoerceValue(BaseLayoutItem.IsControlItemsHostProperty);
            base.CoerceValue(IsLayoutRootProperty);
            base.CoerceValue(BaseLayoutItem.ActualMarginProperty);
            base.CoerceValue(GroupBorderStyleProperty);
            VisitDelegate<BaseLayoutItem> visit = <>c.<>9__530_0;
            if (<>c.<>9__530_0 == null)
            {
                VisitDelegate<BaseLayoutItem> local1 = <>c.<>9__530_0;
                visit = <>c.<>9__530_0 = x => x.CoerceValue(BaseLayoutItem.ActualCaptionWidthProperty);
            }
            this.Accept(visit);
            this.InvalidateActualItemsAppearance();
        }

        protected internal override void OnParentItemsChanged()
        {
            base.OnParentItemsChanged();
            base.CoerceValue(BaseLayoutItem.IsControlItemsHostProperty);
        }

        protected virtual void OnSelectedItemChanged(BaseLayoutItem item, BaseLayoutItem oldItem)
        {
            if (oldItem != null)
            {
                oldItem.SetCurrentValue(BaseLayoutItem.IsSelectedItemProperty, false);
            }
            if (item != null)
            {
                item.SetCurrentValue(BaseLayoutItem.IsSelectedItemProperty, true);
                item.CoerceValue(BaseLayoutItem.IsCloseButtonVisibleProperty);
            }
            this.RaiseSelectedItemChanged(new SelectedItemChangedEventArgs(item, oldItem));
            base.CoerceValue(BaseLayoutItem.IsCloseButtonVisibleProperty);
            if (this.IsTabHost)
            {
                this.OnLayoutChanged();
            }
            this.RaiseWeakSelectedItemChanged(EventArgs.Empty);
        }

        protected virtual void OnSelectedTabIndexChanged(int index, int oldIndex)
        {
            base.CoerceValue(SelectedItemProperty);
            foreach (BaseLayoutItem item in this.Items)
            {
                item.CoerceValue(BaseLayoutItem.IsCloseButtonVisibleProperty);
            }
        }

        protected override void OnTabCaptionWidthChanged(double width)
        {
            base.OnTabCaptionWidthChanged(width);
            VisitDelegate<BaseLayoutItem> visit = <>c.<>9__533_0;
            if (<>c.<>9__533_0 == null)
            {
                VisitDelegate<BaseLayoutItem> local1 = <>c.<>9__533_0;
                visit = <>c.<>9__533_0 = item => item.CoerceValue(BaseLayoutItem.TabCaptionWidthProperty);
            }
            this.AcceptItems(visit);
        }

        protected virtual void OnTabHeaderHasScrollChanged(bool hasScroll)
        {
            if (this.GroupBorderStyle == DevExpress.Xpf.Docking.GroupBorderStyle.Tabbed)
            {
                this.OnLayoutChanged();
            }
        }

        protected virtual void OnTabHeaderLayoutTypeChanged(DevExpress.Xpf.Layout.Core.TabHeaderLayoutType type)
        {
            if (this.GroupBorderStyle == DevExpress.Xpf.Docking.GroupBorderStyle.Tabbed)
            {
                this.OnLayoutChanged();
            }
            base.CoerceValue(TabHeadersAutoFillProperty);
        }

        protected virtual void OnTabHeadersAutoFillChanged(bool autoFill)
        {
            if (this.GroupBorderStyle == DevExpress.Xpf.Docking.GroupBorderStyle.Tabbed)
            {
                this.OnLayoutChanged();
            }
        }

        internal override void OnUIChildrenCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            base.OnUIChildrenCollectionChanged(sender, e);
            if (!base.IsSelectedItem)
            {
                Func<LayoutPanel, bool> evaluator = <>c.<>9__398_0;
                if (<>c.<>9__398_0 == null)
                {
                    Func<LayoutPanel, bool> local1 = <>c.<>9__398_0;
                    evaluator = <>c.<>9__398_0 = x => x.IsSelectedItem;
                }
                if (!this.GetRoot().ParentPanel.Return<LayoutPanel, bool>(evaluator, (<>c.<>9__398_1 ??= () => false)))
                {
                    return;
                }
            }
            base.Manager.Do<DockLayoutManager>(x => x.InvalidateView(this));
        }

        protected override void OnUnloaded()
        {
            foreach (BaseLayoutItem item in this.Items)
            {
                item.OnParentUnloaded();
            }
            if (this.fClearTemplateRequested)
            {
                this.ClearTemplateCore();
            }
            base.OnUnloaded();
        }

        protected override void OnVisualParentChanged(DependencyObject oldParent)
        {
            base.OnVisualParentChanged(oldParent);
            if ((base.VisualParent == null) && (!this.IsInTree() && this.fClearTemplateRequested))
            {
                base.ClearValue(DockLayoutManager.UIScopeProperty);
            }
        }

        protected virtual void PrepareContainerForItem(BaseLayoutItem item)
        {
            if ((item != null) && this.IsUpdating)
            {
                this.PrepareContainerForItemCore(item);
            }
        }

        protected virtual void PrepareContainerForItem(BaseLayoutItem item, object content)
        {
            this.PrepareContainerForItem(item);
            IItemContainer container1 = item as IItemContainer;
            if (container1 == null)
            {
                IItemContainer local1 = container1;
            }
            else
            {
                container1.PrepareContainer(content);
            }
        }

        internal void PrepareContainerForItemCore(BaseLayoutItem item)
        {
            if ((item.CaptionTemplate == null) && (this.ItemCaptionTemplate != null))
            {
                item.CaptionTemplate = this.ItemCaptionTemplate;
            }
            if ((item.CaptionTemplateSelector == null) && (this.ItemCaptionTemplateSelector != null))
            {
                item.CaptionTemplateSelector = this.ItemCaptionTemplateSelector;
            }
            ContentItem item2 = item as ContentItem;
            if (item2 != null)
            {
                if ((item2.ContentTemplate == null) && (this.ItemContentTemplate != null))
                {
                    item2.ContentTemplate = this.ItemContentTemplate;
                }
                if ((item2.ContentTemplateSelector == null) && (this.ItemContentTemplateSelector != null))
                {
                    item2.ContentTemplateSelector = this.ItemContentTemplateSelector;
                }
            }
            if (this.ItemStyle != null)
            {
                item.Style = this.ItemStyle;
            }
        }

        internal override void PrepareForModification(bool isDeserializing)
        {
            base.PrepareForModification(isDeserializing);
            if (base.IsTemplateApplied && !EnvironmentHelper.IsNet45OrNewer)
            {
                this.RemoveLogicalChildren(this.Items);
            }
        }

        private void QueryRebuildLayout(RebuildLayoutOptions options = 0)
        {
            this.rebuildQueryLocker.QueryRebuildLayout(options);
        }

        protected void RaiseLayoutChangedCore()
        {
            if (this.LayoutChanged != null)
            {
                this.LayoutChanged(this, EventArgs.Empty);
            }
        }

        private void RaiseSelectedItemChanged(SelectedItemChangedEventArgs ea)
        {
            base.RaiseEvent(ea);
        }

        private void RaiseWeakItemsChanged(EventArgs args)
        {
            foreach (EventHandler handler in this.handlersWeakItemsChanged)
            {
                handler(this, args);
            }
        }

        private void RaiseWeakSelectedItemChanged(EventArgs args)
        {
            foreach (EventHandler handler in this.handlersWeakSelectedItemChanged)
            {
                handler(this, args);
            }
        }

        protected virtual void RegisterView()
        {
            DockLayoutManager scope = this.Scope as DockLayoutManager;
            if (scope != null)
            {
                scope.RegisterViewIfNeeded(this);
            }
        }

        public bool Remove(BaseLayoutItem item)
        {
            this.BeforeItemRemoved(item);
            return this.Items.Remove(item);
        }

        private void RemoveFromPinned(LayoutPanel panel)
        {
            this.PinnedLeftItems.Remove(panel);
            this.PinnedRightItems.Remove(panel);
        }

        protected void RemoveItemFromItemsInternal(BaseLayoutItem item)
        {
            int index = this.ItemsInternal.IndexOf(item);
            Splitter splitter = this.GetSplitter(index - 1);
            Splitter splitter2 = this.GetSplitter(index + 1);
            if ((splitter != null) || (splitter2 != null))
            {
                this.ItemsInternal.Remove((index == 0) ? splitter2 : splitter);
            }
            this.ItemsInternal.Remove(item);
        }

        protected void RemoveLogicalChildren(IEnumerable children)
        {
            if (children != null)
            {
                foreach (DependencyObject obj2 in children)
                {
                    base.RemoveLogicalChild(obj2);
                }
            }
        }

        private void ResetItemsSource()
        {
            if (this.ItemsSource != null)
            {
                this.BeginUpdate();
                this.Items.ResetItemsSource();
                this.EndUpdate();
            }
        }

        public bool ScrollNext()
        {
            if (!this.TabHeaderHasScroll)
            {
                return false;
            }
            int tabHeaderScrollIndex = this.TabHeaderScrollIndex;
            this.TabHeaderScrollIndex = tabHeaderScrollIndex + 1;
            return true;
        }

        public bool ScrollPrev()
        {
            if (!this.TabHeaderHasScroll)
            {
                return false;
            }
            int tabHeaderScrollIndex = this.TabHeaderScrollIndex;
            this.TabHeaderScrollIndex = tabHeaderScrollIndex - 1;
            return true;
        }

        protected internal override void SelectTemplate()
        {
            if (this.fClearTemplateRequested)
            {
                if ((base.PartMultiTemplateControl != null) && ReferenceEquals(base.PartMultiTemplateControl.LayoutItem, this))
                {
                    base.PartMultiTemplateControl.ClearTemplateIfNeeded(this);
                }
                this.fClearTemplateRequested = false;
            }
            base.SelectTemplate();
        }

        protected internal override void SetHidden(bool value, LayoutGroup customizationRoot)
        {
            base.SetHidden(value, customizationRoot);
            foreach (BaseLayoutItem item in this.Items)
            {
                item.SetHidden(value, item.Parent);
            }
            base.CoerceValue(IsLayoutRootProperty);
        }

        private void SetItemsSourceCore()
        {
            this.BeginUpdate();
            using (this.SelectedTabIndexLocker.Lock())
            {
                this.Items.SetItemsSource(this.ItemsSource);
            }
            this.EndUpdate();
        }

        internal static void SetOwnerGroup(DependencyObject target, LayoutGroup value)
        {
            target.SetValue(OwnerGroupProperty, value);
        }

        internal void SetSelection(BaseLayoutItem item, bool selected)
        {
            if (selected)
            {
                int index = this.Items.IndexOf(item);
                if (this.SelectedTabIndex != index)
                {
                    base.SetCurrentValue(SelectedTabIndexProperty, this.Items.IndexOf(item));
                }
            }
            else if (ReferenceEquals(this.SelectedItem, item))
            {
                using (this.setSelectionLocker.Lock())
                {
                    base.CoerceValue(SelectedTabIndexProperty);
                }
            }
        }

        internal void SyncLogicalTreeWithManager(DockLayoutManager oldValue, DockLayoutManager newValue)
        {
            using (base.SuspendLayoutChange())
            {
                if ((oldValue != null) && oldValue.OptimizedLogicalTree)
                {
                    this.Items.ToList<BaseLayoutItem>().ForEach(x => DockLayoutManager.RemoveLogicalChild(oldValue, x));
                }
                if ((newValue != null) && newValue.OptimizedLogicalTree)
                {
                    this.Items.ToList<BaseLayoutItem>().ForEach(delegate (BaseLayoutItem x) {
                        if (x.SupportsOptimizedLogicalTree)
                        {
                            this.RemoveLogicalChild(x);
                            DockLayoutManager.AddLogicalChild(newValue, x);
                        }
                    });
                }
                else
                {
                    this.AddLogicalChildren(this.Items);
                }
            }
        }

        internal int TabIndexFromItem(BaseLayoutItem item)
        {
            if (!this.Items.Contains(item))
            {
                return -1;
            }
            LayoutPanel panel = item as LayoutPanel;
            if ((panel == null) || !panel.IsPinnedTab)
            {
                Func<BaseLayoutItem, bool> func2 = <>c.<>9__403_1;
                if (<>c.<>9__403_1 == null)
                {
                    Func<BaseLayoutItem, bool> local2 = <>c.<>9__403_1;
                    func2 = <>c.<>9__403_1 = x => !x.IsPinnedTab;
                }
                return (this.Items.Where<BaseLayoutItem>(func2).ToList<BaseLayoutItem>().IndexOf(item) + this.PinnedLeftItems.Count);
            }
            if (panel.TabPinLocation != TabHeaderPinLocation.Far)
            {
                return this.PinnedLeftItems.IndexOf(panel);
            }
            Func<BaseLayoutItem, bool> predicate = <>c.<>9__403_0;
            if (<>c.<>9__403_0 == null)
            {
                Func<BaseLayoutItem, bool> local1 = <>c.<>9__403_0;
                predicate = <>c.<>9__403_0 = x => !x.IsPinnedTab;
            }
            return ((this.Items.Count<BaseLayoutItem>(predicate) + this.PinnedLeftItems.Count) + this.PinnedRightItems.IndexOf(panel));
        }

        private void UnlockItemsAppearanceUpdate()
        {
            int num = this.lockItemsAppearanceUpdateCounter - 1;
            this.lockItemsAppearanceUpdateCounter = num;
            if ((num == 0) && (this.itemsAppearanceUpdatesCount > 0))
            {
                this.OnActualItemsAppearanceChanged();
            }
        }

        protected override void UnlockLogicalTreeCore()
        {
            base.UnlockLogicalTreeCore();
            if (!EnvironmentHelper.IsNet45OrNewer)
            {
                this.AddLogicalChildren(this.Items);
            }
            this.rebuildQueryLocker.RebuildLayout(RebuildLayoutOptions.None);
        }

        protected internal override void UpdateButtons()
        {
            base.CoerceValue(BaseLayoutItem.IsCloseButtonVisibleProperty);
            base.CoerceValue(BaseLayoutItem.IsScrollPrevButtonVisibleProperty);
            base.CoerceValue(BaseLayoutItem.IsScrollNextButtonVisibleProperty);
        }

        protected void UpdateHasSingleItemProperty()
        {
            this.HasSingleItem = (this.Items.Count == 1) && ((this.Items[0].ItemType == LayoutItemType.TabPanelGroup) || (this.Items[0] is LayoutPanel));
        }

        private void UpdateIsTouchEnabled(DependencyObject target, bool isTouchEnabled)
        {
            if (IsTouchEnabledPropertyKey != null)
            {
                target.SetValue(IsTouchEnabledPropertyKey, isTouchEnabled);
            }
        }

        protected void UpdateItemsInternal(NotifyCollectionChangedEventArgs e)
        {
            if (this.Items.IsLockUpdate && (e.Action == NotifyCollectionChangedAction.Remove))
            {
                if (e.OldItems != null)
                {
                    foreach (BaseLayoutItem item in e.OldItems)
                    {
                        if (!this.Items.Contains(item))
                        {
                            this.RemoveItemFromItemsInternal(item);
                        }
                    }
                }
            }
            else
            {
                if (e != null)
                {
                    if (e.Action == NotifyCollectionChangedAction.Reset)
                    {
                        this.ItemsInternal.Clear();
                    }
                    if (e.OldItems != null)
                    {
                        bool flag = (e.Action == NotifyCollectionChangedAction.Move) || (e.Action == NotifyCollectionChangedAction.Replace);
                        foreach (BaseLayoutItem item2 in e.OldItems)
                        {
                            if (!this.Items.Contains(item2) | flag)
                            {
                                this.RemoveItemFromItemsInternal(item2);
                            }
                        }
                    }
                }
                for (int i = 0; i < this.Items.Count; i++)
                {
                    this.AddItemInItemsInternal(this.Items[i], i);
                }
            }
        }

        protected void UpdateVisiblePages()
        {
            this.VisiblePages.Clear();
            foreach (BaseLayoutItem item in this.Items)
            {
                if (item.Visibility == Visibility.Visible)
                {
                    this.VisiblePages.Add(item);
                }
            }
            base.CoerceValue(VisiblePagesCountProperty);
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public double ActualDockItemInterval =>
            (double) base.GetValue(ActualDockItemIntervalProperty);

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataTemplateSelector ActualGroupTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(ActualGroupTemplateSelectorProperty);
            private set => 
                base.SetValue(ActualGroupTemplateSelectorPropertyKey, value);
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public double ActualLayoutGroupInterval =>
            (double) base.GetValue(ActualLayoutGroupIntervalProperty);

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public double ActualLayoutItemInterval =>
            (double) base.GetValue(ActualLayoutItemIntervalProperty);

        [Description("Gets or sets whether a group can be expanded/collapsed.This is a dependency property."), XtraSerializableProperty, Category("Behavior")]
        public bool AllowExpand
        {
            get => 
                (bool) base.GetValue(AllowExpandProperty);
            set => 
                base.SetValue(AllowExpandProperty, value);
        }

        [Description("Gets or sets whether item resizing is enabled for the group's children.This is a dependency property."), XtraSerializableProperty, Category("Layout")]
        public bool? AllowSplitters
        {
            get => 
                (bool?) base.GetValue(AllowSplittersProperty);
            set => 
                base.SetValue(AllowSplittersProperty, value);
        }

        public DevExpress.Xpf.Docking.AutoScrollOnOverflow AutoScrollOnOverflow
        {
            get => 
                (DevExpress.Xpf.Docking.AutoScrollOnOverflow) base.GetValue(AutoScrollOnOverflowProperty);
            set => 
                base.SetValue(AutoScrollOnOverflowProperty, value);
        }

        [Description("Gets or sets the orientation of the group's header.This is a dependency property."), Category("Caption")]
        public System.Windows.Controls.Orientation CaptionOrientation
        {
            get => 
                (System.Windows.Controls.Orientation) base.GetValue(CaptionOrientationProperty);
            set => 
                base.SetValue(CaptionOrientationProperty, value);
        }

        [Description("This member supports the internal infrastructure, and is not intended to be used directly from your code. This is a dependency property."), Category("Behavior")]
        public bool? ControlItemsHost
        {
            get => 
                (bool?) base.GetValue(ControlItemsHostProperty);
            set => 
                base.SetValue(ControlItemsHostProperty, value);
        }

        [Description("Gets or sets if a tab item's content is destroyed when another tab item is selected. This is a dependency property."), Category("Behavior"), XtraSerializableProperty, Obsolete("Use the TabContentCacheMode property instead.")]
        public bool DestroyContentOnTabSwitching
        {
            get => 
                (bool) base.GetValue(DestroyContentOnTabSwitchingProperty);
            set => 
                base.SetValue(DestroyContentOnTabSwitchingProperty, value);
        }

        [Description("Gets or sets whether the current group is destroyed when removing all its children.This is a dependency property."), XtraSerializableProperty, Category("Behavior")]
        public bool DestroyOnClosingChildren
        {
            get => 
                (bool) base.GetValue(DestroyOnClosingChildrenProperty);
            set => 
                base.SetValue(DestroyOnClosingChildrenProperty, value);
        }

        [Description("Gets or sets the distance between immediate child dock items.This is a dependency property."), XtraSerializableProperty, Category("Layout")]
        public double DockItemInterval
        {
            get => 
                (double) base.GetValue(DockItemIntervalProperty);
            set => 
                base.SetValue(DockItemIntervalProperty, value);
        }

        [Description("Gets or sets whether the group is expanded.This is a dependency property."), XtraSerializableProperty, Category("Layout")]
        public bool Expanded
        {
            get => 
                (bool) base.GetValue(ExpandedProperty);
            set => 
                base.SetValue(ExpandedProperty, value);
        }

        [Category("TabHeader")]
        public bool FixedMultiLineTabHeaders
        {
            get => 
                (bool) base.GetValue(FixedMultiLineTabHeadersProperty);
            set => 
                base.SetValue(FixedMultiLineTabHeadersProperty, value);
        }

        [Description("Gets or sets the group's border style. This option is in effect when the LayoutGroup is used to combine layout items, rather than dock items.This is a dependency property."), XtraSerializableProperty, Category("Content")]
        public virtual DevExpress.Xpf.Docking.GroupBorderStyle GroupBorderStyle
        {
            get => 
                (DevExpress.Xpf.Docking.GroupBorderStyle) base.GetValue(GroupBorderStyleProperty);
            set => 
                base.SetValue(GroupBorderStyleProperty, value);
        }

        [Description("Gets or sets the template used to render the LayoutGroup.This is a dependency property."), Category("Content")]
        public DataTemplate GroupTemplate
        {
            get => 
                (DataTemplate) base.GetValue(GroupTemplateProperty);
            set => 
                base.SetValue(GroupTemplateProperty, value);
        }

        [Description("Gets or sets an object that chooses the group's template based on custom logic.This is a dependency property."), Category("Content")]
        public DataTemplateSelector GroupTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(GroupTemplateSelectorProperty);
            set => 
                base.SetValue(GroupTemplateSelectorProperty, value);
        }

        [Description("Gets or sets whether the group is marked with a special flag (has a special accent) that makes the group painted with different outer indents.This is a dependency property."), XtraSerializableProperty, Category("Layout")]
        public bool? HasAccent
        {
            get => 
                (bool?) base.GetValue(HasAccentProperty);
            set => 
                base.SetValue(HasAccentProperty, value);
        }

        [Description("Gets whether the group owns a single item.This is a dependency property.")]
        public bool HasSingleItem
        {
            get => 
                (bool) base.GetValue(HasSingleItemProperty);
            private set => 
                base.SetValue(HasSingleItemPropertyKey, value);
        }

        [Description("Gets whether the current group contains items whose Visibility property is set to Visibility.Visible.This is a dependency property.")]
        public bool HasVisibleItems
        {
            get => 
                (bool) base.GetValue(HasVisibleItemsProperty);
            private set => 
                base.SetValue(HasVisibleItemsPropertyKey, value);
        }

        [Description("Gets whether an animation is in progress.This is a dependency property.")]
        public bool IsAnimated
        {
            get => 
                (bool) base.GetValue(IsAnimatedProperty);
            internal set => 
                base.SetValue(IsAnimatedPropertyKey, value);
        }

        [Description("Gets whether the group is actually expanded.This is a dependency property.")]
        public bool IsExpanded
        {
            get => 
                (bool) base.GetValue(IsExpandedProperty);
            internal set => 
                base.SetValue(IsExpandedPropertyKey, value);
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsLayoutRoot =>
            (bool) base.GetValue(IsLayoutRootProperty);

        [Description("Gets whether the Scroll Next button is visible.")]
        public bool IsScrollNextButtonVisible =>
            (bool) base.GetValue(BaseLayoutItem.IsScrollNextButtonVisibleProperty);

        [Description("Gets whether the Scroll Prev button is visible.")]
        public bool IsScrollPrevButtonVisible =>
            (bool) base.GetValue(BaseLayoutItem.IsScrollPrevButtonVisibleProperty);

        [Description("Gets whether item resizing is actually currently enabled for the LayoutGroup's children.This is a dependency property.")]
        public bool IsSplittersEnabled =>
            (bool) base.GetValue(IsSplittersEnabledProperty);

        [Description("Gets or sets the template used to visualize captions of objects stored as elements in the DockLayoutManager.ItemsSource collection.This is a dependency property.")]
        public DataTemplate ItemCaptionTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ItemCaptionTemplateProperty);
            set => 
                base.SetValue(ItemCaptionTemplateProperty, value);
        }

        [Description("Gets or sets an object that chooses a template used to visualize captions of objects stored as elements in the DockLayoutManager.ItemsSource collection. This is a dependency property.")]
        public DataTemplateSelector ItemCaptionTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(ItemCaptionTemplateSelectorProperty);
            set => 
                base.SetValue(ItemCaptionTemplateSelectorProperty, value);
        }

        [Description("Gets or sets the template used to visualize contents of objects stored as elements in the LayoutGroup.ItemsSource collection.This is a dependency property.")]
        public DataTemplate ItemContentTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ItemContentTemplateProperty);
            set => 
                base.SetValue(ItemContentTemplateProperty, value);
        }

        [Description("Gets or sets an object that chooses a template used to visualize contents of objects stored as elements in the LayoutGroup.ItemsSource collection. This is a dependency property.")]
        public DataTemplateSelector ItemContentTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(ItemContentTemplateSelectorProperty);
            set => 
                base.SetValue(ItemContentTemplateSelectorProperty, value);
        }

        [Description("Provides access to child items."), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public BaseLayoutItemCollection Items { get; private set; }

        [Description("Gets or sets the settings used to customize the appearance of captions for the group's children.This is a dependency property."), Category("Caption")]
        public Appearance ItemsAppearance
        {
            get => 
                (Appearance) base.GetValue(ItemsAppearanceProperty);
            set => 
                base.SetValue(ItemsAppearanceProperty, value);
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public ObservableCollection<object> ItemsInternal { get; private set; }

        [Description("Gets or sets a collection of objects providing information to generate and initialize groups, panels and layout items for the current LayoutGroup container. This is a dependency property.")]
        public IEnumerable ItemsSource
        {
            get => 
                (IEnumerable) base.GetValue(ItemsSourceProperty);
            set => 
                base.SetValue(ItemsSourceProperty, value);
        }

        [Description("")]
        public Style ItemStyle
        {
            get => 
                (Style) base.GetValue(ItemStyleProperty);
            set => 
                base.SetValue(ItemStyleProperty, value);
        }

        [Description("Gets or sets a DataTemplate used to render items stored in the LayoutGroup.ItemsSource collection.")]
        public DataTemplate ItemTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ItemTemplateProperty);
            set => 
                base.SetValue(ItemTemplateProperty, value);
        }

        [Description("Gets or sets a DataTemplateSelector object that provides a way to choose a DataTemplate to render items stored in the LayoutGroup.ItemsSource collection.")]
        public DataTemplateSelector ItemTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(ItemTemplateSelectorProperty);
            set => 
                base.SetValue(ItemTemplateSelectorProperty, value);
        }

        public bool LastChildFill
        {
            get => 
                (bool) base.GetValue(LastChildFillProperty);
            set => 
                base.SetValue(LastChildFillProperty, value);
        }

        [Description("Gets or sets the distance between immediate child LayoutGroup objects.This is a dependency property."), XtraSerializableProperty, Category("Layout")]
        public double LayoutGroupInterval
        {
            get => 
                (double) base.GetValue(LayoutGroupIntervalProperty);
            set => 
                base.SetValue(LayoutGroupIntervalProperty, value);
        }

        [Description("Gets or sets the distance between immediate child LayoutControlItem objects.This is a dependency property."), XtraSerializableProperty, Category("Layout")]
        public double LayoutItemInterval
        {
            get => 
                (double) base.GetValue(LayoutItemIntervalProperty);
            set => 
                base.SetValue(LayoutItemIntervalProperty, value);
        }

        [Description("Gets or sets whether child items are arranged horizontally or vertically within the group.This is a dependency property."), XtraSerializableProperty, Category("Layout")]
        public System.Windows.Controls.Orientation Orientation
        {
            get => 
                (System.Windows.Controls.Orientation) base.GetValue(OrientationProperty);
            set => 
                base.SetValue(OrientationProperty, value);
        }

        [Description("Gets the selected child item within the current group (mostly, this property is used when the current group represents child items as tabs).This is a dependency property.")]
        public BaseLayoutItem SelectedItem
        {
            get => 
                (BaseLayoutItem) base.GetValue(SelectedItemProperty);
            internal set => 
                base.SetValue(SelectedItemPropertyKey, value);
        }

        [Description("Gets or sets the index of the active child item. This property is supported for groups representing its children as tabs.This is a dependency property."), Category("TabHeader")]
        public int SelectedTabIndex
        {
            get => 
                (int) base.GetValue(SelectedTabIndexProperty);
            set => 
                base.SetValue(SelectedTabIndexProperty, value);
        }

        public DevExpress.Xpf.Docking.SelectionOnTabRemoval SelectionOnTabRemoval
        {
            get => 
                (DevExpress.Xpf.Docking.SelectionOnTabRemoval) base.GetValue(SelectionOnTabRemovalProperty);
            set => 
                base.SetValue(SelectionOnTabRemovalProperty, value);
        }

        [XtraSerializableProperty, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public int SerializableSelectedTabPageIndex
        {
            get => 
                this.SelectedTabIndex;
            set => 
                this.serializableSelectedTabPageIndexCore = value;
        }

        [Description("Allows you to hide the Scroll Next button within the group's header. This property is in effect if the scroll buttons have been enabled via the LayoutGroup.TabHeaderLayoutType property."), XtraSerializableProperty, Category("Layout")]
        public bool ShowScrollNextButton
        {
            get => 
                (bool) base.GetValue(BaseLayoutItem.ShowScrollNextButtonProperty);
            set => 
                base.SetValue(BaseLayoutItem.ShowScrollNextButtonProperty, value);
        }

        [Description("Allows you to hide the Scroll Prev button within the group's header. This property is in effect if the scroll buttons have been enabled via the LayoutGroup.TabHeaderLayoutType property."), XtraSerializableProperty, Category("Layout")]
        public bool ShowScrollPrevButton
        {
            get => 
                (bool) base.GetValue(BaseLayoutItem.ShowScrollPrevButtonProperty);
            set => 
                base.SetValue(BaseLayoutItem.ShowScrollPrevButtonProperty, value);
        }

        public bool ShowTabHeaders
        {
            get => 
                (bool) base.GetValue(ShowTabHeadersProperty);
            set => 
                base.SetValue(ShowTabHeadersProperty, value);
        }

        [XtraSerializableProperty, Category("Behavior")]
        public DevExpress.Xpf.Core.TabContentCacheMode TabContentCacheMode
        {
            get => 
                (DevExpress.Xpf.Core.TabContentCacheMode) base.GetValue(TabContentCacheModeProperty);
            set => 
                base.SetValue(TabContentCacheModeProperty, value);
        }

        [Description("Gets whether it's possible to scroll forward through tab headers corresponding to the group's child items. This property is in effect if the current group represents child items as tabs.This is a dependency property.")]
        public bool TabHeaderCanScrollNext
        {
            get => 
                (bool) base.GetValue(TabHeaderCanScrollNextProperty);
            internal set => 
                base.SetValue(TabHeaderCanScrollNextPropertyKey, value);
        }

        [Description("Gets whether it's possible to scroll backward through tab headers corresponding to the group's child items. This property is in effect if the current group represents child items as tabs.This is a dependency property.")]
        public bool TabHeaderCanScrollPrev
        {
            get => 
                (bool) base.GetValue(TabHeaderCanScrollPrevProperty);
            internal set => 
                base.SetValue(TabHeaderCanScrollPrevPropertyKey, value);
        }

        [Description("Gets whether the group's header displays scroll buttons used to scroll through the tab headers corresponding to the group's child items. This property is in effect if the current group represents child items as tabs.This is a dependency property.")]
        public bool TabHeaderHasScroll
        {
            get => 
                (bool) base.GetValue(TabHeaderHasScrollProperty);
            internal set => 
                base.SetValue(TabHeaderHasScrollPropertyKey, value);
        }

        [Description("Gets or sets how the LayoutGroup displays the tab headers corresponding to the group's child items. This property is in effect if the current group represents child items as tabs.This is a dependency property."), XtraSerializableProperty, Category("TabHeader")]
        public DevExpress.Xpf.Layout.Core.TabHeaderLayoutType TabHeaderLayoutType
        {
            get => 
                (DevExpress.Xpf.Layout.Core.TabHeaderLayoutType) base.GetValue(TabHeaderLayoutTypeProperty);
            set => 
                base.SetValue(TabHeaderLayoutTypeProperty, value);
        }

        [Description("Gets the maximum tab header scroll position. This member supports the internal infrastructure, and is not intended to be used directly from your code.")]
        public int TabHeaderMaxScrollIndex
        {
            get => 
                (int) base.GetValue(TabHeaderMaxScrollIndexProperty);
            internal set => 
                base.SetValue(TabHeaderMaxScrollIndexPropertyKey, value);
        }

        [Description("Gets or sets whether the tab headers, corresponding to the group's child items, must be automatically stretched to fill the empty space in a tab row. This property is in effect if the current group represents child items as tabs.This is a dependency property."), XtraSerializableProperty, Category("TabHeader")]
        public bool TabHeadersAutoFill
        {
            get => 
                (bool) base.GetValue(TabHeadersAutoFillProperty);
            set => 
                base.SetValue(TabHeadersAutoFillProperty, value);
        }

        [Description("Gets the index that defines the tab header scroll position. This member supports the internal infrastructure, and is not intended to be used directly from your code.This is a dependency property.")]
        public int TabHeaderScrollIndex
        {
            get => 
                (int) base.GetValue(TabHeaderScrollIndexProperty);
            internal set => 
                base.SetValue(TabHeaderScrollIndexPropertyKey, value);
        }

        [Description("Gets or sets a style applied to the part of the LayoutGroup containing tab headers. This is a dependency property.")]
        public Style TabItemContainerStyle
        {
            get => 
                (Style) base.GetValue(TabItemContainerStyleProperty);
            set => 
                base.SetValue(TabItemContainerStyleProperty, value);
        }

        [Description("Gets or sets an object that chooses a style applied to the LayoutGroup.TabItemContainerStyle property. This is a dependency property.")]
        public StyleSelector TabItemContainerStyleSelector
        {
            get => 
                (StyleSelector) base.GetValue(TabItemContainerStyleSelectorProperty);
            set => 
                base.SetValue(TabItemContainerStyleSelectorProperty, value);
        }

        [Description(""), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public ObservableCollection<BaseLayoutItem> VisiblePages { get; private set; }

        [Description("Gets the number of items in the LayoutGroup.VisiblePages collection.This is a dependency property.")]
        public int VisiblePagesCount =>
            (int) base.GetValue(VisiblePagesCountProperty);

        internal bool AcceptDock =>
            !this.HasNotCollapsedItems || !this.Items.HasVisibleStarItems();

        internal Appearance ActualItemsAppearance
        {
            get
            {
                if (this.actualItemsAppearance == null)
                {
                    this.actualItemsAppearance = new Appearance();
                    this.actualItemsAppearance.Changed += new EventHandler(this.OnActualItemsAppearanceChanged);
                }
                return this.actualItemsAppearance;
            }
        }

        internal Appearance DefaultItemsAppearance
        {
            get
            {
                Appearance appearance2 = this._DefaultItemsAppearance;
                if (this._DefaultItemsAppearance == null)
                {
                    Appearance local1 = this._DefaultItemsAppearance;
                    appearance2 = this._DefaultItemsAppearance = new Appearance();
                }
                return appearance2;
            }
        }

        internal virtual bool HasPlaceHolders =>
            (this.PlaceHolderHelper != null) && this.PlaceHolderHelper.HasPlaceHolders;

        internal LockHelper IsAnimatedLockHelper
        {
            get
            {
                LockHelper isAnimatedLockHelper = this.isAnimatedLockHelper;
                if (this.isAnimatedLockHelper == null)
                {
                    LockHelper local1 = this.isAnimatedLockHelper;
                    isAnimatedLockHelper = this.isAnimatedLockHelper = new LockHelper();
                }
                return isAnimatedLockHelper;
            }
        }

        internal bool IsAutoGenerated { get; set; }

        internal bool IsInContainerGeneration =>
            (bool) this.containerGenerationLocker;

        internal bool IsRootGroup { get; set; }

        internal DevExpress.Xpf.Docking.Internal.PlaceHolderHelper PlaceHolderHelper { get; private set; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        protected internal bool ArrangeAllCachedTabs
        {
            get => 
                (bool) base.GetValue(ArrangeAllCachedTabsProperty);
            set => 
                base.SetValue(ArrangeAllCachedTabsProperty, value);
        }

        protected internal virtual bool HasItems =>
            this.Items.Count > 0;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        protected internal bool HasNotCollapsedItems
        {
            get => 
                (bool) base.GetValue(HasNotCollapsedItemsProperty);
            private set => 
                base.SetValue(HasNotCollapsedItemsPropertyKey, value);
        }

        protected internal virtual bool IgnoreOrientation =>
            this.GroupBorderStyle == DevExpress.Xpf.Docking.GroupBorderStyle.Tabbed;

        protected internal bool IsInitializedAsDockingElement { get; private set; }

        protected internal virtual bool IsTabHost =>
            this.GroupBorderStyle == DevExpress.Xpf.Docking.GroupBorderStyle.Tabbed;

        protected internal bool IsUngroupped { get; set; }

        protected internal LayoutPanel ParentPanel { get; set; }

        protected internal TabbedGroupDisplayMode TabbedGroupDisplayModeCore
        {
            get => 
                (TabbedGroupDisplayMode) base.GetValue(TabbedGroupDisplayModeCoreProperty);
            set => 
                base.SetValue(TabbedGroupDisplayModeCoreProperty, value);
        }

        protected virtual bool CanAddLogicalChild =>
            true;

        private bool IsItemsAppearanceUpdateLocked =>
            this.lockItemsAppearanceUpdateCounter > 0;

        private bool IsTouchEnabled
        {
            get => 
                (bool) base.GetValue(IsTouchEnabledProperty);
            set => 
                base.SetValue(IsTouchEnabledProperty, value);
        }

        private bool IsUpdating =>
            this.updateCount > 0;

        string ISupportOriginalSerializableName.OriginalName { get; set; }

        private ObservableCollection<BaseLayoutItem> PinnedLeftItems { get; set; }

        private ObservableCollection<BaseLayoutItem> PinnedRightItems { get; set; }

        public BaseLayoutItem this[string name] =>
            this.Items[name];

        public BaseLayoutItem this[int index] =>
            this.Items[index];

        bool ILogicalOwner.IsLoaded =>
            base.IsLoaded;

        double ILogicalOwner.ActualWidth =>
            base.ActualWidth;

        double ILogicalOwner.ActualHeight =>
            base.ActualHeight;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LayoutGroup.<>c <>9 = new LayoutGroup.<>c();
            public static Func<BaseLayoutItem, bool> <>9__386_0;
            public static Func<BaseLayoutItem, bool> <>9__387_0;
            public static Func<bool> <>9__387_1;
            public static Func<BaseLayoutItem, bool> <>9__391_0;
            public static Func<LayoutPanel, bool> <>9__398_0;
            public static Func<bool> <>9__398_1;
            public static Func<BaseLayoutItem, bool> <>9__403_0;
            public static Func<BaseLayoutItem, bool> <>9__403_1;
            public static Func<DockLayoutManager, IDockController> <>9__410_0;
            public static Func<BaseLayoutItem, bool> <>9__428_0;
            public static Func<bool> <>9__428_1;
            public static Func<BaseLayoutItem, DateTime> <>9__474_0;
            public static VisitDelegate<BaseLayoutItem> <>9__486_0;
            public static VisitDelegate<BaseLayoutItem> <>9__491_0;
            public static Func<object, bool> <>9__491_1;
            public static VisitDelegate<BaseLayoutItem> <>9__501_0;
            public static VisitDelegate<BaseLayoutItem> <>9__529_0;
            public static VisitDelegate<BaseLayoutItem> <>9__530_0;
            public static VisitDelegate<BaseLayoutItem> <>9__533_0;
            public static Func<BaseLayoutItem, bool> <>9__564_0;
            public static Func<BaseLayoutItem, bool> <>9__568_0;

            internal void <.cctor>b__77_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutGroup) dObj).OnOrientationChanged((Orientation) e.NewValue);
            }

            internal object <.cctor>b__77_1(DependencyObject dObj, object value) => 
                ((LayoutGroup) dObj).CoerceDestroyOnClosingChildren((bool) value);

            internal void <.cctor>b__77_10(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutGroup) dObj).OnActualLayoutItemIntervalChanged((double) e.NewValue);
            }

            internal object <.cctor>b__77_11(DependencyObject dObj, object value) => 
                ((LayoutGroup) dObj).CoerceActualLayoutItemInterval((double) value);

            internal void <.cctor>b__77_12(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutGroup) dObj).OnActualLayoutGroupIntervalChanged((double) e.NewValue);
            }

            internal object <.cctor>b__77_13(DependencyObject dObj, object value) => 
                ((LayoutGroup) dObj).CoerceActualLayoutGroupInterval((double) value);

            internal void <.cctor>b__77_14(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutGroup) dObj).OnActualDockItemIntervalChanged((double) e.NewValue);
            }

            internal object <.cctor>b__77_15(DependencyObject dObj, object value) => 
                ((LayoutGroup) dObj).CoerceActualDockItemInterval((double) value);

            internal void <.cctor>b__77_16(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutGroup) dObj).OnAllowSplittersChanged((bool?) e.NewValue);
            }

            internal object <.cctor>b__77_17(DependencyObject dObj, object value) => 
                ((LayoutGroup) dObj).CoerceAllowSplitters((bool?) value);

            internal void <.cctor>b__77_18(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutGroup) dObj).OnGroupBorderStyleChanged((GroupBorderStyle) e.NewValue);
            }

            internal object <.cctor>b__77_19(DependencyObject dObj, object value) => 
                ((LayoutGroup) dObj).CoerceGroupBorderStyle((GroupBorderStyle) value);

            internal void <.cctor>b__77_2(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutGroup) dObj).OnIsLayoutRootChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__77_20(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutGroup) dObj).OnAllowExpandChanged((bool) e.NewValue);
            }

            internal object <.cctor>b__77_21(DependencyObject dObj, object value) => 
                ((LayoutGroup) dObj).CoerceAllowExpand((bool) value);

            internal void <.cctor>b__77_22(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutGroup) dObj).OnExpandedChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__77_23(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutGroup) dObj).OnIsExpandedChanged((bool) e.NewValue);
            }

            internal object <.cctor>b__77_24(DependencyObject dObj, object value) => 
                ((LayoutGroup) dObj).CoerceIsExpanded((bool) value);

            internal void <.cctor>b__77_25(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutGroup) dObj).OnHasSingleItemChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__77_26(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutGroup) dObj).OnGroupTemplateChanged();
            }

            internal void <.cctor>b__77_27(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutGroup) dObj).OnGroupTemplateChanged();
            }

            internal void <.cctor>b__77_28(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((LayoutGroup) dObj).OnHasNotCollapsedItemsChanged((bool) ea.NewValue);
            }

            internal object <.cctor>b__77_29(DependencyObject dObj, object value) => 
                ((LayoutGroup) dObj).CoerceHasNotCollapsedItems((bool) value);

            internal object <.cctor>b__77_3(DependencyObject dObj, object value) => 
                ((LayoutGroup) dObj).CoerceIsLayoutRoot((bool) value);

            internal object <.cctor>b__77_30(DependencyObject dObj, object value) => 
                ((LayoutGroup) dObj).CoerceHasVisibleItems((bool) value);

            internal void <.cctor>b__77_31(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((LayoutGroup) dObj).OnSelectedItemChanged((BaseLayoutItem) ea.NewValue, (BaseLayoutItem) ea.OldValue);
            }

            internal object <.cctor>b__77_32(DependencyObject dObj, object value) => 
                ((LayoutGroup) dObj).CoerceSelectedItem((BaseLayoutItem) value);

            internal void <.cctor>b__77_33(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((LayoutGroup) dObj).OnSelectedTabIndexChanged((int) ea.NewValue, (int) ea.OldValue);
            }

            internal object <.cctor>b__77_34(DependencyObject dObj, object value) => 
                ((LayoutGroup) dObj).CoerceSelectedTabIndex((int) value);

            internal void <.cctor>b__77_35(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((LayoutGroup) dObj).OnTabHeaderLayoutTypeChanged((TabHeaderLayoutType) ea.NewValue);
            }

            internal object <.cctor>b__77_36(DependencyObject dObj, object value) => 
                ((LayoutGroup) dObj).CoerceTabHeaderLayoutType((TabHeaderLayoutType) value);

            internal void <.cctor>b__77_37(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((LayoutGroup) dObj).OnTabHeadersAutoFillChanged((bool) ea.NewValue);
            }

            internal object <.cctor>b__77_38(DependencyObject dObj, object value) => 
                ((LayoutGroup) dObj).CoerceTabHeadersAutoFill((bool) value);

            internal void <.cctor>b__77_39(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((LayoutGroup) dObj).OnTabHeaderHasScrollChanged((bool) ea.NewValue);
            }

            internal void <.cctor>b__77_4(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutGroup) dObj).OnHasAccentChanged((bool?) e.NewValue);
            }

            internal object <.cctor>b__77_40(DependencyObject dObj, object value) => 
                ((LayoutGroup) dObj).CoerceTabHeaderScrollIndex((int) value);

            internal object <.cctor>b__77_41(DependencyObject dObj, object value) => 
                ((LayoutGroup) dObj).CoerceTabHeaderMaxScrollIndex((int) value);

            internal void <.cctor>b__77_42(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutGroup) dObj).OnIsAnimatedChanged((bool) e.OldValue, (bool) e.NewValue);
            }

            internal void <.cctor>b__77_43(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutGroup) dObj).OnItemsAppearanceChanged((Appearance) e.NewValue);
            }

            internal object <.cctor>b__77_44(DependencyObject dObj, object value) => 
                ((LayoutGroup) dObj).CoerceItemsAppearance((Appearance) value);

            internal object <.cctor>b__77_45(DependencyObject dObj, object value) => 
                ((LayoutGroup) dObj).CoerceVisiblePagesCount((int) value);

            internal void <.cctor>b__77_46(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutGroup) dObj).OnItemsSourceChanged((IEnumerable) e.NewValue, (IEnumerable) e.OldValue);
            }

            internal void <.cctor>b__77_47(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutGroup) dObj).OnItemStyleChanged((Style) e.NewValue, (Style) e.OldValue);
            }

            internal void <.cctor>b__77_48(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutGroup) dObj).OnItemTemplatePropertyChanged();
            }

            internal void <.cctor>b__77_49(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutGroup) dObj).OnItemTemplatePropertyChanged();
            }

            internal object <.cctor>b__77_5(DependencyObject dObj, object value) => 
                ((LayoutGroup) dObj).CoerceHasAccent((bool?) value);

            internal void <.cctor>b__77_50(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutGroup) dObj).OnItemTemplatePropertyChanged();
            }

            internal void <.cctor>b__77_51(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutGroup) dObj).OnItemTemplatePropertyChanged();
            }

            internal void <.cctor>b__77_52(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutGroup) d).OnIsTouchEnabledChanged((bool) e.OldValue, (bool) e.NewValue);
            }

            internal void <.cctor>b__77_53(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutGroup) dObj).OnItemTemplatePropertyChanged();
            }

            internal void <.cctor>b__77_54(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutGroup) dObj).OnItemTemplatePropertyChanged();
            }

            internal void <.cctor>b__77_6(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutGroup) dObj).OnControlItemsHostChanged((bool?) e.NewValue);
            }

            internal void <.cctor>b__77_7(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutGroup) dObj).OnLayoutItemIntervalChanged((double) e.NewValue);
            }

            internal void <.cctor>b__77_8(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutGroup) dObj).OnLayoutGroupIntervalChanged((double) e.NewValue);
            }

            internal void <.cctor>b__77_9(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutGroup) dObj).OnDockItemIntervalChanged((double) e.NewValue);
            }

            internal bool <AddLogicalChildren>b__428_0(BaseLayoutItem x) => 
                x.SupportsOptimizedLogicalTree;

            internal bool <AddLogicalChildren>b__428_1() => 
                false;

            internal IDockController <ClearContainerForItem>b__410_0(DockLayoutManager x) => 
                x.DockController;

            internal bool <EnsureTabHeaderScrollIndex>b__386_0(BaseLayoutItem x) => 
                !x.IsPinnedTab;

            internal bool <GetAllowDockToCurrentItem>b__387_0(BaseLayoutItem x) => 
                x.GetAllowDockToCurrentItem();

            internal bool <GetAllowDockToCurrentItem>b__387_1() => 
                true;

            internal bool <GetIntervals>b__564_0(BaseLayoutItem x) => 
                x.Visibility != Visibility.Collapsed;

            internal DateTime <GetNextTabIndex>b__474_0(BaseLayoutItem x) => 
                x.LastSelectionDateTime;

            internal bool <ItemFromTabIndex>b__391_0(BaseLayoutItem x) => 
                !x.IsPinnedTab;

            internal bool <MovePinnedPanel>b__568_0(BaseLayoutItem x) => 
                !x.IsPinnedTab;

            internal void <OnActualItemsAppearanceChanged>b__486_0(BaseLayoutItem item)
            {
                item.CoerceValue(BaseLayoutItem.ActualAppearanceProperty);
                if (item is LayoutGroup)
                {
                    ((LayoutGroup) item).InvalidateActualItemsAppearance();
                }
            }

            internal void <OnAllowSplittersChanged>b__491_0(BaseLayoutItem item)
            {
                if (item is LayoutGroup)
                {
                    item.CoerceValue(LayoutGroup.AllowSplittersProperty);
                }
            }

            internal bool <OnAllowSplittersChanged>b__491_1(object x) => 
                x is Splitter;

            internal void <OnGroupBorderStyleChanged>b__501_0(BaseLayoutItem item)
            {
                if (item is LayoutGroup)
                {
                    item.CoerceValue(LayoutGroup.GroupBorderStyleProperty);
                }
            }

            internal void <OnOrientationChanged>b__529_0(BaseLayoutItem item)
            {
                if (item is SeparatorItem)
                {
                    item.CoerceValue(SeparatorItem.OrientationProperty);
                }
                if (item is LayoutSplitter)
                {
                    item.CoerceValue(LayoutSplitter.OrientationProperty);
                }
            }

            internal void <OnParentChanged>b__530_0(BaseLayoutItem x)
            {
                x.CoerceValue(BaseLayoutItem.ActualCaptionWidthProperty);
            }

            internal void <OnTabCaptionWidthChanged>b__533_0(BaseLayoutItem item)
            {
                item.CoerceValue(BaseLayoutItem.TabCaptionWidthProperty);
            }

            internal bool <OnUIChildrenCollectionChanged>b__398_0(LayoutPanel x) => 
                x.IsSelectedItem;

            internal bool <OnUIChildrenCollectionChanged>b__398_1() => 
                false;

            internal bool <TabIndexFromItem>b__403_0(BaseLayoutItem x) => 
                !x.IsPinnedTab;

            internal bool <TabIndexFromItem>b__403_1(BaseLayoutItem x) => 
                !x.IsPinnedTab;
        }

        private class DefaultTemplateSelector : DefaultItemTemplateSelectorWrapper.DefaultItemTemplateSelector
        {
            public override DataTemplate SelectTemplate(object item, DependencyObject container)
            {
                GroupPaneContentPresenter presenter = container as GroupPaneContentPresenter;
                LayoutGroup group = item as LayoutGroup;
                if ((group != null) && ((presenter != null) && (presenter.Owner != null)))
                {
                    switch (group.GroupBorderStyle)
                    {
                        case GroupBorderStyle.NoBorder:
                            return presenter.Owner.NoBorderTemplate;

                        case GroupBorderStyle.Group:
                            return presenter.Owner.GroupTemplate;

                        case GroupBorderStyle.GroupBox:
                            return presenter.Owner.GroupBoxTemplate;

                        case GroupBorderStyle.Tabbed:
                            return presenter.Owner.TabbedTemplate;

                        default:
                            break;
                    }
                }
                return null;
            }
        }

        [Flags]
        private enum RebuildLayoutOptions
        {
            None,
            UpdateLayout
        }

        private class RebuildQueryLocker : Locker
        {
            private LayoutGroup.RebuildLayoutOptions _options;
            private readonly LayoutGroup _owner;
            private int _rebuildQueryCount;

            public RebuildQueryLocker(LayoutGroup owner)
            {
                this._owner = owner;
                base.Unlocked += new EventHandler(this.OnRebuildQueryLockerUnlocked);
            }

            private void OnRebuildQueryLockerUnlocked(object sender, EventArgs e)
            {
                if (this._rebuildQueryCount > 0)
                {
                    this.RebuildLayoutCore();
                }
            }

            public void QueryRebuildLayout(LayoutGroup.RebuildLayoutOptions options = 0)
            {
                this._options = options;
                this.RebuildLayoutCore();
            }

            public void RebuildLayout(LayoutGroup.RebuildLayoutOptions options = 0)
            {
                if (this._rebuildQueryCount > 0)
                {
                    this.QueryRebuildLayout(options);
                }
            }

            private void RebuildLayoutCore()
            {
                this._rebuildQueryCount++;
                if (!base.IsLocked && !this._owner.IsLogicalTreeLocked)
                {
                    try
                    {
                        this._owner.RebuildLayoutAndUpdate((this._options & LayoutGroup.RebuildLayoutOptions.UpdateLayout) != LayoutGroup.RebuildLayoutOptions.None);
                    }
                    finally
                    {
                        this._options = LayoutGroup.RebuildLayoutOptions.None;
                        this._rebuildQueryCount = 0;
                    }
                }
            }
        }
    }
}

