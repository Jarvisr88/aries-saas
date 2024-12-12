namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Windows;

    public class TableViewProperties
    {
        public static readonly DependencyProperty FixedAreaStyleProperty = DependencyPropertyManager.RegisterAttached("FixedAreaStyle", typeof(FixedStyle), typeof(TableViewProperties), new FrameworkPropertyMetadata(FixedStyle.None, FrameworkPropertyMetadataOptions.Inherits));

        public static FixedStyle GetFixedAreaStyle(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return (FixedStyle) element.GetValue(FixedAreaStyleProperty);
        }

        public static void SetFixedAreaStyle(DependencyObject element, FixedStyle value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(FixedAreaStyleProperty, value);
        }
    }
}

