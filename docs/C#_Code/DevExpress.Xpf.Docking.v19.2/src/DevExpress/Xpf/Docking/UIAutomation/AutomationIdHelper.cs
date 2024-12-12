namespace DevExpress.Xpf.Docking.UIAutomation
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Windows;

    internal static class AutomationIdHelper
    {
        public static string GetId(object owner) => 
            $"{owner.GetType().Name}({owner.GetHashCode()})";

        public static string GetId(string prefix, object owner) => 
            $"{prefix}_{GetId(owner)}";

        public static string GetIdByLayoutItem(DependencyObject dObject)
        {
            BaseLayoutItem owner = (dObject as BaseLayoutItem) ?? DockLayoutManager.GetLayoutItem(dObject);
            return ((owner != null) ? (string.IsNullOrEmpty(owner.Name) ? (string.IsNullOrEmpty(owner.ActualCaption) ? GetId(dObject.GetType().Name, owner) : owner.ActualCaption) : owner.Name) : GetId(dObject));
        }

        public static string GetLayoutItemName(BaseLayoutItem item, string defaultName) => 
            (item != null) ? (string.IsNullOrEmpty(item.ActualCaption) ? (string.IsNullOrEmpty(item.Name) ? defaultName : item.Name) : item.ActualCaption) : defaultName;
    }
}

