namespace DevExpress.Pdf.ContentGeneration
{
    using System;
    using System.Drawing;

    public interface IFontProviderBuilder : IDisposable
    {
        IPdfExportPlatformFontProvider CreateProvider(Font nativeFont);
    }
}

