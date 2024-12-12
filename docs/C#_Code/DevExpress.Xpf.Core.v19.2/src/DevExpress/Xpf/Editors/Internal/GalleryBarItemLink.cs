namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Bars;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class GalleryBarItemLink : BarItemLink
    {
        static GalleryBarItemLink()
        {
            BarItemLinkBase.UseLightweightTemplatesProperty.OverrideMetadata(typeof(GalleryBarItemLink), new FrameworkPropertyMetadata(false));
            BarItemLinkControlCreator.Default.RegisterObject(typeof(GalleryBarItemLink), typeof(GalleryBarItemLinkControl), (CreateObjectMethod<BarItemLinkControlBase>) (arg => new GalleryBarItemLinkControl((GalleryBarItemLink) arg)));
        }

        public GalleryBarItemLink()
        {
            base.useLightweightTemplatesEntry = false;
        }

        protected internal GalleryBarItem GalleryItem =>
            base.Item as GalleryBarItem;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly GalleryBarItemLink.<>c <>9 = new GalleryBarItemLink.<>c();

            internal BarItemLinkControlBase <.cctor>b__0_0(object arg) => 
                new GalleryBarItemLinkControl((GalleryBarItemLink) arg);
        }
    }
}

