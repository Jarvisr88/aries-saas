namespace DevExpress.Xpf.LayoutControl
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class LayoutItemCustomizationToolbarButton : Button
    {
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(System.Windows.CornerRadius), typeof(LayoutItemCustomizationToolbarButton), null);

        public LayoutItemCustomizationToolbarButton()
        {
            base.DefaultStyleKey = typeof(LayoutItemCustomizationToolbarButton);
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

