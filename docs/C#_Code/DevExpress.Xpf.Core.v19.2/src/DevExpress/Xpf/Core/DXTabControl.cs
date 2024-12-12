namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.ModuleInjection;
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Bars.Automation;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Core.Serialization;
    using DevExpress.Xpf.Utils;
    using DevExpress.Xpf.Utils.About;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Threading;

    [LicenseProvider(typeof(DX_WPFEditors_LicenseProvider)), TemplatePart(Name="PART_TabPanelContainer", Type=typeof(TabPanelContainer)), DXToolboxBrowsable(DXToolboxItemKind.Free), ToolboxTabName("DX.19.2: Navigation & Layout"), DefaultEvent("SelectionChanged"), DefaultProperty("SelectedIndex")]
    public class DXTabControl : HeaderedSelectorBase<DXTabControl, DXTabItem>, ICloneable, IThemedWindowSupport, IMultipleElementRegistratorSupport, IBarNameScopeSupport, IInputElement
    {
        public const string FastRenderPanelName = "PART_FastRenderPanel";
        public const string ContentPresenterName = "PART_SelectedContentHost";
        public const string TabPanelName = "PART_TabPanelContainer";
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty PanelIndentProperty = DependencyProperty.Register("PanelIndent", typeof(Thickness), typeof(DXTabControl));
        public static readonly DependencyProperty BackgroundTemplateProperty = DependencyProperty.Register("BackgroundTemplate", typeof(DataTemplate), typeof(DXTabControl));
        public static readonly DependencyProperty ControlBoxLeftTemplateProperty;
        public static readonly DependencyProperty ControlBoxRightTemplateProperty;
        public static readonly DependencyProperty ControlBoxPanelTemplateProperty;
        public static readonly DependencyProperty ContentHeaderTemplateProperty;
        public static readonly DependencyProperty ContentFooterTemplateProperty;
        public static readonly DependencyProperty ContentHostTemplateProperty;
        private static readonly DependencyPropertyKey ControlBoxLeftPropertyKey;
        private static readonly DependencyPropertyKey ControlBoxRightPropertyKey;
        private static readonly DependencyPropertyKey ControlBoxPanelPropertyKey;
        private static readonly DependencyPropertyKey ContentHeaderPropertyKey;
        private static readonly DependencyPropertyKey ContentFooterPropertyKey;
        private static readonly DependencyPropertyKey ContentHostPropertyKey;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty ControlBoxLeftProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty ControlBoxRightProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty ControlBoxPanelProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty ContentHeaderProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty ContentFooterProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty ContentHostProperty;
        public static readonly DependencyProperty SelectedTabItemProperty;
        public static readonly DependencyProperty DestroyContentOnTabSwitchingProperty;
        public static readonly DependencyProperty TabContentCacheModeProperty;
        public static readonly DependencyProperty AllowMergingProperty;
        private static readonly DependencyPropertyKey ViewInfoPropertyKey;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty ViewInfoProperty;
        private static readonly DependencyPropertyKey MenuInfoPropertyKey;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty MenuInfoProperty;
        public static readonly DependencyProperty ViewProperty;
        private DevExpress.Xpf.Core.Native.FastRenderPanel fastRenderPanelWithoutContentHost;
        private System.Windows.Controls.ContentPresenter contentPresenterWithoutContentHost;
        private List<object> newItems = new List<object>();
        private bool allowUpdateSelectedTabItem = true;
        public DevExpress.Xpf.Core.PrepareTabItemDelegate PrepareTabItemDelegate;

        public event TabControlNewTabbedWindowEventHandler NewTabbedWindow;

        public event TabControlSelectionChangedEventHandler SelectionChanged;

        public event TabControlSelectionChangingEventHandler SelectionChanging;

        public event TabControlTabAddedEventHandler TabAdded;

        public event TabControlTabAddingEventHandler TabAdding;

        public event TabControlTabDroppingOnEmptySpaceEventHandler TabDropping;

        public event TabControlTabHiddenEventHandler TabHidden;

        public event TabControlTabHidingEventHandler TabHiding;

        public event TabControlTabInsertedEventHandler TabInserted;

        public event TabControlTabInsertingEventHandler TabInserting;

        public event TabControlTabMovedEventHandler TabMoved;

        public event TabControlTabMovingEventHandler TabMoving;

        public event TabControlTabRemovedEventHandler TabRemoved;

        public event TabControlTabRemovingEventHandler TabRemoving;

        public event TabControlTabShowingEventHandler TabShowing;

        public event TabControlTabShownEventHandler TabShown;

        public event TabControlTabStartDraggingEventHandler TabStartDragging;

        static DXTabControl()
        {
            ControlBoxLeftTemplateProperty = DependencyProperty.Register("ControlBoxLeftTemplate", typeof(DataTemplate), typeof(DXTabControl), new FrameworkPropertyMetadata(null, (d, e) => ((DXTabControl) d).OnLogicalElementTemplateChanged(ControlBoxLeftProperty, ControlBoxLeftPropertyKey, (DataTemplate) e.NewValue)));
            ControlBoxRightTemplateProperty = DependencyProperty.Register("ControlBoxRightTemplate", typeof(DataTemplate), typeof(DXTabControl), new FrameworkPropertyMetadata(null, (d, e) => ((DXTabControl) d).OnLogicalElementTemplateChanged(ControlBoxRightProperty, ControlBoxRightPropertyKey, (DataTemplate) e.NewValue)));
            ControlBoxPanelTemplateProperty = DependencyProperty.Register("ControlBoxPanelTemplate", typeof(DataTemplate), typeof(DXTabControl), new FrameworkPropertyMetadata(null, (d, e) => ((DXTabControl) d).OnLogicalElementTemplateChanged(ControlBoxPanelProperty, ControlBoxPanelPropertyKey, (DataTemplate) e.NewValue)));
            ContentHeaderTemplateProperty = DependencyProperty.Register("ContentHeaderTemplate", typeof(DataTemplate), typeof(DXTabControl), new FrameworkPropertyMetadata(null, (d, e) => ((DXTabControl) d).OnLogicalElementTemplateChanged(ContentHeaderProperty, ContentHeaderPropertyKey, (DataTemplate) e.NewValue)));
            ContentFooterTemplateProperty = DependencyProperty.Register("ContentFooterTemplate", typeof(DataTemplate), typeof(DXTabControl), new FrameworkPropertyMetadata(null, (d, e) => ((DXTabControl) d).OnLogicalElementTemplateChanged(ContentFooterProperty, ContentFooterPropertyKey, (DataTemplate) e.NewValue)));
            ContentHostTemplateProperty = DependencyProperty.Register("ContentHostTemplate", typeof(DataTemplate), typeof(DXTabControl), new FrameworkPropertyMetadata(null, (d, e) => ((DXTabControl) d).OnLogicalElementTemplateChanged(ContentHostProperty, ContentHostPropertyKey, (DataTemplate) e.NewValue)));
            ControlBoxLeftPropertyKey = DependencyProperty.RegisterReadOnly("ControlBoxLeft", typeof(object), typeof(DXTabControl), null);
            ControlBoxRightPropertyKey = DependencyProperty.RegisterReadOnly("ControlBoxRight", typeof(object), typeof(DXTabControl), null);
            ControlBoxPanelPropertyKey = DependencyProperty.RegisterReadOnly("ControlBoxPanel", typeof(object), typeof(DXTabControl), null);
            ContentHeaderPropertyKey = DependencyProperty.RegisterReadOnly("ContentHeader", typeof(object), typeof(DXTabControl), null);
            ContentFooterPropertyKey = DependencyProperty.RegisterReadOnly("ContentFooter", typeof(object), typeof(DXTabControl), null);
            ContentHostPropertyKey = DependencyProperty.RegisterReadOnly("ContentHost", typeof(object), typeof(DXTabControl), null);
            ControlBoxLeftProperty = ControlBoxLeftPropertyKey.DependencyProperty;
            ControlBoxRightProperty = ControlBoxRightPropertyKey.DependencyProperty;
            ControlBoxPanelProperty = ControlBoxPanelPropertyKey.DependencyProperty;
            ContentHeaderProperty = ContentHeaderPropertyKey.DependencyProperty;
            ContentFooterProperty = ContentFooterPropertyKey.DependencyProperty;
            ContentHostProperty = ContentHostPropertyKey.DependencyProperty;
            SelectedTabItemProperty = DependencyProperty.Register("SelectedTabItem", typeof(DXTabItem), typeof(DXTabControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) => ((DXTabControl) d).OnSelectedTabItemChanged()));
            DestroyContentOnTabSwitchingProperty = DependencyProperty.Register("DestroyContentOnTabSwitching", typeof(bool), typeof(DXTabControl), new PropertyMetadata(true));
            TabContentCacheModeProperty = DependencyProperty.Register("TabContentCacheMode", typeof(DevExpress.Xpf.Core.TabContentCacheMode), typeof(DXTabControl), new PropertyMetadata(DevExpress.Xpf.Core.TabContentCacheMode.None));
            AllowMergingProperty = DependencyPropertyManager.Register("AllowMerging", typeof(bool?), typeof(DXTabControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits, (d, e) => ((DXTabControl) d).OnAllowMergingChanged()));
            ViewInfoPropertyKey = DependencyProperty.RegisterReadOnly("ViewInfo", typeof(TabViewInfo), typeof(DXTabControl), null);
            ViewInfoProperty = ViewInfoPropertyKey.DependencyProperty;
            MenuInfoPropertyKey = DependencyProperty.RegisterReadOnly("MenuInfo", typeof(DXTabControlHeaderMenuInfo), typeof(DXTabControl), null);
            MenuInfoProperty = MenuInfoPropertyKey.DependencyProperty;
            ViewProperty = DependencyProperty.Register("View", typeof(TabControlViewBase), typeof(DXTabControl), new PropertyMetadata(null, (d, e) => ((DXTabControl) d).OnViewPropertyChanged((TabControlViewBase) e.OldValue, (TabControlViewBase) e.NewValue), (d, e) => ((DXTabControl) d).CoerceViewProperty(e as TabControlViewBase)));
            DXTabControlStrategyRegistrator.RegisterDXTabControlStrategy();
            DXSerializer.EnabledProperty.OverrideMetadata(typeof(DXTabControl), new UIPropertyMetadata(false));
            DXSerializer.SerializationIDDefaultProperty.OverrideMetadata(typeof(DXTabControl), new UIPropertyMetadata(typeof(DXTabControl).Name));
            DXSerializer.SerializationProviderProperty.OverrideMetadata(typeof(DXTabControl), new UIPropertyMetadata(new DXTabControlSerializationProvider()));
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(DXTabControl), new FrameworkPropertyMetadata(typeof(DXTabControl)));
            Control.IsTabStopProperty.OverrideMetadata(typeof(DXTabControl), new FrameworkPropertyMetadata(false));
            FrameworkElement.NameProperty.OverrideMetadata(typeof(DXTabControl), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.None, (d, e) => ((DXTabControl) d).OnNameChanged((string) e.OldValue, (string) e.NewValue)));
        }

        public DXTabControl()
        {
            this.SerializationInfo = new DXTabControlSerializationInfo();
            base.Loaded += new RoutedEventHandler(this.OnLoaded);
            base.Unloaded += new RoutedEventHandler(this.OnUnloaded);
            base.SetValue(MenuInfoPropertyKey, new DXTabControlHeaderMenuInfo(this));
            this.OnAllowMergingChanged();
        }

        [CompilerGenerated, DebuggerHidden]
        private int <>n__0(int index, NotifyCollectionChangedAction? originativeAction) => 
            base.GetCoercedSelectedIndex(index, originativeAction);

        public virtual object AddNewTabItem()
        {
            object obj2 = base.AddNewItem();
            DXTabItem tabItem = this.GetTabItem(obj2);
            if (tabItem != null)
            {
                tabItem.SetCurrentValue(DXTabItem.IsNewProperty, true);
            }
            else
            {
                this.newItems.Add(obj2);
            }
            return obj2;
        }

        internal void BeginDragDrop()
        {
            this.IsDragDrop = true;
        }

        public virtual bool CanSelectNext() => 
            base.CanSelectNextItem(false);

        public virtual bool CanSelectPrev() => 
            base.CanSelectPrevItem(false);

        private void CheckNewItems()
        {
            List<object> list = new List<object>();
            foreach (object obj2 in this.newItems)
            {
                if (!base.Items.Contains(obj2))
                {
                    list.Add(obj2);
                }
            }
            foreach (object obj3 in list)
            {
                this.newItems.Remove(obj3);
            }
        }

        internal static DXTabControl CloneCore(DXTabControl tab)
        {
            DXTabControl control = (DXTabControl) Activator.CreateInstance(tab.GetType());
            control.Name = tab.Name;
            control.SetCurrentValue(DXSerializer.EnabledProperty, DXSerializer.GetEnabled(tab));
            if (tab.View != null)
            {
                control.SetValue(ViewProperty, tab.View.Clone());
            }
            control.SetCurrentValue(TabContentCacheModeProperty, tab.TabContentCacheMode);
            control.SetCurrentValue(AllowMergingProperty, tab.AllowMerging);
            control.SetCurrentValue(ControlBoxLeftTemplateProperty, tab.ControlBoxLeftTemplate);
            control.SetCurrentValue(ControlBoxRightTemplateProperty, tab.ControlBoxRightTemplate);
            control.SetCurrentValue(ContentHeaderTemplateProperty, tab.ContentHeaderTemplate);
            control.SetCurrentValue(ContentFooterTemplateProperty, tab.ContentFooterTemplate);
            control.SetCurrentValue(ContentHostTemplateProperty, tab.ContentHostTemplate);
            control.SetCurrentValue(Control.PaddingProperty, tab.Padding);
            control.SetCurrentValue(FrameworkElement.MarginProperty, tab.Margin);
            control.SetCurrentValue(UIElement.SnapsToDevicePixelsProperty, tab.SnapsToDevicePixels);
            control.SetCurrentValue(FrameworkElement.UseLayoutRoundingProperty, tab.UseLayoutRounding);
            control.SetCurrentValue(ItemsControl.ItemTemplateProperty, tab.ItemTemplate);
            control.SetCurrentValue(ItemsControl.ItemTemplateSelectorProperty, tab.ItemTemplateSelector);
            control.SetCurrentValue(ItemsControl.ItemStringFormatProperty, tab.ItemStringFormat);
            control.SetCurrentValue(HeaderedSelectorBase<DXTabControl, DXTabItem>.ItemHeaderTemplateProperty, tab.ItemHeaderTemplate);
            control.SetCurrentValue(HeaderedSelectorBase<DXTabControl, DXTabItem>.ItemHeaderTemplateSelectorProperty, tab.ItemHeaderTemplateSelector);
            control.SetCurrentValue(HeaderedSelectorBase<DXTabControl, DXTabItem>.ItemHeaderStringFormatProperty, tab.ItemHeaderStringFormat);
            control.SetCurrentValue(ItemsControl.ItemContainerStyleProperty, tab.ItemContainerStyle);
            control.SetCurrentValue(ItemsControl.ItemContainerStyleSelectorProperty, tab.ItemContainerStyleSelector);
            control.PrepareTabItemDelegate = tab.PrepareTabItemDelegate;
            return control;
        }

        private object CoerceViewProperty(TabControlViewBase baseValue) => 
            ((baseValue == null) || !baseValue.CheckAccess()) ? new TabControlMultiLineView() : (((baseValue.Owner == null) || ReferenceEquals(baseValue.Owner, this)) ? baseValue : ((TabControlViewBase) baseValue.Clone()));

        protected override DXTabItem CreateContainer() => 
            new DXTabItem();

        protected override DXTabItem CreateContainerForNewItem()
        {
            DXTabItem item = this.CreateContainer();
            item.SetCurrentValue(DXTabItem.IsNewProperty, true);
            item.Header = "New DXTabItem";
            return item;
        }

        object IMultipleElementRegistratorSupport.GetName(object registratorKey) => 
            this.GetNameCore(registratorKey);

        internal void EndDragDrop()
        {
            this.IsDragDrop = false;
        }

        public void ForEachTabItem(Action<DXTabItem> action)
        {
            base.GetContainers().ToList<DXTabItem>().ForEach(action);
        }

        protected override int GetCoercedSelectedIndex(int index, NotifyCollectionChangedAction? originativeAction)
        {
            index = Math.Max(0, index);
            index = Math.Min(base.Items.Count - 1, index);
            return this.View.Return<TabControlViewBase, int>(x => x.CoerceSelection(index, originativeAction), () => this.<>n__0(index, originativeAction));
        }

        protected override bool GetIsNavigationInversed(FlowDirection flowDirection) => 
            (flowDirection != FlowDirection.LeftToRight) && ((this.View != null) && ((this.View.HeaderLocation != HeaderLocation.Right) && (this.View.HeaderLocation != HeaderLocation.Left)));

        public FrameworkElement GetLayoutChild(string childName) => 
            (FrameworkElement) base.GetTemplateChild(childName);

        protected virtual object GetNameCore(object registratorKey)
        {
            if (Equals(registratorKey, typeof(IFrameworkInputElement)))
            {
                return base.Name;
            }
            if (!Equals(registratorKey, typeof(IThemedWindowSupport)))
            {
                throw new ArgumentException("registratorKey");
            }
            return base.IsVisible;
        }

        [IteratorStateMachine(typeof(<GetRegistratorKeysCore>d__247))]
        protected virtual IEnumerable<object> GetRegistratorKeysCore()
        {
            yield return typeof(IFrameworkInputElement);
            yield return typeof(IThemedWindowSupport);
        }

        public DXTabItem GetTabItem(int index) => 
            base.GetContainer(index);

        public DXTabItem GetTabItem(object item) => 
            base.GetContainer(item);

        public bool HasFocusedTabItem()
        {
            bool res = false;
            this.ForEachTabItem(delegate (DXTabItem x) {
                res |= x.IsFocused;
            });
            return res;
        }

        public virtual void HideTabItem(int index, bool raiseEvents = true)
        {
            base.HideItem(index, raiseEvents);
        }

        public void HideTabItem(object item, bool raiseEvents = true)
        {
            this.HideTabItem(base.IndexOf(item), raiseEvents);
        }

        private void InitializeFastRenderOrContentPresenter()
        {
            if (this.fastRenderPanelWithoutContentHost != null)
            {
                if (this.TabContentCacheMode == DevExpress.Xpf.Core.TabContentCacheMode.None)
                {
                    this.UnInitializeFastRender(true);
                }
                else
                {
                    this.UnInitializeContentPresenter(true);
                    this.fastRenderPanelWithoutContentHost.Visibility = Visibility.Visible;
                    this.fastRenderPanelWithoutContentHost.Initialize((ItemsControl) this);
                }
            }
        }

        public virtual void InsertTabItem(object item, int index)
        {
            base.InsertItem(item, index);
        }

        public virtual void MoveTabItem(int oldIndex, int newIndex)
        {
            base.MoveItem(oldIndex, newIndex);
        }

        public void MoveTabItem(object item, int index)
        {
            this.MoveTabItem(base.IndexOf(item), index);
        }

        protected virtual void OnAllowMergingChanged()
        {
            ElementMergingBehavior internalWithInternal = ElementMergingBehavior.InternalWithInternal;
            if (this.AllowMerging != null)
            {
                internalWithInternal = this.AllowMerging.Value ? ElementMergingBehavior.InternalWithExternal : ElementMergingBehavior.Undefined;
            }
            MergingProperties.SetElementMergingBehavior(this, internalWithInternal);
            if (base.IsLoaded)
            {
                this.UpdateViewProperties();
            }
        }

        public override void OnApplyTemplate()
        {
            this.UnInitializeFastRender(false);
            this.UnInitializeContentPresenter(false);
            base.OnApplyTemplate();
            this.PrevButton = (RepeatButton) base.GetTemplateChild("PART_PrevButton");
            this.NextButton = (RepeatButton) base.GetTemplateChild("PART_NextButton");
            this.HeaderMenu = (ToggleButton) base.GetTemplateChild("PART_HeaderMenu");
            this.CloseButton = (Button) base.GetTemplateChild("PART_CloseButton");
            this.TabPanel = (TabPanelContainer) base.GetTemplateChild("PART_TabPanelContainer");
            this.ContentHostPresenter = (FrameworkElement) base.GetTemplateChild("PART_ContentHostPresenter");
            if (this.ContentHostTemplate == null)
            {
                this.fastRenderPanelWithoutContentHost = (DevExpress.Xpf.Core.Native.FastRenderPanel) base.GetTemplateChild("PART_FastRenderPanel");
                this.contentPresenterWithoutContentHost = (System.Windows.Controls.ContentPresenter) base.GetTemplateChild("PART_SelectedContentHost");
                this.InitializeFastRenderOrContentPresenter();
            }
        }

        protected internal override void OnContainerIsEnabledChanged(DXTabItem container, bool oldValue, bool newValue)
        {
            base.OnContainerIsEnabledChanged(container, oldValue, newValue);
            this.UpdateViewProperties();
        }

        protected internal override void OnContainerVisibilityChanged(DXTabItem container, Visibility oldValue, Visibility newValue)
        {
            base.OnContainerVisibilityChanged(container, oldValue, newValue);
            this.UpdateVisibleItemsCount();
            this.UpdateViewProperties();
        }

        protected override AutomationPeer OnCreateAutomationPeer() => 
            NavigationAutomationPeersCreator.Default.Create(this);

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            this.View ??= new TabControlMultiLineView();
        }

        protected override void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool) e.NewValue)
            {
                this.UpdateViewProperties();
            }
            if (ThemedWindowsHelper.GetAllowThemedWindowIntegration(this))
            {
                BarNameScope.GetService<IElementRegistratorService>(this).NameChanged(this, typeof(IThemedWindowSupport), e.OldValue, e.NewValue, false);
            }
        }

        protected override void OnItemContainerGeneratorStatusChanged(object sender, EventArgs e)
        {
            base.OnItemContainerGeneratorStatusChanged(sender, e);
            this.UpdateVisibleItemsCount();
            this.UpdateViewProperties();
            base.Dispatcher.BeginInvoke(new Action(this.UpdateViewProperties), DispatcherPriority.Render, new object[0]);
        }

        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);
            this.CheckNewItems();
            this.UpdateVisibleItemsCount();
            this.UpdateViewProperties();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            TabControlKeyboardController.OnTabControlKeyDown(this, e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            TabControlKeyboardController.OnTabControlKeyUp(this, e);
        }

        protected virtual void OnLoaded(object sender, RoutedEventArgs e)
        {
            FrameworkElementHelper.SetIsLoaded(this, true);
            this.UpdateViewProperties();
            base.Dispatcher.BeginInvoke(new Action(this.UpdateViewProperties), DispatcherPriority.Render, new object[0]);
        }

        protected virtual void OnNameChanged(string oldValue, string newValue)
        {
            BarNameScope.GetService<IElementRegistratorService>(this).NameChanged(this, typeof(IFrameworkInputElement), oldValue, newValue, false);
        }

        protected override void OnSelectedContainerChanged(DXTabItem oldValue, DXTabItem newValue)
        {
            base.OnSelectedContainerChanged(oldValue, newValue);
            if (this.allowUpdateSelectedTabItem)
            {
                base.SetCurrentValue(SelectedTabItemProperty, base.SelectedContainer);
            }
        }

        private void OnSelectedTabItemChanged()
        {
            this.allowUpdateSelectedTabItem = false;
            base.SetCurrentValue(SelectorBase<DXTabControl, DXTabItem>.SelectedContainerProperty, this.SelectedTabItem);
            this.allowUpdateSelectedTabItem = true;
        }

        protected virtual void OnUnloaded(object sender, RoutedEventArgs e)
        {
            FrameworkElementHelper.SetIsLoaded(this, false);
        }

        private void OnViewPropertyChanged(TabControlViewBase oldValue, TabControlViewBase newValue)
        {
            if (!ReferenceEquals(oldValue, newValue))
            {
                Action<TabControlViewBase> action = <>c.<>9__143_0;
                if (<>c.<>9__143_0 == null)
                {
                    Action<TabControlViewBase> local1 = <>c.<>9__143_0;
                    action = <>c.<>9__143_0 = x => x.Assign(null);
                }
                oldValue.Do<TabControlViewBase>(action);
                newValue.Do<TabControlViewBase>(x => x.Assign(this));
                this.UpdateViewProperties();
            }
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            DXTabItem tabItem = (DXTabItem) element;
            if (this.newItems.Contains(item))
            {
                tabItem.SetCurrentValue(DXTabItem.IsNewProperty, true);
                this.newItems.Remove(item);
            }
            base.PrepareContainerForItemOverride(element, item);
            if (!tabItem.IsPropertySet(Control.TabIndexProperty))
            {
                tabItem.TabIndex = base.IndexOf(tabItem);
            }
            this.PrepareTabItemDelegate.Do<DevExpress.Xpf.Core.PrepareTabItemDelegate>(x => x(tabItem, item));
        }

        protected override void RaiseItemAdded(int index, object item)
        {
            base.RaiseItemAdded(index, item);
            this.TabAdded.Do<TabControlTabAddedEventHandler>(x => x(this, new TabControlTabAddedEventArgs(index, item)));
        }

        protected override void RaiseItemAdding(out object item, CancelEventArgs e)
        {
            base.RaiseItemAdding(out item, e);
            TabControlTabAddingEventArgs args1 = new TabControlTabAddingEventArgs();
            args1.Cancel = e.Cancel;
            TabControlTabAddingEventArgs args = args1;
            this.TabAdding.Do<TabControlTabAddingEventHandler>(x => x(this, args));
            item = args.Item;
            e.Cancel = args.Cancel;
        }

        protected override void RaiseItemHidden(int index, object item)
        {
            base.RaiseItemHidden(index, item);
            this.TabHidden.Do<TabControlTabHiddenEventHandler>(x => x(this, new TabControlTabHiddenEventArgs(index, item)));
            this.UpdateViewProperties();
        }

        protected override void RaiseItemHiding(int index, object item, CancelEventArgs e)
        {
            base.RaiseItemHiding(index, item, e);
            TabControlTabHidingEventArgs args1 = new TabControlTabHidingEventArgs(index, item);
            args1.Cancel = e.Cancel;
            TabControlTabHidingEventArgs args = args1;
            this.TabHiding.Do<TabControlTabHidingEventHandler>(x => x(this, args));
            e.Cancel = args.Cancel;
        }

        protected override void RaiseItemInserted(int index, object item)
        {
            base.RaiseItemInserted(index, item);
            this.TabInserted.Do<TabControlTabInsertedEventHandler>(x => x(this, new TabControlTabInsertedEventArgs(index, item)));
        }

        protected override void RaiseItemInserting(int index, object item, CancelEventArgs e)
        {
            base.RaiseItemInserting(index, item, e);
            TabControlTabInsertingEventArgs args1 = new TabControlTabInsertingEventArgs(index, item);
            args1.Cancel = e.Cancel;
            TabControlTabInsertingEventArgs args = args1;
            this.TabInserting.Do<TabControlTabInsertingEventHandler>(x => x(this, args));
            e.Cancel = args.Cancel;
        }

        protected override void RaiseItemMoved(int oldIndex, int newIndex, object item)
        {
            base.RaiseItemMoved(oldIndex, newIndex, item);
            this.TabMoved.Do<TabControlTabMovedEventHandler>(x => x(this, new TabControlTabMovedEventArgs(item, oldIndex, newIndex)));
        }

        protected override void RaiseItemMoving(int oldIndex, int newIndex, object item, CancelEventArgs e)
        {
            base.RaiseItemMoving(oldIndex, newIndex, item, e);
            TabControlTabMovingEventArgs args1 = new TabControlTabMovingEventArgs(item, oldIndex, newIndex);
            args1.Cancel = e.Cancel;
            TabControlTabMovingEventArgs args = args1;
            this.TabMoving.Do<TabControlTabMovingEventHandler>(x => x(this, args));
            e.Cancel = args.Cancel;
        }

        protected override void RaiseItemRemoved(int index, object item)
        {
            base.RaiseItemRemoved(index, item);
            this.TabRemoved.Do<TabControlTabRemovedEventHandler>(x => x(this, new TabControlTabRemovedEventArgs(index, item, this.IsDragDrop)));
        }

        protected override void RaiseItemRemoving(int index, object item, CancelEventArgs e)
        {
            base.RaiseItemRemoving(index, item, e);
            TabControlTabRemovingEventArgs args1 = new TabControlTabRemovingEventArgs(index, item, this.IsDragDrop);
            args1.Cancel = e.Cancel;
            TabControlTabRemovingEventArgs args = args1;
            this.TabRemoving.Do<TabControlTabRemovingEventHandler>(x => x(this, args));
            e.Cancel = args.Cancel;
        }

        protected override void RaiseItemShowing(int index, object item, CancelEventArgs e)
        {
            base.RaiseItemShowing(index, item, e);
            TabControlTabShowingEventArgs args1 = new TabControlTabShowingEventArgs(index, item);
            args1.Cancel = e.Cancel;
            TabControlTabShowingEventArgs args = args1;
            this.TabShowing.Do<TabControlTabShowingEventHandler>(x => x(this, args));
            e.Cancel = args.Cancel;
        }

        protected override void RaiseItemShown(int index, object item)
        {
            base.RaiseItemShown(index, item);
            this.TabShown.Do<TabControlTabShownEventHandler>(x => x(this, new TabControlTabShownEventArgs(index, item)));
            this.UpdateViewProperties();
        }

        protected internal virtual TabControlNewTabbedWindowEventArgs RaiseNewTabbedWindow(object sourceData)
        {
            TabControlNewTabbedWindowEventArgs args = new TabControlNewTabbedWindowEventArgs(Window.GetWindow(this), this, sourceData);
            this.NewTabbedWindow.Do<TabControlNewTabbedWindowEventHandler>(delegate (TabControlNewTabbedWindowEventHandler x) {
                x(this, args);
            });
            return args;
        }

        protected override void RaiseSelectionChanged(int oldIndex, int newIndex, object oldItem, object newItem)
        {
            base.RaiseSelectionChanged(oldIndex, newIndex, oldItem, newItem);
            this.SelectionChanged.Do<TabControlSelectionChangedEventHandler>(x => x(this, new TabControlSelectionChangedEventArgs(oldIndex, newIndex, oldItem, newItem)));
            this.UpdateViewProperties();
        }

        protected override void RaiseSelectionChanging(int oldIndex, int newIndex, object oldItem, object newItem, CancelEventArgs e)
        {
            base.RaiseSelectionChanging(oldIndex, newIndex, oldItem, newItem, e);
            TabControlSelectionChangingEventArgs args1 = new TabControlSelectionChangingEventArgs(oldIndex, newIndex, oldItem, newItem);
            args1.Cancel = e.Cancel;
            TabControlSelectionChangingEventArgs args = args1;
            this.SelectionChanging.Do<TabControlSelectionChangingEventHandler>(x => x(this, args));
            e.Cancel = args.Cancel;
        }

        protected internal virtual TabControlTabDroppingOnEmptySpaceEventArgs RaiseTabDropping(DXTabItem item)
        {
            int index = base.IndexOf(item);
            TabControlTabDroppingOnEmptySpaceEventArgs args = new TabControlTabDroppingOnEmptySpaceEventArgs(index, base.Items[index]);
            this.TabDropping.Do<TabControlTabDroppingOnEmptySpaceEventHandler>(delegate (TabControlTabDroppingOnEmptySpaceEventHandler x) {
                x(this, args);
            });
            return args;
        }

        protected internal virtual TabControlTabStartDraggingEventArgs RaiseTabStartDragging(DXTabItem item)
        {
            int index = base.IndexOf(item);
            TabControlTabStartDraggingEventArgs args = new TabControlTabStartDraggingEventArgs(index, base.Items[index]);
            this.TabStartDragging.Do<TabControlTabStartDraggingEventHandler>(delegate (TabControlTabStartDraggingEventHandler x) {
                x(this, args);
            });
            return args;
        }

        public virtual void RemoveTabItem(int index)
        {
            base.RemoveItem(index);
        }

        public void RemoveTabItem(object item)
        {
            this.RemoveTabItem(base.IndexOf(item));
        }

        public void RestoreLayoutFromStream(Stream stream)
        {
            DXSerializer.DeserializeSingleObject(this, stream, base.GetType().Name);
        }

        public void RestoreLayoutFromXml(string path)
        {
            DXSerializer.DeserializeSingleObject(this, path, base.GetType().Name);
        }

        public void SaveLayoutToStream(Stream stream)
        {
            DXSerializer.SerializeSingleObject(this, stream, base.GetType().Name);
        }

        public void SaveLayoutToXml(string path)
        {
            DXSerializer.SerializeSingleObject(this, path, base.GetType().Name);
        }

        public virtual void SelectFirst()
        {
            base.SelectFirstItem();
        }

        public virtual void SelectLast()
        {
            base.SelectLastItem();
        }

        public virtual void SelectNext()
        {
            base.SelectNextItem(false);
        }

        public virtual void SelectPrev()
        {
            base.SelectPrevItem(false);
        }

        public virtual void SelectTabItem(int index)
        {
            NotifyCollectionChangedAction? originativeAction = null;
            base.SelectItem(this.GetCoercedSelectedIndex(index, originativeAction), false);
        }

        public void SelectTabItem(object item)
        {
            this.SelectTabItem(base.IndexOf(item));
        }

        public virtual void ShowTabItem(int index, bool raiseEvents = true)
        {
            base.ShowItem(index, raiseEvents);
        }

        public void ShowTabItem(object item, bool raiseEvents = true)
        {
            this.ShowTabItem(base.IndexOf(item), raiseEvents);
        }

        object ICloneable.Clone() => 
            CloneCore(this);

        private void UnInitializeContentPresenter(bool removeFromVisualTree)
        {
            if (this.contentPresenterWithoutContentHost != null)
            {
                this.contentPresenterWithoutContentHost.Visibility = Visibility.Collapsed;
                BindingOperations.ClearAllBindings(this.contentPresenterWithoutContentHost);
                this.contentPresenterWithoutContentHost.Content = null;
                this.contentPresenterWithoutContentHost.ContentTemplate = null;
                this.contentPresenterWithoutContentHost.ContentTemplateSelector = null;
                this.contentPresenterWithoutContentHost.ContentStringFormat = null;
                if (removeFromVisualTree)
                {
                    this.contentPresenterWithoutContentHost.RemoveFromVisualTree();
                }
                this.contentPresenterWithoutContentHost = null;
            }
        }

        private void UnInitializeFastRender(bool removeFromVisualTree)
        {
            if (this.fastRenderPanelWithoutContentHost != null)
            {
                this.fastRenderPanelWithoutContentHost.Uninitialize();
                if (removeFromVisualTree)
                {
                    this.fastRenderPanelWithoutContentHost.RemoveFromVisualTree();
                }
                this.fastRenderPanelWithoutContentHost = null;
            }
        }

        public void UpdateViewProperties()
        {
            TabViewInfo info = new TabViewInfo(this);
            if (!info.Equals((TabViewInfo) base.GetValue(ViewInfoProperty)))
            {
                base.SetValue(ViewInfoPropertyKey, info);
            }
            Action<DXTabItem> action = <>c.<>9__149_0;
            if (<>c.<>9__149_0 == null)
            {
                Action<DXTabItem> local1 = <>c.<>9__149_0;
                action = <>c.<>9__149_0 = tabItem => tabItem.UpdateViewPropertiesCore();
            }
            this.ForEachTabItem(action);
            Action<TabControlViewBase> action2 = <>c.<>9__149_1;
            if (<>c.<>9__149_1 == null)
            {
                Action<TabControlViewBase> local2 = <>c.<>9__149_1;
                action2 = <>c.<>9__149_1 = x => x.UpdateViewPropertiesCore();
            }
            this.View.Do<TabControlViewBase>(action2);
            Action<DXTabControlHeaderMenuInfo> action3 = <>c.<>9__149_2;
            if (<>c.<>9__149_2 == null)
            {
                Action<DXTabControlHeaderMenuInfo> local3 = <>c.<>9__149_2;
                action3 = <>c.<>9__149_2 = x => x.UpdateHasItems();
            }
            (base.GetValue(MenuInfoProperty) as DXTabControlHeaderMenuInfo).Do<DXTabControlHeaderMenuInfo>(action3);
        }

        private void UpdateVisibleItemsCount()
        {
            Func<DXTabItem, bool> predicate = <>c.<>9__120_0;
            if (<>c.<>9__120_0 == null)
            {
                Func<DXTabItem, bool> local1 = <>c.<>9__120_0;
                predicate = <>c.<>9__120_0 = x => x.Visibility == Visibility.Visible;
            }
            this.VisibleItemsCount = base.GetContainers().Where<DXTabItem>(predicate).Count<DXTabItem>();
        }

        public DataTemplate BackgroundTemplate
        {
            get => 
                (DataTemplate) base.GetValue(BackgroundTemplateProperty);
            set => 
                base.SetValue(BackgroundTemplateProperty, value);
        }

        public DataTemplate ControlBoxLeftTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ControlBoxLeftTemplateProperty);
            set => 
                base.SetValue(ControlBoxLeftTemplateProperty, value);
        }

        public DataTemplate ControlBoxRightTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ControlBoxRightTemplateProperty);
            set => 
                base.SetValue(ControlBoxRightTemplateProperty, value);
        }

        public DataTemplate ControlBoxPanelTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ControlBoxPanelTemplateProperty);
            set => 
                base.SetValue(ControlBoxPanelTemplateProperty, value);
        }

        public DataTemplate ContentHeaderTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ContentHeaderTemplateProperty);
            set => 
                base.SetValue(ContentHeaderTemplateProperty, value);
        }

        public DataTemplate ContentFooterTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ContentFooterTemplateProperty);
            set => 
                base.SetValue(ContentFooterTemplateProperty, value);
        }

        public DataTemplate ContentHostTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ContentHostTemplateProperty);
            set => 
                base.SetValue(ContentHostTemplateProperty, value);
        }

        [Obsolete("Use the SelectedContainer property.")]
        public DXTabItem SelectedTabItem
        {
            get => 
                (DXTabItem) base.GetValue(SelectedTabItemProperty);
            set => 
                base.SetValue(SelectedTabItemProperty, value);
        }

        [Obsolete("Use the TabContentCacheMode property.")]
        public bool DestroyContentOnTabSwitching
        {
            get => 
                (bool) base.GetValue(DestroyContentOnTabSwitchingProperty);
            set => 
                base.SetValue(DestroyContentOnTabSwitchingProperty, value);
        }

        [Description("Gets or sets whether the DXTabControl's tabs are cached all at once or only when selected.")]
        public DevExpress.Xpf.Core.TabContentCacheMode TabContentCacheMode
        {
            get => 
                (DevExpress.Xpf.Core.TabContentCacheMode) base.GetValue(TabContentCacheModeProperty);
            set => 
                base.SetValue(TabContentCacheModeProperty, value);
        }

        public bool? AllowMerging
        {
            get => 
                (bool?) base.GetValue(AllowMergingProperty);
            set => 
                base.SetValue(AllowMergingProperty, value);
        }

        [Description("Gets or sets a view of the DXTabControl.")]
        public TabControlViewBase View
        {
            get => 
                (TabControlViewBase) base.GetValue(ViewProperty);
            set => 
                base.SetValue(ViewProperty, value);
        }

        [Description("Gets the 'previous' scroll button.")]
        public RepeatButton PrevButton { get; private set; }

        [Description("Gets the 'next' scroll button.")]
        public RepeatButton NextButton { get; private set; }

        [Description("Gets the tab control's header menu button.")]
        public ToggleButton HeaderMenu { get; private set; }

        public Button CloseButton { get; private set; }

        [Description("Gets the panel that contains tab headers.")]
        public TabPanelContainer TabPanel { get; private set; }

        private FrameworkElement ContentHostPresenter { get; set; }

        protected internal DevExpress.Xpf.Core.Native.FastRenderPanel FastRenderPanel =>
            this.fastRenderPanelWithoutContentHost ?? (this.ContentHostPresenter as DevExpress.Xpf.Core.Native.ContentHostPresenter).With<DevExpress.Xpf.Core.Native.ContentHostPresenter, DevExpress.Xpf.Core.Native.FastRenderPanel>((<>c.<>9__95_0 ??= x => (x.ContentHostChild as DevExpress.Xpf.Core.Native.FastRenderPanel)));

        protected internal System.Windows.Controls.ContentPresenter ContentPresenter =>
            this.contentPresenterWithoutContentHost ?? (this.ContentHostPresenter as DevExpress.Xpf.Core.Native.ContentHostPresenter).With<DevExpress.Xpf.Core.Native.ContentHostPresenter, System.Windows.Controls.ContentPresenter>((<>c.<>9__97_0 ??= x => (x.ContentHostChild as System.Windows.Controls.ContentPresenter)));

        [XtraSerializableProperty(XtraSerializationVisibility.Content), EditorBrowsable(EditorBrowsableState.Never)]
        public DXTabControlSerializationInfo SerializationInfo { get; private set; }

        protected internal int VisibleItemsCount { get; private set; }

        private bool IsDragDrop { get; set; }

        IEnumerable<object> IMultipleElementRegistratorSupport.RegistratorKeys =>
            this.GetRegistratorKeysCore();

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DXTabControl.<>c <>9 = new DXTabControl.<>c();
            public static Func<ContentHostPresenter, FastRenderPanel> <>9__95_0;
            public static Func<ContentHostPresenter, ContentPresenter> <>9__97_0;
            public static Func<DXTabItem, bool> <>9__120_0;
            public static Action<TabControlViewBase> <>9__143_0;
            public static Action<DXTabItem> <>9__149_0;
            public static Action<TabControlViewBase> <>9__149_1;
            public static Action<DXTabControlHeaderMenuInfo> <>9__149_2;

            internal void <.cctor>b__104_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DXTabControl) d).OnNameChanged((string) e.OldValue, (string) e.NewValue);
            }

            internal void <.cctor>b__104_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DXTabControl) d).OnLogicalElementTemplateChanged(DXTabControl.ControlBoxLeftProperty, DXTabControl.ControlBoxLeftPropertyKey, (DataTemplate) e.NewValue);
            }

            internal object <.cctor>b__104_10(DependencyObject d, object e) => 
                ((DXTabControl) d).CoerceViewProperty(e as TabControlViewBase);

            internal void <.cctor>b__104_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DXTabControl) d).OnLogicalElementTemplateChanged(DXTabControl.ControlBoxRightProperty, DXTabControl.ControlBoxRightPropertyKey, (DataTemplate) e.NewValue);
            }

            internal void <.cctor>b__104_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DXTabControl) d).OnLogicalElementTemplateChanged(DXTabControl.ControlBoxPanelProperty, DXTabControl.ControlBoxPanelPropertyKey, (DataTemplate) e.NewValue);
            }

            internal void <.cctor>b__104_4(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DXTabControl) d).OnLogicalElementTemplateChanged(DXTabControl.ContentHeaderProperty, DXTabControl.ContentHeaderPropertyKey, (DataTemplate) e.NewValue);
            }

            internal void <.cctor>b__104_5(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DXTabControl) d).OnLogicalElementTemplateChanged(DXTabControl.ContentFooterProperty, DXTabControl.ContentFooterPropertyKey, (DataTemplate) e.NewValue);
            }

            internal void <.cctor>b__104_6(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DXTabControl) d).OnLogicalElementTemplateChanged(DXTabControl.ContentHostProperty, DXTabControl.ContentHostPropertyKey, (DataTemplate) e.NewValue);
            }

            internal void <.cctor>b__104_7(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DXTabControl) d).OnSelectedTabItemChanged();
            }

            internal void <.cctor>b__104_8(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DXTabControl) d).OnAllowMergingChanged();
            }

            internal void <.cctor>b__104_9(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DXTabControl) d).OnViewPropertyChanged((TabControlViewBase) e.OldValue, (TabControlViewBase) e.NewValue);
            }

            internal ContentPresenter <get_ContentPresenter>b__97_0(ContentHostPresenter x) => 
                x.ContentHostChild as ContentPresenter;

            internal FastRenderPanel <get_FastRenderPanel>b__95_0(ContentHostPresenter x) => 
                x.ContentHostChild as FastRenderPanel;

            internal void <OnViewPropertyChanged>b__143_0(TabControlViewBase x)
            {
                x.Assign(null);
            }

            internal void <UpdateViewProperties>b__149_0(DXTabItem tabItem)
            {
                tabItem.UpdateViewPropertiesCore();
            }

            internal void <UpdateViewProperties>b__149_1(TabControlViewBase x)
            {
                x.UpdateViewPropertiesCore();
            }

            internal void <UpdateViewProperties>b__149_2(DXTabControlHeaderMenuInfo x)
            {
                x.UpdateHasItems();
            }

            internal bool <UpdateVisibleItemsCount>b__120_0(DXTabItem x) => 
                x.Visibility == Visibility.Visible;
        }

    }
}

