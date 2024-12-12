namespace DevExpress.Xpf.Printing
{
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class ImageCollectionEditorInfo : ImageInplaceEditorInfo
    {
        public ImageCollectionEditorInfo(string editorName, IEnumerable<System.Drawing.Image> images, bool sizeOptionsEnabled, string editorDisplayName = null) : base(editorName, CreateEditorOptions(images, sizeOptionsEnabled), editorDisplayName)
        {
            Func<System.Drawing.Image, ImageGalleryItem> selector = <>c.<>9__14_0;
            if (<>c.<>9__14_0 == null)
            {
                Func<System.Drawing.Image, ImageGalleryItem> local1 = <>c.<>9__14_0;
                selector = <>c.<>9__14_0 = x => new ImageGalleryItem(x);
            }
            this.<ImageCollection>k__BackingField = images.Select<System.Drawing.Image, ImageGalleryItem>(selector).ToArray<ImageGalleryItem>();
            this.<SizeOptionsEnabled>k__BackingField = sizeOptionsEnabled;
        }

        public ImageCollectionEditorInfo(string editorName, IDictionary<string, System.Drawing.Image> images, bool searchEnabled, bool sizeOptionsEnabled, string editorDisplayName = null) : base(editorName, CreateEditorOptions(images, sizeOptionsEnabled, searchEnabled), editorDisplayName)
        {
            Func<KeyValuePair<string, System.Drawing.Image>, ImageGalleryItem> selector = <>c.<>9__15_0;
            if (<>c.<>9__15_0 == null)
            {
                Func<KeyValuePair<string, System.Drawing.Image>, ImageGalleryItem> local1 = <>c.<>9__15_0;
                selector = <>c.<>9__15_0 = x => new ImageGalleryItem(x.Value, x.Key);
            }
            this.<ImageCollection>k__BackingField = images.Select<KeyValuePair<string, System.Drawing.Image>, ImageGalleryItem>(selector).ToArray<ImageGalleryItem>();
            this.<SearchEnabled>k__BackingField = searchEnabled;
            this.<SizeOptionsEnabled>k__BackingField = sizeOptionsEnabled;
            this.<ShowCaption>k__BackingField = true;
        }

        private static ImageEditorOptions CreateEditorOptions(IEnumerable<System.Drawing.Image> images, bool sizeOptionsEnabled)
        {
            ImageEditorOptions options1 = new ImageEditorOptions();
            options1.AllowLoadImage = false;
            options1.AllowDraw = false;
            options1.AllowSearchPredefinedImages = false;
            options1.AllowChangeSizeOptions = sizeOptionsEnabled;
            ImageEditorOptions options = options1;
            images.ForEach<System.Drawing.Image>(delegate (System.Drawing.Image image) {
                options.PredefinedImages.Add(new ImageGalleryItem(image));
            });
            return options;
        }

        private static ImageEditorOptions CreateEditorOptions(IDictionary<string, System.Drawing.Image> images, bool sizeOptionsEnabled, bool searchEnabled)
        {
            ImageEditorOptions options1 = new ImageEditorOptions();
            options1.AllowLoadImage = false;
            options1.AllowDraw = false;
            options1.AllowChangeSizeOptions = sizeOptionsEnabled;
            options1.AllowSearchPredefinedImages = searchEnabled;
            ImageEditorOptions options = options1;
            images.ForEach<KeyValuePair<string, System.Drawing.Image>>(delegate (KeyValuePair<string, System.Drawing.Image> imageAndDisplayName) {
                options.PredefinedImages.Add(new ImageGalleryItem(imageAndDisplayName.Value, imageAndDisplayName.Key));
            });
            return options;
        }

        public IEnumerable<ImageGalleryItem> ImageCollection { get; }

        public bool SearchEnabled { get; }

        public bool SizeOptionsEnabled { get; }

        public bool ShowCaption { get; }

        internal override DevExpress.Xpf.Printing.EditingFieldType EditingFieldType =>
            DevExpress.Xpf.Printing.EditingFieldType.Image;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ImageCollectionEditorInfo.<>c <>9 = new ImageCollectionEditorInfo.<>c();
            public static Func<System.Drawing.Image, ImageGalleryItem> <>9__14_0;
            public static Func<KeyValuePair<string, System.Drawing.Image>, ImageGalleryItem> <>9__15_0;

            internal ImageGalleryItem <.ctor>b__14_0(System.Drawing.Image x) => 
                new ImageGalleryItem(x);

            internal ImageGalleryItem <.ctor>b__15_0(KeyValuePair<string, System.Drawing.Image> x) => 
                new ImageGalleryItem(x.Value, x.Key);
        }
    }
}

