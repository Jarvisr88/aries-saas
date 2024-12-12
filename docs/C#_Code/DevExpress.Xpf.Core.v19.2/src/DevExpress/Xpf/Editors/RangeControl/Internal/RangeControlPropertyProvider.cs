namespace DevExpress.Xpf.Editors.RangeControl.Internal
{
    using System;
    using System.Windows;

    public class RangeControlPropertyProvider : DependencyObject
    {
        public static readonly DependencyProperty InvertLeftThumbProperty;
        public static readonly DependencyProperty InvertRightThumbProperty;

        static RangeControlPropertyProvider()
        {
            Type ownerType = typeof(RangeControlPropertyProvider);
            InvertLeftThumbProperty = DependencyProperty.Register("InvertLeftThumb", typeof(bool), ownerType);
            InvertRightThumbProperty = DependencyProperty.Register("InvertRightThumb", typeof(bool), ownerType);
        }

        public bool InvertLeftThumb
        {
            get => 
                (bool) base.GetValue(InvertLeftThumbProperty);
            set => 
                base.SetValue(InvertLeftThumbProperty, value);
        }

        public bool InvertRightThumb
        {
            get => 
                (bool) base.GetValue(InvertRightThumbProperty);
            set => 
                base.SetValue(InvertRightThumbProperty, value);
        }
    }
}

