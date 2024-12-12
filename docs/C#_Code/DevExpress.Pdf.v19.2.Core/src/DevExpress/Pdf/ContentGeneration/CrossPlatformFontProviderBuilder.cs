namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using DevExpress.Text.Fonts;
    using System;
    using System.Drawing;

    public class CrossPlatformFontProviderBuilder : IFontProviderBuilder, IDisposable
    {
        private readonly DXFontEngine fontEngine = DXFontEngine.CreatePlatformEngine();

        public IPdfExportPlatformFontProvider CreateProvider(Font nativeFont) => 
            new Provider(this.fontEngine, PdfFontNamesHelper.GetDescriptor(nativeFont.FontFamily.Name, (PdfFontStyle) nativeFont.Style), nativeFont.SizeInPoints);

        public void Dispose()
        {
            this.fontEngine.Dispose();
        }

        private class Provider : IPdfExportPlatformFontProvider
        {
            private readonly DXFontEngine fontEngine;
            private readonly DXFontDescriptor descriptor;
            private readonly float fontSize;

            public Provider(DXFontEngine fontEngine, DXFontDescriptor descriptor, float fontSize)
            {
                this.fontEngine = fontEngine;
                this.descriptor = descriptor;
                this.fontSize = fontSize;
            }

            public IPdfExportPlatformFont GetPlatformFont() => 
                ((IPdfExportPlatformFontProvider) new DXFont(this.fontEngine.SystemFontCollection.FindFirstMatchingFontFace(this.descriptor), this.fontSize)).GetPlatformFont();

            public object Key =>
                this.descriptor;
        }
    }
}

