namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Collections.Generic;

    public static class CaptionAlignHelper
    {
        public static double GetActualCaptionWidth(LayoutControlItem item)
        {
            double desiredCaptionWidth = item.DesiredCaptionWidth;
            return (((item.CaptionAlignMode == CaptionAlignMode.AutoSize) || ((item.CaptionLocation == CaptionLocation.Top) || (item.CaptionLocation == CaptionLocation.Bottom))) ? desiredCaptionWidth : ((item.CaptionAlignMode != CaptionAlignMode.Custom) ? ((item.Parent == null) ? desiredCaptionWidth : GetActualCaptionWidth(GetAffectedItems(item, item.CaptionAlignMode), desiredCaptionWidth)) : (double.IsNaN(item.CaptionWidth) ? desiredCaptionWidth : item.CaptionWidth)));
        }

        private static double GetActualCaptionWidth(LayoutControlItem[] items, double value)
        {
            double num = value;
            for (int i = 0; i < items.Length; i++)
            {
                LayoutControlItem item = items[i];
                if (!double.IsNaN(item.CaptionWidth))
                {
                    num = Math.Max(num, item.CaptionWidth);
                }
                if (item.HasDesiredCaptionWidth)
                {
                    num = Math.Max(num, item.DesiredCaptionWidth);
                }
            }
            return num;
        }

        private static LayoutControlItem[] GetAffectedItems(LayoutControlItem item, CaptionAlignMode mode)
        {
            if (!HasAffectedItems(item, mode))
            {
                return new LayoutControlItem[0];
            }
            List<LayoutControlItem> list = new List<LayoutControlItem>();
            using (IEnumerator<BaseLayoutItem> enumerator = LayoutItemsHelper.GetEnumerator(GetAlignRoot(item), itemToAlign => itemToAlign.CaptionAlignMode == mode))
            {
                while (enumerator.MoveNext())
                {
                    LayoutControlItem current = enumerator.Current as LayoutControlItem;
                    if ((current != null) && current.IsVisibleCore)
                    {
                        list.Add(current);
                    }
                }
            }
            return list.ToArray();
        }

        private static LayoutGroup GetAlignRoot(LayoutControlItem item)
        {
            LayoutGroup parent = item.Parent;
            while ((parent != null) && (parent.CaptionAlignMode == CaptionAlignMode.Default))
            {
                parent = parent.Parent;
            }
            return (parent ?? item.GetRoot());
        }

        internal static LayoutControlItem[] GetAllAffectedItems(LayoutControlItem item, CaptionAlignMode mode)
        {
            List<LayoutControlItem> list = new List<LayoutControlItem>();
            using (IEnumerator<BaseLayoutItem> enumerator = LayoutItemsHelper.GetEnumerator(GetAlignRoot(item)))
            {
                while (enumerator.MoveNext())
                {
                    LayoutControlItem current = enumerator.Current as LayoutControlItem;
                    if ((current != null) && current.IsVisibleCore)
                    {
                        list.Add(current);
                    }
                }
            }
            return list.ToArray();
        }

        internal static double GetCaptionWidth(BaseLayoutItem item) => 
            GetCaptionWidth(item, item.CaptionWidth);

        public static double GetCaptionWidth(BaseLayoutItem item, double value) => 
            (!double.IsNaN(value) || (item.Parent == null)) ? value : GetCaptionWidth(item.Parent);

        internal static double GetTabCaptionWidth(BaseLayoutItem item) => 
            GetTabCaptionWidth(item, item.TabCaptionWidth);

        public static double GetTabCaptionWidth(BaseLayoutItem item, double value) => 
            (!double.IsNaN(value) || (item.Parent == null)) ? value : GetTabCaptionWidth(item.Parent);

        public static bool HasAffectedItems(LayoutControlItem item, CaptionAlignMode mode) => 
            (item.Parent != null) && ((mode == CaptionAlignMode.AlignInGroup) || (mode == CaptionAlignMode.Default));

        public static void UpdateAffectedItems(LayoutControlItem item, CaptionAlignMode mode)
        {
            if (HasAffectedItems(item, mode))
            {
                UpdateAffectedItemsCore(GetAllAffectedItems(item, mode));
            }
        }

        public static void UpdateAffectedItems(LayoutControlItem item, CaptionAlignMode oldMode, CaptionAlignMode newMode)
        {
            LayoutControlItem[] affectedItems = GetAffectedItems(item, oldMode);
            UpdateAffectedItemsCore(affectedItems);
            foreach (LayoutControlItem item2 in GetAffectedItems(item, newMode))
            {
                if (Array.IndexOf<LayoutControlItem>(affectedItems, item2) == -1)
                {
                    item2.CoerceValue(BaseLayoutItem.ActualCaptionWidthProperty);
                }
            }
        }

        private static void UpdateAffectedItemsCore(LayoutControlItem[] affectedItems)
        {
            for (int i = 0; i < affectedItems.Length; i++)
            {
                if (affectedItems[i].HasDesiredCaptionWidth)
                {
                    affectedItems[i].CoerceValue(BaseLayoutItem.ActualCaptionWidthProperty);
                }
            }
        }
    }
}

