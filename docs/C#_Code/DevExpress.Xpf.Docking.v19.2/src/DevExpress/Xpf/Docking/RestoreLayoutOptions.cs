namespace DevExpress.Xpf.Docking
{
    using DevExpress.Utils.Serializing;
    using System;
    using System.Windows;

    public class RestoreLayoutOptions
    {
        public static readonly DependencyProperty AddNewPanelsProperty;
        public static readonly DependencyProperty RemoveOldPanelsProperty;
        public static readonly DependencyProperty AddNewLayoutControlItemsProperty;
        public static readonly DependencyProperty RemoveOldLayoutControlItemsProperty;
        public static readonly DependencyProperty AddNewLayoutGroupsProperty;
        public static readonly DependencyProperty RemoveOldLayoutGroupsProperty;
        public static readonly DependencyProperty FloatPanelsRestoreOffsetProperty;
        public static readonly DependencyProperty DockLayoutManagerRestoreOffsetProperty;

        static RestoreLayoutOptions()
        {
            DependencyPropertyRegistrator<RestoreLayoutOptions> registrator = new DependencyPropertyRegistrator<RestoreLayoutOptions>();
            registrator.RegisterAttached<bool>("AddNewPanels", ref AddNewPanelsProperty, true, null, null);
            registrator.RegisterAttached<bool>("RemoveOldPanels", ref RemoveOldPanelsProperty, true, null, null);
            registrator.RegisterAttached<bool>("AddNewLayoutControlItems", ref AddNewLayoutControlItemsProperty, true, null, null);
            registrator.RegisterAttached<bool>("RemoveOldLayoutControlItems", ref RemoveOldLayoutControlItemsProperty, true, null, null);
            registrator.RegisterAttached<bool>("AddNewLayoutGroups", ref AddNewLayoutGroupsProperty, false, null, null);
            registrator.RegisterAttached<bool>("RemoveOldLayoutGroups", ref RemoveOldLayoutGroupsProperty, false, null, null);
            registrator.RegisterAttached<Point>("FloatPanelsRestoreOffset", ref FloatPanelsRestoreOffsetProperty, new Point(double.NaN, double.NaN), null, null);
            registrator.RegisterAttached<Point>("DockLayoutManagerRestoreOffset", ref DockLayoutManagerRestoreOffsetProperty, new Point(double.NaN, double.NaN), null, null);
        }

        public static bool GetAddNewLayoutControlItems(DependencyObject obj) => 
            (bool) obj.GetValue(AddNewLayoutControlItemsProperty);

        public static bool GetAddNewLayoutGroups(DependencyObject obj) => 
            (bool) obj.GetValue(AddNewLayoutGroupsProperty);

        public static bool GetAddNewPanels(DependencyObject obj) => 
            (bool) obj.GetValue(AddNewPanelsProperty);

        [XtraSerializableProperty]
        public static Point GetDockLayoutManagerRestoreOffset(DependencyObject obj) => 
            (Point) obj.GetValue(DockLayoutManagerRestoreOffsetProperty);

        [XtraSerializableProperty]
        public static Point GetFloatPanelsRestoreOffset(DependencyObject obj) => 
            (Point) obj.GetValue(FloatPanelsRestoreOffsetProperty);

        public static bool GetRemoveOldLayoutControlItems(DependencyObject obj) => 
            (bool) obj.GetValue(RemoveOldLayoutControlItemsProperty);

        public static bool GetRemoveOldLayoutGroups(DependencyObject obj) => 
            (bool) obj.GetValue(RemoveOldLayoutGroupsProperty);

        public static bool GetRemoveOldPanels(DependencyObject obj) => 
            (bool) obj.GetValue(RemoveOldPanelsProperty);

        public static void SetAddNewLayoutControlItems(DependencyObject obj, bool value)
        {
            obj.SetValue(AddNewLayoutControlItemsProperty, value);
        }

        public static void SetAddNewLayoutGroups(DependencyObject obj, bool value)
        {
            obj.SetValue(AddNewLayoutGroupsProperty, value);
        }

        public static void SetAddNewPanels(DependencyObject obj, bool value)
        {
            obj.SetValue(AddNewPanelsProperty, value);
        }

        public static void SetDockLayoutManagerRestoreOffset(DependencyObject obj, Point value)
        {
            obj.SetValue(DockLayoutManagerRestoreOffsetProperty, value);
        }

        public static void SetFloatPanelsRestoreOffset(DependencyObject obj, Point value)
        {
            obj.SetValue(FloatPanelsRestoreOffsetProperty, value);
        }

        public static void SetRemoveOldLayoutControlItems(DependencyObject obj, bool value)
        {
            obj.SetValue(RemoveOldLayoutControlItemsProperty, value);
        }

        public static void SetRemoveOldLayoutGroups(DependencyObject obj, bool value)
        {
            obj.SetValue(RemoveOldLayoutGroupsProperty, value);
        }

        public static void SetRemoveOldPanels(DependencyObject obj, bool value)
        {
            obj.SetValue(RemoveOldPanelsProperty, value);
        }
    }
}

