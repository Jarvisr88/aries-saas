namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Windows;
    using System.Windows.Media;

    public class ColorGalleryItem : GalleryItem
    {
        public static readonly DependencyProperty ColorProperty;
        public static readonly DependencyProperty HideBorderSideProperty;

        static ColorGalleryItem()
        {
            Type ownerType = typeof(ColorGalleryItem);
            ColorProperty = DependencyPropertyManager.Register("Color", typeof(System.Windows.Media.Color), ownerType);
            HideBorderSideProperty = DependencyPropertyManager.Register("HideBorderSide", typeof(DevExpress.Xpf.Bars.HideBorderSide), ownerType, new PropertyMetadata(DevExpress.Xpf.Bars.HideBorderSide.All));
        }

        public System.Windows.Media.Color Color
        {
            get => 
                (System.Windows.Media.Color) base.GetValue(ColorProperty);
            set => 
                base.SetValue(ColorProperty, value);
        }

        public DevExpress.Xpf.Bars.HideBorderSide HideBorderSide
        {
            get => 
                (DevExpress.Xpf.Bars.HideBorderSide) base.GetValue(HideBorderSideProperty);
            set => 
                base.SetValue(HideBorderSideProperty, value);
        }
    }
}

