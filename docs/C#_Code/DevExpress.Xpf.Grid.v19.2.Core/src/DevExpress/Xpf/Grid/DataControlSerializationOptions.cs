namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Windows;

    public class DataControlSerializationOptions
    {
        public static readonly DependencyProperty RemoveOldColumnsProperty = DependencyPropertyManager.RegisterAttached("RemoveOldColumns", typeof(bool), typeof(DataControlSerializationOptions), new PropertyMetadata(true));
        public static readonly DependencyProperty AddNewColumnsProperty = DependencyPropertyManager.RegisterAttached("AddNewColumns", typeof(bool), typeof(DataControlSerializationOptions), new PropertyMetadata(true));

        public static bool GetAddNewColumns(DependencyObject obj) => 
            (bool) obj.GetValue(AddNewColumnsProperty);

        public static bool GetRemoveOldColumns(DependencyObject obj) => 
            (bool) obj.GetValue(RemoveOldColumnsProperty);

        public static void SetAddNewColumns(DependencyObject obj, bool value)
        {
            obj.SetValue(AddNewColumnsProperty, value);
        }

        public static void SetRemoveOldColumns(DependencyObject obj, bool value)
        {
            obj.SetValue(RemoveOldColumnsProperty, value);
        }
    }
}

