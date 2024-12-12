namespace DevExpress.Xpf.Printing
{
    using DevExpress.Utils;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class ImageGalleryItem : ImmutableObject
    {
        public ImageGalleryItem(System.Drawing.Image image)
        {
            Guard.ArgumentNotNull(image, "image");
            this.<Image>k__BackingField = image;
        }

        public ImageGalleryItem(System.Drawing.Image image, string displayName) : this(image)
        {
            Guard.ArgumentIsNotNullOrEmpty(displayName, "displayName");
            this.<DisplayName>k__BackingField = displayName;
        }

        public System.Drawing.Image Image { get; }

        public string DisplayName { get; }
    }
}

