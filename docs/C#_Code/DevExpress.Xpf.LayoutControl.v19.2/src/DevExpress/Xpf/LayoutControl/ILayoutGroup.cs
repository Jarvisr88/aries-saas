namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;

    public interface ILayoutGroup : ILayoutControlBase, IScrollControl, IPanel, IControl, ILayoutModelBase, ILayoutGroupModel, ILiveCustomizationAreasProvider
    {
        void AllowItemSizingChanged();
        void ApplyItemStyle();
        void ChildHorizontalAlignmentChanged(FrameworkElement child);
        void ChildVerticalAlignmentChanged(FrameworkElement child);
        void ClearItemStyle();
        void CopyTabHeaderInfo(FrameworkElement fromChild, FrameworkElement toElement);
        bool DesignTimeClick(DXMouseButtonEventArgs args);
        FrameworkElements GetArrangedLogicalChildren(bool visibleOnly);
        Rect GetClipBounds(FrameworkElement child, FrameworkElement relativeTo);
        IEnumerable<string> GetDependencyPropertiesWithOverriddenDefaultValue();
        LayoutItemInsertionInfo GetInsertionInfoForEmptyArea(FrameworkElement element, Point p);
        LayoutItemInsertionKind GetInsertionKind(FrameworkElement destinationItem, Point p);
        LayoutItemInsertionPoint GetInsertionPoint(FrameworkElement element, FrameworkElement destinationItem, LayoutItemInsertionKind insertionKind, Point p);
        Rect GetInsertionPointBounds(bool isInternalInsertion, FrameworkElement relativeTo);
        void GetInsertionPoints(FrameworkElement element, FrameworkElement destinationItem, FrameworkElement originalDestinationItem, LayoutItemInsertionKind insertionKind, LayoutItemInsertionPoints points);
        Rect GetInsertionPointZoneBounds(FrameworkElement destinationItem, LayoutItemInsertionKind insertionKind, int pointIndex, int pointCount);
        FrameworkElement GetItem(Point p, bool ignoreLayoutGroups, bool ignoreLocking);
        HorizontalAlignment GetItemHorizontalAlignment(FrameworkElement item);
        Style GetItemStyle();
        VerticalAlignment GetItemVerticalAlignment(FrameworkElement item);
        void GetLayoutItems(LayoutItems layoutItems);
        int GetTabIndex(Point absolutePosition);
        void InitChildFromGroup(FrameworkElement child, FrameworkElement group);
        void InsertElement(FrameworkElement element, LayoutItemInsertionPoint insertionPoint, LayoutItemInsertionKind insertionKind);
        bool IsChildBorderless(ILayoutGroup child);
        bool IsChildPermanent(ILayoutGroup child, bool keepTabs);
        bool IsExternalInsertionPoint(FrameworkElement element, FrameworkElement destinationItem, LayoutItemInsertionKind insertionKind);
        bool IsRemovableForOptimization(bool considerContent, bool keepEmptyTabs);
        void LayoutItemLabelsAlignmentChanged();
        void LayoutItemLabelWidthChanged(FrameworkElement layoutItem);
        bool MakeChildVisible(FrameworkElement child);
        ILayoutGroup MoveChildrenToNewGroup();
        bool MoveChildrenToParent();
        ILayoutGroup MoveChildToNewGroup(FrameworkElement child);
        void MoveNonUserDefinedChildrenToAvailableItems();
        bool OptimizeLayout(bool keepEmptyTabs);
        void SetItemHorizontalAlignment(FrameworkElement item, HorizontalAlignment value, bool updateWidth);
        void SetItemVerticalAlignment(FrameworkElement item, VerticalAlignment value, bool updateHeight);
        void UpdatePartStyle(LayoutGroupPartStyle style);

        HorizontalAlignment ActualHorizontalAlignment { get; }

        VerticalAlignment ActualVerticalAlignment { get; }

        Size ActualMinSize { get; }

        Size ActualMaxSize { get; }

        Rect ChildAreaBounds { get; }

        HorizontalAlignment DesiredHorizontalAlignment { get; }

        VerticalAlignment DesiredVerticalAlignment { get; }

        bool HasNewChildren { get; }

        bool HasUniformLayout { get; }

        object Header { get; }

        DataTemplate HeaderTemplate { get; }

        bool IsBorderless { get; }

        bool IsCollapsed { set; }

        bool IsCustomization { get; set; }

        bool IsItemLabelsAlignmentScope { get; }

        bool IsLocked { get; set; }

        bool IsRoot { get; }

        bool IsUIEmpty { get; }

        Style ItemSizerStyle { get; set; }

        ILayoutControl Root { get; }

        int SelectedTabIndex { get; set; }

        Orientation VisibleOrientation { get; }

        GroupBoxDisplayMode ActualGroupBoxDisplayMode { get; }
    }
}

