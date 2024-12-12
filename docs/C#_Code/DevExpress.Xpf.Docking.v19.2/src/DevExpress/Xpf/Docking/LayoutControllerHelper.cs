namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public static class LayoutControllerHelper
    {
        public static int BoolComparer(object value1, object value2) => 
            (((bool) value1) == ((bool) value2)) ? 0 : 1;

        public static int CompareCaptionLocation(object captionLocation1, object captionLocation2)
        {
            bool flag = (((CaptionLocation) captionLocation1) == CaptionLocation.Default) || (((CaptionLocation) captionLocation1) == CaptionLocation.Left);
            return ((flag & ((((CaptionLocation) captionLocation2) == CaptionLocation.Default) || (((CaptionLocation) captionLocation2) == CaptionLocation.Left))) ? 0 : (Equals(captionLocation1, captionLocation2) ? 0 : 1));
        }

        public static int CompareGroupBorderStyle(object style1, object style2) => 
            (((GroupBorderStyle) style1) == ((GroupBorderStyle) style2)) ? 0 : 1;

        public static int CompareImageLocation(object imageLocation1, object imageLocation2) => 
            ((((ImageLocation) imageLocation1) == ImageLocation.AfterText) == (((ImageLocation) imageLocation2) == ImageLocation.AfterText)) ? 0 : 1;

        public static int CompareObjects(object ha1, object ha2) => 
            (ha1 == ha2) ? 0 : 1;

        public static int CompareOrientation(object orientation1, object orientation2) => 
            ((((Orientation) orientation1) == Orientation.Horizontal) == (((Orientation) orientation2) == Orientation.Horizontal)) ? 0 : 1;

        public static T CreateCommand<T>(DockLayoutManager container, BaseLayoutItem[] items) where T: LayoutControllerCommand, new()
        {
            ILayoutController layoutController = GetLayoutController(container);
            if (layoutController != null)
            {
                return layoutController.CreateCommand<T>(items);
            }
            return default(T);
        }

        private static ILayoutController GetLayoutController(DockLayoutManager container) => 
            container?.LayoutController;

        public static ILayoutController GetLayoutController(object obj)
        {
            DockLayoutManager container = obj as DockLayoutManager;
            if (container == null)
            {
                DependencyObject obj2 = obj as DependencyObject;
                container = (obj2 != null) ? DockLayoutManager.GetDockLayoutManager(obj2) : null;
            }
            return GetLayoutController(container);
        }

        public static object GetSameValue(DependencyProperty property, BaseLayoutItem[] items, Comparison<object> match)
        {
            if ((items == null) || (items.Length == 0))
            {
                return null;
            }
            object y = items[0].GetValue(property);
            for (int i = 1; i < items.Length; i++)
            {
                if (match(items[i].GetValue(property), y) != 0)
                {
                    return null;
                }
            }
            return y;
        }

        public static bool HasOnlyGroups(BaseLayoutItem[] items)
        {
            foreach (BaseLayoutItem item in items)
            {
                if (!(item is LayoutGroup))
                {
                    return false;
                }
            }
            return true;
        }
    }
}

