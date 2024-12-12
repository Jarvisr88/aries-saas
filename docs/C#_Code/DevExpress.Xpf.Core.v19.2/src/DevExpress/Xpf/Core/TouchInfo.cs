namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class TouchInfo : DependencyObject
    {
        public static readonly DependencyProperty MarginProperty;
        public static readonly DependencyProperty PaddingProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private DependencyProperty targetProperty;

        static TouchInfo()
        {
            Type ownerType = typeof(TouchInfo);
            MarginProperty = DependencyPropertyManager.RegisterAttached("Margin", typeof(TouchInfo), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(TouchInfo.OnTouchPropertyChanged)));
            PaddingProperty = DependencyPropertyManager.RegisterAttached("Padding", typeof(TouchInfo), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(TouchInfo.OnTouchPropertyChanged)));
        }

        public static TouchInfo GetMargin(DependencyObject d) => 
            (TouchInfo) d.GetValue(MarginProperty);

        public static TouchInfo GetPadding(DependencyObject d) => 
            (TouchInfo) d.GetValue(PaddingProperty);

        public static void OnTouchPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        public static void SetMargin(DependencyObject d, TouchInfo value)
        {
            d.SetValue(MarginProperty, value);
        }

        public static void SetPadding(DependencyObject d, TouchInfo value)
        {
            d.SetValue(PaddingProperty, value);
        }

        public Thickness Value { get; set; }

        public Thickness TouchValue { get; set; }

        public double? Scale { get; set; }

        public DependencyProperty TargetProperty
        {
            get => 
                this.targetProperty;
            set => 
                this.targetProperty = value;
        }
    }
}

