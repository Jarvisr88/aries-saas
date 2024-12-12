namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Controls.Primitives;

    public class DxScrollBar : ScrollBar
    {
        public static readonly DependencyProperty CornerRadiusProperty;

        static DxScrollBar()
        {
            System.Windows.CornerRadius defaultValue = new System.Windows.CornerRadius();
            CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(System.Windows.CornerRadius), typeof(DxScrollBar), new FrameworkPropertyMetadata(defaultValue));
        }

        public System.Windows.CornerRadius CornerRadius
        {
            get => 
                (System.Windows.CornerRadius) base.GetValue(CornerRadiusProperty);
            set => 
                base.SetValue(CornerRadiusProperty, value);
        }
    }
}

