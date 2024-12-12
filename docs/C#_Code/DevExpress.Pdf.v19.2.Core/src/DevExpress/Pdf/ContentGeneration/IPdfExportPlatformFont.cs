namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using DevExpress.Text.Fonts;
    using System;
    using System.Drawing;

    public interface IPdfExportPlatformFont
    {
        DXCTLShaper CreateCTLShaper();
        Font CreateGDIPlusFont(float fontSize);
        bool ShouldEmbed(PdfCreationOptions creationOptions);

        DXFontDescriptor Descriptor { get; }

        DXFontSimulations Simulations { get; }

        PdfFontMetrics Metrics { get; }

        PdfFontFile FontFile { get; }
    }
}

