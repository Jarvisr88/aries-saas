namespace DevExpress.Xpf.Printing
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ImageEditorOptions
    {
        public bool AllowLoadImage { get; set; }

        public bool AllowChangeSizeOptions { get; set; }

        public bool AllowDraw { get; set; }

        public bool AllowClear { get; set; }

        public ICollection<ImageGalleryItem> PredefinedImages { get; } = new List<ImageGalleryItem>()

        public bool AllowSearchPredefinedImages { get; set; }
    }
}

