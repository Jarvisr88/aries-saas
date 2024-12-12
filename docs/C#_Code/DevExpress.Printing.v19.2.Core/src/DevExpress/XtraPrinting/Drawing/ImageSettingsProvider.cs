namespace DevExpress.XtraPrinting.Drawing
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    internal class ImageSettingsProvider : IImageSettingsProvider
    {
        private readonly Dictionary<ImageSource, SizeF> dictionary = new Dictionary<ImageSource, SizeF>();

        public void Clear()
        {
            this.dictionary.Clear();
        }

        public SizeF GetResolutionImageSize(ImageSource imageSource, bool useImageResolution)
        {
            SizeF imageSize;
            if (!this.dictionary.TryGetValue(imageSource, out imageSize))
            {
                imageSize = imageSource.GetImageSize(useImageResolution);
                this.dictionary.Add(imageSource, imageSize);
            }
            return imageSize;
        }
    }
}

