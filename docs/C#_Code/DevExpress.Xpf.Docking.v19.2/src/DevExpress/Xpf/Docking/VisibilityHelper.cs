namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Windows;

    public static class VisibilityHelper
    {
        public static bool ContainsNotCollapsedItems(LayoutGroup group)
        {
            bool flag;
            using (IEnumerator<BaseLayoutItem> enumerator = group.Items.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        BaseLayoutItem current = enumerator.Current;
                        if (current.Visibility == Visibility.Collapsed)
                        {
                            continue;
                        }
                        if (current is LayoutGroup)
                        {
                            LayoutGroup group2 = (LayoutGroup) current;
                            if ((group2.ItemType == LayoutItemType.Group) && ((group2.GroupBorderStyle == GroupBorderStyle.NoBorder) && (!group2.HasNotCollapsedItems && !group2.GetIsDocumentHost())))
                            {
                                continue;
                            }
                            if ((group2.ItemType == LayoutItemType.TabPanelGroup) && !group2.HasNotCollapsedItems)
                            {
                                continue;
                            }
                            if ((group2.ItemType == LayoutItemType.DocumentPanelGroup) && (!group2.HasNotCollapsedItems && (!group2.GetIsDocumentHost() && !((DocumentGroup) group2).ShowWhenEmpty)))
                            {
                                continue;
                            }
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

        public static Visibility Convert(bool isVisible, Visibility invisibleValue = 2) => 
            isVisible ? Visibility.Visible : invisibleValue;

        public static bool GetIsVisible(LayoutGroup group) => 
            (group.ParentPanel == null) ? GetIsVisible(group, group.IsVisibleCore) : group.ParentPanel.IsVisibleCore;

        public static bool GetIsVisible(UIElement element) => 
            element.IsVisible;

        public static bool GetIsVisible(BaseLayoutItem item, bool isVisible) => 
            (!isVisible || (item.Parent == null)) ? ((!isVisible || (!(item is LayoutGroup) || (((LayoutGroup) item).ParentPanel == null))) ? isVisible : GetIsVisible(item as LayoutGroup)) : GetIsVisible(item.Parent);

        public static bool HasVisibleItems(LayoutGroup group)
        {
            bool flag;
            if (group == null)
            {
                return false;
            }
            using (IEnumerator<BaseLayoutItem> enumerator = group.Items.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        BaseLayoutItem current = enumerator.Current;
                        if (!current.IsVisibleCore)
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
}

