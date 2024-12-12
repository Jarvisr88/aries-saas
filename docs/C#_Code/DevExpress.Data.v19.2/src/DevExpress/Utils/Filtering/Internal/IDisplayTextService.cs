namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Runtime.InteropServices;

    public interface IDisplayTextService
    {
        string GetCaption();
        string GetDescription();
        string GetDisplayText(object value);
        string GetEditMask(out int maskType);
        object GetHtmlImages();

        DevExpress.Utils.Filtering.Internal.DisplayFormat DisplayFormat { get; }

        DevExpress.Utils.Filtering.Internal.AutoHeight AutoHeight { get; }
    }
}

