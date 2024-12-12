namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.LayoutControl.UIAutomation;
    using DevExpress.Xpf.Utils.About;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Xml;

    [StyleTypedProperty(Property="GroupBoxStyle", StyleTargetType=typeof(DevExpress.Xpf.LayoutControl.GroupBox)), StyleTypedProperty(Property="ItemStyle", StyleTargetType=typeof(LayoutItem)), StyleTypedProperty(Property="TabsStyle", StyleTargetType=typeof(DXTabControl)), StyleTypedProperty(Property="TabStyle", StyleTargetType=typeof(DXTabItem)), DXToolboxBrowsable(DXToolboxItemKind.Free), LicenseProvider(typeof(DX_WPF_ControlRequiredForReports_LicenseProvider))]
    public class LayoutGroup : LayoutControlBase, ILayoutGroup, ILayoutControlBase, IScrollControl, IPanel, IControl, ILayoutModelBase, ILayoutGroupModel, ILiveCustomizationAreasProvider, ILayoutControlCustomizableItem
    {
        public const System.Windows.Controls.Orientation DefaultOrientation = System.Windows.Controls.Orientation.Horizontal;
        public static readonly DependencyProperty GroupBoxStyleProperty;
        public static readonly DependencyProperty HeaderProperty;
        public static readonly DependencyProperty HeaderTemplateProperty;
        public static readonly DependencyProperty IsCollapsedProperty;
        public static readonly DependencyProperty IsCollapsibleProperty;
        public static readonly DependencyProperty IsLockedProperty;
        public static readonly DependencyProperty ItemLabelsAlignmentProperty;
        public static readonly DependencyProperty ItemStyleProperty;
        public static readonly DependencyProperty KeepSelectionOnTabRemovalProperty;
        public static readonly DependencyProperty MeasureSelectedTabChildOnlyProperty;
        public static readonly DependencyProperty OrientationProperty;
        public static readonly DependencyProperty SelectedTabIndexProperty;
        protected static readonly DependencyProperty StoredSizeProperty;
        public static readonly DependencyProperty TabsStyleProperty;
        public static readonly DependencyProperty TabStyleProperty;
        public static readonly DependencyProperty ViewProperty;
        public static readonly DependencyProperty GroupBoxDisplayModeProperty;
        private static readonly DependencyPropertyKey ActualGroupBoxDisplayModePropertyKey;
        public static readonly DependencyProperty ActualGroupBoxDisplayModeProperty;
        private bool _IsActuallyCollapsed;
        private bool _IsCustomization;
        private object _StoredMinSizePropertyValue;
        private HorizontalAlignment? _lastDesiredHorizontalAlignment;
        private VerticalAlignment? _lastDesiredVerticalAlignment;
        private static readonly DependencyProperty TabNavigationInfoProperty;
        private static readonly DependencyProperty ControlTabNavigationInfoProperty;
        protected static DependencyProperty ChildHorizontalAlignmentListener;
        protected static DependencyProperty ChildVerticalAlignmentListener;
        protected static DependencyProperty ChildWidthListener;
        protected static DependencyProperty ChildHeightListener;
        protected static DependencyProperty ChildMaxWidthListener;
        protected static DependencyProperty ChildMaxHeightListener;
        protected static DependencyProperty ChildStyleListener;
        protected static DependencyProperty ChildVisibilityListener;
        private static DependencyProperty CollapseDirectionProperty;
        private bool _HasTabs;
        private FrameworkElement _SelectedTabChild;
        private static readonly DependencyProperty IsItemStyleSetByGroupProperty;

        public event EventHandler Collapsed;

        public event EventHandler Expanded;

        public event ValueChangedEventHandler<FrameworkElement> SelectedTabChildChanged;

        static LayoutGroup()
        {
            GroupBoxStyleProperty = DependencyProperty.Register("GroupBoxStyle", typeof(Style), typeof(LayoutGroup), new PropertyMetadata((o, e) => ((LayoutGroup) o).OnPartStyleChanged(LayoutGroupPartStyle.GroupBox)));
            HeaderProperty = DependencyProperty.Register("Header", typeof(object), typeof(LayoutGroup), new PropertyMetadata((o, e) => ((LayoutGroup) o).OnHeaderChanged()));
            HeaderTemplateProperty = DependencyProperty.Register("HeaderTemplate", typeof(DataTemplate), typeof(LayoutGroup), new PropertyMetadata((o, e) => ((LayoutGroup) o).OnHeaderTemplateChanged()));
            IsCollapsedProperty = DependencyProperty.Register("IsCollapsed", typeof(bool), typeof(LayoutGroup), new PropertyMetadata((o, e) => ((LayoutGroup) o).OnIsCollapsedChanged()));
            IsCollapsibleProperty = DependencyProperty.Register("IsCollapsible", typeof(bool), typeof(LayoutGroup), new PropertyMetadata((o, e) => ((LayoutGroup) o).OnIsCollapsibleChanged()));
            IsLockedProperty = DependencyProperty.Register("IsLocked", typeof(bool), typeof(LayoutGroup), null);
            ItemLabelsAlignmentProperty = DependencyProperty.Register("ItemLabelsAlignment", typeof(LayoutItemLabelsAlignment), typeof(LayoutGroup), new PropertyMetadata((o, e) => ((LayoutGroup) o).OnItemLabelsAlignmentChanged()));
            ItemStyleProperty = DependencyProperty.Register("ItemStyle", typeof(Style), typeof(LayoutGroup), new PropertyMetadata((o, e) => ((LayoutGroup) o).OnItemStyleChanged()));
            KeepSelectionOnTabRemovalProperty = DependencyProperty.Register("KeepSelectionOnTabRemoval", typeof(bool), typeof(LayoutGroup));
            MeasureSelectedTabChildOnlyProperty = DependencyProperty.Register("MeasureSelectedTabChildOnly", typeof(bool), typeof(LayoutGroup), new PropertyMetadata((o, e) => ((LayoutGroup) o).OnMeasureSelectedTabChildOnlyChanged()));
            OrientationProperty = DependencyProperty.Register("Orientation", typeof(System.Windows.Controls.Orientation), typeof(LayoutGroup), new PropertyMetadata(System.Windows.Controls.Orientation.Horizontal, (o, e) => ((LayoutGroup) o).OnOrientationChanged()));
            SelectedTabIndexProperty = DependencyProperty.Register("SelectedTabIndex", typeof(int), typeof(LayoutGroup), new PropertyMetadata(delegate (DependencyObject o, DependencyPropertyChangedEventArgs e) {
                if (((int) e.NewValue) < 0)
                {
                    o.SetValue(e.Property, e.OldValue);
                }
                else
                {
                    ((LayoutGroup) o).OnSelectedTabIndexChanged((int) e.OldValue);
                }
            }));
            StoredSizeProperty = DependencyProperty.Register("StoredSize", typeof(double), typeof(LayoutGroup), new PropertyMetadata((double) 1.0 / (double) 0.0));
            TabsStyleProperty = DependencyProperty.Register("TabsStyle", typeof(Style), typeof(LayoutGroup), new PropertyMetadata((o, e) => ((LayoutGroup) o).OnPartStyleChanged(LayoutGroupPartStyle.Tabs)));
            TabStyleProperty = DependencyProperty.Register("TabStyle", typeof(Style), typeof(LayoutGroup), new PropertyMetadata((o, e) => ((LayoutGroup) o).OnPartStyleChanged(LayoutGroupPartStyle.Tab)));
            ViewProperty = DependencyProperty.Register("View", typeof(LayoutGroupView), typeof(LayoutGroup), new PropertyMetadata((o, e) => ((LayoutGroup) o).OnViewChanged()));
            GroupBoxDisplayModeProperty = DependencyProperty.Register("GroupBoxDisplayMode", typeof(DevExpress.Xpf.LayoutControl.GroupBoxDisplayMode), typeof(LayoutGroup), new PropertyMetadata(DevExpress.Xpf.LayoutControl.GroupBoxDisplayMode.Default, (d, e) => ((LayoutGroup) d).UpdateActualGroupBoxDisplayMode()));
            ActualGroupBoxDisplayModePropertyKey = DependencyProperty.RegisterReadOnly("ActualGroupBoxDisplayMode", typeof(DevExpress.Xpf.LayoutControl.GroupBoxDisplayMode), typeof(LayoutGroup), new PropertyMetadata(DevExpress.Xpf.LayoutControl.GroupBoxDisplayMode.Default, (d, e) => ((LayoutGroup) d).InitGroupBoxDisplayMode()));
            ActualGroupBoxDisplayModeProperty = ActualGroupBoxDisplayModePropertyKey.DependencyProperty;
            TabNavigationInfoProperty = DependencyProperty.RegisterAttached("TabNavigationInfo", typeof(object), typeof(LayoutGroup), null);
            ControlTabNavigationInfoProperty = DependencyProperty.RegisterAttached("ControlTabNavigationInfo", typeof(object), typeof(LayoutGroup), null);
            ChildHorizontalAlignmentListener = RegisterChildPropertyListener("HorizontalAlignment", typeof(LayoutGroup));
            ChildVerticalAlignmentListener = RegisterChildPropertyListener("VerticalAlignment", typeof(LayoutGroup));
            ChildWidthListener = RegisterChildPropertyListener("Width", typeof(LayoutGroup));
            ChildHeightListener = RegisterChildPropertyListener("Height", typeof(LayoutGroup));
            ChildMaxWidthListener = RegisterChildPropertyListener("MaxWidth", typeof(LayoutGroup));
            ChildMaxHeightListener = RegisterChildPropertyListener("MaxHeight", typeof(LayoutGroup));
            ChildStyleListener = RegisterChildPropertyListener("Style", typeof(LayoutGroup));
            ChildVisibilityListener = RegisterChildPropertyListener("Visibility", typeof(LayoutGroup));
            CollapseDirectionProperty = DependencyProperty.Register("CollapseDirection", typeof(System.Windows.Controls.Orientation), typeof(LayoutGroup), new PropertyMetadata((o, e) => ((LayoutGroup) o).OnCollapseDirectionChanged()));
            IsItemStyleSetByGroupProperty = DependencyProperty.RegisterAttached("IsItemStyleSetByGroup", typeof(bool), typeof(LayoutGroup), null);
            LayoutControlBase.PaddingProperty.OverrideMetadata(typeof(LayoutGroup), new PropertyMetadata(new Thickness(0.0)));
            DependencyPropertyRegistrator<LayoutGroup>.New().OverrideDefaultStyleKey();
        }

        public LayoutGroup()
        {
            this.ItemSizers = this.CreateItemSizers();
            this.ItemSizers.SizingAreaWidth = base.ItemSpace;
            this._IsActuallyCollapsed = this.IsActuallyCollapsed;
            About.CheckLicenseShowNagScreen(typeof(LayoutGroup));
        }

        protected override void AddChildFromXML(IList children, FrameworkElement element, int index)
        {
            DependencyObject parent = element.Parent;
            ILayoutControl root = this.Root;
            if (root == null)
            {
                ILayoutControl local1 = root;
            }
            else
            {
                root.AvailableItems.Remove(element);
            }
            base.AddChildFromXML(children, element, index);
            if (ReferenceEquals(element.Parent, this) && !ReferenceEquals(parent, this))
            {
                this.HasNewChildren = true;
            }
        }

        public void ApplyItemStyle()
        {
            foreach (FrameworkElement element in base.GetLogicalChildren(false))
            {
                this.ApplyItemStyle(element);
            }
        }

        protected void ApplyItemStyle(LayoutItem item)
        {
            if ((item.Style == null) || GetIsItemStyleSetByGroup(item))
            {
                item.SetValueIfNotDefault(FrameworkElement.StyleProperty, this.GetItemStyle());
                SetIsItemStyleSetByGroup(item, true);
            }
        }

        protected void ApplyItemStyle(FrameworkElement element)
        {
            if (element is LayoutItem)
            {
                this.ApplyItemStyle((LayoutItem) element);
            }
            else if (element.IsLayoutGroup())
            {
                ((ILayoutGroup) element).ApplyItemStyle();
            }
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            this.HasNewChildren = false;
            return base.ArrangeOverride(finalSize);
        }

        protected override void AttachChildPropertyListeners(FrameworkElement child)
        {
            base.AttachChildPropertyListeners(child);
            AttachChildPropertyListener(child, "HorizontalAlignment", ChildHorizontalAlignmentListener);
            AttachChildPropertyListener(child, "VerticalAlignment", ChildVerticalAlignmentListener);
            AttachChildPropertyListener(child, "Width", ChildWidthListener);
            AttachChildPropertyListener(child, "Height", ChildHeightListener);
            AttachChildPropertyListener(child, "MaxWidth", ChildMaxWidthListener);
            AttachChildPropertyListener(child, "MaxHeight", ChildMaxHeightListener);
            AttachChildPropertyListener(child, "Style", ChildStyleListener);
            AttachChildPropertyListener(child, "Visibility", ChildVisibilityListener);
        }

        private IEnumerable<UIElement> BaseGetInternalElements() => 
            base.GetInternalElements();

        private void BindTabControlForeground()
        {
            if (this.TabControl != null)
            {
                if (System.Windows.DependencyPropertyHelper.GetValueSource(this.TabControl, Control.ForegroundProperty).BaseValueSource <= BaseValueSource.Inherited)
                {
                    base.ClearValue(Control.ForegroundProperty);
                }
                else
                {
                    Binding binding = new Binding("Foreground");
                    binding.Source = this.TabControl;
                    base.SetBinding(Control.ForegroundProperty, binding);
                }
            }
        }

        protected virtual bool CanItemSizing() => 
            (this.Root != null) && this.Root.AllowItemSizing;

        protected void CheckGroupBox()
        {
            if (this.HasGroupBox && (this.GroupBox == null))
            {
                this.GroupBox = this.CreateGroupBox();
                this.InitGroupBox();
                base.Children.Add(this.GroupBox);
            }
            if (!this.HasGroupBox && (this.GroupBox != null))
            {
                base.Children.Remove(this.GroupBox);
                this.GroupBox = null;
            }
        }

        protected void CheckHasTabsChanged()
        {
            if (this.HasTabs != this._HasTabs)
            {
                this._HasTabs = this.HasTabs;
                this.OnHasTabsChanged();
            }
        }

        protected void CheckIsActuallyCollapsedChanged()
        {
            if (this.IsActuallyCollapsed != this._IsActuallyCollapsed)
            {
                this._IsActuallyCollapsed = this.IsActuallyCollapsed;
                this.OnIsActuallyCollapsedChanged();
            }
        }

        protected void CheckSelectedTabChildChanged()
        {
            if (this.HasTabs)
            {
                FrameworkElement selectedTabChild = this.SelectedTabChild;
                if (!ReferenceEquals(selectedTabChild, this._SelectedTabChild))
                {
                    FrameworkElement oldValue = this._SelectedTabChild;
                    this._SelectedTabChild = selectedTabChild;
                    this.OnSelectedTabChildChanged(oldValue);
                }
            }
        }

        protected void CheckTabControl()
        {
            if (this.HasTabs && (this.TabControl == null))
            {
                this.TabControl = this.CreateTabControl();
                ThemedWindowsHelper.SetAllowThemedWindowIntegration(this.TabControl, false);
                this.InitTabControl();
                if (this.IsInDesignTool())
                {
                    base.Children.Add(this.TabControl);
                }
                else
                {
                    base.Children.Insert(0, this.TabControl);
                }
                this.BindTabControlForeground();
                ThemeManager.AddThemeChangedHandler(this.TabControl, new ThemeChangedRoutedEventHandler(this.OnTabControlThemeChanged));
            }
            if (!this.HasTabs && (this.TabControl != null))
            {
                ThemeManager.RemoveThemeChangedHandler(this.TabControl, new ThemeChangedRoutedEventHandler(this.OnTabControlThemeChanged));
                base.ClearValue(Control.ForegroundProperty);
                this.FinalizeTabControl();
                base.Children.Remove(this.TabControl);
                this.TabControl.ClearValue(ThemedWindowsHelper.AllowThemedWindowIntegrationProperty);
                this.TabControl = null;
            }
        }

        protected void CheckTabControlIndex()
        {
            if (this.HasTabs && !this.IsInDesignTool())
            {
                int index = base.Children.IndexOf(this.TabControl);
                if (index > 0)
                {
                    base.Children.RemoveAt(index);
                    base.Children.Insert(0, this.TabControl);
                }
            }
        }

        protected void CheckUnfocusableState(FrameworkElement child)
        {
            if (this.IsChildUnfocusable(child))
            {
                this.InitializeUnfocusableState(child);
            }
            else
            {
                this.FinalizeUnfocusableState(child);
            }
        }

        protected void CheckUnfocusableStateForChildren()
        {
            foreach (FrameworkElement element in base.Children)
            {
                this.CheckUnfocusableState(element);
            }
        }

        protected void ClearItemStyle()
        {
            if (this.GetItemStyle() == null)
            {
                foreach (FrameworkElement element in base.GetLogicalChildren(false))
                {
                    this.ClearItemStyle(element);
                }
            }
        }

        protected void ClearItemStyle(LayoutItem item)
        {
            if (GetIsItemStyleSetByGroup(item))
            {
                SetIsItemStyleSetByGroup(item, false);
                item.ClearValue(FrameworkElement.StyleProperty);
            }
        }

        protected void ClearItemStyle(FrameworkElement element)
        {
            if (element is LayoutItem)
            {
                this.ClearItemStyle((LayoutItem) element);
            }
            else if (element.IsLayoutGroup())
            {
                ((ILayoutGroup) element).ClearItemStyle();
            }
        }

        protected void CopyTabHeaderInfo(FrameworkElement fromChild, FrameworkElement toElement)
        {
            LayoutGroup o = toElement as LayoutGroup;
            object tabHeader = this.GetTabHeader(fromChild, false);
            DataTemplate tabHeaderTemplate = this.GetTabHeaderTemplate(fromChild);
            if ((o == null) || o.IsRoot)
            {
                DevExpress.Xpf.LayoutControl.LayoutControl.SetTabHeader(toElement, tabHeader);
                DevExpress.Xpf.LayoutControl.LayoutControl.SetTabHeaderTemplate(toElement, tabHeaderTemplate);
            }
            else if (!this.IsInDesignTool())
            {
                o.SetValueIfNotDefault(HeaderProperty, tabHeader);
                o.SetValueIfNotDefault(HeaderTemplateProperty, tabHeaderTemplate);
            }
        }

        protected override PanelControllerBase CreateController() => 
            new LayoutGroupController(this);

        public virtual LayoutGroup CreateGroup()
        {
            LayoutGroup group = (LayoutGroup) this.GetGroupType().GetConstructor(Type.EmptyTypes).Invoke(null);
            if (this.GroupBoxDisplayMode != DevExpress.Xpf.LayoutControl.GroupBoxDisplayMode.Default)
            {
                group.GroupBoxDisplayMode = this.GroupBoxDisplayMode;
            }
            return group;
        }

        protected virtual DevExpress.Xpf.LayoutControl.GroupBox CreateGroupBox() => 
            new DevExpress.Xpf.LayoutControl.GroupBox();

        protected virtual ElementSizers CreateItemSizers() => 
            new ElementSizers(this);

        protected override LayoutProviderBase CreateLayoutProvider() => 
            new LayoutGroupProvider(this);

        protected override LayoutParametersBase CreateLayoutProviderParameters() => 
            new LayoutGroupParameters(base.ItemSpace, this.CanItemSizing() ? this.ItemSizers : null);

        protected virtual DXTabItem CreateTab() => 
            new DXTabItem();

        protected virtual DXTabControl CreateTabControl() => 
            new DXTabControl();

        protected override void DetachChildPropertyListeners(FrameworkElement child)
        {
            base.DetachChildPropertyListeners(child);
            if (!(child.Parent is LayoutGroup))
            {
                DetachChildPropertyListener(child, ChildHorizontalAlignmentListener);
                DetachChildPropertyListener(child, ChildVerticalAlignmentListener);
                DetachChildPropertyListener(child, ChildWidthListener);
                DetachChildPropertyListener(child, ChildHeightListener);
                DetachChildPropertyListener(child, ChildMaxWidthListener);
                DetachChildPropertyListener(child, ChildMaxHeightListener);
                DetachChildPropertyListener(child, ChildStyleListener);
                DetachChildPropertyListener(child, ChildVisibilityListener);
            }
        }

        FrameworkElement ILayoutControlCustomizableItem.AddNewItem()
        {
            if (!this.HasTabs)
            {
                return null;
            }
            LayoutGroup element = this.CreateGroup();
            base.Children.Add(element);
            return element;
        }

        void ILayoutGroup.AllowItemSizingChanged()
        {
            this.OnAllowItemSizingChanged();
        }

        void ILayoutGroup.ChildHorizontalAlignmentChanged(FrameworkElement child)
        {
            this.OnChildHorizontalAlignmentChanged(child);
        }

        void ILayoutGroup.ChildVerticalAlignmentChanged(FrameworkElement child)
        {
            this.OnChildVerticalAlignmentChanged(child);
        }

        void ILayoutGroup.ClearItemStyle()
        {
            this.ClearItemStyle();
        }

        void ILayoutGroup.CopyTabHeaderInfo(FrameworkElement fromChild, FrameworkElement toElement)
        {
            this.CopyTabHeaderInfo(fromChild, toElement);
        }

        bool ILayoutGroup.DesignTimeClick(DXMouseButtonEventArgs args) => 
            this.OnDesignTimeClick(args);

        Rect ILayoutGroup.GetClipBounds(FrameworkElement child, FrameworkElement relativeTo) => 
            this.LayoutProvider.GetClipBounds(this, child, relativeTo);

        [IteratorStateMachine(typeof(<DevExpress-Xpf-LayoutControl-ILayoutGroup-GetDependencyPropertiesWithOverriddenDefaultValue>d__377))]
        IEnumerable<string> ILayoutGroup.GetDependencyPropertiesWithOverriddenDefaultValue() => 
            new <DevExpress-Xpf-LayoutControl-ILayoutGroup-GetDependencyPropertiesWithOverriddenDefaultValue>d__377(-2);

        LayoutItemInsertionInfo ILayoutGroup.GetInsertionInfoForEmptyArea(FrameworkElement element, Point p) => 
            this.LayoutProvider.GetInsertionInfoForEmptyArea(this, element, p);

        LayoutItemInsertionKind ILayoutGroup.GetInsertionKind(FrameworkElement destinationItem, Point p) => 
            (ReferenceEquals(destinationItem, this) || (!destinationItem.IsLayoutGroup() || ((ILayoutGroup) destinationItem).IsLocked)) ? this.LayoutProvider.GetInsertionKind(this, destinationItem, p) : ((ILayoutGroup) destinationItem).GetInsertionKind(destinationItem, p);

        LayoutItemInsertionPoint ILayoutGroup.GetInsertionPoint(FrameworkElement element, FrameworkElement destinationItem, LayoutItemInsertionKind insertionKind, Point p)
        {
            LayoutItemInsertionPoints points = new LayoutItemInsertionPoints();
            ((ILayoutGroup) this).GetInsertionPoints(element, destinationItem, destinationItem, insertionKind, points);
            for (int i = 0; i < points.Count; i++)
            {
                Rect rect = ((ILayoutGroup) this).GetInsertionPointZoneBounds(destinationItem, insertionKind, i, points.Count);
                if (rect.Contains(p))
                {
                    return points[i];
                }
            }
            return null;
        }

        Rect ILayoutGroup.GetInsertionPointBounds(bool isInternalInsertion, FrameworkElement relativeTo) => 
            !isInternalInsertion ? this.GetBounds(relativeTo) : this.MapRect(base.ContentBounds, relativeTo);

        void ILayoutGroup.GetInsertionPoints(FrameworkElement element, FrameworkElement destinationItem, FrameworkElement originalDestinationItem, LayoutItemInsertionKind insertionKind, LayoutItemInsertionPoints points)
        {
            this.LayoutProvider.GetInsertionPoints(this, element, destinationItem, originalDestinationItem, insertionKind, points);
        }

        Rect ILayoutGroup.GetInsertionPointZoneBounds(FrameworkElement destinationItem, LayoutItemInsertionKind insertionKind, int pointIndex, int pointCount) => 
            (ReferenceEquals(destinationItem, this) || (!destinationItem.IsLayoutGroup() || ((ILayoutGroup) destinationItem).IsLocked)) ? this.LayoutProvider.GetInsertionPointZoneBounds(this, destinationItem, insertionKind, pointIndex, pointCount) : ((ILayoutGroup) destinationItem).GetInsertionPointZoneBounds(destinationItem, insertionKind, pointIndex, pointCount);

        FrameworkElement ILayoutGroup.GetItem(Point p, bool ignoreLayoutGroups, bool ignoreLocking) => 
            this.Controller.GetItem(p, ignoreLayoutGroups, ignoreLocking);

        HorizontalAlignment ILayoutGroup.GetItemHorizontalAlignment(FrameworkElement item) => 
            this.LayoutProvider.GetItemHorizontalAlignment(item);

        Style ILayoutGroup.GetItemStyle() => 
            this.GetItemStyle();

        VerticalAlignment ILayoutGroup.GetItemVerticalAlignment(FrameworkElement item) => 
            this.LayoutProvider.GetItemVerticalAlignment(item);

        void ILayoutGroup.GetLayoutItems(LayoutItems layoutItems)
        {
            if (!this.IsItemLabelsAlignmentScope)
            {
                this.LayoutProvider.GetLayoutItems(base.GetLogicalChildren(true), layoutItems);
            }
        }

        int ILayoutGroup.GetTabIndex(Point absolutePosition) => 
            this.HasTabs ? this.GetTabIndex(absolutePosition) : -1;

        void ILayoutGroup.InitChildFromGroup(FrameworkElement child, FrameworkElement group)
        {
            this.LayoutProvider.InitChildFromGroup(child, group);
        }

        void ILayoutGroup.InsertElement(FrameworkElement element, LayoutItemInsertionPoint insertionPoint, LayoutItemInsertionKind insertionKind)
        {
            this.LayoutProvider.InsertElement(this, element, insertionPoint, insertionKind);
        }

        bool ILayoutGroup.IsChildBorderless(ILayoutGroup child) => 
            this.IsChildBorderless(child);

        bool ILayoutGroup.IsChildPermanent(ILayoutGroup child, bool keepTabs) => 
            this.IsChildPermanent(child, keepTabs);

        bool ILayoutGroup.IsExternalInsertionPoint(FrameworkElement element, FrameworkElement destinationItem, LayoutItemInsertionKind insertionKind) => 
            this.LayoutProvider.IsExternalInsertionPoint(this, element, destinationItem, insertionKind);

        bool ILayoutGroup.IsRemovableForOptimization(bool considerContent, bool keepEmptyTabs) => 
            this.IsRemovableForOptimization(considerContent, keepEmptyTabs);

        void ILayoutGroup.LayoutItemLabelsAlignmentChanged()
        {
            this.UpdateItemLabelsAlignment();
        }

        void ILayoutGroup.LayoutItemLabelWidthChanged(FrameworkElement layoutItem)
        {
            this.ItemLabelsAlignmentChanged();
        }

        bool ILayoutGroup.MakeChildVisible(FrameworkElement child) => 
            this.MakeChildVisible(child);

        void ILayoutGroup.MoveNonUserDefinedChildrenToAvailableItems()
        {
            this.MoveNonUserDefinedChildrenToAvailableItems();
        }

        void ILayoutGroup.SetItemHorizontalAlignment(FrameworkElement item, HorizontalAlignment value, bool updateWidth)
        {
            this.LayoutProvider.SetItemHorizontalAlignment(item, value, updateWidth);
        }

        void ILayoutGroup.SetItemVerticalAlignment(FrameworkElement item, VerticalAlignment value, bool updateHeight)
        {
            this.LayoutProvider.SetItemVerticalAlignment(item, value, updateHeight);
        }

        void ILayoutGroup.UpdatePartStyle(LayoutGroupPartStyle style)
        {
            this.InitPartStyle(style);
            this.ForEachGroup(group => group.UpdatePartStyle(style));
        }

        void ILiveCustomizationAreasProvider.GetLiveCustomizationAreas(IList<Rect> areas, FrameworkElement relativeTo)
        {
            Rect clipBounds = this.GetClipBounds(relativeTo);
            if (!clipBounds.IsEmpty)
            {
                foreach (Rect rect2 in this.OnGetLiveCustomizationAreas(relativeTo))
                {
                    rect2.Intersect(clipBounds);
                    if (!rect2.IsEmpty)
                    {
                        areas.Add(rect2);
                    }
                }
                foreach (FrameworkElement element in base.GetLogicalChildren(true))
                {
                    if (element is ILiveCustomizationAreasProvider)
                    {
                        ((ILiveCustomizationAreasProvider) element).GetLiveCustomizationAreas(areas, relativeTo);
                    }
                }
            }
        }

        protected virtual void FinalizeTab(DXTabItem tab)
        {
            tab.Tag = null;
        }

        protected virtual void FinalizeTabControl()
        {
            this.FinalizeTabs();
        }

        protected virtual void FinalizeTabs()
        {
            foreach (DXTabItem item in (IEnumerable) this.TabControl.Items)
            {
                this.FinalizeTab(item);
            }
        }

        protected void FinalizeUnfocusableState(FrameworkElement child)
        {
            this.RestorePropertyValue(child, ControlTabNavigationInfoProperty, KeyboardNavigation.ControlTabNavigationProperty);
            this.RestorePropertyValue(child, TabNavigationInfoProperty, KeyboardNavigation.TabNavigationProperty);
        }

        protected override FrameworkElement FindByXMLID(string id)
        {
            FrameworkElement element = this.Root.FindControl(id);
            if ((element != null) && DevExpress.Xpf.LayoutControl.LayoutControl.GetIsUserDefined(element))
            {
                Func<DevExpress.Xpf.LayoutControl.LayoutControl, bool> evaluator = <>c.<>9__290_0;
                if (<>c.<>9__290_0 == null)
                {
                    Func<DevExpress.Xpf.LayoutControl.LayoutControl, bool> local1 = <>c.<>9__290_0;
                    evaluator = <>c.<>9__290_0 = x => x.AddRestoredItemsToAvailableItems;
                }
                if ((this.Root as DevExpress.Xpf.LayoutControl.LayoutControl).Return<DevExpress.Xpf.LayoutControl.LayoutControl, bool>(evaluator, <>c.<>9__290_1 ??= () => true))
                {
                    this.Root.SetID(element);
                    element = null;
                }
            }
            return element;
        }

        protected void ForEachGroup(Action<ILayoutGroup> action)
        {
            foreach (FrameworkElement element in base.GetLogicalChildren(false))
            {
                if (element.IsLayoutGroup())
                {
                    action((ILayoutGroup) element);
                }
            }
        }

        public FrameworkElements GetArrangedLogicalChildren(bool visibleOnly)
        {
            FrameworkElements logicalChildren = base.GetLogicalChildren(visibleOnly);
            this.LayoutProvider.ArrangeItems(logicalChildren);
            return logicalChildren;
        }

        protected virtual FrameworkElements GetChildrenForItemLabelsAlignment()
        {
            if (this.IsActuallyCollapsed)
            {
                return new FrameworkElements();
            }
            if (!this.HasTabs)
            {
                return base.GetLogicalChildren(true);
            }
            FrameworkElements elements = new FrameworkElements();
            if (this.SelectedTabChild != null)
            {
                elements.Add(this.SelectedTabChild);
            }
            return elements;
        }

        protected override Thickness GetClientPadding()
        {
            Thickness clientPadding = base.GetClientPadding();
            if (this.Border != null)
            {
                ThicknessHelper.Inc(ref clientPadding, this.BorderContentPadding);
            }
            return clientPadding;
        }

        protected Rect GetClipBounds(FrameworkElement relativeTo) => 
            (this.IsRoot || !(base.Parent is ILayoutGroup)) ? this.GetVisualBounds(relativeTo) : ((ILayoutGroup) base.Parent).GetClipBounds(this, relativeTo);

        protected virtual HorizontalAlignment? GetGroupBoxHorizontalAlignment()
        {
            if (this.IsActuallyCollapsed && (this.CollapseDirection == System.Windows.Controls.Orientation.Horizontal))
            {
                return 0;
            }
            return null;
        }

        protected virtual GroupBoxState GetGroupBoxState() => 
            this.IsActuallyCollapsed ? GroupBoxState.Minimized : GroupBoxState.Normal;

        protected virtual VerticalAlignment? GetGroupBoxVerticalAlignment()
        {
            if (this.IsActuallyCollapsed && (this.CollapseDirection == System.Windows.Controls.Orientation.Vertical))
            {
                return 0;
            }
            return null;
        }

        protected virtual Type GetGroupType() => 
            base.GetType();

        [IteratorStateMachine(typeof(<GetInternalElements>d__134))]
        protected override IEnumerable<UIElement> GetInternalElements()
        {
            IEnumerator<UIElement> enumerator = this.BaseGetInternalElements().GetEnumerator();
        Label_PostSwitchInIterator:;
            if (enumerator.MoveNext())
            {
                UIElement current = enumerator.Current;
                yield return current;
                goto Label_PostSwitchInIterator;
            }
            else
            {
                enumerator = null;
                if (this.Border != null)
                {
                    yield return this.Border;
                }
            }
        }

        protected static bool GetIsItemStyleSetByGroup(LayoutItem item) => 
            (bool) item.GetValue(IsItemStyleSetByGroupProperty);

        protected virtual bool GetIsPermanent() => 
            (this.View != LayoutGroupView.Group) || !string.IsNullOrEmpty(base.Name);

        protected bool GetIsPermanent(bool keepTabs) => 
            this.GetIsPermanent() || (!this.IsRoot && ((base.Parent is ILayoutGroup) && ((ILayoutGroup) base.Parent).IsChildPermanent(this, keepTabs)));

        protected internal ItemAlignment GetItemAlignment(LayoutItem item) => 
            this.LayoutProvider.GetItemAlignment(item);

        protected internal ItemAlignment GetItemAlignment(LayoutItem item, bool horizontal) => 
            this.LayoutProvider.GetItemAlignment(item, horizontal);

        protected ILayoutGroup GetItemLabelsAlignmentScope(FrameworkElement item)
        {
            while ((item != null) && (!(item is ILayoutGroup) || !((ILayoutGroup) item).IsItemLabelsAlignmentScope))
            {
                item = item.Parent as FrameworkElement;
            }
            return (ILayoutGroup) item;
        }

        protected virtual Style GetItemStyle()
        {
            Style itemStyle = this.ItemStyle;
            if ((itemStyle == null) && (!this.IsRoot && (base.Parent is ILayoutGroup)))
            {
                itemStyle = ((ILayoutGroup) base.Parent).GetItemStyle();
            }
            return itemStyle;
        }

        protected Style GetPartActualStyle(LayoutGroupPartStyle style)
        {
            Style partStyle = this.GetPartStyle(style);
            Style style2 = partStyle;
            if (partStyle == null)
            {
                Style local1 = partStyle;
                if (this.Root == null)
                {
                    return null;
                }
                style2 = this.Root.GetPartStyle(style);
            }
            return style2;
        }

        protected Style GetPartStyle(LayoutGroupPartStyle style)
        {
            switch (style)
            {
                case LayoutGroupPartStyle.GroupBox:
                    return this.GroupBoxStyle;

                case LayoutGroupPartStyle.Tabs:
                    return this.TabsStyle;

                case LayoutGroupPartStyle.Tab:
                    return this.TabStyle;
            }
            return null;
        }

        protected virtual object GetTabHeader(FrameworkElement child, bool useDefaultValues)
        {
            if (child == null)
            {
                return (useDefaultValues ? LocalizationRes.LayoutGroup_TabHeader_Empty : null);
            }
            if (DevExpress.Xpf.LayoutControl.LayoutControl.GetTabHeader(child) != null)
            {
                return DevExpress.Xpf.LayoutControl.LayoutControl.GetTabHeader(child);
            }
            LayoutGroup group = child as LayoutGroup;
            return (((group == null) || (group.IsRoot || (group.Header == null))) ? (useDefaultValues ? LocalizationRes.LayoutGroup_TabHeader_Default : null) : group.Header);
        }

        protected virtual DataTemplate GetTabHeaderTemplate(FrameworkElement child)
        {
            if (child == null)
            {
                return null;
            }
            if (DevExpress.Xpf.LayoutControl.LayoutControl.GetTabHeaderTemplate(child) != null)
            {
                return DevExpress.Xpf.LayoutControl.LayoutControl.GetTabHeaderTemplate(child);
            }
            LayoutGroup group = child as LayoutGroup;
            return (((group == null) || (group.IsRoot || (group.HeaderTemplate == null))) ? null : group.HeaderTemplate);
        }

        protected int GetTabIndex(FrameworkElement child)
        {
            for (int i = 0; i < this.TabControl.Items.Count; i++)
            {
                if (((DXTabItem) this.TabControl.Items[i]).Tag == child)
                {
                    return i;
                }
            }
            return -1;
        }

        protected int GetTabIndex(Point absolutePosition)
        {
            UIElement item = this.TabControl.FindElement(absolutePosition, element => (element is DXTabItem) && this.TabControl.Items.Contains(element));
            return ((item != null) ? this.TabControl.Items.IndexOf(item) : -1);
        }

        protected override string GetXMLID(FrameworkElement element) => 
            this.Root.GetID(element);

        protected virtual void InitChildrenMaxSizeAsDesiredSize()
        {
            foreach (FrameworkElement element in base.GetLogicalChildren(true))
            {
                if (DevExpress.Xpf.LayoutControl.LayoutControl.GetUseDesiredWidthAsMaxWidth(element) && ((element.DesiredSize.Width != 0.0) && double.IsInfinity(element.MaxWidth)))
                {
                    element.MaxWidth = element.DesiredSize.Width;
                }
                if (DevExpress.Xpf.LayoutControl.LayoutControl.GetUseDesiredHeightAsMaxHeight(element) && ((element.DesiredSize.Height != 0.0) && double.IsInfinity(element.MaxHeight)))
                {
                    element.MaxHeight = element.DesiredSize.Height;
                }
            }
        }

        protected virtual void InitGroupBox()
        {
            this.GroupBox.Content = new BorderContentFiller(this);
            ((BorderContentFiller) this.GroupBox.Content).InvalidateMeasure();
            Binding binding = new Binding("Header");
            binding.Source = this;
            this.GroupBox.SetBinding(HeaderedContentControlBase.HeaderProperty, binding);
            this.InitGroupBoxHeaderTemplate();
            Binding binding2 = new Binding("IsCollapsible");
            binding2.Source = this;
            binding2.Converter = new BoolToVisibilityConverter();
            this.GroupBox.SetBinding(DevExpress.Xpf.LayoutControl.GroupBox.MinimizeElementVisibilityProperty, binding2);
            this.InitGroupBoxAlignment();
            this.InitGroupBoxDisplayMode();
            this.InitGroupBoxState();
            this.InitGroupBoxStyle();
            this.GroupBox.SetZIndex(-1000);
            this.GroupBox.StateChanged += (o, e) => this.OnGroupBoxStateChanged();
            Binding binding3 = new Binding("MinimizationDirection");
            binding3.Source = this.GroupBox;
            base.SetBinding(CollapseDirectionProperty, binding3);
        }

        protected void InitGroupBoxAlignment()
        {
            if (this.GroupBox != null)
            {
                HorizontalAlignment? groupBoxHorizontalAlignment = this.GetGroupBoxHorizontalAlignment();
                this.GroupBox.SetValue(FrameworkElement.HorizontalAlignmentProperty, (groupBoxHorizontalAlignment != null) ? groupBoxHorizontalAlignment.GetValueOrDefault() : DependencyProperty.UnsetValue);
                VerticalAlignment? groupBoxVerticalAlignment = this.GetGroupBoxVerticalAlignment();
                this.GroupBox.SetValue(FrameworkElement.VerticalAlignmentProperty, (groupBoxVerticalAlignment != null) ? groupBoxVerticalAlignment.GetValueOrDefault() : DependencyProperty.UnsetValue);
            }
        }

        protected void InitGroupBoxDisplayMode()
        {
            this.UpdateActualGroupBoxDisplayMode();
            if (this.GroupBox != null)
            {
                this.GroupBox.SetCurrentValue(DevExpress.Xpf.LayoutControl.GroupBox.DisplayModeProperty, this.ActualGroupBoxDisplayMode);
            }
        }

        protected void InitGroupBoxHeaderTemplate()
        {
            if (this.GroupBox != null)
            {
                this.GroupBox.SetValueIfNotDefault(HeaderedContentControlBase.HeaderTemplateProperty, this.HeaderTemplate);
            }
        }

        protected void InitGroupBoxState()
        {
            if (this.GroupBox != null)
            {
                this.GroupBox.SetCurrentValue(DevExpress.Xpf.LayoutControl.GroupBox.StateProperty, this.GetGroupBoxState());
            }
        }

        protected void InitGroupBoxStyle()
        {
            if (this.GroupBox != null)
            {
                this.GroupBox.SetValueIfNotDefault(FrameworkElement.StyleProperty, this.GetPartActualStyle(LayoutGroupPartStyle.GroupBox));
            }
        }

        protected virtual void InitGroupForChild(LayoutGroup group, FrameworkElement child)
        {
            if (this.HasTabs)
            {
                this.CopyTabHeaderInfo(child, group);
            }
            else
            {
                group.Orientation = this.Orientation;
            }
            this.LayoutProvider.InitGroupForChild(group, child);
        }

        protected void InitializeUnfocusableState(FrameworkElement child)
        {
            if (!this.IsInDesignTool())
            {
                this.StorePropertyValue(child, KeyboardNavigation.ControlTabNavigationProperty, ControlTabNavigationInfoProperty);
                this.StorePropertyValue(child, KeyboardNavigation.TabNavigationProperty, TabNavigationInfoProperty);
                if (child.IsKeyboardFocusWithin && (Keyboard.FocusedElement is DependencyObject))
                {
                    FocusManager.SetFocusedElement(FocusManager.GetFocusScope((DependencyObject) Keyboard.FocusedElement), null);
                }
                KeyboardNavigation.SetTabNavigation(child, KeyboardNavigationMode.None);
                KeyboardNavigation.SetControlTabNavigation(child, KeyboardNavigationMode.None);
            }
        }

        protected void InitPartStyle(LayoutGroupPartStyle style)
        {
            switch (style)
            {
                case LayoutGroupPartStyle.GroupBox:
                    this.InitGroupBoxStyle();
                    return;

                case LayoutGroupPartStyle.Tabs:
                    this.InitTabControlStyle();
                    return;

                case LayoutGroupPartStyle.Tab:
                    this.UpdateTabs(true);
                    return;
            }
        }

        protected virtual void InitTab(DXTabItem tab, FrameworkElement child, bool initStyle)
        {
            tab.Header = this.GetTabHeader(child, true);
            tab.SetValueIfNotDefault(HeaderedSelectorItemBase<DXTabControl, DXTabItem>.HeaderTemplateProperty, this.GetTabHeaderTemplate(child));
            BorderContentFiller content = tab.Content as BorderContentFiller;
            content = new BorderContentFiller(this);
            content.InvalidateMeasure();
            tab.Content = content;
            if (initStyle)
            {
                tab.SetValueIfNotDefault(FrameworkElement.StyleProperty, this.GetPartActualStyle(LayoutGroupPartStyle.Tab));
            }
            tab.Tag = child;
            if (child != null)
            {
                tab.Visibility = child.Visibility;
                tab.SetBinding(child, "IsEnabled", UIElement.IsEnabledProperty);
            }
        }

        protected virtual void InitTabControl()
        {
            this.InitTabs(true);
            this.InitTabControlStyle();
            this.TabControl.SetZIndex(-1000);
            this.TabControl.SelectionChanged += new TabControlSelectionChangedEventHandler(this.OnTabControlSelectionChanged);
            this.UpdateTabControlSelectedIndex();
            this.CheckSelectedTabChildChanged();
        }

        protected void InitTabControlStyle()
        {
            if (this.HasTabs)
            {
                this.TabControl.SetValueIfNotDefault(FrameworkElement.StyleProperty, this.GetPartActualStyle(LayoutGroupPartStyle.Tabs));
                this.TabControl.ApplyStyleValuesToPropertiesWithLocalValues();
            }
        }

        protected virtual void InitTabs(bool initStyle)
        {
            FrameworkElements logicalChildren = base.GetLogicalChildren(true);
            if ((logicalChildren.Count == 0) && ((this.TabControl.Items.Count == 1) && (this.GetTabIndex((FrameworkElement) null) == 0)))
            {
                this.InitTab((DXTabItem) this.TabControl.Items[0], null, initStyle);
            }
            else
            {
                bool flag = false;
                for (int i = 0; i < logicalChildren.Count; i++)
                {
                    DXTabItem item;
                    FrameworkElement child = logicalChildren[i];
                    int tabIndex = this.GetTabIndex(child);
                    if (tabIndex == -1)
                    {
                        item = this.CreateTab();
                        this.TabControl.Items.Insert(i, item);
                        flag = true;
                    }
                    else
                    {
                        item = (DXTabItem) this.TabControl.Items[tabIndex];
                        if (tabIndex != i)
                        {
                            this.TabControl.Items.RemoveAt(tabIndex);
                            this.TabControl.Items.Insert(i, item);
                        }
                    }
                    this.InitTab(item, child, (tabIndex == -1) | initStyle);
                }
                if (flag)
                {
                    this.TabControl.SelectedIndex = -1;
                }
                for (int j = this.TabControl.Items.Count - 1; j >= logicalChildren.Count; j--)
                {
                    this.FinalizeTab((DXTabItem) this.TabControl.Items[j]);
                    this.TabControl.Items.RemoveAt(j);
                }
                if (logicalChildren.Count == 0)
                {
                    DXTabItem newItem = this.CreateTab();
                    this.TabControl.Items.Add(newItem);
                    this.InitTab(newItem, null, true);
                }
            }
        }

        protected virtual void InvalidateChildrenMeasure()
        {
            base.InvalidateMeasure();
            foreach (FrameworkElement element in base.GetLogicalChildren(true))
            {
                LayoutGroup group = element as LayoutGroup;
                if (group != null)
                {
                    group.InvalidateChildrenMeasure();
                }
            }
        }

        protected static void InvalidateParents(FrameworkElement child)
        {
            FrameworkElement parent = child;
            while (true)
            {
                parent = (FrameworkElement) parent.Parent;
                if (parent != null)
                {
                    parent.InvalidateMeasure();
                }
                if (!parent.IsLayoutGroup())
                {
                    return;
                }
            }
        }

        protected virtual bool IsChildBorderless(ILayoutGroup child) => 
            !this.HasTabs;

        protected virtual bool IsChildPermanent(ILayoutGroup child, bool keepTabs) => 
            this.HasTabs & keepTabs;

        protected virtual bool IsChildUnfocusable(FrameworkElement child) => 
            base.IsLogicalChild(child) && this.IsLogicalChildUnfocusable(child);

        protected void IsItemLabelsAlignmentScopeChanged()
        {
            ILayoutGroup itemLabelsAlignmentScope = this.GetItemLabelsAlignmentScope((FrameworkElement) base.Parent);
            if (itemLabelsAlignmentScope != null)
            {
                itemLabelsAlignmentScope.LayoutItemLabelsAlignmentChanged();
            }
            if (this.IsItemLabelsAlignmentScope)
            {
                this.UpdateItemLabelsAlignment();
            }
        }

        protected virtual bool IsLogicalChildUnfocusable(FrameworkElement logicalChild) => 
            this.IsActuallyCollapsed || (this.HasTabs && !ReferenceEquals(logicalChild, this.SelectedTabChild));

        protected virtual bool IsRemovableForOptimization(bool considerContent, bool keepEmptyTabs)
        {
            bool flag = considerContent && (base.GetLogicalChildren(false).Count == 0);
            return (!this.GetIsPermanent(!flag | keepEmptyTabs) && (!considerContent | flag));
        }

        protected override bool IsTempChild(UIElement child) => 
            base.IsTempChild(child) || this.ItemSizers.IsItem(child);

        protected void ItemLabelsAlignmentChanged()
        {
            ILayoutGroup itemLabelsAlignmentScope = this.GetItemLabelsAlignmentScope(this);
            if (itemLabelsAlignmentScope != null)
            {
                itemLabelsAlignmentScope.LayoutItemLabelsAlignmentChanged();
            }
        }

        protected virtual bool MakeChildVisible(FrameworkElement child)
        {
            if (!this.HasGroupBox)
            {
                return (!this.HasTabs || this.SelectTab(child));
            }
            if (this.IsActuallyCollapsed)
            {
                this.IsCollapsed = false;
            }
            return !this.IsActuallyCollapsed;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            Size size = base.MeasureOverride(availableSize);
            if ((((size.Width > availableSize.Width) || (size.Height > availableSize.Height)) && !this.IsRoot) && ((base.Parent is LayoutGroup) && !((LayoutGroup) base.Parent).IsMeasuring))
            {
                ((LayoutGroup) base.Parent).InvalidateMeasure();
            }
            return size;
        }

        protected void MoveChildrenToAvailableItems(FrameworkElement lastRemainingChild = null)
        {
            FrameworkElements logicalChildren = base.GetLogicalChildren(false);
            for (int i = (lastRemainingChild != null) ? (logicalChildren.IndexOf(lastRemainingChild) + 1) : 0; i < logicalChildren.Count; i++)
            {
                FrameworkElement item = logicalChildren[i];
                if ((item.GetType() == this.GetGroupType()) && !((LayoutGroup) item).IsPermanent)
                {
                    ((LayoutGroup) item).MoveChildrenToAvailableItems(null);
                }
                else
                {
                    this.Root.AvailableItems.Add(item);
                }
            }
        }

        public ILayoutGroup MoveChildrenToNewGroup()
        {
            LayoutGroup group = this.CreateGroup();
            ((ILayoutGroupModel) group).Orientation = this.Orientation;
            foreach (FrameworkElement element in base.GetLogicalChildren(false))
            {
                base.Children.Remove(element);
                group.Children.Add(element);
            }
            base.Children.Add(group);
            return group;
        }

        public bool MoveChildrenToParent()
        {
            if (this.IsLocked)
            {
                return false;
            }
            Panel parent = base.Parent as Panel;
            if (parent == null)
            {
                return false;
            }
            int index = parent.Children.IndexOf(this);
            FrameworkElements logicalChildren = base.GetLogicalChildren(false);
            for (int i = logicalChildren.Count - 1; i >= 0; i--)
            {
                FrameworkElement element = logicalChildren[i];
                base.Children.Remove(element);
                if (parent is ILayoutGroup)
                {
                    ((ILayoutGroup) parent).InitChildFromGroup(element, this);
                }
                parent.Children.Insert(index, element);
            }
            return true;
        }

        public ILayoutGroup MoveChildToNewGroup(FrameworkElement child)
        {
            int index = base.Children.IndexOf(child);
            base.Children.RemoveAt(index);
            LayoutGroup group = this.CreateGroup();
            this.InitGroupForChild(group, child);
            group.Children.Add(child);
            base.Children.Insert(index, group);
            return group;
        }

        protected void MoveNonUserDefinedChildrenToAvailableItems()
        {
            foreach (FrameworkElement element in base.GetLogicalChildren(false))
            {
                if (!DevExpress.Xpf.LayoutControl.LayoutControl.GetIsUserDefined(element))
                {
                    this.Root.AvailableItems.Add(element);
                    continue;
                }
                if (element.IsLayoutGroup())
                {
                    ((ILayoutGroup) element).MoveNonUserDefinedChildrenToAvailableItems();
                }
            }
        }

        protected virtual void OnAllowHorizontalSizingChanged(FrameworkElement child)
        {
            base.InvalidateArrange();
        }

        protected virtual void OnAllowItemSizingChanged()
        {
            base.InvalidateArrange();
            Action<ILayoutGroup> action = <>c.<>9__312_0;
            if (<>c.<>9__312_0 == null)
            {
                Action<ILayoutGroup> local1 = <>c.<>9__312_0;
                action = <>c.<>9__312_0 = group => group.AllowItemSizingChanged();
            }
            this.ForEachGroup(action);
        }

        protected virtual void OnAllowVerticalSizingChanged(FrameworkElement child)
        {
            base.InvalidateArrange();
        }

        protected override Size OnArrange(Rect bounds)
        {
            this.ItemSizers.MarkItemsAsUnused();
            Size size = base.OnArrange(bounds);
            this.ItemSizers.DeleteUnusedItems();
            if (!this.IsItemLabelsAlignmentScope)
            {
                this.ItemLabelsAlignmentChanged();
            }
            else
            {
                this.LayoutProvider.AlignLayoutItemLabels(this, this.GetChildrenForItemLabelsAlignment());
            }
            return size;
        }

        protected override void OnAttachedPropertyChanged(FrameworkElement child, DependencyProperty property, object oldValue, object newValue)
        {
            base.OnAttachedPropertyChanged(child, property, oldValue, newValue);
            if (ReferenceEquals(property, DevExpress.Xpf.LayoutControl.LayoutControl.AllowHorizontalSizingProperty))
            {
                this.OnAllowHorizontalSizingChanged(child);
            }
            if (ReferenceEquals(property, DevExpress.Xpf.LayoutControl.LayoutControl.AllowVerticalSizingProperty))
            {
                this.OnAllowVerticalSizingChanged(child);
            }
            if (ReferenceEquals(property, DevExpress.Xpf.LayoutControl.LayoutControl.TabHeaderProperty))
            {
                this.OnTabHeaderChanged(child);
            }
            if (ReferenceEquals(property, DevExpress.Xpf.LayoutControl.LayoutControl.TabHeaderTemplateProperty))
            {
                this.OnTabHeaderTemplateChanged(child);
            }
            if (ReferenceEquals(property, DevExpress.Xpf.LayoutControl.LayoutControl.UseDesiredWidthAsMaxWidthProperty))
            {
                this.OnUseDesiredWidthAsMaxWidthChanged(child);
            }
            if (ReferenceEquals(property, DevExpress.Xpf.LayoutControl.LayoutControl.UseDesiredHeightAsMaxHeightProperty))
            {
                this.OnUseDesiredHeightAsMaxHeightChanged(child);
            }
        }

        protected override void OnBeforeArrange(Size finalSize)
        {
            if ((this.Border != null) && (this.BorderContent != null))
            {
                Thickness borderContentPadding = this.BorderContentPadding;
                this.BorderContentPadding = new Thickness(0.0);
                Rect finalRect = base.CalculateClientBounds(finalSize);
                this.Border.Arrange(finalRect);
                if (this.IsBorderContentVisible && (this.BorderContent.GetVisualParent() != null))
                {
                    Rect bounds = this.BorderContent.GetBounds(this);
                    Rect rect3 = new Rect();
                    this.BorderContentPadding = !(bounds == rect3) ? RectHelper.Padding(finalRect, bounds) : borderContentPadding;
                }
            }
            base.OnBeforeArrange(finalSize);
        }

        protected override void OnBeforeMeasure(Size availableSize)
        {
            this.CheckTabControlIndex();
            this.UpdateTabs(false);
            this.UpdateTabControlSelectedIndex();
            this.CheckSelectedTabChildChanged();
            if ((this.Border != null) && (this.BorderContent != null))
            {
                Thickness borderContentPadding = this.BorderContentPadding;
                this.BorderContentPadding = new Thickness(0.0);
                Size size = base.CalculateClientBounds(availableSize).Size();
                Size size2 = size;
                this.BorderContent.IsEmpty = this.LayoutProvider.IsContentEmpty(base.GetLogicalChildren(true));
                this.Border.InvalidateParentsOfModifiedChildren();
                this.Border.Measure(size);
                if (this.IsBorderContentVisible)
                {
                    bool flag = ((this.BorderContent.AvailableSize.Width == 0.0) && double.IsNaN(base.Width)) && (this.ActualHorizontalAlignment != HorizontalAlignment.Stretch);
                    bool flag2 = ((this.BorderContent.AvailableSize.Height == 0.0) && double.IsNaN(base.Height)) && (this.ActualVerticalAlignment != VerticalAlignment.Stretch);
                    if (flag | flag2)
                    {
                        if (flag)
                        {
                            size.Width = double.PositiveInfinity;
                        }
                        if (flag2)
                        {
                            size.Height = double.PositiveInfinity;
                        }
                        this.Border.Measure(size);
                    }
                }
                this.BorderMinSize = this.Border.DesiredSize;
                if (double.IsInfinity(size.Width) || double.IsInfinity(size.Height))
                {
                    if (double.IsInfinity(size.Width))
                    {
                        size.Width = this.Border.DesiredSize.Width;
                    }
                    if (double.IsInfinity(size.Height))
                    {
                        size.Height = this.Border.DesiredSize.Height;
                    }
                    if (this.IsBorderContentVisible)
                    {
                        this.BorderContent.InvalidateMeasure();
                    }
                    this.Border.Measure(size);
                }
                if (this.IsBorderContentVisible)
                {
                    this.BorderContentPadding = this.BorderContent.IsMeasureValid ? new Thickness(size.Width - this.BorderContent.AvailableSize.Width, size.Height - this.BorderContent.AvailableSize.Height, 0.0, 0.0) : borderContentPadding;
                }
                this.Border.Measure(size2);
            }
            base.OnBeforeMeasure(availableSize);
        }

        protected override void OnChildAdded(FrameworkElement child)
        {
            base.OnChildAdded(child);
            if (child.IsLayoutGroup())
            {
                this.OnChildGroupAdded((ILayoutGroup) child);
            }
            if (this.Root != null)
            {
                this.Root.ControlAdded(child);
            }
            this.ApplyItemStyle(child);
            this.CheckUnfocusableState(child);
        }

        protected virtual void OnChildGroupAdded(ILayoutGroup childGroup)
        {
            childGroup.IsCustomization = this.IsCustomization;
            childGroup.ItemSizerStyle = this.ItemSizerStyle;
            foreach (LayoutGroupPartStyle style in typeof(LayoutGroupPartStyle).GetValues())
            {
                childGroup.UpdatePartStyle(style);
            }
        }

        protected virtual void OnChildGroupHeaderChanged(LayoutGroup childGroup)
        {
            this.UpdateTab(childGroup, false);
        }

        protected virtual void OnChildGroupHeaderTemplateChanged(LayoutGroup childGroup)
        {
            this.UpdateTab(childGroup, false);
        }

        protected virtual void OnChildHorizontalAlignmentChanged(FrameworkElement child)
        {
            InvalidateParents(child);
        }

        protected override void OnChildPropertyChanged(FrameworkElement child, DependencyProperty propertyListener, object oldValue, object newValue)
        {
            base.OnChildPropertyChanged(child, propertyListener, oldValue, newValue);
            if (ReferenceEquals(propertyListener, ChildHorizontalAlignmentListener))
            {
                this.OnChildHorizontalAlignmentChanged(child);
            }
            if (ReferenceEquals(propertyListener, ChildVerticalAlignmentListener))
            {
                this.OnChildVerticalAlignmentChanged(child);
            }
            if (ReferenceEquals(propertyListener, ChildWidthListener) || (ReferenceEquals(propertyListener, ChildHeightListener) || (ReferenceEquals(propertyListener, ChildMaxWidthListener) || ReferenceEquals(propertyListener, ChildMaxHeightListener))))
            {
                this.OnChildSizeChanged(child);
            }
            if (ReferenceEquals(propertyListener, ChildStyleListener))
            {
                this.OnChildStyleChanged(child);
            }
            if (ReferenceEquals(propertyListener, ChildVisibilityListener) && ((newValue != null) && !newValue.Equals(oldValue)))
            {
                this.OnChildVisibilityChanged(child);
            }
        }

        protected override void OnChildRemoved(FrameworkElement child)
        {
            base.OnChildRemoved(child);
            if (this.Root != null)
            {
                this.Root.ControlRemoved(child);
            }
            if (child.Parent == null)
            {
                this.ClearItemStyle(child);
            }
            if (!(child.Parent is LayoutGroup) && (child is Control))
            {
                this.FinalizeUnfocusableState((Control) child);
            }
        }

        protected virtual void OnChildSizeChanged(FrameworkElement child)
        {
            InvalidateParents(child);
        }

        protected virtual void OnChildStyleChanged(FrameworkElement child)
        {
            LayoutItem item = child as LayoutItem;
            if (item != null)
            {
                if (item.Style == null)
                {
                    this.ApplyItemStyle(item);
                }
                else if (GetIsItemStyleSetByGroup(item) && !ReferenceEquals(item.Style, this.GetItemStyle()))
                {
                    SetIsItemStyleSetByGroup(item, false);
                }
            }
        }

        protected virtual void OnChildVerticalAlignmentChanged(FrameworkElement child)
        {
            InvalidateParents(child);
        }

        protected virtual void OnChildVisibilityChanged(FrameworkElement child)
        {
            this.UpdateDesiredAlignment();
            if (this.Root != null)
            {
                this.Root.ControlVisibilityChanged(child);
            }
        }

        protected virtual void OnCollapsed()
        {
            if (this.Collapsed != null)
            {
                this.Collapsed(this, EventArgs.Empty);
            }
        }

        protected virtual void OnCollapseDirectionChanged()
        {
            this.InitGroupBoxAlignment();
            base.Changed();
            InvalidateParents(this);
        }

        protected override AutomationPeer OnCreateAutomationPeer() => 
            new LayoutGroupAutomationPeer(this);

        protected virtual bool OnDesignTimeClick(DXMouseButtonEventArgs args) => 
            (!this.HasGroupBox || !((IGroupBox) this.GroupBox).DesignTimeClick(args)) ? (this.HasTabs && this.OnTabControlDesignTimeClick(args)) : true;

        protected virtual void OnExpanded()
        {
            if (this.Expanded != null)
            {
                this.Expanded(this, EventArgs.Empty);
            }
        }

        [IteratorStateMachine(typeof(<OnGetLiveCustomizationAreas>d__319))]
        protected virtual IEnumerable<Rect> OnGetLiveCustomizationAreas(FrameworkElement relativeTo)
        {
            IEnumerator<ElementSizer> enumerator = this.ItemSizers.GetEnumerator();
        Label_PostSwitchInIterator:;
            if (enumerator.MoveNext())
            {
                yield return enumerator.Current.GetBounds(relativeTo);
                goto Label_PostSwitchInIterator;
            }
            else
            {
                enumerator = null;
                if (this.HasGroupBox && (this.GroupBox.MinimizeElementVisibility == Visibility.Visible))
                {
                    Rect rect = ((IGroupBox) this.GroupBox).MinimizeElementBounds;
                    if (!rect.IsEmpty)
                    {
                        rect = this.GroupBox.MapRect(rect, relativeTo);
                        yield return rect;
                    }
                }
            }
            if (this.HasTabs)
            {
                if ((this.TabControl.TabPanel != null) && this.TabControl.TabPanel.IsInVisualTree())
                {
                    yield return this.TabControl.TabPanel.GetBounds(relativeTo);
                }
                if ((this.TabControl.PrevButton != null) && this.TabControl.PrevButton.IsInVisualTree())
                {
                    yield return this.TabControl.PrevButton.GetBounds(relativeTo);
                }
            }
            while (true)
            {
                if ((this.TabControl.NextButton != null) && this.TabControl.NextButton.IsInVisualTree())
                {
                    yield return this.TabControl.NextButton.GetBounds(relativeTo);
                }
            }
        }

        protected virtual void OnGroupBoxStateChanged()
        {
            if (this.IsCollapsible)
            {
                bool flag = this.GroupBox.State == GroupBoxState.Minimized;
                if (this.IsCollapsed != flag)
                {
                    this.IsCollapsed = flag;
                    if (this.Root != null)
                    {
                        this.Root.ModelChanged(new LayoutControlModelPropertyChangedEventArgs(this, "IsCollapsed", IsCollapsedProperty));
                    }
                }
            }
        }

        protected virtual void OnHasTabsChanged()
        {
            this.CheckTabControl();
            this.IsItemLabelsAlignmentScopeChanged();
            this.CheckUnfocusableStateForChildren();
        }

        protected virtual void OnHeaderChanged()
        {
            if (!this.IsRoot && (base.Parent is LayoutGroup))
            {
                ((LayoutGroup) base.Parent).OnChildGroupHeaderChanged(this);
            }
        }

        protected virtual void OnHeaderTemplateChanged()
        {
            this.InitGroupBoxHeaderTemplate();
            if (!this.IsRoot && (base.Parent is LayoutGroup))
            {
                ((LayoutGroup) base.Parent).OnChildGroupHeaderTemplateChanged(this);
            }
        }

        protected virtual void OnIsActuallyCollapsedChanged()
        {
            this.InitGroupBoxAlignment();
            this.InitGroupBoxState();
            this.UpdateCollapsedSize();
            this.IsItemLabelsAlignmentScopeChanged();
            base.Changed();
            if (base.Parent is UIElement)
            {
                ((UIElement) base.Parent).InvalidateMeasure();
            }
            this.CheckUnfocusableStateForChildren();
            if (this.IsActuallyCollapsed)
            {
                this.OnCollapsed();
            }
            else
            {
                this.OnExpanded();
                InvalidateParents(this);
            }
        }

        protected virtual void OnIsCollapsedChanged()
        {
            this.CheckIsActuallyCollapsedChanged();
        }

        protected virtual void OnIsCollapsibleChanged()
        {
            this.CheckIsActuallyCollapsedChanged();
        }

        protected virtual void OnIsCustomizationChanged()
        {
            base.InvalidateArrange();
        }

        protected virtual void OnItemLabelsAlignmentChanged()
        {
            this.IsItemLabelsAlignmentScopeChanged();
        }

        protected override void OnItemSpaceChanged(double oldValue, double newValue)
        {
            base.OnItemSpaceChanged(oldValue, newValue);
            this.ItemSizers.SizingAreaWidth = base.ItemSpace;
        }

        protected virtual void OnItemStyleChanged()
        {
            this.ApplyItemStyle();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            this.UpdateActualGroupBoxDisplayMode();
        }

        protected override Size OnMeasure(Size availableSize)
        {
            Size size = base.OnMeasure(availableSize);
            this.InitChildrenMaxSizeAsDesiredSize();
            return size;
        }

        protected virtual void OnMeasureSelectedTabChildOnlyChanged()
        {
            base.InvalidateMeasure();
        }

        protected virtual bool OnOptimizeLayout(bool keepEmptyTabs)
        {
            bool flag = false;
            foreach (FrameworkElement element in base.GetLogicalChildren(false))
            {
                if (element.IsLayoutGroup() && ((ILayoutGroup) element).IsRemovableForOptimization(true, keepEmptyTabs))
                {
                    base.Children.Remove(element);
                    flag = true;
                }
            }
            FrameworkElements logicalChildren = base.GetLogicalChildren(false);
            if (!this.IsRoot && this.IsRemovableForOptimization(false, keepEmptyTabs))
            {
                if ((logicalChildren.Count == 1) || ((logicalChildren.Count > 1) && ((base.Parent is ILayoutGroup) && (((ILayoutGroup) base.Parent).Orientation == this.Orientation))))
                {
                    this.MoveChildrenToParent();
                    flag = true;
                }
            }
            else if ((logicalChildren.Count == 1) && logicalChildren[0].IsLayoutGroup())
            {
                ILayoutGroup group = (ILayoutGroup) logicalChildren[0];
                if (group.IsRemovableForOptimization(false, keepEmptyTabs))
                {
                    if (this.Orientation != group.Orientation)
                    {
                        ((ILayoutGroupModel) this).Orientation = group.Orientation;
                        flag = true;
                    }
                    if (group.MoveChildrenToParent())
                    {
                        if ((this.Header == null) && (this.HeaderTemplate == null))
                        {
                            if (group.Header != null)
                            {
                                this.Header = group.Header;
                            }
                            if (group.HeaderTemplate != null)
                            {
                                this.HeaderTemplate = group.HeaderTemplate;
                            }
                        }
                        base.Children.Remove(group.Control);
                        flag = true;
                    }
                }
            }
            return flag;
        }

        protected virtual void OnOrientationChanged()
        {
            base.Changed();
        }

        protected virtual void OnPartStyleChanged(LayoutGroupPartStyle style)
        {
            this.InitPartStyle(style);
        }

        protected virtual void OnSelectedTabChildChanged(FrameworkElement oldValue)
        {
            this.UnfocusTabChild(oldValue);
            this.CheckUnfocusableStateForChildren();
            if (this.MeasureSelectedTabChildOnly)
            {
                base.InvalidateMeasure();
            }
            else
            {
                base.InvalidateArrange();
            }
            if (this.SelectedTabChildChanged != null)
            {
                this.SelectedTabChildChanged(this, new ValueChangedEventArgs<FrameworkElement>(oldValue, this.SelectedTabChild));
            }
        }

        protected virtual void OnSelectedTabIndexChanged(int oldValue)
        {
            this.UpdateTabControlSelectedIndex();
        }

        protected virtual bool OnTabControlDesignTimeClick(DXMouseButtonEventArgs args)
        {
            int tabIndex = this.GetTabIndex(args.GetPosition(null));
            if (tabIndex == -1)
            {
                return false;
            }
            if (this.TabControl.SelectedIndex != tabIndex)
            {
                this.TabControl.SelectedIndex = tabIndex;
            }
            else
            {
                this.Root.TabClicked(this, this.SelectedTabChild);
            }
            return true;
        }

        protected virtual void OnTabControlSelectionChanged()
        {
            ((ILayoutGroup) this).SelectedTabIndex = this.TabControl.SelectedIndex;
            if ((this.TabControl.SelectedIndex != -1) && (this.Root != null))
            {
                this.Root.TabClicked(this, this.SelectedTabChild);
            }
        }

        private void OnTabControlSelectionChanged(object sender, TabControlSelectionChangedEventArgs e)
        {
            if (!this.DisableTabControlSelectionChangedNotification)
            {
                this.OnTabControlSelectionChanged();
            }
        }

        private void OnTabControlThemeChanged(DependencyObject sender, ThemeChangedRoutedEventArgs e)
        {
            this.BindTabControlForeground();
        }

        protected virtual void OnTabHeaderChanged(FrameworkElement child)
        {
            this.UpdateTab(child, false);
        }

        protected virtual void OnTabHeaderTemplateChanged(FrameworkElement child)
        {
            this.UpdateTab(child, false);
        }

        protected virtual void OnUseDesiredHeightAsMaxHeightChanged(FrameworkElement child)
        {
            if (DevExpress.Xpf.LayoutControl.LayoutControl.GetUseDesiredHeightAsMaxHeight(child) && (child.DesiredSize.Height != 0.0))
            {
                double height = child.Height;
                if (!double.IsNaN(height))
                {
                    child.Height = double.NaN;
                    base.UpdateLayout();
                }
                child.MaxHeight = child.DesiredSize.Height;
                child.Height = height;
            }
            if (!DevExpress.Xpf.LayoutControl.LayoutControl.GetUseDesiredHeightAsMaxHeight(child))
            {
                child.MaxHeight = double.PositiveInfinity;
            }
        }

        protected virtual void OnUseDesiredWidthAsMaxWidthChanged(FrameworkElement child)
        {
            if (DevExpress.Xpf.LayoutControl.LayoutControl.GetUseDesiredWidthAsMaxWidth(child) && (child.DesiredSize.Width != 0.0))
            {
                double width = child.Width;
                if (!double.IsNaN(width))
                {
                    child.Width = double.NaN;
                    base.UpdateLayout();
                }
                child.MaxWidth = child.DesiredSize.Width;
                child.Width = width;
            }
            if (!DevExpress.Xpf.LayoutControl.LayoutControl.GetUseDesiredWidthAsMaxWidth(child))
            {
                child.MaxWidth = double.PositiveInfinity;
            }
        }

        protected virtual void OnViewChanged()
        {
            this.CheckGroupBox();
            this.CheckIsActuallyCollapsedChanged();
            this.CheckHasTabsChanged();
            base.Changed();
        }

        public bool OptimizeLayout() => 
            this.OptimizeLayout(false);

        public bool OptimizeLayout(bool keepEmptyTabs)
        {
            bool flag = false;
            int count = 0;
            while (true)
            {
                FrameworkElements logicalChildren = base.GetLogicalChildren(false);
                if (logicalChildren.Count == count)
                {
                    return (this.OnOptimizeLayout(keepEmptyTabs) | flag);
                }
                foreach (FrameworkElement element in logicalChildren)
                {
                    if (element.IsLayoutGroup())
                    {
                        flag = ((ILayoutGroup) element).OptimizeLayout(keepEmptyTabs) | flag;
                    }
                }
                count = logicalChildren.Count;
            }
        }

        protected override FrameworkElement ReadChildFromXMLCore(FrameworkElement element, XmlReader xml, IList children, int index, string id)
        {
            if (xml.Name != this.GetGroupType().Name)
            {
                element = base.ReadChildFromXMLCore(element, xml, children, index, id);
            }
            else
            {
                LayoutGroup control = element as LayoutGroup;
                if (control == null)
                {
                    control = this.CreateGroup();
                    if (element == null)
                    {
                        ILayoutControl root = this.Root;
                        if (root == null)
                        {
                            ILayoutControl local1 = root;
                        }
                        else
                        {
                            root.SetID(control, id);
                        }
                    }
                    DevExpress.Xpf.LayoutControl.LayoutControl.SetIsUserDefined(control, true);
                    element = control;
                }
                this.AddChildFromXML(children, control, index);
                control.ReadFromXML(xml);
            }
            return element;
        }

        protected override void ReadChildrenFromXML(XmlReader xml, out FrameworkElement lastChild)
        {
            base.ReadChildrenFromXML(xml, out lastChild);
            this.MoveChildrenToAvailableItems(lastChild);
        }

        protected override void ReadCustomizablePropertiesFromXML(XmlReader xml)
        {
            base.ReadCustomizablePropertiesFromXML(xml);
            this.ReadPropertyFromXML(xml, HeaderProperty, "Header", typeof(object));
            this.ReadPropertyFromXML(xml, IsCollapsedProperty, "IsCollapsed", typeof(bool));
            this.ReadPropertyFromXML(xml, IsLockedProperty, "IsLocked", typeof(bool));
            this.ReadPropertyFromXML(xml, LayoutControlBase.ItemSpaceProperty, "ItemSpace", typeof(double));
            this.ReadPropertyFromXML(xml, OrientationProperty, "Orientation", typeof(System.Windows.Controls.Orientation));
            this.ReadPropertyFromXML(xml, LayoutControlBase.PaddingProperty, "Padding", typeof(Thickness));
            this.ReadPropertyFromXML(xml, SelectedTabIndexProperty, "SelectedTabIndex", typeof(int));
            this.ReadPropertyFromXML(xml, ViewProperty, "View", typeof(LayoutGroupView));
            this.ReadPropertyFromXML(xml, KeepSelectionOnTabRemovalProperty, "KeepSelectedTab", typeof(bool));
            this.ReadPropertyFromXML(xml, StoredSizeProperty, "StoredSize", typeof(double));
            if (!this.IsRoot)
            {
                this.ReadCustomizablePropertiesFromXML(this, xml);
            }
            else
            {
                this.Root.ReadElementFromXML(xml, this);
            }
        }

        protected override void ReadCustomizablePropertiesFromXML(FrameworkElement element, XmlReader xml)
        {
            base.ReadCustomizablePropertiesFromXML(element, xml);
            element.ReadPropertyFromXML(xml, FrameworkElement.HorizontalAlignmentProperty, "HorizontalAlignment", typeof(HorizontalAlignment));
            element.ReadPropertyFromXML(xml, FrameworkElement.VerticalAlignmentProperty, "VerticalAlignment", typeof(VerticalAlignment));
            element.ReadPropertyFromXML(xml, FrameworkElement.WidthProperty, "Width", typeof(double));
            element.ReadPropertyFromXML(xml, FrameworkElement.HeightProperty, "Height", typeof(double));
            if (element is LayoutItem)
            {
                ((LayoutItem) element).ReadCustomizablePropertiesFromXML(xml);
            }
            ILayoutControl root = this.Root;
            if (root == null)
            {
                ILayoutControl local1 = root;
            }
            else
            {
                root.ReadElementFromXML(xml, element);
            }
        }

        private void RestorePropertyValue(DependencyObject dObj, DependencyProperty from, DependencyProperty to)
        {
            object storedInfo = dObj.GetValue(from);
            if (storedInfo != null)
            {
                dObj.RestorePropertyValue(to, storedInfo);
                dObj.ClearValue(from);
            }
        }

        public bool SelectTab(FrameworkElement child)
        {
            if (!this.HasTabs)
            {
                return false;
            }
            int index = base.GetLogicalChildren(true).IndexOf(child);
            bool flag = index != -1;
            if (flag)
            {
                ((ILayoutGroup) this).SelectedTabIndex = index;
            }
            return flag;
        }

        protected static void SetIsItemStyleSetByGroup(LayoutItem item, bool value)
        {
            if (value)
            {
                item.SetValue(IsItemStyleSetByGroupProperty, value);
            }
            else
            {
                item.ClearValue(IsItemStyleSetByGroupProperty);
            }
        }

        private void StorePropertyValue(DependencyObject dObj, DependencyProperty from, DependencyProperty to)
        {
            if (dObj.GetValue(to) == null)
            {
                dObj.SetValue(to, dObj.StorePropertyValue(from));
            }
        }

        protected void UnfocusTabChild(FrameworkElement child)
        {
            if (child != null)
            {
                FrameworkElement focusedElement = FocusManager.GetFocusedElement(FocusManager.GetFocusScope(child)) as FrameworkElement;
                if ((focusedElement != null) && focusedElement.FindIsInParents(child))
                {
                    this.TabControl.Focus();
                }
            }
        }

        protected virtual void UpdateActualGroupBoxDisplayMode()
        {
            DevExpress.Xpf.LayoutControl.GroupBoxDisplayMode mode = ((this.GroupBoxDisplayMode != DevExpress.Xpf.LayoutControl.GroupBoxDisplayMode.Default) || ReferenceEquals(this.Root, this)) ? this.GroupBoxDisplayMode : ((this.Root != null) ? this.Root.ActualGroupBoxDisplayMode : this.GroupBoxDisplayMode);
            this.ActualGroupBoxDisplayMode = mode;
        }

        protected virtual void UpdateCollapsedSize()
        {
            DependencyProperty dp = (this.CollapseDirection == System.Windows.Controls.Orientation.Horizontal) ? FrameworkElement.WidthProperty : FrameworkElement.HeightProperty;
            DependencyProperty property2 = (this.CollapseDirection == System.Windows.Controls.Orientation.Horizontal) ? FrameworkElement.MinWidthProperty : FrameworkElement.MinHeightProperty;
            if (this.IsActuallyCollapsed)
            {
                double d = (double) base.GetValue(dp);
                double num2 = (double) base.GetValue(property2);
                if (!double.IsNaN(d))
                {
                    this.StoredSize = d;
                    base.SetValue(dp, (double) 1.0 / (double) 0.0);
                }
                if (num2 != 0.0)
                {
                    this._StoredMinSizePropertyValue = this.StorePropertyValue(property2);
                    base.SetValue(property2, 0.0);
                }
            }
            if (!this.IsActuallyCollapsed)
            {
                if (this.IsPropertyAssigned(StoredSizeProperty))
                {
                    base.SetValue(dp, this.StoredSize);
                    base.ClearValue(StoredSizeProperty);
                }
                if (this._StoredMinSizePropertyValue != null)
                {
                    this.RestorePropertyValue(property2, this._StoredMinSizePropertyValue);
                    this._StoredMinSizePropertyValue = null;
                }
            }
        }

        private void UpdateDesiredAlignment()
        {
            HorizontalAlignment? nullable = this._lastDesiredHorizontalAlignment;
            HorizontalAlignment desiredHorizontalAlignment = this.DesiredHorizontalAlignment;
            if (!((((HorizontalAlignment) nullable.GetValueOrDefault()) == desiredHorizontalAlignment) ? (nullable == null) : true))
            {
                VerticalAlignment? nullable2 = this._lastDesiredVerticalAlignment;
                VerticalAlignment desiredVerticalAlignment = this.DesiredVerticalAlignment;
                if (!((((VerticalAlignment) nullable2.GetValueOrDefault()) == desiredVerticalAlignment) ? (nullable2 == null) : true))
                {
                    return;
                }
            }
            this._lastDesiredHorizontalAlignment = new HorizontalAlignment?(this.DesiredHorizontalAlignment);
            this._lastDesiredVerticalAlignment = new VerticalAlignment?(this.DesiredVerticalAlignment);
            LayoutGroup parent = base.Parent as LayoutGroup;
            if (parent != null)
            {
                parent.InvalidateMeasure();
                parent.UpdateDesiredAlignment();
            }
        }

        protected virtual void UpdateItemLabelsAlignment()
        {
            if (!base.IsArranging)
            {
                base.InvalidateArrange();
            }
        }

        protected override void UpdateOriginalDesiredSize(ref Size originalDesiredSize)
        {
            base.UpdateOriginalDesiredSize(ref originalDesiredSize);
            if (this.Border != null)
            {
                Thickness contentPadding = base.ContentPadding;
                originalDesiredSize.Width = Math.Max(originalDesiredSize.Width, (this.BorderMinSize.Width - this.BorderContentPadding.Left) - (contentPadding.Left + contentPadding.Right));
                originalDesiredSize.Height = Math.Max(originalDesiredSize.Height, (this.BorderMinSize.Height - this.BorderContentPadding.Top) - (contentPadding.Top + contentPadding.Bottom));
            }
        }

        protected void UpdateTab(FrameworkElement child, bool updateStyle)
        {
            if (this.HasTabs)
            {
                int tabIndex = this.GetTabIndex(child);
                if (tabIndex != -1)
                {
                    this.InitTab((DXTabItem) this.TabControl.Items[tabIndex], child, updateStyle);
                }
            }
        }

        protected void UpdateTabControlSelectedIndex()
        {
            if (this.HasTabs)
            {
                this.DisableTabControlSelectionChangedNotification = true;
                this.TabControl.SelectedIndex = Math.Min(Math.Max(0, this.SelectedTabIndex), this.TabControl.Items.Count - 1);
                this.DisableTabControlSelectionChangedNotification = false;
                this.CheckSelectedTabChildChanged();
            }
        }

        protected void UpdateTabs(bool updateStyle)
        {
            if (this.HasTabs && ((this.TabControl != null) && (this.TabControl.SelectedContainer != null)))
            {
                this.DisableTabControlSelectionChangedNotification = true;
                FrameworkElement tag = this.TabControl.SelectedContainer.Tag as FrameworkElement;
                this.InitTabs(updateStyle);
                if (this.KeepSelectionOnTabRemoval)
                {
                    this.SelectedTabIndex = base.GetLogicalChildren(true).IndexOf(tag);
                }
                this.DisableTabControlSelectionChangedNotification = false;
            }
        }

        protected override void WriteChildToXML(FrameworkElement child, XmlWriter xml)
        {
            if (child.GetType() == this.GetGroupType())
            {
                ((LayoutGroup) child).WriteToXML(xml);
            }
            else
            {
                base.WriteChildToXML(child, xml);
            }
        }

        protected override void WriteCustomizablePropertiesToXML(XmlWriter xml)
        {
            base.WriteCustomizablePropertiesToXML(xml);
            this.WritePropertyToXML(xml, HeaderProperty, "Header");
            this.WritePropertyToXML(xml, IsCollapsedProperty, "IsCollapsed");
            this.WritePropertyToXML(xml, IsLockedProperty, "IsLocked");
            this.WritePropertyToXML(xml, LayoutControlBase.ItemSpaceProperty, "ItemSpace");
            this.WritePropertyToXML(xml, OrientationProperty, "Orientation");
            this.WritePropertyToXML(xml, LayoutControlBase.PaddingProperty, "Padding");
            this.WritePropertyToXML(xml, SelectedTabIndexProperty, "SelectedTabIndex");
            this.WritePropertyToXML(xml, ViewProperty, "View");
            this.WritePropertyToXML(xml, KeepSelectionOnTabRemovalProperty, "KeepSelectedTab");
            this.WritePropertyToXML(xml, StoredSizeProperty, "StoredSize");
            if (!this.IsRoot)
            {
                this.WriteCustomizablePropertiesToXML(this, xml);
            }
            else
            {
                this.Root.WriteElementToXML(xml, this);
            }
        }

        protected override void WriteCustomizablePropertiesToXML(FrameworkElement element, XmlWriter xml)
        {
            base.WriteCustomizablePropertiesToXML(element, xml);
            element.WritePropertyToXML(xml, FrameworkElement.HorizontalAlignmentProperty, "HorizontalAlignment");
            element.WritePropertyToXML(xml, FrameworkElement.VerticalAlignmentProperty, "VerticalAlignment");
            element.WritePropertyToXML(xml, FrameworkElement.WidthProperty, "Width");
            element.WritePropertyToXML(xml, FrameworkElement.HeightProperty, "Height");
            if (element is LayoutItem)
            {
                ((LayoutItem) element).WriteCustomizablePropertiesToXML(xml);
            }
            ILayoutControl root = this.Root;
            if (root == null)
            {
                ILayoutControl local1 = root;
            }
            else
            {
                root.WriteElementToXML(xml, element);
            }
        }

        protected override void WriteToXMLCore(XmlWriter xml)
        {
            if (this.IsPermanent)
            {
                xml.WriteAttributeString("ID", this.GetXMLID(this));
            }
            base.WriteToXMLCore(xml);
        }

        [Description("Gets the group's actual horizontal alignment.")]
        public HorizontalAlignment ActualHorizontalAlignment =>
            this.IsPropertySet(FrameworkElement.HorizontalAlignmentProperty) ? base.HorizontalAlignment : this.DesiredHorizontalAlignment;

        [Description("Gets the group's actual vertical alignment.")]
        public VerticalAlignment ActualVerticalAlignment =>
            this.IsPropertySet(FrameworkElement.VerticalAlignmentProperty) ? base.VerticalAlignment : this.DesiredVerticalAlignment;

        [Description("Gets the group's actual minimum size.")]
        public Size ActualMinSize
        {
            get
            {
                Size minSize = this.GetMinSize();
                Size contentSize = this.LayoutProvider.GetActualMinOrMaxSize(base.GetLogicalChildren(true), true, this.CreateLayoutProviderParameters());
                SizeHelper.UpdateMaxSize(ref minSize, base.CalculateSizeFromContentSize(contentSize));
                return minSize;
            }
        }

        [Description("Gets the group's actual maximum size.")]
        public Size ActualMaxSize
        {
            get
            {
                Size maxSize = this.GetMaxSize();
                Size contentSize = this.LayoutProvider.GetActualMinOrMaxSize(base.GetLogicalChildren(true), false, this.CreateLayoutProviderParameters());
                SizeHelper.UpdateMinSize(ref maxSize, base.CalculateSizeFromContentSize(contentSize));
                return maxSize;
            }
        }

        [Description("This member supports the internal infrastructure, and is not intended to be used directly from your code.")]
        public LayoutGroupController Controller =>
            (LayoutGroupController) base.Controller;

        [Description("Gets or sets the style applied to the GroupBox that represents the current LayoutGroup when the LayoutGroup.View property is set to LayoutGroupView.GroupBox.This is a dependency property.")]
        public Style GroupBoxStyle
        {
            get => 
                (Style) base.GetValue(GroupBoxStyleProperty);
            set => 
                base.SetValue(GroupBoxStyleProperty, value);
        }

        [Description("Gets or sets the group's header.This is a dependency property."), XtraSerializableProperty]
        public object Header
        {
            get => 
                base.GetValue(HeaderProperty);
            set => 
                base.SetValue(HeaderProperty, value);
        }

        [Description("Gets or sets a data template used to display the group's header.This is a dependency property.")]
        public DataTemplate HeaderTemplate
        {
            get => 
                (DataTemplate) base.GetValue(HeaderTemplateProperty);
            set => 
                base.SetValue(HeaderTemplateProperty, value);
        }

        [Description("Gets whether the group is actually collapsed.")]
        public bool IsActuallyCollapsed =>
            this.HasGroupBox && (this.IsCollapsible && this.IsCollapsed);

        [Description("Gets or sets whether the group is collapsed.This is a dependency property."), XtraSerializableProperty]
        public bool IsCollapsed
        {
            get => 
                (bool) base.GetValue(IsCollapsedProperty);
            set => 
                base.SetValue(IsCollapsedProperty, value);
        }

        [Description("Gets or sets whether the group may be collapsed.This is a dependency property.")]
        public bool IsCollapsible
        {
            get => 
                (bool) base.GetValue(IsCollapsibleProperty);
            set => 
                base.SetValue(IsCollapsibleProperty, value);
        }

        [Description("Gets or sets whether the group is locked, and so items cannot be selected, moved within, into or outside the group.This is a dependency property."), XtraSerializableProperty]
        public bool IsLocked
        {
            get => 
                (bool) base.GetValue(IsLockedProperty);
            set => 
                base.SetValue(IsLockedProperty, value);
        }

        [Description("Gets whether the group remains alive during layout optimization.")]
        public bool IsPermanent =>
            this.GetIsPermanent(true);

        [Description("Gets whether the current object is a root object for layout elements.")]
        public virtual bool IsRoot =>
            false;

        [Description("Gets or sets how content regions of LayoutItems are aligned, according to their labels. This is a dependency property.")]
        public virtual LayoutItemLabelsAlignment ItemLabelsAlignment
        {
            get => 
                (LayoutItemLabelsAlignment) base.GetValue(ItemLabelsAlignmentProperty);
            set => 
                base.SetValue(ItemLabelsAlignmentProperty, value);
        }

        [Description("Gets or sets the style of layout items that belong to the current group and nested groups (provided that the nested group's ItemStyle property is not set).This is a dependency property.")]
        public Style ItemStyle
        {
            get => 
                (Style) base.GetValue(ItemStyleProperty);
            set => 
                base.SetValue(ItemStyleProperty, value);
        }

        [Description("In a tabbed UI, gets or sets if a measurement operation (computing control sizes and positioning) is performed for a tab only when this tab is selected. This is a dependency property.")]
        public bool MeasureSelectedTabChildOnly
        {
            get => 
                (bool) base.GetValue(MeasureSelectedTabChildOnlyProperty);
            set => 
                base.SetValue(MeasureSelectedTabChildOnlyProperty, value);
        }

        [Description("Gets or sets whether the group's items are arranged horizontally or vertically.This is a dependency property."), XtraSerializableProperty]
        public System.Windows.Controls.Orientation Orientation
        {
            get => 
                (System.Windows.Controls.Orientation) base.GetValue(OrientationProperty);
            set => 
                base.SetValue(OrientationProperty, value);
        }

        [Description("Gets the layout control that owns the current group.")]
        public ILayoutControl Root =>
            this.GetLayoutControl();

        [Description("Gets the currently selected tabbed child. This member is in effect when the current group is rendered as a tabbed group.")]
        public FrameworkElement SelectedTabChild
        {
            get
            {
                if (!this.HasTabs)
                {
                    return null;
                }
                FrameworkElements logicalChildren = base.GetLogicalChildren(true);
                return (((logicalChildren.Count == 0) || (this.TabControl.SelectedIndex == -1)) ? null : logicalChildren[this.TabControl.SelectedIndex]);
            }
        }

        [Description("Gets or sets the index of the selected tabbed child among visible children. This member is in effect when the current group is rendered as a tabbed group.This is a dependency property."), XtraSerializableProperty]
        public int SelectedTabIndex
        {
            get => 
                (int) base.GetValue(SelectedTabIndexProperty);
            set => 
                base.SetValue(SelectedTabIndexProperty, value);
        }

        [Description("Gets or sets a style applied to the group, when the group is represented as a tab container.This is a dependency property.")]
        public Style TabsStyle
        {
            get => 
                (Style) base.GetValue(TabsStyleProperty);
            set => 
                base.SetValue(TabsStyleProperty, value);
        }

        [Description("Gets or sets a style applied to the group's individual tabs, when the group is represented as a tab container.This is a dependency property.")]
        public Style TabStyle
        {
            get => 
                (Style) base.GetValue(TabStyleProperty);
            set => 
                base.SetValue(TabStyleProperty, value);
        }

        [Description("Gets or sets the LayoutGroup's visual style. This is a dependency property."), XtraSerializableProperty]
        public LayoutGroupView View
        {
            get => 
                (LayoutGroupView) base.GetValue(ViewProperty);
            set => 
                base.SetValue(ViewProperty, value);
        }

        public DevExpress.Xpf.LayoutControl.GroupBoxDisplayMode GroupBoxDisplayMode
        {
            get => 
                (DevExpress.Xpf.LayoutControl.GroupBoxDisplayMode) base.GetValue(GroupBoxDisplayModeProperty);
            set => 
                base.SetValue(GroupBoxDisplayModeProperty, value);
        }

        public DevExpress.Xpf.LayoutControl.GroupBoxDisplayMode ActualGroupBoxDisplayMode
        {
            get => 
                (DevExpress.Xpf.LayoutControl.GroupBoxDisplayMode) base.GetValue(ActualGroupBoxDisplayModeProperty);
            protected set => 
                base.SetValue(ActualGroupBoxDisplayModePropertyKey, value);
        }

        public bool KeepSelectionOnTabRemoval
        {
            get => 
                (bool) base.GetValue(KeepSelectionOnTabRemovalProperty);
            set => 
                base.SetValue(KeepSelectionOnTabRemovalProperty, value);
        }

        protected bool HasNewChildren { get; private set; }

        protected override bool NeedsChildChangeNotifications =>
            true;

        protected virtual FrameworkElement Border =>
            !this.HasGroupBox ? (!this.HasTabs ? null : ((FrameworkElement) this.TabControl)) : ((FrameworkElement) this.GroupBox);

        protected virtual BorderContentFiller BorderContent =>
            !this.HasGroupBox ? (!this.HasTabs ? null : this.TabControlContent) : this.GroupBoxContent;

        protected Thickness BorderContentPadding { get; set; }

        protected Size BorderMinSize { get; private set; }

        protected virtual LayoutGroupCollapseMode CollapseMode =>
            !this.IsActuallyCollapsed ? (!this.HasTabs ? LayoutGroupCollapseMode.None : LayoutGroupCollapseMode.OneChildVisible) : LayoutGroupCollapseMode.NoChildrenVisible;

        protected HorizontalAlignment DesiredHorizontalAlignment =>
            this.LayoutProvider.GetDesiredHorizontalAlignment(base.GetLogicalChildren(true));

        protected VerticalAlignment DesiredVerticalAlignment =>
            this.LayoutProvider.GetDesiredVerticalAlignment(base.GetLogicalChildren(true));

        protected virtual bool IsBorderContentVisible =>
            !this.HasGroupBox || !this.IsActuallyCollapsed;

        protected virtual bool IsBorderless =>
            this.View == LayoutGroupView.Group;

        protected virtual bool IsUIEmpty =>
            this.IsBorderless && (base.GetLogicalChildren(true).Count == 0);

        protected LayoutGroupProvider LayoutProvider =>
            (LayoutGroupProvider) base.LayoutProvider;

        protected virtual FrameworkElement UncollapsedChild =>
            this.HasTabs ? this.SelectedTabChild : null;

        protected System.Windows.Controls.Orientation CollapseDirection =>
            (System.Windows.Controls.Orientation) base.GetValue(CollapseDirectionProperty);

        protected DevExpress.Xpf.LayoutControl.GroupBox GroupBox { get; private set; }

        protected virtual bool HasGroupBox =>
            this.View == LayoutGroupView.GroupBox;

        private BorderContentFiller GroupBoxContent =>
            (BorderContentFiller) this.GroupBox.Content;

        protected bool DisableTabControlSelectionChangedNotification { get; set; }

        protected virtual bool HasTabs =>
            this.View == LayoutGroupView.Tabs;

        protected DXTabControl TabControl { get; private set; }

        private BorderContentFiller TabControlContent =>
            (BorderContentFiller) this.TabControl.SelectedItemContent;

        protected bool IsCustomization
        {
            get => 
                this._IsCustomization;
            set
            {
                if (this.IsCustomization != value)
                {
                    this._IsCustomization = value;
                    this.ForEachGroup(group => group.IsCustomization = this.IsCustomization);
                    this.OnIsCustomizationChanged();
                }
            }
        }

        protected virtual bool IsItemLabelsAlignmentScope =>
            (this.ItemLabelsAlignment != LayoutItemLabelsAlignment.Default) || (this.IsActuallyCollapsed || this.HasTabs);

        protected ElementSizers ItemSizers { get; private set; }

        protected Style ItemSizerStyle
        {
            get => 
                this.ItemSizers.ItemStyle;
            set
            {
                if (!ReferenceEquals(this.ItemSizerStyle, value))
                {
                    this.ItemSizers.ItemStyle = value;
                    this.ForEachGroup(group => group.ItemSizerStyle = this.ItemSizerStyle);
                }
            }
        }

        [XtraSerializableProperty]
        protected double StoredSize
        {
            get => 
                (double) base.GetValue(StoredSizeProperty);
            set => 
                base.SetValue(StoredSizeProperty, value);
        }

        protected virtual System.Windows.Controls.Orientation VisibleOrientation
        {
            get
            {
                if (!this.HasTabs)
                {
                    return this.Orientation;
                }
                HeaderLocation headerLocation = this.TabControl.View.HeaderLocation;
                return (((headerLocation == HeaderLocation.Top) || (headerLocation == HeaderLocation.Bottom)) ? System.Windows.Controls.Orientation.Horizontal : System.Windows.Controls.Orientation.Vertical);
            }
        }

        System.Windows.Controls.Orientation ILayoutGroupModel.CollapseDirection =>
            this.CollapseDirection;

        LayoutGroupCollapseMode ILayoutGroupModel.CollapseMode =>
            this.CollapseMode;

        bool ILayoutGroupModel.MeasureUncollapsedChildOnly =>
            this.MeasureSelectedTabChildOnly;

        System.Windows.Controls.Orientation ILayoutGroupModel.Orientation
        {
            get => 
                this.Orientation;
            set
            {
                if (this.Orientation != value)
                {
                    if (value == System.Windows.Controls.Orientation.Horizontal)
                    {
                        base.ClearValue(OrientationProperty);
                        if (this.Orientation == value)
                        {
                            return;
                        }
                    }
                    this.Orientation = value;
                }
            }
        }

        FrameworkElement ILayoutGroupModel.UncollapsedChild =>
            this.UncollapsedChild;

        Rect ILayoutGroup.ChildAreaBounds =>
            base.ContentBounds;

        HorizontalAlignment ILayoutGroup.DesiredHorizontalAlignment =>
            this.DesiredHorizontalAlignment;

        VerticalAlignment ILayoutGroup.DesiredVerticalAlignment =>
            this.DesiredVerticalAlignment;

        bool ILayoutGroup.HasNewChildren
        {
            get
            {
                bool flag;
                if (this.HasNewChildren)
                {
                    return true;
                }
                using (IEnumerator<FrameworkElement> enumerator = base.GetLogicalChildren(false).GetEnumerator())
                {
                    while (true)
                    {
                        if (enumerator.MoveNext())
                        {
                            FrameworkElement current = enumerator.Current;
                            if (!current.IsLayoutGroup() || !((ILayoutGroup) current).HasNewChildren)
                            {
                                continue;
                            }
                            flag = true;
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    }
                }
                return flag;
            }
        }

        bool ILayoutGroup.HasUniformLayout =>
            this.LayoutProvider.IsLayoutUniform(base.GetLogicalChildren(true));

        bool ILayoutGroup.IsBorderless =>
            this.IsBorderless;

        bool ILayoutGroup.IsCustomization
        {
            get => 
                this.IsCustomization;
            set => 
                this.IsCustomization = value;
        }

        bool ILayoutGroup.IsItemLabelsAlignmentScope =>
            this.IsItemLabelsAlignmentScope;

        bool ILayoutGroup.IsUIEmpty =>
            this.IsUIEmpty;

        Style ILayoutGroup.ItemSizerStyle
        {
            get => 
                this.ItemSizerStyle;
            set => 
                this.ItemSizerStyle = value;
        }

        int ILayoutGroup.SelectedTabIndex
        {
            get => 
                this.TabControl.SelectedIndex;
            set => 
                this.SelectedTabIndex = value;
        }

        System.Windows.Controls.Orientation ILayoutGroup.VisibleOrientation =>
            this.VisibleOrientation;

        bool ILayoutControlCustomizableItem.CanAddNewItems =>
            this.HasTabs;

        bool ILayoutControlCustomizableItem.HasHeader =>
            true;

        object ILayoutControlCustomizableItem.Header
        {
            get => 
                this.Header;
            set => 
                this.Header = value;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LayoutGroup.<>c <>9 = new LayoutGroup.<>c();
            public static Func<DevExpress.Xpf.LayoutControl.LayoutControl, bool> <>9__290_0;
            public static Func<bool> <>9__290_1;
            public static Action<ILayoutGroup> <>9__312_0;

            internal void <.cctor>b__20_0(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutGroup) o).OnPartStyleChanged(LayoutGroupPartStyle.GroupBox);
            }

            internal void <.cctor>b__20_1(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutGroup) o).OnHeaderChanged();
            }

            internal void <.cctor>b__20_10(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutGroup) o).OnPartStyleChanged(LayoutGroupPartStyle.Tabs);
            }

            internal void <.cctor>b__20_11(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutGroup) o).OnPartStyleChanged(LayoutGroupPartStyle.Tab);
            }

            internal void <.cctor>b__20_12(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutGroup) o).OnViewChanged();
            }

            internal void <.cctor>b__20_13(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutGroup) d).UpdateActualGroupBoxDisplayMode();
            }

            internal void <.cctor>b__20_14(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutGroup) d).InitGroupBoxDisplayMode();
            }

            internal void <.cctor>b__20_15(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutGroup) o).OnCollapseDirectionChanged();
            }

            internal void <.cctor>b__20_2(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutGroup) o).OnHeaderTemplateChanged();
            }

            internal void <.cctor>b__20_3(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutGroup) o).OnIsCollapsedChanged();
            }

            internal void <.cctor>b__20_4(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutGroup) o).OnIsCollapsibleChanged();
            }

            internal void <.cctor>b__20_5(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutGroup) o).OnItemLabelsAlignmentChanged();
            }

            internal void <.cctor>b__20_6(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutGroup) o).OnItemStyleChanged();
            }

            internal void <.cctor>b__20_7(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutGroup) o).OnMeasureSelectedTabChildOnlyChanged();
            }

            internal void <.cctor>b__20_8(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutGroup) o).OnOrientationChanged();
            }

            internal void <.cctor>b__20_9(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                if (((int) e.NewValue) < 0)
                {
                    o.SetValue(e.Property, e.OldValue);
                }
                else
                {
                    ((LayoutGroup) o).OnSelectedTabIndexChanged((int) e.OldValue);
                }
            }

            internal bool <FindByXMLID>b__290_0(DevExpress.Xpf.LayoutControl.LayoutControl x) => 
                x.AddRestoredItemsToAvailableItems;

            internal bool <FindByXMLID>b__290_1() => 
                true;

            internal void <OnAllowItemSizingChanged>b__312_0(ILayoutGroup group)
            {
                group.AllowItemSizingChanged();
            }
        }

        [CompilerGenerated]
        private sealed class <DevExpress-Xpf-LayoutControl-ILayoutGroup-GetDependencyPropertiesWithOverriddenDefaultValue>d__377 : IEnumerable<string>, IEnumerable, IEnumerator<string>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private string <>2__current;
            private int <>l__initialThreadId;

            [DebuggerHidden]
            public <DevExpress-Xpf-LayoutControl-ILayoutGroup-GetDependencyPropertiesWithOverriddenDefaultValue>d__377(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private bool MoveNext()
            {
                int num = this.<>1__state;
                if (num == 0)
                {
                    this.<>1__state = -1;
                    this.<>2__current = "Padding";
                    this.<>1__state = 1;
                    return true;
                }
                if (num == 1)
                {
                    this.<>1__state = -1;
                }
                return false;
            }

            [DebuggerHidden]
            IEnumerator<string> IEnumerable<string>.GetEnumerator()
            {
                LayoutGroup.<DevExpress-Xpf-LayoutControl-ILayoutGroup-GetDependencyPropertiesWithOverriddenDefaultValue>d__377 d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new LayoutGroup.<DevExpress-Xpf-LayoutControl-ILayoutGroup-GetDependencyPropertiesWithOverriddenDefaultValue>d__377(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<System.String>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            string IEnumerator<string>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }



        protected class BorderContentFiller : Control
        {
            private bool _IsEmpty;
            protected readonly Size EmptyDesiredSize = new Size(60.0, 30.0);

            public BorderContentFiller(LayoutGroup owner)
            {
                base.IsTabStop = false;
                this.Owner = owner;
            }

            protected override Size ArrangeOverride(Size arrangeBounds)
            {
                if (!this.Owner.IsArranging)
                {
                    this.Owner.InvalidateArrange();
                }
                return base.ArrangeOverride(arrangeBounds);
            }

            protected override Size MeasureOverride(Size availableSize)
            {
                this.AvailableSize = availableSize;
                if (!this.IsEmpty)
                {
                    return base.MeasureOverride(availableSize);
                }
                SizeHelper.UpdateMinSize(ref availableSize, this.EmptyDesiredSize);
                return availableSize;
            }

            public Size AvailableSize { get; private set; }

            public bool IsEmpty
            {
                get => 
                    this._IsEmpty;
                set
                {
                    if (this.IsEmpty != value)
                    {
                        this._IsEmpty = value;
                        FrameworkElement visualParent = this;
                        while (true)
                        {
                            visualParent.InvalidateMeasure();
                            visualParent = visualParent.GetVisualParent();
                            if ((visualParent == null) || ReferenceEquals(visualParent, this.Owner))
                            {
                                return;
                            }
                        }
                    }
                }
            }

            public LayoutGroup Owner { get; private set; }
        }
    }
}

