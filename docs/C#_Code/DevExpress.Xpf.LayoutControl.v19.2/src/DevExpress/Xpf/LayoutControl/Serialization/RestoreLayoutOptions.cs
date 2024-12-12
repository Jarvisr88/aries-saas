namespace DevExpress.Xpf.LayoutControl.Serialization
{
    using System;
    using System.Windows;

    public class RestoreLayoutOptions
    {
        public static readonly DependencyProperty AddNewItemsProperty = DependencyProperty.RegisterAttached("AddNewItems", typeof(bool), typeof(RestoreLayoutOptions), new PropertyMetadata(true));
        public static readonly DependencyProperty RemoveOldItemsProperty = DependencyProperty.RegisterAttached("RemoveOldItems", typeof(bool), typeof(RestoreLayoutOptions), new PropertyMetadata(true));

        public static bool GetAddNewItems(DependencyObject obj) => 
            (bool) obj.GetValue(AddNewItemsProperty);

        public static bool GetRemoveOldItems(DependencyObject obj) => 
            (bool) obj.GetValue(RemoveOldItemsProperty);

        public static void SetAddNewItems(DependencyObject obj, bool value)
        {
            obj.SetValue(AddNewItemsProperty, value);
        }

        public static void SetRemoveOldItems(DependencyObject obj, bool value)
        {
            obj.SetValue(RemoveOldItemsProperty, value);
        }
    }
}

