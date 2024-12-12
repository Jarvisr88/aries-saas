namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;

    public static class IntervalHelper
    {
        private static bool ContainsControlItemsHosts(LayoutGroup group) => 
            (group != null) && group.Items.ContainsOnlyControlItemsOrItsHosts();

        public static DependencyProperty GetTargetProperty(BaseLayoutItem item1, BaseLayoutItem item2)
        {
            if ((item1 != null) && (item2 != null))
            {
                if ((item1.ItemType == LayoutItemType.Group) && (item2.ItemType == LayoutItemType.Group))
                {
                    if ((HasAccent(item1) && (item1.IsControlItemsHost || ContainsControlItemsHosts(item1 as LayoutGroup))) || (HasAccent(item2) && (item2.IsControlItemsHost || ContainsControlItemsHosts(item2 as LayoutGroup))))
                    {
                        return LayoutGroup.ActualLayoutGroupIntervalProperty;
                    }
                    return LayoutGroup.ActualDockItemIntervalProperty;
                }
                if ((item1.ItemType == LayoutItemType.Panel) || (item2.ItemType == LayoutItemType.Panel))
                {
                    return LayoutGroup.ActualDockItemIntervalProperty;
                }
                if ((item1.ItemType == LayoutItemType.ControlItem) && (item2.ItemType == LayoutItemType.Group))
                {
                    return (HasAccent(item2) ? LayoutGroup.ActualLayoutGroupIntervalProperty : LayoutGroup.ActualLayoutItemIntervalProperty);
                }
                if ((item2.ItemType == LayoutItemType.ControlItem) && (item1.ItemType == LayoutItemType.Group))
                {
                    return (HasAccent(item1) ? LayoutGroup.ActualLayoutGroupIntervalProperty : LayoutGroup.ActualLayoutItemIntervalProperty);
                }
                if ((item1.ItemType == LayoutItemType.ControlItem) && (item2.ItemType == LayoutItemType.ControlItem))
                {
                    return LayoutGroup.ActualLayoutItemIntervalProperty;
                }
                if ((item1.ItemType == LayoutItemType.ControlItem) && IsFixed(item2))
                {
                    return LayoutGroup.ActualLayoutItemIntervalProperty;
                }
                if ((item2.ItemType == LayoutItemType.ControlItem) && IsFixed(item1))
                {
                    return LayoutGroup.ActualLayoutItemIntervalProperty;
                }
                if (IsFixed(item1) && IsFixed(item2))
                {
                    return LayoutGroup.ActualLayoutItemIntervalProperty;
                }
            }
            return LayoutGroup.ActualDockItemIntervalProperty;
        }

        private static bool HasAccent(BaseLayoutItem group)
        {
            bool? nullable = (bool?) group.GetValue(LayoutGroup.HasAccentProperty);
            return ((nullable == null) ? false : nullable.Value);
        }

        private static bool IsFixed(BaseLayoutItem item) => 
            (item is FixedItem) && (!(item is SeparatorItem) && !(item is LayoutSplitter));
    }
}

