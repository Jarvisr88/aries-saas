namespace DevExpress.Xpf.Editors.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class DXSeparator : Separator
    {
        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation", typeof(System.Windows.Controls.Orientation), typeof(DXSeparator), new PropertyMetadata(System.Windows.Controls.Orientation.Horizontal));

        static DXSeparator()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(DXSeparator), new FrameworkPropertyMetadata(typeof(DXSeparator)));
        }

        public System.Windows.Controls.Orientation Orientation
        {
            get => 
                (System.Windows.Controls.Orientation) base.GetValue(OrientationProperty);
            set => 
                base.SetValue(OrientationProperty, value);
        }
    }
}

