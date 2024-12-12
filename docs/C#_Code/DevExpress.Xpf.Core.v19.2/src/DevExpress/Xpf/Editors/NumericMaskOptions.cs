namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Windows;

    public class NumericMaskOptions : DependencyObject
    {
        public static readonly DependencyProperty AlwaysShowDecimalSeparatorProperty;

        static NumericMaskOptions()
        {
            Type ownerType = typeof(NumericMaskOptions);
            AlwaysShowDecimalSeparatorProperty = DependencyProperty.RegisterAttached("AlwaysShowDecimalSeparator", typeof(bool), ownerType, new UIPropertyMetadata(true, new PropertyChangedCallback(NumericMaskOptions.AlwaysShowDecimalSeparatorPropertyChanged)));
        }

        private static void AlwaysShowDecimalSeparatorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextEdit edit = d as TextEdit;
            if (edit != null)
            {
                ((TextEditPropertyProvider) edit.PropertyProvider).SetAlwaysShowDecimalSeparator((bool) e.NewValue);
            }
        }

        public static bool GetAlwaysShowDecimalSeparator(DependencyObject d) => 
            (bool) d.GetValue(AlwaysShowDecimalSeparatorProperty);

        public static void SetAlwaysShowDecimalSeparator(DependencyObject d, bool value)
        {
            d.SetValue(AlwaysShowDecimalSeparatorProperty, value);
        }
    }
}

