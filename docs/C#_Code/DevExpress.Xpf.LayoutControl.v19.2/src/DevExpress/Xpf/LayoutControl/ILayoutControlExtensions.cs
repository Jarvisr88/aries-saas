namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    internal static class ILayoutControlExtensions
    {
        private static void GetLayoutItems(FrameworkElements items, LayoutItems layoutItems)
        {
            foreach (FrameworkElement element in items)
            {
                if (element is LayoutItem)
                {
                    LayoutItem item = (LayoutItem) element;
                    layoutItems.Add(item);
                    continue;
                }
                if (element.IsLayoutGroup())
                {
                    GetLayoutItems(((ILayoutGroup) element).GetLogicalChildren(false), layoutItems);
                }
            }
        }

        internal static void RequestEditorValidation(this ILayoutControl layoutControl)
        {
            LayoutItems layoutItems = new LayoutItems();
            GetLayoutItems(layoutControl.GetLogicalChildren(false), layoutItems);
            foreach (LayoutItem item in layoutItems)
            {
                DataLayoutItem item1 = item as DataLayoutItem;
                if (item1 == null)
                {
                    DataLayoutItem local1 = item1;
                    continue;
                }
                item1.DoValidate();
            }
        }
    }
}

