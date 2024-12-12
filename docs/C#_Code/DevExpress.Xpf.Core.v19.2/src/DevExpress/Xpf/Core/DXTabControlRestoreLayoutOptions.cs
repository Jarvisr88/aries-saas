namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;

    public static class DXTabControlRestoreLayoutOptions
    {
        public static readonly DependencyProperty AddNewTabsProperty = DependencyProperty.RegisterAttached("AddNewTabs", typeof(bool), typeof(DXTabControlRestoreLayoutOptions), new PropertyMetadata(true));
        public static readonly DependencyProperty RemoveOldTabsProperty = DependencyProperty.RegisterAttached("RemoveOldTabs", typeof(bool), typeof(DXTabControlRestoreLayoutOptions), new PropertyMetadata(true));

        public static bool GetAddNewTabs(DXTabControl obj) => 
            (bool) obj.GetValue(AddNewTabsProperty);

        public static bool GetRemoveOldTabs(DXTabControl obj) => 
            (bool) obj.GetValue(RemoveOldTabsProperty);

        public static void SetAddNewTabs(DXTabControl obj, bool value)
        {
            obj.SetValue(AddNewTabsProperty, value);
        }

        public static void SetRemoveOldTabs(DXTabControl obj, bool value)
        {
            obj.SetValue(RemoveOldTabsProperty, value);
        }
    }
}

