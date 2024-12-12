namespace DevExpress.Pdf.ContentGeneration
{
    using System;
    using System.Drawing;

    public class GDIExportFontProviderBuilder : IFontProviderBuilder, IDisposable
    {
        public IPdfExportPlatformFontProvider CreateProvider(Font nativeFont) => 
            new PdfExportGdiPlatformFontProvider(nativeFont);

        public void Dispose()
        {
        }
    }
}

