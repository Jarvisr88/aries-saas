namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class GalleryBarItem : BarItem
    {
        public static readonly DependencyProperty GalleryProperty;

        static GalleryBarItem()
        {
            BarItem.UseLightweightTemplatesProperty.OverrideMetadata(typeof(GalleryBarItem), new FrameworkPropertyMetadata(false));
            BarItemLinkCreator.Default.RegisterObject(typeof(GalleryBarItem), typeof(GalleryBarItemLink), (CreateObjectMethod<BarItemLink>) (arg => new GalleryBarItemLink()));
            GalleryProperty = DependencyPropertyManager.Register("Gallery", typeof(DevExpress.Xpf.Bars.Gallery), typeof(GalleryBarItem), new PropertyMetadata(null));
        }

        public GalleryBarItem()
        {
            base.useLightweightTemplatesEntry = false;
        }

        public DevExpress.Xpf.Bars.Gallery Gallery
        {
            get => 
                (DevExpress.Xpf.Bars.Gallery) base.GetValue(GalleryProperty);
            set => 
                base.SetValue(GalleryProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly GalleryBarItem.<>c <>9 = new GalleryBarItem.<>c();

            internal BarItemLink <.cctor>b__1_0(object arg) => 
                new GalleryBarItemLink();
        }
    }
}

