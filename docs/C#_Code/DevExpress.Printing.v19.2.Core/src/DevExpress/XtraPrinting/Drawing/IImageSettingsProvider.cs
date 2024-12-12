namespace DevExpress.XtraPrinting.Drawing
{
    using System;
    using System.Drawing;

    internal interface IImageSettingsProvider
    {
        void Clear();
        SizeF GetResolutionImageSize(ImageSource img, bool useImageResolution);
    }
}

