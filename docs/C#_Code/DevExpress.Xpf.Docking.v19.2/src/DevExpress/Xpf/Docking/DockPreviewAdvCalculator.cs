namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking.Internal;
    using DevExpress.Xpf.Docking.VisualElements;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;

    internal static class DockPreviewAdvCalculator
    {
        private static int CalcInsertIndex(LayoutGroup targetGroup, BaseLayoutItem targetItem, InsertType insertType)
        {
            if (targetGroup == null)
            {
                return ((insertType == InsertType.Before) ? 0 : 1);
            }
            List<BaseLayoutItem> list = new List<BaseLayoutItem>();
            foreach (BaseLayoutItem item in targetGroup.Items)
            {
                if (LayoutItemsHelper.IsActuallyVisibleInTree(item))
                {
                    list.Add(item);
                }
            }
            return (!ReferenceEquals(targetGroup, targetItem) ? (list.IndexOf(targetItem) + ((insertType == InsertType.After) ? 1 : 0)) : ((insertType == InsertType.Before) ? 0 : list.Count));
        }

        private static List<GridLength> CreateGroupSpaces(LayoutGroup group, Orientation orientation)
        {
            List<GridLength> list = new List<GridLength>();
            foreach (BaseLayoutItem item in group.Items)
            {
                if (LayoutItemsHelper.IsActuallyVisibleInTree(item))
                {
                    list.Add((orientation == Orientation.Horizontal) ? item.ItemWidth : item.ItemHeight);
                }
            }
            return list;
        }

        private static GridLength CreateSpaceForDockItem(BaseLayoutItem item, Orientation orientation)
        {
            bool flag = orientation == Orientation.Horizontal;
            GridLength length = flag ? item.ItemWidth : item.ItemHeight;
            DockSituation lastDockSituation = item.GetLastDockSituation();
            if (lastDockSituation != null)
            {
                GridLength length2 = flag ? lastDockSituation.Width : lastDockSituation.Height;
                if (length2.IsAbsolute)
                {
                    length = length2;
                }
            }
            return length;
        }

        public static Rect DockPreviewItem(BaseLayoutItem dockItem, BaseLayoutItem targetItem, DockType type)
        {
            if ((type == DockType.Fill) && ((targetItem is LayoutPanel) || (targetItem is TabbedGroup)))
            {
                return ElementHelper.GetRect(GetManager(targetItem).GetViewElement(targetItem));
            }
            LayoutGroup actualTargetGroup = GetActualTargetGroup(targetItem, type);
            Orientation actualOrientation = ((type != DockType.Fill) || (actualTargetGroup == null)) ? type.ToOrientation() : actualTargetGroup.Orientation;
            List<GridLength> gridLengths = new List<GridLength>();
            if (actualTargetGroup == null)
            {
                gridLengths.Add((actualOrientation == Orientation.Horizontal) ? targetItem.ItemWidth : targetItem.ItemHeight);
            }
            else if (NeedResetSingleNestedItemLength(dockItem, actualTargetGroup))
            {
                gridLengths.Add(GetSingleNestedItemLength(actualTargetGroup, actualOrientation));
            }
            else
            {
                gridLengths.AddRange(CreateGroupSpaces(actualTargetGroup, actualOrientation));
            }
            Rect bounds = GetBounds(actualTargetGroup ?? targetItem);
            int index = CalcInsertIndex(actualTargetGroup, targetItem, type.ToInsertType());
            gridLengths.Insert(index, CreateSpaceForDockItem(dockItem, actualOrientation));
            return InflateInvisibleRect(MeasureSpaces(bounds, gridLengths, actualOrientation, index), type, bounds);
        }

        private static LayoutGroup GetActualTargetGroup(BaseLayoutItem targetItem, DockType dockType)
        {
            LayoutGroup targetGroup = targetItem as LayoutGroup;
            if ((targetGroup == null) || (targetGroup.IgnoreOrientation && (dockType != DockType.Fill)))
            {
                targetGroup = targetItem.Parent;
            }
            return GetGroupToDock(targetGroup, dockType);
        }

        private static Rect GetBounds(BaseLayoutItem item)
        {
            Thickness padding = new Thickness();
            IUIElement uIElement = item;
            FloatGroup group = item as FloatGroup;
            if (group != null)
            {
                uIElement = group.GetUIElement<FloatPanePresenter>();
                padding = ((FloatPanePresenter) uIElement).GetFloatingMargin();
            }
            Rect rect = ElementHelper.GetRect(GetManager(item).GetViewElement(uIElement));
            RectHelper.Deflate(ref rect, padding);
            return rect;
        }

        private static LayoutGroup GetGroupToDock(LayoutGroup targetGroup, DockType dockType) => 
            ((targetGroup?.Orientation == dockType.ToOrientation()) || (dockType == DockType.Fill)) ? targetGroup : null;

        private static DockLayoutManager GetManager(BaseLayoutItem item) => 
            item.GetDockLayoutManager();

        private static GridLength GetSingleNestedItemLength(LayoutGroup group, Orientation actualOrientation)
        {
            BaseLayoutItem item = group.Items[0];
            GridLength itemWidth = item.ItemWidth;
            GridLength itemHeight = item.ItemHeight;
            LayoutGroup parent = group.Parent;
            if (parent != null)
            {
                if (!parent.IgnoreOrientation && (parent.Orientation != Orientation.Horizontal))
                {
                    if (!itemHeight.IsAbsolute)
                    {
                        itemHeight = group.ItemHeight;
                    }
                }
                else if (!itemWidth.IsAbsolute)
                {
                    itemWidth = group.ItemWidth;
                }
            }
            return ((actualOrientation == Orientation.Horizontal) ? itemWidth : itemHeight);
        }

        private static Rect InflateInvisibleRect(Rect rect, DockType type, Rect targetBounds)
        {
            if ((rect.Width == 0.0) && (type.ToOrientation() == Orientation.Horizontal))
            {
                rect.Width = 10.0;
                if (type == DockType.Right)
                {
                    rect.X = targetBounds.Right - 10.0;
                }
            }
            if ((rect.Height == 0.0) && (type.ToOrientation() == Orientation.Vertical))
            {
                rect.Height = 10.0;
                if (type == DockType.Bottom)
                {
                    rect.Y = targetBounds.Bottom - 10.0;
                }
            }
            return rect;
        }

        private static unsafe Rect MeasureSpaces(Rect bounds, List<GridLength> gridLengths, Orientation orientation, int dockIndex)
        {
            Grid grid = new Grid();
            for (int i = 0; i < gridLengths.Count; i++)
            {
                FrameworkElement element = new FrameworkElement();
                if (orientation == Orientation.Horizontal)
                {
                    ColumnDefinition definition1 = new ColumnDefinition();
                    definition1.Width = gridLengths[i];
                    grid.ColumnDefinitions.Add(definition1);
                    Grid.SetColumn(element, i);
                }
                else
                {
                    RowDefinition definition2 = new RowDefinition();
                    definition2.Height = gridLengths[i];
                    grid.RowDefinitions.Add(definition2);
                    Grid.SetRow(element, i);
                }
                grid.Children.Add(element);
            }
            grid.Arrange(bounds);
            if (orientation == Orientation.Horizontal)
            {
                Rect* rectPtr1 = &bounds;
                rectPtr1.X += grid.ColumnDefinitions[dockIndex].Offset;
                bounds.Width = Math.Min(grid.ColumnDefinitions[dockIndex].ActualWidth, bounds.Width);
            }
            else
            {
                Rect* rectPtr2 = &bounds;
                rectPtr2.Y += grid.RowDefinitions[dockIndex].Offset;
                bounds.Height = Math.Min(grid.RowDefinitions[dockIndex].ActualHeight, bounds.Height);
            }
            return bounds;
        }

        private static bool NeedResetSingleNestedItemLength(BaseLayoutItem dockItem, LayoutGroup group)
        {
            if (group.IsPermanent || (group.Items.Count != 1))
            {
                return false;
            }
            if (!ReferenceEquals(PlaceHolderHelper.GetAffectedGroup(dockItem, PlaceHolderState.Docked), group))
            {
                return false;
            }
            bool hasPlaceHolders = group.PlaceHolderHelper.GetPlaceHolders().FirstOrDefault<PlaceHolder>(x => !ReferenceEquals(x.Owner, dockItem), null) != null;
            return DockControllerHelper.CanUnboxGroupWithSingleItem(group, GetManager(group), hasPlaceHolders);
        }
    }
}

