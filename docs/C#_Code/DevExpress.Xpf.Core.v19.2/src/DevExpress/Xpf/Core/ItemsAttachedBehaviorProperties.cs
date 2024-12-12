namespace DevExpress.Xpf.Core
{
    using System;
    using System.ComponentModel;
    using System.Windows;

    internal class ItemsAttachedBehaviorProperties
    {
        public static readonly DependencyProperty SourceProperty = DependencyProperty.RegisterAttached("Source", typeof(object), typeof(ItemsAttachedBehaviorProperties), new PropertyMetadata(null));

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static object GetSource(DependencyObject obj) => 
            obj.GetValue(SourceProperty);

        public static void SetSource(DependencyObject obj, object value)
        {
            obj.SetValue(SourceProperty, value);
        }
    }
}

