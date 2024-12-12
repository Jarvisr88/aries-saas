namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using DevExpress.Xpf.Grid.Hierarchy;
    using System;
    using System.Windows;

    public class DataViewDragDropManagerHelper
    {
        public static readonly DependencyProperty IsAttachedProperty = DependencyProperty.RegisterAttached("IsAttached", typeof(bool), typeof(DataViewDragDropManagerHelper), new PropertyMetadata(false));

        public static bool GetIsAttached(DependencyObject obj) => 
            (bool) obj.GetValue(IsAttachedProperty);

        public static HierarchyPanel GetPanel(DataViewBase view) => 
            view.DataPresenter.Panel;

        public static void SetIsAttached(DependencyObject obj, bool value)
        {
            obj.SetValue(IsAttachedProperty, value);
        }
    }
}

