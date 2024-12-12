namespace DevExpress.Pdf.ContentGeneration
{
    using System;

    public interface IPdfExportPlatformFontProvider
    {
        IPdfExportPlatformFont GetPlatformFont();

        object Key { get; }
    }
}

